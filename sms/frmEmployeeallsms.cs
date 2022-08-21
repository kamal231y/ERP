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
    public partial class frmEmployeeallsms : Form
    {
        DataTable StudentFee = new DataTable();
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;  
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmEmployeeallsms()
        {
            InitializeComponent();
            Binddept();
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

        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "All";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "DEPT_DESC";
            cmbDept.ValueMember = "DEPT_ID";
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
            cmbDept.Text = "All";
            StudentFee.Rows.Clear();
           

        }

        private void sms1()
        {
             if (txtmsg.Text.Trim() == "")
            {
                MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmsg.Focus(); return;

            }

            else
            {
                try
                {
                    
                    for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
                    {
                        if ((dataGridView1.Rows[z].Cells["Mobile"].Value.ToString() != "" || dataGridView1.Rows[z].Cells["Mobile"].Value.ToString() != null) && dataGridView1.Rows[z].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        {

                            string msg = "|";
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            if (radiomale.Checked == true)

                                msg = SMS.SMS(txtmsg.Text.Trim(), 0, dataGridView1.Rows[z].Cells["Mobile"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Emp_Name"].Value.ToString().Trim().Replace(" ", ""));
                            else
                                msg = SMS.SMS(txtmsg.Text.Trim(), 1, dataGridView1.Rows[z].Cells["Mobile"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Emp_Name"].Value.ToString().Trim().Replace(" ", ""));
                            string[] str = msg.Split('|');
                            if (str[0].ToString().Trim().Replace(" ", "") == "error")
                                MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
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





      
       
        
       


        
        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
           // bindrollno();
           InitGrid();
            
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
            string str;
            if (cmbDept.Text == "" || cmbDept.Text == "All" || cmbDept.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmbDept.SelectedValue.ToString();


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select EMP_MSG_ALLOWED AS Selected,Emp_code,Emp_Name,Dept_Desc,Emp_Mobile as Mobile  from View_tblemployee where  Dept_ID like " + str + " order by Emp_code", sc);

            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                dataGridView1.DataSource = ds;
            }

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
            //bindClass();
            msgtype();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void chkpartpayment_CheckStateChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
        

       
       

       

     }
}