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
        
        public MainWindow()
        {
            InitializeComponent();
            txtRegNum.Focus();
        }

        

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            dbOperations.getData( string student_id = txtRegNum.Text);
            dbOperations.pushData();
        }

        private void txtRegNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                getData();
                pushData();
            }
        }
    }
}
