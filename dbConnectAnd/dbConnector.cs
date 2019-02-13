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
        static string connStr = "server = 192.168.1.27; user = db_admin; database = lab_log ; port = 3306; password = angelus04;";
        
        public dbConnector()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public extern MySqlConnection conn { get; }

        ~dbConnector()
        {
            conn.Close();
        }
    }
}
