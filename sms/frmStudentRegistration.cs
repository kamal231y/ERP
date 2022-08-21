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
using sms.DAL;
using sms.BLClass;
using System.Collections;
namespace sms
{
    public partial class frmStudentRegistration : Form
    {
        DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);

        //string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmStudentRegistration()
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
            cancel_form();
        }

        private void frmStudentRegistration_Load(object sender, EventArgs e)
        {

            bindRegno();
            bindclass();
            bindExam();
            BindAccunt();
            bindSession();
            bindFormNo();
        }

        public void bindRegno()
        {


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("Select max(id)+1 as max from tblStudentFormRegistration", sc);
            sc.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (dr["max"].ToString() == "")
                {
                    txtRegNo.Text = "1"+"2021";

                }
                else
                {
                    txtRegNo.Text = Convert.ToString(Convert.ToInt32(dr["max"])) + "2021";
                }

            }



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
            row["ACC_NAME"] = "Select";
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

        private void create()
        {
            if (cmbFormNo.SelectedValue.ToString() == "")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand cmd = new SqlCommand("select count(Formnoid) from tblStudentFormRegistration where Formnoid=" + cmbFormNo.SelectedValue + "", sc);
                SqlDataAdapter da = new SqlDataAdapter();
                //SqlDataAdapter da = new SqlDataAdapter("select count(Formnoid) from tblStudentFormRegistration where Formnoid="+cmbFormNo.SelectedValue+"", sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int cnt = Convert.ToInt32(cmd.ExecuteNonQuery().ToString());
                if (cnt == 0)
                {

                    //SqlCommand com = new SqlCommand("tblStudentFormRegistration '" + cmbFormNo.SelectedValue + "','" + txtRegNo.Text.Trim() + "','" + cmbRegDate.SelectedValue + "','" + txtStudentName.Text.Trim() + "', '" + register + "','" + txtFatherName.Text.Trim() + "','" + txtMotherName.Text.Trim() + "'," + txtMobNo.Text.Trim() + ",'" + cmbClass.SelectedValue + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + dt1 + "'", sc);

                    sc.Open();
                    da.Fill(ds);
                    cmd.ExecuteNonQuery();
                    sc.Close();

                }

                else
                {
                    MsgBox.ShowBox(" Student is already registered ");
                    rdbRegisered.Checked = true;
                }
            }
        }
        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dtpreg = Convert.ToDateTime(dtp1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtpdob = Convert.ToDateTime(dtpDOB.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtexam = Convert.ToDateTime(dtExam.Value, System.Globalization.CultureInfo.InvariantCulture);
            DialogResult dialogResult = MessageBox.Show("Are You Sure to Create Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbFormNo.SelectedValue.ToString() == "")
                {
                    MsgBox.ShowBox("Form No should not be blank");
                    return;
                }

                if (txtRegNo.Text == "")
                {
                    MsgBox.ShowBox("Period should not be blank");
                }
                int register;
                if (rdbRegisered.Checked == true)
                {
                    register = 1;
                }
                else
                {
                    register = 0;

                }
                DateTime dt1 = System.DateTime.Now.AddYears(-3);
                //if (dtpDOB.Value > dt1)
                //{
                //    MsgBox.ShowBox("Student should not be less than 3 year");
                //    return;


                //}
                SqlCommand com = new SqlCommand("Insert_tblStudentFormRegistration " + Convert.ToInt16(cmbFormNo.Text).ToString() + ",'" + (txtRegNo.Text) + "','" + dtpreg.ToString("MM/dd/yyyy") + "','" + txtStudentName.Text.Trim() + "','" + txtFatherName.Text.Trim() + "','" + txtMotherName.Text + "','" + (txtMobNo.Text) + "','" + cmbClass.SelectedValue + "','" + cmbGender.Text + "','" + cmbTestexam.SelectedValue + "','" + dtpdob.ToString("MM/dd/yyyy") + "','" + txtAddress.Text + "','" + Convert.ToDecimal(txtAmount.Text) + "','" + (cmbMode.Text) + "','" + Convert.ToInt32(cmbAccounts.SelectedValue.ToString()) + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + dt1.ToString("MM/dd/yyyy") + "','" + dtexam.ToString("MM/dd/yyyy") + "','" + dtp3.Text + "','" + dtp2.Text + "','" + cmbsession.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        DialogResult dialogResult1 = MessageBox.Show("Your Registration No is '" + txtRegNo.Text + "'. Are You Sure to print Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult1 == DialogResult.Yes)
                        {
                            print();
                            DialogResult dialogResult2 = MessageBox.Show("Are you sure to send SMS ???", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult2 == DialogResult.Yes)
                            {
                                sms1();
                                MessageBox.Show("Message sent successfully.");
                            }
                            cancel_form();

                        }
                        else
                        {
                            DialogResult dialogResult2 = MessageBox.Show("Are you sure to send SMS ???", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult2 == DialogResult.Yes)
                            {
                                sms1();
                                MessageBox.Show("Message sent successfully.");
                            }
                            cancel_form();
                        }


                    }
                    else
                    {
                        MsgBox.ShowBox("This FormRegistration  already exist");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                cancel_form();
            }
        }

        private void sms1()
        {

            try
            {

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,Your Ward, " + "  " + txtStudentName.Text + " is registered in Class " + cmbClass.Text + "  .His/her Reg. No. is " + txtRegNo.Text.Trim() + ". Thanks ";
                DAL.smsclass SMS = new sms.DAL.smsclass();
                SMS.SMS(msgtxt, 0, txtMobNo.Text.Trim().Replace(" ", ""), txtStudentName.Text);
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


        private void btn_delete_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel_form();
            bindRegno();

        }


        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select *  from CLASS_T WHERE CLS_DP!='N' order by Cls_SEQUENCE ", sc);
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

        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblAdmissionSchedule order by Testid", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "Select";
            row["Testid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbTestexam.DataSource = ds.Tables[0];
            cmbTestexam.DisplayMember = "Name";
            cmbTestexam.ValueMember = "Testid";
        }
        private void bindFormNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (cast(FormNoId as varchar)) as FormNoId  from tblFormRegistration where session = '" + cmbsession.Text + "' order by FormNoId ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["FormNoId"] = "Select";
            row["FormNoId"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbFormNo.DataSource = ds.Tables[0];
            cmbFormNo.ValueMember = "FormNoId";
            cmbFormNo.DisplayMember = "FormNoId";
        }


        private void cancel_form()
        {

            btn_create.Enabled = true;
            //btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            cmbFormNo.Text = "-Select-";
            //dtp1.Text = "";
            txtStudentName.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtMobNo.Text = "";
            cmbClass.Text = "-Select-";
            cmbGender.Text = "-Select-";
            cmbTestexam.Text = "-Select-";
            txtRegNo.Text = "";
            txtAddress.Text = "";
            txtAmount.Text = "";
            cmbAccounts.Text = "Cash/Ac";
            cmbMode.Text = "-Select-";


        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFormNo_Leave(object sender, EventArgs e)
        {
           
            {
                if (cmbFormNo.Text != "" || cmbFormNo.Text != null)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand cmd = new SqlCommand("select * from tblStudentFormRegistration where Formnoid='" + cmbFormNo.Text + "' and session='"  + cmbsession.Text + "'", sc);
                    sc.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        rdbRegisered.Checked = true;
                        rdbNonRegistered.Checked = false;
                        btn_create.Enabled = false;
                        txtStudentName.Text = dr["StudentName"].ToString();
                        txtMotherName.Text = dr["MotherName"].ToString();
                        txtFatherName.Text = dr["FatherName"].ToString();
                        txtAddress.Text = dr["Address"].ToString();
                        txtMobNo.Text = dr["Mobileno"].ToString();
                         txtRegNo.Text = dr["RegNo"].ToString();
                       // cmbGender.Text = (dr["Gender"].ToString());
                        cmbGender.SelectedItem = (dr["Gender"].ToString());
                        cmbClass.SelectedValue = Convert.ToInt32(dr["Classid"].ToString());
                        cmbTestexam.SelectedValue = Convert.ToInt32(dr["Testexamid"].ToString());
                        dtp1.Text = (dr["RDate"].ToString());
                        dtpDOB.Text = (dr["DOB"].ToString());
                        txtAmount.Text = dr["Amount"].ToString();
                        cmbAccounts.SelectedValue = Convert.ToInt32(dr["AccountsId"].ToString());
                        cmbMode.Text = dr["Mode"].ToString();
                        cmbsession.Text = dr ["session"].ToString(); 

                    }
                    else
                    {
                        bindRegno();
                        dr.Close();
                        rdbNonRegistered.Checked = true;
                        rdbRegisered.Checked = false;
                        cmd = new SqlCommand("select * from tblFormRegistration where FormNoid='" + cmbFormNo.SelectedText + "' and session = '" + cmbsession.Text + "'", sc);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtStudentName.Text = dr["Name"].ToString();
                            txtMotherName.Text = dr["MotherName"].ToString();
                            txtFatherName.Text = dr["FatherName"].ToString();
                            cmbGender.SelectedItem = (dr["Gender"].ToString());
                            dtpDOB.Text = (dr["DOB"].ToString());
                            txtMobNo.Text = dr["Mobile"].ToString();
                            cmbClass.Text = dr["class"].ToString();
                       
                        }
                    }



                }



            }
        }

        private void txtMotherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.charonly(e);
        }

        private void txtFatherName_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.charonly(e);
        }

        private void txtMobNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            print();
        }
        private void print()
        {
            cryAdmCard.RegNo = cmbFormNo.Text;// txtRegNo.Text;
            cryAdmCard obj = new cryAdmCard();
            obj.ShowDialog();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            if (cmbClass.Text != "" || cmbClass.Text != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand cmd = new SqlCommand("select * from class_fee_T where CLF_FEE_ID=1 and CLF_CLS_ID=" + cmbClass.SelectedValue + "", sc);
                sc.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    txtAmount.Text = dr["CLF_D_AMOUNT"].ToString();

                }
            }
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {

            {
                if (cmbFormNo.Text != "" || cmbFormNo.Text != null)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand cmd = new SqlCommand("select * from tblStudentFormRegistration where regno='" + txtRegNo.Text + "'", sc);
                    sc.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        rdbRegisered.Checked = true;
                        rdbNonRegistered.Checked = false;
                        txtStudentName.Text = dr["StudentName"].ToString();
                        txtMotherName.Text = dr["MotherName"].ToString();
                        txtFatherName.Text = dr["FatherName"].ToString();
                        txtAddress.Text = dr["Address"].ToString();
                        txtMobNo.Text = dr["Mobileno"].ToString();
                        // txtRegNo.Text = dr["RegNo"].ToString();
                        //cmbGender.SelectedValue = (dr["Gender"].ToString());
                        cmbGender.SelectedItem = (dr["Gender"].ToString());
                        cmbClass.SelectedValue = Convert.ToInt32(dr["Classid"].ToString());
                        cmbTestexam.SelectedValue = Convert.ToInt32(dr["Testexamid"].ToString());
                        dtp1.Text = (dr["RDate"].ToString());
                        dtpDOB.Text = (dr["DOB"].ToString());
                        txtAmount.Text = dr["Amount"].ToString();
                        cmbAccounts.SelectedValue = Convert.ToInt32(dr["AccountsId"].ToString());
                        cmbMode.Text = dr["Mode"].ToString();
                        btn_create.Enabled = false;
                        btn_delete.Enabled = true;




                    }
                    else
                    {
                        dr.Close();
                        btn_create.Enabled = true;
                        rdbNonRegistered.Checked = true;
                        rdbRegisered.Checked = false;
                        cmd = new SqlCommand("select * from tblFormRegistration where FormNoid='" + cmbFormNo.SelectedText + "' and session='" + cmbsession.Text + "'", sc);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            cmbsession.Text = dr["session"].ToString();
                            txtStudentName.Text = dr["Name"].ToString();
                            txtMotherName.Text = dr["MotherName"].ToString();
                            txtFatherName.Text = dr["FatherName"].ToString();
                            txtAddress.Text = dr["Address"].ToString();
                            txtMobNo.Text = dr["Mobileno"].ToString();
                            cmbGender.SelectedItem = (dr["Gender"].ToString());
                            cmbClass.SelectedValue = Convert.ToInt32(dr["Classid"].ToString());
                            cmbTestexam.SelectedValue = Convert.ToInt32(dr["Testexamid"].ToString());
                            dtp1.Text = (dr["RDate"].ToString());
                            dtpDOB.Text = (dr["DOB"].ToString());
                        }
                    }



                }



            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtRegNo.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtStudentName.Text == "")
            {
                MsgBox.ShowBox("Student Name Should not be blank...");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update tblStudentFormRegistration set StudentName='" + txtStudentName.Text + "',MotherName='" + txtMotherName.Text + "',FatherName='" + txtFatherName.Text + "',Address='" + txtAddress.Text + "',Mobileno='" + txtMobNo.Text + "',Amount='" + txtAmount.Text + "',session='" + cmbsession.Text + "' where RegNo='" + txtRegNo.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = cmd.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Form Updated...");
                        //LoadGrid();
                        //Cancil_form();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Form already exist");
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

        private void cmbTestexam_Leave(object sender, EventArgs e)
        {
            if (cmbTestexam.Text != "" || cmbTestexam.Text != "Select")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter sda = new SqlDataAdapter("select * from tbladmissionschedule where name like '" + cmbTestexam.Text + "'", sc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dtExam.Text = dt.Rows[0]["Dates"].ToString();
                    dtp3.Text = dt.Rows[0]["FromDate"].ToString();
                    dtp2.Text = dt.Rows[0]["Todate"].ToString();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            cryRegReceipt.RegNo =  txtRegNo.Text;
            cryRegReceipt obj = new cryRegReceipt();
            obj.ShowDialog();
        }

        private void cmbsession_Leave(object sender, EventArgs e)
        {
            bindFormNo();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbsession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFormNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }




    //private void cmbFormNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (cmbFormNo.Text != "" || cmbFormNo.SelectedValue != null)
    //    {
    //        dataclass dc = new dataclass();
    //        SqlConnection sc = dc.con();
    //        SqlCommand cmd = new SqlCommand("select * from tblFormRegistrationSale where FromNo='" + cmbFormNo.Text + "'", sc);
    //        sc.Open();
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.Read())
    //        {
    //            rdbRegisered.Checked = true;
    //            rdbNonRegistered.Checked = false;

    //        }
    //        else
    //        {
    //            rdbNonRegistered.Checked = true;
    //            rdbRegisered.Checked = false;

    //        }



    //    }



    //}


}



