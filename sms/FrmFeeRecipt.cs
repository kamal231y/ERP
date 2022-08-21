using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using sms.DATALAYER;
using System.Data.SqlClient;
using System.Data.OleDb;
using MsgBoxRet;
using System.Reflection;


namespace sms
{   
   
    public partial class FrmFeeRecipt : Form
    {
        public static int bank = 1;
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader; 
        validation v1 = new validation();
        int ret;
        int RowIndex1 = 0;
        string partfee;
        int Stuid;
        SqlCommand cmd1;
        decimal num;
        string month;
        string noofmonth;
        decimal dues;
        string Balance;
        SqlDataReader dr;
        SqlTransaction tran;
         string Fee;
         string total;
         string Prev;
         string concession;
         string Paid;
         string comment;
         string Stu_id;
         string ROLL,phone;
         public string lblAdm; string Nameofstudent;
        public FrmFeeRecipt()
        {
            InitializeComponent();
             Cancel_form();
            
        }


        void Cancel_form()
        {
            listBox1.Visible = false;
            lblmonth.Visible = false;
            cmb_class.Enabled = true;
            cmb_rollno.Enabled = true;
            txtlatefee.Enabled = true;
            txt_paid.Enabled = true;
            cmbmop.Enabled = true;
            cmb_accounts.Enabled = true;
            txtstuno.Enabled = true;
            button1.Enabled = false;
            txtCardNo.Text = "";
            txt_totalfee.Text = "";
            txt_prevdue.Text = "";
            txtlatefee.Text = "";
            cmb_class.Text = "";
            stuname.Text = "";
            cmb_class.Text = "-Select-";
            cmbBankName.Text = "-Select-";
            cmbmop.Text = "CASH";
            txt_prevdue.Text = "";
            txt_totalfee.Text = "";
            txtDDNo.Text = "";
            txtstuno.Text = "";
            txt_paid.Text = "";
            txt_nettotal.Text = "";
            txt_month.Text = "";
            txt_concession.Text="";
            txt_balance.Text="";
            txt_coments.Text="";
            txt_date.Text=DateTime.Now.ToString();
            cmb_rollno.Text = "";
           // cmb_accounts.Text = "-Select-";
            btn_create.Enabled = true;
            txt_month.Enabled = true;
            bindReceiptNo();
            dataGridView1.DataSource = DBNull.Value;
            dataGridView2.DataSource = DBNull.Value;
            dataGridView3.DataSource = DBNull.Value;
             btnprint.Enabled = false;


        }
        private void FrmFeeRecipt_Load(object sender, EventArgs e)
        {
            bindclass();
           bindReceiptNo();
            bindAccount();         
            bindMOP();
            bindBank();
           // LoadGrid();
            txtstuno.Focus();
            MenuList();
            bindcmpname();
            if (sms.Form1.GlobalLoginUserType.ToString() != "ADMINISTRATOR")
            {
                //txtlatefee.Enabled = false;
                //txt_concession.Enabled = false;
               // button1.Visible = false;
            }
        }

