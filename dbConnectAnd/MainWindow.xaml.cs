using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace dbConnectAnd
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    { 
        MySqlDataReader data;

        public MainWindow()
        {
            InitializeComponent();
            txtRegNum.Focus();
        }

        public void getData()
        {
            string student_id = txtRegNum.Text;
            try
            {
                queryString(student_id.ToLower());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void pushData()
        {
            string student_id = Convert.ToString(lblRegNum.Content);
            string student_name = Convert.ToString(lblName.Content);
            string[] name = new string [] { student_name };

            try
            {
                queryString(student_id, name);
                lblStatus.Content = "You may Enter ";
                txtRegNum.Text = "";
                txtRegNum.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                lblStatus.Content = "Failure";
            }
        }

        public void queryString(string student_id, params string[] student_name)
        {
            using (var conn = Utilities.GetConnection())
            {
                conn.Open();
                if (student_name.Length > 0)
                {
                    string year = DateTime.Now.ToString("yyyy-MM-dd");
                    string time = DateTime.Now.ToString("HH:mm:ss");
                    MySqlCommand get = conn.CreateCommand();
                    get.CommandText = string.Format("INSERT INTO entry_log(student_id, student_name, year, time) VALUES ('{0}', '{1}', '{2}', '{3}') ", student_id, student_name[0], year, time);
                    data = get.ExecuteReader();

                }
                else
                {
                    MySqlCommand get = conn.CreateCommand();
                    get.CommandText = "SELECT * FROM student_details WHERE student_id= '" + student_id + "'";
                    data = get.ExecuteReader();
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            lblRegNum.Content = data.GetString(data.GetOrdinal("student_id"));
                            lblName.Content = data.GetString(data.GetOrdinal("student_name"));
                            lblProgram.Content = data.GetString(data.GetOrdinal("student_prog"));
                            lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
                        }
                        pushData();
                    }
                    else
                    {
                        lblRegNum.Content = "";
                        lblName.Content = "";
                        lblProgram.Content = "";
                        lblTime.Content = "";
                        lblStatus.Content = "Error:Student Registration Number "+ student_id +" does not exist. Please check your input and try again." ;
                        lblStatus.FontSize = 12;
                        txtRegNum.Text = "";
                    }
                }
            }
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            getData();   
        }

        private void txtRegNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                getData();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var window = new searchDB();
            window.Show();
        }
    }

    public static class Utilities
    {
        public static MySqlConnection GetConnection()
        {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = "server = 192.168.1.27; user = root; database = lab1_log ; port = 3306; password = admin123;";
                return conn;
        }
    }
}
