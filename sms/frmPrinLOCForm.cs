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
    public partial class frmPrinLOCForm : Form
    {
        string classid;
       // string xith;
       SqlDataReader dr;
       public frmPrinLOCForm()
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
            if (cmbclass.Text == "X")
            {
                cryXLOCForm.classid = "11";
                cryXLOCForm.sectionname = cmbSection.Text;
                cryXLOCForm obj = new cryXLOCForm();
                obj.ShowDialog();
            }
            if (cmbclass.Text == "XII")
            {
                cryXLOCForm.classid = "13";
                cryXLOCForm.sectionname = cmbSection.Text;
                cryXLOCForm obj = new cryXLOCForm();
                obj.ShowDialog();
            }
        }

        private void frmPrinLOCForm_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbclass.Text != "Select")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string str;
                if (cmbclass.Text == "X")
                    str = " select sec_desc from section_T where sec_cls_id=11 order by sec_desc ";
                else
                    str = " select sec_desc from section_T where sec_cls_id=13 order by sec_desc ";
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].NewRow();
                cmbSection.DataSource = ds.Tables[0];
                cmbSection.DisplayMember = "sec_desc";
                cmbSection.ValueMember = "sec_desc";
            }
        }

       
      
        }

        
        

       

        

        

        

       
        
    }

