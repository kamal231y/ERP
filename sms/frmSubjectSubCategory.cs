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
    public partial class frmSubjectSubCategory : Form
    {
        BLclass bl = new BLclass();
        validation v1 = new validation();

        string evolution;
        public frmSubjectSubCategory()
        {
            InitializeComponent();
            InitGrid();
            Cancil_form();
        }
        public void InitGrid()
        {
            LoadGrid();

        }
        private void OnlyCapital(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            txtSubCategoryid.Text = "";
            txtSubCategoryName.Text = "";
            cmbSubjectCategory.Text = "-Select-";

            btnUpdate.Enabled = false;
            btn_delete.Enabled = true;
            bindSubCategory();
            bindSubCategory1();
            bindSubCategory2();


        }
        private void LoadGrid()
        {
            string id = "%"; 
            if(cmbSubjectCategory.SelectedValue!=null)
                 if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id =(CMBSUB1.SelectedValue.ToString());
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
            dataGridView1.Columns["SUBSC_CODE"].Visible = false;


           
        }
        private void bindSubCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 order by SUBSC_ID", sc);
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
                id =Convert.ToInt32( cmbSubjectCategory.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_ID", sc);
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
        {  int id=0;
        if (CMBSUB1.SelectedValue != null)
            id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
        dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_ID", sc);
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
            int id = 0; int FLAG = 2;
            if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
            {
                id = Convert.ToInt32(CMBSUB2.SelectedValue.ToString());
                FLAG = 4;
            }
            else if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
            {
                id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
                FLAG = 3;
            }
            else if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
            {
                id = Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString());
                FLAG = 2;
            }

            if (rbtMarks.Checked == true)
            {
                evolution = "1";
            }
            else
            {
                evolution = "0";
            }
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtSubCategoryName.Text == "")
            {
                MsgBox.ShowBox("Sub Category Name should not be blank");
                return;
            }
            //SqlCommand com = new SqlCommand("Insert_tblSectionMaster '" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "','" + SchoolManagementSystem.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
            SqlCommand com = new SqlCommand("insert_SUBJECT_SUB_CATEGORY_T '" + txtSubCategoryName.Text + "','" + id + "','" + evolution + "',"+FLAG+"", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Sub Category Created...");
                   // Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Sub Category Name already exist");
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = 0; int FLAG = 2;
            if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
            {
                id = Convert.ToInt32(CMBSUB2.SelectedValue.ToString());
                FLAG = 4;
            }
            else if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
            {
                id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
                FLAG = 3;
            }
            else if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
            {
                id = Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString());
                FLAG = 2;
            }

            if (rbtMarks.Checked == true)
            {
                evolution = "1";
            }
            else
            {
                evolution = "0";
            }
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtSubCategoryid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }

            else
            {
                //SqlCommand com = new SqlCommand("Update_tblSectionMaster '" + txtSectionid.Text + "','" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "'", sc);
                SqlCommand com = new SqlCommand("dbo.Update_SUBJECT_Sub_CATEGORY_T '" + txtSubCategoryid.Text + "','" + txtSubCategoryName.Text + "','" + id + "','" + evolution + "',"+FLAG+"", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Sub Category Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Sub Category already exist");
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

        private void frmSubjectSubCategory_Load(object sender, EventArgs e)
        {
            bindSubCategory();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
          //  dataclass dd = new dataclass();
          //  SqlConnection sc = dd.con();
          //  SqlTransaction tran;
          //sc.Open();
          //              tran = sc.BeginTransaction();
          //              SqlCommand cmd1 = new SqlCommand();
          //              //SqlCommand cmd1 = new SqlCommand("insert_SUBJECT_MARKS_T '" + cmbClass.SelectedValue + "','" + cmbExam.SelectedValue + "','" + Stuid + "','" + txt_month.Text + "','" + txt_nettotal.Text + "','" + txt_paid.Text + "','" + txt_prevdue.Text + "','" + txt_concession.Text + "','" + cmbmop.SelectedValue + "','" + txtDDNo.Text + "','" + cmbBankName.SelectedValue + "','" + Convert.ToDateTime(dt3) + "','" + cmb_accounts.SelectedValue + "','" + txt_coments.Text + "','" + txt_balance.Text + "','" + txt_totalfee.Text + "','" + Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + Convert.ToDateTime(dt1) + "'", sc);
          //              //SqlCommand cmd1 = new SqlCommand("select * from View_SUBJECT_MARKS_T  where SBM_EXAM_ID='" + cmbExam.SelectedValue + "' and SBM_CLS_ID='" + cmbClass.SelectedValue + "'", sc);
          //              SqlDataReader dr;
          //              cmd1.Transaction = tran;
          //              try
          //              {

          //                  //if (cmd1.ExecuteNonQuery() > 0)
          //                  //{

          //                  for (int z = 0; z < dataGridView1.Rows.Count; z++)
          //                  {
          //                      cmd1 = new SqlCommand();
          //                      cmd1.CommandType = CommandType.StoredProcedure;
          //                      cmd1.CommandText = "UPDATE_SUBJECT_CODE";
          //                      cmd1.Connection = sc;
          //                      cmd1.Parameters.AddWithValue("@SBM_SUB_ID", dataGridView1.Rows[z].Cells["SUBSC_ID"].Value);
          //                      cmd1.Parameters.AddWithValue("@SBM_MIN_MARK", dataGridView1.Rows[z].Cells["SUBSC_CODE"].Value);
          //                      cmd1.Transaction = tran;
          //                      cmd1.ExecuteNonQuery();

          //                  }

          //                  tran.Commit();
          //                  MsgBox.ShowBox("Subject Marks Created...");
          //                  LoadGrid();

          //              }
          //              catch (Exception ex)
          //              {
          //                  tran.Rollback();
          //                  //MsgBox.ShowBox("Subject Marks  Created...");
          //              }
          //              finally
          //              {
          //                  sc.Close();
          //              }
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        dataclass dc = new dataclass();
                        SqlConnection conn = dc.con();
                        if (txtSubCategoryid.Text == "")
                        {
                            MsgBox.ShowBox("Please Click to Detail Row!!!");
                            return;
                        }
                        SqlCommand objCmd = new SqlCommand("DELETE FROM SUBJECT_SUB_CATEGORY_T WHERE SUBSC_ID=" + txtSubCategoryid.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox("Subject Sub Category Deleted Successfully!!!");
                            Cancil_form();
                            InitGrid();
                        }
                        else
                        {
                            MsgBox.ShowBox(" Deleting Error!!!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }
        

        private void cmbSubjectCategory_Leave(object sender, EventArgs e)
        {
            bindSubCategory1();
            LoadGrid();
        }

        private void CMBSUB1_Leave(object sender, EventArgs e)
        {
            bindSubCategory2(); LoadGrid();
        }

        private void CMBSUB2_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            {
                btn_create.Enabled = false;
                if (dataGridView1.Rows.Count > 0)
                {


                    txtSubCategoryName.Text = dataGridView1.CurrentRow.Cells["SubCategory"].Value.ToString();
                    txtSubCategoryid.Text = dataGridView1.CurrentRow.Cells["SUBSC_ID"].Value.ToString();
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("BIND_SUBCATEGORY", sc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@SUBID", Convert.ToInt32(txtSubCategoryid.Text));
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cmbSubjectCategory.SelectedValue = dt.Rows[0]["level1"].ToString();
                        bindSubCategory1();
                        CMBSUB1.SelectedValue = dt.Rows[0]["level2"].ToString();
                        bindSubCategory2();
                        CMBSUB2.SelectedValue = dt.Rows[0]["level3"].ToString();

                    }

                    //txtSubCategoryName.Text = dataGridView1.CurrentRow.Cells["SUBC_ID"].Value.ToString();


                    btnUpdate.Enabled = true;
                    btn_delete.Enabled = true;
                }
            }
        }

        

        
      


        }
    }

