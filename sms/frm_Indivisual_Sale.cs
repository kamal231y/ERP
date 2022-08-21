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
    public partial class frm_Indivisual_Sale : Form
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

        public frm_Indivisual_Sale()
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
            
        }

        private void Cancil_form()
        {
            //cmbClass.SelectedValue = 0;
            txtDiscount.Text = "0";
            txtPPrice.Text = "0";

            txtTotalAmount.Text = "0";
            txtGDiscount.Text = "0";
            txtGTotalAmt.Text = "0";
            txtTotal.Text = "0";
            txtnetTotal.Text = "";
            //txtGTotalAmt.Text = "0";
            txtPPrice.Text = "0";
            txtAgainst.Text = "";
            txtBillNo.Text = "";
            cmbVenCode.SelectedValue = "0";
            txtQty.Text = "";
            //// dataGridView1.DataSource = DBNull.Value; 
            BindClass();


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
                cmd.CommandText = "sp_insert_tblpurchase";
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
                    cmd.CommandText = "sp_insert_tblpurchaseDetail";
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
    
        private void BindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }
        
        private void BindBookName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select itm_id,itm_desc from tblitem where cls_id= '"+cmbClass.SelectedValue+"'order by itm_desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["itm_desc"] = "Select";
            row["itm_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbBookName.DataSource = ds.Tables[0];
            cmbBookName.DisplayMember = "itm_desc";
            cmbBookName.ValueMember = "itm_ID";
        }
        private void BindPublisher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from view_publisher where cls_id='" + cmbClass.SelectedValue + "' and itm_id='" + cmbBookName.SelectedValue + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["pub_name"] = "Select";
            row["itm_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbPublisher.DataSource = ds.Tables[0];
            cmbPublisher.DisplayMember = "pub_name";
            cmbPublisher.ValueMember = "itm_ID";
        }

        private void BindVendor()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblledger", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbVenCode.DataSource = ds.Tables[0];
            cmbVenCode.DisplayMember = "Name";
            cmbVenCode.ValueMember = "id";
        }
     
      
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
            btn_save.Enabled = true;
            //if (cmbCurrency.Text == "-Select-")
            //{
            //    MsgBox.ShowBox("Please Select Currency.......");
            //    return;
            //}
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            //sc.Open();
            //com = new SqlCommand("delete from tmp_PURCHASE_DETAIL_T where UserId='"+Form1.GlobalLoginUserName+"'",sc);
            //com.ExecuteNonQuery();
            //sc.Close();


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
            com.Parameters.AddWithValue("@PURD_DISCOUNT", Convert.ToDecimal(txtDiscount.Text));
            com.Parameters.AddWithValue("@PURD_net_total", Convert.ToDecimal(txtnetTotal.Text));
            com.Parameters.AddWithValue("@pub", Convert.ToInt32(cmbPublisher.SelectedValue));
            com.Parameters.AddWithValue("@class", Convert.ToInt32(cmbClass.SelectedValue));
            com.Parameters.AddWithValue("@item",Convert.ToInt32(txtid.Text));
            dr = com.ExecuteReader();
            sc.Close();

            initgrid();
            com = new SqlCommand("select sum(PURD_AMOUNT) as gamount ,sum(PURD_DISCOUNT) as gdiscount,sum(PURD_AMOUNT-PURD_DISCOUNT) as gnettotal from tmp_PURCHASE_DETAIL_T ", sc);
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
            txtQty.Text ="0";
            txtDiscount.Text = "0";
            txtnetTotal.Text = "0";
            txtTotal.Text = "0";
            txtPPrice.Text = "0";
            
            
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
            com = new SqlCommand("delete from tmp_PURCHASE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            
            bindPurno();
            BindVendor();
            BindClass();
            initgrid();
        }
        private void bindPurno()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_PurNo ", sc);
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
            txtnetTotal.Text = (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtDiscount.Text)).ToString();
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
            com = new SqlCommand("delete from tmp_PURCHASE_DETAIL_T where item='" + txtid.Text + "' and PURD_PUR_ID='" + txtPurNo.Text + "'", sc);
            com.ExecuteNonQuery();
            sc.Close();
            initgrid();
            com = new SqlCommand("select sum(PURD_AMOUNT) as gamount ,sum(PURD_DISCOUNT) as gdiscount,sum(PURD_AMOUNT-PURD_DISCOUNT) as gnettotal from tmp_PURCHASE_DETAIL_T ", sc);
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
        }
        private void Bindbdetail()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select PPrice,ITM_ID from tblitem where cls_id='" + cmbClass.SelectedValue + "' and itm_id='" + cmbBookName.SelectedValue + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPPrice.Text = dt.Rows[0][0].ToString();
                txtid.Text = dt.Rows[0][1].ToString();
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
                SqlCommand cmd = new SqlCommand("select pur_no from Purchase_T where pur_no='" + txtPurNo.Text.Replace("'", "''") + "'  order by pur_no", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    sc.Close();
                    arr.Add(txtPurNo.Text);
                    int i = dd.InsertRows("sp_LoadTemp_PurchaseGrid",ref arr,true);
                    initgrid();
                    SqlCommand com = new SqlCommand("select sum(PURD_AMOUNT) as gamount ,sum(PURD_DISCOUNT) as gdiscount,sum(PURD_AMOUNT-PURD_DISCOUNT) as gnettotal from view_Purchase where  pur_no='" + txtPurNo.Text.Replace("'", "''") + "'", sc);
                    sc.Open();
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        txtTotalAmount.Text = dr["gamount"].ToString();
                        txtGDiscount.Text = dr["gdiscount"].ToString();
                        txtGTotalAmt.Text = dr["gnettotal"].ToString();
                        sc.Close();
                    }
                    SqlCommand com1 = new SqlCommand("Select * from Purchase_T where pur_no='" + txtPurNo.Text.Replace("'", "''") + "'", sc);
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
                    btnDelete.Enabled = false ;
                    sc.Close();
                    arr.Clear();
                    arr.Add("");
                    int j = dd.InsertRows("Bind_PurNo", ref arr, true);
                    if (txtPurNo.Text == arr[0].ToString())
                    {
                        cmd.CommandText = "DELETE tmp_PURCHASE_DETAIL_T";
                        sc.Open();
                        int z = cmd.ExecuteNonQuery();
                        initgrid();
                        return;
                    }
                    else
                    {
                        MsgBox.ShowBox("Purchase No# " + txtPurNo.Text + " not found...");
                        Cancil_form();
                    }
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
                    SqlCommand com = new SqlCommand("exec sp_DeletePurchase '" + txtPurNo.Text + "'", sc);
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
            
            //cryPurchase obj = new cryPurchase();
            //obj.ShowDialog();

            
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

      }
}

 