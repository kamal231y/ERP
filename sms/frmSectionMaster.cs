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
    public partial class frmSectionMaster : Form
    {
        public frmSectionMaster()
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
           // Cancil_form();
            InitGrid();
            bindClass();
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            txtstrength.Text = "";
            cmbTeacher.Text = "-Select-";
            cmbClass.Text = "-Select-";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtSectionname.Text = "";
           
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["SEC_ID"].Visible = false;
            dataGridView1.Columns["SEC_DESC"].Width = 100;
            dataGridView1.Columns["SEC_DESC"].HeaderText = "Section";
            dataGridView1.Columns["CLS_DESC"].Width = 100;
            dataGridView1.Columns["CLS_DESC"].HeaderText = "Class";
            dataGridView1.Columns["SEC_STRENGTH"].Width = 100;
            dataGridView1.Columns["SEC_STRENGTH"].HeaderText = "Strength";
            dataGridView1.Columns["SEC_AVAILABLE"].Visible = false;
            dataGridView1.Columns["EMP_NAME"].Width = 140;
            dataGridView1.Columns["EMP_NAME"].HeaderText = "Teacher";
            
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_Section_T where CLS_DESC= '" + cmbClass.Text + "' ORDER BY SEC_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindTeacher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EMPLOYEE_T ORDER BY EMP_NAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbTeacher.DataSource = ds.Tables[0];
            cmbTeacher.DisplayMember = "EMP_NAME";
            cmbTeacher.ValueMember = "EMP_ID";
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Class_T WHERE CLS_DP!='N' ORDER BY CLS_SEQUENCE", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "CLS_DESC";
            cmbClass.ValueMember = "CLS_ID";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtSectionname.Text == "")
            {
                MsgBox.ShowBox("Batch Name should not be blank");
                return;
            }
            else if (txtstrength.Text == "")
            {
                MsgBox.ShowBox("Strength should not be blank");
                return;
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_Section_T " + cmbClass.SelectedValue + ", '" + txtSectionname.Text + "'," + txtstrength.Text + "," + cmbTeacher.SelectedValue + "," + txtstrength.Text + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Section Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Section Name already exist");
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
            InitGrid();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click_1(object sender, EventArgs e)
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
            if (txtSectionid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtSectionname.Text == "")
            {
                MsgBox.ShowBox("Section Should not be blank...");
            }
            else
            {
                
                        SqlCommand com = new SqlCommand("Update_Section_T '" + txtSectionid.Text + "','" + cmbClass.SelectedValue + "', '" + txtSectionname.Text + "','" + txtstrength.Text + "','" + cmbTeacher.SelectedValue + "'", sc);
                        SqlDataReader dr;
                        sc.Open();
                        try
                        {
                            dr = com.ExecuteReader();
                            if (dr.RecordsAffected > 0)
                            {
                                MsgBox.ShowBox("Section Updated...");
                                Cancil_form();
                                LoadGrid();
                            }
                            else
                            {
                                MsgBox.ShowBox("This Section Name already exist");
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
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtSectionid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("select * from Section_T where SEC_CLS_ID='" + cmbClass.SelectedValue + "'and SEC_DESC='" + txtSectionname.Text + "'", sc);
                        sc.Open();
                        SqlDataReader dr1 = cmd.ExecuteReader();

                        if (dr1.Read())
                        {
                            if (dr1["SEC_STRENGTH"].ToString() == dr1["SEC_AVAILABLE"].ToString())
                            {
                                dr1.Close();
                                sc.Close();
                                SqlCommand objCmd = new SqlCommand("DELETE_SECTION_T " + txtSectionid.Text + "", conn);
                                SqlDataReader dr;
                                objCmd.Connection.Open();
                                dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                                if (dr.RecordsAffected > 0)
                                {
                                    MsgBox.ShowBox(" Section Deleted Successfully!!!");
                                    Cancil_form();
                                    InitGrid();
                                }
                                else
                                {
                                    MsgBox.ShowBox(" Section Already Used!!!");
                                }
                            }

                            else
                            {
                                MsgBox.ShowBox("This Section Name Not DELETED Because it is used");
                            }
                        }
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
               // txtSectionname.Text = dataGridView1.CurrentRow.Cells["SEC_DESC"].Value.ToString();
                //txtAlias.Text = dataGridView1.CurrentRow.Cells["alias"].Value.ToString();
                txtSectionid.Text = dataGridView1.CurrentRow.Cells["SEC_ID"].Value.ToString();
                cmbClass.Text = dataGridView1.CurrentRow.Cells["CLS_DESC"].Value.ToString();
                txtstrength.Text = dataGridView1.CurrentRow.Cells["SEC_STRENGTH"].Value.ToString();
                cmbTeacher.Text = dataGridView1.CurrentRow.Cells["EMP_NAME"].Value.ToString();

                
               txtSectionname.Text = dataGridView1.CurrentRow.Cells["SEC_DESC"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void frmSectionMaster_Load(object sender, EventArgs e)
        {
            bindClass();
            bindTeacher();
            LoadGrid();
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
    }
}
