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
using System.Diagnostics;

namespace sms
{
    public partial class frmadminPermission : Form
    {
        public frmadminPermission()
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

        private void BindGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblADMINRIGHTS where userType!='SUPERADMIN' order by id", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        

        private void frmPermission_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                sc.Open();
                SqlCommand com = new SqlCommand("select * from tblADMINRIGHTS where id='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'", sc);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtId.Text = dr["id"].ToString();
                    lblMsg.Text ="User Type : "+ dr["UserType"].ToString()+" User Name : "+ dr["UserName"].ToString();
                    chkM1.Checked = Convert.ToBoolean(dr["Master"]);
                    
                    chkReg1.Checked = Convert.ToBoolean(dr["REG1"]);
                    
                    
                    chkStu3.Checked = Convert.ToBoolean(dr["STU1"]);
                    

                    chkF1.Checked = Convert.ToBoolean(dr["FEE1"]);
                    

                    chkExam1.Checked = Convert.ToBoolean(dr["EXAM1"]);
                   
                    chkT1.Checked = Convert.ToBoolean(dr["T1"]);
                    

                    ckhAcc1.Checked = Convert.ToBoolean(dr["Acc1"]);
                  

                    ckhInv1.Checked = Convert.ToBoolean(dr["Inv1"]);
                    

                    ckhPr1.Checked = Convert.ToBoolean(dr["PayRoll1"]);
                  

                    chkLib1.Checked = Convert.ToBoolean(dr["Lib1"]);
                   

                    chkSms1.Checked = Convert.ToBoolean(dr["sms1"]);
                   
                    
                    //chkBackUp.Checked = Convert.ToBoolean(dr["BCB"]);
                    //chkRestore.Checked = Convert.ToBoolean(dr["BR"]);

                    //chkCreateUser.Checked = Convert.ToBoolean(dr["UCU"]);
                    //chkChangePassword.Checked = Convert.ToBoolean(dr["UCP"]);
                    //chkPermission.Checked = Convert.ToBoolean(dr["P"]);
                    
                }
                sc.Close();
            }
        }

        

        private void btUpdate_Click(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "update_tblADMINRIGHTS";
            cmd.Connection = sc;
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@M1", Convert.ToInt32(chkM1.Checked));
           
            cmd.Parameters.AddWithValue("@REG1", Convert.ToInt32(chkReg1.Checked));
            

            cmd.Parameters.AddWithValue("@STU1", Convert.ToInt32(chkStu3.Checked));
            


            cmd.Parameters.AddWithValue("@FEE1", Convert.ToInt32(chkF1.Checked));
         

            cmd.Parameters.AddWithValue("@EXAM1", Convert.ToInt32(chkExam1.Checked));
         

            cmd.Parameters.AddWithValue("@T1", Convert.ToInt32(chkT1.Checked));
          
            cmd.Parameters.AddWithValue("@SEQ1", Convert.ToInt32(chkChangePassword.Checked));
           

            cmd.Parameters.AddWithValue("@Acc1", Convert.ToInt32(ckhAcc1.Checked));
           

            cmd.Parameters.AddWithValue("@Inv1", Convert.ToInt32(ckhInv1.Checked));
          

            cmd.Parameters.AddWithValue("@PayRoll1", Convert.ToInt32(ckhPr1.Checked));
           

            cmd.Parameters.AddWithValue("@Lib1", Convert.ToInt32(chkLib1.Checked));
          

            cmd.Parameters.AddWithValue("@sms1", Convert.ToInt32(chkSms1.Checked));
           

            //cmd.Parameters.AddWithValue("@BCB", Convert.ToInt32(chkBackUp.Checked));
            //cmd.Parameters.AddWithValue("@BR", Convert.ToInt32(chkRestore.Checked));
            //cmd.Parameters.AddWithValue("@UCU", Convert.ToInt32(chkCreateUser.Checked));
            //cmd.Parameters.AddWithValue("@UCP", Convert.ToInt32(chkChangePassword.Checked));
            //cmd.Parameters.AddWithValue("@P", Convert.ToInt32(chkPermission.Checked));

             sc.Open();
            int w = cmd.ExecuteNonQuery();

            if (w > 0)
            {
                MsgBox.ShowBox("Record Updated...");
            }
            else
            {
                MsgBox.ShowBox("Updating Error...");
            }
            sc.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStu1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStu36_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStu46_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStu9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7h_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStu11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkStu23_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkF8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkF9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
