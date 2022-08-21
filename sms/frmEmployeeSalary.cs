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
    public partial class frmEmployeeSalary : Form

    {
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmEmployeeSalary()
        {
            InitializeComponent();
            Binddept();
        }
        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "DEPT_DESC";
            cmbDept.ValueMember = "DEPT_ID";
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView2.Columns["Emp_Code"].Width = 100;
            dataGridView2.Columns["Employee"].Width = 200;
            dataGridView2.Columns["Designation"].Width = 100;
            dataGridView2.Columns["Basic"].Width = 100;
            dataGridView2.Columns["Absent"].Width = 100;
            dataGridView2.Columns["Salary"].Width = 100;
            dataGridView2.Columns["PrevBalance"].Width = 100;
            dataGridView2.Columns["EPF"].Width = 100;
            dataGridView2.Columns["Net"].Width = 100;
            dataGridView2.Columns["Paid"].Width = 100;
            dataGridView2.Columns["SAL_YEAR"].Visible = false;
            dataGridView2.Columns["SAL_MONTH"].Visible = false;
            dataGridView2.Columns["Department"].Visible = false;
        }

        private void LoadGrid()
        {
            DateTime ddt1;
            ddt1 = dtattendance.Value;

            string month ;
            string Year;
            month = ddt1.Month.ToString();
            Year = ddt1.Year.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT EMP_CODE,Employee,Department,designation,Basic,Sal_Days,Absent ,Salary,HRA,SA,Advance,EPF,ESI,PrevBalance,Net,Paid,Balance,SAL_YEAR,SAL_MONTH FROM View_EmployeeSalary where SAL_MONTH=" + month + " AND SAL_YEAR=" + Year + " and EMP_DEPT_ID like '" + cmbDept.SelectedValue + "' ", sc);
                //where Department like '" + cmbDept.Text + "%' and SAL_MONTH like '" + month + "' and SAL_YEAR='"+Year+"'
               

                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
            }
        }
        
    
        private void FeeExampted_Load(object sender, EventArgs e)
        {
            InitGrid();
           
        }
        void Cancel_Form()
        {
           

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

        }

   

        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //SqlTransaction tran;
          
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();

           
            //sc.Open();
            //tran = sc.BeginTransaction();
            //SqlCommand cmd1 = new SqlCommand();
            //SqlDataReader dr;
            //cmd1.Transaction = tran;
            //try
            //{

            //    //if (cmd1.ExecuteNonQuery() > 0)
            //    //{

            //    for (int z = 0; z < dataGridView2.Rows.Count - 1; z++)
            //    {
            //        cmd1 = new SqlCommand();
            //        cmd1.CommandType = CommandType.StoredProcedure;
            //        cmd1.CommandText = "insert_Student_FEE_Exemption";
            //        cmd1.Connection = sc;
            //        //cmd1.Parameters.AddWithValue("@receiptno", Convert.ToInt32(txt_receiptno.Text));
            //        cmd1.Parameters.AddWithValue("@STU_ID", 0);
            //        cmd1.Parameters.AddWithValue("@FEE_ID", dataGridView2.Rows[z].Cells["Fee_Id"].Value);
            //        cmd1.Parameters.AddWithValue("@Exempted", dataGridView2.Rows[z].Cells["Exemption"].Value);
                   
            //        cmd1.Transaction = tran;
            //        cmd1.ExecuteNonQuery();
            //    }
            //    tran.Commit();
            //    MsgBox.ShowBox("RECORD SAVED...");
            //    LoadGrid();
            //    //Cancil_form();
            //    //}
            //    //else
            //    //{
            //    //    MsgBox.ShowBox("This Subject Marks  already exist");
            //    //}

            //}
            //catch (Exception ex)
            //{
            //    tran.Rollback();
            //    //MsgBox.ShowBox("Subject Marks  Created...");
            //}
            //finally
            //{
            //    sc.Close();
            //}

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtattendance_ValueChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }



        private void btnCreate_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure tO Create Salary!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    DateTime ddt1;
                    ddt1 = dtattendance.Value;

                    string month;
                    string Year;
                    month = ddt1.Month.ToString();
                    Year = ddt1.Year.ToString();
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("Bind_SALARY", sc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@DEPT", Convert.ToInt32(cmbDept.SelectedValue));
                    da.SelectCommand.Parameters.AddWithValue("@SAL_MONTH", Convert.ToInt32(month));
                    da.SelectCommand.Parameters.AddWithValue("@SAL_YEAR", Convert.ToInt32(Year));
                    sc.Open();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void cmbDept_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

     
    }
}
