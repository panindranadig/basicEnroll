using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twinkle;

namespace basicEnroll
{
    public partial class Add_Sales : Form
    {
        public int userid = 0;
        public Add_Sales()
        {
            InitializeComponent();
            disableUI();
        }
        public void disableUI()
        {
            // grey out buttons except customer name , phone number and search field
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btnFetchPrice_Click(object sender, EventArgs e)
        {
            bool isuser = SqlHelper.UserExists(textBox1.Text, textBox2.Text, out userid);
            if (isuser)
            {
                //enable fetch button,booklist,search button
            }
            else
            {
                lblisnotCustomer.Visible = true;
                // enable add customer
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
