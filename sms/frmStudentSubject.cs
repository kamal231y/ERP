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
    public partial class frmStudentSubject : Form
    {
        DataTable StudentFee = new DataTable();
          
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        int RowIndex = 0;
        int RowIndex2 = 0;
        public frmStudentSubject()
        {
            InitializeComponent();
        }

        public void InitGrid1()
        {
            LoadGrid();
            dataGridView1.Columns["SUB_ID"].Visible = false;
            dataGridView1.Columns["SUB_DESC"].Width = 200;
            dataGridView1.Columns["SUB_DESC"].HeaderText = "Subject Avilable";
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
            SqlDataAdapter da = new SqlDataAdapter("select SUB_ID,SUB_DESC from View_ClassSubject2 where SEC_ID = '" + str + "' ORDER BY SUB_ID", sc);
            da.Fill(dt);
             dataGridView1.DataSource = dt;
            
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";
        }
        private void bindrollno()
        {
            string StateName;
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID =  '" + StateName + "' order by  STC_ROLL_NO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "-Select-";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
        }
       
        public void InitGrid2()
        {
            try
            {
                LoadGrid2();
                dataGridView2.Columns["SUB_DESC"].Width = 200;
                dataGridView2.Columns["SUB_DESC"].HeaderText = "Subject Selected";
                dataGridView2.Columns["SUB_ID"].Visible = false;
            }
            catch (Exception ex)
            {
               // MsgBox.ShowBox(ex.ToString());
  
            }
        }

        private void LoadGrid2()
        {
            StudentFee.Rows.Clear();
            
            string str;
            string str1;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
            {
                str = "0";
            }
            else
            {
                str = cmb_class.SelectedValue.ToString();
            }
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "System.Data.DataRowView")
            {
                str1 = "0";
            }
            else
            {
                str1 = cmb_rollno.SelectedValue.ToString();
            }
           dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select  SUB_ID,SUB_DESC from View_classStudentSubject2 where  STS_STU_ID=" + str1 + "", sc);

            da.Fill(StudentFee);
            dataGridView2.DataSource = StudentFee;

           
        }
        private void Bindname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from student_T where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
            }
            else
            {
                txtStuname.Text = "";
            
            }

        }
        void Cancel_Form()
        {
            cmb_class.Text = "-Select-";
            StudentFee.Rows.Clear();
            cmb_rollno.Text = "";
            txtStuname.Text = "";

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
                MsgBox.ShowBox("Please Select Class !!!!!");
                return;
            }
            else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
            {
                MsgBox.ShowBox("Please Select RollNo !!!!!");
                return;
            }
            else if (dataGridView2.Rows.Count < 0)
            {
                MsgBox.ShowBox("Please Select subject !!!!!");
                return;
            }
            else
            {
                sc.Open();


                SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_SUBJECT_T where STS_STU_ID=" + cmb_rollno.SelectedValue + " ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmd12 = new SqlCommand(" delete from STUDENT_SUBJECT_T where STS_STU_ID=" + cmb_rollno.SelectedValue + "", sc);
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
                        cmd1.CommandText = "insert_STUDENT_SUBJECT_T";
                        cmd1.Connection = sc;

                        cmd1.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(cmb_rollno.SelectedValue));
                       // cmd1.Parameters.AddWithValue("@STS_SUB_ID", Convert.ToInt32(cmb_class.SelectedValue));
                        cmd1.Parameters.AddWithValue("@STS_SUB_ID", Convert.ToInt32(dataGridView2.Rows[z].Cells["SUB_ID"].Value));
                        //STS_STU_ID  ,	STS_SUB_ID  
                        cmd1.Transaction = tran;
                        cmd1.ExecuteNonQuery();
                    }
                    tran.Commit();
                    MsgBox.ShowBox("Student subject Created...");
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
            int count = dataGridView2.Rows.Count ;
            int ID = (int)dataGridView1.Rows[RowIndex].Cells[0].Value;
            Boolean TF = false;
            for (int i= 0; i < dataGridView2.Rows.Count ; i++)
            {
                int feeid=Convert.ToInt32( dataGridView2.Rows[i].Cells[0].Value);
                    if (ID == feeid)
                {
                    TF = true;
                    break;
                   
                }
            }
            if (TF == false)
            {

                DataRow dr = StudentFee.NewRow();
                dr["SUB_ID"] = dataGridView1.Rows[RowIndex].Cells[0].Value;
                dr["SUB_DESC"] = dataGridView1.Rows[RowIndex].Cells[1].Value;
                StudentFee.Rows.Add(dr);
                dataGridView2.DataSource = StudentFee;
                dataGridView2.Columns["SUB_DESC"].Width = 200;
                dataGridView2.Columns["SUB_DESC"].HeaderText = "Subject Selected";
                dataGridView2.Columns["SUB_ID"].Visible = false;
                               
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
                    StudentFee.Rows.RemoveAt(RowIndex);
                    dataGridView2.DataSource = StudentFee;
                    dataGridView2.Columns["SUB_DESC"].Width = 200;
                    dataGridView2.Columns["SUB_DESC"].HeaderText = "Subject Selected";
                    dataGridView2.Columns["SUB_ID"].Visible = false;

                }
                catch (Exception ex)
                { }
                
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_rollno.Text != "" )
            {
                StudentFee.Rows.Clear();
                int count = dataGridView1.Rows.Count;
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        DataRow dr = StudentFee.NewRow();
                        dr["SUB_ID"] = dataGridView1.Rows[i].Cells[0].Value;
                        dr["SUB_DESC"] = dataGridView1.Rows[i].Cells[1].Value;
                        StudentFee.Rows.Add(dr);
                    }
                    dataGridView2.DataSource = StudentFee;
                    dataGridView2.Columns["SUB_DESC"].Width = 200;
                    dataGridView2.Columns["SUB_DESC"].HeaderText = "Subject Selected";
                    dataGridView2.Columns["SUB_ID"].Visible = false;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = DBNull.Value;
            StudentFee.Rows.Clear();
        }

       

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex2 = e.RowIndex;

        }

        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
        }

        

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Student Subject!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand("delete from STUDENT_SUBJECT_T where STS_STU_ID=" + cmb_rollno.SelectedValue + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Student Subject Deleted Successfully!!!");

                        //  LoadGrid();
                        //Cancil_form();
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

        private void frmStudentSubject_Load(object sender, EventArgs e)
        {
            bindClass();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            bindrollno();
            InitGrid1();
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            InitGrid2();
            txtStuname.Text = "";
            Bindname();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure to create Student Subject for all class Student!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
                {
                    MsgBox.ShowBox("Please Select Class !!!!!");
                    return;
                }
                else if (dataGridView2.Rows.Count == 0)
                {
                    MsgBox.ShowBox("Please Select Subject !!!!!");
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
                            cmd1.CommandText = "Insert_STUDENT_SUBJECT_T_ClassWise";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@SEF_CLS_ID", Convert.ToInt32(cmb_class.SelectedValue));
                            cmd1.Parameters.AddWithValue("@STS_SUB_ID", Convert.ToInt32(dataGridView2.Rows[z].Cells["SUB_ID"].Value));
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