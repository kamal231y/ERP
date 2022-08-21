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
    public partial class frmStudentindividualsms : Form
    {
        DataTable StudentFee = new DataTable();
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;  
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmStudentindividualsms()
        {
            InitializeComponent();
        }

        private void msgtype()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select id,msgtype  from tblmsgtype order by msgtype ", sc);
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
      
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";
        }
        private void bindrollno()
        {
            string StateName;
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_STUDENTreport where STC_SEC_ID =  '" + StateName + "' and stu_status='y' order by  ROLL", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "-Select-";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "admNO";
            cmb_rollno.ValueMember = "STU_ID";
        }
       
        
        private void Bindname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from student_T where STU_ID like '" + cmb_rollno.SelectedValue + "' and STU_STATUS='Y'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
                txtmobile.Text = dt.Rows[0]["STU_Mobile"].ToString();
            }
            else
            {
                txtStuname.Text = "";
                txtmobile.Text = "";
            }

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
            cmb_class.Text = "-Select-";
            StudentFee.Rows.Clear();
            cmb_rollno.Text = "";
            txtStuname.Text = "";

        }

     
        private void sms1()
        {
            if (cmb_rollno.Text.Trim() == "")
            {
                MessageBox.Show("Select Adm No. !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_rollno.Focus();
                return;
            }
            else if (txtmsg.Text.Trim() == "")
            {
                MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmsg.Focus(); return;

            }
            else if (txtmobile.Text.Trim() == "")
            {
                MessageBox.Show("Fill MobileNo Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmobile.Focus(); return;

            }
            else
            {
                try
                {
                    string msg = "|";
                    DAL.smsclass SMS = new sms.DAL.smsclass();
                    if (radiomale.Checked == true)
                        msg = SMS.SMS(txtmsg.Text.Trim(), 0, txtmobile.Text.Trim().Replace(" ", ""), txtStuname.Text.Trim());
                    else
                       msg= SMS.SMS(txtmsg.Text.Trim(), 1, txtmobile.Text.Trim().Replace(" ", ""), txtStuname.Text.Trim());
                    string[] str = msg.Split('"');
                    if (str[3].ToString().Trim().Replace(" ", "") == "3001")
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);
                    else
                    MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                              
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
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }





      
       
        
       


        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            txtStuname.Text = "";
            Bindname();
        }

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindrollno();
        }


        private void frmStudentindividualsms_Load(object sender, EventArgs e)
        {
            bindClass();
            Bindmsg(); msgtype();
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

        private void btn_close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtmsg_TextChanged(object sender, EventArgs e)
        {
            string str;

            str = txtmsg.Text.ToString();
            label9.Text = " Character : " + str.Length;
            label9.Visible = true;
        }

        private void label9_Leave(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

       
     }
}