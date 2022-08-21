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
    public partial class frmEmpAttendance : Form

    {
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmEmpAttendance()
        {
            InitializeComponent();
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView2.Columns["EmplyeeId"].Visible = false;
            dataGridView2.Columns["EmplyeeName"].Width = 200;
            dataGridView2.Columns["Present"].Width = 100;
            dataGridView2.Columns["Present"].HeaderText = "Exempted";
            dataGridView2.Columns["dept_desc"].Width = 100;
            dataGridView2.Columns["dept_desc"].HeaderText = "Department";
            dataGridView2.Columns["desg_desc"].Width = 100;
            dataGridView2.Columns["desg_desc"].HeaderText = "Designation";
        }

        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("FindEmployeeAttendance ", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@attendancedate", dtattendance.Text);

                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
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
    }
}
