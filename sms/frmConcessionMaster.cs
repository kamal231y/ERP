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
    public partial class frmConcessionMaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmConcessionMaster()
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
            //bindstate();
            Cancil_form();
            
        }
        private void Cancil_form()
        {
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            //cmbState.Text = "Select";
            txtcity.Text = "";
            txtconcesssion.Text = "";
            radiomale.Checked = true;
            InitGrid();
        }
      
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["mode"].Visible = false;
            dataGridView1.Columns["studentno"].Width = 150;
            dataGridView1.Columns["studentno"].HeaderText = "Adm. No.";
            dataGridView1.Columns["modetype"].Width = 100;
            dataGridView1.Columns["modetype"].HeaderText = "Mode";
            dataGridView1.Columns["amount"].Width = 100;
            dataGridView1.Columns["amount"].HeaderText = "Concession";
           
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select *,(case when mode=1 then 'Percent' else 'Flat' end) as modetype  from Student_Concession where studentno like '" + txtcity.Text + "%' ORDER BY studentno", sc);
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Concession!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM Student_Concession WHERE ID=" + txtcityid.Text + "  ", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Concession Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("Concession can't  Delete!!!");
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
            if (txtcity.Text == "")
            {
                MsgBox.ShowBox("Student Adm. No Should not be blank...");
            }
            else if (txtconcesssion.Text == "")
            {
                MsgBox.ShowBox("Concesssion Should not be blank...");
            }
            else
            {    int mode=0;
            if (radiomale.Checked == true)
                mode = 1;
            else
                mode = 0;

            SqlCommand com = new SqlCommand(" exec insert_Consessionmaster " + txtcity.Text + "," + mode + "," + txtconcesssion.Text + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Concession Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Concession already exist");
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
            else  if (txtcity.Text == "")
            {
                MsgBox.ShowBox("Student Adm. No Should not be blank...");
            }
            else if (txtconcesssion.Text == "")
            {
                MsgBox.ShowBox("Concesssion Should not be blank...");
            }
            else
            {
                int mode = 0;
                if (radiomale.Checked == true)
                    mode = 1;
                else
                    mode = 0;
                SqlCommand com = new SqlCommand("update Student_Concession set studentno=" + (txtcity.Text) + ",mode=" + mode + ",amount=" + (txtconcesssion.Text) + "  where id=" + txtcityid.Text + " ", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Concession Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("Updating Error !!!");
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
            //dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["mode"].Visible = false;
            //dataGridView1.Columns["studentno"].Width = 200;
            //dataGridView1.Columns["studentno"].HeaderText = "Student Sr. No.";
            //dataGridView1.Columns["modetype"].Width = 200;
            //dataGridView1.Columns["modetype"].HeaderText = "Mode";
            //dataGridView1.Columns["amount"].Width = 200;
            //dataGridView1.Columns["amount"].HeaderText = "Concession";
            btnCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtcity.Text = dataGridView1.CurrentRow.Cells["studentno"].Value.ToString();
                txtcityid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                if (Convert.ToInt32(dataGridView1.CurrentRow.Cells["mode"].Value.ToString()) == 1)
                    radiomale.Checked = true;
                else
                    radiofemale.Checked = true;
                txtconcesssion.Text = dataGridView1.CurrentRow.Cells["amount"].Value.ToString();
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

       
    }
}
