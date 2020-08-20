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

namespace Twinkle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnlog_Click(object sender, EventArgs e)
        {
               
            try
            {
                // reading password from INI file
                TextReaderHelper txti = new TextReaderHelper();
                string pasc = Twinkle.TextReaderHelper.IniReader(txtuser.Text);


                if (pasc == txtpass.Text)
                {
                    this.Hide();
                    main main = new main();
                    main.UserName = txtuser.Text;

                    main.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect Password");
                }

            }
            catch(Exception ex)
            {
                LogHelperLib.LogHelper.Log(LogHelperLib.LogTarget.File, ex.Message.ToString());
            }
            }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
