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
    /// Blah Blah Blah This is tiring
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlDataReader data;//Variable for executing and storing data from db
        List<string> inlab = new List<string>();
        
        public MainWindow()
        {
            InitializeComponent();
            txtRegNum.Focus();//Put cursor in text box on startup
        }

        /*
            This function is to retrieve student data from database by getting the student_id from text box and parse
            it the query function which then retrieves the data and displays it on the windows
            The function takes no input and returns no data
            Its main function is to display the errors that occur on retrieving the data
        */
        public void getData()
        {

            string student_id = txtRegNum.Text;//Get student id from textbox
            if (inlab.Contains(student_id))
            {
                logout(student_id);
            }
            else
            {
                try
                {
                    queryString(student_id.ToLower());//Parse student id to query function
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);//throw an error via Messagebox incase of Fatal Failure
                }
            }

        }
        /*
            The function pushData() is responsible for writing student details 
            nd entry time into lab to the database for later retrieval
            The function takes no input and returns no data
            It captures data from the labels on the MainWindow and parses them to 
            the query function which pushes the data to the database
        */

        private void pushData()
        {
            //Get student data from the labels on the MainWindow
            string student_id = Convert.ToString(lblRegNum.Content);
            string student_name = Convert.ToString(lblName.Content);
            string[] name = new string [] { student_name };//Add student name to new string array for params passing

            try
            {
                queryString(student_id, name);//Parse the student details to the query function
                lblStatus.Content = "You may Enter ";
                txtRegNum.Text = "";
                txtRegNum.Focus();//Clear TextBox and return focus on it
            }
            catch (Exception ex)
            {
                //Capture Fatal error and display it in a window informing the user the errorand update 
                //lblStatus to shpw it failed to get data into database
                MessageBox.Show(ex.Message, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                lblStatus.Content = "Failure";
            }
        }

        /*
            The query() function receives input from either getData() or pushData() functions 
            and then perfoms actions based on the number of parameters
            received
            The function can receive either one or two parameters
            Para 1: string student_id (this is the id number of the student to be retrieved or pushed to database)
            Para2: params string[] student_name (this is an optional parameter: the name os the student)
            If the function receives just one parameter that parameter is taken to be the student id and
            it goes on to request for detail from database
            If the function receives two parameters, it takes the action of pushing the data to the log database
            Issue: The function hangs for a secomd or two on first id scan.Probably first connection to db
            still to look for fix
        */
        private void queryString(string student_id)
        {
            using (var conn = Connection.GetConnection())//Get connection to database
            {
                conn.Open();//Open connection to db

                //Create a SQL string to retrieve data from database and execute it
                MySqlCommand get = conn.CreateCommand();
                get.CommandText = "SELECT * FROM student_details WHERE student_reg= '" + student_id + "'";
                data = get.ExecuteReader();

                if (data.HasRows)//Check if any data was found in the db
                {
                    while (data.Read())//read through the data
                    {
                        //Fill the labels with data received from database
                        lblRegNum.Content = data.GetString(data.GetOrdinal("student_reg"));
                        lblName.Content = data.GetString(data.GetOrdinal("student_name"));
                        lblProgram.Content = data.GetString(data.GetOrdinal("student_prog"));
                        lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
                    }
                    pushData();//Call on the pushData function to act on the data
                    inlab.Add(student_id);
                }
                else
                {
                    //if student id was not found, take appropiate action and inform user
                    lblReset();
                    //Update the Status label with relevant information
                    lblStatus.Content = "Error:Student Registration Number "+ student_id +" does not exist. Please check your input and try again." ;
                    lblStatus.FontSize = 12;
                    
                }
            }
        }

        private void lblReset()
        {
            lblRegNum.Content = "";
            lblName.Content = "";
            lblProgram.Content = "";
            lblTime.Content = "";
            txtRegNum.Text = "";
        }

        private void queryString(string student_id, params string[] student_name)
        {
            using (var conn = Connection.GetConnection())//Get connection to database
            {
                conn.Open();//Open connection to db
                //Create a SQL command to insert the data parsed and the time into database
                MySqlCommand get = conn.CreateCommand();
                get.CommandText = string.Format("INSERT INTO entry_log(student_reg, student_name, date, entry_time) VALUES ('{0}', '{1}', CURDATE(), TIME(NOW()) ) ", student_id, student_name[0]);
                get.ExecuteNonQuery();

            }
        }

        public void logout(string student_id)
        {
            using (var conn = Connection.GetConnection())//Get connection to database
            {
                conn.Open();//Open connection to db
                MySqlCommand get = conn.CreateCommand();
                get.CommandText =string.Format(" UPDATE entry_log SET logout_time = TIME(NOW()) where student_reg = '{0}' ORDER BY log_id DESC LIMIT 1", student_id);
                get.ExecuteNonQuery();
            }
            lblReset();
            lblStatus.Content = "Logout";
            inlab.Remove(student_id);

        }

        /*
            Handle the event clicking of btnSubmit
        */
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            getData();//Call function getData()
        }

        /*
            Handle the event Enter Key Press or the submit received from the barcode
        */

        private void txtRegNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {

                getData();//call function getData()
         
            }
        }

        /*
            Open the window for searching the log database
            Work in progress
        */
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Self explaining code
            //No need for further Detail
            var window = new searchDB();
            window.Show();
        }

        private void txtRegNum_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }

    /*
        This class handles the declaration of the connection top the db to prevent unnecessary repetition in the above class
    */

    public static class Connection
    {
        public static MySqlConnection GetConnection()
        {
                MySqlConnection conn = new MySqlConnection();//Create new mysql connection object
                conn.ConnectionString = "server = localhost; user = trevortaks; database = lab_log ; port = 3306; password = angelus04;";
                return conn;//return the connection object to calling function
        }
    }
}

/*
    Future Work

    -Come up with appropriate name for the Program
    -Add more functionality to the program 
        -Search by time
        -search by name
        -search by ID
        -Daily, Weekly and Monthly Reports on lab activity
        -intergrate logout time if possible(Done)
        -Make a better user interface
    -Make program more modular
        -less dependent on the window 
        -add classes for appropriate actions
        -Connect to Database via ADO>NET
 * +
    -Fix Issues
        -Fix a few second hang on scanning of first id
*/