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
    public partial class Frm_Colection : Form
    {
        public Frm_Colection()
        {
            InitializeComponent();
        }

        private void Frm_Colection_Load(object sender, EventArgs e)
        {

        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
