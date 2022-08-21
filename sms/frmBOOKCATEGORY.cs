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
    public partial class frmBOOKCATEGORY:Form

    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmBOOKCATEGORY()
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
           // InitGrid();
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
            btn_delete.Enabled = false;
            txtstatename.Text = "";
            btn_create.Enabled = true;
            
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["bc_ID"].Visible = false;
            dataGridView1.Columns["bc_DESC"].Width = 260;
            dataGridView1.Columns["bc_DESC"].HeaderText = "Book Category";
            
          
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from Book_cat_T where bc_DESC like '" + txtstatename.Text + "%' ORDER BY bc_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["bc_ID"].Visible = false;
            dataGridView1.Columns["bc_DESC"].Width = 260;
            dataGridView1.Columns["bc_DESC"].HeaderText = "Book Category";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            //DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Book Category  Name Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("INSERT_Book_cat_T '" + txtstatename.Text + "' ", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Book Category   Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Book Category  name already exist");
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtstatename.Text = dataGridView1.CurrentRow.Cells["bc_DESC"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["bc_ID"].Value.ToString();
                btnUpdate.Enabled=true;
                btn_delete.Enabled = true;
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
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Book Category  Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("UPDATE_Book_cat_T'" + txtstateid.Text + "','" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Book Category Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Book Category  Name already exist");
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
                    if (txtstateid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM Book_cat_T WHERE bc_ID=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Book Category  Deleted Successfully!!!");
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
           // LoadGrid();
        }

        private void frmStateMaster_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtstatename_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
       
    }
}
