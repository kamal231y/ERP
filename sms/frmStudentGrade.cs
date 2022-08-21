using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;

namespace sms
{
    public partial class frmStudentGrade : Form
    {
        public frmStudentGrade()
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
            BindCategory(); bindclass();
        }

        private void btn_create_Click(object sender, EventArgs e)
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
            if (cmbSSubCat.Text == "-Select-")
            {
                MsgBox.ShowBox("Subcategory should not be blank");
                return;
            }
            if (cmbSCat.Text == "-Select-")
            {
                MsgBox.ShowBox("category should not be blank");
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
                        cmd1.CommandText = "insert_Student_SUBJECT_Grade";
                        cmd1.Connection = sc;
                        //cmd1.Parameters.AddWithValue("@receiptno", Convert.ToInt32(txt_receiptno.Text));
                        cmd1.Parameters.AddWithValue("@stu_id", dataGridView1.Rows[z].Cells["sid"].Value);
                        cmd1.Parameters.AddWithValue("@SBM_cat", cmbSCat.SelectedValue);
                        cmd1.Parameters.AddWithValue("@SBM_sub", cmbSSubCat.SelectedValue);
                        if(dataGridView1.Rows[z].Cells["Marks"].Value.ToString()=="")
                        cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", 0);
                        else
                       cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", dataGridView1.Rows[z].Cells["Marks"].Value);
                        cmd1.Transaction = tran;
                        cmd1.ExecuteNonQuery();
                    }
                    tran.Commit();
                    MsgBox.ShowBox("Student Subject Grade Created...");
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


        private void BindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd = new SqlCommand("select * from dbo.SUBJECT_CATEGORY_T", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["SUBC_DESC"] = "-Select-";
            row["SUBC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbSCat.DataSource = dt;
            cmbSCat.DisplayMember = "SUBC_DESC";
            cmbSCat.ValueMember = "SUBC_ID";
            sc.Close();
        }
        private void BindSubCategory()
        {
            if (cmbSCat.SelectedValue != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                sc.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.SUBJECT_CATEGORY_T cat,SUBJECT_SUB_CATEGORY_T sub where cat.SUBC_ID=sub.SUBSC_CAT_ID and sub.SUBSC_CAT_ID=" + cmbSCat.SelectedValue + "  order by sub.SUBSC_ID", sc);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                da.Fill(dt);

                DataRow row = dt.NewRow();
                row["SUBSC_DESC"] = "-Select-";
                row["SUBSC_ID"] = 0;
                dt.Rows.InsertAt(row, 0);

                cmbSSubCat.DataSource = dt;
                cmbSSubCat.DisplayMember = "SUBSC_DESC";
                cmbSSubCat.ValueMember = "SUBSC_ID";
                sc.Close();
            }
        }
        private void cmbSCat_Leave(object sender, EventArgs e)
        {
            BindSubCategory();
           
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
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
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
        private void frmStudentGrade_Load(object sender, EventArgs e)
        {
           
            bindExam();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (cmbSCat.Text == "" || cmbSCat.Text == "-Select-" || cmbSCat.Text.Trim() == "System.Data.DataRowView")
                str2 = "0";
            else
                str2 = cmbSCat.SelectedValue.ToString();
            if (cmbSSubCat.Text == "" || cmbSSubCat.Text == "-Select-" || cmbSSubCat.Text.Trim() == "System.Data.DataRowView")
                str3 = "0";
            else
                str3 = cmbSSubCat.SelectedValue.ToString();
            SqlDataAdapter da = new SqlDataAdapter("Find_StudentGrade", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@class", str1);
            da.SelectCommand.Parameters.AddWithValue("@cat", str2);
            da.SelectCommand.Parameters.AddWithValue("@subcat", str3);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbExam.SelectedValue = 0;
            cmbSCat.SelectedValue = 0;
            cmbSSubCat.SelectedValue = 0;
            cmbClass.SelectedValue = 0;
        }

        private void cmbSSubCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbSCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

       

        
    }
}