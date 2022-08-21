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
    public partial class frmFeeCollectionComment : Form
    {
        dataclass dd = new dataclass();
        SqlConnection sc;
        SqlDataReader dr;
        SqlCommand cmd;
        public string lblAdm; public string phone;
        public frmFeeCollectionComment()
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFeeCollectionComment_Load(object sender, EventArgs e)
        {
               label5.Text = lblAdm;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (label5.Text == "Please Enter Adm. No." || label5.Text == "")
            {
                MsgBox.ShowBox("Adm. no. should not be blank in fee collection form....");
                
            }
            
            else
            {
                
                if(checkno()==false)
                {
                 dataclass dc = new dataclass();
                 sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("insert_Comment", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@coomentid", 0);
                da.SelectCommand.Parameters.AddWithValue("@admno", lblAdm.Trim().Replace(" ", ""));
                da.SelectCommand.Parameters.AddWithValue("@comment", richTextBox1.Text.Trim());
                da.SelectCommand.Parameters.AddWithValue("@flag", 0);
                DataTable dt = new DataTable();
                da.Fill(dt); 
                try
                {
 
                    if (dt.Rows.Count > 0)
                    {
                        MsgBox.ShowBox("Comment saved successfully....");
                        string complainno= dt.Rows[0][0].ToString();
                        string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,Your problem has been logged against Complaint No " + complainno + " It will be our immense pleasure to let you know after resolved the same. For further query please call on 9914310080 or 01752360691.Thanks ";
                        smsclass smssc = new smsclass();
                      //string str= smssc.SMS(msgtxt, 0, phone.Trim().Replace(" ", ""),);
                       
                   
                    }
                    else
                    {
                        MsgBox.ShowBox("Comment for this Adm. no. already exists...");

                    }

                }
                catch (Exception ex)
                {
                    MsgBox.ShowBox(ex.ToString());
                }
                finally
                {
                    sc.Close();

                }
            }
                else
                {
                    MsgBox.ShowBox(" comment for this Adm. no. already  exists...");

                }


            }
        }
        private bool checkno()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool ret;
            SqlCommand com = new SqlCommand("select * from tblcomment where admno=" + lblAdm + " and status=0", sc);
            sc.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                ret = true;
            }
            else
            {
                ret = false;
            }

            return ret;
            sc.Close();

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}