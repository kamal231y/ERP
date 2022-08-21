using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.DAL;
using sms.BLClass;
using System.Collections;
using System.IO;
using System.Reflection;

namespace sms
{
    public partial class frmSetting : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmSetting()
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
        }
      
     
 
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure To Change Setting!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da;
                    da = new SqlDataAdapter("Update_Setting", sc);
                    da.SelectCommand.CommandTimeout = 300;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@fee", chkfee.Checked);
                    da.SelectCommand.Parameters.AddWithValue("@sale", chkSale.Checked);
                    da.SelectCommand.Parameters.AddWithValue("@report", chkreport.Checked);
                    da.SelectCommand.Parameters.AddWithValue("@sms", chksms.Checked);
                    da.SelectCommand.Parameters.AddWithValue("@FEE_SMS", chkfeesms.Checked);
                    da.SelectCommand.Parameters.AddWithValue("@FEE_REMINDER", chkreminder.Checked);
                    if (chkhindiformt.Checked == true)
                    {
                        da.SelectCommand.Parameters.AddWithValue("@TCFORMAT", 1);
                    }
                    else {
                        da.SelectCommand.Parameters.AddWithValue("@TCFORMAT", 0);                    
                    }


                    System.Data.DataTable dt = new System.Data.DataTable();
                    da.Fill(dt);
                    bind_data();
                }
                catch (Exception ex) { }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bind_data();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            bind_data();
          
        }
        private void bind_data()
        {
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SETTING_T ", sc);
                    sc.Open();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    chkfee.Checked = Convert.ToBoolean(dt.Rows[0][0].ToString());
                    chkSale.Checked = Convert.ToBoolean(dt.Rows[0][1].ToString());
                    chkreport.Checked = Convert.ToBoolean(dt.Rows[0][2].ToString());
                    chksms.Checked = Convert.ToBoolean(dt.Rows[0][3].ToString());
                    chkfeesms.Checked = Convert.ToBoolean(dt.Rows[0][5].ToString());
                    chkreminder.Checked = Convert.ToBoolean(dt.Rows[0][6].ToString());

                    if (dt.Rows[0]["TC_FORMAT"].ToString() == "1")
                    {
                        chkhindiformt.Checked = true;
                    }
                    else {
                        chkhindiformt.Checked = false;
                    }
                    Form1.fee_reciept = Convert.ToBoolean(dt.Rows[0][0].ToString());
                    Form1.sale_reciept = Convert.ToBoolean(dt.Rows[0][1].ToString());
                    Form1.report_reciept = Convert.ToBoolean(dt.Rows[0][2].ToString());
                    Form1.SMS = Convert.ToBoolean(dt.Rows[0][3].ToString());
                    Form1.feesms = Convert.ToBoolean(dt.Rows[0][5].ToString());
                    Form1.fee_reminder = Convert.ToBoolean(dt.Rows[0][6].ToString());
                    Form1.TC_FORMAT = Convert.ToInt16(dt.Rows[0]["TC_FORMAT"].ToString());
                }
                catch (Exception EX) { }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseIcard obj = new ChooseIcard();
            //obj.MdiParent = this;
            obj.ShowDialog();
        }

        

    }
}
