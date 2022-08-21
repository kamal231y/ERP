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
    public partial class frmemployeeindividualsms : Form
    {
        DataTable StudentFee = new DataTable();
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;  
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmemployeeindividualsms()
        {
            InitializeComponent();
            Binddept();
        }
        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "DEPT_DESC";
            cmbDept.ValueMember = "DEPT_ID";
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

        private void bindemployee()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from View_tblemployee where Dept_Id like '" + cmbDept.SelectedValue + "' order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Emp_Code"] = "Select";
            row["Emp_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbempcode.DataSource = ds.Tables[0];
            cmbempcode.DisplayMember = "Emp_Code";
            cmbempcode.ValueMember = "EMP_ID";
        }




        private void Bindname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblemployee where Emp_ID like '" + cmbempcode.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["Emp_NAME"].ToString();
                txtmobile.Text = dt.Rows[0]["Emp_Mobile"].ToString();
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
            cmbDept.Text = "-Select-";
            StudentFee.Rows.Clear();
            cmbempcode.Text = "";
            txtStuname.Text = "";

        }

        private void sms1()
        {
            if (cmbempcode.Text.Trim() == "")
            {
                MessageBox.Show("Select Emp. Code. !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbempcode.Focus();
            }
            else if (txtmsg.Text.Trim() == "")
            {
                MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmsg.Focus();
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
                    string[] str = msg.Split('|');
                    if (str[0].ToString().Trim().Replace(" ","")=="error")
                        MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);
                              
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

        

        private void frmStudentindividualsms_Load(object sender, EventArgs e)
        {
           // bindClass();
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
           // bindClass();
            msgtype();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindemployee();
        }

        private void cmbempcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStuname.Text = "";
            Bindname();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radiomale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radiofemale_CheckedChanged(object sender, EventArgs e)
        {

        }

       
        

       
       

       

     }
}