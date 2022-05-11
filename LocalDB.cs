using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UIKitTutorials
{
    internal class LocalDB
    {
        public static SqlConnection GetDBConnection()
        {
            // Connection String.
            String connString = @"Data Source=localhost\sqlexpress; Initial Catalog=oktadmin; Integrated Security=True; Connect Timeout = 10;";
            

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
        
        
    }
}
