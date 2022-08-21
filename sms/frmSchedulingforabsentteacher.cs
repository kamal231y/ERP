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
    public partial class frmSchedulingforabsentteacher: Form
    {
        public frmSchedulingforabsentteacher()
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
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select T2.EMP_ID,T2.EMP_NAME from EMPLOYEE_ATTENDANCE_T t1,EMPLOYEE_T t2 where t1.SAT_STU_ID=t2.EMP_ID and T1.ATT_TYPE='A' AND cast(sat_date as date) ='" + dtdob.ToString("MM/dd/yyyy") + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "EMP_NAME";
            cmbClass.ValueMember = "EMP_ID";
        }
        private void bindExam()
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT lecture_id,LECTURE_DESC FROM View_CLASS_SCHEDULING where DAY_name='" + dtdob.DayOfWeek + "' and TEACHER_ID='" + cmbClass.SelectedValue + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["LECTURE_DESC"] = "Select";
            row["lecture_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "LECTURE_DESC";
            cmbExam.ValueMember = "lecture_ID";
        }
        public void InitGrid()
        {
            LoadGrid();


        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_absent_schedule ORDER BY SCH_ID DESC ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;           
        }
        private void bindCategory()
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" SELECT * FROM View_CLASS_SCHEDULING where DAY_name='" + dtdob.DayOfWeek + "' and TEACHER_ID='" + cmbClass.SelectedValue + "'  and LECTURE_id='" + cmbExam.SelectedValue + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
     
            CMBCATEGORY.DataSource = ds.Tables[0];
            CMBCATEGORY.DisplayMember = "subsc_DESC";
            CMBCATEGORY.ValueMember = "subject_ID";
        }
        private void bindsubject()
            {
             DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM View_CLASS_SCHEDULING where DAY_name='" + dtdob.DayOfWeek + "' and emp_id  IN(select T2.EMP_ID from EMPLOYEE_ATTENDANCE_T t1,EMPLOYEE_T t2 where t1.SAT_STU_ID=t2.EMP_ID and T1.ATT_TYPE='P' AND CAST(SAT_DATE AS DATE)='" + dtdob.ToString("MM/dd/yyyy") + "' AND lecture_id not in ('" + cmbExam.SelectedValue + "') and class= ('" + CMBSUB2.Text + "'))", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB1.DataSource = ds.Tables[0];
            CMBSUB1.DisplayMember = "EMP_NAME";
            CMBSUB1.ValueMember = "EMP_ID";
        }
        private void bindallteacher()
        
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM View_CLASS_SCHEDULING where DAY_name='" + dtdob.DayOfWeek + "' and emp_id  IN(select T2.EMP_ID from EMPLOYEE_ATTENDANCE_T t1,EMPLOYEE_T t2 where t1.SAT_STU_ID=t2.EMP_ID and T1.ATT_TYPE='P' AND CAST(SAT_DATE AS DATE)='" + dtdob.ToString("MM/dd/yyyy") + "' AND lecture_id not in ('" + cmbExam.SelectedValue + "'))", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB1.DataSource = ds.Tables[0];
            CMBSUB1.DisplayMember = "EMP_NAME";
            CMBSUB1.ValueMember = "EMP_ID";
        }
        private void bindTeacher()
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT SEC_ID,class FROM View_CLASS_SCHEDULING where DAY_name='" + dtdob.DayOfWeek + "' and emp_id ='" + cmbClass.SelectedValue + "'AND lecture_id in ('" + cmbExam.SelectedValue + "')", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
           
            CMBSUB2.DataSource = ds.Tables[0];
            CMBSUB2.DisplayMember = "class";
            CMBSUB2.ValueMember = "SEC_ID";
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
                    DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
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
                            cmd1.CommandText = "insert_Schedulingabsentteacher";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@dATE", dtdob.ToString());
                            cmd1.Parameters.AddWithValue("@Teacher", cmbClass.SelectedValue);
                            cmd1.Parameters.AddWithValue("@lecture", cmbExam.SelectedValue);
                            cmd1.Parameters.AddWithValue("@subject", CMBCATEGORY.SelectedValue);
                            cmd1.Parameters.AddWithValue("@status", Convert.ToInt32(status));
                            cmd1.Parameters.AddWithValue("@NEW_TEACHER", CMBSUB1.SelectedValue);
                            cmd1.Parameters.AddWithValue("@class", CMBSUB2.SelectedValue);
                            cmd1.Parameters.AddWithValue("@day", dtdob.DayOfWeek);
                            
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
            //bindclass(); bindsubject();
            //bindExam();
            //bindCategory();
            //LoadGrid();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CMBSUB1_Leave(object sender, EventArgs e)
        {
            //int id = 0;
            //if (CMBSUB1.SelectedValue != null)
            //    id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
            //bindTeacher(id);
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            crySchedulerforabsent.dt1 = dob.Value;
            crySchedulerforabsent obj=new crySchedulerforabsent();
            obj.ShowDialog();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                
                txtid.Text = dataGridView1.CurrentRow.Cells["SCH_ID"].Value.ToString();
                dob.Text= dataGridView1.CurrentRow.Cells["sch_date"].Value.ToString();

                //cmbClass.Text = dataGridView1.CurrentRow.Cells["OLD_TEACHER"].Value.ToString();
                //cmbExam.Text = dataGridView1.CurrentRow.Cells["LECTURE_DESC"].Value.ToString();
                //CMBCATEGORY.Text = dataGridView1.CurrentRow.Cells["SUBSC_DESC"].Value.ToString();
                ////CMBSUB1.SelectedValue =Convert.ToInt16(dataGridView1.CurrentRow.Cells["New_Teacher"].Value.ToString());                
                ////CMBSUB2.Text = dataGridView1.CurrentRow.Cells["CLASS"].Value.ToString();

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
                        SqlCommand objCmd = new SqlCommand("DELETE FROM CLASS_SCHEDULER_ABSENT_TEACHER WHERE SCH_ID=" + txtid.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox(" Record Delete Successfully!!!");

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

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void cmbExam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            bindExam();
        }

        private void cmbExam_Leave(object sender, EventArgs e)
        {
            bindCategory();
            bindTeacher();
            if (rdsch.Checked == true)
            {
                bindsubject();
            }
            else if (rdcosch.Checked == true)
            {
                bindallteacher();
            }

            
        }

        private void dob_ValueChanged(object sender, EventArgs e)
        {
            bindclass();

        }

        private void CMBCATEGORY_Leave(object sender, EventArgs e)
        {
            bindallteacher();
        }

        private void rdsch_Leave(object sender, EventArgs e)
        {
            if (rdsch.Checked == true)
            {
                bindsubject();
            }

        }

        private void rdcosch_Leave(object sender, EventArgs e)
        {
            bindallteacher();
        }

        private void CMBSUB2_Leave(object sender, EventArgs e)
        {
            bindsubject();
        }

        private void rdsch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdsch.Checked == true)
            {
                bindsubject();
            }
            else if (rdcosch.Checked == true)
            {
                bindallteacher();
            }
        }

        private void rdcosch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdsch.Checked == true)
            {
                bindsubject();
            }
            else if (rdcosch.Checked == true)
            {
                bindallteacher();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {

                txtid.Text = dataGridView1.CurrentRow.Cells["SCH_ID"].Value.ToString();
                cmbClass.Text = dataGridView1.CurrentRow.Cells["OLD_TEACHER"].Value.ToString();
                cmbExam.Text = dataGridView1.CurrentRow.Cells["LECTURE_DESC"].Value.ToString();
                CMBCATEGORY.Text = dataGridView1.CurrentRow.Cells["SUBSC_DESC"].Value.ToString();
                //CMBSUB1.SelectedValue =Convert.ToInt16(dataGridView1.CurrentRow.Cells["New_Teacher"].Value.ToString());                
                //CMBSUB2.Text = dataGridView1.CurrentRow.Cells["CLASS"].Value.ToString();

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }

        }
    }
}
