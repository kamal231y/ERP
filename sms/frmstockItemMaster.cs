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
    public partial class frmstockItemMaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmstockItemMaster()
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
           bindclass(); 
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            btn_create.Enabled = true;
            txtBook.Text = "";
            //txtAlias.Text = "";
            txtstateid.Text = "";
           
            cmb_dept.Text = "ALL";
            txtMRP.Text = "";
          
            txtOpnStock.Text = "";
            txtBarcode.Text = "";

          InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["Item_ID"].Visible = false;
            dataGridView1.Columns["Item_Name"].Width = 300;
            dataGridView1.Columns["Item_Name"].HeaderText = "Book / Item";
            
          
        }
     

        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select DEPT_ID,DEPT_DESC  from DEPARTMENT_T order by DEPT_DESC ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "ALL";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_dept.DataSource = ds.Tables[0];
            cmb_dept.DisplayMember = "DEPT_DESC";
            cmb_dept.ValueMember = "DEPT_ID";
        }
        private void LoadGrid()
        {
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ItemMaster_T ",sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void OnlyCapital(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text == "")
                txtBarcode.Text = "0";
            if (txtOpnStock.Text == "")
                txtOpnStock.Text = "0";
            DateTime dt1 = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str= sc.DataSource.ToString();
            if (txtBook.Text == "")
            {
                MsgBox.ShowBox(" Item not be blank...");
                return;
            }
             else if (cmb_dept.Text == "" || cmb_dept.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select department....."); return;
            }
            else
            {
                SqlCommand com = new SqlCommand("insert_ItemMaster '" + txtBarcode.Text.Trim().Replace(" ", "") + "','" + txtBook.Text + "','" + cmb_dept.SelectedValue + "','" + txtMRP.Text + "','" + txtOpnStock.Text + "','" + dt1.ToString("MM/dd/yyyy") + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Item Created...");
                        Cancil_form();
                        LoadGrid(); return;
                    }
                    else
                    {
                        MsgBox.ShowBox("This  Item/ Barcode already exist"); return;
                    }
                    
                }
                catch (Exception ex)
                {
                    MsgBox.ShowBox(ex.ToString());
                }
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
            btnUpdate.Enabled = true;
            btn_delete.Enabled = true;
            btn_create.Enabled = false; 
            if (dataGridView1.Rows.Count > 0)
            {
                txtstateid.Text = dataGridView1.CurrentRow.Cells["Item_ID"].Value.ToString();
                txtBook.Text = dataGridView1.CurrentRow.Cells["Item_Name"].Value.ToString();
             

                cmb_dept.SelectedValue = dataGridView1.CurrentRow.Cells["Depart_Name"].Value.ToString();
                txtMRP.Text = dataGridView1.CurrentRow.Cells["Mrp"].Value.ToString();

                txtOpnStock.Text = dataGridView1.CurrentRow.Cells["opening_stock"].Value.ToString();
                txtBarcode.Text = dataGridView1.CurrentRow.Cells["Barcode_No"].Value.ToString();


                dateofadm.Text = dataGridView1.CurrentRow.Cells["opening_date"].Value.ToString();
                btnUpdate.Enabled=true;
                btn_delete.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text == "")
                txtBarcode.Text = "0";
            if (txtOpnStock.Text == "")
                txtOpnStock.Text = "0";
            DateTime dt1 = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtstateid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            else  if (txtBook.Text == "")
            {
                MsgBox.ShowBox("Book / Item Should not be blank...");
            }
           else if (cmb_dept.Text == "" || cmb_dept.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Department.....");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_ItemMaster '" + txtstateid.Text + "','" + txtBarcode.Text.Trim().Replace(" ", "") + "','" + txtBook.Text + "','" + cmb_dept.SelectedValue + "','" + txtMRP.Text + "','" + txtOpnStock.Text + "','" + dt1.ToString("MM/dd/yyyy") + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Item Updated..."); 
                        Cancil_form();
                        LoadGrid();
                        return;
                    }
                    else
                    {
                        MsgBox.ShowBox("This  Item already exist");
                        return;
                    }

                }
                catch (Exception ex)
                { MsgBox.ShowBox(ex.ToString()); }
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
                    SqlCommand objCmd = new SqlCommand(" Delete_ItemMaster " + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Item Deleted Successfully!!!");
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
                    MsgBox.ShowBox(ex.ToString());
                }
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

  

        private void frmBookItemMaster_Load(object sender, EventArgs e)
        {
            bindclass(); InitGrid();
        }


      
           
   
        

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }     
       
    }
}
