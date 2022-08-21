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
    public partial class frmStudentMarksRollwise : Form
    {
        string stuid;
        public frmStudentMarksRollwise()
        {
            InitializeComponent();
            bindclass(); bindExam();
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


        private void btn_create_Click(object sender, EventArgs e)
        {
            SqlTransaction tran;
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
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
            else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
            {
                MsgBox.ShowBox("Please Select RollNo ");
                return;
            }
            else
            {

                sc.Open();
                tran = sc.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand();
                //SqlCommand cmd1 = new SqlCommand("insert_SUBJECT_MARKS_T '" + cmbClass.SelectedValue + "','" + cmbExam.SelectedValue + "','" + Stuid + "','" + txt_month.Text + "','" + txt_nettotal.Text + "','" + txt_paid.Text + "','" + txt_prevdue.Text + "','" + txt_concession.Text + "','" + cmbmop.SelectedValue + "','" + txtDDNo.Text + "','" + cmbBankName.SelectedValue + "','" + Convert.ToDateTime(dt3) + "','" + cmb_accounts.SelectedValue + "','" + txt_coments.Text + "','" + txt_balance.Text + "','" + txt_totalfee.Text + "','" + Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + Convert.ToDateTime(dt1) + "'", sc);
                //SqlCommand cmd1 = new SqlCommand("select * from View_SUBJECT_MARKS_T  where SBM_EXAM_ID='" + cmbExam.SelectedValue + "' and SBM_CLS_ID='" + cmbClass.SelectedValue + "'", sc);
                SqlDataReader dr;
                cmd1.Transaction = tran;
                try
                {

                    //if (cmd1.ExecuteNonQuery() > 0)
                    //{

                    for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
                    {
                        cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "insert_Student_SUBJECT_MARKS";
                        cmd1.Connection = sc;
                        cmd1.Parameters.AddWithValue("@stu_id", cmb_rollno.SelectedValue);
                        cmd1.Parameters.AddWithValue("@SBM_CLS_ID", dataGridView1.Rows[z].Cells["secid"].Value);
                        cmd1.Parameters.AddWithValue("@SBM_EXAM_ID", cmbExam.SelectedValue);
                        cmd1.Parameters.AddWithValue("@SBM_SUB_ID", dataGridView1.Rows[z].Cells["Subid"].Value);
                        if (dataGridView1.Rows[z].Cells["Marks"].Value==null)
                            cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", 0);
                        else
                            cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", dataGridView1.Rows[z].Cells["Marks"].Value);
                        cmd1.Parameters.AddWithValue("@FullMarks", dataGridView1.Rows[z].Cells["FullMark"].Value);
                       
                        cmd1.Transaction = tran;
                        cmd1.ExecuteNonQuery();
                    }
                    tran.Commit();
                    MsgBox.ShowBox("Student Subject Marks Created...");
                    LoadGrid();
                    //Cancil_form();
                    //}
                    //else
                    //{
                    //    MsgBox.ShowBox("This Subject Marks  already exist");
                    //}

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    //MsgBox.ShowBox("Subject Marks  Created...");
                }
                finally
                {
                    sc.Close();
                }

            }

        }



        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbClass.SelectedValue = 0;
            cmbExam.SelectedValue = 0;
            cmbExam.SelectedValue = 0;
            cmb_rollno.SelectedValue = 0;
            dataGridView1.DataSource = DBNull.Value;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            //bindclass(); bindExam();
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


        public void InitGrid()
        {
            LoadGrid();
            //dataGridView1.Columns["SEC_ID"].Width = 40;
            //dataGridView1.Columns["SEC_DESC"].Width = 100;
            ////dataGridView1.Columns["Alias"].Width = 60;
            //dataGridView1.Columns["userid"].Width = 60;
            //dataGridView1.Columns["date"].Width = 80;
            //dataGridView1.Columns["terminalid"].Width = 100;
        }
        private void LoadGrid()
        {

            string str1; string str2; string str3;
            dataGridView1.AutoGenerateColumns = false;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
                str1 = "0";
            else
                str1 = cmbClass.SelectedValue.ToString();
            if (cmbExam.Text == "" || cmbExam.Text == "-Select-" || cmbExam.Text.Trim() == "System.Data.DataRowView")
                str2 = "0";
            else
                str2 = cmbExam.SelectedValue.ToString();
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "-Select-" || cmb_rollno.Text.Trim() == "System.Data.DataRowView")
                str3 = "0";
            else
                str3 = cmb_rollno.SelectedValue.ToString();
            SqlDataAdapter da = new SqlDataAdapter("Find_StudentMarksRollWise", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@class", str1);
            da.SelectCommand.Parameters.AddWithValue("@exam", str2);
            da.SelectCommand.Parameters.AddWithValue("@roll", str3);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }


    

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StateName;
            if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmbClass.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
            

        }

        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
    


    }
         
}
