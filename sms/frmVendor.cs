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
    public partial class frmVendor : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmVendor()
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
            bindCity();
            bindState();
            Cancil_form();
            InitGrid();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Cancil_form()
        {
            btVendorCreate.Enabled = true;
            btVendorUpate.Enabled = false;
            btVendorDelete.Enabled = false;
            txtVendorName.Text = "";
            cmbState.Text = "";
            cmbCity.Text = "";
            txtPinCode.Text = "";
            txtaddress.Text = "";
            txtPhoneno.Text = "";
            txtEmail.Text = "";
            txtMobileNo.Text = "";
            txtFax.Text = "";
            txtTinNo.Text = "";
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["vendor_id"].Visible = false;
            dataGridView1.Columns["VendorName"].Width = 200;
            dataGridView1.Columns["VendorName"].HeaderText = "Vendor Name ";
            dataGridView1.Columns["address"].Width = 200;
            dataGridView1.Columns["address"].HeaderText = "Address";
            dataGridView1.Columns["state_name"].Width = 110;
            dataGridView1.Columns["state_name"].HeaderText = "State Name";
            dataGridView1.Columns["city_name"].Width = 135;
            dataGridView1.Columns["city_name"].HeaderText = "City Name";
            dataGridView1.Columns["pin"].Width = 80;
            dataGridView1.Columns["pin"].HeaderText = "Pin Code";
            dataGridView1.Columns["mobile_no"].Width = 80;
            dataGridView1.Columns["mobile_no"].HeaderText = "Mobile No";
            dataGridView1.Columns["email"].Width = 80;
            dataGridView1.Columns["email"].HeaderText = "Email";
            dataGridView1.Columns["fax"].Width = 80;
            dataGridView1.Columns["fax"].HeaderText = "Fax";
            dataGridView1.Columns["cdate"].Width = 80;
            dataGridView1.Columns["cdate"].HeaderText = "Date";
   
            
          
        }
        private void bindState()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from state_t  order by STATE_NAME ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STATE_NAME"] = "Select";
            row["state_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbState.DataSource = ds.Tables[0];
            cmbState.DisplayMember = "STATE_NAME";
            cmbState.ValueMember = "state_id";

        }
        private void bindCity()
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_class_T where stc_sec_id =  '" + cmbClass.SelectedValue + "' order by  STC_ROLL_NO", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["stc_roll_no"] = "0";
            //row["stc_stu_id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            //cmbstuName.DataSource = ds.Tables[0];
            //cmbstuName.DisplayMember = "stc_roll_no";
            //cmbstuName.ValueMember = "stc_stu_id";
            //cmbstuName.Text = ROLL;



            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from city_t where StateCode =  '" + cmbState.SelectedValue + "' order by  city_id ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CITY_NAME"] = "Select";
            row["city_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbCity.DataSource = ds.Tables[0];
            cmbCity.DisplayMember = "CITY_NAME";
            cmbCity.ValueMember = "city_id";
            //cmbCity.Text = ROLL;

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_vendordetail  where vendorname like '"+txtVendorName.Text+"%'  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
      

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btVendorCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtVendorName.Text = dataGridView1.CurrentRow.Cells["vendorname"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["vendor_id"].Value.ToString();
                cmbState.Text = dataGridView1.CurrentRow.Cells["state_name"].Value.ToString();
                cmbCity.Text = dataGridView1.CurrentRow.Cells["city_name"].Value.ToString();
                txtaddress.Text = dataGridView1.CurrentRow.Cells["address"].Value.ToString();
                txtPhoneno.Text = dataGridView1.CurrentRow.Cells["phone_no"].Value.ToString();
                txtMobileNo.Text = dataGridView1.CurrentRow.Cells["mobile_no"].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
                txtTinNo.Text = dataGridView1.CurrentRow.Cells["tin_no"].Value.ToString();
                txtFax.Text = dataGridView1.CurrentRow.Cells["fax"].Value.ToString();
                txtPinCode.Text = dataGridView1.CurrentRow.Cells["pin"].Value.ToString();
                btVendorUpate.Enabled = true;
                btVendorDelete.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Vendor!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtVendorName.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand(" delete_Vendor " + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Vendor Deleted Successfully!!!");
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

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void frmPublisherMaster_Load(object sender, EventArgs e)
        {
            bindState();
            bindCity();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btVendorCreate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtVendorName.Text == "")
            {
                MsgBox.ShowBox("Vendor shoule not be blank...");
            }
            else
            {
                //SqlCommand com = new SqlCommand(" '" + txtstatename.Text + "'", sc);
                SqlCommand com = new SqlCommand("sp_AddVendor '" + txtVendorName.Text + "'," + cmbState.SelectedValue + "," + cmbCity.SelectedValue + ",'" + txtaddress.Text + "','" + txtPinCode.Text + "','" + txtPhoneno.Text + "','" + txtMobileNo.Text + "','" + (txtEmail.Text) + "','" + txtTinNo.Text + "','" + txtFax.Text + "'", sc);

                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Vendor Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Vendor already exist");
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btVendorUpate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtstateid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            //if (txtnoofbook.Text == "")
            //{
            //    NoOfBook = 0;
            //}

            //else
            //{
            //    NoOfBook = Convert.ToInt32(txtnoofbook.Text);

            //}


            SqlCommand com = new SqlCommand("update_Vendor '" + txtstateid.Text + "', '" + txtVendorName.Text + "'," + cmbState.SelectedValue + "," + cmbCity.SelectedValue + ",'" + txtaddress.Text + "','" + txtPinCode.Text + "','" + txtPhoneno.Text + "','" + txtMobileNo.Text + "','" + (txtEmail.Text) + "','" + txtTinNo.Text + "','" + txtFax.Text + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Vendor Update Successfully...");
                    Cancil_form();
           
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Vendor already exist");
                }

            }
            catch (Exception ex)
            { }
            finally
            {
                sc.Close();
            }
        }

        private void bt_delete_Click(object sender, EventArgs e)
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblVendor_Detail WHERE  vendor_id=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Vendor Deleted Successfully!!!");
                        Cancil_form();
                        LoadGrid();
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

        private void btVendorCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btVendorClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btVendorClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbState_Leave(object sender, EventArgs e)
        {
            bindCity();
        }

       

       

      
       
    }
}
