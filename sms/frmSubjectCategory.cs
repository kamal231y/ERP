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
    public partial class frmSubjectCategory : Form
    {
        string evolution;
        public frmSubjectCategory()
        {
            InitializeComponent();
            InitGrid();
            Cancil_form();
        }
        public void InitGrid()
        {
            LoadGrid();

        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            txtCategoryName.Text = "";
            cmbSubCategory.Text = "Select";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;


        }
        private void LoadGrid()
        {
            string cat;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (cmbSubCategory.Text == "" || cmbSubCategory.Text == "Select")
                cat = "%";
            else
                cat = cmbSubCategory.Text;
            SqlDataAdapter da = new SqlDataAdapter("  SELECT SUBSC_ID,SUBSC_DESC,SUBSC_EVO_ID FROM SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 AND  SUBSC_DESC LIKE '" + txtCategoryName.Text + "%' ORDER BY SUBSC_DESC  ", sc);
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["SUBSC_EVO_ID"].Width = 250;
            dataGridView1.Columns["SUBSC_EVO_ID"].HeaderText="Category Type";
            dataGridView1.Columns["SUBSC_DESC"].Width = 355;
            dataGridView1.Columns["SUBSC_DESC"].HeaderText = "Category Name";
            dataGridView1.Columns["SUBSC_ID"].Visible = false;
           
        }

        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_TYPE_T order by SBT_ID", sc);
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SBT_DESC"] = "Select";
            row["SBT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSubCategory.DataSource = ds.Tables[0];
            cmbSubCategory.DisplayMember = "SBT_DESC";
            cmbSubCategory.ValueMember = "SBT_ID";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {

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
            if (txtCategoryName.Text == "")
            {
                MsgBox.ShowBox("Category Name should not be blank");
                return;
            }
            //SqlCommand com = new SqlCommand("Insert_tblSectionMaster '" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "','" + SchoolManagementSystem.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
            SqlCommand com = new SqlCommand("Insert_SUBJECT_CATEGORY '" + txtCategoryName.Text + "','" + cmbSubCategory.SelectedValue + "','" + evolution + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Subject Category Created...");
                    // Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Subject Category already exist");
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSubjectCategory_Load(object sender, EventArgs e)
        {
            bindCategory();
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
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
                SqlCommand com = new SqlCommand("Update_SUBJECT_CATEGORY '" + txtSubCategoryid.Text + "','" + txtCategoryName.Text + "','" + cmbSubCategory.SelectedValue + "','" + evolution + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Subject Category Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Subject Category already exist");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {


                txtCategoryName.Text = dataGridView1.CurrentRow.Cells["SUBSC_DESC"].Value.ToString();
                txtSubCategoryid.Text = dataGridView1.CurrentRow.Cells["SUBSC_ID"].Value.ToString();
                //cmbSubCategory.Text = dataGridView1.CurrentRow.Cells["TYPE"].Value.ToString();
                

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
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
                        MsgBox.ShowBox("Subject Category Deleted Successfully!!!");
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            LoadGrid(); 
        }

      
 
       

       

       
    }
}
