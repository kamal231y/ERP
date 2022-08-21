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
    public partial class frmBOOKFINEMaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmBOOKFINEMaster()
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
            InitGrid();
        }
        private void Cancil_form()
        {
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            //cmbState.Text = "Select";
            txtcity.Text = "";

        }
      
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["days"].Width = 260;
            dataGridView1.Columns["days"].HeaderText = "No of Days";
            dataGridView1.Columns["rate"].Width = 260;
            dataGridView1.Columns["rate"].HeaderText = "Rate";
            
           
        }
        private void LoadGrid()
        {
            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from book_fine where days like '" + txtcity.Text + "%' ORDER BY ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["days"].Width = 260;
            dataGridView1.Columns["days"].HeaderText = "No of Days";
            dataGridView1.Columns["rate"].Width = 260;
            dataGridView1.Columns["rate"].HeaderText = "Rate";
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM book_fine WHERE ID=" + txtcityid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Fine Deleted Successfully!!!");
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
            if (txtcity.Text == "")
            {
                MsgBox.ShowBox("No of Days Should not be blank...");
            }
            if (textBox1.Text == "" || textBox1.Text == "0")
            {
                MsgBox.ShowBox("Fine Should not be Zero...");
            }
            else
            {
                SqlCommand com = new SqlCommand("insert into book_fine values('" + txtcity.Text + "','" + textBox1.Text+ "')", sc);
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
                        MsgBox.ShowBox("Record  already exist");
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
            txtcity.Text="";
            textBox1.Text = "";
            //LoadGrid();
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
            if (txtcity.Text == "")
            {
                MsgBox.ShowBox("No of Days Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update book_fine set days='" + txtcity.Text + "',rate='"+textBox1.Text+"' where id= '" + Convert.ToInt32(txtcityid.Text) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Record Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Record already exist");
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
                txtcity.Text = dataGridView1.CurrentRow.Cells["days"].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells["rate"].Value.ToString();
                txtcityid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
