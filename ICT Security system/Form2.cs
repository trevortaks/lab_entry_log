using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;


namespace ICT_Security_system
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        dbConnectAnd.MainWindow newWindow;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            newWindow = new dbConnectAnd.MainWindow();
            newWindow.Show();
        }
    }
}
