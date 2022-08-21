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
using System.Diagnostics;
using System.Collections;
using sms.DAL;


namespace sms
{
    public partial class frmbookgroup : Form
    {
        dataclass dd = new dataclass();
        SqlConnection sc;
        SqlDataAdapter da;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlTransaction tran;
        dataclass dc = new dataclass();
        DataSet ds;
        public string From;
        public string To;
        public string PayMode;
        public string partyName;
        public string serviceType;
        public string TotalSrTax;
        public string TotalNetAmount;
        public string TotalAmount;
            
        //string PayMode;
        bool DeleteButton = true;
        bool UpdateButton = true;

        string PayMode1;
        decimal Amount;
        string srtaxalowed;
        decimal unitMul;

        string Word;
         string billno;
         string jobtype;
         DateTime billdate;
         string consignor;
         string container;
         string invoiceno;
         string address;
         string unit;
         string size;
         string Billto;
         decimal total;
         decimal NetAmount;
        decimal ExChangeRate;

        public frmbookgroup()
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
            //cmbClass.SelectedValue = 0;
            txtDiscount.Text = "0";
            txtPPrice.Text = "0";
            txtPurName.Text = "";
            btn_save.Enabled = true;
            btn_Add.Enabled = true;
            btn_Delete.Enabled = true;
            dtpBillDate.Enabled = true;
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();
            sc.Open();
            com = new SqlCommand("delete from tmp_PURCHASE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();

            bindPurno();
           bindclass();
            initgrid();
            txtQty.Text = "0";
            txtDiscount.Text = "0";
            txtnetTotal.Text = "0";
            txtTotal.Text = "0";
            txtPPrice.Text = "0";
            cmbBookName.Text = "";
            cmbPublisher.Text = "";
            txtBarcode.Text = "";
            listBox1.Visible = false;

        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            
            if (txtPurName.Text == "")
            {
                MsgBox.ShowBox("Please Enter Set Name.......");
                return;
            }
            if (dataGridView1.Rows.Count > 0)
            {
                SqlDataReader dr;
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlCommand com = new SqlCommand();


                sc.Open();
                com = new SqlCommand("delete from Book_Group where PURD_PUR_ID='" + txtPurNo.Text + "' ", sc);
                com.ExecuteNonQuery();
                sc.Close();
                
                
                sc = dc.con();
                sc.Open();
                tran = sc.BeginTransaction();
                try
                {

                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_insert_bookgroup";
                        cmd.Connection = sc;
                        cmd.Parameters.AddWithValue("@PURD_PUR_ID", txtPurNo.Text);
                        cmd.Parameters.AddWithValue("@PURD_ITEM_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["item"].Value));
                        cmd.Parameters.AddWithValue("@PURD_RATE", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_RATE"].Value));
                        cmd.Parameters.AddWithValue("@PURD_QUANTITY", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_QUANTITY"].Value));
                        cmd.Parameters.AddWithValue("@PUR_DISCOUNT", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_DISCOUNT"].Value));
                        cmd.Parameters.AddWithValue("@PURD_AMOUNT", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_AMOUNT"].Value));
                        cmd.Parameters.AddWithValue("@PURD_CLS_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["class"].Value));
                        cmd.Parameters.AddWithValue("@PURD_Pub_Id", Convert.ToInt32(dataGridView1.Rows[z].Cells["Publisher"].Value));
                        cmd.Parameters.AddWithValue("@PURD_PUR_NAME", txtPurName.Text);
                        
                        //cmd.Parameters.AddWithValue("@ExchangeRate", Convert.ToDecimal(dataGridView1.Rows[z].Cells["ExchangeRate"].Value));
                        //cmd.Parameters.AddWithValue("@userid", Form1.GlobalLoginUserName.ToString());
                        //cmd.Parameters.AddWithValue("@TerminalId", sc.WorkstationId.ToString());
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                    }



                    tran.Commit();
                    sc.Close();
                    Cancil_form();
                    dataGridView1.DataSource = DBNull.Value;
                    MsgBox.ShowBox("Book Set Created Successfully.....");
                    initgrid();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    sc.Close();
                    MessageBox.Show(ex.Message, "Warning....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                { }
            }
            else
            {
                MessageBox.Show("No Record To Save .......");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void initgrid()
        {
            BindGrid();
            dataGridView1.Columns["PURD_PUR_ID"].Visible = false;
            dataGridView1.Columns["publisher"].Visible = false;
            dataGridView1.Columns["class"].Visible = false;
            dataGridView1.Columns["ITEM"].Visible = false;
            dataGridView1.Columns["PURD_Barcode"].Width = 80;
            dataGridView1.Columns["PURD_Barcode"].HeaderText = "Barcode";
            dataGridView1.Columns["PURD_ITEM"].Width = 200;
            dataGridView1.Columns["PURD_ITEM"].HeaderText = "Book/Item Name";
            dataGridView1.Columns["PURD_CLS"].Width = 100;
            dataGridView1.Columns["PURD_CLS"].HeaderText = "Class";
            dataGridView1.Columns["PURD_ITEM_PUB"].Width = 180;
            dataGridView1.Columns["PURD_ITEM_PUB"].HeaderText = "Publisher";
            
            dataGridView1.Columns["PURD_RATE"].Width = 80;
            dataGridView1.Columns["PURD_RATE"].HeaderText = "Rate";
            dataGridView1.Columns["PURD_QUANTITY"].Width = 80;
            dataGridView1.Columns["PURD_QUANTITY"].HeaderText = "Quantity";
            dataGridView1.Columns["PURD_AMOUNT"].Width = 80;
            dataGridView1.Columns["PURD_AMOUNT"].HeaderText = "Amount";
            dataGridView1.Columns["PURD_DISCOUNT"].Width = 80;
            dataGridView1.Columns["PURD_DISCOUNT"].HeaderText = "Discount";
            dataGridView1.Columns["PURD_net_total"].Width = 80;
            dataGridView1.Columns["PURD_net_total"].HeaderText = "Net Total";
            
        }

        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID,CLS_DESC  from CLASS_T order by CLS_DESC ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "CLS_DESC";
            cmbClass.ValueMember = "CLS_ID";
        }
        
        //private void BindBookName()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    SqlDataAdapter da = new SqlDataAdapter(" select itm_id,itm_desc from tblitem where cls_id in (" + cmbClass.SelectedValue + ",0) order by itm_desc", sc);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    DataRow row = ds.Tables[0].NewRow();
        //    row["itm_desc"] = "Select";
        //    row["itm_ID"] = 0;
        //    ds.Tables[0].Rows.InsertAt(row, 0);

        //    cmbBookName.DataSource = ds.Tables[0];
        //    cmbBookName.DisplayMember = "itm_desc";
        //    cmbBookName.ValueMember = "itm_ID";
        //}
        //private void BindPublisher()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    SqlDataAdapter da = new SqlDataAdapter(" select * from view_publisher where cls_id in (" + cmbClass.SelectedValue + ",0) and itm_id='" + cmbBookName.SelectedValue + "'", sc);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    DataRow row = ds.Tables[0].NewRow();
        //    row["pub_name"] = "Select";
        //    row["pub_ID"] = 0;
        //    ds.Tables[0].Rows.InsertAt(row, 0);

        //    cmbPublisher.DataSource = ds.Tables[0];
        //    cmbPublisher.DisplayMember = "pub_name";
        //    cmbPublisher.ValueMember = "pub_ID";
        //}

       
      
        private void BindGrid()
        {
            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tmp_PURCHASE_DETAIL_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
          
        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (cmbBookName.Text == "")
            {
                MsgBox.ShowBox("Please Select Book.......");
                return;
            }
            if (txtQty.Text == "")
            {
                MsgBox.ShowBox("Please Select Qty.......");
                return;
            }
            if (txtnetTotal.Text != "" || txtnetTotal.Text == "0")
            {
                try
                {
                    decimal discount = 0;
                    txtTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();
                    discount = ((Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100);
                    txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - ((Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100)).ToString();
                    SqlDataReader dr;
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    SqlCommand com = new SqlCommand();


                    sc.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "insert_tmp_PURCHASE_DETAIL_T";
                    com.Connection = sc;
                    com.Parameters.AddWithValue("@PURD_PUR_ID", txtPurNo.Text);
                    //com.Parameters.AddWithValue("@billdate", Convert.ToDateTime(dtpBillDate.Value));
                    com.Parameters.AddWithValue("@PURD_ITEM", cmbBookName.Text);
                    com.Parameters.AddWithValue("@PURD_CLS", cmbClass.Text);
                    com.Parameters.AddWithValue("@PURD_ITEM_PUB", cmbPublisher.Text);
                    com.Parameters.AddWithValue("@PURD_Barcode", txtBarcode.Text);
                    com.Parameters.AddWithValue("@PURD_RATE", Convert.ToDecimal(txtPPrice.Text));
                    com.Parameters.AddWithValue("@PURD_QUANTITY", txtQty.Text);
                    com.Parameters.AddWithValue("@PURD_AMOUNT", Convert.ToDecimal(txtTotal.Text));
                    com.Parameters.AddWithValue("@PURD_DISCOUNT", Convert.ToDecimal(discount));
                    com.Parameters.AddWithValue("@PURD_net_total", Convert.ToDecimal(txtnetTotal.Text));
                    com.Parameters.AddWithValue("@pub", Convert.ToInt32(cmbPublisher.SelectedValue));
                    com.Parameters.AddWithValue("@class", Convert.ToInt32(cmbClass.SelectedValue));
                    com.Parameters.AddWithValue("@item", Convert.ToInt32(txtid.Text));
                    dr = com.ExecuteReader();
                    sc.Close();

                    initgrid();

                   // cmbClass.SelectedValue = 0;
                    txtQty.Text = "0";
                    txtDiscount.Text = "0";
                    txtnetTotal.Text = "0";
                    txtTotal.Text = "0";
                    txtPPrice.Text = "0";
                    cmbBookName.Text = "";
                    cmbPublisher.Text = "";
                    txtBarcode.Text = "";
                    listBox1.Visible = false;
                }
                catch (Exception ex) {
                    MsgBox.ShowBox(ex.ToString());

                }
            }
            else
            {
                MsgBox.ShowBox("Pls Select Book/Item...");

            }
        }
       
     
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
           
        }

        
        private void cmbClass_Leave(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }

       

        private void frmbookgroup_Load(object sender, EventArgs e)
        {
            bindclass();
        }
        private void bindPurno()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("bind_BGno", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                txtPurNo.Text = dt.Rows[0][0].ToString();


        }

       

        private void txtPPrice_Leave(object sender, EventArgs e)
        {
            if(txtQty.Text=="")
            txtQty.Text="1";
            txtTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();
            txtnetTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (txtDiscount.Text == "")
                txtDiscount.Text = "0";
            txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtDiscount.Text)).ToString();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (txtid.Text == "")
                {
                    MsgBox.ShowBox("Please Click Record.......");
                    return;
                }
                else
                {
                    try
                    {
                        SqlDataReader dr;
                        dataclass dd = new dataclass();
                        SqlConnection sc = dd.con();
                        SqlCommand com = new SqlCommand();


                        sc.Open();
                        com = new SqlCommand("delete from tmp_PURCHASE_DETAIL_T where PURD_PUR_ID='" + txtPurNo.Text + "' and ITEM='" + txtid.Text + "'", sc);
                        com.ExecuteNonQuery();
                        sc.Close();
                        initgrid();
                        txtid.Text = "";
                        txtQty.Text = "0";
                        txtDiscount.Text = "0";
                        txtnetTotal.Text = "0";
                        txtTotal.Text = "0";
                        txtPPrice.Text = "0";
                        cmbBookName.Text = "";
                        cmbPublisher.Text = "";
                        txtBarcode.Text = "";
                        listBox1.Visible = false;
                    }
                    catch (Exception ex) {
                        MsgBox.ShowBox(ex.ToString());
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["ITEM"].Value.ToString();
                cmbClass.SelectedValue = dataGridView1.CurrentRow.Cells["class"].Value.ToString();
                cmbBookName.SelectedValue = dataGridView1.CurrentRow.Cells["ITEM"].Value.ToString();
                cmbPublisher.SelectedValue = dataGridView1.CurrentRow.Cells["publisher"].Value.ToString();
                txtQty.Text = dataGridView1.CurrentRow.Cells["PURD_QUANTITY"].Value.ToString();
                txtPPrice.Text = dataGridView1.CurrentRow.Cells["PURD_RATE"].Value.ToString();
                txtTotal.Text = dataGridView1.CurrentRow.Cells["PURD_AMOUNT"].Value.ToString();
                txtDiscount.Text = dataGridView1.CurrentRow.Cells["PURD_DISCOUNT"].Value.ToString();
                txtnetTotal.Text = dataGridView1.CurrentRow.Cells["PURD_net_total"].Value.ToString();
                btn_Add.Enabled = false;
               
            }
        }

       

        private void cmbBookName_Leave(object sender, EventArgs e)
        {
            listBox1.Focus();
            //BindPublisher();
           // Bindbdetail();
        }
        private void Bindbdetail()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select MRP,ITM_ID,Discount,pub_id from tblitem where cls_id in(" + cmbClass.SelectedValue + ",0) and itm_id='" + cmbBookName.SelectedValue + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPPrice.Text = dt.Rows[0][0].ToString();
                txtid.Text = dt.Rows[0][1].ToString();
                txtDiscount.Text = dt.Rows[0][2].ToString();
                if (txtDiscount.Text == "")
                    txtDiscount.Text = "0";
                cmbPublisher.SelectedValue = dt.Rows[0][3].ToString();
                txtQty.Text = "1";
                txtTotal.Text = dt.Rows[0][0].ToString();
                txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtDiscount.Text)).ToString();
            }
            
        }

        private void cmbPublisher_Leave(object sender, EventArgs e)
        {
            Bindbdetail();
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if  (txtQty.Text != "")
            {
            txtTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();
            txtnetTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();

            }

        }


        private void txtPurNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPurNo.Text != "")
                {
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    SqlDataReader dr;
                    SqlDataAdapter da = new SqlDataAdapter("select purd_Pur_Id,purd_Pur_NAME from Book_Group where purd_Pur_Id='" + txtPurNo.Text.Replace("'", "''") + "'  order by purd_Pur_Id", sc);
                   

                  
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ArrayList arr = new ArrayList();
                    if (dt.Rows.Count>0)
                    {
                        txtPurName.Text = dt.Rows[0][1].ToString();
                        sc.Close();
                        arr.Add(txtPurNo.Text);
                      //  arr.Add("%");
                        int i = dd.InsertRows("sp_LoadTemp_BGroupGrid", ref arr, true);
                        initgrid();



                        dtpBillDate.Enabled = false;
                       
                        
                   

                    }
                    else
                    {                  
                     Cancil_form();
                    }
                    
                }
            }
            catch (Exception EX)
            {
                MsgBox.ShowBox(EX.ToString());

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtPurNo.Text == "")
            {
                MsgBox.ShowBox("Please Click Record.......");
                return;
            }
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from book_Group where PURD_PUR_ID = '"+ txtPurNo.Text +"'", sc);
            com.ExecuteNonQuery();
            sc.Close();
            initgrid();
            Cancil_form();

        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    SqlDataAdapter da = new SqlDataAdapter("select * from tblitem where groupid like '" + txtBarcode.Text.Trim().Replace(" ", "") + "'", sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cmbClass.SelectedValue = dt.Rows[0]["CLS_ID"].ToString();
                        cmbBookName.SelectedValue = dt.Rows[0]["ITM_ID"].ToString();
                        cmbPublisher.SelectedValue = dt.Rows[0]["PUB_ID"].ToString();
                        txtPPrice.Text = dt.Rows[0]["MRP"].ToString();
                        txtid.Text = dt.Rows[0]["ITM_ID"].ToString();
                        txtDiscount.Text = dt.Rows[0]["discount"].ToString();
                        txtQty.Focus();
                    }
                    else
                    {
                        cmbClass.SelectedValue = 0;
                        txtQty.Text="";
                        cmbBookName.SelectedValue = 0;
                       
                        cmbPublisher.SelectedValue = 0;
                        txtPPrice.Text = "0";
                        txtid.Text = ""; txtDiscount.Text="0";
                    }
                }
            }
            catch (Exception EX)
            {
                MsgBox.ShowBox(EX.ToString());

            }
        }

        
        private void cmbBookName_TextChanged(object sender, EventArgs e)
        {
            {

                string bookname, className ;

                //if (cmbBookName.Text != "")
                //{
                   
                //    bookname = cmbBookName.Text.Trim().ToString();
                //}
                if (cmbClass.Text == "Select" || cmbClass.Text == "" || cmbClass.Text == "System.Data.DataRowView")
                {
                    className = "%";
                }
                else
                {
                    className = cmbClass.SelectedValue.ToString();
                }
                //if (txtBarcode.Text == "")
                //{
                //    bookcode = "%";
                //}
                //else
                //{
                //    bookcode = txtBarcode.Text.ToString();
                //}
                
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select itm_id,Groupid,itm_desc,pub_name,cls_desc from View_ITEM_DETAIL where itm_desc like '" + cmbBookName.Text + "%' and CLS_ID LIKE '" + className + "%'  ORDER BY itm_desc ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '|' + ds1.Tables[0].Rows[j][1].ToString() + '|' + ds1.Tables[0].Rows[j][2].ToString() + '|' + ds1.Tables[0].Rows[j][3].ToString() + '|' + ds1.Tables[0].Rows[j][4].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = cmbBookName.Top + cmbBookName.Height;
                    //listBox1.Width = txtstuno.Width;
                    listBox1.Left = cmbBookName.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }
        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    txtid.Text = str[0].ToString();
                    listBox1.Visible = false;
                    bindstuadmleave();

                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
        }
        private void bindstuadmleave()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_item_detail where  ITM_ID like '" + txtid.Text.Trim().Replace(" ", "") + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                //cmbClass.Enabled = false;
                //cmb_rollno.Enabled = false;
                cmbBookName.Text = dt.Rows[0]["ITM_DESC"].ToString();
                txtBarcode.Text = dt.Rows[0]["groupid"].ToString();
                cmbClass.SelectedValue = dt.Rows[0]["CLS_ID"].ToString();
                cmbPublisher.Text = dt.Rows[0]["Pub_Name"].ToString();
                txtPPrice.Text = dt.Rows[0]["MRP"].ToString();
                txtQty.Text = "1";
                listBox1.Visible = false;
               
                txtQty.Focus();

                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;

            }
            else
            {
                // MsgBox.ShowBox("This Adm No does not exit !!!");
                cmbClass.SelectedValue = 0;
                cmbPublisher.SelectedValue = 0;
                cmbBookName.Text = "";
                txtBarcode.Text = "";
                txtQty.Text = "";
                txtPPrice.Text = "";
               
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            {

                string bookname, className, bookcode, Publisher;

                //if (cmbBookName.Text == "-Select-" || cmbBookName.Text == "" || cmbBookName.Text == "System.Data.DataRowView")
                //{
                //    bookname = "%";
                //}
                //else
                //{
                //    bookname = cmbBookName.Text.Trim().ToString();
                //}
                if (cmbClass.Text == "Select" || cmbClass.Text == "" || cmbClass.Text == "System.Data.DataRowView")
                {
                    className = "%";
                }
                else
                {
                    className = cmbClass.SelectedValue.ToString();
                }
                if (txtBarcode.Text != "")
                {
                   
                    bookcode = txtBarcode.Text.ToString();
                }
                //if (cmbPublisher.Text == "" || cmbPublisher.Text == "Select" || cmbPublisher.Text == "System.Data.DataRowView")
                //{
                //    Publisher = "%";
                //}
                //else
                //{
                //    Publisher = cmbPublisher.SelectedValue.ToString();
                //}
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select itm_id,Groupid,itm_desc,pub_name,cls_desc from View_ITEM_DETAIL where  groupid like '" + txtBarcode.Text + "%'  and CLS_ID LIKE '" + className + "%'  ORDER BY itm_desc ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '|' + ds1.Tables[0].Rows[j][1].ToString() + '|' + ds1.Tables[0].Rows[j][2].ToString() + '|' + ds1.Tables[0].Rows[j][3].ToString() + '|' + ds1.Tables[0].Rows[j][4].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = txtBarcode.Top + txtBarcode.Height;
                    //listBox1.Width = txtstuno.Width;
                    listBox1.Left = txtBarcode.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            listBox1.Focus();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    txtid.Text = str[0].ToString();
                    listBox1.Visible = false;
                    bindstuadmleave();

                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

      
      }
}
