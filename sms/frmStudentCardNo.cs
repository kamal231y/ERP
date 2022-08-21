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
    public partial class frmStudentCardNo : Form
    {
        int rollNo = 0;
        int stuid = 0; int RowIndex = -1;
        Boolean TF = false;
        public frmStudentCardNo()
        {
            InitializeComponent();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }



        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            bindclass();
            LoadGrid();
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
            DataTable ds = new DataTable();
            string str;
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmbClass.SelectedValue.ToString();


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,STU_ADM_NO,STU_FIRST_NAME,STC_ROLL_NO,CARD_NO from View_studentCard where SEC_CLS_ID like " + str + " order by STC_ROLL_NO", sc);

            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                dataGridView1.DataSource = ds;
            }

        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbClass.Text = "-Select-";
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnupdate_Click(int sid, int roll)
        {
            SqlTransaction tran;
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbClass.Text == "-Select-")
            {
                MsgBox.ShowBox("class should not be blank");
                return;
            }
            sc.Open();
            tran = sc.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand();

            SqlDataReader dr;
            cmd1.Transaction = tran;
            try
            {

                cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "Update_RollNo";
                cmd1.Connection = sc;
                cmd1.Parameters.AddWithValue("@SBM_CLS_ID", cmbClass.SelectedValue);
                cmd1.Parameters.AddWithValue("@rollNo", roll);
                cmd1.Parameters.AddWithValue("@stuid", stuid);
                cmd1.Transaction = tran;
                cmd1.ExecuteNonQuery();

                tran.Commit();
                MsgBox.ShowBox("Roll No Updated...");
                //LoadGrid();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
            stuid = Convert.ToInt32(dataGridView1.Rows[RowIndex].Cells[0].Value);
            if (dataGridView1.Rows.Count > 0)
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns[3].Index)
                    dataGridView1.BeginEdit(true);
        }





        private void btnupdate_Click(object sender, EventArgs e)
        {
            SqlTransaction tran;
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            
            if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            {
                MsgBox.ShowBox("Please Select class ");
                return;
            }
           
            else
            {

                sc.Open();
                tran = sc.BeginTransaction();
                SqlCommand cmd1 = new SqlCommand();
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
                        cmd1.CommandText = "insert_studentCard_T";
                        cmd1.Connection = sc;
                        cmd1.Parameters.AddWithValue("@STUDENT_ID", Convert.ToInt32( dataGridView1.Rows[z].Cells["STU_ID"].Value));
                        cmd1.Parameters.AddWithValue("@CARD_NO", dataGridView1.Rows[z].Cells["CARD_NO"].Value);
                        cmd1.Transaction = tran;
                        cmd1.ExecuteNonQuery();
                    }
                    tran.Commit();
                    MsgBox.ShowBox("Student Card Number Created...");
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
    }
}
