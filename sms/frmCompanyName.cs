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
    public partial class frmCompanyName : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmCompanyName()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Green;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Green;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;

            MsgBoxRetType.backColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonBackColour = Color.Green;
            MsgBoxRetType.buttonForeColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonStyle = FlatStyle.Flat;
            MsgBoxRetType.titleBackColour = Color.Green;
            MsgBoxRetType.titleForeColour = Color.WhiteSmoke;
            MsgBoxRetType.messageBoxStyle = FlatStyle.Flat;
            MsgBoxRetType.foreColour = Color.Green;
            InitializeComponent();
            Cancil_form();
           
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
            btnUpdate.Enabled = false;
           // txtstatename.Text = "";
            //txtAlias.Text = "";
            InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["COM_ID"].Visible = false;
            //dataGridView1.Columns["SCHOOL_MOBILENO"].Width = 660;
            //dataGridView1.Columns["SCHOOL_MAILID"].Width = 660;
            //dataGridView1.Columns["SCHOOL_CODE"].Width = 660;
            //dataGridView1.Columns["SCHOOL_AFFILIATION"].Width = 660;
            //dataGridView1.Columns["SCHOOL_ADDRESS"].Width = 660;
            dataGridView1.Columns["SCHOOL_CODE"].HeaderText = "School code";
            dataGridView1.Columns["AFFLIATION_CODE"].HeaderText = "Affiliation Code";
            dataGridView1.Columns["companypincode"].HeaderText = "Address 2";
            dataGridView1.Columns["EMAIL"].HeaderText = "Email";
            dataGridView1.Columns["companyaddress"].HeaderText = "Address";
            dataGridView1.Columns["companytelno"].HeaderText = "Mobile No.";
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select COM_ID ,companytelno , EMAIL, SCHOOL_CODE, AFFLIATION_CODE, companyaddress,companypincode  from COMPANY_T ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtmobile.Text = dataGridView1.CurrentRow.Cells["companytelno"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["COM_ID"].Value.ToString();
                txtmail.Text = dataGridView1.CurrentRow.Cells["EMAIL"].Value.ToString();
                txtschoolcode.Text = dataGridView1.CurrentRow.Cells["SCHOOL_CODE"].Value.ToString();
                txtaffilation.Text = dataGridView1.CurrentRow.Cells["AFFLIATION_CODE"].Value.ToString();
                txtadd1.Text = dataGridView1.CurrentRow.Cells["companyaddress"].Value.ToString();
                txtadd2.Text = dataGridView1.CurrentRow.Cells["companypincode"].Value.ToString();

                btnUpdate.Enabled=true;
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
            if (txtmobile.Text == "")
            {
                MsgBox.ShowBox("Mobile No. Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_companyt  '" + txtmobile.Text + "','" + txtmail.Text + "','" + txtschoolcode.Text + "','" + txtaffilation.Text + "','" + txtadd1.Text + "','" + txtadd2.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("School Detail Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("Updating Error");
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
      
        }

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void frmStateMaster_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void txtstateid_TextChanged(object sender, EventArgs e)
        {

        }

       

       
       
    }
}
