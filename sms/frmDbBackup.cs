using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;

namespace sms
{
    public partial class frmDbBackup : Form
    {
        public frmDbBackup()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog obj = new SaveFileDialog();
            obj.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            obj.Title = "Specify Backup Location...";
            obj.FileName = "schoolKantadanjiOdisha";
            obj.Filter = "Backup Files (*.bak)|*.bak";
            obj.FilterIndex = 1;
            obj.ShowDialog();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                txtBackUp.Text = obj.FileName;
            }
        }

        private void btBackup_Click(object sender, EventArgs e)
        {
            CreateBackup.Backup(txtBackUp.Text);
            txtBackUp.Text = "";
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            txtBackUp.Text = @"D:\iss\backup\schoolKantadanjiOdisha" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".bak";
        }

        private void frmDbBackup_Load(object sender, EventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
