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
    public partial class frmDesignationMaster : Form
    {
        BLclass bl = new BLclass();
        public frmDesignationMaster()
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
            Binddept();
            Cancil_form();
            
            
        }
        private void OnlyCapital(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }
        private void Cancil_form()
        {
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            cmbDept.Text = "Select";
            txtDesignation.Text = "";
            InitGrid();
        }
        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "DEPT_DESC";
            cmbDept.ValueMember = "DEPT_ID";
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["DESG_ID"].Visible = false; ;
            dataGridView1.Columns["DESG_DESC"].Width = 150;
            dataGridView1.Columns["DESG_DESC"].HeaderText="Designation";
            dataGridView1.Columns["DEPT_DESC"].Width = 200;
            dataGridView1.Columns["DEPT_DESC"].HeaderText="Departmrnt";
            //dataGridView1.Columns["userid"].Width = 90;
            //dataGridView1.Columns["date"].Width = 90;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string command;
            if (cmbDept.SelectedText == "Select")
                command = "select * from dbo.View_tbldesignation ORDER BY Desg_ID";
            else
                command = "select * from dbo.View_tbldesignation ORDER BY DESG_DESC";
            SqlDataAdapter da = new SqlDataAdapter(command, sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Designation!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtcityid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM DESIGNATION_T WHERE DESG_ID=" + txtcityid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Designation Deleted Successfully!!!");
                        Cancil_form();
                        
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtDesignation.Text == "")
            {
                MsgBox.ShowBox("Department Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_DESIGNATION_T '" + txtDesignation.Text + "','" + Convert.ToInt32(cmbDept.SelectedValue) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Designation Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Designation Name already exist");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }


      

        private void cmbdept_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtdesignation_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmDesignationMaster_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtcityid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtDesignation.Text == "")
            {
                MsgBox.ShowBox("Designation Name Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_DESIGNATION_T '" + Convert.ToInt32(txtcityid.Text) + "','" + txtDesignation.Text + "','" + Convert.ToInt32(cmbDept.SelectedValue) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Designation Updated...");
                        Cancil_form();
                     }
                    else
                    {
                        MsgBox.ShowBox("Updating Error !!!");
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
            btnCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtDesignation.Text = dataGridView1.CurrentRow.Cells["DESG_DESC"].Value.ToString();
                cmbDept.Text = dataGridView1.CurrentRow.Cells["DEPT_DESC"].Value.ToString();
                txtcityid.Text = dataGridView1.CurrentRow.Cells["DESG_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        

       
       
        

       
       




    }
}
