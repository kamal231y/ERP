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
using System.IO;
using System.Reflection;
using System.Data.OleDb;
namespace sms
{
    public partial class frmScheduling: Form
    {
        public frmScheduling()
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
            Cancil_form();
        }
        private void Cancil_form()
        {
            cmbClass.Text = "Select";
            cmbExam.Text = "Select";
            LoadGrid();
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;

        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["class"] = "Select";
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
            SqlDataAdapter da = new SqlDataAdapter(" select * from day_t", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DAY_NAME"] = "Select";
            row["DAY"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "DAY_NAME";
            cmbExam.ValueMember = "DAY";
        }
        public void InitGrid()
        {
            LoadGrid();


        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM View_CLASS_SCHEDULING ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["SCH_ID"].Visible = false;
            dataGridView1.Columns["SUBJECT_ID"].Visible = false;
            dataGridView1.Columns["TEACHER_ID"].Visible = false;
            dataGridView1.Columns["Class"].Width = 100;
            dataGridView1.Columns["DAY_NAME"].Width = 150;
            dataGridView1.Columns["DAY_NAME"].HeaderText = "Day";
            dataGridView1.Columns["LECTURE_DESC"].HeaderText = "Lecture";
            dataGridView1.Columns["LECTURE_DESC"].Width = 150;
            dataGridView1.Columns["EMP_NAME"].HeaderText = "Teacher";
            dataGridView1.Columns["EMP_NAME"].Width = 150;
            dataGridView1.Columns["SUBSC_DESC"].HeaderText = "Subject";
            dataGridView1.Columns["SUBSC_DESC"].Width = 150;
            dataGridView1.Columns["EMP_CODE"].HeaderText = "TeacherCode";
            dataGridView1.Columns["EMP_CODE"].Width = 150;
        }
        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from lecture_t  order by lecture_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["lecture_DESC"] = "-Select-";
            row["lecture_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBCATEGORY.DataSource = ds.Tables[0];
            CMBCATEGORY.DisplayMember = "lecture_DESC";
            CMBCATEGORY.ValueMember = "lecture_ID";
        }
        private void bindsubject()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE  subsc_cat_id  in (48,78)  order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "Select";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB1.DataSource = ds.Tables[0];
            CMBSUB1.DisplayMember = "SUBSC_DESC";
            CMBSUB1.ValueMember = "SUBSC_ID";
        }
        private void bindTeacher( int id)
        
        
        {  
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select  EMP_ID,EMP_NAME from TEACHER_SUBJECT_T t1,EMPLOYEE_T t2   order by EMP_ID", sc);
            SqlDataAdapter da = new SqlDataAdapter("select  EMP_ID,EMP_NAME from TEACHER_SUBJECT_T t1,EMPLOYEE_T t2 where t1.TSB_EMP_ID=t2.EMP_ID and t1.TSB_SUB_ID=" + id + " order by EMP_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "-Select-";
            row["EMP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB2.DataSource = ds.Tables[0];
            CMBSUB2.DisplayMember = "EMP_NAME";
            CMBSUB2.ValueMember = "EMP_ID";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
          
            {
                SqlTransaction tran;
                DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbClass.Text == "Select" || cmbClass.Text == "")
                {
                    MsgBox.ShowBox("Pls Select Class ....");
                    return;
                }
                else if (cmbExam.Text == "Select" || cmbExam.Text == "")
                {
                    MsgBox.ShowBox("Pls Select Day ....");
                    return;
                }
                    else if (CMBCATEGORY.Text == "Select" || CMBCATEGORY.Text == "")
                {
                    MsgBox.ShowBox("Pls Select Lecture ....");
                    return;
                }
                    

                       else if (CMBSUB1.Text == "Select" || CMBSUB1.Text == "")
                {
                    MsgBox.ShowBox("Pls Select Subject ....");
                    return;
                }
                       else if (CMBSUB2.Text == "Select" || CMBSUB2.Text == "")
                {
                    MsgBox.ShowBox("Pls Select Teacher ....");
                    return;
                }
                else
                {
                    sc.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr;
                    try
                    {
                        {
                            string status;
                            if (rdsch.Checked == true)
                                status = "0";
                            else
                                status = "1";
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_Scheduling";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@class", cmbClass.SelectedValue);
                            cmd1.Parameters.AddWithValue("@day", cmbExam.SelectedValue);
                            cmd1.Parameters.AddWithValue("@lecture", CMBCATEGORY.SelectedValue);
                            cmd1.Parameters.AddWithValue("@status", Convert.ToInt32(status));
                            cmd1.Parameters.AddWithValue("@subject", CMBSUB1.SelectedValue);
                            cmd1.Parameters.AddWithValue("@Teacher", CMBSUB2.SelectedValue);
                            dr = cmd1.ExecuteReader();
                            if (dr.RecordsAffected > 0)
                            {
                                MsgBox.ShowBox("Record Created...");

                                LoadGrid();
                             //   Cancil_form();
                            }
                            else
                            {
                                MsgBox.ShowBox("Record already exist");
                            }
                        }
                       
                        }
                    catch (Exception ex)
                    {
                        //MsgBox.ShowBox("Subject Marks  Created...");
                    }
                    finally
                    {
                        sc.Close();
                    }


                }
            }
           
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (cmbClass.Text == "Select")
            {
                MsgBox.ShowBox("You Must Select Class ...!!!");
                return;
            }
            if (cmbExam.Text == "Select")
            {
                MsgBox.ShowBox("You Must select Examination...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_RESULT_T_GradingScale_CoSolatic '" + txtid.Text + "','2','" + cmbExam.Text + "'", sc);

                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Grade Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Grade Name already exist");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
        }
        private void SubjectMarks_Load(object sender, EventArgs e)
        {
            bindclass(); bindsubject();
            bindExam(); bindCategory(); LoadGrid();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CMBSUB1_Leave(object sender, EventArgs e)
            {
            int id = 0;
            if (CMBSUB1.SelectedValue != null)
                id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
            bindTeacher(id);
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["SCH_ID"].Value.ToString();
                cmbClass.Text = dataGridView1.CurrentRow.Cells["Class"].Value.ToString();
                cmbExam.Text = dataGridView1.CurrentRow.Cells["DAY_NAME"].Value.ToString();
                CMBCATEGORY.Text = dataGridView1.CurrentRow.Cells["LECTURE_DESC"].Value.ToString();
                CMBSUB1.SelectedValue = dataGridView1.CurrentRow.Cells["SUBJECT_ID"].Value.ToString();
                bindTeacher(Convert.ToInt32(dataGridView1.CurrentRow.Cells["SUBJECT_ID"].Value.ToString()));
                CMBSUB2.SelectedValue = dataGridView1.CurrentRow.Cells["TEACHER_ID"].Value.ToString();

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Record!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                  
                    else
                    {
                        SqlCommand objCmd = new SqlCommand("DELETE FROM CLASS_SCHEDULER_T WHERE SCH_ID=" + txtid.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox(" Record Successfully!!!");

                            LoadGrid();
                            Cancil_form();
                        }
                        else
                        {
                            MsgBox.ShowBox(" Deleting Error!!!");
                        }
                    }
                }
                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void CMBSUB1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