        private void bindcmpname()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from company_t", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["com_name"].ToString();
                textBox2.Text = dt.Rows[0]["companytelno"].ToString();
            }
        }
        private void MenuList()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from tblRights where UserName= '" + sms.Form1.GlobalLoginUserName.ToString() + "'", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from tblRights where UserName='" + sms.Form1.GlobalLoginUserName.ToString() + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                button3.Visible = Convert.ToBoolean(dr["FEE3"]);
                button2.Visible = Convert.ToBoolean(dr["T5"]);
               

            }
            else
            {
                button3.Enabled = false;
                button2.Enabled = false;
                
            }
            sc.Close();
        }



        private void bindReceiptNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("Select max(FRC_NO)+1 as max from FEE_RECEIPT_T", sc);
            sc.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (dr["max"].ToString() == "")
                {
                    txt_receiptno.Text = "1";

                }
                else
                {
                    txt_receiptno.Text = Convert.ToString(Convert.ToInt32(dr["max"]));
                }

            }
            listBox1.Visible = false;
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "-Select-";
           
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";
        }
        private void bindMOP()

        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblmop order by id", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbmop.DataSource = ds.Tables[0];
            cmbmop.DisplayMember = "mop";
            cmbmop.ValueMember = "id";
        }
        private void bindBank()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblBank order by Bank_id ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Bank_Name"] = "-Select-";
            row["Bank_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbBankName.DataSource = ds.Tables[0];
            cmbBankName.DisplayMember = "Bank_Name";
            cmbBankName.ValueMember = "Bank_id";
        }
    
        private void bindAccount()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblledger where id in (1,2)  order by id  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["ACC_NAME"] = "-Select-";
            //row["ACC_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_accounts.DataSource = ds.Tables[0];
            cmb_accounts.DisplayMember = "NAME";
            cmb_accounts.ValueMember = "id";
        }
        private void bindmonth()
        {
            if (chkpartpayment.Checked == false)
            {

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("Search_Monthnumber ", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@stu_id", Stu_id);
                da.SelectCommand.Parameters.AddWithValue("@part_payment", Convert.ToInt32(chkpartpayment.Checked));
                sc.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                string num = dt.Rows[0]["MONTHNO"].ToString();
                txt_month.Text = dt.Rows[0]["MONTHNO"].ToString();
            }


        }

        
        //private bool CheckType()
        //{
        //    bool ret = true;
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    SqlCommand com = new SqlCommand("select * from ",sc);
        //    SqlDataReader dr = com.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        ret = true;
        //    }
        //    else
        //    {
        //        ret = false;
        //    }
        //    return ret;
        //}
        private void BindGrid(string month, decimal dues)
        {

            if (txt_month.Text == "" || txt_month.Text == "0")
            {
                num = 1;

            }
            else
            {
                num = Convert.ToDecimal(txt_month.Text);
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();



                    SqlDataAdapter da1 = new SqlDataAdapter("select distinct FEE_ID as Fee_Id, FEE_DESC as Fee, SEF_FEE_AMOUNT as Amount from View_tblClassFeeSingle where FEE_ID in (select Fee_Id from FEE_DETAIL_T where " + month + " =1) and SEF_STU_ID=" + Stuid + "  ", sc);
                    DataTable  dt = new DataTable();
                    da1.Fill(dt);
                    
                    decimal sum = 0,Discount=0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                         sum = sum + Convert.ToDecimal(dt.Rows[i][2].ToString());
                         Discount = Discount + Convert.ToDecimal(dt.Rows[i][3].ToString());
                    }
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["Fee_Id"].Visible = false;
                    dataGridView1.Columns["Fee"].Width = 200;
                    dataGridView1.Columns["Amount"].Width = 100;
                    
                    txt_totalfee.Text = (sum).ToString();
                    txt_concession.Text = Discount.ToString();
                    txt_prevdue.Text = dues.ToString();
                    balanceCalculate();
                    
                 }


        private void BIND_DATA()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select DUES from STUDENT_OLD_DUES_T where Stu_id=" + Stu_id + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                dues = Convert.ToDecimal(dt.Rows[0]["DUES"].ToString());
            else
                dues = 0;
            sc.Open();
            bindmonth();
                bindFee();
                LoadStudentFeeStatus();
                LoadGrid();
        }
        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_paid_Leave(object sender, EventArgs e)
        {
            balanceCalculate();
        }

        public void balanceCalculate()
        {

            if (txt_concession.Text == "")
            {
                txt_concession.Text = "0";

            }
            if (txt_paid.Text == "")
            {
                txt_paid.Text = "0";

            }
            if (txtlatefee.Text == "")
            {
                txtlatefee.Text = "0";

            }
            if (txt_balance.Text == "")
            {
                txt_balance.Text = "0";

            }
            if (txt_totalfee.Text == "")
            {
                txt_totalfee.Text = "0";

            }

            txt_nettotal.Text = (Convert.ToDecimal(txt_totalfee.Text) + dues + Convert.ToDecimal(txtlatefee.Text)).ToString();
            decimal sum1 = Convert.ToDecimal(txt_concession.Text);
            decimal paidAmount = (Convert.ToDecimal(txt_nettotal.Text) - sum1);
            txt_paid.Text = paidAmount.ToString();
            txt_balance.Text = (paidAmount - Convert.ToDecimal(txt_paid.Text)).ToString();
        }


        private void btn_create_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure to Create Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                bindReceiptNo();
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_T where STU_ID=" + Stu_id + "", sc);
                DataTable sibling = new DataTable();
                da.Fill(sibling);
                if (sibling.Rows.Count > 0)
                {
                    DateTime dt1 = Convert.ToDateTime(txt_date.Value, System.Globalization.CultureInfo.InvariantCulture);
                    // DateTime dt2 = Convert.ToDateTime(DDdate.Value, System.Globalization.CultureInfo.InvariantCulture);
                    //DateTime dt3 = Convert.ToDateTime(DDdate.Value, System.Globalization.CultureInfo.InvariantCulture);

                    tran = sc.BeginTransaction();

                    if (cmb_class.SelectedValue.ToString() == "0" || cmb_class.Text == "")
                    {
                        MsgBox.ShowBox("Please Select Class...");
                    }


                    else
                        if (cmbmop.Text == "-Select-" || cmbmop.Text == "")
                        {
                            MsgBox.ShowBox("Please Select MOP ...");
                        }
                        else
                            if (cmbmop.Text == "Cheque" && txtDDNo.Text == "")
                            {

                                MsgBox.ShowBox("Please Enter Cheque No...");

                            }
                            else
                                if (cmb_accounts.SelectedValue.ToString() == "0" || cmb_accounts.Text == "")
                                {
                                    MsgBox.ShowBox("Please Select Account ...");
                                }
                                else
                                    if (dataGridView1.Rows.Count == 0)
                                    {
                                        MsgBox.ShowBox("No Fee To Save ......");
                                    }
                                    else
                                    {
                                        bindReceiptNo();
                                        SqlCommand com = new SqlCommand("Insert_student_FeeReciept " + txt_receiptno.Text + ",'" + dt1.ToString("MM/dd/yyyy") + "'," + Stu_id + "," + num + "," + txt_totalfee.Text + "," + txt_paid.Text + "," + txt_prevdue.Text + "," + txt_concession.Text + "," + cmbmop.SelectedValue + ",'" + txtDDNo.Text + "'," + cmb_accounts.SelectedValue + ",'" + txt_coments.Text + "'," + txt_balance.Text + ",'" + sms.Form1.GlobalLoginUserName.ToString() + "'," + txtlatefee.Text + ",'" + lblmonth.Text.Trim() + "','" + Nameofstudent.Trim() + "'," + Convert.ToInt32(chkpartpayment.Checked) + ",'" + cmbBankName.SelectedValue + "'", sc);

                                        SqlDataReader dr;
                                        com.Transaction = tran;
                                        try
                                        {

                                            if (com.ExecuteNonQuery() > 0)
                                            {

                                                for (int z = 0; z < dataGridView1.Rows.Count; z++)
                                                {
                                                    cmd1 = new SqlCommand();
                                                    cmd1.CommandType = CommandType.StoredProcedure;
                                                    cmd1.CommandText = "insert_tblinfo";
                                                    cmd1.Connection = sc;
                                                    cmd1.Parameters.AddWithValue("@receiptno", Convert.ToInt32(txt_receiptno.Text));
                                                    cmd1.Parameters.AddWithValue("@FEE_ID", dataGridView1.Rows[z].Cells["Fee_Id"].Value);
                                                    if (chkpartpayment.Checked == false)
                                                        cmd1.Parameters.AddWithValue("@fee", dataGridView1.Rows[z].Cells["Amount"].Value);
                                                    else
                                                        cmd1.Parameters.AddWithValue("@fee", txt_paid.Text);
                                                    cmd1.Transaction = tran;
                                                    cmd1.ExecuteNonQuery();
                                                }
                                                tran.Commit();
                                                // MsgBox.ShowBox("Student Fee Created...");
                                                LoadStudentFeeStatus();
                                                LoadGrid();
                                                if (Form1.feesms)
                                                    sms1();
                                                ////////////////////////print Receipt //////////////////////
                                                DialogResult dialogResult1 = MessageBox.Show("Your Receipt No is '" + txt_receiptno.Text + "'. Are You Sure to print Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                                if (dialogResult1 == DialogResult.Yes)
                                                {

                                                    Balance = txt_balance.Text;
                                                    Fee = txt_totalfee.Text;
                                                    total = txt_nettotal.Text;
                                                    comment = txt_coments.Text;
                                                    Prev = txt_prevdue.Text;
                                                    concession = txt_concession.Text;
                                                    Paid = txt_paid.Text;
                                                    noofmonth = txt_month.Text;
                                                    showstudentfeeledger.balance = Balance;

                                                    showstudentfeeledger.rec_no = Convert.ToInt32(txt_receiptno.Text);
                                                    showstudentfeeledger.stu_id = Convert.ToInt32(Stu_id);
                                                    showstudentfeeledger.balance = Balance;
                                                    showstudentfeeledger.Fee = Fee;
                                                    showstudentfeeledger.total = total;
                                                    showstudentfeeledger.comment = comment;
                                                    showstudentfeeledger.concession = Convert.ToDecimal(concession);
                                                    showstudentfeeledger.Paid = Paid;
                                                    showstudentfeeledger.Prev = Prev;
                                                    if (chkpartpayment.Checked == true)
                                                    {
                                                        showstudentfeeledger.monthname = "";
                                                    }
                                                    else
                                                    {

                                                        showstudentfeeledger.monthname = lblmonth.Text;
                                                    }
                                                    showstudentfeeledger.mode = 1;
                                                    showstudentfeeledger.modename = Convert.ToInt16(cmbmop.SelectedValue);
                                                    showstudentfeeledger.cheqno = "Cheque No" + txtDDNo.Text;
                                                    showstudentfeeledger obj = new showstudentfeeledger();
                                                    obj.ShowDialog();
                                                }
                                                ///////////////////////////////////////////////////////////////////////////////////////
                                                else
                                                {
                                                    Cancel_form();
                                                }


                                            }
                                            else
                                            {
                                                MsgBox.ShowBox("This Class Fee already exist");
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            tran.Rollback();
                                            MsgBox.ShowBox(ex.ToString());
                                            txt_coments.Text = ex.ToString();
                                        }
                                        finally
                                        {
                                            sc.Close();
                                        }
                                    }
                }
                else
                {
                    MsgBox.ShowBox("Fee Attached With Sibling Fee................");
                }
            }
            else
            {
                Cancel_form();
            }
        }

        
           
            
        private void sms1()
        {

            {
                //dataclass dc1 = new dataclass();
                //SqlConnection sc1 = dc1.connsms();
                //SqlDataAdapter da1 = new SqlDataAdapter("select * from  TBLSMS where userid='" + Form1.SMSKEY + "' and balance>0", sc1);
                //if (sc1 != null && sc1.State == ConnectionState.Closed)
                //{
                //    MessageBox.Show(" SMS SENT !!!");

                //}
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();

                SqlDataAdapter da = new SqlDataAdapter("select Student_Name,Class,Roll,Phone from View_StudentReport where STU_ID like '" + Stu_id + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Phone"].ToString() != "" || dt.Rows[i]["Phone"] != null)
                        {

                            try
                            {
                                DAL.smsclass SMS = new sms.DAL.smsclass();
                                string msgtxt = "Dear Parents, Your Ward, " + "  " + dt.Rows[i]["Student_Name"].ToString() + ", " + dt.Rows[i]["Class"].ToString() + " Rs. " + txt_paid.Text.Trim() + " wide Reciept No " + txt_receiptno.Text.Trim() + " received against month " + lblmonth.Text + " . Your Balance is " + txt_balance.Text + ".Thanks  "; //. further enquiry  Contact " + textBox1.Text + "," + textBox2.Text.Trim().Replace(" ", "") + ".
                                SMS.SMS(msgtxt, 0, dt.Rows[i]["Phone"].ToString().Trim().Replace(" ", ""), stuname.Text.Trim());

                            }

                            catch (Exception ex)
                            {
                                //tr.Abort();
                            }
                            finally
                            {
                               
                            }
                        }
                    }

                }
                else
                    MessageBox.Show("There is No PhoneNo for  sending  SMS. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select FRC_NO,FRC_DATE,FRC_MONTH,FRC_PREV_DUE,FRC_FEE_AMT,FRC_LATE_FINE,FRC_CONCESSION,FRC_PAID_AMT,( (FRC_FEE_AMT+FRC_LATE_FINE+FRC_PREV_DUE)-(FRC_CONCESSION+FRC_PAID_AMT)) as Balance,(case when frc_status=0 then 'Paid' else 'Cancel' end) as Status,FRC_COMMENT AS Remark from FEE_RECEIPT_T where FRC_STU_ID=" + Stu_id + " order by FRC_DATE,FRC_NO desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns["FRC_NO"].Width = 80;
            dataGridView2.Columns["FRC_NO"].HeaderText = "Reciept";
            dataGridView2.Columns["FRC_DATE"].Width = 70;
            dataGridView2.Columns["FRC_DATE"].HeaderText = "Date";
            dataGridView2.Columns["FRC_MONTH"].Width = 70;
            dataGridView2.Columns["FRC_MONTH"].HeaderText = "Month";
            dataGridView2.Columns["FRC_PREV_DUE"].Width = 100;
            dataGridView2.Columns["FRC_PREV_DUE"].HeaderText = "Prev. Dues";
            dataGridView2.Columns["FRC_FEE_AMT"].Width = 100;
            dataGridView2.Columns["FRC_FEE_AMT"].HeaderText = "Total Fee";
            dataGridView2.Columns["FRC_LATE_FINE"].Width = 100;
            dataGridView2.Columns["FRC_LATE_FINE"].HeaderText = "Late Fine";
            dataGridView2.Columns["FRC_CONCESSION"].Width = 100;
            dataGridView2.Columns["FRC_CONCESSION"].HeaderText = "Concession";
            dataGridView2.Columns["FRC_PAID_AMT"].Width = 70;
            dataGridView2.Columns["FRC_PAID_AMT"].HeaderText = "Paid";
            dataGridView2.Columns["Balance"].Width = 70;
            dataGridView2.Columns["Balance"].HeaderText = "Balance";
            dataGridView2.Columns["Status"].Width = 70;
            dataGridView2.Columns["Status"].HeaderText = "Status";
            dataGridView2.Columns["Remark"].Width = 150;
            dataGridView2.Columns["Remark"].HeaderText = "Remark";

        }
       
         private void LoadStudentFeeStatusGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select ss.* from STUDENT_FEE_STATUS ss,FEE_RECEIPT_T ft where ss.STU_ID=ft.FRC_STU_ID and  ft.FRC_NO='" + txt_receiptno.Text + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            //dataGridView3.Columns["FRC_NO"].Width = 100;
            //dataGridView3.Columns["FRC_NO"].HeaderText = "Reciept";
           
             
        }


  
        private void LoadStudentFeeStatus()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SFR_ID,SFR_STU_ID,SFR_ONCE,CASE WHEN SFR_APRIL=50000 THEN 'N/A' when SFR_APRIL=6000 THEN 'PAID'   ELSE CAST(SFR_APRIL AS VARCHAR) END AS SFR_APRIL,CASE WHEN SFR_MAY=50000 THEN 'N/A' when SFR_MAY=6000 THEN 'PAID' ELSE CAST(SFR_MAY AS VARCHAR) END  AS SFR_MAY,CASE WHEN SFR_JUNE=50000 THEN 'N/A' when SFR_JUNE=6000 THEN 'PAID' ELSE CAST(SFR_JUNE AS VARCHAR) END AS SFR_JUNE,CASE WHEN SFR_JULY=50000 THEN 'N/A' ELSE CAST(SFR_JULY AS VARCHAR) END AS SFR_JULY,CASE WHEN SFR_AUGUST=50000 THEN 'N/A' ELSE CAST(SFR_AUGUST AS VARCHAR) END AS SFR_AUGUST,CASE WHEN SFR_SEPTEMBER=50000 THEN 'N/A' ELSE CAST(SFR_SEPTEMBER AS VARCHAR)  END AS SFR_SEPTEMBER,CASE WHEN SFR_OCTOBER=50000 THEN 'N/A' ELSE CAST(SFR_OCTOBER AS VARCHAR) END AS SFR_OCTOBER,CASE WHEN SFR_NOVEMBER=50000 THEN 'N/A' ELSE CAST(SFR_NOVEMBER AS VARCHAR) END AS SFR_NOVEMBER,CASE WHEN SFR_DECEMBER=50000 THEN 'N/A' ELSE CAST(SFR_DECEMBER AS VARCHAR) END AS SFR_DECEMBER,CASE WHEN SFR_JANUARY=50000 THEN 'N/A' ELSE CAST(SFR_JANUARY AS VARCHAR) END AS SFR_JANUARY,CASE WHEN SFR_FEBRUARY=50000 THEN 'N/A' ELSE CAST(SFR_FEBRUARY AS VARCHAR) END AS SFR_FEBRUARY,CASE WHEN SFR_MARCH=50000 THEN 'N/A' ELSE CAST(SFR_MARCH AS VARCHAR) END AS SFR_MARCH from STUDENT_RECEIPT_T where SFR_STU_ID=" + Stu_id + "", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
           
                   dataGridView3.DataSource = ds.Tables[0];
                dataGridView3.Columns["SFR_ID"].Visible = false;
                dataGridView3.Columns["SFR_STU_ID"].Visible = false;
                dataGridView3.Columns["SFR_ONCE"].Visible = false;
                dataGridView3.Columns["SFR_APRIL"].Width = 80;
                dataGridView3.Columns["SFR_APRIL"].HeaderText = "APR";
                dataGridView3.Columns["SFR_MAY"].Width = 80;
                dataGridView3.Columns["SFR_MAY"].HeaderText = "MAY";
                dataGridView3.Columns["SFR_JUNE"].Width = 80;
                dataGridView3.Columns["SFR_JUNE"].HeaderText = "JUN";
                dataGridView3.Columns["SFR_JULY"].Width = 80;
                dataGridView3.Columns["SFR_JULY"].HeaderText = "JUL";
                dataGridView3.Columns["SFR_AUGUST"].Width = 80;
                dataGridView3.Columns["SFR_AUGUST"].HeaderText = "AUG";
                dataGridView3.Columns["SFR_SEPTEMBER"].Width = 80;
                dataGridView3.Columns["SFR_SEPTEMBER"].HeaderText = "SEP";
                dataGridView3.Columns["SFR_OCTOBER"].Width = 80;
                dataGridView3.Columns["SFR_OCTOBER"].HeaderText = "OCT";
                dataGridView3.Columns["SFR_NOVEMBER"].Width = 80;
                dataGridView3.Columns["SFR_NOVEMBER"].HeaderText = "NOV";
                dataGridView3.Columns["SFR_DECEMBER"].Width = 80;
                dataGridView3.Columns["SFR_DECEMBER"].HeaderText = "DEC";
                dataGridView3.Columns["SFR_JANUARY"].Width = 80;
                dataGridView3.Columns["SFR_JANUARY"].HeaderText = "JAN";
                dataGridView3.Columns["SFR_FEBRUARY"].Width = 80;
                dataGridView3.Columns["SFR_FEBRUARY"].HeaderText = "FEB";
                dataGridView3.Columns["SFR_MARCH"].Width = 80;
                dataGridView3.Columns["SFR_MARCH"].HeaderText = "MAR";
            

        }
        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Cancel Reciept!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    if (txt_receiptno.Text == "")
                    {
                        MsgBox.ShowBox("Please Enter Receipt No !!!");
                        return;
                    }

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "CANCEL_RECEIPT";
                    cmd1.Connection = sc;
                    sc.Open();
                    cmd1.Parameters.AddWithValue("@REC_NO", Convert.ToInt32(txt_receiptno.Text.Trim()));
                    cmd1.Parameters.AddWithValue("@user_name", sms.Form1.GlobalLoginUserName.ToString());
                    int i = cmd1.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MsgBox.ShowBox(" Receipt Canceled Successfully!!!");

                        //  LoadGrid();
                        
                    }
                    else
                    {
                        MsgBox.ShowBox(" Receipt can't be Canceled . it is intermediate Reciept No!!!");
                    }
                    Cancel_form();
                }
                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }




        private void txt_month_Leave(object sender, EventArgs e)
        {
            bindFee();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            Balance = txt_balance.Text;
            Fee = txt_totalfee.Text;
            total = txt_nettotal.Text;
            comment = txt_coments.Text;
            Prev = txt_prevdue.Text;
            concession = txt_concession.Text;
            Paid = txt_paid.Text;
            showstudentfeeledger.balance = Balance;

            //if (dataGridView2.Rows.Count > 0)
            //{
                showstudentfeeledger.rec_no = Convert.ToInt32(txt_receiptno.Text);
                showstudentfeeledger.stu_id = Convert.ToInt32( Stu_id);
                showstudentfeeledger.balance = Balance;
                showstudentfeeledger.Fee= Fee;
                showstudentfeeledger.total = total;
                showstudentfeeledger.comment=comment;
                showstudentfeeledger.concession = Convert.ToDecimal(concession);
                showstudentfeeledger.Paid = Paid;
                showstudentfeeledger.Prev = Prev;
                if (chkpartpayment.Checked == true)
                {
                    showstudentfeeledger.monthname = "";
                }
                else
                {

                    showstudentfeeledger.monthname = lblmonth.Text;
                }
                showstudentfeeledger.mode = 0;
                showstudentfeeledger.modename = Convert.ToInt16(cmbmop.SelectedValue);
                showstudentfeeledger.cheqno = "Cheque No - " + txtDDNo.Text;
                showstudentfeeledger obj = new showstudentfeeledger();
                obj.ShowDialog();
            //}
            //else
            //{
            //    MsgBox.ShowBox("No Records Found to be Print....");
            //}
        }
        private void BindInfo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("select * from View_showstudentfeeledger where FRC_NO='"+txt_receiptno.Text+"'", sc);
            sc.Open();

            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                cmb_class.Text = dr["CLS_DESC"].ToString() + dr["SEC_DESC"].ToString();
                bindroll();
                cmb_rollno.Text = dr["STC_ROLL_NO"].ToString();
                cmbBankName.SelectedValue = dr["frc_bank"].ToString();
                cmbmop.Text = dr["mop"].ToString();
                cmb_accounts.Text = dr["ACC_NAME"].ToString();
                txt_coments.Text = dr["FRC_COMMENT"].ToString();
                txt_concession.Text = dr["FRC_CONCESSION"].ToString();
                txt_month.Text = dr["FRC_MONTH"].ToString();
                txt_nettotal.Text = ((Convert.ToInt32(dr["FRC_FEE_AMT"].ToString()) + Convert.ToInt32(dr["FRC_LATE_FINE"].ToString()) + Convert.ToInt32(dr["FRC_PREV_DUE"].ToString())) - Convert.ToInt32(dr["FRC_CONCESSION"].ToString())).ToString();
                txt_paid.Text = dr["FRC_PAID_AMT"].ToString();
                txt_prevdue.Text = dr["FRC_PREV_DUE"].ToString();
                txt_totalfee.Text = dr["FRC_FEE_AMT"].ToString();
                txtlatefee.Text = dr["FRC_LATE_FINE"].ToString();
                txt_balance.Text=((Convert.ToInt32( txt_nettotal.Text)- Convert.ToInt32(dr["FRC_PAID_AMT"].ToString()))).ToString();
                txtDDNo.Text = dr["FRC_ddno"].ToString();
                txtstuno.Text = dr["STU_ADM_NO"].ToString();               
                stuname.Text = dr["STU_FIRST_NAME"].ToString() + dr["STU_MID_NAME"].ToString() + dr["STU_LAST_NAME"].ToString();
                lblmonth.Text = dr["Month"].ToString();
                sc.Close();
                   
            }
        }
        private bool CheckReceiptNo()
        {
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool retno = true;
            cmd1 = new SqlCommand("select * from  FEE_RECEIPT_T where FRC_NO="+txt_receiptno.Text+" ", sc);
            sc.Open();
            dr = cmd1.ExecuteReader();
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
        private void txt_receiptno_Leave(object sender, EventArgs e)
        {
               BindInfo();
              if (CheckReceiptNo())
            {
                btnprint.Enabled = true;
                btn_create.Enabled = false;
                button1.Enabled = true;
                txt_month.Enabled = false;
                cmb_class.Enabled = false;
                cmb_rollno.Enabled = false;
                txtlatefee.Enabled = false;
                txt_paid.Enabled = false;
                cmbmop.Enabled = false;
                cmb_accounts.Enabled = false;
                txtstuno.Enabled = false;
                 dataclass dc = new dataclass();
                SqlConnection sc = dc.con();


                

                SqlDataAdapter ada = new SqlDataAdapter("select * from View_showstudentfeeledger where FRC_NO='" + txt_receiptno.Text + "'", sc);

                DataTable dt = new DataTable();
                ada.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["STU_ADM_NO"].ToString();
                    txt_date.Text = dt.Rows[0]["FRC_DATE"].ToString();

                    stuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + " " + dt.Rows[0]["STU_MID_NAME"].ToString() + " " + dt.Rows[0]["STU_LAST_NAME"].ToString();

                    Stu_id =dt.Rows[0]["STU_ID"].ToString();

                    //partfee = dt.Rows[0]["FEE_DESC"].ToString();
                    //if (partfee == "PART FEE")
                    //{
                    //    chkpartpayment.Checked = true;
                       
                    //}
                    //else
                    //{
                    //    chkpartpayment.Checked = false ;

                    //}

                }

                dt.Rows.Clear();


                SqlDataAdapter da1 = new SqlDataAdapter("select FEE_DESC as Fee,FRD_FEE_AMT as Amount from  ViewFeeRecieptDetail where FRC_NO=" + txt_receiptno.Text + "", sc);
                DataTable fee = new DataTable();
                da1.Fill(fee);
                dataGridView1.DataSource = fee;
                dataGridView1.Columns["Fee"].Width = 200;
                dataGridView1.Columns["Amount"].Width = 100;
                LoadStudentFeeStatus();
                LoadGrid();
            }
              listBox1.Visible = false;
            
         
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_form();
         
        }

        private void txt_paid_KeyUp(object sender, KeyEventArgs e)
        {
          
        }

     

        private void txt_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

       
        private void txt_prevdue_Leave(object sender, EventArgs e)
        {
            balanceCalculate();
        }

        private void txt_concession_Leave(object sender, EventArgs e)
        {
            balanceCalculate();
        }

        private void bindFee()
        {
            if (txt_month.Text == "")
            {
                num = 1;
            }
            else
            {
                num = Convert.ToInt32(txt_month.Text);
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Search_Month ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", num);
            da.SelectCommand.Parameters.AddWithValue("@stu_id", Stu_id);
            da.SelectCommand.Parameters.AddWithValue("@part_payment",Convert.ToInt32(chkpartpayment.Checked));

            sc.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Fee_Id"].Visible = false;
            dataGridView1.Columns["fee"].Width = 200;
            dataGridView1.Columns["Amount"].Width = 100;
           // dataGridView1.Columns["discount"].Width = 100;
            //dataGridView1.Columns["Name"].Visible = false;
            decimal sum = 0,Discount=0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sum = sum + Convert.ToDecimal(dt.Rows[i][2].ToString());
                    Discount = Discount + Convert.ToDecimal(dt.Rows[i][3].ToString());
                }
                //txt_concession.Text = dt.Rows[0]["Discount"].ToString();
                Nameofstudent = dt.Rows[0]["Name"].ToString();
            }
            //////////////MONTHNAME/////////////////////////
           
            da = new SqlDataAdapter("Search_Month_NAME ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", num);
            da.SelectCommand.Parameters.AddWithValue("@stu_id", Stu_id);
            da.SelectCommand.Parameters.AddWithValue("@part_payment", Convert.ToInt32(chkpartpayment.Checked));
            DataTable MONTHNAME = new DataTable();
            da.Fill(MONTHNAME);
            if (MONTHNAME.Rows.Count > 0)
                lblmonth.Text = MONTHNAME.Rows[0][0].ToString();
            else
                lblmonth.Text = " ";
          ///////////////////find Late Fee//////////////////
            DateTime dtdob = Convert.ToDateTime(txt_date.Value, System.Globalization.CultureInfo.InvariantCulture);
            da = new SqlDataAdapter("Find_Late_Fee ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", num);
            da.SelectCommand.Parameters.AddWithValue("@stu_id", Stu_id);
            da.SelectCommand.Parameters.AddWithValue("@date", dtdob.ToString("MM/dd/yyyy"));
            da.SelectCommand.Parameters.AddWithValue("@part_payment", Convert.ToInt32(chkpartpayment.Checked));
            DataTable latefee = new DataTable();
           da.Fill(latefee);
           if (latefee.Rows.Count>0)
            txtlatefee.Text = latefee.Rows[0][0].ToString();
           else
               txtlatefee.Text = "0";
