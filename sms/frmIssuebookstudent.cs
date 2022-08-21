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
    public partial class frmIssuebookemployee : Form
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
        public frmIssuebookemployee()
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
            listBox1.Visible=false;
            textBox2.Enabled = true;
            txtstuno.Text = "";
            txtRollNo.Enabled = true;
            cmbstuName.Enabled = true;
            cmbClass.Enabled = true;
            dtpBillDate.Enabled = true;
            btn_save.Enabled = true;
            btn_Add.Enabled = true;
            btn_Delete.Enabled = true;         
            btn_save.Enabled = true;
            btnDelete.Enabled = false;           
            cmbstuName.Text = "Select";
            txtRollNo.Text = "";
            textBox2.Text = "";
            txtPartyAdd.Text = "";           
            BindClass();
            SqlDataReader dr;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand();


            sc.Open();
            com = new SqlCommand("delete from tmp_ISSUE_BOOK_T", sc);
            com.ExecuteNonQuery();
            sc.Close();
            sc.Open();
            com = new SqlCommand("delete from tmp_ISSUE_BOOK_DETAIL", sc);
            com.ExecuteNonQuery();
            sc.Close();
            bindPurno();
            BindClass();
            initgrid();
            txtitemBarcode.Text = "";
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from student_t where (stu_adm_no = '" + txtstuno.Text.Trim().Replace(" ", "") + "') ", sc);
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
                    cmd.CommandText = "sp_insert_ISSUE_BOOK_T";
                    cmd.Connection = sc;
                    cmd.Parameters.AddWithValue("@ISSUE_NO", Convert.ToInt32(txtPurNo.Text));
                    cmd.Parameters.AddWithValue("@ISSUE_DATE", Convert.ToDateTime(dtpBillDate.Value, System.Globalization.CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@STU_ID", Convert.ToInt32(StuId));
                    cmd.Parameters.AddWithValue("@CLS_ID", Convert.ToInt32(cmbClass.SelectedValue));
                    cmd.Parameters.AddWithValue("@PUR_NARRATION", txtagainst.Text);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();

                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_insert_ISSUE_BOOK_DETAIL_T";
                        cmd.Connection = sc;
                        cmd.Parameters.AddWithValue("@PURD_PUR_ID", txtPurNo.Text);
                        cmd.Parameters.AddWithValue("@STU_ID", Convert.ToInt32(StuId));
                        cmd.Parameters.AddWithValue("@PURD_ITEM_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["item"].Value));                        
                        cmd.Parameters.AddWithValue("@PURD_CLS_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["class"].Value));
                        cmd.Parameters.AddWithValue("@PURD_BOOK", dataGridView1.Rows[z].Cells["SUBJECT"].Value.ToString());
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
                        MsgBox.ShowBox("Book Issued Successfully.....");
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
            dataGridView1.Columns["ISSUE_ID"].Visible = false;
            dataGridView1.Columns["class"].Visible = false;
            dataGridView1.Columns["ITEM"].Visible = false;
            dataGridView1.Columns["PURD_Barcode"].Width = 80;
            dataGridView1.Columns["PURD_Barcode"].HeaderText = "Barcode";
            dataGridView1.Columns["ITEM"].Width = 200;
            dataGridView1.Columns["ITEM"].HeaderText = "Book";
            dataGridView1.Columns["CLASS"].Width = 100;
            dataGridView1.Columns["CLASS"].HeaderText = "Class";
            dataGridView1.Columns["Subject"].Width = 100;
            dataGridView1.Columns["Subject"].HeaderText = "Subject";
            

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

        private void BindSubject()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select sub_desc from view_bmaster where barcode_no='" + txtitemBarcode.Text + "' order by sub_desc", sc);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textqty.Text = dt.Rows[0]["sub_desc"].ToString();
            }
        }
        private void BindStudentName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_class_T where stc_sec_id =  '" + cmbClass.SelectedValue + "' order by  stc_stu_id", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["stc_roll_no"] = "0";
            row["stc_stu_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbstuName.DataSource = ds.Tables[0];
            cmbstuName.DisplayMember = "stc_roll_no";
            cmbstuName.ValueMember = "stc_stu_id";
            cmbstuName.Text = ROLL;
        }

        private void BindGrid1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tmp_book_DETAIL_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void BindGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tmp_ISSUE_BOOK_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

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
                    com.CommandText = "insert_tmp_ISSUE_BOOK_T";
                    com.Connection = sc;
                    com.Parameters.AddWithValue("@PURD_PUR_ID", txtPurNo.Text);
                    com.Parameters.AddWithValue("@PURD_ITEM", txtitemBarcode.Text);
                    com.Parameters.AddWithValue("@PURD_CLS", cmbClass.Text);
                    com.Parameters.AddWithValue("@PURD_Barcode", txtitemBarcode.Text);
                    com.Parameters.AddWithValue("@class", Convert.ToInt32(cmbClass.SelectedValue));
                    com.Parameters.AddWithValue("@subject", textqty.Text);
                    dr = com.ExecuteReader();
                    sc.Close();
                    initgrid();                                   
                   
                    txtitemBarcode.Text = "";
                    txtitemBarcode.Focus();
                }
                catch (Exception ex)
                {
                    MsgBox.ShowBox(ex.ToString());

                }        
        
           
        }
        


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            txtPurNo.Focus();
        }


        private void cmbClass_Leave(object sender, EventArgs e)
        {
            BindTxtCls();         
            BindStudentName();
        }
        private void BindTxtCls()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  and sec_id='" + cmbClass.SelectedValue + "'", sc);
            sc.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
                txtCls.Text = dr["cls_id"].ToString();
            sc.Close();
        }

        private void bindPurno()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_issueNo ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                txtPurNo.Text = dt.Rows[0][0].ToString();
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
                        com = new SqlCommand("delete from tmp_ISSUE_BOOK_T where  item='" + txtid.Text + "'", sc);
                        com.ExecuteNonQuery();
                        sc.Close();
                        initgrid();
                        txtid.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MsgBox.ShowBox(ex.ToString());
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            btn_save.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["item"].Value.ToString();            
                txtitemBarcode.Text = dataGridView1.CurrentRow.Cells["PURD_Barcode"].Value.ToString();
                txtitemBarcode.Text = dataGridView1.CurrentRow.Cells["subject"].Value.ToString();
            }
        }
       
        private bool CheckReceiptNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool retno = true;
            cmd = new SqlCommand("select * from  ISSUE_BOOK_T where ISSUE_ID=" + txtPurNo.Text + " ", sc);
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
                SqlCommand cmd = new SqlCommand("select * from ISSUE_BOOK_T where ISSUE_no='" + txtPurNo.Text.Replace("'", "''") + "'  order by issue_no", sc);
                sc.Open();
                dr = cmd.ExecuteReader();
                ArrayList arr = new ArrayList();
                if (dr.Read())
                {
                    sc.Close();
                    arr.Add(txtPurNo.Text);
                    int i = dd.InsertRows("sp_LoadTemp_issueGrid", ref arr, true);
                    initgrid();
                  
                    sc.Close();

                    SqlCommand com1 = new SqlCommand("Select * from View_issuebook where issue_no='" + txtPurNo.Text.Replace("'", "''") + "'", sc);
                    sc.Open();
                    dr = com1.ExecuteReader();
                    if (dr.Read())
                    {
                        cmbClass.SelectedValue = dr["cls_id"].ToString();
                       
                        cmbstuName.SelectedValue = dr["stu_id"].ToString();
                        txtPartyAdd.Text = dr["address"].ToString();
                        textBox2.Text = dr["fathername"].ToString();
                        dtpBillDate.Text = dr["issue_Date"].ToString();
                        txtRollNo.Text = dr["student_name"].ToString();
                        txtstuno.Text = dr["admno"].ToString();
                        sc.Close();
                    }

                    dtpBillDate.Enabled = false;
                    btn_Add.Enabled = false;
                    btn_Delete.Enabled = false;
                    
                    btn_save.Enabled = false;
                    btnDelete.Enabled = true;

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
                    arr.Add(txtPurNo.Text);
                    Cancil_form();
                    
                }
                listBox1.Visible = false;
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
          
            sc.Open();
            com = new SqlCommand("delete from tmp_SALE_DETAIL_T", sc);
            com.ExecuteNonQuery();
            sc.Close();

            bindPurno();
            BindClass();
            //initgrid();

        }


      
       
        private void print()
        {
            //
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("select * from View_issuebook where  issue_no='" + txtPurNo.Text + "'", sc);
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                txtCls.Text = dr["cls_id"].ToString();
                txtStuId.Text = dr["stu_id"].ToString();
                sc.Close();
            }
            //
            if (txtCls.Text == "")
            {
                ClassName = "%";
            }
            else
            {
                ClassName = txtCls.Text;
            }
            if (txtStuId.Text == "")
            {
                StuId = "%";
            }
            else
            {
                StuId = txtStuId.Text;
            }
          
            if (txtPurNo.Text == "")
            {
                GroupNo = "%";
            }
            else
            {
                GroupNo = txtPurNo.Text;
            }
            cryIssuebookReport.GroupNo = GroupNo;
            cryIssuebookReport.classid = ClassName;
            cryIssuebookReport.StuId = StuId;
            cryIssuebookReport obj = new cryIssuebookReport();
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
                cmbstuName.Text = dt.Rows[0]["STU_First_name"].ToString();
            }
        }

        private void cmbstuName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbstuName.SelectedValue != null && cmbstuName.SelectedValue.ToString() != "0")
                    ROLL = cmbstuName.SelectedValue.ToString();
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter ada = new SqlDataAdapter("select * from view_studentdetail where sec_id='" + cmbClass.SelectedValue + "' and stu_id=" + ROLL + "", sc);
                DataTable dt = new DataTable();
                ada.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["STU_ADM_NO"].ToString();
                    txtPartyAdd.Text = dt.Rows[0]["STU_per_addr"].ToString();
                    textBox2.Text = dt.Rows[0]["STU_FATHER_NAME"].ToString();
                    txtRollNo.Text = dt.Rows[0]["Stu_first_name"].ToString();
                    txtRollNo.Enabled = false;
                    txtStuId.Text = dt.Rows[0]["stu_id"].ToString();
                    cmbstuName.Text = ROLL;

                }
                else
                {
                    cmbstuName.Text = ROLL;
                }
            }
            catch (Exception ex) { }
        }

       
        private void bindadm()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmbClass.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmbClass.Enabled = false;
                txtstuno.Leave += new EventHandler(cmbClass_Leave);
                ROLL = dt.Rows[0]["ROLL"].ToString();
                
                cmbstuName.Enabled = true;

                txtRollNo.Text = dt.Rows[0]["Student_Name"].ToString();
                txtRollNo.Enabled = false;
                textBox2.Text = dt.Rows[0]["FatherName"].ToString();
               
              BindStudentName();

            }
            else
            {
                cmbClass.SelectedValue = 0;
                ROLL = "";
                Stu_id = "";
                txtRollNo.Text = "";

            }
        }

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            bindadm();

        }

        private void txtstuno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {

            {
                try
                {
                    if (e.KeyChar == 13)
                    {
                        dataclass dd = new dataclass();
                        dataclass dc = new dataclass();
                        SqlConnection sc = dc.con();
                        SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AccNo like '" + txtBarcode.Text.Trim().Replace(" ", "") + "'  ", sc);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            cmbClass.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                            BindStudentName();
                            cmbstuName.SelectedValue = dt.Rows[0]["STU_ID"].ToString();
                            txtRollNo.Text = dt.Rows[0]["Student_Name"].ToString();
                            txtstuno.Text = dt.Rows[0]["admno"].ToString();
                            textBox2.Text = dt.Rows[0]["fathername"].ToString();
                            txtPartyAdd.Text = dt.Rows[0]["address"].ToString();
                            BindTxtCls();
                            txtitemBarcode.Focus();
                        }
                        else
                        {
                            cmbClass.SelectedValue = 0;
                            BindStudentName();
                            cmbstuName.SelectedValue = 0;
                            txtRollNo.Text = "";
                            txtstuno.Text = "";
                            textBox2.Text = "";
                            txtPartyAdd.Text = "";
                            txtitemBarcode.Focus();
                        }
                    }
                }
                catch (Exception EX)
                {
                    MsgBox.ShowBox(EX.ToString());

                }
            }
        }

        private void txtitemBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtitemBarcode.Text == "" || txtitemBarcode.Text == "Select")
            {

            }
            BindSubject();        
           
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRollNo_TextChanged(object sender, EventArgs e)
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
                    listBox1.Top = txtstuno.Top + txtstuno.Height;
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

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
              if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    txtstuno.Text = str[0].ToString();
                    listBox1.Visible = false;
                    bindadm();



                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                
            }
        }

        private void txtitemBarcode_Leave(object sender, EventArgs e)
        {
            BindSubject();
        }
        }

        }

        
    
