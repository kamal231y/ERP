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
    public partial class frmstockIssueSaleInd : Form
    {
        dataclass dd = new dataclass();
        SqlConnection sc;
        SqlDataAdapter da;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlTransaction tran;
        dataclass dc = new dataclass();
        DataSet ds; string Stu_id;
        string ROLL;
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




        public frmstockIssueSaleInd()
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

        private void Cancil_form()
        {
            txtempname.Enabled = true;
            cmbdepatment.SelectedValue = 0;
            dtpBillDate.Enabled = true;
            btn_save.Enabled = true ;
            btn_Add.Enabled = true;
            btnDelete.Enabled = false  ;
            txtSaleNo.Text = "";
            txtcardno.Text = "";
            txtempid.Text = "";
            cmbdepatment.Text = "";
            txtempname.Text = "";
            texfname.Text = "";
            texaddress.Text = "";
            txtitemBarcode.Text = "";
            texbookid.Text = "";
            cmbBookName.Text = "";
            txtQty.Text = "";
            txtremark.Text = "";
            dtpBillDate.Text = "";
            texid.Text = "";
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from tmp_ISSUE_stock", sc);
            com.ExecuteNonQuery();
            sc.Close();
            sc.Open();
            com = new SqlCommand("delete from tmp_SALE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            InitGrid();
            bindsale();
            listBox1.Visible = false;
          
       
        }
        private void bindsale()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_stocksaleNo ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                txtSaleNo.Text = dt.Rows[0][0].ToString();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Width = 50;
            dataGridView1.Columns["id"].HeaderText = "ID";
            //dataGridView1.Columns["sale_no"].Width = 150;
            //dataGridView1.Columns["sale_no"].HeaderText = "SALE_NO";
            dataGridView1.Columns["barcode"].Width = 200;
            dataGridView1.Columns["barcode"].HeaderText = "ITEM_BARCODE";
            //dataGridView1.Columns["book_id"].Width = 200;
            //dataGridView1.Columns["book_id"].HeaderText = "BOOK_ID";
            dataGridView1.Columns["item"].Width = 200;
            dataGridView1.Columns["item"].HeaderText = "Item";
            dataGridView1.Columns["qty"].Width = 150;
            dataGridView1.Columns["qty"].HeaderText = "QTY";
            
          
            
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tmp_ISSUE_stock ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bind1()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ITEMMASTER_T where (depart_name ='" + cmbdepatment.SelectedValue + "' or depart_name=0) order by ITEM_NAME  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["ITEM_NAME"] = "Select";
            row["ITEM_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbBookName.DataSource = ds.Tables[0];
            cmbBookName.DisplayMember = "ITEM_NAME";
            cmbBookName.ValueMember = "ITEM_ID";

             
    }
        private void bind2()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from DEPARTMENT_T order by DEPT_DESC ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbdepatment.DataSource = ds.Tables[0];
            cmbdepatment.DisplayMember = "DEPT_DESC";
            cmbdepatment.ValueMember = "DEPT_ID";

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select emp_id from employee_t where emp_code='" + txtempid.Text + "' ", sc);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                if (dataGridView1.Rows.Count > 0)
                {
                    StuId = dt.Rows[0]["emp_id"].ToString();
                    sc = dc.con();
                    sc.Open();
                    tran = sc.BeginTransaction();
                    try
                    {
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_insert_tblstock";
                        cmd.Connection = sc;
                        cmd.Parameters.AddWithValue("@PUR_NO", Convert.ToInt32(txtSaleNo.Text));
                        cmd.Parameters.AddWithValue("@PUR_BILL_NO", Convert.ToInt32(txtSaleNo.Text));
                        cmd.Parameters.AddWithValue("@PUR_DATE", Convert.ToDateTime(dtpBillDate.Value, System.Globalization.CultureInfo.InvariantCulture));
                        cmd.Parameters.AddWithValue("@PUR_VCR_ID", Convert.ToInt32(StuId));
                        cmd.Parameters.AddWithValue("@pur_dept_id", Convert.ToInt32(cmbdepatment.SelectedValue));//cmbBookName.SelectedValue
                        cmd.Parameters.AddWithValue("@PUR_NARRATION", txtremark.Text);
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();

                        for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_insert_stock_detail_T";
                            cmd.Connection = sc;
                            cmd.Parameters.AddWithValue("@PURD_PUR_ID", txtSaleNo.Text);
                            cmd.Parameters.AddWithValue("@PURD_ITEM_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["item_id"].Value));
                            cmd.Parameters.AddWithValue("@PURD_QUANTITY", Convert.ToDecimal(dataGridView1.Rows[z].Cells["qty"].Value));
                            cmd.Parameters.AddWithValue("@PURD_CLS_ID", Convert.ToDecimal(dataGridView1.Rows[z].Cells["dept_id"].Value));
                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        DialogResult dialogResult = MessageBox.Show("Are You Sure To Print SALE Number - " + txtSaleNo.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            print();
                            Cancil_form();
                        }
                        else
                        {
                            Cancil_form();
                            MsgBox.ShowBox("Sale Created Successfully.....");
                            InitGrid();
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
              
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            txtSaleNo.Focus();


        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {


            btn_save.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                texbookid.Text = dataGridView1.CurrentRow.Cells["item_id"].Value.ToString();
                cmbBookName.Text = dataGridView1.CurrentRow.Cells["ITEM"].Value.ToString();
                txtQty.Text = dataGridView1.CurrentRow.Cells["qty"].Value.ToString();
            }
        

        }
        private bool CheckReceiptNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool retno = true;
            cmd = new SqlCommand("select * from  SALE_T where SALE_id=" + txtSaleNo.Text + " ", sc);
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

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (texid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM salemaster2_t ", conn); ;
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("  Deleted Successfully!!!");
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

        private void frmIssueSale_Load(object sender, EventArgs e)
        {
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from tmp_ISSUE_stock", sc);
            com.ExecuteNonQuery();
            sc.Close();       
            sc.Open();
            com = new SqlCommand("delete from tmp_ISSUE_stock_DETAIL", sc);
            com.ExecuteNonQuery();
            sc.Close();

            bindsale();
            bind2();
            InitGrid();
            bind1();

        }


        private void BindPublisher()
        {
          
               
        }
        private void cmbBookName_Leave(object sender, EventArgs e)
        {
            BindPublisher();
        }
        private void print()
        
        {

            if (texbookid.Text == "")
            {
                if (cmbdepatment.Text == "" || cmbdepatment.Text == "Select")
                {
                    ClassName = "%";
                }
                else
                {
                    ClassName = cmbdepatment.SelectedValue.ToString();
                }
            }
            else
            {
                ClassName = texbookid.Text;
            }

            if (cmbBookName.Text == "" || cmbBookName.Text == "Select")
            {
                StuId = "%";
            }
            else
            {
                // StuId = cmbBookName.SelectedValue.ToString();
            }
            if (txtSaleNo.Text == "")
            {
                GroupNo = "%";
            }
            else
            {
                GroupNo = txtSaleNo.Text;
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

        }

        private void cmbstuName_Leave(object sender, EventArgs e)
        {
           
        }

        private void cmbPublisher_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
           
        }
        private void totalquatity()
        {
        }
        private void txtDiscount_Leave(object sender, EventArgs e)
        {
        }

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            bindadm();

        }

        private void txtpaid_Leave(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
                try
                {
                    
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    SqlCommand com = new SqlCommand();
                    sc.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "insert_tmp_ISSUE_stock";
                    com.Connection = sc;
                    com.Parameters.AddWithValue("@PURD_PUR_ID", txtSaleNo.Text);
                    com.Parameters.AddWithValue("@PURD_ITEM", cmbBookName.Text);
                    com.Parameters.AddWithValue("@qty", Convert.ToInt32(txtQty.Text));
                    com.Parameters.AddWithValue("@deptname", cmbdepatment.Text);
                    com.Parameters.AddWithValue("@PURD_Barcode", txtitemBarcode.Text);

                    com.Parameters.AddWithValue("@dept", Convert.ToInt32(cmbdepatment.SelectedValue));
                    com.Parameters.AddWithValue("@item_id", Convert.ToInt32(cmbBookName.SelectedValue));
                    dr = com.ExecuteReader();
                    sc.Close();
                    InitGrid();
                   
                    txtQty.Text = "0";                  
                    cmbBookName.SelectedValue = 0;
                    BindPublisher();                 
                    txtitemBarcode.Text = "";
                    txtitemBarcode.Focus();
                }
                catch (Exception ex)
                {
                    MsgBox.ShowBox(ex.ToString());

                }
            }          

        private void btn_Delete_Click(object sender, EventArgs e)
        {

            
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (texbookid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("delete from tmp_ISSUE_stock WHERE item_id=" + texbookid.Text + "", conn); ;
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {                       
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

        private void txtSaleNo_TextChanged(object sender, EventArgs e)
        {

            //dataclass dc = new dataclass();
            //SqlConnection conn = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from salemaster2_t where sale_no='"+txtSaleNo.Text+"'", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //if (dt.Rows.Count>0)
            //{
            //    txtcardno.Text = dt.Rows[0]["card_no"].ToString();
            //    txtempid.Text = dt.Rows[0]["employee_id"].ToString();
            //    cmbdepatment.Text = dt.Rows[0]["department"].ToString();
            //    txtempname.Text = dt.Rows[0]["employee_name"].ToString();
            //    texfname.Text = dt.Rows[0]["father_name"].ToString();
            //    texaddress.Text = dt.Rows[0]["address"].ToString();
            //    dtpBillDate.Text = dt.Rows[0]["stock_date"].ToString();
            //    txtremark.Text = dt.Rows[0]["remark"].ToString();
            //}

            listBox1.Visible = false;
         
      
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {

        }

        private void txtleave_Leave(object sender, EventArgs e)
        {
            if (txtSaleNo.Text != "")
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("select  sale_no from SALEstock_T where sale_no='" + txtSaleNo.Text.Replace("'", "''") + "'  and saleStatus=0 order by SALE_NO", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                   // string SALE_DISCOUNT = dr["SALE_DISCOUNT"].ToString();
                    //string SALE_PAID = dr["SALE_PAID"].ToString();
                   // txtTotalAmount.Text = dr["sale_amount"].ToString();
                    //textBox3.Text = dr["PRV_BAL"].ToString();
                    sc.Close();
                    //arr.Add(txtSaleNo.Text);
                    //int i = dd.InsertRows("sp_LoadTemp_SaleGrid", ref arr, true);
                    //initgrid();
                    //txtGDiscount.Text = SALE_DISCOUNT;
                    //txtpaid.Text = SALE_PAID;
                    //txtGTotalAmt.Text = ((Convert.ToDecimal(txtTotalAmount.Text)) + (Convert.ToDecimal(textBox3.Text)) - (Convert.ToDecimal(txtGDiscount.Text))).ToString();
                    //txtbalance.Text = (Convert.ToDecimal(txtGTotalAmt.Text) - Convert.ToDecimal(txtpaid.Text)).ToString();
                     SqlCommand com1 = new SqlCommand("Select * from View_SaleS where SALE_no='" + txtSaleNo.Text.Replace("'", "''") + "' and salestatus=0", sc);
                    sc.Open();//Select * from SALE_T where SALE_no='" + txtPurNo.Text.Replace("'", "''") + "' and salestatus=0", sc);
                    dr = com1.ExecuteReader();
                    if (dr.Read())
                    {
                        texfname.Text = (dr["stu_father_name"].ToString());
                        txtcardno.Text = (dr["emp_cardno"].ToString());
                        txtempid.Text = (dr["EMP_CODE"].ToString());

                        txtempname.Text = (dr["stu_first_name"].ToString());
                        texaddress.Text = (dr["stu_per_Addr"].ToString());
                        txtremark.Text = (dr["sale_remark"].ToString());
                        dtpBillDate.Text = dr["SALE_Date"].ToString();
                        cmbdepatment.SelectedValue = dr["STU_ID"].ToString();
                        texbookid.Text = dr["cls_id"].ToString();
                       // cmbBookName.SelectedValue = (dr["stc_roll_noaaa"].ToString();

                        cmbBookName.SelectedValue = dr["SALD_ITEM_ID"].ToString();
                        txtitemBarcode.Text = dr["Barcode_No"].ToString();
                        txtQty.Text = dr["QUANTITY"].ToString();
                        sc.Close();
                        // balance();
                    }

                    dtpBillDate.Enabled = false;
                    btn_Add.Enabled = false;
                    btn_Delete.Enabled = false;

                    //txtTotalAmount.Enabled = false;
                    //txtGDiscount.Enabled = false;
                    //txtGTotalAmt.Enabled = false;
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

        private void txtempname_TextChanged(object sender, EventArgs e)
        {
            string Name;

            if (txtempname.Text == "-Select-" || txtempname.Text == "" || txtempname.Text == "System.Data.DataRowView")
            {
                Name = "%";
            }
            else
            {
                Name = txtempname.Text.Trim().ToString();
            }
            dataclass dd = new dataclass();

            SqlConnection sc1 = dd.con();
            int i, j;
            listBox1.Items.Clear();
            sc1.Open();
            SqlDataAdapter daa = new SqlDataAdapter("select emp_code,emp_Name,dept_desc,emp_mobile from View_tblemployee where emp_Name LIKE '" + Name + "' + '%' order by emp_name desc ", sc1);
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
                listBox1.Top = txtempname.Top + txtempname.Height;             
                listBox1.Visible = true;
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Visible = false;
            }

        }
        private void bindadm()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblemployee where  emp_code like '" + txtempid.Text.Trim().Replace(" ", "") + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                Stu_id = dt.Rows[0]["emp_ID"].ToString();
                cmbdepatment.Text = dt.Rows[0]["dept_desc"].ToString();
                txtempname.Text = dt.Rows[0]["emp_Name"].ToString();
                texfname.Text = dt.Rows[0]["emp_relative_name"].ToString();
                texaddress.Text = dt.Rows[0]["emp_address"].ToString();
                bind1();
            }
            else
            {
                cmbdepatment.SelectedValue = 0;
                ROLL = "";
                Stu_id = "";
                txtempname.Text = "";

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    txtempid.Text = str[0].ToString();
                    listBox1.Visible = false;
                    bindadm();



                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {

            }
            listBox1.Visible = false;
        }

        private void txtitemBarcode_Leave(object sender, EventArgs e)
        {
              try
                {
                        dataclass dd = new dataclass();
                        SqlConnection sc = dd.con();
                        SqlDataAdapter da = new SqlDataAdapter("select * from ITEMMASTER_T where barcode_no='" + txtitemBarcode.Text+"' order by ITEM_NAME", sc);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            
                            cmbBookName.SelectedValue = dt.Rows[0]["ITeM_ID"].ToString();
                            texbookid.Text = dt.Rows[0]["ITeM_ID"].ToString();
                            txtQty.Text = "1";
                            txtQty.Focus();
                        }
                        else
                        {
                            
                        }
                    }
                
                catch (Exception EX)
                {
                    MsgBox.ShowBox(EX.ToString());

                }
            }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            print();
        }
        
           

        }
                }
            
           
           

       
        

        
    
