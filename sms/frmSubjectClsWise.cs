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
    public partial class frmSubjectClsWise : Form
    {
        BLclass bl = new BLclass();
        validation v1 = new validation();

        string evolution;
        public frmSubjectClsWise()
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
           
            cmbSubjectCategory.Text = "-Select-";

            btnUpdate.Enabled = false;
            btn_delete.Enabled = true;
            bindSubCategory();
            bindSubCategory1();
            BindClass(); LoadGrid();


        }
        private void LoadGrid()
        {
            string cls, cat;
            if (CMBCLS.Text == "" || CMBCLS.Text == "-Select-" || CMBCLS.Text == "System.Data.DataRowView")
            {
                cls = "%";
            }
            else
            {
                cls = CMBCLS.SelectedValue.ToString();
            }
            if (cmbSubjectCategory.Text == "" || cmbSubjectCategory.Text == "-Select-" || cmbSubjectCategory.Text == "System.Data.DataRowView" )
            {
                cat = "%";
            }
            else
            {
                cat = cmbSubjectCategory.SelectedValue.ToString();
            }
 
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM VIEW_SUBJECT_CLASS WHERE CLS_ID LIKE '" + cls + "' and CLS_SUB_CAT_ID LIKE '" + cat + "' order by CLS_SEQUENCE", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["CLS_ID"].Visible = false;
            dataGridView1.Columns["CLS_SUB_CAT_ID"].Visible = false;
            dataGridView1.Columns["CLS_SUB_ID"].Visible = false;
            dataGridView1.Columns["CLS_SEQUENCE"].Visible = false;
            dataGridView1.Columns["CLS_DESC"].Width = 100;
            dataGridView1.Columns["CATEGORY"].Width = 200;
            dataGridView1.Columns["SUBJECT"].Width = 200;
            
            


           
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
    
        private void btn_create_Click(object sender, EventArgs e)
        {
          
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (CMBSUB1.Text == "" || CMBSUB1.Text == "-Select-")
            {
                MsgBox.ShowBox("Subject Name should not be blank");
                return;
            }
            SqlCommand com = new SqlCommand("insert_SUBJECT_CLASS_T '" + CMBCLS.SelectedValue + "','" + cmbSubjectCategory.SelectedValue + "','" + CMBSUB1.SelectedValue + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Subject Created...");
                   // Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Subject Name already exist");
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
            
            
            
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();
            //if (CMBSUB1.Text == "")
            //{
            //    MsgBox.ShowBox("Please Click to Detail Row!!!");
            //    return;
            //}

            //else
            //{
            //    //SqlCommand com = new SqlCommand("Update_tblSectionMaster '" + txtSectionid.Text + "','" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "'", sc);
            //    SqlCommand com = new SqlCommand("Update_SUBJECT_Sub_CATEGORY_T '" + txtSubCategoryid.Text + "','" + 0 + "','" + 0 + "','" + 0 + "',"+0+"", sc);
            //    SqlDataReader dr;
            //    sc.Open();
            //    try
            //    {
            //        dr = com.ExecuteReader();
            //        if (dr.RecordsAffected > 0)
            //        {
            //            MsgBox.ShowBox("Sub Category Updated...");
            //            Cancil_form();
            //            LoadGrid();
            //        }
            //        else
            //        {
            //            MsgBox.ShowBox("This Sub Category already exist");
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    finally
            //    {
            //        sc.Close();
            //    }
            //}
        }
        private void BindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CLASS_T order by CLS_SEQUENCE ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CLS_DESC"] = "-Select-";
            row["CLS_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            CMBCLS.DataSource = dt;
            CMBCLS.DisplayMember = "CLS_DESC";
            CMBCLS.ValueMember = "CLS_ID";
            sc.Close();
        }

        private void frmSubjectSubCategory_Load(object sender, EventArgs e)
        {
            bindSubCategory(); BindClass(); LoadGrid();
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

            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Subject!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        dataclass dc = new dataclass();
                        SqlConnection conn = dc.con();
                        if (txtsub.Text == "")
                        {
                            MsgBox.ShowBox("Please Click to Detail Row!!!");
                            return;
                        }
                        SqlCommand objCmd = new SqlCommand("DELETE FROM SUBJECT_CLASS_T WHERE CLS_ID='" + txtcls.Text + "' and CLS_SUB_CAT_ID='"+ txtSubCategoryid.Text +"' AND CLS_SUB_ID='"+ txtsub.Text +"'", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox("Subject Deleted Successfully!!!");
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
             LoadGrid();
        }

        private void CMBSUB2_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                btn_create.Enabled = false;
                if (dataGridView1.Rows.Count > 0)
                {
                    txtcls.Text = dataGridView1.CurrentRow.Cells["CLS_ID"].Value.ToString();
                    txtSubCategoryid.Text = dataGridView1.CurrentRow.Cells["CLS_SUB_CAT_ID"].Value.ToString();
                    txtsub.Text = dataGridView1.CurrentRow.Cells["CLS_SUB_ID"].Value.ToString();
                    CMBCLS.Text = dataGridView1.CurrentRow.Cells["CLS_DESC"].Value.ToString();
                    cmbSubjectCategory.Text = dataGridView1.CurrentRow.Cells["CATEGORY"].Value.ToString();
                    CMBSUB1.Text = dataGridView1.CurrentRow.Cells["SUBJECT"].Value.ToString();

                    //txtSubCategoryName.Text = dataGridView1.CurrentRow.Cells["SUBC_ID"].Value.ToString();


                    btnUpdate.Enabled = true;
                    btn_delete.Enabled = true;
                }
            }
            catch (Exception ex)
            { }
        }

        

        
      


        }
    }

