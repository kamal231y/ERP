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
   
    public partial class FrmFeeReciptUP : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader; 
        validation v1 = new validation();
        int ret;
        int Stuid;
        SqlCommand cmd1;
        decimal num;
        string month;
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
         string ROLL;
         public FrmFeeReciptUP()
        {
            InitializeComponent();
             Cancel_form();
            
        }


        void Cancel_form()
        {
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
            cmbmop.Text = "-Select-";
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
            txtCardNo.Focus();      
            
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
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblmop order by id ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["mop"] = "CASH";
            row["id"] = 1;
            ds.Tables[0].Rows.InsertAt(row, 0);

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
            SqlDataAdapter da = new SqlDataAdapter(" select * from ACCOUNTS_T order by ACC_ID  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["ACC_NAME"] = "-Select-";
            //row["ACC_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_accounts.DataSource = ds.Tables[0];
            cmb_accounts.DisplayMember = "ACC_NAME";
            cmb_accounts.ValueMember = "ACC_ID";
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
                    
                               
                   
                    decimal sum = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                         sum = sum + Convert.ToDecimal(dt.Rows[i][2].ToString());
                    }
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["Fee_Id"].Visible = false;
                    dataGridView1.Columns["Fee"].Width = 200;
                    dataGridView1.Columns["Amount"].Width = 100;
                    
                    txt_totalfee.Text = (sum).ToString();
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

            txt_nettotal.Text = (Convert.ToDecimal(txt_totalfee.Text) + dues + Convert.ToDecimal(txtlatefee.Text)).ToString();
            decimal sum1 = Convert.ToDecimal(txt_concession.Text);
            decimal paidAmount = (Convert.ToDecimal(txt_nettotal.Text) - sum1);
            txt_paid.Text = paidAmount.ToString();
            txt_balance.Text = (paidAmount - Convert.ToDecimal(txt_paid.Text)).ToString();
        }


        private void btn_create_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_T where STU_ID=" + Stu_id + " and STU_FEE_ATTACHED=0", sc);
            DataTable sibling = new DataTable();
            da.Fill(sibling);
            if (sibling.Rows.Count > 0)
            {
                DateTime dt1 = Convert.ToDateTime(txt_date.Value, System.Globalization.CultureInfo.InvariantCulture);
                //DateTime dt2 = txt_date.Text; //Convert.ToDateTime(txt_date.Text, System.Globalization.CultureInfo.InvariantCulture);
                // DateTime dt3 = Convert.ToDateTime(DDdate.Text, System.Globalization.CultureInfo.InvariantCulture);

                tran = sc.BeginTransaction();
               
                if (cmb_class.SelectedValue.ToString() == "0" || cmb_class.Text == "")
                {
                    MsgBox.ShowBox("Please Select Class...");
                }
                else
                    if (cmb_rollno.Text == "")
                    {
                        MsgBox.ShowBox("Please Select Roll No...");
                    }

                    else
                        if (cmbmop.Text == "-Select-" || cmbmop.Text == "")
                        {
                            MsgBox.ShowBox("Please Select MOP ...");
                        }
                        else
                            if (cmb_accounts.SelectedValue.ToString() == "0" || cmb_accounts.Text == "")
                            {
                                MsgBox.ShowBox("Please Select Account ...");
                            }
                            else
                                if (dataGridView1.Rows.Count==0)
                                {
                                    MsgBox.ShowBox("No Fee To Save ......");
                                }
                            else
                            {
                                SqlCommand com = new SqlCommand("Insert_student_FeeReciept " + txt_receiptno.Text + ",'" + dt1.ToString("MM/dd/yyyy") + "'," + Stu_id + "," + num + "," + txt_totalfee.Text + "," + txt_paid.Text + "," + txt_prevdue.Text + "," + txt_concession.Text + "," + cmbmop.SelectedValue + ",'" + txtDDNo.Text + "'," + cmb_accounts.SelectedValue + ",'" + txt_coments.Text + "'," + txt_balance.Text + "," + txtlatefee.Text + ",'" + lblmonth.Text.Trim() + "',"+Convert.ToInt32(chkpartpayment.Checked)+"", sc);
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
                                            if(chkpartpayment.Checked==false)
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
                                        if(Form1.SMS)
                                          sms1();
                                        ////////////////////////print Receipt //////////////////////
                                        DialogResult dialogResult = MessageBox.Show("Are You Sure to print Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                        if (dialogResult == DialogResult.Yes)
                                        {
                                            Balance = txt_balance.Text;
                                            Fee = txt_totalfee.Text;
                                            total = txt_nettotal.Text;
                                            comment = txt_coments.Text;
                                            Prev = txt_prevdue.Text;
                                            concession = txt_concession.Text;
                                            Paid = txt_paid.Text;
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
                                            showstudentfeeledger.monthname = lblmonth.Text;

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
                                    MsgBox.ShowBox("error...");
                                }
                                finally
                                {
                                    sc.Close();
                                }
                            }
            }
            else
            {
                MsgBox.ShowBox(" Fee Attached With Sibling Fee................");
            }
            }
        private void sms1()
        {

            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select Student_Name,Class,Roll,Phone from View_StudentReport where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
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
                                string rollno, sec;

                                // 'msgtxt = "Dear Parents,Kindly Pay, Your Ward " + "  " + stuname + ", " + class + "-" + sec + ", Roll No." + rollno + " Fee's Rs. " + amt + "   before " + bdate + "  * Thanks"
                                string msgtxt = "Dear Parents, Thanks for Pay,Your Ward, " + "  " + dt.Rows[i]["Student_Name"].ToString() + ", " + dt.Rows[i]["Class"].ToString() + " , Roll No." + dt.Rows[i]["Roll"].ToString() + " Fee's Rs. " + txt_paid.Text + "  on " + txt_date.Text + ".Now your remaining balance is Rs. " + txt_balance.Text + ". Thanks ";
                                da = new SqlDataAdapter("select * from  TBLSMS", sc);
                                DataTable dtsms = new DataTable();
                                da.Fill(dtsms);
                                if (dtsms.Rows.Count > 0)
                                {
                                    string msgid = dtsms.Rows[0]["userid"].ToString().Trim();
                                    string msgname = dtsms.Rows[0]["Name"].ToString().Trim();
                                    string msgpwd = dtsms.Rows[0]["password"].ToString().Trim();
                                    //string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=cra-santosh13&password=98765&type=0&dlr=1&destination=8743007745&source=NVSPSH&message=" + msgtxt + "";

                                    string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=" + msgid + "&password=" + msgpwd + "&type=0&dlr=1&destination=" + dt.Rows[i]["Phone"].ToString().Trim() + "&source=" + msgname + "&message=" + msgtxt + "";
                                    data = client.OpenRead(baseurl);
                                    reader = new StreamReader(data);
                                    string s = reader.ReadToEnd();
                                }
                                //tr.Abort();


                                //       MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                            }

                            catch (Exception ex)
                            {
                                //tr.Abort();
                                MessageBox.Show("SMS not send... !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                data.Close();
                                reader.Close();
                            }
                        }
                    }
                    //MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                }
                else
                    MessageBox.Show("There is No PhoneNo for  sending  SMS. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);


            }
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select FRC_NO,FRC_DATE,FRC_MONTH,FRC_PREV_DUE,FRC_FEE_AMT,FRC_LATE_FINE,FRC_CONCESSION,FRC_PAID_AMT,( (FRC_FEE_AMT+FRC_LATE_FINE+FRC_PREV_DUE)-(FRC_CONCESSION+FRC_PAID_AMT)) as Balance,(case when frc_status=0 then 'Paid' else 'Cancel' end) as Status from FEE_RECEIPT_T where FRC_STU_ID=" + Stu_id + " order by FRC_DATE desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns["FRC_NO"].Width = 100;
            dataGridView2.Columns["FRC_NO"].HeaderText = "Reciept";
            dataGridView2.Columns["FRC_DATE"].Width = 100;
            dataGridView2.Columns["FRC_DATE"].HeaderText = "Date";
            dataGridView2.Columns["FRC_MONTH"].Width = 100;
            dataGridView2.Columns["FRC_MONTH"].HeaderText = "Month";
            dataGridView2.Columns["FRC_PREV_DUE"].Width = 120;
            dataGridView2.Columns["FRC_PREV_DUE"].HeaderText = "Prev. Dues";
            dataGridView2.Columns["FRC_FEE_AMT"].Width = 120;
            dataGridView2.Columns["FRC_FEE_AMT"].HeaderText = "Total Fee";
            dataGridView2.Columns["FRC_LATE_FINE"].Width = 120;
            dataGridView2.Columns["FRC_LATE_FINE"].HeaderText = "Late Fine";
            dataGridView2.Columns["FRC_CONCESSION"].Width = 100;
            dataGridView2.Columns["FRC_CONCESSION"].HeaderText = "Concession";
            dataGridView2.Columns["FRC_PAID_AMT"].Width = 100;
            dataGridView2.Columns["FRC_PAID_AMT"].HeaderText = "Paid";
            dataGridView2.Columns["Balance"].Width = 70;
            dataGridView2.Columns["Balance"].HeaderText = "Balance";
            dataGridView2.Columns["Status"].Width = 100;
            dataGridView2.Columns["Status"].HeaderText = "Status";

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
            SqlDataAdapter da = new SqlDataAdapter(" select SFR_ID,SFR_STU_ID,SFR_ONCE,CASE WHEN SFR_APRIL=50000 THEN 'N/A' ELSE CAST(SFR_APRIL AS VARCHAR) END AS SFR_APRIL,CASE WHEN SFR_MAY=50000 THEN 'N/A' ELSE CAST(SFR_MAY AS VARCHAR) END  AS SFR_MAY,CASE WHEN SFR_JUNE=50000 THEN 'N/A' ELSE CAST(SFR_JUNE AS VARCHAR) END AS SFR_JUNE,CASE WHEN SFR_JULY=50000 THEN 'N/A' ELSE CAST(SFR_JULY AS VARCHAR) END AS SFR_JULY,CASE WHEN SFR_AUGUST=50000 THEN 'N/A' ELSE CAST(SFR_AUGUST AS VARCHAR) END AS SFR_AUGUST,CASE WHEN SFR_SEPTEMBER=50000 THEN 'N/A' ELSE CAST(SFR_SEPTEMBER AS VARCHAR)  END AS SFR_SEPTEMBER,CASE WHEN SFR_OCTOBER=50000 THEN 'N/A' ELSE CAST(SFR_OCTOBER AS VARCHAR) END AS SFR_OCTOBER,CASE WHEN SFR_NOVEMBER=50000 THEN 'N/A' ELSE CAST(SFR_NOVEMBER AS VARCHAR) END AS SFR_NOVEMBER,CASE WHEN SFR_DECEMBER=50000 THEN 'N/A' ELSE CAST(SFR_DECEMBER AS VARCHAR) END AS SFR_DECEMBER,CASE WHEN SFR_JANUARY=50000 THEN 'N/A' ELSE CAST(SFR_JANUARY AS VARCHAR) END AS SFR_JANUARY,CASE WHEN SFR_FEBRUARY=50000 THEN 'N/A' ELSE CAST(SFR_FEBRUARY AS VARCHAR) END AS SFR_FEBRUARY,CASE WHEN SFR_MARCH=50000 THEN 'N/A' ELSE CAST(SFR_MARCH AS VARCHAR) END AS SFR_MARCH from STUDENT_RECEIPT_T where SFR_STU_ID=" + Stu_id + "", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
           
                   dataGridView3.DataSource = ds.Tables[0];
                dataGridView3.Columns["SFR_ID"].Visible = false;
                dataGridView3.Columns["SFR_STU_ID"].Visible = false;
                dataGridView3.Columns["SFR_ONCE"].Visible = false;
                dataGridView3.Columns["SFR_APRIL"].Width = 80;
                dataGridView3.Columns["SFR_APRIL"].HeaderText = "JUL";
                dataGridView3.Columns["SFR_MAY"].Width = 80;
                dataGridView3.Columns["SFR_MAY"].HeaderText = "AUG";
                dataGridView3.Columns["SFR_JUNE"].Width = 80;
                dataGridView3.Columns["SFR_JUNE"].HeaderText = "SEP";
                dataGridView3.Columns["SFR_JULY"].Width = 80;
                dataGridView3.Columns["SFR_JULY"].HeaderText = "OCT";
                dataGridView3.Columns["SFR_AUGUST"].Width = 80;
                dataGridView3.Columns["SFR_AUGUST"].HeaderText = "NOV";
                dataGridView3.Columns["SFR_SEPTEMBER"].Width = 80;
                dataGridView3.Columns["SFR_SEPTEMBER"].HeaderText = "DEC";
                dataGridView3.Columns["SFR_OCTOBER"].Width = 80;
                dataGridView3.Columns["SFR_OCTOBER"].HeaderText = "JAN";
                dataGridView3.Columns["SFR_NOVEMBER"].Width = 80;
                dataGridView3.Columns["SFR_NOVEMBER"].HeaderText = "FEB";
                dataGridView3.Columns["SFR_DECEMBER"].Width = 80;
                dataGridView3.Columns["SFR_DECEMBER"].HeaderText = "MAR";
                dataGridView3.Columns["SFR_JANUARY"].Width = 80;
                dataGridView3.Columns["SFR_JANUARY"].HeaderText = "APR";
                dataGridView3.Columns["SFR_FEBRUARY"].Width = 80;
                dataGridView3.Columns["SFR_FEBRUARY"].HeaderText = "MAY";
                dataGridView3.Columns["SFR_MARCH"].Width = 80;
                dataGridView3.Columns["SFR_MARCH"].HeaderText = "JUN";
            

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
                showstudentfeeledger.monthname = lblmonth.Text;
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
              //  cmbBankName.Text = dr["Bank_Name"].ToString();
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
                //   txtDDNo.Text = dr["FRC_ddno"].ToString();
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

                    stuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + " " + dt.Rows[0]["STU_MID_NAME"].ToString() + " " + dt.Rows[0]["STU_LAST_NAME"].ToString();

                    Stu_id =dt.Rows[0]["STU_ID"].ToString();

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
            SqlDataAdapter da = new SqlDataAdapter("Search_Month", sc);
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
            decimal sum = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sum = sum + Convert.ToDecimal(dt.Rows[i][2].ToString());
                }
            }
            //////////////MONTHNAME/////////////////////////
            da = new SqlDataAdapter("Search_Month_NAME_UP ", sc);
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
            da = new SqlDataAdapter("Find_Late_Fee ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", num);
            da.SelectCommand.Parameters.AddWithValue("@stu_id", Stu_id);
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

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ","") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                ROLL = dt.Rows[0]["ROLL"].ToString();
                stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
            else
            {
                cmb_class.SelectedValue = 0;
                ROLL = "0";
                Stu_id = "0";
                stuname.Text = "";
                BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }

        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            bindroll();
        }
        private void bindroll()
        {
            {
                dataGridView1.DataSource = DBNull.Value;

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
                SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataRow row = dt.NewRow();
                    row["STC_ROLL_NO"] = "0";
                    row["STC_STU_ID"] = 0;
                    dt.Rows.InsertAt(row, 0);
                    cmb_rollno.DataSource = dt;
                    cmb_rollno.DisplayMember = "STC_ROLL_NO";
                    cmb_rollno.ValueMember = "STC_STU_ID";
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
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmb_rollno.Text = dt.Rows[0]["Roll"].ToString();
                stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                BIND_DATA();

            }
            else
            {
                Stu_id = "0";
                stuname.Text = "";
                BIND_DATA();
            }

        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AccNo like '" + txtCardNo.Text + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                txtCardNo.TextChanged += new EventHandler(cmb_class_Leave);
                cmb_rollno.SelectedValue = dt.Rows[0]["STU_ID"].ToString();
                stuname.Text = dt.Rows[0]["Student_Name"].ToString();
                BIND_DATA();

            }
            else
            {
                Stu_id = "0";
                stuname.Text = "";
                BIND_DATA();
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

      

        
        }


    }

