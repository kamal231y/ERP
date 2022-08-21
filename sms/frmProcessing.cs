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
    public partial class frmProcessing : Form
    {
        public frmProcessing()
        {
            InitializeComponent();
        }
        public frmProcessing(string msg, string title)
        {
            InitializeComponent();
            lblMsg.Text = msg;
            lblCaption.Text = title;
        }

        private void frmProcessing_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
                progressBar1.Value = 0;
            progressBar1.Value = progressBar1.Value + 1;
        }
    }
}
