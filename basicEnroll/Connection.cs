using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinkle
{

    /// <summary>
    /// Singleteon Patteren SQL connection handler
    /// </summary>
    class Connection
    {
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        SqlDataAdapter da;
        private static Connection obj = null;
        private static readonly object mylockobject = new object();
        public static Connection getInstance()
        {
            lock (mylockobject)
            {
                if (obj == null)
                {
                    obj = new Connection();
                }

            }

            return obj;
        }

        private Connection()
        {

        }
        public static SqlConnection connect()
        {

            // Reading from XML config
            string s = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;


            SqlConnection con = new SqlConnection(s);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Close();
            }
            return con;

        }
    }
}
