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

namespace sms
{
    public partial class frmRegMaster : Form
    {
        string usertype;
       SqlDataReader dr;
       public frmRegMaster()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;

            MsgBoxRetType.backColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonBackColour = Color.Red;
            MsgBoxRetType.buttonForeColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonStyle = FlatStyle.Flat;
            MsgBoxRetType.titleBackColour = Color.Red;
            MsgBoxRetType.titleForeColour = Color.WhiteSmoke;
            MsgBoxRetType.messageBoxStyle = FlatStyle.Flat;
            MsgBoxRetType.foreColour = Color.Red;
            InitializeComponent();
            //cmbbilltype.SelectedIndex = 0;
            
            
        }
      
        

        private void butformsale_Click(object sender, EventArgs e)
        {
            frmRegistrationFormSale obj = new frmRegistrationFormSale();
            obj.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmRegistrationFormSale obj = new frmRegistrationFormSale();
            obj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmStudentRegistration obj = new frmStudentRegistration();
            obj.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmStudentRegistration obj = new frmStudentRegistration();
            obj.ShowDialog();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
    }  
      
 }

