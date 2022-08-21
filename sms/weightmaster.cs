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
    public partial class weightmaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public weightmaster()
       
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

        
        private void Cancil_form()
        {
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtstatename.Text = "";
            btn_create.Enabled = true;
            //txtAlias.Text = "";
            InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["WEIGHT_ID"].Visible = false; 
            dataGridView1.Columns["WEIGHT_DESC"].Width =350;
            dataGridView1.Columns["WEIGHT_DESC"].HeaderText = "Weight";
           
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from WEIGHT_T where WEIGHT_DESC like '" + txtstatename.Text + "%' ORDER BY WEIGHT_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_create_Click(object sender, EventArgs e)
       
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str= sc.DataSource.ToString();
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Weight Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_weight '" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Weight Created...");
                        Cancil_form();
                        
                    }
                    else
                    {
                        MsgBox.ShowBox("This Weight already exist");
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
                MsgBox.ShowBox("weight Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_tblweight '" + txtstateid.Text + "','" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("weight Updated...");
                        Cancil_form();
                       
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

        private void btn_delete_Click(object sender, EventArgs e)
        
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Weight!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM WEIGHT_T WHERE WEIGHT_ID=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Weight Deleted Successfully!!!");
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtstatename.Text = dataGridView1.CurrentRow.Cells["WEIGHT_DESC"].Value.ToString();
                //txtAlias.Text = dataGridView1.CurrentRow.Cells["alias"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["WEIGHT_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void txtstatename_TextChanged(object sender, EventArgs e)
        {
            Exception X = new Exception();

            TextBox T = (TextBox)sender;

            try
            {
                if (T.Text != "-")
                {
                    double x = double.Parse(T.Text);

                    if (T.Text.Contains(','))
                        throw X;
                }
            }
            catch (Exception)
            {
                try
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
                catch (Exception) { }
            }
        }

        private void weightmaster_Load(object sender, EventArgs e)
        {

        }

       
    }
}
