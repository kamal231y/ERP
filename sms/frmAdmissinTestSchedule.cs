using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using sms.BLClass;
using System.Data.SqlClient;
using MsgBoxRet;

namespace sms
{
    public partial class frmAdmissinTestSchedule : Form
    {
        public frmAdmissinTestSchedule()
        {
            InitializeComponent();
            Cancil_form();
            InitGrid();
        }

        private void Cancil_form()
        {
            btn_create.Enabled = true;
            txtName.Text = "";
            dtp1.Text = "";
            dtp2.Text = "";
            dtp3.Text = "";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["Testid"].Visible=false;
            dataGridView1.Columns["Dates"].Width = 130;
            dataGridView1.Columns["Name"].Width = 130;
            dataGridView1.Columns["FromDate"].Width = 160;
            dataGridView1.Columns["FromDate"].HeaderText="From Time";
            dataGridView1.Columns["Todate"].Width = 160;
            dataGridView1.Columns["Todate"].HeaderText = "To Time";
            //dataGridView1.Columns["terminalid"].Width = 100;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select Testid,CONVERT(VARCHAR(10),Dates, 103 )  AS Dates ,Name,CONVERT(VARCHAR(10),FromDate, 8 )  AS FromDate, CONVERT(VARCHAR(10),ToDate, 8 )  AS  Todate from tblAdmissionSchedule ORDER BY Testid", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }



        private void frmAdmissinTestSchedule_Load(object sender, EventArgs e)
        {
            LoadGrid();

        }

        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtName.Text == "")
            {
                MsgBox.ShowBox("Name should not be blank");
                return;
            }
            //SqlCommand com = new SqlCommand("isnert_tblAdmessionSchedule '" + dtp1.Text + "', '" + txtName.Text + "','" + dtp2.Text + "','" + dtp3.Text + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
            SqlCommand com = new SqlCommand("isnert_tblAdmessionSchedule '" + dtp1.Text + "', '" + txtName.Text + "','" + dtp2.Text + "','" + dtp3.Text + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Test Admission Created...");
                    //Cancil_form();
                    //LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Admission Name already exist");
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
            LoadGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
               
                txtAdmissionid.Text = dataGridView1.CurrentRow.Cells["Testid"].Value.ToString();
                dtp1.Text = dataGridView1.CurrentRow.Cells["Dates"].Value.ToString();
                dtp2.Text = dataGridView1.CurrentRow.Cells["FromDate"].Value.ToString();
                dtp3.Text = dataGridView1.CurrentRow.Cells["Todate"].Value.ToString();

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }

        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtAdmissionid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtName.Text == "")
            {
                MsgBox.ShowBox("Test Nmae Should not be blank...");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update tblAdmissionSchedule set Dates='"+dtp1.Text+"',Name='"+txtName.Text+"',FromDate='"+dtp2.Text+"',ToDate='"+dtp3.Text+"'  where Testid='" + txtAdmissionid.Text + "'",sc);
               SqlDataReader dr; 
            sc.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Form Updated...");
                    LoadGrid();
                    Cancil_form();
                }
                else
                {
                    MsgBox.ShowBox("This Form already exist");
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
                    if (dtp1.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblAdmissionSchedule WHERE Testid=" + txtAdmissionid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Form Deleted Successfully!!!");
                        Cancil_form();
                        LoadGrid();
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

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txtAdmissionid_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtp2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
