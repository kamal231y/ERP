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
    public partial class frmRackBinMaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmRackBinMaster()
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

        public void binddropdown()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Rack_T ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Rack_no"] = "Select";
            row["Rack_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "Rack_no";
            cmbDept.ValueMember = "Rack_id";
        }

        
    
           
        private void Cancil_form()
        {
            cmbDept.Text = "Select";
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtstrength.Text = "";
            txtbin.Text = "";

        }
      
        public void InitGrid()
        {
            LoadGrid();
            //dataGridView1.Columns["CITY_ID"].Visible = false;
            //dataGridView1.Columns["rack_no"].Width = 660;
            //dataGridView1.Columns["BIN"].HeaderText = "City";
            
           
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_Bin WHERE RACK_NO='" + cmbDept.SelectedValue + "'", sc);
            DataTable data = new DataTable();
            da.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.Columns["BIN_ID"].Visible = false;
            dataGridView1.Columns["rack_no"].Visible = false;            
            dataGridView1.Columns["rackno"].Width = 260;
            dataGridView1.Columns["STRENGTH"].Width = 260;
            dataGridView1.Columns["rackno"].HeaderText = "RACK _NO";
            dataGridView1.Columns["BIN_NO"].HeaderText = "BIN NO";
            dataGridView1.Columns["STRENGTH"].HeaderText = "STRENGTH";
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM Bin_T WHERE Bin_id= '"+ txtcityid.Text + "'", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" City Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtbin.Text == "")
            {
                MsgBox.ShowBox("Bin No Should not be blank...");
                return;
            }
            if (cmbDept.Text == "" || cmbDept.Text == "Select")
            {
                MsgBox.ShowBox("Rack No Should not be blank...");
                return;
            }
            if (txtstrength.Text == "")
            {
                MsgBox.ShowBox("Strength Should not be blank...");
                return;
            }
            else
            {
                SqlCommand com = new SqlCommand("INSERT_Bin_T '" + cmbDept.SelectedValue + "','"+txtbin.Text+"','"+txtstrength.Text+"'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Bin Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Bin No already exist");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            LoadGrid();
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
            if (txtbin.Text == "")
            {
                MsgBox.ShowBox("Bin No Should not be blank...");
                return;
            }
            if (cmbDept.Text == "" || cmbDept.Text == "Select")
            {
                MsgBox.ShowBox("Rack No Should not be blank...");
                return;
            }
            if (txtstrength.Text == "")
            {
                MsgBox.ShowBox("Strength Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_Bin_T'" + Convert.ToInt32(txtcityid.Text) + "','" + cmbDept.SelectedValue + "','" + txtbin.Text + "','" + txtstrength.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Bin Updated Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Bin Name already exist");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtbin.Text = dataGridView1.CurrentRow.Cells["Bin_No"].Value.ToString();
                cmbDept.Text = dataGridView1.CurrentRow.Cells["RackNo"].Value.ToString();
                txtstrength.Text = dataGridView1.CurrentRow.Cells["Strength"].Value.ToString();
                txtcityid.Text = dataGridView1.CurrentRow.Cells["BIN_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
            LoadGrid(); binddropdown();
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtcity_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbDept_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
    }
}
