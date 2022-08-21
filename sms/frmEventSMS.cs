using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using sms.DATALAYER;
using System.Data.SqlClient;
using System.Data.OleDb;
using MsgBoxRet;
using System.Reflection;


namespace sms
{
    public partial class frmEventSMS : Form
    {
        WebRequest request = null;
        DataTable StudentFee = new DataTable();
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;  
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmEventSMS()
        {
            InitializeComponent();
        }

        private void msgtype()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select id,msgtype  from tblmsgtype where id in (6,7) order by msgtype  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["msgtype"] = "-Select-";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbmsgtype.DataSource = ds.Tables[0];
            cmbmsgtype.DisplayMember = "msgtype";
            cmbmsgtype.ValueMember = "id";
        }
      
       
        
        private void Bindmsg()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select msg from tblmsgtype where ID like '" + cmbmsgtype.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtmsg.Text = dt.Rows[0]["msg"].ToString();
            }

        }
        void Cancel_Form()
        {
          
          

        }

        private void sms1()
        {
            
            if (txtmsg.Text.Trim() == "")
            {
                MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmsg.Focus();
            }
           
             for (int z = 0; z < dataGridView1.Rows.Count-1; z++)
                  {
                     string str11111 = dataGridView1.Rows[z].Cells["Selected"].Value.ToString();
                      if (dataGridView1.Rows[z].Cells["Selected"].Value.ToString() == "True")
                      if ((dataGridView1.Rows[z].Cells["MobileNo"].Value.ToString() != "" || dataGridView1.Rows[z].Cells["MobileNo"].Value.ToString() != null) && dataGridView1.Rows[z].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        {
                          try
                            {
                                string msg = txtmsg.Text;
                                DAL.smsclass SMS = new sms.DAL.smsclass();
                                SMS.SMS(msg, 0, dataGridView1.Rows[z].Cells["MobileNo"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Name"].Value.ToString().Trim().Replace(" ", "")); 
                            }


                            catch (Exception ex)
                            {
                                
                                MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                               
                            }
                        
                     }
             }
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                
                
                   
         
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }





      
       
        
       


       

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void InitGrid()
        {
            LoadGrid();
           // dataGridView1.Columns["STU_ID"].Visible = false;
            //dataGridView1.Columns["STU_ADM_NO"].Width = 100;
            //dataGridView1.Columns["STU_ADM_NO"].HeaderText = "ADM. NO.";
            //dataGridView1.Columns["StudentName"].Width = 200;
            //dataGridView1.Columns["StudentName"].HeaderText = "StudentName";
            //dataGridView1.Columns["STC_ROLL_NO"].Width = 100;
            //dataGridView1.Columns["STC_ROLL_NO"].HeaderText = "Strength";
            //dataGridView1.Columns["STU_Mobile"].Width = 140;
            //dataGridView1.Columns["STU_Mobile"].HeaderText = "Mobile";

        }

        private void LoadGrid()
        {
            DataTable ds = new DataTable();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_Events", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@selected", chkpartpayment.Checked);
            da.Fill(ds);
           dataGridView1.DataSource = ds;

        }
       
        private void frmStudentindividualsms_Load(object sender, EventArgs e)
        {
            Bindmsg();
        }

        private void cmbmsgtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmsg.Text = "";
            Bindmsg();

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            sms1();
        }

        private void frmStudentindividualsms_Load_1(object sender, EventArgs e)
        {
            msgtype(); LoadGrid();

        }

        private void chkpartpayment_CheckStateChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtmsg_TextChanged(object sender, EventArgs e)
        {
            string str;

            str = txtmsg.Text.ToString();
            label19.Text = " Character : " + str.Length;
            label19.Visible = true;
        }

       
    

     }
}