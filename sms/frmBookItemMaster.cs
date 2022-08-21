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
    public partial class frmBookItemMaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmBookItemMaster()
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
        private void frmBookItemMaster_Load(object sender, EventArgs e)
        {
            BindPublisher(); bindclass(); InitGrid();
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
            BindPublisher(); bindclass(); 
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            btn_create.Enabled = true;
            txtBook.Text = "";
            //txtAlias.Text = "";
            txtstateid.Text = "";
            cmb_Pub.Text = "Select";
            cmb_class.Text = "ALL";
            txtMRP.Text = "";
            txtPprice.Text = "";
            txtOpnStock.Text = "";
            cmb_Group.Text = "";
            txtsize.Text = "";
            txtDiscount.Text = ""; InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["itm_ID"].Visible = false;
            dataGridView1.Columns["Groupid"].Width = 100;
            dataGridView1.Columns["Groupid"].HeaderText = "B/I Code";
            dataGridView1.Columns["itm_desc"].Width = 300;
            dataGridView1.Columns["itm_desc"].HeaderText = "Book / Item";
            dataGridView1.Columns["cls_desc"].Width = 100;
            dataGridView1.Columns["cls_desc"].HeaderText = "Class";
             dataGridView1.Columns["pub_name"].Width = 300;
            dataGridView1.Columns["pub_name"].HeaderText = "Publisher";
           
            dataGridView1.Columns["MRP"].Width = 100;
            dataGridView1.Columns["MRP"].HeaderText = "MRP";
            dataGridView1.Columns["PPrice"].Width = 100;
            dataGridView1.Columns["PPrice"].HeaderText = "P.Price";
            dataGridView1.Columns["Openingstock"].Width = 100;
            dataGridView1.Columns["Openingstock"].HeaderText = "Stock";
            dataGridView1.Columns["Pub_ID"].Visible = false;
            dataGridView1.Columns["Cls_ID"].Visible = false;
            dataGridView1.Columns["Discount"].Visible = false;

        }
        private void BindPublisher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblpublisher ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["pub_name"] = "Select";
            row["pub_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_Pub.DataSource = ds.Tables[0];
            cmb_Pub.DisplayMember = "pub_name";
            cmb_Pub.ValueMember = "pub_ID";

        }

        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID,CLS_DESC  from CLASS_T order by CLS_DESC ", sc);
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
        private void LoadGrid()
        {
                      
            string className,bookname,Publisher;
            if (cmb_Pub.Text == "" || cmb_Pub.Text == "Select" || cmb_Pub.Text == "System.Data.DataRowView")
            {
                Publisher = "%";
            }
            else
            {
                Publisher = cmb_Pub.SelectedValue.ToString();
            }
            if (cmb_class.Text == "" || cmb_class.Text == "ALL" || cmb_class.Text == "System.Data.DataRowView")
            {
                className = "%";
            }
            else
            {
                className = cmb_class.SelectedValue.ToString();
            }
            if (txtBook.Text == "" || txtBook.Text == "Select" || txtBook.Text == "System.Data.DataRowView")
            {
                bookname = "%";
            }
            else
            {
                bookname = txtBook.Text.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select itm_id,Groupid,itm_desc,cls_desc,pub_name,mrp,PPrice,Openingstock,Pub_id,Cls_id,Discount from View_ITEM_DETAIL where itm_desc like '" + bookname + "%' and CLS_ID LIKE '" + className + "%' and Pub_ID LIKE '" + Publisher + "%' ORDER BY itm_desc", sc);
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
            if (txtDiscount.Text == "")
                txtDiscount.Text = "0";
            if (txtOpnStock.Text == "")
                txtOpnStock.Text = "0";
            DateTime dt1 = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str= sc.DataSource.ToString();
            if (txtBook.Text == "")
            {
                MsgBox.ShowBox("Book / Item not be blank...");
                return;
            }
            else if (cmb_Group.Text == "" )
            {
                MsgBox.ShowBox("Pls Select Code....."); return;
            }
             else if (cmb_Pub.Text == "" || cmb_Pub.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Publisher....."); return;
            }
            else
            {
                SqlCommand com = new SqlCommand("insert_tblItem '" + txtBook.Text + "','" + cmb_Pub.SelectedValue + "','" + cmb_class.SelectedValue + "','" + txtMRP.Text + "','" + txtPprice.Text + "','" + txtOpnStock.Text + "','"+dt1.ToString("MM/dd/yyyy")+"','" + cmb_Group.Text.Trim().Replace(" ","") + "','" + txtDiscount.Text + "','" + txtsize.Text.ToUpper()+ "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Book / Item Created...");
                        Cancil_form();
                        LoadGrid(); return;
                    }
                    else
                    {
                        MsgBox.ShowBox("This Book / Item/ Barcode already exist"); return;
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
                txtBook.Text = dataGridView1.CurrentRow.Cells["itm_desc"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["itm_id"].Value.ToString();
                cmb_Pub.SelectedValue = dataGridView1.CurrentRow.Cells["PUB_ID"].Value.ToString();
                cmb_class.SelectedValue = dataGridView1.CurrentRow.Cells["CLS_ID"].Value.ToString();
                txtMRP.Text = dataGridView1.CurrentRow.Cells["MRP"].Value.ToString();
                txtPprice.Text = dataGridView1.CurrentRow.Cells["PPRICE"].Value.ToString();
                txtOpnStock.Text = dataGridView1.CurrentRow.Cells["OpeningStock"].Value.ToString();
                cmb_Group.Text = dataGridView1.CurrentRow.Cells["GroupId"].Value.ToString();
                txtDiscount.Text = dataGridView1.CurrentRow.Cells["Discount"].Value.ToString();
      //          txtsize.Text = dataGridView1.CurrentRow.Cells["SIZE"].Value.ToString();
       //         dateofadm.Text = dataGridView1.CurrentRow.Cells["openingdate"].Value.ToString();
                btnUpdate.Enabled=true;
                btn_delete.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "")
                txtDiscount.Text = "0";
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
           else if (cmb_Pub.Text == "" || cmb_Pub.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Publisher.....");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_tblItem '" + txtstateid.Text + "','" + txtBook.Text + "','" + cmb_Pub.SelectedValue + "','" + cmb_class.SelectedValue + "','" + txtMRP.Text + "','" + txtPprice.Text + "','" + txtOpnStock.Text + "','" + dt1.ToString("MM/dd/yyyy") + "','" + cmb_Group.Text.Trim().Replace(" ", "") + "','" + txtDiscount.Text + "','" + txtsize.Text.ToUpper() + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Book / Item Updated..."); 
                        Cancil_form();
                        LoadGrid();
                        return;
                    }
                    else
                    {
                        MsgBox.ShowBox("This Book / Item already exist");
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Book/Item!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand(" Delete_tblItem " + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Book / Item Deleted Successfully!!!");
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

  

       


        private void txtDiscount_Leave(object sender, EventArgs e)
        {
        //    try
        //    {
        //    decimal max = 0, pp = 0, disc = 0;
        //    if (txtDiscount.Text != "")
        //        disc = Convert.ToDecimal(txtDiscount.Text);
        //    if (txtMRP.Text != "")
        //        max = Convert.ToDecimal(txtMRP.Text);
        //        txtPprice.Text=(max-((max*disc)/100)).ToString();
        //    }
        //    catch(Exception ex)
        //{}
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void txtsize_TextChanged(object sender, EventArgs e)
        {

        }     
       
    }
}
