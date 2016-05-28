using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Application.Current.MainWindow;

namespace dbConnectAnd
{
    class dbOperations
    {

        public dbOperations(){
            string connStr = "server = 192.168.1.27; user = root; database = lab1_log ; port = 3306; password = admin123;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public void getData(string student_id)
        {
            
            try
            {
                MySqlCommand get = conn.CreateCommand();
                get.CommandText = "SELECT * FROM student_details WHERE student_id= '" + student_id + "'";
                MySqlDataReader data = get.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        lblRegNum.Content = data.GetString(data.GetOrdinal("student_id"));
                        lblName.Content = data.GetString(data.GetOrdinal("student_name"));
                        lblProgram.Content = data.GetString(data.GetOrdinal("student_prog"));
                        lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
                else
                {
                    lblStatus.Content = "Error:Student Registration Number does not exist. Please check your input and try again.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            conn.Close();

        }

        public void pushData()
        {
            string student_id = Convert.ToString(lblRegNum.Content);
            string student_name = Convert.ToString(lblName.Content);
            string year = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");

            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            try
            {
                MySqlCommand get = conn.CreateCommand();
                get.CommandText = string.Format("INSERT INTO entry_log(student_id, student_name, year, time) VALUES ('{0}', '{1}', '{2}', '{3}') ", student_id, student_name, year, time);
                MySqlDataReader data = get.ExecuteReader();
                lblStatus.Content = "You may Enter ";
                txtRegNum.Text = "";
                txtRegNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                lblStatus.Content = "Failure";
            }
            conn.Close();
        }
    }
}
