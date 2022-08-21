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
    public partial class frmRegistrationFormSale : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public static string Sname;
        public static string InqNo;
        public static string FNo;
        string word;
         

        public frmRegistrationFormSale()

           
        {
            InitializeComponent();
            Cancil_form();
            InitGrid();
        }
           

        private void btn_create_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpdate = Convert.ToDateTime(dtpDate.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpafrom = Convert.ToDateTime(dtpFromAdmission.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpato = Convert.ToDateTime(dtpToAdmission.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpcon = Convert.ToDateTime(dtpCounselling.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpdob = Convert.ToDateTime(dtpDOB.Value, System.Globalization.CultureInfo.InvariantCulture);
            
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtName.Text == "")
            {
                MsgBox.ShowBox("Name should not be blank");
                return;
            }
            SqlCommand com = new SqlCommand("Insert_tblFormRegistration'" + Convert.ToInt32(txtBillNo.Text) + "','" + dtpdate.ToString("MM/dd/yyyy") + "','" + (txtName.Text) + "','" + txtFormNo.Text + "','" + Convert.ToDecimal(txtAmount.Text) + "','" + (cmbMode.Text) + "','" + Convert.ToInt32(cmbAccounts.SelectedValue.ToString()) + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1.ToString("MM/dd/yyyy") + "','" + dtpdob.ToString("MM/dd/yyyy") + "','" + txtMob.Text+ "','" + txtFatherName.Text + "','" + txtMotherName.Text + "','" + cmbClass.Text + "','" + cmbGender.Text + "','"+ cmbsession.Text +"'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Form Sale Created...");
                    DialogResult dialogResult = MessageBox.Show(" Are You Sure to print Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        print();
                    }
                    DialogResult dialogResult1 = MessageBox.Show(" Are You Sure to send an sms!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult1 == DialogResult.Yes)
                    {
                        sms1();
                        MessageBox.Show("Message sent successfully.");
                    }
                    Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Form is already Sale");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sc.Close();
            }

        }
        private void sms1()
        {

            try
            {

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,Your Ward, " + "  " + txtName.Text + " is enroll in our school.His/her Form No. is " + txtFormNo.Text.Trim() + ". Thanks ";
                DAL.smsclass SMS = new sms.DAL.smsclass();
                SMS.SMS(msgtxt, 0, txtMob.Text.Trim().Replace(" ", ""), txtName.Text);
            }

            catch (Exception ex)
            {
                //tr.Abort();
                MessageBox.Show("SMS not send... !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //data.Close();
                //reader.Close();
            }

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
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtBillNo.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtMob.Text = "";
            cmbClass.Text = "Select";
            cmbGender.Text = "Select";
            dtpDOB.Text = ""; 
            dtpDate.Text = "";
            txtName.Text = "";
            txtFormNo.Text = "";
            txtAmount.Text = "";
            cmbAccounts.Text = "Cash/Ac";
            cmbMode.Text = "Cash";
            BindBillNo();
            
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Billno"].Width = 90;
            dataGridView1.Columns["Bdate"].Width = 110;
            dataGridView1.Columns["Name"].Width = 150;
            dataGridView1.Columns["FATHERNAME"].Width = 150;
            dataGridView1.Columns["MOTHERNAME"].Width = 150;
            dataGridView1.Columns["FormNoid"].Width = 90;
            dataGridView1.Columns["session"].Width = 100;
            dataGridView1.Columns["Amount"].Width = 90;
            dataGridView1.Columns["Mode"].Width = 120;
            dataGridView1.Columns["Billno"].HeaderText = "Bill No.";
            dataGridView1.Columns["Bdate"].HeaderText = "Bill Date";
            dataGridView1.Columns["Name"].HeaderText = "Name";
            dataGridView1.Columns["FATHERNAME"].HeaderText = "FATHERNAME";
            dataGridView1.Columns["MOTHERNAME"].HeaderText = "MOTHERNAME";
            dataGridView1.Columns["session"].HeaderText = "Session";
            dataGridView1.Columns["FormNoid"].HeaderText = "Form No.";
            dataGridView1.Columns["Amount"].HeaderText = "Amount";
            dataGridView1.Columns["Mode"].HeaderText = "Mode";
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id,Billno,Bdate,Name,FATHERNAME,MOTHERNAME,Mobile,session,FormNoid,Amount,Mode from tblFormRegistration order by FormNoid", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void BindAccunt()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd = new SqlCommand("select * from ACCOUNTS_T where ACC_ID=1", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["ACC_NAME"] = "Cash/Ac";
            row["ACC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbAccounts.DataSource = dt;
            cmbAccounts.DisplayMember = "ACC_NAME";
            cmbAccounts.ValueMember = "ACC_ID";
            sc.Close();
        }
        private void bindSession()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from session_T  order by id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["Session"] = "2015-2016";
            //row["id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "Session";
            cmbsession.ValueMember = "id";

        }
        private void frmRegistrationFormSale_Load(object sender, EventArgs e)
        {

            BindBillNo(); BindAccunt(); bindclass(); bindSession();
            //if (InqNo != null)
            //{
            //    dataclass dc = new dataclass();
            //    SqlConnection sc = dc.con();
            //    SqlDataAdapter sda = new SqlDataAdapter("select * from view_tblInquiry where InqNo=" + InqNo + "", sc);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        txtName.Text = dt.Rows[0]["Studentname"].ToString();
            //        txtMob.Text = dt.Rows[0]["Mobileno"].ToString();
            //        txtFatherName.Text = dt.Rows[0]["fatherName"].ToString();
            //        txtMotherName.Text = dt.Rows[0]["MotherName"].ToString();
            //        cmbClass.Text = dt.Rows[0]["cls_desc"].ToString();
            //        cmbGender.Text = dt.Rows[0]["Gender"].ToString();
            //        dtpDOB.Text = dt.Rows[0]["DOB"].ToString();
            //        cmbsession.Text = dt.Rows[0]["session"].ToString();
            //    }
            //}
        }

        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select *  from CLASS_T WHERE CLS_DP!='N'  order by Cls_SEQUENCE ", sc);
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

        private void BindBillNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select max(BillNo) from tblFormRegistration", sc);
            DataTable  dt = new DataTable();
            adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "")
                    txtBillNo.Text = "1";
                else

                    txtBillNo.Text = (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtMob.Text = dataGridView1.CurrentRow.Cells["Mobile"].Value.ToString();
                txtRegid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                txtBillNo.Text = dataGridView1.CurrentRow.Cells["BillNo"].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                txtFormNo.Text = dataGridView1.CurrentRow.Cells["FormNoid"].Value.ToString();
                txtAmount.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();
                dtpDate.Text = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["BDate"].Value).ToString("dd/MM/yyyy");
                cmbMode.Text = dataGridView1.CurrentRow.Cells["Mode"].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells["FATHERNAME"].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells["MOTHERNAME"].Value.ToString();
            
              //  cmbAccounts.Text = dataGridView1.CurrentRow.Cells["Ledger"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpdate = Convert.ToDateTime(dtpDate.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpafrom = Convert.ToDateTime(dtpFromAdmission.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpato = Convert.ToDateTime(dtpToAdmission.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpcon = Convert.ToDateTime(dtpCounselling.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpdob = Convert.ToDateTime(dtpDOB.Value, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            //if (txtRegid.Text == "")
            //{
            //    MsgBox.ShowBox("Section should  not be blank...");

            //}
            //else
            {
                SqlCommand com = new SqlCommand("Update_tblFormRegistration  '" + txtRegid.Text + "','" + Convert.ToInt16(txtBillNo.Text) + "','" + dtpdate.ToString("MM/dd/yyyy") + "','" + txtName.Text + "','" + txtFormNo.Text + "','" + Convert.ToDecimal(txtAmount.Text) + "','" + cmbMode.Text + "','" + Convert.ToInt32(cmbAccounts.SelectedValue.ToString()) + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1.ToString("MM/dd/yyyy") + "','" + dtpdob.ToString("MM/dd/yyyy") + "'," + txtMob.Text + ",'" + txtFatherName.Text + "','" + txtMotherName.Text + "','" + cmbClass.Text + "','" + cmbGender.Text + "','" + cmbsession.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Registration Form Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Registration Form already exist");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            dataclass dp = new dataclass();
            SqlConnection sc = dp.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblStudentFormRegistration WHERE  Formnoid='"+txtFormNo.Text+"'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                 MsgBox.ShowBox("Form Already used..");
            }
             else
              {
                        DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {

                                dataclass dc = new dataclass();
                                SqlConnection conn = dc.con();
                                if (txtRegid.Text == "")
                                {
                                    MsgBox.ShowBox("Please Click to Detail Row!!!");
                                    return;
                                }
                                SqlCommand objCmd = new SqlCommand("DELETE FROM tblFormRegistration WHERE id=" + txtRegid.Text + "", conn);
                                SqlDataReader dr;
                                objCmd.Connection.Open();
                                dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                                if (dr.RecordsAffected > 0)
                                {
                                    MsgBox.ShowBox(" Registration form Delete Successfully!!!");
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
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            //do something else
                        }
                    }

        }

        private void txtFormNo_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection conn = dc.con();
            SqlDataAdapter objCmd = new SqlDataAdapter(" select id,formfrom,fromto,amount from tblFromMaster where formfrom<= '" + txtFormNo.Text + "' and fromto>='" + txtFormNo.Text + "' and session = '" + cmbsession.Text + " ' ", conn);
            DataTable dt = new DataTable();
            objCmd.Fill(dt);

            if (dt.Rows.Count>0)
            {
                txtAmount.Text = dt.Rows[0]["amount"].ToString();
                dataclass dc1 = new dataclass();
                SqlConnection conn1 = dc1.con();
                SqlDataAdapter objCmd1 = new SqlDataAdapter(" select * FROM tblFormRegistration where FormNoid='" + txtFormNo.Text + "' and session = '" + cmbsession.Text + "' ", conn1);
                DataTable dt1 = new DataTable();
                objCmd1.Fill(dt1);
                //SqlCommand obj = new SqlCommand("select * FROM tblFormRegistration where FormNoid='" + txtFormNo.Text + "' and session = '"+ cmbsession.Text +"' ", conn);
                //SqlDataReader dr1;
                //obj.Connection.Open();
                //dr1 = obj.ExecuteReader();
                if (dt1.Rows.Count > 0)
                {
                    
                    txtName.Text = dt1.Rows[0]["name"].ToString();
                    txtMob.Text = dt1.Rows[0]["Mobile"].ToString();
                    txtFatherName.Text = dt1.Rows[0]["fatherName"].ToString();
                    txtMotherName.Text = dt1.Rows[0]["MotherName"].ToString();
                    cmbClass.Text = dt1.Rows[0]["clASS"].ToString();
                    cmbGender.Text = dt1.Rows[0]["Gender"].ToString();
                    dtpDOB.Text = dt1.Rows[0]["DOB"].ToString();
                    txtBillNo.Text = dt1.Rows[0]["billno"].ToString();
                  //  cmbsession.Text = dt.Rows[0]["session"].ToString();

                    //txtFormNo.Focus();
                    MsgBox.ShowBox(" Already Exists !!!");
                    
                }
                else
                {
                    
                }
            }
            else
            {
                MsgBox.ShowBox(" This Form No. not Exists pl create Form !!!");
                txtFormNo.Text = "";
               // txtFormNo.Focus();


            }
        
            }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            vl.digitonly(e);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = vl.AllowNumberWithDecimal(e.KeyChar, txtAmount.Text);
            //vl.digitonly(e);

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
          //  MessageBox.Show(Convert.ToInt32(e.KeyChar).ToString());
            vl.charonly(e);
            //e.KeyChar = vl.digitonly(e.KeyChar, txtName.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFormNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = vl.AllowOnlyNumber(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            print();
        }
        private void print()
        {
            decimal abb;
            abb = Convert.ToDecimal(txtAmount.Text.Trim());
            int abbc = Convert.ToInt32(abb);
            word = NumberToWords(abbc);
            cryRegFormSaleREport.Sname = txtName.Text;
            cryRegFormSaleREport.FNo = txtFormNo.Text;
            cryRegFormSaleREport.word = word;
            cryRegFormSaleREport obj = new cryRegFormSaleREport();
            obj.ShowDialog();
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                    //words += unitsMap[number % 10];
                }
            }

            return words;
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        public string RegNo;
        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void txtFormNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbsession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label82_Click(object sender, EventArgs e)
        {

        }

        private void cmbsession_Leave(object sender, EventArgs e)
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select max(FormNoid) from tblFormRegistration where session ='" + cmbsession.Text + "'", sc);
            DataTable dt = new DataTable();
            adap.Fill(dt);                      //max(formNoid) 
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "")
                    txtFormNo.Text = "1";
                else

                    txtFormNo.Text = (Convert.ToInt32(dt.Rows[0][0].ToString())+1).ToString();
               
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtRegid_TextChanged(object sender, EventArgs e)
        {

        }

        

       
    }
}
