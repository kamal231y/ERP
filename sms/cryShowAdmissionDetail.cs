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
using MsgBoxRet;
namespace sms
{
    public partial class cryShowAdmissionDetail : Form
    {

          public static string hostel;
          public static string classname;
          public static string yaer;


                public cryShowAdmissionDetail()
        {

            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            // string str = "select sid as Sr_No, Admission_no,AccNo,dob as DOB,Admission_date, StudentName,father_name ,mother_name ,sex= case when  sex='M' then 'Male' else 'Female' end, Class,rollno as RollNo, (paddress+''+CityName) as Address,contactno1 as Contact_No  from View_StudentReport where class like '" + classname + "' and stu_type1 like '" + stutype1 + "'and CAST( Year(Admission_date) as varchar) like '" + stutype1 + "'";
             string str = " select  *  from View_StudentReport where STC_SEC_ID like '" + classname + "' and STU_HOSTLER like '" + hostel + "'and CAST( (STU_SESSION) as varchar) like '" + yaer + "' order by class,roll";
             //AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_studentReport");

             cRptStudentDetail crptdetail = new cRptStudentDetail();
            crptdetail.SetDataSource(ds1.Tables["View_studentReport"]);
          
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());

            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();
        }
    }
}
