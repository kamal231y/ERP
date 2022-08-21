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
    public partial class frmGrading1 : Form
    {
        public frmGrading1()
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
            cmbResult.Text = "Select";
            txtGrade.Text = "";
            txtMaxMarks.Text = "";
            txtMinMarks.Text = "";
            txtRemarks.Text = "";
            txtGradePoint.Text = "";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
           

        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["RES_ID"].Visible = false;
            dataGridView1.Columns["RES_CAT_ID"].Visible = false;
            dataGridView1.Columns["RES_MAX_PER"].HeaderText = "Max Marks";  
            dataGridView1.Columns["RES_MIN_PER"].HeaderText = "Min Marks";
            dataGridView1.Columns["RES_GRADE"].HeaderText = "Grade";
            dataGridView1.Columns["RES_GRADE_POINT"].HeaderText = "Grade Point";
            dataGridView1.Columns["RES_DESC"].HeaderText = "Result";
            dataGridView1.Columns["RES_REMARK"].Width = 150;
            dataGridView1.Columns["RES_REMARK"].HeaderText = "Remarks";  
        }
        private void LoadGrid()
        { string res;
        if (cmbResult.Text == "" || cmbResult.Text == "Select")
            res = "%";
        else
            res = cmbResult.Text;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select RES_ID,RES_CAT_ID,RES_MAX_PER,RES_MIN_PER,RES_GRADE,RES_GRADE_POINT,RES_DESC,RES_REMARK from RESULT_T where  RES_CAT_ID=1 and RES_GRADE like '" + txtGrade.Text + "%' and RES_MAX_PER  like '" + txtMaxMarks.Text + "%' and RES_MIN_PER like '" + txtMinMarks.Text + "%'and RES_DESC like '" + res + "%' and RES_GRADE_POINT like '" + txtGradePoint.Text + "%'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (txtGrade.Text == "")
                {
                    MsgBox.ShowBox("Grade should not be blank");
                    return;
                }
                //SqlCommand com = new SqlCommand("Insert_tblSectionMaster '" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "','" + SchoolManagementSystem.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
                SqlCommand com = new SqlCommand("Insert_RESULT_T_ScholasticArea '1','" + cmbResult.Text + "'," + Convert.ToDecimal(txtMaxMarks.Text) + "," + Convert.ToDecimal(txtMinMarks.Text) + ",'" + txtGrade.Text + "','" + Convert.ToInt32(txtGradePoint.Text) + "','" + txtRemarks.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Grade Scholastic Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Grade Scholastic Name already exist");
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
            catch( Exception EX){}
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
                //SqlCommand com = new SqlCommand("Update_tblSectionMaster '" + txtSectionid.Text + "','" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "'", sc);
                SqlCommand com = new SqlCommand("Update_RESULT_T_ScholasticArea '" + txtGradeid.Text + "', '1','" + cmbResult.Text + "'," + Convert.ToDecimal(txtMaxMarks.Text) + "," + Convert.ToDecimal(txtMinMarks.Text) + ",'" + txtGrade.Text + "','" + Convert.ToInt32(txtGradePoint.Text) + "','" + txtRemarks.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Grade Scholastic Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Grade Scholastic already exist");
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM RESULT_T WHERE RES_ID=" + txtGradeid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Grade Scholastic Deleted Successfully!!!");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
               
                
                cmbResult.Text = dataGridView1.CurrentRow.Cells["RES_DESC"].Value.ToString();
                txtGradeid.Text = dataGridView1.CurrentRow.Cells["RES_ID"].Value.ToString();
                txtMaxMarks.Text = dataGridView1.CurrentRow.Cells["RES_MAX_PER"].Value.ToString();
                txtMinMarks.Text = dataGridView1.CurrentRow.Cells["RES_MIN_PER"].Value.ToString();
                txtGrade.Text = dataGridView1.CurrentRow.Cells["RES_GRADE"].Value.ToString();
                txtGradePoint.Text = dataGridView1.CurrentRow.Cells["RES_GRADE_POINT"].Value.ToString();
                txtRemarks.Text = dataGridView1.CurrentRow.Cells["RES_REMARK"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void frmGrading1_Load(object sender, EventArgs e)
        {
            LoadGrid();

        }

        private void txtMaxMarks_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtGrade_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtGradePoint_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtMinMarks_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        

       
       

       
       
      
      
       

      

      

       
        

        
    }
}
