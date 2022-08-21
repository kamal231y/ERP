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
    public partial class frmArrangebook : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmArrangebook()
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
        public void bindsubject()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" Select * from SUBJECT_T order by sub_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["sub_DESC"] = "Select";
            row["sub_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_subject.DataSource = ds.Tables[0];
            cmb_subject.DisplayMember = "sub_DESC";
            cmb_subject.ValueMember = "sub_ID";

        }
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID,CLS_DESC  from CLASS_T order by cls_sequence ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "ALL";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "CLS_DESC";
            cmb_class.ValueMember = "CLS_ID";
        }
        public void Publisher()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Public_T ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            cmb_publisher.DataSource = ds.Tables[0];
            cmb_publisher.DisplayMember = "Publication_NAME";
            cmb_publisher.ValueMember = "Publication_Id";
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
            SqlDataAdapter da = new SqlDataAdapter("select * from View_ARRAGEBOOK ", sc);
            DataTable data = new DataTable();
            da.Fill(data);
            dataGridView1.DataSource = data;
            
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
                SqlCommand com = new SqlCommand("Insert_TblARRANGEBOOK '" + cmbDept.SelectedValue + "','" + txtbin.Text + "','" + cmb_subject.SelectedValue + "','" + cmb_class.SelectedValue + "','" + cmb_publisher.SelectedValue + "','" + txtstrength.Text + "','" + textBox1.Text + "','" + textBox3.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Record Save...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Record  already exist");
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
                SqlCommand com = new SqlCommand("Update_TblARRANGEBOOK '" + Convert.ToInt32(txtcityid.Text) + "''" + cmbDept.SelectedValue + "','" + txtbin.Text + "','" + cmb_subject.SelectedValue + "','" + cmb_class.SelectedValue + "','" + cmb_publisher.SelectedValue + "','" + txtstrength.Text + "','" + textBox1.Text + "','" + textBox3.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Record Updated ...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Record  already exist");
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
                cmbDept.Text = dataGridView1.CurrentRow.Cells["Rack_No"].Value.ToString();
                txtstrength.Text = dataGridView1.CurrentRow.Cells["Strength"].Value.ToString();
                txtcityid.Text = dataGridView1.CurrentRow.Cells["BIN_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
            LoadGrid(); binddropdown(); bindsubject(); Publisher(); bindclass();
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

        private void txtbin_TextChanged(object sender, EventArgs e)
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from bin_t where rack_no= '" + cmbDept.SelectedValue + "' and bin_no='" + txtbin.Text + "' ", sc);
            //DataTable ds = new DataTable();
            //da.Fill(ds);
            //if (ds.Rows.Count > 0)
            //{
            //    dataclass dc1 = new dataclass();
            //    SqlConnection sc1 = dc.con();
            //    SqlDataAdapter da1 = new SqlDataAdapter("Find_strenght ", sc);
            //    da1.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    da1.SelectCommand.Parameters.AddWithValue("@rack", cmbDept.SelectedValue);
            //    da1.SelectCommand.Parameters.AddWithValue("@bin", txtbin.Text);
            //    sc1.Open();
            //    DataTable dt = new DataTable();
            //    da1.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
                   
            //    }
            //    else
            //    {
            //        MessageBox.Show("Strenght Full For This Bin No");

            //    }
             
            //}
            //else
            //{
            //    MessageBox.Show("This Bin No not Exists");
            //}
           
        }

        private void cmb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

       
    }
}
