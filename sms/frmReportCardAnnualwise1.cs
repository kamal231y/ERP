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
    public partial class  frmReportCardAnnualwise1 : Form
    {
        string stuid;
        public frmReportCardAnnualwise1()
        {
            InitializeComponent();
            bindclass();
            bindAdmNo(); bindExam();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by SEC_ID ", sc);
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
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,STU_ADM_NO from student_t where STU_CLS_ID like '" + sec + "' AND STU_STATUS= 'Y' order by stu_id", sc);
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
            cmb_rollno.SelectedValue = 0;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EXAMINATION_T order by exam_id ", sc);
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
       
        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            //bindclass(); bindExam();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1; string str3; string str2; string str4;
            if (rdcosch.Checked == true)
                str2 = "2";
            else
                str2 = "1";
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
            {
                MsgBox.ShowBox("Please Select Class");
            }

            else
            {
                str1 = cmbClass.SelectedValue.ToString();
                if (cmbExam.Text == "" || cmbExam.Text == "-Select-" || cmbExam.Text == "System.Data.DataRowView")
                {
                    MsgBox.ShowBox("Please Select Exam");
                }

                else
                {
                    str4 = cmbExam.SelectedValue.ToString();
                    if (cmb_rollno.Text == "" || cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "System.Data.DataRowView")
                        str3 = "%";
                    else
                        str3 = cmb_rollno.SelectedValue.ToString();

                    DataTable dt = new DataTable();
                    cryReportCardAnnualwise1.classid = str1;
                    cryReportCardAnnualwise1.stuid = str3;
                    cryReportCardAnnualwise1.type = str2;
                    cryReportCardAnnualwise1.exam = str4;
                    cryReportCardAnnualwise1.examName = cmbExam.Text.ToString() ;
               
                    cryReportCardAnnualwise1.session = sms.Form1.session.ToString();
                    cryReportCardAnnualwise1 obj = new cryReportCardAnnualwise1();
                    obj.ShowDialog();

                }
            }
        }
      
        private void cmbClass_Leave(object sender, EventArgs e)
        {
          
        }
        

        private void cmbExam_Leave(object sender, EventArgs e)
        {

        }       

        }
    
}
         
