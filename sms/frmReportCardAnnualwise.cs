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
    public partial class  frmReportCardAnnualwise : Form
    {
        string stuid;
        public frmReportCardAnnualwise()
        {
            InitializeComponent();
            bindclass();
            bindAdmNo();
            bindrollnumber();
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
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,STU_ADM_NO from student_t where STU_CLS_ID like '"+sec+"' order by stu_id", sc);
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
        private void bindrollnumber()
        {
            string class1;
            if (cmbClass.SelectedValue.ToString() == "Select" || cmbClass.SelectedValue.ToString() == "")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbClass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID =  " + class1 + " order by  STC_ROLL_NO", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["STC_ROLL_NO"] = "0";
            row["STC_STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_rollnumber.DataSource = dt;
            cmb_rollnumber.DisplayMember = "STC_ROLL_NO";
            cmb_rollnumber.ValueMember = "STC_STU_ID";
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

        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            //bindclass(); bindExam();
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            string str1; string str3; string str2; string str4 ;string ut;
            if (rdcosch.Checked == true)
                str2 = "2";
            else
                str2 = "1";
            if (final.Checked == true)
                str4 = "7";
            else if (term2.Checked == true)
                str4 = "6";
            else
               str4 = "3";
            if (UT1.Checked == true)
                ut = "1";
            else if (UT2.Checked == true)
                ut = "2";
            else if (UT4.Checked == true)
                ut = "3";
            else
                ut = "4";


            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
            {
             MsgBox.ShowBox("Please Select Class");
            }
          
            else
            {
                str1 = cmbClass.SelectedValue.ToString();

                if (cmb_rollno.Text == "" || cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "System.Data.DataRowView")
                str3 = "%";
            else
                str3 = cmb_rollno.SelectedValue.ToString();


            DataTable dt = new DataTable();
             cryReportCardAnnualwise.classid=str1;
            cryReportCardAnnualwise.stuid = str3;
            cryReportCardAnnualwise.type = str2;
            cryReportCardAnnualwise.exam = str4;
            cryReportCardAnnualwise.ut = ut;
            cryReportCardAnnualwise.CLSSEQ = Convert.ToInt16(textBox1.Text.ToString());
            cryReportCardAnnualwise.session = sms.Form1.session.ToString();
            cryReportCardAnnualwise obj = new cryReportCardAnnualwise();             
            obj.ShowDialog();              
                        
            }
        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select CLS_SEQUENCE from view_section_t where sec_id='" + cmbClass.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0][0].ToString();

            }
            bindAdmNo();
            bindrollnumber();
        }

        private void final_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        
         

        }

   }
         
