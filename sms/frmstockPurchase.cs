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
    public partial class frmstockPurchase : Form
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
        public string GroupNo;
       
            
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

        public frmstockPurchase()
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
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();
            sc.Open();
            com = new SqlCommand("delete from tmp_stock_PURCHASE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            dtpBillDate.Enabled = true;
            txtBillNo.Enabled = true;
            txtAgainst.Enabled = true;
            cmbVenCode.Enabled = true;
            btn_Add.Enabled = true;
            btn_Delete.Enabled = true;
            txtTotalAmount.Enabled = true;
            txtGDiscount.Enabled = true;
            txtGTotalAmt.Enabled = true;
            btn_save.Enabled = true;
            btnDelete.Enabled = false;
            txtDiscount.Text = "0";
            txtPPrice.Text = "0";
            txtTotalAmount.Text = "0";
            txtGDiscount.Text = "0";
            txtGTotalAmt.Text = "0";
            txtTotal.Text = "0";
            txtnetTotal.Text = "";
            txtPPrice.Text = "0";
            txtAgainst.Text = "";
            txtBillNo.Text = "";
            cmbVenCode.SelectedValue = "0";
            txtQty.Text = "";
            txtBarcode.Text="";
            bindPurno();
            BindVendor();
            BindClass();
            initgrid();


        }
       

        private void btn_save_Click(object sender, EventArgs e)
        {
           
            sc = dc.con();
            sc.Open();
            tran = sc.BeginTransaction();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insert_stocktblpurchase";
                cmd.Connection = sc;
                cmd.Parameters.AddWithValue("@PUR_NO", Convert.ToInt32(txtPurNo.Text));
                cmd.Parameters.AddWithValue("@PUR_BILL_NO", (txtBillNo.Text));
                cmd.Parameters.AddWithValue("@PUR_DATE", Convert.ToDateTime(dtpBillDate.Value, System.Globalization.CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@PUR_AMOUNT", Convert.ToDecimal(txtTotalAmount.Text));
                cmd.Parameters.AddWithValue("@PUR_DISCOUNT", Convert.ToDecimal(txtGDiscount.Text));
                cmd.Parameters.AddWithValue("@PUR_VCR_ID", Convert.ToInt32(cmbVenCode.SelectedValue));
                cmd.Parameters.AddWithValue("@PUR_NARRATION", txtAgainst.Text);
                //cmd.Parameters.AddWithValue("@userid", Form1.GlobalLoginUserName.ToString());
                //cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                //cmd.Parameters.AddWithValue("@TerminalId", sc.WorkstationId.ToString());
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

                for (int z = 0; z < dataGridView1.Rows.Count; z++)
                {
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_insert_tblstockpurchaseDetail";
                    cmd.Connection = sc;
                    cmd.Parameters.AddWithValue("@PURD_PUR_ID", Convert.ToInt32(txtPurNo.Text));
                    cmd.Parameters.AddWithValue("@PURD_ITEM_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["item"].Value));
                    cmd.Parameters.AddWithValue("@PURD_RATE", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_RATE"].Value));
                    cmd.Parameters.AddWithValue("@PURD_QUANTITY", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_QUANTITY"].Value));
                    cmd.Parameters.AddWithValue("@PUR_DISCOUNT", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_DISCOUNT"].Value));
                    cmd.Parameters.AddWithValue("@PURD_AMOUNT", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_AMOUNT"].Value));
                    cmd.Parameters.AddWithValue("@PURD_CLS_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["class"].Value));
                    cmd.Parameters.AddWithValue("@PURD_Pub_Id", Convert.ToInt32(dataGridView1.Rows[z].Cells["Publisher"].Value));
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
                MsgBox.ShowBox("Bill Created Successfully.....");
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
            dataGridView1.Columns["PURD_CLS"].HeaderText = "Dept";
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
    
        private void BindClass()
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

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "DEPT_DESC";
            cmbClass.ValueMember = "DEPT_ID";
        }
        
        private void BindBookName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select item_id,item_name from ItemMaster_T where (depart_name= '" + cmbClass.SelectedValue + "' or depart_name=0) order by item_name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["item_name"] = "Select";
            row["item_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbBookName.DataSource = ds.Tables[0];
            cmbBookName.DisplayMember = "item_name";
            cmbBookName.ValueMember = "item_id";
        }
        private void BindPublisher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblpublisher ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();            
            cmbPublisher.DataSource = ds.Tables[0];
            cmbPublisher.DisplayMember = "pub_name";
            cmbPublisher.ValueMember = "pub_ID";
        }

        private void BindVendor()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TBLVENDOR_DETAIL", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["vendorName"] = "Select";
            row["vendor_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbVenCode.DataSource = ds.Tables[0];
            cmbVenCode.DisplayMember = "vendorName";
            cmbVenCode.ValueMember = "vendor_id";
        }
     
      
        private void BindGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tmp_stock_PURCHASE_DETAIL_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
          
        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                decimal discount = 0;
                txtTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();
                discount = ((Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100);
                txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - ((Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100)).ToString();
                btn_save.Enabled = true;
                SqlDataReader dr;
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlCommand com = new SqlCommand();

                sc.Open();
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "insert_tmp_stockPURCHASE_DETAIL_T";
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
                com = new SqlCommand("select sum(PURD_AMOUNT-PURD_DISCOUNT) as gnettotal from tmp_stock_PURCHASE_DETAIL_T ", sc);
                sc.Open();
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    txtTotalAmount.Text = dr["gnettotal"].ToString();
                    txtGDiscount.Text = "0";
                    txtGTotalAmt.Text = dr["gnettotal"].ToString();
                    sc.Close();
                }
                cmbClass.SelectedValue = 0;
                cmbPublisher.SelectedValue = 0;
                cmbBookName.SelectedValue = 0;
                txtQty.Text = "0";
                txtDiscount.Text = "0";
                txtnetTotal.Text = "0";
                txtTotal.Text = "0";
                txtPPrice.Text = "0";


            }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.ToString());

            }
        }
       
     
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        
        private void cmbClass_Leave(object sender, EventArgs e)
        {
            BindBookName();
        }

       

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from tmp_stock_PURCHASE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            
            bindPurno();
            BindVendor();
            BindClass();
            initgrid();
            BindPublisher();
        }
        private void bindPurno()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_stockPurNo ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                txtPurNo.Text = dt.Rows[0][0].ToString();


        }

       

        private void txtPPrice_Leave(object sender, EventArgs e)
        {
            if (txtQty.Text != "")
            {
                txtTotal.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPPrice.Text)).ToString();
            }
           
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (txtDiscount.Text != "")
            {
                txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - ((Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text)) / 100)).ToString();
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MsgBox.ShowBox("Please Click Record.......");
                return;
            }
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from tmp_stock_PURCHASE_DETAIL_T where item='" + txtid.Text + "' and PURD_PUR_ID='" + txtPurNo.Text + "'", sc);
            com.ExecuteNonQuery();
            sc.Close();
            initgrid();
            com = new SqlCommand("select sum(PURD_AMOUNT) as gamount ,sum(PURD_DISCOUNT) as gdiscount,sum(PURD_AMOUNT-PURD_DISCOUNT) as gnettotal from tmp_stock_PURCHASE_DETAIL_T ", sc);
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtTotalAmount.Text = dr["gamount"].ToString();
                txtGDiscount.Text = dr["gdiscount"].ToString();
                txtGTotalAmt.Text = dr["gnettotal"].ToString();
                sc.Close();
            }
            cmbClass.SelectedValue = 0;
            cmbPublisher.SelectedValue = 0;
            cmbBookName.SelectedValue = 0;
            txtQty.Text = "0";
            txtDiscount.Text = "0";
            txtnetTotal.Text = "0";
            txtTotal.Text = "0";
            txtPPrice.Text = "0";
 
            //Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            btn_save.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["item"].Value.ToString();
                cmbClass.Text = dataGridView1.CurrentRow.Cells["PURD_CLS"].Value.ToString();
                cmbBookName.Text = dataGridView1.CurrentRow.Cells["PURD_ITEM"].Value.ToString();
                cmbPublisher.Text = dataGridView1.CurrentRow.Cells["PURD_ITEM_PUB"].Value.ToString();
                txtQty.Text = dataGridView1.CurrentRow.Cells["PURD_QUANTITY"].Value.ToString();
                txtPPrice.Text = dataGridView1.CurrentRow.Cells["PURD_RATE"].Value.ToString();
                txtTotal.Text = dataGridView1.CurrentRow.Cells["PURD_AMOUNT"].Value.ToString();
                txtDiscount.Text = dataGridView1.CurrentRow.Cells["PURD_DISCOUNT"].Value.ToString();
                txtnetTotal.Text = dataGridView1.CurrentRow.Cells["PURD_net_total"].Value.ToString();
               
            }
        }

       

        private void cmbBookName_Leave(object sender, EventArgs e)
        {
            BindPublisher();
            Bindbdetail();
        }
        private void Bindbdetail()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select mrp,ITeM_ID from ItemMaster_T where depart_name='" + cmbClass.SelectedValue + "' and ITeM_ID='" + cmbBookName.SelectedValue + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPPrice.Text = dt.Rows[0][0].ToString();
                txtid.Text = dt.Rows[0][1].ToString();
                txtQty.Text = "1";
            }
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            ////DataRow row = ds.Tables[0].NewRow();
            ////row["pub_name"] = "Select";
            ////row["itm_ID"] = 0;
            ////ds.Tables[0].Rows.InsertAt(row, 0);

            //txtPPrice.Text = dt.Rows[0][0].ToString();
            //cmbPublisher.DisplayMember = "pub_name";
            //cmbPublisher.ValueMember = "itm_ID";
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
            }
            //txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtDiscount.Text)).ToString();


        }
        private bool CheckReceiptNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool retno = true;
            cmd = new SqlCommand("select * from  tblpurchase where pur_id=" + txtPurNo.Text + " ", sc);
            sc.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                retno = true;
            }
            else
            {
                retno = false;
            }
            return retno;

        }


        private void txtPurNo_Leave(object sender, EventArgs e)
        {
            if (txtPurNo.Text != "")
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select * from stock_Purchase_T where pur_no='" + txtPurNo.Text.Replace("'", "''") + "'  order by pur_no", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    txtTotalAmount.Text = dr["pur_amount"].ToString();
                    txtGDiscount.Text = dr["pur_discount"].ToString();
                    sc.Close();
                    arr.Add(txtPurNo.Text);
                    int i = dd.InsertRows("sp_LoadTemp_stockPurchaseGrid", ref arr, true);
                    initgrid();
                   
                       
                        txtGTotalAmt.Text = (Convert.ToDecimal(txtTotalAmount.Text) - ((Convert.ToDecimal(txtTotalAmount.Text)) * Convert.ToDecimal(txtGDiscount.Text)) / 100).ToString();


                        SqlCommand com1 = new SqlCommand("Select * from stock_Purchase_T where pur_no='" + txtPurNo.Text.Replace("'", "''") + "'", sc);
                    sc.Open();
                    dr = com1.ExecuteReader();
                    if (dr.Read())
                    {
                        txtAgainst.Text = (dr["Pur_Narration"].ToString());
                        txtBillNo.Text = (dr["Pur_Bill_No"].ToString());
                        dtpBillDate.Text = dr["PUR_Date"].ToString();
                        cmbVenCode.SelectedValue = dr["Pur_Vcr_Id"].ToString();
                        sc.Close();
                    }
                  
                    dtpBillDate.Enabled = false;
                    txtBillNo.Enabled = false;
                    txtAgainst.Enabled = false;
                    cmbVenCode.Enabled = false;
                    btn_Add.Enabled = false;
                    btn_Delete.Enabled = false;
                   
                    txtTotalAmount.Enabled = false;
                    txtGDiscount.Enabled = false;
                    txtGTotalAmt.Enabled = false;
                    btn_save.Enabled = false;
                    btnDelete.Enabled = true;
                    
                }
                else
                {
                    Cancil_form();
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtPurNo.Text == "")
            {
                MsgBox.ShowBox("Please Enter Purchase Number !!!");
                return;
            }
            if (txtPurNo.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure Purchase Number - " + txtPurNo.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("exec sp_DeletestockPurchase '" + txtPurNo.Text + "'", sc);
                    sc.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    sc.Close();
                    MsgBox.ShowBox(" Purchase Deleted !!!");
                    Cancil_form();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                cryPurchaseReport.StuId = txtPurNo.Text.Trim();
                cryPurchaseReport obj = new cryPurchaseReport();
                obj.ShowDialog();
            }
            else
            {
                MsgBox.ShowBox(" No Record To Print!!!");

            }
            
        }


        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            {
                try
                {
                    if (e.KeyChar == 13)
                    {
                        dataclass dd = new dataclass();
                        SqlConnection sc = dd.con();
                        SqlDataAdapter da = new SqlDataAdapter("select * from ItemMaster_T where barcode_no like '" + txtBarcode.Text.Trim().Replace(" ", "") + "'", sc);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            cmbClass.SelectedValue = dt.Rows[0]["depart_name"].ToString();
                            BindBookName();
                            cmbBookName.SelectedValue = dt.Rows[0]["ITeM_ID"].ToString();
                            //BindPublisher();
                            //cmbPublisher.SelectedValue = dt.Rows[0]["PUB_ID"].ToString();
                            txtPPrice.Text = dt.Rows[0]["MRP"].ToString();
                            //txtDiscount.Text = dt.Rows[0]["discount"].ToString();
                            txtid.Text = dt.Rows[0]["ITeM_ID"].ToString();
                            txtQty.Text = "1";
                            txtQty.Focus();
                        }
                        else
                        {
                            BindBookName();
                            cmbBookName.SelectedValue = 0;
                            BindPublisher();
                            cmbPublisher.SelectedValue = 0;
                            txtPPrice.Text = "0";
                            txtDiscount.Text = "0";
                            txtid.Text = ""; txtQty.Text = "0";
                        }
                    }
                }
                catch (Exception EX)
                {
                    MsgBox.ShowBox(EX.ToString());

                }
            }

        }

        private void txtGDiscount_Leave(object sender, EventArgs e)
        {
            decimal paid = 0, desc = 0; 
            decimal grandtotal = 0;
            if (txtGDiscount.Text != "")
                desc = Convert.ToDecimal(txtGDiscount.Text);
              if (txtGTotalAmt.Text != "")
                grandtotal = Convert.ToDecimal(txtGTotalAmt.Text);
            if (desc > 100)
            {
                MsgBox.ShowBox("Discount Will be  less or equal 100% .....");
               
            }
            else
                balance();
        }
        private void balance()
        {
            try
            {
                decimal total, grandtotal, discount = 0;
                total = 0; grandtotal = 0; discount = 0; 
                if (txtTotalAmount.Text != "")
                    total = Convert.ToDecimal(txtTotalAmount.Text);
                if (txtGDiscount.Text != "")
                    discount = Convert.ToDecimal(txtGDiscount.Text);
                  grandtotal = total - ((total * discount) / 100);
                txtGTotalAmt.Text = grandtotal.ToString();

            }
            catch (Exception ex)
            { ex.ToString(); }
        }

        private void cmbVenCode_Leave(object sender, EventArgs e)
        {
           

        }

      }
}

 