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
    public partial class FrmLedgerHead : Form
    {
        public FrmLedgerHead()
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
        }

        private void FrmAccountHead_Load(object sender, EventArgs e)
        {
            LoadGrid();
            bindGroupType();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["Gid"].Width = 40;
            dataGridView1.Columns["GName"].Width = 100;
            //dataGridView1.Columns["ClassName"].Width = 60;
            dataGridView1.Columns["AccountHeaderType"].Width = 60;
            dataGridView1.Columns["GType"].Width = 60;
            dataGridView1.Columns["userid"].Width = 60;
            dataGridView1.Columns["date"].Width = 80;
            dataGridView1.Columns["terminalid"].Width = 100;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblGroup ORDER BY GName", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindGroupType()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblGroupType", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["type"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbgroup.DataSource = ds.Tables[0];
            cmbgroup.DisplayMember = "Type";
            cmbgroup.ValueMember = "id";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            bool value = true;
            if (rbmarks.Checked == true)
            {
                value = true;

            }
            if (rbgrade.Checked == true)
            {
                value = false;
            }
            bool value1 = true;

            if (rbMain.Checked == true)
            {
                value1 = true;
            }
            if (rbOSub.Checked == true)
            {
                value1 = false;
            }


            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbgroup.SelectedValue.ToString() == "")
            {
                MsgBox.ShowBox("Type should not be blank");
                return;
            }
            SqlCommand com = new SqlCommand("Insert_tblgroup '" + txthead.Text + "','" + Convert.ToInt32(value) + "','" + Convert.ToInt32(value1) + "', '" + cmbgroup.SelectedValue + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + dt1 + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("AccountHeads Created...");
                    Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This AccountHeads Name already exist");
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
            bool value = true; ;
            if (rbmarks.Checked == true)
            {
                value = true;
            }
            if (rbgrade.Checked == true)
            {
                value = false;
            }
            bool value1 = true;


            if (rbMain.Checked == true)
            {
                value1 = true;
            }
            if (rbOSub.Checked == true)
            {
                value1 = false;
            }

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtsubjectid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (cmbgroup.SelectedValue.ToString() == "")
            {
                MsgBox.ShowBox("State Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_tblgroup '" + txtsubjectid.Text + "','" + txthead.Text + "', '" + Convert.ToInt32(value) + "','" + Convert.ToInt32(value1) + "', '" + cmbgroup.SelectedValue + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("AccountHeads Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This AccountHeads Name already exist");
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
                    if (txtsubjectid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblSUBJECTMaster WHERE Sectioncode=" + txtsubjectid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" AccountHeads Deleted Successfully!!!");
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
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txthead.Text = "";
            cmbgroup.SelectedValue = 0;
           

        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                cmbgroup.Text = dataGridView1.CurrentRow.Cells["GroupType"].Value.ToString();
                txthead.Text = dataGridView1.CurrentRow.Cells["GName"].Value.ToString();
                txtsubjectid.Text = dataGridView1.CurrentRow.Cells["Gid"].Value.ToString();

                if (dataGridView1.CurrentRow.Cells["AccountHeaderType"].Value.ToString() == "Profit And Loss ")
                {
                    rbmarks.Checked = true;
                }
                else
                {
                    rbgrade.Checked = true;
                }
                //rbMain.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_TYPE_ID"].Value.ToString());
                if (dataGridView1.CurrentRow.Cells["Type"].Value.ToString() == "Assets ")
                {
                    rbMain.Checked = true;
                }
                else
                {
                    rbOSub.Checked = true;
                }
                //rbgrade.Checked =Convert.ToBoolean( dataGridView1.CurrentRow.Cells["SUB_EVO_ID"].Value.ToString());
                //rbOSub.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_TYPE_ID"].Value.ToString());

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            WindowState = FormWindowState.Minimized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
