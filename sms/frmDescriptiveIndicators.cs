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
    public partial class frmDescriptiveIndicators : Form
    {
        public frmDescriptiveIndicators()
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
            InitGrid();
            Cancil_form();
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            //cmbSCat.Text = "-Select-";
            cmbSSubCat.Text = "-Select-";
            cmbmarks.Text = "";
            txtGrade.Text = "";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            LoadGrid();

        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["ID"].Width = 100;
            dataGridView1.Columns["CATEGORY"].Width = 150;
            dataGridView1.Columns["SUB_CATEGORY"].Width = 150;
            dataGridView1.Columns["Mark"].Width = 100;
            dataGridView1.Columns["DESCRIPTION"].Width =500;
            
        }
        private void LoadGrid()
        {
            string sub, subsub, mark;
            if (cmbSCat.Text == "" || cmbSCat.Text == "-Select-")
                sub = "%";
            else
                sub = cmbSCat.Text;
            if (cmbSSubCat.Text == "" || cmbSSubCat.Text == "-Select-")
                subsub = "%";
            else
                subsub = cmbSSubCat.Text;
            if (cmbmarks.Text == "")
                mark = "%";
            else
                mark = cmbmarks.Text;

          
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from DESCRIPTIVE_INDICATOR_T ", sc);
            SqlDataAdapter da = new SqlDataAdapter(" select a.D_ID as ID, c.SUBSC_DESC as CATEGORY,b.SUBSC_DESC as SUB_CATEGORY,a.D_DESC as DESCRIPTION,a.D_Mark as Mark  from dbo.DESCRIPTIVE_INDICATOR_T a ,dbo.SUBJECT_SUB_CATEGORY_T b,dbo.SUBJECT_SUB_CATEGORY_T c where  a.D_SUB_CAT_ID=b.SUBSC_ID and a.D_CAT_ID=c.SUBSC_ID and c.SUBsC_DESC like '" + sub + "' and b.SUBSC_DESC like '" + subsub + "'and  a.D_DESC like '" + txtGrade.Text + "%'  and a.D_Mark like '" + mark + "'", sc);           
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void BindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 AND SUBSC_ID NOT IN (48,78,79) ORDER BY SUBSC_DESC", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbSCat.DataSource = dt;
            cmbSCat.DisplayMember = "SUBSC_DESC";
            cmbSCat.ValueMember = "SUBSC_ID";
            sc.Close();
        }
        private void Bindgradepoint()
        {
            if (cmbSSubCat.SelectedValue != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                sc.Open();
                SqlCommand cmd = new SqlCommand("select * from tblgradepoint where Gradepoint not in (select D_Mark from DESCRIPTIVE_INDICATOR_T where D_SUB_CAT_ID=" + cmbSSubCat.SelectedValue + ") ", sc);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //DataRow row = dt.NewRow();
                //row["Gradepoint"] = "-Select-";
                //row["ID"] = 0;
                //dt.Rows.InsertAt(row, 0);

                cmbmarks.DataSource = dt;
                cmbmarks.DisplayMember = "Gradepoint";
                cmbmarks.ValueMember = "ID";
                sc.Close();
            }

        }


        //private void BindSubCategory()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    sc.Open();
        //    SqlCommand cmd = new SqlCommand("select * from SUBJECT_SUB_CATEGORY_T", sc);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);

        //    DataRow row = dt.NewRow();
        //    row["SUBSC_DESC"] = "-Select-";
        //    row["SUBSC_ID"] = 0;
        //    dt.Rows.InsertAt(row, 0);

        //    cmbSSubCat.DataSource = dt;
        //    cmbSSubCat.DisplayMember = "SUBSC_DESC";
        //    cmbSSubCat.ValueMember = "SUBSC_ID";
        //    sc.Close();
        //}
        private void btn_create_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbSCat.Text == "" || cmbSCat.Text == "-Select-")
            {
                MsgBox.ShowBox("Pls Select Category");
                return;

            }
            if (cmbSSubCat.Text == "" || cmbSSubCat.Text == "-Select-")
            {
                MsgBox.ShowBox("Pls Select SubCategory");
                return;

            }

            if (txtGrade.Text == "")
            {
                MsgBox.ShowBox("Grade should not be blank");
                return;

            }

            SqlCommand com = new SqlCommand("Insert_DESCRIPTIVE_INDICATOR_T '" + Convert.ToInt32(cmbSCat.SelectedValue.ToString()) + "','" + Convert.ToInt32(cmbSSubCat.SelectedValue.ToString()) + "','" + txtGrade.Text + "', " +cmbmarks.SelectedValue + "", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Descriptive Indicator Created...");
                    Cancil_form();
                    LoadGrid();
                    return;
                }
                else
                {
                    MsgBox.ShowBox("This Descriptive Indicator already exist");
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

        private void DescriptiveIndicators1_Load(object sender, EventArgs e)
        {
            //  BindSubCategory();
          
            BindCategory();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtGradeid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtGrade.Text == "")
            {
                MsgBox.ShowBox("Grade Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_DESCRIPTIVE_INDICATOR_T " + txtGradeid.Text + "," + cmbSCat.SelectedValue+"," + cmbSSubCat.SelectedValue + ",'" + txtGrade.Text + "',"+cmbmarks.Text+"", sc);
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                //cmbSCat.Text = dataGridView1.CurrentRow.Cells["D_CAT_ID"].Value.ToString();
                //cmbSSubCat.Text = dataGridView1.CurrentRow.Cells["D_SUB_CAT_ID"].Value.ToString();
                //txtGradeid.Text = dataGridView1.CurrentRow.Cells["D_ID"].Value.ToString();
                //txtGrade.Text = dataGridView1.CurrentRow.Cells["D_DESC"].Value.ToString();

                cmbSCat.Text = dataGridView1.CurrentRow.Cells["CATEGORY"].Value.ToString();
                cmbSSubCat.Text = dataGridView1.CurrentRow.Cells["SUB_CATEGORY"].Value.ToString();
                txtGradeid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                txtGrade.Text = dataGridView1.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                cmbmarks.Text = dataGridView1.CurrentRow.Cells["Mark"].Value.ToString();
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
                    if (txtGradeid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM DESCRIPTIVE_INDICATOR_T WHERE D_ID=" + txtGradeid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Grade Deleted Successfully!!!");
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void cmbSCat_Leave(object sender, EventArgs e)
        {
            BindSubCategory();
            LoadGrid();
        }

        private void BindSubCategory()
        {
            int id = 0;
            if (cmbSCat.SelectedValue != null)
                id = Convert.ToInt32(cmbSCat.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSSubCat.DataSource = ds.Tables[0];
            cmbSSubCat.DisplayMember = "SUBSC_DESC";
            cmbSSubCat.ValueMember = "SUBSC_ID";
            
        }

        private void cmbSSubCat_Leave(object sender, EventArgs e)
        {
            Bindgradepoint();
            LoadGrid();
        }

          private void txtGrade_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

          private void cmbSCat_Leave_1(object sender, EventArgs e)
          {
              BindSubCategory();
          }

        

        
    }
}
