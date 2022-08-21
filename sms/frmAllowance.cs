using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;
using System.Collections;
using sms.DATALAYER;
using System.Reflection;
using System.IO;


namespace sms
{
    public partial class frmAllowance : Form
    {
        //BLclass bl = new BLclass();
        validation vl = new validation();
        public frmAllowance()
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
            bindAccountName();
            Cancil_form();
            InitGrid();
        }
        private void Cancil_form()
        {
           
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            cmbState.Text = "Select";
            txtcity.Text = "";
            txtvalue.Text = "";
            cmbaccount.Text = "Select";
            chkrefundable.Checked = true;
            cmbState.Focus();
        }
        private void bindstate()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from ALLOWENCE_TYPE_T ORDER BY TYPE_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["TYPE_DESC"] = "Select";
            row["TYPE_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbState.DataSource=ds.Tables[0];
            cmbState.DisplayMember = "TYPE_DESC";
            cmbState.ValueMember = "TYPE_ID";
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["AT_ID"].Visible = false;
            dataGridView1.Columns["ACC_NAME"].Width = 120;
            dataGridView1.Columns["ACC_NAME"].HeaderText = "ACCOUNT"; 
            dataGridView1.Columns["TYPE_DESC"].Width = 100;
            dataGridView1.Columns["TYPE_DESC"].HeaderText = "TYPE";
            dataGridView1.Columns["AT_DESC"].Width =225;
            dataGridView1.Columns["AT_DESC"].HeaderText = "ALLOWANCE && TAXES"; 
            dataGridView1.Columns["AT_VALUE"].Width = 100;
            dataGridView1.Columns["AT_VALUE"].HeaderText = "VALUE";
            dataGridView1.Columns["APPLY_ON"].Width = 90;
            dataGridView1.Columns["AT_APPLY_ON"].Visible = false;
        }
        private void LoadGrid()
        {
            string StateName;
            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                StateName="%";
            }
            else
            {
                StateName=cmbState.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select AT_ID,ACC_NAME,TYPE_DESC,AT_DESC,AT_VALUE,(CASE WHEN AT_APPLY_ON=1 THEN 'Y' ELSE 'N' END) AS APPLY_ON ,AT_APPLY_ON from View_ALLOWANCES_T where TYPE_DESC Like '" + StateName + "' and AT_DESC like '" + txtcity.Text + "%' ORDER BY TYPE_DESC,AT_DESC", sc);
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
                    //SqlCommand objCmd1 = new SqlCommand("select * from tblPartyMaster WHERE City=" + txtcityid.Text + "", conn);
                    //SqlDataAdapter da = new SqlDataAdapter(objCmd1);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    MsgBox.ShowBox(" City Used !!");
                    //    return;
                    //}
                    SqlCommand objCmd = new SqlCommand("DELETE FROM ALLOWANCES_T WHERE AT_ID=" + txtcityid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" ALLOWANCE Deleted Successfully!!!");
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
            if (cmbState.SelectedValue.ToString() == "0" || cmbState.Text=="")
            {
                MsgBox.ShowBox("Please Select Allowance Type.....");
            }
            if (cmbaccount.SelectedValue.ToString() == "0" || cmbaccount.Text == "")
            {
                MsgBox.ShowBox("Please Select Account Name.....");
            }
            else if (txtcity.Text == "")
            {
                MsgBox.ShowBox("Allowance Name Should not be blank...");
            }
            else
            {
                if (txtvalue.Text == "")
                    txtvalue.Text = "0";
                SqlCommand com = new SqlCommand("INSERT_ALLOWANCES  '" + Convert.ToInt32(cmbState.SelectedValue) + "','" + Convert.ToInt32(cmbaccount.SelectedValue) + "','" + txtcity.Text + "'," + txtvalue.Text + "," + Convert.ToInt32(chkrefundable.Checked) + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("ALLOWANCE Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This ALLOWANCE Name already exist");
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
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (cmbState.SelectedValue.ToString() == "0" || cmbState.Text == "")
            {
                MsgBox.ShowBox("Please Select Allowance Type.....");
            }
            if (cmbaccount.SelectedValue.ToString() == "0" || cmbaccount.Text == "")
            {
                MsgBox.ShowBox("Please Select Account Name.....");
            }
            else if (txtcity.Text == "")
            {
                MsgBox.ShowBox("Allowance Name Should not be blank...");
            }
            else
            {
                if (txtvalue.Text == "")
                    txtvalue.Text = "0";
                SqlCommand com = new SqlCommand("UPDATE_ALLOWANCES  "+txtcityid.Text+",'" + Convert.ToInt32(cmbState.SelectedValue) + "','" + Convert.ToInt32(cmbaccount.SelectedValue) + "','" + txtcity.Text + "'," + txtvalue.Text + "," + Convert.ToInt32(chkrefundable.Checked) + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("ALLOWANCE Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This ALLOWANCE already exist");
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
                txtcity.Text = dataGridView1.CurrentRow.Cells["AT_DESC"].Value.ToString();
                cmbState.Text = dataGridView1.CurrentRow.Cells["TYPE_DESC"].Value.ToString();
                txtcityid.Text = dataGridView1.CurrentRow.Cells["AT_ID"].Value.ToString();
                cmbaccount.Text = dataGridView1.CurrentRow.Cells["ACC_NAME"].Value.ToString();
                txtvalue.Text = dataGridView1.CurrentRow.Cells["AT_VALUE"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["AT_APPLY_ON"].Value.ToString() == "1")
                    chkrefundable.Checked = true;
                else
                    chkrefundable.Checked = false; 
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtcity_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void bindAccountName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from ACCOUNTS_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["ACC_NAME"] = "Select";
            row["ACC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbaccount.DataSource = ds.Tables[0];
            cmbaccount.DisplayMember = "ACC_NAME";
            cmbaccount.ValueMember = "ACC_ID";
        }
    
    }
}
