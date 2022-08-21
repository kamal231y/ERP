using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sms
{
    public partial class frmBackup : Form
    {
        
        public frmBackup()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            txtBackUp.Text = "";
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btBackup_Click(object sender, EventArgs e)
        {
            CreateBackup.Backup(txtBackUp.Text);
            txtBackUp.Text = "";
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog obj = new SaveFileDialog();
            obj.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            obj.Title = "Specify Backup Location...";
            obj.FileName = "IssSchool";
            obj.Filter = "Backup Files (*.bak)|*.bak";
            obj.FilterIndex = 1; 
            obj.ShowDialog();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                txtBackUp.Text = obj.FileName;
            }

        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            txtBackUp.Text = @"D:\Backup\IssSchool" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".bak";
        }

        private void btCancel_Click_1(object sender, EventArgs e)
        {
            txtBackUp.Text = @"D:\Backup\IssSchool" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".bak";
        }

       
    }
}
