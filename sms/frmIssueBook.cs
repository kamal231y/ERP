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
    public partial class frmIssueBook : Form
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
        public string ClassName;
        public string StuId;
            
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

        public frmIssueBook()
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

       
       

        private void btn_save_Click(object sender, EventArgs e)
        {
            sc = dc.con();
            sc.Open();
            tran = sc.BeginTransaction();
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insert_tblSALE";
                cmd.Connection = sc;
                cmd.Parameters.AddWithValue("@PUR_NO", Convert.ToInt32(txtPurNo.Text));              
                cmd.Parameters.AddWithValue("@PUR_DATE", Convert.ToDateTime(dtpBillDate.Value, System.Globalization.CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@PUR_AMOUNT", Convert.ToDecimal(txtTotalAmount.Text));
                cmd.Parameters.AddWithValue("@PUR_DISCOUNT", Convert.ToDecimal(txtGDiscount.Text));
                cmd.Parameters.AddWithValue("@PUR_VCR_ID", Convert.ToInt32(cmbBookName.SelectedValue));
                cmd.Parameters.AddWithValue("@PUR_BILL_NO", Convert.ToInt32(cmbClass.SelectedValue));
                cmd.Parameters.AddWithValue("@PUR_NARRATION", txtagainst.Text);
                //cmd.Parameters.AddWithValue("@userid", Form1.GlobalLoginUserName.ToString());
                //cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                //cmd.Parameters.AddWithValue("@TerminalId", sc.WorkstationId.ToString());
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                for (int z = 0; z < dataGridView1.Rows.Count; z++)
                {
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_insert_Sale_detail_T";
                    cmd.Connection = sc;
                    cmd.Parameters.AddWithValue("@PURD_PUR_ID", txtPurNo.Text);
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
                DialogResult dialogResult = MessageBox.Show("Are You Sure To Print SALE Number - " + txtPurNo.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    print();
                    Cancil_form();
                }
                else
                {
                    Cancil_form();
                    MsgBox.ShowBox("Sale Created Successfully.....");
                    initgrid();
                }
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
        private void initgrid1()
        {
            BindGrid1();
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
            SqlDataAdapter da = new SqlDataAdapter("select * from view_studentdetail where sec_id= '"+cmbClass.SelectedValue+"'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["stu_first_name"] = "Select";
            row["stu_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbBookName.DataSource = ds.Tables[0];
            cmbBookName.DisplayMember = "stu_first_name";
            cmbBookName.ValueMember = "stu_ID";
        }
        
        //private void BindGropuNo()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    SqlDataAdapter da = new SqlDataAdapter(" select * from Book_Group ", sc);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    DataRow row = ds.Tables[0].NewRow();
        //    row["PURD_pur_id"] = "Select";
        //    row["PURD_ID"] = 0;
        //    ds.Tables[0].Rows.InsertAt(row, 0);

        //    comboBox1.DataSource = ds.Tables[0];
        //    comboBox1.DisplayMember = "PURD_ID";
        //    comboBox1.ValueMember = "PURD_ID";//PURD_ID
        //}
        private void BindGrid1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tmp_PURCHASE_DETAIL_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

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
            com.CommandText = "insert_tmp_SALE_DETAIL_T";
            com.Connection = sc;
            com.Parameters.AddWithValue("@SALD_SALE_ID", txtPurNo.Text);
            //com.Parameters.AddWithValue("@billdate", Convert.ToDateTime(dtpBillDate.Value));
            com.Parameters.AddWithValue("@SALD_ITEM_ID", cmbBookName.Text);
            com.Parameters.AddWithValue("@SALD_RATE", cmbClass.Text);
            //com.Parameters.AddWithValue("@PURD_ITEM_PUB", cmbPublisher.Text);
            //com.Parameters.AddWithValue("@PURD_Barcode", txtBarcode.Text);
            //com.Parameters.AddWithValue("@PURD_RATE", Convert.ToDecimal(txtPPrice.Text));
            //com.Parameters.AddWithValue("@SALD_QUANTITY", txt.Text);
            //com.Parameters.AddWithValue("@SALD_AMOUNT", Convert.ToDecimal(txtTotal.Text));
            //com.Parameters.AddWithValue("@SALD_DISCOUNT", Convert.ToDecimal(txtDiscount.Text));
            ////com.Parameters.AddWithValue("@PURD_net_total", Convert.ToDecimal(txtnetTotal.Text));
            //com.Parameters.AddWithValue("@pub", Convert.ToInt32(cmbPublisher.SelectedValue));
            //com.Parameters.AddWithValue("@class", Convert.ToInt32(cmbClass.SelectedValue));
            //com.Parameters.AddWithValue("@item",Convert.ToInt32(txtid.Text));
            dr = com.ExecuteReader();
            sc.Close();
            initgrid();
            cmbClass.SelectedValue = 0;    
        }

       
     
        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            Cancil_form();
        }

        
        private void cmbClass_Leave(object sender, EventArgs e)
        {
            BindBookName();
        }
        
        private void bindPurno()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_saleNo ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                txtPurNo.Text = dt.Rows[0][0].ToString();
        }
        private void bindGrpupNo()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_saleNo ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                textBox1.Text = dt.Rows[0][0].ToString();
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
            com = new SqlCommand("delete from TempTblBillGrDetails where UserId='" + Form1.GlobalLoginUserName + "' and Description='" + txtid.Text + "'", sc);
            com.ExecuteNonQuery();
            sc.Close();
            initgrid();
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            btn_save.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["item"].Value.ToString();
                cmbClass.Text = dataGridView1.CurrentRow.Cells["PURD_CLS"].Value.ToString();
                cmbBookName.Text = dataGridView1.CurrentRow.Cells["PURD_ITEM"].Value.ToString();
                //cmbPublisher.Text = dataGridView1.CurrentRow.Cells["PURD_ITEM_PUB"].Value.ToString();
                //txtQty.Text = dataGridView1.CurrentRow.Cells["PURD_QUANTITY"].Value.ToString();
                //txtPPrice.Text = dataGridView1.CurrentRow.Cells["PURD_RATE"].Value.ToString();
                //txtTotal.Text = dataGridView1.CurrentRow.Cells["PURD_AMOUNT"].Value.ToString();
                //txtDiscount.Text = dataGridView1.CurrentRow.Cells["PURD_DISCOUNT"].Value.ToString();
                //txtnetTotal.Text = dataGridView1.CurrentRow.Cells["PURD_net_total"].Value.ToString();      
            }
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
                //txtPPrice.Text = dt.Rows[0][0].ToString();
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

       
        private bool CheckReceiptNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool retno = true;
            cmd = new SqlCommand("select * from  SALE_T where SALE_id=" + txtPurNo.Text + " ", sc);
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
                SqlCommand cmd = new SqlCommand("select sale_no from sale_T where sale_no='" + txtPurNo.Text.Replace("'", "''") + "'  and saleStatus=0 order by SALE_no", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    sc.Close();
                    arr.Add(txtPurNo.Text);
                    int i = dd.InsertRows("sp_LoadTemp_SaleGrid", ref arr, true);
                    initgrid();
                    SqlCommand com = new SqlCommand("select sum(SALE_AMOUNT) as gamount,sum(SALE_DISCOUNT) as gdiscount,sum(SALE_AMOUNT-SALE_DISCOUNT) as gnettotal from view_sale where  sale_no='" + txtPurNo.Text.Replace("'", "''") + "'", sc);
                    sc.Open();
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        txtTotalAmount.Text = dr["gamount"].ToString();
                        txtGDiscount.Text = dr["gdiscount"].ToString();
                        txtGTotalAmt.Text = dr["gnettotal"].ToString();
                        sc.Close();
                    }
                    SqlCommand com1 = new SqlCommand("Select * from View_Sale where SALE_no='" + txtPurNo.Text.Replace("'", "''") + "' and salestatus=0", sc);
                    sc.Open();//Select * from SALE_T where SALE_no='" + txtPurNo.Text.Replace("'", "''") + "' and salestatus=0", sc);
                    dr = com1.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox2.Text = (dr["stu_father_name"].ToString());
                        txtRollNo.Text = (dr["stc_Roll_no"].ToString());
                        txtPartyAdd.Text = (dr["stu_per_Addr"].ToString());
                        txtagainst.Text = (dr["sale_remark"].ToString());
                        dtpBillDate.Text = dr["SALE_Date"].ToString();
                        cmbClass.SelectedValue = dr["cls_id"].ToString();
                        txtCls.Text = dr["cls_id"].ToString();
                        cmbBookName.Text = (dr["stu_first_name"].ToString());

                        sc.Close();
                    }
                  
                    dtpBillDate.Enabled = false;
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
                    MessageBox.Show("This Sale no. is  already canceled...... ");
                    //dtpBillDate.Enabled = true;
                    //btn_Add.Enabled = true;
                    //btn_Delete.Enabled = true;

                    //txtTotalAmount.Enabled = true;
                    //txtGDiscount.Enabled = true;
                    //txtGTotalAmt.Enabled = true;
                    //btn_save.Enabled = true;
                    //btnDelete.Enabled = false ;
                    //sc.Close();
                    //arr.Clear();
                    //arr.Add("");
                    //int j = dd.InsertRows("Bind_SaleNo", ref arr, true);
                    //if (txtPurNo.Text == arr[0].ToString())
                    //{
                    //    cmd.CommandText = "DELETE tmp_SALE_DETAIL_T";
                    //    sc.Open();
                    //    int z = cmd.ExecuteNonQuery();
                    //    initgrid();
                    //    return;
                    //}
                    //else
                    //{
                    //    MsgBox.ShowBox("Purchase No# " + txtPurNo.Text + " not found...");
                    //    Cancil_form();
                    //}
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtPurNo.Text == "")
            {
                MsgBox.ShowBox("Please Enter SALE Number !!!");
                return;
            }
            if (txtPurNo.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Are You Sure SALE Number - " + txtPurNo.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("exec sp_DeleteSALE '" + txtPurNo.Text + "'", sc);
                    sc.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    sc.Close();
                    MsgBox.ShowBox(" SALE Deleted !!!");
                    Cancil_form();
                }
            }
        }
        private void Cancil_form()
        {
            dtpBillDate.Enabled = true;
            btn_Add.Enabled = true;
            textBox1.Text = "";
            btn_Delete.Enabled = true;
            cmbBookName.Text = "Select";
            txtRollNo.Text = "";
            textBox2.Text = "";
            txtPartyAdd.Text = "";
            txtTotalAmount.Enabled = false;
            txtGDiscount.Enabled = true;
            txtGTotalAmt.Enabled = false;
            btn_save.Enabled = true;
            btnDelete.Enabled = false;
            cmbClass.SelectedValue = 0;
            txtTotalAmount.Text = "0";
            txtGDiscount.Text = "0";
            txtGTotalAmt.Text = "0";
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();
           sc.Open();
            com = new SqlCommand("delete from tmp_Purchase_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            sc.Open();
            com = new SqlCommand("delete from tmp_SALE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();

            bindPurno();
            BindClass();
            //BindGropuNo();
            initgrid();
       
            
        }
        private void frmIssueSale_Load(object sender, EventArgs e)
        {
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();
            sc.Open();
            com = new SqlCommand("delete from tmp_Purchase_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            //SqlDataReader dr;
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();
            //SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from tmp_SALE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            
            bindPurno();
            BindClass();
            //BindGropuNo();
            initgrid();
       
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select Purd_Pur_Id from Book_Group where Purd_Pur_Id='" + textBox1.Text.Replace("'", "''") + "'  order by Purd_Pur_Id", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    sc.Close();
                    arr.Add(textBox1.Text);
                    int i = dd.InsertRows("sp_LoadTemp_BGroupGrid", ref arr, true);
                    initgrid();
                    SqlCommand com = new SqlCommand("select sum(purd_AMOUNT) as gamount,sum(purd_DISCOUNT) as gdiscount,sum(purd_AMOUNT-purd_DISCOUNT) as gnettotal from Book_Group where  Purd_Pur_Id='" + textBox1.Text.Replace("'", "''") + "'", sc);
                    sc.Open();
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        txtTotalAmount.Text = dr["gamount"].ToString();
                        txtGDiscount.Text = dr["gdiscount"].ToString();
                        txtGTotalAmt.Text = dr["gnettotal"].ToString();
                        sc.Close();
                    }
                    dtpBillDate.Enabled = false;
                    btn_Add.Enabled = false;
                    btn_Delete.Enabled = false;
                   // btn_save.Enabled = false;
                    //btnDelete.Enabled = true;

                }
                else
                {
                    dtpBillDate.Enabled = true;
                    btn_Add.Enabled = true;
                    btn_Delete.Enabled = true;
                    btn_save.Enabled = true;
                    btnDelete.Enabled = false;
                    sc.Close();
                    arr.Clear();
                    arr.Add("");
                   // int j = dd.InsertRows("bind_BGno", ref arr, true);
                    if (textBox1.Text == arr[0].ToString())
                    {
                        cmd.CommandText = "DELETE tmp_Purchase_DETAIL_T";
                        sc.Open();
                        int z = cmd.ExecuteNonQuery();
                        initgrid1();
                        return;
                    }
                    else
                    {
                        MsgBox.ShowBox("Book Group No# " + textBox1.Text + " not found...");
                        Cancil_form();
                    }
                }
            }
        }

        private void cmbBookName_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter ada = new SqlDataAdapter("select * from view_studentdetail where sec_id='" + cmbClass.SelectedValue + "' and stu_id='" + cmbBookName.SelectedValue+ "'", sc);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPartyAdd.Text = dt.Rows[0]["STU_per_addr"].ToString();
                textBox2.Text = dt.Rows[0]["STU_FATHER_NAME"].ToString();
                txtRollNo.Text= dt.Rows[0]["Stc_roll_no"].ToString();
            }
        }
        private void print()
        {

            if (txtCls.Text == "")
            {
                if (cmbClass.Text == "" || cmbClass.Text == "Select")
                {
                    ClassName = "%";
                }
                else
                {
                    ClassName = cmbClass.SelectedValue.ToString();
                }
            }
            else
            {
                ClassName = txtCls.Text;
            }

            if (cmbBookName.Text == "" || cmbBookName.Text == "Select")
            {
                StuId = "%";
            }
            else
            {
                StuId = cmbBookName.SelectedValue.ToString();
            }
            if (txtPurNo.Text == "")
            {
                GroupNo = "%";
            }
            else
            {
                GroupNo = txtPurNo.Text;
            }
            // cryPurchase.dt1 = dtpBillDate.Value;
            //cryIssueSaleReport.dt2 = dateTimePicker2.Value;
            //cryIssueSaleReport.classid = ClassName;
            //cryIssueSaleReport.StuId = StuId;
            cryIssueSaleReport.GroupNo = GroupNo;
            cryIssueSaleReport.classid = ClassName;
            cryIssueSaleReport.StuId = StuId;

            cryIssueSaleReport obj = new cryIssueSaleReport();
            obj.ShowDialog();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            print();
        }

        private void txtRollNo_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter ada = new SqlDataAdapter("select * from view_studentdetail where sec_id='" + cmbClass.SelectedValue + "' and STC_ROLL_NO='" + txtRollNo.Text + "'", sc);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPartyAdd.Text = dt.Rows[0]["STU_per_addr"].ToString();
                textBox2.Text = dt.Rows[0]["STU_FATHER_NAME"].ToString();
                cmbBookName.Text = dt.Rows[0]["STU_First_name"].ToString();
            }
        }

        private void txtRollNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      }
}
