using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twinkle;

namespace basicEnroll
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void btnAddFromCSV_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string pathcsv = folderBrowserDialog1.SelectedPath;
            TextReaderHelper.ValidateLoadCSVFile( pathcsv);
            //above will do bulk upload
        }
    }
}
