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
    public partial class frmIssueSale : Form
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
        string ROLL;
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

        public frmIssueSale()
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
            if (txtstuno.Text == "")
            {
                MsgBox.ShowBox("Pls Enter Admission No. .....");
                txtstuno.Text = "";
                txtstuno.Focus();
            }
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_salebal where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                if (dataGridView1.Rows.Count > 0)
                {
                    StuId = dt.Rows[0]["stu_id"].ToString();
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
                        cmd.Parameters.AddWithValue("@PUR_VCR_ID", Convert.ToInt32(StuId));
                        cmd.Parameters.AddWithValue("@PUR_BILL_NO", Convert.ToInt32(cmbClass.SelectedValue));
                        cmd.Parameters.AddWithValue("@PUR_NARRATION", txtagainst.Text.ToString());
                        cmd.Parameters.AddWithValue("@PAID", Convert.ToDecimal(txtpaid.Text));
                        cmd.Parameters.AddWithValue("@BALANCE", Convert.ToDecimal(txtbalance.Text));
                        cmd.Parameters.AddWithValue("@PRV_BAL", Convert.ToDecimal(textBox3.Text));
                        //  cmd.Parameters.AddWithValue("@SETNAME", textBox1.Text);
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
                            cmd.Parameters.AddWithValue("@PURD_AMOUNT", Convert.ToDecimal(dataGridView1.Rows[z].Cells["PURD_amount"].Value));
                            cmd.Parameters.AddWithValue("@PURD_CLS_ID", Convert.ToInt32(cmbClass.SelectedValue));
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
            //row["SEC_ID"] = "%";
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }

        private void BindBookName()
        {
            string StateName;
            if (cmbClass.Text == "-Select-" || cmbClass.Text == "" || cmbClass.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmbClass.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                DataRow row = dt.NewRow();
                row["STC_ROLL_NO"] = "0";
                row["STC_STU_ID"] = 0;
                dt.Rows.InsertAt(row, 0);
                cmbBookName.DataSource = dt;
                cmbBookName.DisplayMember = "STC_ROLL_NO";
                cmbBookName.ValueMember = "STC_STU_ID";
                cmbBookName.Text = ROLL;
            }


        }
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
            //bindgroup();

        }
        private void bindgroup()
        {
            if (cmbClass.SelectedValue != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select max(PURD_PUR_ID) from Book_Group where PURD_CLS_ID =  '" + cmbClass.SelectedValue + "' ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    textBox1.Text = dt.Rows[0][0].ToString();
            }
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
                SqlCommand cmd = new SqlCommand("select PRV_BAL,sale_no,SALE_DISCOUNT,SALE_PAID,sale_amount from sale_T where sale_no='" + txtPurNo.Text.Replace("'", "''") + "'  and saleStatus=0 order by SALE_no", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    string SALE_DISCOUNT = dr["SALE_DISCOUNT"].ToString();
                    string SALE_PAID = dr["SALE_PAID"].ToString();
                    txtTotalAmount.Text = dr["sale_amount"].ToString();
                    textBox3.Text = dr["PRV_BAL"].ToString();
                    sc.Close();
                    arr.Add(txtPurNo.Text);
                    int i = dd.InsertRows("sp_LoadTemp_SaleGrid", ref arr, true);
                    initgrid();
                    txtGDiscount.Text = SALE_DISCOUNT;
                    txtpaid.Text = SALE_PAID;
                    txtGTotalAmt.Text = ((Convert.ToDecimal(txtTotalAmount.Text)) + (Convert.ToDecimal(textBox3.Text)) - (Convert.ToDecimal(txtGDiscount.Text))).ToString();
                    txtbalance.Text = (Convert.ToDecimal(txtGTotalAmt.Text) - Convert.ToDecimal(txtpaid.Text)).ToString();
                    SqlCommand com1 = new SqlCommand("Select * from View_Sale where SALE_no='" + txtPurNo.Text.Replace("'", "''") + "' and salestatus=0", sc);
                    sc.Open();//Select * from SALE_T where SALE_no='" + txtPurNo.Text.Replace("'", "''") + "' and salestatus=0", sc);
                    dr = com1.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox2.Text = (dr["stu_father_name"].ToString());
                        txtRollNo.Text = (dr["stu_first_name"].ToString());
                        txtPartyAdd.Text = (dr["stu_per_Addr"].ToString());
                        txtagainst.Text = (dr["sale_remark"].ToString());
                        dtpBillDate.Text = dr["SALE_Date"].ToString();
                        cmbClass.SelectedValue = dr["cls_id"].ToString();
                        txtCls.Text = dr["cls_id"].ToString();
                        cmbBookName.Text = (dr["stc_roll_no"].ToString());
                        sc.Close();
                        // balance();
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
                    SqlCommand com = new SqlCommand("exec sp_DeleteSALE '" + txtPurNo.Text + "','" + txtstuno.Text + "'", sc);
                    sc.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    sc.Close();
                    MsgBox.ShowBox(" SALE Deleted !!!");
                    Cancil_form();
                }
                else
                {
                    MsgBox.ShowBox("This  SALE No can't be Deleted !!!");
                }
            }
        }
        private void Cancil_form()
        {
            listBox1.Visible = false;
            textBox3.Text = "";
            cmbClass.Enabled = true;
            txtpaid.Text = "";
            cmbBookName.Enabled = true;


            txtRollNo.Enabled = true;

            textBox2.Enabled = true;
            txtPartyAdd.Enabled = true;

            textBox2.Enabled = true;

            txtRollNo.Enabled = true;
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
            BindBookName();
            txtTotalAmount.Text = "0"; txtstuno.Text = "";
            txtGDiscount.Text = "0";
            txtGTotalAmt.Text = "0"; txtBarcode.Text = "";
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
            listBox1.Visible = false;


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
            setname();

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void cmbBookName_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter ada = new SqlDataAdapter("select * from view_studentdetail where sec_id='" + cmbClass.SelectedValue + "' and stu_id='" + cmbBookName.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPartyAdd.Text = dt.Rows[0]["STU_per_addr"].ToString();
                txtPartyAdd.Enabled = false;
                textBox2.Text = dt.Rows[0]["STU_FATHER_NAME"].ToString();
                textBox2.Enabled = false;
                txtRollNo.Text = dt.Rows[0]["Stu_first_name"].ToString();
                txtRollNo.Enabled = false;
                textBox1.Focus();
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
                // StuId = cmbBookName.SelectedValue.ToString();
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
            SqlDataAdapter ada = new SqlDataAdapter("select * from view_studentdetail where sec_id='" + cmbClass.SelectedValue + "' and STC_ROLL_NO='" + cmbBookName.Text + "'", sc);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPartyAdd.Text = dt.Rows[0]["STU_per_addr"].ToString();
                txtPartyAdd.Enabled = false;
                textBox2.Text = dt.Rows[0]["STU_FATHER_NAME"].ToString();
                textBox2.Enabled = false;
                cmbBookName.Text = dt.Rows[0]["STU_First_name"].ToString();
                cmbBookName.Enabled = false;
            }
        }

        private void balance()
        {
            try
            {
                decimal total, grandtotal, discount, paid, balance, predues;
                total = 0; grandtotal = 0; discount = 0; paid = 0; balance = 0; predues = 0;
                if (txtTotalAmount.Text != "")
                    total = Convert.ToDecimal(txtTotalAmount.Text);
                if (txtGDiscount.Text != "")
                    discount = Convert.ToDecimal(txtGDiscount.Text);
                if (txtpaid.Text != "")
                    paid = Convert.ToDecimal(txtpaid.Text);
                if (textBox3.Text != "")
                    predues = Convert.ToDecimal(textBox3.Text);
                grandtotal = total - (discount) + predues;

                //balance = grandtotal - paid;
                txtGTotalAmt.Text = grandtotal.ToString();
                txtpaid.Text = grandtotal.ToString();
                txtbalance.Text = (Convert.ToDecimal((Convert.ToDecimal(grandtotal.ToString()) - Convert.ToDecimal((txtpaid.Text).ToString())))).ToString();

            }
            catch (Exception ex)
            { ex.ToString(); }
        }

        private void txtGDiscount_TextChanged(object sender, EventArgs e)
        {
            decimal discount = 0;
            if (txtGDiscount.Text != "")
            {
                discount = Convert.ToDecimal(txtGDiscount.Text);
                //if (discount > 100)
                //{
                //    MsgBox.ShowBox("Discount will be less or equal to 100 % .....");
                //    txtGDiscount.Text = "";
                //    txtGDiscount.Focus();
                //}
                //else
                //{
                txtpaid.Text = "0";
                balance();
            }
        }

        private void txtpaid_TextChanged(object sender, EventArgs e)
        {
            //decimal paid = 0;
            //decimal grandtotal= 0;

            //if (txtpaid.Text != "")
            //    paid = Convert.ToDecimal(txtpaid.Text);
            //if (txtGTotalAmt.Text != "")
            //    grandtotal = Convert.ToDecimal(txtGTotalAmt.Text);
            //if (paid > grandtotal)
            //{
            //    MsgBox.ShowBox("Paid Amount will be less or equal Total Amount To Be Given .....");
            //    txtpaid.Text = "";
            //    txtpaid.Focus();
            //}
            //else
            // balance();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
        private void stuadmno()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_salebal where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                cmbClass.Enabled = false;
                cmbBookName.Enabled = false;
                // Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmbClass.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                txtstuno.Leave += new EventHandler(cmbClass_Leave);
                ROLL = dt.Rows[0]["ROLL"].ToString();
                txtRollNo.Text = dt.Rows[0]["Student_Name"].ToString();
                textBox2.Text = dt.Rows[0]["FatherName"].ToString();
                textBox3.Text = dt.Rows[0]["balance"].ToString();
                textBox2.Enabled = false;
                if (textBox3.Text == "")
                {
                    textBox3.Text = "0";
                }
                BindBookName();
                //bindgroup();
                // textBox1.Focus();
                //BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
            else
            {
                cmbClass.SelectedValue = 0;
                txtRollNo.Text = "0";
                //Stu_id = "0";
                textBox2.Text = "";
                // BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }

        }

        private void txtstuno_Leave(object sender, EventArgs e)
        {

            bindadm();

        }
        private void bindadm()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_salebal where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                StuId = dt.Rows[0]["STU_ID"].ToString();
                //txtstuno.Text = dt.Rows[0]["STU_ID"].ToString(); 
                cmbClass.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmbClass.Enabled = false;
                txtstuno.Leave += new EventHandler(cmbClass_Leave);
                ROLL = dt.Rows[0]["ROLL"].ToString();
                txtRollNo.Text = dt.Rows[0]["Student_Name"].ToString();
                txtRollNo.Enabled = false;
                textBox2.Text = dt.Rows[0]["FatherName"].ToString();
                textBox1.Text = dt.Rows[0]["balance"].ToString();
                textBox2.Enabled = false;
                if (textBox1.Text == "")
                {
                    textBox1.Text = "0";
                }
                BindBookName();

            }
            else
            {
                cmbClass.SelectedValue = 0;
                ROLL = "";
                StuId = "";
                txtRollNo.Text = "";

            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    dataclass dd = new dataclass();
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("select STC_SEC_ID,STU_ID,Student_Name,admno,fathername,address from View_studentReport where  AccNo like '" + txtBarcode.Text.Trim().Replace(" ", "") + "'  ", sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        cmbClass.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                        BindBookName();
                        cmbBookName.SelectedValue = dt.Rows[0]["STU_ID"].ToString();
                        txtRollNo.Text = dt.Rows[0]["Student_Name"].ToString();
                        txtstuno.Text = dt.Rows[0]["admno"].ToString();
                        textBox2.Text = dt.Rows[0]["fathername"].ToString();
                        txtPartyAdd.Text = dt.Rows[0]["address"].ToString();
                        textBox1.Focus();
                    }
                    else
                    {
                        cmbClass.SelectedValue = 0;
                        BindBookName();
                        cmbBookName.SelectedValue = 0;
                        txtRollNo.Text = "";
                        txtstuno.Text = "";
                        textBox2.Text = "";
                        txtPartyAdd.Text = "";
                    }
                }
            }
            catch (Exception EX)
            {
                MsgBox.ShowBox(EX.ToString());

            }
        }

        private void txtGDiscount_Leave(object sender, EventArgs e)
        {
            decimal paid = 0, desc = 0; txtpaid.Text = "0";
            decimal grandtotal = 0;
            if (txtGDiscount.Text != "")
                desc = Convert.ToDecimal(txtGDiscount.Text);
            if (txtpaid.Text != "")
                paid = Convert.ToDecimal(txtpaid.Text);
            if (txtGTotalAmt.Text != "")
                grandtotal = Convert.ToDecimal(txtGTotalAmt.Text);
            //if (desc > 100)
            //{
            //    MsgBox.ShowBox("Discount Will be  less or equal 100% .....");
            //    txtpaid.Text = "";
            //    txtpaid.Focus();
            //}
            //else
            balance();
        }

        private void txtpaid_Leave(object sender, EventArgs e)
        {
            decimal paid = 0;
            decimal grandtotal = 0;

            if (txtpaid.Text != "")
                paid = Convert.ToDecimal(txtpaid.Text);
            if (txtGTotalAmt.Text != "")
                grandtotal = Convert.ToDecimal(txtGTotalAmt.Text);
            if (paid > grandtotal)
            {
                MsgBox.ShowBox("Paid Amount will be less or equal Total Amount To Be Given .....");
                txtpaid.Text = "";
                txtpaid.Focus();
            }
            else
            {
                decimal total, discount, balance, predues;
                total = 0; grandtotal = 0; discount = 0; paid = 0; balance = 0; predues = 0;
                if (txtTotalAmount.Text != "")
                    total = Convert.ToDecimal(txtTotalAmount.Text);
                if (txtGDiscount.Text != "")
                    discount = Convert.ToDecimal(txtGDiscount.Text);
                if (txtpaid.Text != "")
                    paid = Convert.ToDecimal(txtpaid.Text);
                if (textBox3.Text != "")
                    predues = Convert.ToDecimal(textBox3.Text);
                grandtotal = total - (discount) + predues;

                //balance = grandtotal - paid;
                txtGTotalAmt.Text = grandtotal.ToString();
                txtpaid.Text = paid.ToString();
                txtbalance.Text = (Convert.ToDecimal((Convert.ToDecimal(grandtotal.ToString()) - Convert.ToDecimal((txtpaid.Text).ToString())))).ToString();
            }
        }

        private void txtRollNo_TextChanged(object sender, EventArgs e)
        {
            {

                string Name;

                if (txtRollNo.Text == "-Select-" || txtRollNo.Text == "" || txtRollNo.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtRollNo.Text.Trim().ToString();
                }
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '/' + ds1.Tables[0].Rows[j][1].ToString() + '/' + ds1.Tables[0].Rows[j][2].ToString() + '/' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    //listBox1.Top = txtstuno.Top + txtstuno.Height;
                    //listBox1.Width = txt_section.Width;
                    //listBox1.Left = txt_section.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    txtstuno.Text = str[0].ToString();
                    listBox1.Visible = false;
                    stuadmno();



                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {

                string Name;



                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox2.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select distinct Purd_Pur_Id,PURD_PUR_NAME from Book_Group where PURD_PUR_NAME like '" + textBox1.Text + "' + '%'  order by Purd_Pur_Id", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox2.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '/' + ds1.Tables[0].Rows[j][1].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox2.Top = txtstuno.Top + txtstuno.Height;
                    //listBox1.Width = txt_section.Width;
                    //listBox1.Left = txt_section.Left;
                    listBox2.Visible = true;
                }
                else
                {
                    listBox2.Items.Clear();
                    listBox2.Visible = false;
                }

            }



            //if (textBox1.Text != "")
            //{
            //    dataclass dd = new dataclass();
            //    SqlConnection sc = dd.con();
            //    SqlDataReader dr;
            //    SqlCommand cmd = new SqlCommand("select Purd_Pur_Id from Book_Group where Purd_Pur_Id='" + textBox1.Text.Replace("'", "''") + "'  order by Purd_Pur_Id", sc);
            //    sc.Open();
            //    dr = cmd.ExecuteReader();
            //    ArrayList arr = new ArrayList();
            //    if (dr.Read())
            //    {
            //        sc.Close();
            //        arr.Add(textBox1.Text);
            //        int i = dd.InsertRows("sp_LoadTemp_BGroupGrid", ref arr, true);
            //        initgrid();
            //        SqlCommand com = new SqlCommand("select sum(purd_AMOUNT) as gamount,sum(purd_DISCOUNT) as gdiscount,sum(purd_AMOUNT-purd_DISCOUNT) as gnettotal from Book_Group where  Purd_Pur_Id='" + textBox1.Text.Replace("'", "''") + "'", sc);
            //        sc.Open();
            //        dr = com.ExecuteReader();
            //        if (dr.Read())
            //        {
            //            txtTotalAmount.Text = dr["gnettotal"].ToString();
            //            txtGDiscount.Text = "0";
            //            //txtGTotalAmt.Text = dr["gnettotal"].ToString();
            //            //txtpaid.Text = dr["gnettotal"].ToString();
            //            sc.Close();
            //        }
            //        dtpBillDate.Enabled = true;
            //        btn_Add.Enabled = false;
            //        btn_Delete.Enabled = false;
            //        // btn_save.Enabled = false;
            //        //btnDelete.Enabled = true;

            //    }
            //    else
            //    {
            //        dtpBillDate.Enabled = true;
            //        btn_Add.Enabled = true;
            //        btn_Delete.Enabled = true;
            //        btn_save.Enabled = true;
            //        btnDelete.Enabled = false;
            //        sc.Close();
            //        arr.Clear();
            //        arr.Add("");
            //        // int j = dd.InsertRows("bind_BGno", ref arr, true);
            //        if (textBox1.Text == arr[0].ToString())
            //        {
            //            cmd.CommandText = "DELETE tmp_Purchase_DETAIL_T";
            //            sc.Open();
            //            int z = cmd.ExecuteNonQuery();
            //            initgrid1();
            //            return;
            //        }
            //        else
            //        {
            //            MsgBox.ShowBox("Book Group No# " + textBox1.Text + " not found...");
            //            Cancil_form();
            //        }
            //    }
            //}
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {

            if (listBox2.Visible == true)
            {
                try
                {
                    string[] str = listBox2.SelectedItem.ToString().Split('/');
                    textBox1.Text = str[1].ToString();
                    string akky = str[0].ToString();
                    // listBox1.Visible = false;
                    stubookissue(akky);
                    listBox2.Visible = false;


                }
                catch
                { }
            }
            if (listBox2.SelectedIndex == 0)
            {

            }

        }


        public void stubookissue(string akky)
        {

            if (akky != "")
            {
                string clssecid = cmbClass.SelectedValue.ToString();

                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select Purd_Pur_Id from Book_Group where Purd_Pur_Id='" + akky + "'  order by Purd_Pur_Id", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    sc.Close();
                    arr.Add(akky);
                    arr.Add(clssecid);
                    int i = dd.InsertRows("sp_LoadTemp_BGroupGrid", ref arr, true);
                    initgrid();
                    SqlCommand com = new SqlCommand("select sum(purd_AMOUNT) as gamount,sum(purd_DISCOUNT) as gdiscount,sum(purd_AMOUNT-purd_DISCOUNT) as gnettotal from Book_Group where  Purd_Pur_Id='" + akky + "'", sc);
                    sc.Open();
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        txtTotalAmount.Text = dr["gnettotal"].ToString();
                        txtGDiscount.Text = "0";
                        txtGTotalAmt.Text = dr["gnettotal"].ToString();
                        txtpaid.Text = dr["gnettotal"].ToString();
                        sc.Close();
                    }
                    dtpBillDate.Enabled = true;
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
                    if (akky == arr[0].ToString())
                    {
                        cmd.CommandText = "DELETE tmp_Purchase_DETAIL_T";
                        sc.Open();
                        int z = cmd.ExecuteNonQuery();
                        initgrid1();
                        return;
                    }
                    else
                    {
                        MsgBox.ShowBox("Book Group Name# " + textBox1.Text + " not found...");
                        Cancil_form();
                    }
                }
            }


        }

        private void setname()
        {

            dataclass dd = new dataclass();

            SqlConnection sc1 = dd.con();
            sc1.Open();
            SqlDataAdapter daa = new SqlDataAdapter("select distinct Purd_Pur_Id,PURD_PUR_NAME from Book_Group   order by Purd_Pur_Id", sc1);
            DataSet ds1 = new DataSet();

            daa.Fill(ds1);
            dataGridView2.DataSource = ds1.Tables[0];
            dataGridView2.Columns["Purd_Pur_Id"].Width = 100;
            dataGridView2.Columns["Purd_Pur_Id"].HeaderText = "SET ID";
            dataGridView2.Columns["PURD_PUR_NAME"].Width = 100;
            dataGridView2.Columns["PURD_PUR_NAME"].HeaderText = "SET NAME";

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = true;
            if (dataGridView2.Rows.Count > 0)
            {
                // textBox1.Text = dataGridView2.CurrentRow.Cells["PURD_PUR_NAME"].Value.ToString();
                string akky = dataGridView2.CurrentRow.Cells["Purd_Pur_Id"].Value.ToString();

                // listBox1.Visible = false;
                stubookissue(akky);

            }
        }



    }
}
