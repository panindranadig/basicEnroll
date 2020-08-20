using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twinkle;

namespace basicEnroll
{
    public partial class SearchUser : Form
    {
        public SearchUser()
        {
            InitializeComponent();

            lblNoUser.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            populateGrid(textBox1.Text, textBox2.Text);
        }

       public  void populateGrid(string user,string phone)
        {
            bool isfounduser = false;

            
            DataTable dt = null;
            isfounduser =  SqlHelper.SearchUser(user,phone,out dt);
            if (isfounduser == false)
            {
                lblNoUser.Visible = true;
            }
            else
            {

                dataGridView1.DataSource = dt;
               
            }
            // bind the data now    
          

        }
    }
}
