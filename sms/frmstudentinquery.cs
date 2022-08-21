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
    public partial class frmstudentinquery: Form
    {
        DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);

        //string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmstudentinquery()
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
            //bindFormNo();
            bindclass();
          

        }

        public void bindRegno()
        {
             
      
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("  Select max(formnoid)+1 as max from inqueryRegistration", sc);
            sc.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (dr["max"].ToString() == "")
                {
                    txtRegNo.Text = "1";

                }
                else
                {
                    txtRegNo.Text = Convert.ToString(Convert.ToInt32(dr["max"]));
                }

            }
            
       
          
        }

       


        private void create()
        {
            if (cmbFormNo.SelectedValue.ToString() == "")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand cmd = new SqlCommand("select count(Formnoid) from inqueryRegistration where Formnoid=" + cmbFormNo.SelectedValue + "", sc);
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure to Create Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();


                if (txtStudentName.Text == "")
                {
                    MsgBox.ShowBox("Name should not be blank");
                }
                int register;
               
                DateTime dt1 = System.DateTime.Now.AddYears(-3);
                //if (dtpDOB.Value > dt1)
                //{
                //    MsgBox.ShowBox("Student should not be less than 3 year");
                //    return;


                //}
                SqlCommand com = new SqlCommand("Insert_inqueryRegistration " + txtRegNo.Text + ",'" + dtpreg.ToString("MM/dd/yyyy") + "','" + txtStudentName.Text.Trim() + "','" + txtFatherName.Text.Trim() + "','" + txtMotherName.Text + "','" + (txtMobNo.Text) + "','" + cmbClass.SelectedValue + "','" + cmbGender.Text + "','" + dtpdob.ToString("MM/dd/yyyy") + "','" + txtAddress.Text + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + dt1.ToString("MM/dd/yyyy") + "'", sc);
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
                             cancel_form();

                         }
                         else
                         {
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
            SqlDataAdapter da = new SqlDataAdapter(" select *  from CLASS_T   order by Cls_SEQUENCE ", sc);
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
      
        private void bindFormNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id, cast(FormNoId as varchar) as FormNoId  from tblFormRegistration order by FormNoId ", sc);

       //     SqlDataAdapter da = new SqlDataAdapter("select FormNoId,(cast(FormNoId as varchar)+'/'+cast(YEAR(Bdate)as varchar)) as show from tblFormRegistration order by FormNoId ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["FormNoId"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbFormNo.DataSource = ds.Tables[0];
            cmbFormNo.ValueMember = "id";
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
          //  cmbTestexam.Text = "-Select-";
            txtRegNo.Text = "";
            txtAddress.Text="";
            //txtAmount.Text = "";
            //cmbAccounts.Text = "Cash/Ac";
            //cmbMode.Text = "Cash";
            

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFormNo_Leave(object sender, EventArgs e)
        {
            bindRegno();
            {
                if (cmbFormNo.Text != "" || cmbFormNo.Text != null)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand cmd = new SqlCommand("select * from inqueryRegistration where Formnoid='" + cmbFormNo.SelectedText + "'", sc);
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
                       // cmbTestexam.SelectedValue = Convert.ToInt32(dr["Testexamid"].ToString());
                        dtp1.Text = (dr["RDate"].ToString());
                        //dtpDOB.Text = (dr["DOB"].ToString());
                        //txtAmount.Text = dr["Amount"].ToString();
                        //cmbAccounts.SelectedValue = Convert.ToInt32( dr["AccountsId"].ToString());
                        //cmbMode.Text = dr["Mode"].ToString();



                    

                    }
                    else
                    {
                        dr.Close();
                        rdbNonRegistered.Checked = true;
                        rdbRegisered.Checked = false;
                        cmd = new SqlCommand("select * from inqueryRegistration where FormNoid='" + cmbFormNo.SelectedText + "'", sc);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtStudentName.Text = dr["Name"].ToString();
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
            cryAdmCard.RegNo = txtRegNo.Text;
            cryAdmCard obj = new cryAdmCard();
            obj.ShowDialog();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            //if (cmbClass.Text != "" || cmbClass.Text != null)
            //{
            //    dataclass dc = new dataclass();
            //    SqlConnection sc = dc.con();
            //    SqlCommand cmd = new SqlCommand("select * from class_fee_T where CLF_FEE_ID=1 and CLF_CLS_ID=" + cmbClass.SelectedValue + "", sc);
            //    sc.Open();
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {

            //        txtAmount.Text = dr["CLF_D_AMOUNT"].ToString();
                                        
            //    }
            //}
        }

        private void txtRegNo_Leave(object sender, EventArgs e)
        {
           
            {
                if (cmbFormNo.Text != "" || cmbFormNo.Text != null)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand cmd = new SqlCommand("select * from inqueryRegistration where Formnoid='" + txtRegNo.Text + "'", sc);
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
                        //cmbTestexam.SelectedValue = Convert.ToInt32(dr["Testexamid"].ToString());
                        dtp1.Text = (dr["RDate"].ToString());
                        //dtpDOB.Text = (dr["DOB"].ToString());
                        //txtAmount.Text = dr["Amount"].ToString();
                        //cmbAccounts.SelectedValue = Convert.ToInt32(dr["AccountsId"].ToString());
                        //cmbMode.Text = dr["Mode"].ToString();
                        btn_create.Enabled = false;
                        btn_delete.Enabled = true;




                    }
                    else
                    {
                        dr.Close();
                        btn_create.Enabled = true;
                        rdbNonRegistered.Checked = true;
                        rdbRegisered.Checked = false;
                        cmd = new SqlCommand("select * from tblFormRegistration where FormNoid='" + cmbFormNo.SelectedText + "'", sc);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtStudentName.Text = dr["Name"].ToString();
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
                SqlCommand cmd = new SqlCommand("update inqueryRegistration set StudentName='" + txtStudentName.Text + "',MotherName='" + txtMotherName.Text + "',FatherName='" + txtFatherName.Text + "',Address='" + txtAddress.Text + "',Mobileno='" + txtMobNo.Text + "' where Formnoid='" + txtRegNo.Text + "'", sc);
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

        private void btn_delete_Click_1(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("delete from  inqueryRegistration  where Formnoid='" + txtRegNo.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = cmd.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Data Delete Successfully...");
                        //LoadGrid();
                        //Cancil_form();
                    }
                    else
                    {
                        MsgBox.ShowBox("No Data Delete");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                    cancel_form();
                    bindRegno();

                }
            }
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
        
    

                       