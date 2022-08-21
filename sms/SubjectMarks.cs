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
    public partial class SubjectMarks : Form
    {
        public SubjectMarks()
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
            bindSubCategory(); bindSubCategory1(); bindSubCategory2();
            btn_create.Enabled = true;
            //cmbClass.Text = "Select";
            cmbExam.Text = "Select";
            //LoadGrid();
            dataGridView1.DataSource = DBNull.Value;

        }


        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CLASS_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["CLS_DESC"] = "Select";
            //row["CLS_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "CLS_DESC";
            cmbClass.ValueMember = "CLS_ID";
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


        }
        private void LoadGrid12()
        {
            string id = "%";
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Category,SubCategory,SUBSC_ID,SUBSC_CODE FROM View_SubCategory where SUBSC_CAT_ID LIKE '" + id + "' order by SUBSC_CAT_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["CATEGORY"].Visible = true;
            dataGridView1.Columns["CATEGORY"].HeaderText = "Category";
            dataGridView1.Columns["CATEGORY"].Width = 200;
            dataGridView1.Columns["SubCategory"].Width = 200;
            //dataGridView1.Columns["DESCRIPTION"].HeaderText = "Sub Category Name";
            dataGridView1.Columns["SUBSC_ID"].Visible = false;
            //dataGridView1.Columns["SUBSC_CAT_ID"].Visible = false;



        }
        private void LoadGrid()
        {
            string id = "0";
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("BIND_SUBJECT_MARKS ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
            da.SelectCommand.Parameters.AddWithValue("@CATEGORY", id);
            da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(cmbExam.SelectedValue));
            sc.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["appid"].Visible = false;
            dataGridView1.Columns["SUBJECT"].Width = 300;
            dataGridView1.Columns["Max_Marks"].Width = 210;
            dataGridView1.Columns["Min_Marks"].Width = 210;
        }

        private void bindSubCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 and subsc_id in (48,78,79) order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSubjectCategory.DataSource = ds.Tables[0];
            cmbSubjectCategory.DisplayMember = "SUBSC_DESC";
            cmbSubjectCategory.ValueMember = "SUBSC_ID";
        }
        private void bindSubCategory1()
        {
            int id = 0;
            if (cmbSubjectCategory.SelectedValue != null)
                id = Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB1.DataSource = ds.Tables[0];
            CMBSUB1.DisplayMember = "SUBSC_DESC";
            CMBSUB1.ValueMember = "SUBSC_ID";
        }
        private void bindSubCategory2()
        {
            int id = 0;
            if (CMBSUB1.SelectedValue != null)
                id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB2.DataSource = ds.Tables[0];
            CMBSUB2.DisplayMember = "SUBSC_DESC";
            CMBSUB2.ValueMember = "SUBSC_ID";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            string id = "0";
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());
            if (dataGridView1.Rows.Count > 0)
            {
                SqlTransaction tran;
                DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbClass.Text == "Select")
                {
                    MsgBox.ShowBox("class should not be blank");
                    return;
                }
                else if (cmbExam.Text == "Select")
                {
                    MsgBox.ShowBox("Exam should not be blank");
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

                        for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_SUBJECT_MARKS_T";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@SBM_CLS_ID", cmbClass.SelectedValue);
                            cmd1.Parameters.AddWithValue("@SBM_EXAM_ID", cmbExam.SelectedValue);
                            cmd1.Parameters.AddWithValue("@SBM_SUB_ID", dataGridView1.Rows[z].Cells["appid"].Value);
                            string str22 = dataGridView1.Rows[z].Cells["Max_Marks"].Value.ToString();
                            if (dataGridView1.Rows[z].Cells["Max_Marks"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", 0);
                            else
                                cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", dataGridView1.Rows[z].Cells["Max_Marks"].Value);
                            if (dataGridView1.Rows[z].Cells["Min_Marks"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@SBM_MIN_MARK", 0);
                            else
                                cmd1.Parameters.AddWithValue("@SBM_MIN_MARK", dataGridView1.Rows[z].Cells["Min_Marks"].Value);
                            cmd1.Parameters.AddWithValue("@CATEGORY", id);
                            cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();

                        }

                        tran.Commit();
                        //SqlCommand cmd2 = new SqlCommand();
                        //cmd2.CommandType = CommandType.StoredProcedure;
                        //cmd2.Connection = sc;
                        //cmd2.CommandText = "UPDATE_SUBJECT_MARKS_T";
                        //cmd2.Parameters.AddWithValue("@CLASS", cmbClass.SelectedValue);
                        //cmd2.Parameters.AddWithValue("@CATEGORY", id);
                        //cmd2.Parameters.AddWithValue("@EXAM", cmbExam.SelectedValue);
                        //cmd2.Transaction = tran;
                        //cmd2.ExecuteNonQuery();
                        //MsgBox.ShowBox("Subject Marks Created...");
                        LoadGrid();


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
            else
            {
                MsgBox.ShowBox("No Record To Save........");
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
                SqlCommand com = new SqlCommand("update_RESULT_T_GradingScale_CoSolatic '" + txtGradeid.Text + "','2','" + cmbExam.Text + "'", sc);

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
            bindClass();
            bindExam(); bindSubCategory();

        }

        private void cmbExam_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSubjectCategory_Leave(object sender, EventArgs e)
        {
            bindSubCategory1();
            LoadGrid();
        }

        private void CMBSUB1_Leave(object sender, EventArgs e)
        {
            bindSubCategory2();
            LoadGrid();
        }

        private void CMBSUB2_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
