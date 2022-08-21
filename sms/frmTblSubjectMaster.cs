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


namespace sms
{
    public partial class frmTblSubjectMaster : Form
    {
        public frmTblSubjectMaster()
        {
            InitializeComponent();
            InitGrid();
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["SUB_ID"].Visible = false;
            dataGridView1.Columns["SUB_DESC"].Width = 750;
            dataGridView1.Columns["SUB_DESC"].HeaderText="Subject";
           
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtstatename.Text = "";
            //txtAlias.Text = "";

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select SUB_DESC,SUB_ID from SUBJECT_T where SUB_DESC like '" + txtstatename.Text + "%' ORDER BY SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("State Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_SUBJECT_T'" + txtstatename.Text + "','0','0','0'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Subject Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Subject Name already exist");
                    }

                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtstateid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Subject Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_SUBJECT_T '" + txtstateid.Text + "','" + txtstatename.Text + "','0','0','0'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Subject Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This State Name already exist");
                    }

                }
                catch (Exception ex)
                { }
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
                    if (txtstateid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM SUBJECT_T WHERE SUB_ID=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Subject Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
                        dataGridView1.Columns["SUB_DESC"].Width = 190;
                        dataGridView1.Columns["Alias"].Width = 180;
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtstatename.Text = dataGridView1.CurrentRow.Cells["SUB_DESC"].Value.ToString();
                //txtAlias.Text = dataGridView1.CurrentRow.Cells["Alias"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["SUB_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            
        }

        private void frmTblSubjectMaster_Load(object sender, EventArgs e)
        {
            LoadGrid();
            dataGridView1.Columns["SUB_DESC"].Width = 440;
            //dataGridView1.Columns["Alias"].Width = 200;
            dataGridView1.Columns["SUB_ID"].Width = 0;
        }

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
           
        }
    }
}
