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

namespace sms
{
    public partial class frmteacherSubject : Form
    {
        DataTable StudentFee = new DataTable();

        CheckBox chkbox = new CheckBox();
        DataTable dt;
        int RowIndex = 0;
        int RowIndex2 = 0;
        public frmteacherSubject()
        {
            InitializeComponent();
        }

        public void InitGrid1()
        {
            LoadGrid();
            dataGridView1.Columns["FEE_ID"].Visible = false;
            dataGridView1.Columns["FEE_DESC"].Width = 200;
            dataGridView1.Columns["FEE_DESC"].HeaderText = "Subject Avilable";
        }

        void LoadGrid()
        {
            DataTable dt = new DataTable();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select subsc_id as FEE_ID,subsc_desc as FEE_DESC from SUBJECT_SUB_CATEGORY_T WHERE  subsc_cat_id  in (48,78) order by SUBSC_DESC", sc);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DESIGNATION_T  order by DESG_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DESG_DESC"] = "Select";
            row["DESG_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "DESG_DESC";
            cmb_class.ValueMember = "DESG_ID";
        }
        private void bindrollno()
        {
            string StateName;
            if (cmb_class.Text == "Select" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from Employee_t where EMP_DESG_ID ='" + StateName + "' order by  EMP_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_ID"] = "0";
            dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "EMP_NAME";
            cmb_rollno.ValueMember = "EMP_ID";
        }

        public void InitGrid2()
        {
            LoadGrid2();
            dataGridView2.Columns["Subject_Selected"].Width = 200;
            dataGridView2.Columns["Subject_Selected"].HeaderText = "Subject Selected";
            dataGridView2.Columns["FEE_ID"].Visible = false;
        }

        private void LoadGrid2()
        {
            StudentFee.Rows.Clear();

            string str;
            string str1;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "System.Data.DataRowView")
                str1 = "0";
            else
                str1 = cmb_rollno.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select t1.TSB_SUB_ID as FEE_ID,t2.SUBSC_DESC  as Subject_Selected from TEACHER_SUBJECT_T t1,SUBJECT_SUB_CATEGORY_T t2 where t1.TSB_SUB_ID=t2.SUBSC_ID and t1.TSB_EMP_ID=" + str1 + "", sc);

            da.Fill(StudentFee);
            dataGridView2.DataSource = StudentFee;


        }
        private void Bindname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  emp_name from employee_t where emp_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["emp_name"].ToString();
                    //+ dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
            }

        }
        void Cancel_Form()
        {
            cmb_class.Text = "-Select-";
            StudentFee.Rows.Clear();
            cmb_rollno.Text = "";
            txtStuname.Text = "";
            dataGridView1.DataSource = DBNull.Value;
            dataGridView2.DataSource = DBNull.Value;


        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }





        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                MsgBox.ShowBox("Please Select Designation !!!!!");
                return;
            }
            else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
            {
                MsgBox.ShowBox("Please Select Emplopyee !!!!!");
                return;
            }
            else if (dataGridView2.Rows.Count <= 0)
            {
                MsgBox.ShowBox("Please Select Subject !!!!!");
                return;
            }
            else
            {
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from TEACHER_SUBJECT_T where TSB_EMP_ID=" + cmb_rollno.SelectedValue + " ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmd12 = new SqlCommand(" delete from TEACHER_SUBJECT_T where TSB_EMP_ID=" + cmb_rollno.SelectedValue + "", sc);
                    cmd12.ExecuteNonQuery();
                }
                SqlTransaction tran;
                tran = sc.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr;
                cmd1.Transaction = tran;
                try
                {


                    for (int z = 0; z <= dataGridView2.Rows.Count - 1; z++)
                    {
                        cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "Insert_TEACHER_SUBJECT_T";
                        cmd1.Connection = sc;

                        cmd1.Parameters.AddWithValue("@TSB_EMP_ID", Convert.ToInt32(cmb_rollno.SelectedValue));
                        //cmd1.Parameters.AddWithValue("@SEF_CLS_ID", Convert.ToInt32(cmb_class.SelectedValue));
                        cmd1.Parameters.AddWithValue("@TSB_SUB_ID", Convert.ToInt32(dataGridView2.Rows[z].Cells["FEE_ID"].Value));

                        cmd1.Transaction = tran;
                        cmd1.ExecuteNonQuery();
                    }
                    tran.Commit();
                    MsgBox.ShowBox("Teacher Subject Created...");
                    LoadGrid();
                    // Cancil_form();
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

        private void btnGT_Click(object sender, EventArgs e)
        {
            int count = dataGridView2.Rows.Count;
            int ID = (int)dataGridView1.Rows[RowIndex].Cells[0].Value;
            Boolean TF = false;
            for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
            {
                int feeid = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value);
                if (ID == feeid)
                {
                    TF = true;
                    break;

                }
            }
            if (TF == false)
            {

                DataRow dr = StudentFee.NewRow();
                dr["FEE_ID"] = dataGridView1.Rows[RowIndex].Cells[0].Value;
                dr["Subject_Selected"] = dataGridView1.Rows[RowIndex].Cells[1].Value;
                StudentFee.Rows.Add(dr);
                dataGridView2.DataSource = StudentFee;
                dataGridView2.Columns["Subject_Selected"].Width = 200;
                dataGridView2.Columns["Subject_Selected"].HeaderText = "Subject Selected";
                dataGridView2.Columns["FEE_ID"].Visible = false;

            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                try
                {
                    StudentFee.Rows.RemoveAt(RowIndex2);
                    dataGridView2.DataSource = StudentFee;
                    dataGridView2.Columns["Subject_Selected"].Width = 200;
                    dataGridView2.Columns["Subject_Selected"].HeaderText = "Subject Selected";
                    dataGridView2.Columns["FEE_ID"].Visible = false;

                }
                catch (Exception ex)
                { }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_rollno.Text != "")
            {
                StudentFee.Rows.Clear();
                int count = dataGridView1.Rows.Count;
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        DataRow dr = StudentFee.NewRow();
                        dr["FEE_ID"] = dataGridView1.Rows[i].Cells[0].Value;
                        dr["Subject_Selected"] = dataGridView1.Rows[i].Cells[1].Value;
                        StudentFee.Rows.Add(dr);
                    }
                    dataGridView2.DataSource = StudentFee;
                    dataGridView2.Columns["Subject_Selected"].Width = 200;
                    dataGridView2.Columns["Subject_Selected"].HeaderText = "Subject Selected";
                    dataGridView2.Columns["FEE_ID"].Visible = false;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = DBNull.Value;
            StudentFee.Rows.Clear();
        }

        private void frmStudentFee_Load(object sender, EventArgs e)
        {
            bindClass();

        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex2 = e.RowIndex;

        }

        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridView2.DataSource = DBNull.Value;
            //InitGrid2();
            //txtStuname.Text = "";
            //Bindname();
        }

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = DBNull.Value;
            //dataGridView2.DataSource = DBNull.Value;
            //cmb_rollno.Text = "";
            //txtStuname.Text = "";
            //bindrollno();
            //InitGrid1();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Student Fee!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
                    {
                        MsgBox.ShowBox("Please Select Class !!!!!");
                        return;
                    }
                    else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
                    {
                        MsgBox.ShowBox("Please Select RollNo !!!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("delete from STUDENT_FEE_T where SEF_STU_ID=" + cmb_rollno.SelectedValue + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Student Fee Deleted Successfully!!!");

                        //  LoadGrid();
                        Cancel_Form();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex2 = e.RowIndex;
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBNull.Value;
            dataGridView2.DataSource = DBNull.Value;
            cmb_rollno.Text = "";
            txtStuname.Text = "";
            bindrollno();
            InitGrid1();
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            dataGridView2.DataSource = DBNull.Value;
            InitGrid2();
            txtStuname.Text = "";
            Bindname();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure to create Student Fee for all class Student!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
                {
                    MsgBox.ShowBox("Please Select Class !!!!!");
                    return;
                }
                else if (dataGridView2.Rows.Count == 0)
                {
                    MsgBox.ShowBox("Please Select Fee !!!!!");
                    return;
                }
                else
                {
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    sc.Open();
                    SqlTransaction tran;
                    tran = sc.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr;
                    cmd1.Transaction = tran;
                    try
                    {


                        for (int z = 0; z <= dataGridView2.Rows.Count - 1; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "Insert_STUDENT_FEE_T_ClassWise";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@SEF_CLS_ID", Convert.ToInt32(cmb_class.SelectedValue));
                            cmd1.Parameters.AddWithValue("@SEF_FEE_ID", Convert.ToInt32(dataGridView2.Rows[z].Cells["FEE_ID"].Value));
                            cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        MsgBox.ShowBox("Student Fee Created...");
                        LoadGrid();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MsgBox.ShowBox("error...");
                    }
                    finally
                    {
                        sc.Close();
                    }


                }
            }
        }
       
    }
}
