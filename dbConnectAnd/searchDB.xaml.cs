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
using System.Globalization;
using System.Threading;
//using Xceed.Wpf.Toolkit;

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
           /* CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture = ci;*/
            DateTime dateTime = DateTime.Now;
        }

        /*Function to retrieve all instances of an id in the log database and display it to the viewer
            Function not final product 
            Subject to change at any time
        */

        public void search() {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;//initialise MySql connection variables to null
            DataTable dataTable = new DataTable();//Create new datatable instance to store data from db
            string student_id = txtRegNum.Text;//Get student id from text box
            try
            {
                string sql =string.Format("SELECT * FROM entry_log WHERE student_reg = '{0}' ", student_id.ToLower());

                conn = new MySqlConnection("server = localhost; user = trevortaks; database = lab_log ; port = 3306; password = angelus04;");

                cmd = new MySqlCommand(sql, conn);//Execute the select statement againt the lab1_log db

                conn.Open();//Open Connection

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))//get the data using MySqlAdapter
                {
                    da.Fill(dataTable);//Self Explanatory Statement
                }
                //Populate datagrid in Window with data retrieved
                dataGridView.ItemsSource = dataTable.DefaultView;
                dataGridView.DataContext = dataTable.TableName;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(string.Format("An error occurred {0}", ex.Message), "Error");//Capture Error
            }
            finally
            {
                if (conn != null) conn.Close();//Close Connection
            }
        }

       /* private void searchDB()
        {
            if(txtRegNum)
        }*/

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
