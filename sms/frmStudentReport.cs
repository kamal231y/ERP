using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using MsgBoxRet;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace sms
{
    public partial class frmStudentReport : Form
    {
        string tmode;
        string house;
        string studentname;
        string fathername;
        string Admissionno;
        string accno;
       string rollno;
       string sex;
       string hostel;
       string ward; 
       string classname;
       string category;
       string Caste;
       string CategoryType;
       string cityname; decimal min;
       decimal max;
       string deleted;
       SqlDataReader dr;
       public frmStudentReport()
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
            //cmbbilltype.SelectedIndex = 0;            
            Cancil_form();           
        }
                   
        private void Cancil_form()
        {
            panelsearch.Visible = true;
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbclass.Text = "Select";
            cmbroll.Text = "Select";
            cmbCategory.Text = "Select";
            cmbcity.Text = "Select";
            cmbsession.Text = "Select";
            cmbadmno.Text = "Select";
            txtfathername.Text = "";
            txtfname.Text = "";
            //showstudentreport();
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();            
        }

        private void frmGrDetail_Load(object sender, EventArgs e)
        {
            Bindclass(); bindcity(); bindreligion(); //BindRNo();
            //BindAdmNo(); bindAcc();
            bindHouse();
            bindCategory();
            bindcategory(); bindTransportMode(); MenuList(); bindSession();

        }

        private void bindSession()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from session_T  order by id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Session"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "Session";
            cmbsession.ValueMember = "id";

        }
        private void MenuList()
        {
            dataclass  dc = new dataclass();
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

                button3.Visible = Convert.ToBoolean(dr["STU1"]);
                button1.Visible = Convert.ToBoolean(dr["FEE2"]);
                btnfee.Visible = Convert.ToBoolean(dr["FEE5"]);
                btnattend.Visible = Convert.ToBoolean(dr["STU12"]);
                btncert.Visible = Convert.ToBoolean(dr["cer1"]);
                btnexam.Visible = Convert.ToBoolean(dr["EXAM9"]);
                button2.Visible = Convert.ToBoolean(dr["sms1"]);
                
            }
            else
            {
                button3.Enabled = false;
                button1.Enabled = false;
                btnfee.Enabled = false;
                btnattend.Enabled = false;
                btncert.Enabled = false;
                btnexam.Enabled = false;
                button2.Enabled = false;
             



            }
            sc.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelsearch.Visible = true;
            Cancil_form();
           
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CASTE_ID,CASTE_DESC from CASTE_T order by CASTE_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CASTE_DESC"] = "Select";
            row["CASTE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbsiblingclass.DataSource = dt;
            cmbsiblingclass.DisplayMember = "CASTE_DESC";
            cmbsiblingclass.ValueMember = "CASTE_ID";

        }
        private void bindTransportMode()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select trnsport_id,trnsport_name from Transport_Mode_T ORDER BY trnsport_id", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["trnsport_name"] = "Select";
            row["trnsport_id"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbtrans.DataSource = dt;
            cmbtrans.DisplayMember = "trnsport_name";
            cmbtrans.ValueMember = "trnsport_id";

        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            showstudentreport();
        
        }
      private void showstudentreport()
         {
             if (cmbHouse.Text == "" || cmbHouse.Text == "Select" || cmbHouse.Text == "System.Data.DataRowView")
             {
                 house = "%";
             }
             else
             {
                 house = cmbHouse.Text.ToString();
             }
             if (cmbtrans.Text == "" || cmbtrans.Text == "0" || cmbtrans.Text == "Select" || cmbtrans.Text == "System.Data.DataRowView")
             {
                 tmode = "%";
             }
             else
             {
                 tmode = cmbtrans.SelectedValue.ToString();
             }

             if ( txtfname.Text == "")
             {
                 studentname  = "%";
             }
             else
             {
                 studentname = txtfname.Text;
             }
             if (txtfathername.Text == "")
             {
                 fathername = "%";
             }
             else
             {
                 fathername = txtfathername.Text;
             }
             if (cmbsession.Text == "Select" || cmbsession.Text == "0" || cmbsession.Text == "System.Data.DataRowView")
             {
                 accno = "%";
             }
             else
             {
                 accno = cmbsession.Text ;
             }
             if (cmbadmno.Text == "Select" || cmbadmno.Text == "0" || cmbadmno.Text == "System.Data.DataRowView")
             {
                 Admissionno = "%";
             }
             else
             {
                 Admissionno = cmbadmno.Text ;
             }
             if (cmbclass.Text == "" || cmbclass.Text == "Select" || cmbclass.Text == "System.Data.DataRowView")
             {
                 classname = "%";
             }
             else
             {
                 classname = cmbclass.SelectedValue.ToString();
             }
             if (cmbroll.Text == "Select" || cmbroll.Text == "0" || cmbroll.Text == "System.Data.DataRowView")
             {
                 rollno = "%";
             }
             else
             {
                 rollno = cmbroll.SelectedText ;
             }
             if (cmbCategory.Text == "Select" || cmbCategory.Text == "" || cmbCategory.Text == "System.Data.DataRowView")
             {
                 category = "%";
             }
             else
             {
                 category  = cmbCategory.SelectedValue.ToString();
             }
             if (cmbcategory1.Text == "Select" || cmbcategory1.Text == "" || cmbcategory1.Text == "System.Data.DataRowView")
             {
                 CategoryType = "%";
             }
             else
             {
                 CategoryType = cmbcategory1.SelectedValue.ToString();
             }
             if (cmbsiblingclass.Text == "Select" || cmbsiblingclass.Text == "" || cmbsiblingclass.Text == "System.Data.DataRowView")
             {
                 Caste = "%";
             }
             else
             {
                 Caste = cmbsiblingclass.SelectedValue.ToString();
             }
             if (cmbcity.Text == "Select" || cmbcity.Text == "" || cmbcity.Text == "System.Data.DataRowView")
             {
                 cityname = "%";
             }
             else
             {
                 cityname  = cmbcity.Text;
             }

             if (rdsexboth. Checked)
             {
                 sex = "%";
             }
             else
             {
                 if (radiomale.Checked)
                 {
                     sex = "Male";
                 }
                 else
                 {
                     sex = "Female";
                 }
             }
             if (rdbothward.Checked)
             {
                 ward = "%";
             }
             else
             {
                 if (radionormal.Checked)
                 {
                     ward = "N";

                 }
                 else if (radiostaff.Checked)
                 {
                     ward = "S";
                 }
                 else
                 {
                     ward = "R";
                 }
             }

             if (rdbothhostel.Checked)
             {
                 hostel = "%";
             }
             else
             {
                 if (radiohostler.Checked)
                 {
                     hostel = "H";

                 }
                 else
                 {
                     hostel = "N";
                 }
             }
             if (cmbincome.Text == "" || cmbincome.Text == "Select")
             {
                 min = 0;
                 max = 0;
                 
             }
             else if (cmbincome.Text == "Less than 50,000")
             {
                 min = 0; max = 50000;
             }
             else if (cmbincome.Text == "1,00000 to 3,00000")
             {
                 min = 100000; max =300000;
             }
             else if (cmbincome.Text == "3,00000 to 5,00000")
             {
                 min = 300000; max = 500000;
             }
             else
             {
                 min = 500000; max = 99999999999;
             }
             if (chkisCurrent.Checked == true)
                 deleted = "N";
             else
                 deleted = "Y";
            
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

           if (cmbincome.Text == "" || cmbincome.Text == "Select")
               str = " select AdmNo,AccNo as Acc_No,Class,Roll, Student_Name,FatherName,Phone,MotherName,MOTHER_MOB,stu_House_id,Sex,Address,Aadhar,CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date from View_StudentReport where STC_SEC_ID like '" + classname + "'  and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "%'  and CITY_NAME like '" + cityname + "%' and AdmNo like '" + Admissionno + "%'  and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'  and STU_Trans_Mode like '" + tmode + "' and STU_house_ID like '" + house + "%'   ORDER BY stu_id desc";
           else
               str = " select AdmNo,AccNo as Acc_No,Class,Roll, Student_Name,FatherName,Phone,MotherName,MOTHER_MOB,stu_House_id,Sex,Address,Aadhar,CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date from View_StudentReport where STC_SEC_ID like '" + classname + "'  and STU_SIBLING_ID like '" + CategoryType + "' and STU_CASTE_ID like '" + Caste + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "%'  and CITY_NAME like '" + cityname + "%' and AdmNo like '" + Admissionno + "%' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'   and STU_house_ID like '" + house + "%' and STU_Trans_Mode like '" + tmode + "' ORDER BY stu_id desc";
       //    str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name";

          
          if(str!="")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                    dataGridView1.DataSource = dt;
                    lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    dataGridView1.Columns["AdmNo"].Width = 100;
                    dataGridView1.Columns["Acc_No"].Width = 80;
                    dataGridView1.Columns["DOB"].Width = 75;
                    dataGridView1.Columns["Adm_Date"].Width = 80;
                    dataGridView1.Columns["Student_Name"].Width = 170;
                    dataGridView1.Columns["FatherName"].Width = 150;
                    dataGridView1.Columns["MotherName"].Width = 150;
                    dataGridView1.Columns["Sex"].Width = 50;
                    dataGridView1.Columns["Class"].Width = 70;
                    dataGridView1.Columns["Roll"].Width = 50;
                    dataGridView1.Columns["Address"].Width = 160;
                    dataGridView1.Columns["Phone"].Width = 100;
                ////}
                ////else
                //{
                //    dataGridView1.DataSource = dt;
                //   // lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                //    //MsgBox.ShowBox("No Records Found....");

                //}
           }

            sc.Close(); 
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                cryshowstudentdetail.dt1 = dateTimePicker1.Value;
                cryshowstudentdetail.dt2 = dateTimePicker2.Value;
                cryshowstudentdetail.classname = classname;
                cryshowstudentdetail.rollno = rollno;
                cryshowstudentdetail.sex = sex;
                cryshowstudentdetail.hostel = hostel;
                cryshowstudentdetail.ward = ward;

                cryshowstudentdetail.studentname = studentname;
                cryshowstudentdetail.fathername = fathername;
                cryshowstudentdetail.Admissionno = Admissionno;
                cryshowstudentdetail.accno = accno;
                cryshowstudentdetail.classname = classname;
                cryshowstudentdetail.category = category;
                cryshowstudentdetail.cityname = cityname;
                cryshowstudentdetail.min = min;
                cryshowstudentdetail.max = max;
                cryshowstudentdetail.deleted = deleted;
                cryshowstudentdetail.house = house;
                cryshowstudentdetail.tmode = tmode;
                cryshowstudentdetail obj = new cryshowstudentdetail();
                
                obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
               
         }
        private void bindcategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CAT_ID,CAT_DESC from CATEGORY_T order by CAT_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CAT_DESC"] = "Select";
            row["CAT_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcategory1.DataSource = dt;
            cmbcategory1.DisplayMember = "CAT_DESC";
            cmbcategory1.ValueMember = "CAT_ID";

        }

        
        private void bindHouse()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select HOUSE_ID,HOUSE_DESC from HOUSE_T order by HOUSE_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["HOUSE_DESC"] = "Select";
            row["HOUSE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbHouse.DataSource = dt;
            cmbHouse.DisplayMember = "HOUSE_DESC";
            cmbHouse.ValueMember = "HOUSE_ID";

        }
       private void bindgridd()
        {
            dataclass dc = new dataclass();
        
                   SqlConnection sc = dc.con();
                   SqlDataAdapter da = new SqlDataAdapter("select sid as Sr_No, Admission_no,AccNo,dob as DOB,Admission_date,(fname+' '+mname+' '+lname) as StudentName,father_name ,mother_name ,sex= case when  sex='M' then 'Male' else 'Female' end,(CLS_DESC+''+SEC_DESC) as Class,rollno as RollNo, (paddress+''+CityName) as Address,contactno1 as Contact_No  from View_StudentReport", sc);
                   DataSet ds = new DataSet();
                   da.Fill(ds);
                   dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindcity()
        {
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select CITY_ID,CITY_NAME from CITY_T order by  CITY_NAME", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["CITY_NAME"] = "Select";
            row["CITY_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcity.DataSource = dt;
            cmbcity.DisplayMember = "CITY_NAME";
            cmbcity.ValueMember = "CITY_ID";
        }
        private void bindreligion()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select RLG_ID,RLG_DESC from RELIGION_T order by RLG_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["RLG_DESC"] = "Select";
            row["RLG_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "RLG_DESC";
            cmbCategory.ValueMember = "RLG_ID";

        }

        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,SEC_ID from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N'  order by CLS_SEQUENCE ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbclass.DataSource = dt;
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
            sc.Close();
          //  showstudentreport();
        }
        
        private void bindAcc()
        {
            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select STU_ACC_NO, STU_ID  from STUDENT_T  order by STU_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STU_ACC_NO"] = "0";
            row["STU_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "STU_ACC_NO";
            cmbsession.ValueMember = "STU_ID";
        }
        
        private void cmbclass_Leave(object sender, EventArgs e)
        {
            BindRNo();
            showstudentreport();
        }
       private void BindRNo()
        {
            string class1;
            if (cmbclass.SelectedValue.ToString() == "Select" || cmbclass.SelectedValue.ToString() == "")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID =  " + class1 + " order by  STC_ROLL_NO", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["STC_ROLL_NO"] = "0";
            row["STC_STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbroll.DataSource = dt;
            cmbroll.DisplayMember = "STC_ROLL_NO";
            cmbroll.ValueMember = "STC_STU_ID";
        }
       private void BindAdmNo()
       {
           string class1;
           if (cmbclass.Text == "Select" || cmbclass.Text == "")
           {
               class1 = "%";
           }
           else
           {
               class1 = cmbclass.SelectedValue.ToString();
           }
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter("select STU_ID,STU_ADM_NO from STUDENT_T  order by STU_ADM_NO", sc);
           System.Data.DataTable dt2 = new System.Data.DataTable();
           da.Fill(dt2);
           DataRow row1 = dt2.NewRow();
           row1["STU_ADM_NO"] = "Select";
           row1["STU_ID"] = 0;
           dt2.Rows.InsertAt(row1, 0);
           cmbadmno.DataSource = dt2;
           cmbadmno.DisplayMember = "STU_ADM_NO";
           cmbadmno.ValueMember = "STU_ID";
         //  showstudentreport();
       }
        
        private void txtfname_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void txtfathername_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbroll_Leave(object sender, EventArgs e)
        {
            showstudentreport();  
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (dataGridView1.Rows.Count > 0)
            {
                frmAdmission.admno = dataGridView1.CurrentRow.Cells["AdmNo"].Value.ToString();
                frmAdmission obj = new frmAdmission();
                obj.ShowDialog();
            }
        }

        private void cmbadmno_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

       
        private void radioscholor_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void radiohostler_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothhostel_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radionormal_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiostaff_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothward_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiomale_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiofemale_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdsexboth_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

      
        



        private void radiofemale_CheckedChanged_1(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiomale_CheckedChanged_1(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void chkisCurrent_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbincome_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void panelsearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void cmbsiblingclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbcategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            if (cmbHouse.Text == "" || cmbHouse.Text == "Select" || cmbHouse.Text == "System.Data.DataRowView")
            {
                house = "%";
            }
            else
            {
                house = cmbHouse.Text.ToString();
            }
            if (txtfname.Text == "")
            {
                studentname = "%";
            }
            else
            {
                studentname = txtfname.Text;
            }
            if (txtfathername.Text == "")
            {
                fathername = "%";
            }
            else
            {
                fathername = txtfathername.Text;
            }
            if (cmbsession.Text == "Select" || cmbsession.Text == "0" || cmbsession.Text == "System.Data.DataRowView")
            {
                accno = "%";
            }
            else
            {
                accno = cmbsession.Text;
            }
            if (cmbadmno.Text == "Select" || cmbadmno.Text == "0" || cmbadmno.Text == "System.Data.DataRowView")
            {
                Admissionno = "%";
            }
            else
            {
                Admissionno = cmbadmno.Text;
            }
            if (cmbclass.Text == "" || cmbclass.Text == "Select" || cmbclass.Text == "System.Data.DataRowView")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString();
            }
            if (cmbroll.Text == "Select" || cmbroll.Text == "0" || cmbroll.Text == "System.Data.DataRowView")
            {
                rollno = "%";
            }
            else
            {
                rollno = cmbroll.SelectedValue.ToString();
            }
            if (cmbCategory.Text == "Select" || cmbCategory.Text == "" || cmbCategory.Text == "System.Data.DataRowView")
            {
                category = "%";
            }
            else
            {
                category = cmbCategory.SelectedValue.ToString();
            }
            if (cmbcategory1.Text == "Select" || cmbcategory1.Text == "" || cmbcategory1.Text == "System.Data.DataRowView")
            {
                CategoryType = "%";
            }
            else
            {
                CategoryType = cmbcategory1.SelectedValue.ToString();
            }
            if (cmbsiblingclass.Text == "Select" || cmbsiblingclass.Text == "" || cmbsiblingclass.Text == "System.Data.DataRowView")
            {
                Caste = "%";
            }
            else
            {
                Caste = cmbsiblingclass.SelectedValue.ToString();
            }
            if (cmbcity.Text == "Select" || cmbcity.Text == "" || cmbcity.Text == "System.Data.DataRowView")
            {
                cityname = "%";
            }
            else
            {
                cityname = cmbcity.Text;
            }

            if (rdsexboth.Checked)
            {
                sex = "%";
            }
            else
            {
                if (radiomale.Checked)
                {
                    sex = "Male";
                }
                else
                {
                    sex = "Female";
                }
            }
            if (rdbothward.Checked)
            {
                ward = "%";
            }
            else
            {
                if (radionormal.Checked)
                {
                    ward = "N";

                }
                else
                {
                    ward = "S";
                }
            }

            if (rdbothhostel.Checked)
            {
                hostel = "%";
            }
            else
            {
                if (radiohostler.Checked)
                {
                    hostel = "H";

                }
                else
                {
                    hostel = "N";
                }
            }
            if (cmbincome.Text == "" || cmbincome.Text == "Select")
            {
                min = 0;
                max = 0;

            }
            else if (cmbincome.Text == "Less than 50,000")
            {
                min = 0; max = 50000;
            }
            else if (cmbincome.Text == "1,00000 to 3,00000")
            {
                min = 100000; max = 300000;
            }
            else if (cmbincome.Text == "3,00000 to 5,00000")
            {
                min = 300000; max = 500000;
            }
            else
            {
                min = 500000; max = 99999999999;
            }
            if (chkisCurrent.Checked == true)
                deleted = "N";
            else
                deleted = "Y";

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

            if (cmbincome.Text == "" || cmbincome.Text == "Select")
                str = "SELECT DAY(DOB) AS DOB_DAY,MONTH(DOB) AS DOB_MONTH,YEAR(DOB) AS DOB_YEAR ,DAY(Adm_Date) AS ADM_DAY,MONTH(Adm_Date) AS ADM_MONTH,YEAR(Adm_Date) AS ADM_YEAR , AdmNo,AccNo as CardNo,CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date,Student_Name,FatherName,Phone,MotherName,MOTHER_MOB,Sex,Class,Roll,Address,CITY_NAME,STU_RLG_ID,STU_CASTE_ID,STU_INTRODUCER_CODE AS CATEGORY,STU_NATIONAL_ID,FATHER_QUALIFICATION,MOTHER_QUL,STU_SESSION,STU_SIBLING_ID,AADHAR,ADMCLASS,STU_FATHER_OCP_ID,STU_MOTHER_OCP_ID from View_StudentReport where STC_SEC_ID like '" + classname + "'  and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "'  and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'  and STU_HOUSE_ID like '" + house + "' order by CLS_SEQUENCE,Roll,Student_Name,AdmNo";
            else
                str = "SELECT DAY(DOB) AS DOB_DAY,MONTH(DOB) AS DOB_MONTH,YEAR(DOB) AS DOB_YEAR ,DAY(Adm_Date) AS ADM_DAY,MONTH(Adm_Date) AS ADM_MONTH,YEAR(Adm_Date) AS ADM_YEAR ,  AdmNo,AccNo as CardNo,CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date,Student_Name,FatherName,Phone,MotherName,MOTHER_MOB,Sex,Class,Roll,Address,CITY_NAME,STU_RLG_ID,STU_CASTE_ID,STU_INTRODUCER_CODE AS CATEGORY,STU_NATIONAL_ID,FATHER_QUALIFICATION,MOTHER_QUL,STU_SESSION,STU_SIBLING_ID,AADHAR,ADMCLASS,STU_FATHER_OCP_ID,STU_MOTHER_OCP_ID from View_StudentReport where STC_SEC_ID like '" + classname + "'  and STU_SIBLING_ID like '" + CategoryType + "' and STU_CASTE_ID like '" + Caste + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "%' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and stu_session like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'   and STU_HOUSE_ID like '" + house + "' order by CLS_SEQUENCE,Roll,Student_Name,AdmNo";
            //    str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name";


            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataSet dt = new System.Data.DataSet();
                da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    List<STUDENT_EXPORT> SD = new List<STUDENT_EXPORT>();

                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        SD.Add(new STUDENT_EXPORT());
                        SD[i].DOB =" "+dt.Tables[0].Rows[i]["DOB_DAY"].ToString() + "/" + dt.Tables[0].Rows[i]["DOB_MONTH"].ToString() + "/" + dt.Tables[0].Rows[i]["DOB_YEAR"].ToString()+" ";
                        SD[i].ADM_DATE = " " + dt.Tables[0].Rows[i]["ADM_DAY"].ToString() + "/" + dt.Tables[0].Rows[i]["ADM_MONTH"].ToString() + "/" + dt.Tables[0].Rows[i]["ADM_YEAR"].ToString() + " ";
                        SD[i].ADMISSION_NO = dt.Tables[0].Rows[i]["AdmNo"].ToString();
                        SD[i].SR_NO = dt.Tables[0].Rows[i]["CardNo"].ToString();
                        SD[i].STUDENT_NAME = dt.Tables[0].Rows[i]["Student_Name"].ToString();
                        SD[i].FATHER_NAME = dt.Tables[0].Rows[i]["FatherName"].ToString();
                        SD[i].PHONE = dt.Tables[0].Rows[i]["Phone"].ToString();
                        SD[i].MOTHER_NAME = dt.Tables[0].Rows[i]["MotherName"].ToString();
                        SD[i].MOTHER_MOBILE = dt.Tables[0].Rows[i]["MOTHER_MOB"].ToString();
                        SD[i].GENDER = dt.Tables[0].Rows[i]["Sex"].ToString();
                        SD[i].CLASS = dt.Tables[0].Rows[i]["Class"].ToString();
                        SD[i].ROLL_NO = dt.Tables[0].Rows[i]["Roll"].ToString();
                        SD[i].ADDRESS = dt.Tables[0].Rows[i]["Address"].ToString();
                        SD[i].CITY_NAME = dt.Tables[0].Rows[i]["CITY_NAME"].ToString();
                        SD[i].STU_RELIGION = dt.Tables[0].Rows[i]["STU_RLG_ID"].ToString();
                        SD[i].STUDENT_CAST = dt.Tables[0].Rows[i]["STU_CASTE_ID"].ToString();
                        SD[i].CATEGORY = dt.Tables[0].Rows[i]["CATEGORY"].ToString();
                        SD[i].NATIONALITY = dt.Tables[0].Rows[i]["STU_NATIONAL_ID"].ToString();
                        SD[i].FATHER_QUALIFICATION = dt.Tables[0].Rows[i]["FATHER_QUALIFICATION"].ToString();
                        SD[i].MOTHER_QUALIFICATION = dt.Tables[0].Rows[i]["MOTHER_QUL"].ToString();
                        SD[i].SESSION = dt.Tables[0].Rows[i]["STU_SESSION"].ToString();
                        SD[i].SIBLING_ID = dt.Tables[0].Rows[i]["STU_SIBLING_ID"].ToString();

                        SD[i].AADHAR = dt.Tables[0].Rows[i]["AADHAR"].ToString();
                        SD[i].ADMISSION_CLASS = dt.Tables[0].Rows[i]["ADMCLASS"].ToString();
                        SD[i].FATHER_OCCUPATION = dt.Tables[0].Rows[i]["STU_FATHER_OCP_ID"].ToString();
                        SD[i].MOTHER_OCCUPATION = dt.Tables[0].Rows[i]["STU_MOTHER_OCP_ID"].ToString();
                    }
                    System.Data.DataTable ds = ToDataTable(SD);
                    ExportDataSetToExcel(ds);
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    lblcount.Text = "Total No Of Record : " + "0";
                    //MsgBox.ShowBox("No Records Found....");

                }
            }

            sc.Close();

        }


        public static System.Data.DataTable ToDataTable<STUDENT_EXPORT>(List<STUDENT_EXPORT> items)
        {
            System.Data.DataTable dataTable = new System.Data.DataTable(typeof(STUDENT_EXPORT).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(STUDENT_EXPORT).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (STUDENT_EXPORT item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }







        private void ExportDataSetToExcel(System.Data.DataTable ds)
        {
            try
            {
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                // Loop over DataTables in DataSet.
               // DataTableCollection collection = ds;
               // for (int i = collection.Count; i > 0; i--)
               // {
                    Sheets xlSheets = null;
                    Worksheet xlWorksheet = null;
                    //Create Excel Sheets
                    xlSheets = ExcelApp.Sheets;
                    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                                   Type.Missing, Type.Missing, Type.Missing);
                    System.Data.DataTable table = ds;
                    xlWorksheet.Name = table.TableName;
                    for (int j = 1; j < table.Columns.Count + 1; j++)
                    {
                        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    }
                    // Storing Each row and column value to excel sheet

              

                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        for (int l = 0; l < table.Columns.Count; l++)
                        {
                           // if(table.Rows[l][""].ToString().Equals(""))
                            ExcelApp.Cells[k + 2, l + 1] =
                            table.Rows[k].ItemArray[l].ToString();
                        }
                    }
                    ExcelApp.Columns.AutoFit();
               // }
                ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
                ExcelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.ShowDialog();
        }

        private void btnfee_Click(object sender, EventArgs e)
        {
            if (!Form1.board)
            {
                FrmFeeRecipt obj = new FrmFeeRecipt();
                obj.ShowDialog();
            }
            else
            {
                FrmFeeReciptUP obj = new FrmFeeReciptUP();
                obj.ShowDialog();
            }
        }

        private void btnattend_Click(object sender, EventArgs e)
        {
            frmstudentAttendance obj = new frmstudentAttendance();
            obj.ShowDialog();
        }

        private void btncert_Click(object sender, EventArgs e)
        {
            frmTC obj = new frmTC(); obj.ShowDialog();
        }

        private void btnexam_Click(object sender, EventArgs e)
        {
            frmReportCardAnnualwise obj = new frmReportCardAnnualwise();
            obj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmStudentallsms obj = new frmStudentallsms();
            obj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmClassFeeSingle obj = new frmClassFeeSingle();
            obj.ShowDialog();
        }

      
        private void cmbtrans_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbsession_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbHouse_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

     

        
        }       
    }