///////////////////////////////////////////////////////////////////////////////
         
            txt_totalfee.Text = (sum).ToString();
            txt_prevdue.Text = dues.ToString();
            txt_concession.Text = Discount.ToString();
          balanceCalculate();
          if (chkpartpayment.Checked == true)
              dataGridView1.Visible = false;
          else
              dataGridView1.Visible = true;

        }

 

        private void txtlatefee_Leave(object sender, EventArgs e)
        {
            balanceCalculate();
        }

        private void txt_paid_Leave_1(object sender, EventArgs e)
        {
            decimal sum1 = Convert.ToDecimal(txt_concession.Text);
            decimal paidAmount = (Convert.ToDecimal(txt_nettotal.Text) - sum1);
          
            txt_balance.Text = (paidAmount - Convert.ToDecimal(txt_paid.Text)).ToString();
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            { dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;
               
                if(cmb_rollno.SelectedValue==null)
               da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);
 
                else
                da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BIND_DATA();


                }
                else
                {
                    txtstuno.Text = "";
                }
            }
        }

        private void txt_month_Leave_1(object sender, EventArgs e)
        {
            bindFee();
        }
        private void bindstuadmleave()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'and stu_status='y'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                dataclass dc1 = new dataclass();
                SqlConnection sc1 = dc.con();
                SqlDataAdapter da1 = new SqlDataAdapter("select * from STUDENT_FEE_EXEMPTED_T  T1,View_studentReport T2 where  T2.AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'and T1.EXEMPTION='1'  AND T1.STU_ID=T2.STU_ID ", sc);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    MessageBox.Show("Fee Exempted");
                }
                dataclass dc2 = new dataclass();
                SqlConnection sc2 = dc.con();
                SqlDataAdapter da2 = new SqlDataAdapter("select * from STUDENT_T WHERE STU_ADM_NO like '" + txtstuno.Text.Trim().Replace(" ", "") + "'and STU_WARD='R' ", sc);
                DataTable dt2 = new DataTable();
                da1.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    MessageBox.Show(" RTE Student...");
                }
                cmb_class.Enabled = false;
                cmb_rollno.Enabled = false;
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                txtstuno.Leave += new EventHandler(cmb_class_Leave);
                ROLL = dt.Rows[0]["ROLL"].ToString();


                stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                listBox1.Visible = false;
                phone = dt.Rows[0]["phone"].ToString();
                BIND_DATA(); btn_create.Focus();

                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;

            }
            else
            {
               // MsgBox.ShowBox("This Adm No does not exit !!!");
                cmb_class.SelectedValue = 0;
                ROLL = "0";
                Stu_id = "0";
                stuname.Text = "";
                BIND_DATA(); phone = "";
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
        }


        private void txtstuno_Leave(object sender, EventArgs e)
        {
            bindstuadmleave();
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            bindroll();
        }
        private void bindroll()
        {
            {

                string StateName;
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    StateName = "0";
                }
                else
                {
                    StateName = cmb_class.SelectedValue.ToString();
                }

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where STC_SEC_ID like  '" + StateName + "' and stu_status='y' order by  ROLL", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataRow row = dt.NewRow();
                    row["ROLL"] = "0";
                    row["STU_ID"] = 0;
                    dt.Rows.InsertAt(row, 0);
                    cmb_rollno.DataSource = dt;
                    cmb_rollno.DisplayMember = "ROLL";
                    cmb_rollno.ValueMember = "STU_ID";
                    cmb_rollno.Text = ROLL;
                }
            }
        }

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AccNo like '" + txtCardNo.Text + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                phone = dt.Rows[0]["phone"].ToString();
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                bindroll();
                cmb_rollno.Text = dt.Rows[0]["Roll"].ToString();
                stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtstuno.Text = dt.Rows[0]["admno"].ToString();
                BIND_DATA();
                listBox1.Visible = false;
                btn_create.Focus();
            }
            else
            {
                Stu_id = "0";
                stuname.Text = "";
                txtstuno.Text = "";
                BIND_DATA();
                listBox1.Visible = false;
            }

        }

        

        private void txt_month_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txt_receiptno_KeyPress(object sender, KeyPressEventArgs e)
       {
            v1.digitonly(e);
            

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txtstuno.Text == "")
            {
                MsgBox.ShowBox("Adm. no. should not be blank in fee collection form....");
            }
            else
            {
                if (checkno() == true)
                {
                    frmFeeCollectionComment obj = new frmFeeCollectionComment();
                    obj.lblAdm = txtstuno.Text;
                    obj.phone = phone;
                    obj.ShowDialog();
                }
                else
                {
                    MsgBox.ShowBox("Invalid Adm no. ");
                }
            }
        }

        private bool checkno()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            bool ret;
            SqlCommand com = new SqlCommand("select * from STUDENT_T where STU_ADM_NO=" + txtstuno.Text + "", sc);
            sc.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                ret = true;
            }
            else
            {
                ret = false;
            }

            return ret;
            sc.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmStudentFee obj = new frmStudentFee();
            obj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmStuBusstop obj = new FrmStuBusstop();
            obj.ShowDialog();
        }

        private void txtCardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtCardNo_Leave();

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    txtstuno.Text = str[0].ToString();
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

        private void stuname_TextChanged(object sender, EventArgs e)
        {
            {

                string Name, CLS;

                if (stuname.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = stuname.Text.Trim().ToString();
                }
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    CLS = "%";
                }
                else
                {
                    CLS = cmb_class.SelectedValue.ToString();
                }
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_cls_id like'" + CLS + "' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '|' + ds1.Tables[0].Rows[j][1].ToString() + '|' + ds1.Tables[0].Rows[j][2].ToString() + '|' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = stuname.Top + stuname.Height;
                    //listBox1.Width = txtstuno.Width;
                    listBox1.Left = stuname.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }

        }

        private void chkpartpayment_CheckedChanged(object sender, EventArgs e)
        {
            bindFee();
        }
        private void bindAccount1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblledger where id in (2)  order by id  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["ACC_NAME"] = "-Select-";
            //row["ACC_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_accounts.DataSource = ds.Tables[0];
            cmb_accounts.DisplayMember = "NAME";
            cmb_accounts.ValueMember = "id";
        }
        private void cmbmop_Leave(object sender, EventArgs e)
        {
            if (cmbmop.Text != "Cash")
            {
               // bindAccount1();
                cmbBankName.Enabled = true;
                txtDDNo.Enabled = true;
                cmbBankName.Focus();

            }
            else
            {
                cmbBankName.Enabled = false;
                txtDDNo.Enabled = false;
                txt_paid.Focus();
            }
        }

        private void cmbBankName_Leave(object sender, EventArgs e)
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();

            //SqlDataAdapter da = new SqlDataAdapter("select * from tblbank where bank_name='" + cmbBankName.Text + "' ", sc);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{

            //}

            //else
            //{
            //    if (cmbBankName.Text != "Select")
            //    {
            //        cmbBankName.Text = "";
            //        MsgBox.ShowBox("Bank Not Found !!!!");
            //        if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Bank ??"))
            //        {
            //            bank = 0;
            //            frmBankmaster obj = new frmBankmaster();
            //            obj.ShowDialog();
            //            bindBank();
            //            cmbBankName.Focus();
            //        }
            //    }
            //}
            //txtDDNo.Focus();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex1 = e.RowIndex;
        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
            decimal sum = 0, Discount = 0;
            string Remarks = "Concession in ";
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {

                    sum = sum + Convert.ToDecimal(dataGridView1.Rows[i].Cells["Amount"].Value);
                    Discount = Discount + Convert.ToDecimal(dataGridView1.Rows[i].Cells["Discount"].Value);
                    if (Convert.ToDecimal(dataGridView1.Rows[i].Cells["Discount"].Value) > 0)
                    {
                        Remarks = Remarks + (dataGridView1.Rows[i].Cells["Fee"].Value).ToString()+",";
                    }
                }
                    txt_totalfee.Text = (sum).ToString();
                    txt_concession.Text = Discount.ToString();
                    txt_prevdue.Text = dues.ToString();
                    txt_coments.Text = Remarks.ToString();
                    balanceCalculate();
               
            }


        private void cmb_rollno_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BIND_DATA();
                    listBox1.Visible = false;

                }
                else
                {
                    txtstuno.Text = "";
                }
            }
        }

        private void cmb_rollno_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BIND_DATA();

                    listBox1.Visible = false;
                }
                else
                {
                    txtstuno.Text = "";
                }
            }
        }

        private void cmb_rollno_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BIND_DATA();

                    listBox1.Visible = false;
                }
                else
                {
                    txtstuno.Text = "";
                }
            }
        }

        private void txtCardNo_Leave()
        {

        }

        private void txt_balance_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_balance_Leave(object sender, EventArgs e)
        {

        }

        private void txt_receiptno_TextChanged(object sender, EventArgs e)
        {

        }
        }

        
     
      

        
  }



