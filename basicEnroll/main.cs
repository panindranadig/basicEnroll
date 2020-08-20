using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using basicEnroll;

namespace Twinkle
{
    public partial class main : Form
    {
        public string UserName { get; set; }
        
        public main()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void enrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void enrollesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
         
        }

        private void main_Load(object sender, EventArgs e)
        {
         

            toolStripStatusLabel3.Text = UserName;
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AddCustomer adsales = new AddCustomer();
            adsales.Show();
            //Users user = new Users();
            //user.ShowDialog();
        }

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            SearchUser usersearch = new SearchUser();
            usersearch.Show();
        }

        private void btnEnrollees_Click(object sender, EventArgs e)
        {
            AddCustomer addCust = new AddCustomer();
            addCust.Show();
        }

        private void maintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserName != "master")
            {
                deleteCustomerToolStripMenuItem.Visible = false;
            }
        }
    }
}
