using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twinkle
{
    /// <summary>
    /// Single responsible Flat file handler
    /// </summary>
    class TextReaderHelper
    {

       

            public static Match match1;
        private static FileInfo[] files;

        public static void ValidateLoadCSVFile(string path)
            {
            try
            {
                bool FileValidflag = false;
                // Program.log.Info("Validating Event input File");

                DirectoryInfo di = new DirectoryInfo(path);
                files = di.GetFiles("*.csv");

                if (files.Length == 0)
                {
                    FileValidflag = false;
                    //Program.log.Info("No File Found");
                }
              
                
                else
                {
                    string testString = @"^userdata_20\d{2}(?:-\d{2}){2}.csv$";
                   
                    Regex fileRegex1 = new Regex(testString);
                    

                    match1 = fileRegex1.Match(Convert.ToString(files[0]));

                    if (!match1.Success)
                    {
                        
                    }
                    else
                    {
                        //File Name Validation Check 

                        

                        DataSet ds = new DataSet();

                        //"Getting data from .CSV file

                       
                            using (OleDbConnection conn = new OleDbConnection())
                            {
                                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"/userdata/xls" + "; " + "Extended Properties='Text;HDR=YES;FMT=Delimited;'";
                                SqlCommand SqlComm = new SqlCommand();


                                using (OleDbCommand comm = new OleDbCommand())
                                {
                                    comm.CommandText = "Select * from " + "[" + System.IO.Path.GetFileName(Convert.ToString(files[0])) + "]";
                                    comm.Connection = conn;
                                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                                    {
                                        da.SelectCommand = comm;
                                        da.Fill(ds);
                                    }
                                }
                            }

                           
                            SqlBulkCopy bulk = new SqlBulkCopy(SqlHelper.sqlConnection1);
                            bulk.DestinationTableName = "Users";
                            foreach (DataColumn col in ds.Tables[0].Columns)
                                bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            bulk.WriteToServer(ds.Tables[0]);

                         //File data Copy to  DB table 

                           
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelperLib.LogHelper.Log(LogHelperLib.LogTarget.File, ex.Message.ToString());

            }
        }


       


         


            

           


            public static void GenerateReport()
            {
            try
            {
                int excelflag = 0;


                {
                    

                    
                        using (SqlHelper.sqlConnection1)
                        {

                            SqlDataAdapter da = new SqlDataAdapter("Select report From Users", SqlHelper.sqlConnection1); // query needs to update here
                            DataSet ds = new DataSet();
                            da.Fill(ds);


                            if (!(ds.Tables[0].Rows.Count == 0))
                            {
                                excelflag = 1;
                            }
                            else
                            {
                                excelflag = 0;
                            }

                            //Generating Excel File

                            if (excelflag == 1)
                               DstoOpenXML(ds, "UserPurchase history");
                        }
                   
                }

            }
            catch (Exception ex)
            {
                // Log file exception 
               
            }
        }


        

            public static void DstoOpenXML(DataSet ds, string sheet)
            {
            try
            {

               // Program.log.Info("in open xml function");

                string file = Application.StartupPath + @"/report.xlsx";
                var fileinfo = new FileInfo(file);

              

                using (ExcelPackage pck = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheet);
                    ws.Cells[1, 1, 1, 9].Style.Font.Bold = true;
                    ws.Cells[1, 7, 10000, 9].Style.Numberformat.Format = "mm/dd/yyyy";
                    ws.Cells["A1"].LoadFromDataTable(ds.Tables[0], true);

                    ws.Cells.AutoFitColumns();

                    pck.Save();
                    pck.Dispose();
                }

                //Excel generated

            }
            catch (Exception ex)
            {
               // Log exception
            }
        }


            

        public static string IniReader(string usr)
        {
            string passc = string.Empty;

            INIFile intex = new INIFile();
           passc= intex.ReadINI("Users", usr);
            return passc;
        }
    }
    class INIFile
    {
        private string filePath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key,
        string val,
        string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key,
        string def,
        StringBuilder retVal,
        int size,
        string filePath);

        public INIFile()
        {
            this.filePath = Application.StartupPath + @"\UsersDetails.ini";
        }

        public void WriteINI(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value.ToLower(), this.filePath);
        }

        public string ReadINI(string section, string key)
        {
            StringBuilder SB = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
   //         WriteINI(section, "storekeeper", "store123");
            return SB.ToString();
        }

        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }
}
