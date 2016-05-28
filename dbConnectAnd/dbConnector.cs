using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace dbConnectAnd
{
    public class dbConnector: MainWindow
    {
        static string connStr = "server = 192.168.1.27; user = root; database = lab1_log ; port = 3306; password = admin123;";
        
        public dbConnector()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public MySqlConnection conn { get; }

        ~dbConnector()
        {
            conn.Close();
        }
    }
}
