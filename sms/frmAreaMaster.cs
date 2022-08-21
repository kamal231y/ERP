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
    public partial class frmAreaMaster : Form
    {
        DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmAreaMaster()
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
            bindstate();
            //bindCity();
            Cancil_form();
            InitGrid();
        }

        private void Cancil_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;

            cmbState.Text = "Select";
            cmbCity.Text = "";
            txtlocation.Text = "";
            txtlocationid.Text = "";
        }
        private void bindstate()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblStateMaster order by statename", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["StateName"] = "Select";
            row["StateCode"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbState.DataSource = ds.Tables[0];
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "StateCode";
        }

        private void bindCity()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da;
            if (cmbState.Text=="Select" || cmbState.Text=="")
                da = new SqlDataAdapter(" select * from tblCityMaster where stateCode like '%'", sc);
            else
                da = new SqlDataAdapter(" select * from tblCityMaster where stateCode='" + Convert.ToInt32(cmbState.SelectedValue) + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CityName"] = "Select";
            row["CityCode"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbCity.DataSource = ds.Tables[0];
            cmbCity.DisplayMember = "CityName";
            cmbCity.ValueMember = "CityCode";
        }

        public void InitGrid()
        {
            LoadGrid();
            
        }
        private void LoadGrid()
        {
            string StateName,CityName;
            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbState.Text;
            }
            if (cmbCity.Text == "Select" || cmbCity.Text == "")
            {
                CityName = "%";
            }
            else
            {
                CityName = cmbCity.Text;
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_tblArea where StateName like '"+StateName+"' and CityName like '"+CityName+"' and AreaName Like '"+txtlocation.Text+"%' ORDER BY StateName,CityName", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbState.SelectedValue.ToString() == "0" || cmbState.Text == "")
            {
                MsgBox.ShowBox("Please Select State...");
            }
            else if (cmbCity.SelectedValue.ToString() == "0" || cmbCity.Text == "")
            {
                MsgBox.ShowBox("Please Select City...");
            }
            else if (txtlocation.Text == "")
            {
                MsgBox.ShowBox("Area Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_tblAreaMaster '" + txtlocation.Text + "','" + Convert.ToInt32(cmbState.SelectedValue) + "','" + Convert.ToInt32(cmbCity.SelectedValue) + "','" + Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Area Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Area Name already exist");
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

        private void cmbState_Leave(object sender, EventArgs e)
        {
            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                MsgBox.ShowBox("Please Select State. !!!");
                return;
            }
            else
            {
                bindCity();
            }
            LoadGrid();
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
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
            if (txtlocationid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            else if (cmbState.SelectedValue.ToString() == "0" || cmbState.Text == "")
            {
                MsgBox.ShowBox("Please Select State...");
            }
            else if (cmbCity.SelectedValue.ToString() == "0" || cmbCity.Text == "")
            {
                MsgBox.ShowBox("Please Select City...");
            }
            else if (txtlocation.Text == "")
            {
                MsgBox.ShowBox("Area Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_tblAreaMaster  '"+txtlocationid.Text+"','" + txtlocation.Text + "','" + Convert.ToInt32(cmbState.SelectedValue) + "','" + Convert.ToInt32(cmbCity.SelectedValue) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Area Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Area Name already exist");
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
                    if (txtlocationid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblAreaMaster WHERE AreaCode=" + txtlocationid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Area Deleted Successfully!!!");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtlocationid.Text = dataGridView1.CurrentRow.Cells["Area Code"].Value.ToString();
                txtlocation.Text = dataGridView1.CurrentRow.Cells["Area Name"].Value.ToString();
                cmbState.Text = dataGridView1.CurrentRow.Cells["StateName"].Value.ToString();
                bindCity();
                cmbCity.Text = dataGridView1.CurrentRow.Cells["CityName"].Value.ToString();
                
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbCity_Leave(object sender, EventArgs e)
        {
            if (cmbCity.Text == "Select" || cmbCity.Text == "")
            {
                MsgBox.ShowBox("Please City State. !!!");
                return;
            }
            LoadGrid();
        }

        private void txtlocation_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void frmAreaMaster_Load(object sender, EventArgs e)
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

       
    }
}
