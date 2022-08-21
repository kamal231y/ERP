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
    public partial class frmReportCard : Form
    {
        string stuid;
        public frmReportCard()
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

            if (cmbExam.Text == "-Select-" || cmbExam.Text == "")
            {
                MsgBox.ShowBox("Please Select Exam type");
                return;
            }
            else if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
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
                    if (cmbExam.Text == "" || cmbExam.Text == "-Select-" || cmbExam.Text.Trim() == "System.Data.DataRowView")
                        str2 = "%";
                    else
                        str2 = cmbExam.SelectedValue.ToString();
                    if (cmb_rollno.Text == "" || cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "System.Data.DataRowView")
                        str3 = "%";
                    else
                        str3 = cmb_rollno.SelectedValue.ToString();

                   
                    string examT;
                    update_record(str1, str3, str2);
                    cryReportCardtermWise.Class = str1;
                    cryReportCardtermWise.stuid = str3;
                    cryReportCardtermWise.exam = str2;
                    cryReportCardtermWise.examT = cmbExam.Text;
                    cryReportCardtermWise obj = new cryReportCardtermWise();
                    obj.ShowDialog();
                }
                 catch(Exception ex)
            {

            }
            
           
        }
        }
        private void update_record(string classid, string stu_id,string exam)
        {
            SqlCommand cmd;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Create_ReportCard_termwise", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@class", classid);
            da.SelectCommand.Parameters.AddWithValue("@stu_id", stu_id);
            da.SelectCommand.Parameters.AddWithValue("@exam", exam); 
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        string code = string.Empty;
                        if (dr["str_exam_id"].ToString() == "1" || dr["str_exam_id"].ToString() == "4")
                            code = "E1" + dr["subsc_code"].ToString();
                        if (dr["str_exam_id"].ToString() == "2" || dr["str_exam_id"].ToString() == "5")
                            code = "E2" + dr["subsc_code"].ToString();
                        if (dr["str_exam_id"].ToString() == "3" || dr["str_exam_id"].ToString() == "6")
                            code = "E3" + dr["subsc_code"].ToString();
                        cmd = new SqlCommand("update STUDENT_MARKS_ENTRY set " + code.Trim() + " ='" + dr["grade"].ToString().Trim() + "' where stu_id =" + dr["stu_id"].ToString() + " ", sc);
                        sc.Open();
                        cmd.ExecuteNonQuery();
                        sc.Close();
                    }
                    catch (Exception ex) { }
                }
            
            
            }
        
        }
        private void cmbClass_Leave(object sender, EventArgs e)
        {
            //string StateName;
            //if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            //{
            //    StateName = "0";
            //}
            //else
            //{
            //    StateName = cmbClass.SelectedValue.ToString();
            //}

            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "0";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            //cmb_rollno.DataSource = dt;
            //cmb_rollno.DisplayMember = "STC_ROLL_NO";
            //cmb_rollno.ValueMember = "STC_STU_ID";
        }

   

   




    }
         
}
