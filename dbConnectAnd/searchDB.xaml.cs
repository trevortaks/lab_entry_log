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
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace dbConnectAnd
{
    /// <summary>
    /// Interaction logic for searchDB.xaml
    /// </summary>
    public partial class searchDB : Window
    {
        public searchDB()
        {
            InitializeComponent();
        }


        public void search() {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            DataTable dataTable = new DataTable();
            string student_id = txtRegNum.Text;
            try
            {
                string sql =string.Format("SELECT * FROM entry_log WHERE student_id = '{0}' ", student_id.ToLower());

                conn = new MySqlConnection("server = 192.168.1.27; user = root; database = lab1_log ; port = 3306; password = admin123;");

                cmd = new MySqlCommand(sql, conn);

                conn.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                }

                dataGridView.ItemsSource = dataTable.DefaultView;
                dataGridView.DataContext = dataTable.TableName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred {0}", ex.Message), "Error");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            search();
        }

        private void txtRegNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                search();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            search();
        }
    }
}
