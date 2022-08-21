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

namespace sms
{
    public partial class frmClassScheduling : Form
    {
        string stuid;
        public frmClassScheduling()
        {
            InitializeComponent();
            bindclass(); bindExam(); bindAdmNo();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "class";
            cmbClass.ValueMember = "SEC_ID";
        }
        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EXAMINATION_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EXAM_DESC"] = "Select";
            row["EXAM_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "EXAM_DESC";
            cmbExam.ValueMember = "EXAM_ID";
        }
        private void bindAdmNo()
        {
            string sec;
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
            {
                sec = "%";
            }
            else
            {
                sec = cmbClass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,STU_ADM_NO from student_t where STU_CLS_ID like '" + sec + "' order by stu_id", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STU_ADM_NO"] = "-Select-";
            row["STU_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_rollno.DataSource = ds.Tables[0];
            cmb_rollno.DisplayMember = "STU_ADM_NO";
            cmb_rollno.ValueMember = "STU_ID";
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbClass.SelectedValue = 0;
            cmbExam.SelectedValue = 0;
            cmbExam.SelectedValue = 0;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();


        }
        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            //bindclass(); bindExam();
        } 
        private void button1_Click(object sender, EventArgs e)
        {
           if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            {
                MsgBox.ShowBox("Please Select class ");
                return;
            }
            else
            {
                try
                {
                    string str1; string str2;
                    string str3;
                    lblclass.Text = "0";
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();

                    if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
                        str1 = "%";
                    else
                        str1 = cmbClass.SelectedValue.ToString();
                       cryScheduler.classname = str1;
                       cryScheduler obj = new cryScheduler();
                    obj.ShowDialog();
                }
                 catch(Exception ex)
            {

            }
            
           
        }
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {

        }
      

    }
         
}
