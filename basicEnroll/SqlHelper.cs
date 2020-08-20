using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Data.Common;

namespace Twinkle
{
    /// <summary>
    /// Single Responsibality SQL Handler
    /// </summary>
    class SqlHelper
    {



        public static System.Data.SqlClient.SqlConnection sqlConnection1  = Connection.connect(); 
      

        public static bool blankissue = false;
        public static List<int> a = new List<int>();
        public static List<int> b = new List<int>();
        public static Regex r = new Regex(@"\s+");

        
       



        public static void Run_Query(string s)
        {
            try
            {
                

                {
                    using (SqlCommand command = new SqlCommand
                    ("select * from users", sqlConnection1))
                    {
                        using (SqlDataReader reader1 = command.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                if (reader1[0].Equals(DBNull.Value))
                                {
                                    a.Add(0);
                                }
                                else if (Convert.ToString(reader1[0]).StartsWith(" ") || Convert.ToString(reader1[0]).Contains(" "))
                                {
                                    blankissue = true;
                                }
                                else
                                {
                                    a.Add(Convert.ToInt32(reader1[0]));
                                }
                                //Console.WriteLine(reader1[0].ToString());
                            }

                            while (reader1.NextResult())
                            {
                                while (reader1.Read())
                                {
                                    if (reader1[0].Equals(DBNull.Value))
                                    {
                                        a.Add(0);
                                    }
                                    else if (Convert.ToString(reader1[0]).StartsWith(" ") || Convert.ToString(reader1[0]).Contains(" "))
                                    {
                                        blankissue = true;
                                    }
                                    else
                                    {
                                        a.Add(Convert.ToInt32(reader1[0]));
                                    }
                                }
                            }
                        }
                    }
                }
                //sqlConnection1.Close();
            }
            catch (Exception ex)
            {
               
            }
        }


       

    

        /// <summary>
        /// Bulk insert from 
        /// </summary>
        public static void bulkInsert()
        {
           
            SqlBulkCopy bulk1 = new SqlBulkCopy(sqlConnection1);
            //bulk1.DestinationTableName = "UserReport";
            //foreach (DataColumn col in ex1.Columns)
            //    bulk1.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            //bulk1.WriteToServer(ex1);
        }

        /// <summary>
        /// check if user exits
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool UserExists(string user,string phone,out int id)
        {

            string queryString = "select id from customer where customer_name = " + user + " OR+ phone_number = " + phone;
            id = 0;
            SqlCommand command = new SqlCommand(queryString, sqlConnection1);


            int count = (int)command.ExecuteScalar();
            if (count <= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
            }
           
        
        public static bool SearchUser(string user, string phone,out DataTable dt)
        {
            dt = null;
            bool retval = false;
            try
            {
                // write the sql statement to execute    

                string queryString = "select id from customer where customer_name = " + user + " OR+ phone_number = " + phone;
                int id = 0;
                SqlCommand command = new SqlCommand(queryString, sqlConnection1);
                

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = (int)reader[0];


                    // Call Close when done reading.
                    reader.Close();
                    //
                }


                if (id == 0)

                {
                    retval = false;
                }
                else
                {
                    string sql = "select c.customer_name, c.dob, c.address, c.phone_number,  m.dateOfPurchase from customer c, membership m where c.id = m.customer_id AND c.id = " + id;

                    // instantiate the command object to fire   
                    DataSet dt1 = new DataSet();
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConnection1))
                    {
                        // get the adapter object and attach the command object to it    
                        using (DataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(dt1);
                        }

                        dt = dt1.Tables[0];
                    }
                    retval= true;
                }
            }
            catch
            {
                // write dblogger and event logger
                
            }
           return retval;
        }

        public static bool FetchPrices(string user, string phone)
        {

//            ---check if the customer is new  as per above query, insert new customer and book order details give 2 % discount

//select 0.98 * (b.amount)from customer c, book_order b where c.id = 2;

//            ---check if new customer, if the bill is more than 5000 then discount 3 %
//             select 0.97 * (b.amount)from customer c,  membership m, book_order b where c.id = m.customer_id AND b.amount >= 5000;


//            --if the existing customer is member since 5 years then flat discount of 20 %
//            SELECT * FROM membership WHERE dateOfPurchase < now() - '5 years'::interval;

//            --check if the existing customer and number of purchases is greater than 5 books if count is greater than 5 then discount 5 %
//           select count(*) from book_order where customer_id = 2 AND order_date = '2020-01-01'::date;

            return true;
        }
    }
}
