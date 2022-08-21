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
    public partial class frmPrinXIILOCForm : Form
    {
        string classid;
       // string xith;
       SqlDataReader dr;
       public frmPrinXIILOCForm()
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

        //    test = cmbuser.SelectedValue.ToString();
        //    classn = cmbuser.Text.ToString();
        //    cryRegistrationReport.test = test;
        //    cryRegistrationReport.classn = classn;
        //    cryRegistrationReport obj = new cryRegistrationReport();
        //    obj.ShowDialog();
        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                cryXLOCForm.classid = "12";
                cryXLOCForm obj = new cryXLOCForm();
                obj.ShowDialog();
            }
            if(radioButton2.Checked==true)
            {
                cryXLOCForm.classid = "14";
                cryXLOCForm obj = new cryXLOCForm();
                obj.ShowDialog();
            }
        }       
      
        }

        
        

       

        

        

        

       
        
    }

