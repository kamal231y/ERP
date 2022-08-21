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
    public partial class FrmStudentRoll : Form
    {
        int rollNo = 0;
        int stuid = 0; int RowIndex = -1;
        Boolean TF = false;
        public FrmStudentRoll()
        {
            InitializeComponent();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by CLS_SEQUENCE ", sc);
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
            //LoadGrid();
            dataGridView1.Columns["stu_ID"].Visible = false;
            dataGridView1.Columns["ADMNO"].Width = 100;
            //dataGridView1.Columns["Alias"].Width = 60;
            dataGridView1.Columns["student_name"].Width = 400;
            dataGridView1.Columns["ROLL"].Width = 100;
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
            //SqlDataAdapter da = new SqlDataAdapter("select distinct sr.STU_ID,sr.STU_ADM_NO,(sr.STU_FIRST_NAME+sr.STU_MID_NAME +sr.STU_LAST_NAME) as StudentName,ts.STC_ROLL_NO from  STUDENT_T sr,STUDENT_CLASS_T ts where sr.STU_ID=ts.STC_STU_ID and sr.STU_CLS_ID like " + str + " order by ts.STC_ROLL_NO", sc);
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,ADMNO,student_name as StudentName,ROLL from  view_studentreport where STC_SEC_ID like " + str + " and stu_status='Y' order by roll", sc);
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                dataGridView1.DataSource = ds;
                dataGridView1.Columns["stu_ID"].Visible = false;
                dataGridView1.Columns["ADMNO"].Width = 100;
                //dataGridView1.Columns["Alias"].Width = 60;
                dataGridView1.Columns["StudentName"].Width = 450;
                dataGridView1.Columns["ROLL"].Width = 100;

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



        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            rollNo = Convert.ToInt32(dataGridView1.CurrentCell.Value);
            DataTable ds = new DataTable();
            string str;
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmbClass.SelectedValue.ToString();

            dataGridView1.AutoGenerateColumns = false;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select ROLL from  view_studentreport where STC_SEC_ID like " + str + " and stu_status='Y' order by roll", sc);

            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                rollNo = Convert.ToInt32(dataGridView1.CurrentCell.Value);

                RowIndex = e.RowIndex;
                int count = dataGridView1.Rows.Count - 1;

                Boolean TF = false;
                foreach (DataRow row in ds.Rows)
                {
                    if (Convert.ToInt32(row[0].ToString()) == (rollNo))
                    {
                        TF = true;
                        break;
                    }
                }

                if (TF)
                {
     
                    MsgBox.ShowBox("This Roll No Is Already Alloted .....");                  
                }
                else
                {
                    if (rollNo != 0)
                        btnupdate_Click(stuid, rollNo);
                }
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            SqlTransaction tran;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            sc.Open();
            tran = sc.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand();

            SqlDataReader dr;
            cmd1.Transaction = tran;
            try
            {
                for (int z = 0; z < dataGridView1.Rows.Count; z++)
                {
                    cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "Update_RollNo";
                    cmd1.Connection = sc;
                    cmd1.Parameters.AddWithValue("@SBM_CLS_ID", cmbClass.SelectedValue);
                    if (dataGridView1.Rows[z].Cells["ROLL"].Value.ToString() == "")
                        cmd1.Parameters.AddWithValue("@rollNo", 0);
                    else
                        cmd1.Parameters.AddWithValue("@rollNo", dataGridView1.Rows[z].Cells["ROLL"].Value);
                    cmd1.Parameters.AddWithValue("@stuid", dataGridView1.Rows[z].Cells["STU_ID"].Value);
                    cmd1.Transaction = tran;
                    cmd1.ExecuteNonQuery();

                }
                MsgBox.ShowBox("Roll No. Updated...");
                tran.Commit();
                LoadGrid();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MsgBox.ShowBox("Roll No. Not Updated...");
            }
            finally
            {
                sc.Close();
            }
        }

        private void cmbClass_KeyDown(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void cmbClass_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
    }
}
