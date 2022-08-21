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
    public partial class frmVehicleMake : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmVehicleMake()
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
        private void OnlyCapital(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
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
            btn_create.Enabled = true;
            txtstatename.Text = "";
            //txtAlias.Text = "";
            InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["vmk_ID"].Visible = false;
            dataGridView1.Columns["vmk_desc"].Width = 660;
            dataGridView1.Columns["vmk_desc"].HeaderText = "Vehicle Make";
            
          
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from VEHICLE_MAKE_T where vmk_desc like '" + txtstatename.Text + "%' ORDER BY vmk_ID", sc);
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
                MsgBox.ShowBox("Vehicle Make not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("insert_VEHICLE_MAKE_T '" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Vehicle Make Created...");
                        Cancil_form();
                        
                    }
                    else
                    {
                        MsgBox.ShowBox("This Vehicle Make already exist");
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
                txtstatename.Text = dataGridView1.CurrentRow.Cells["vmk_desc"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["vmk_ID"].Value.ToString();
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
                MsgBox.ShowBox("Vehicle Make Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_VEHICLE_MAKE_T '" + txtstateid.Text + "','" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Vehicle Make Updated...");
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Vehicle Make!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM VEHICLE_MAKE_T WHERE vmk_ID=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Vehicle Make Deleted Successfully!!!");
                        Cancil_form();
                        
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

        private void VehicleModel_Load(object sender, EventArgs e)
        {

        }

        private void txtstatename_TextChanged(object sender, EventArgs e)
        {
            TextBox T = (TextBox)sender;
            try
            {
                //Not Allowing Numbers
                char[] UnallowedCharacters = { '0', '1',
                                           '2', '3', 
                                           '4', '5',
                                           '6', '7',
                                           '8', '9'};

                if (textContainsUnallowedCharacter(T.Text, UnallowedCharacters))
                {
                    int CursorIndex = T.SelectionStart - 1;
                    T.Text = T.Text.Remove(CursorIndex, 1);

                    //Align Cursor to same index
                    T.SelectionStart = CursorIndex;
                    T.SelectionLength = 0;
                }
            }
            catch (Exception) { }
        }

        private bool textContainsUnallowedCharacter(string T, char[] UnallowedCharacters)
        {
            for (int i = 0; i < UnallowedCharacters.Length; i++)
                if (T.Contains(UnallowedCharacters[i]))
                    return true;

            return false;
        }      
       
    }
}
