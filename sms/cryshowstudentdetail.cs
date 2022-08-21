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
    public partial class cryshowstudentdetail : Form
    {
          public  static DateTime dt1;
          public  static DateTime dt2;

     public static     string studentname;
     public static string fathername;
     public static string Admissionno;
     public static string accno;
     public static string rollno;
     public static string sex;
     public static string hostel;
     public static string ward;
     public static string classname;
          public static string category;
          public static string cityname;
          public static decimal min;
          public static decimal max;
          public static string deleted;
          public static string house;
          public static string tmode;
        
        public cryshowstudentdetail()
        {

            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select *    from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' or (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name", sc);
             if (min == 0 && max == 0)
                 str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone,stu_image_path  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "'  and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'  and STU_HOUSE_ID like '" + house + "' and STU_Trans_Mode like '" + tmode + "' order by Class,Roll";
            else
                 str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone,stu_image_path  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'   and STU_HOUSE_ID like '" + house + "' and STU_Trans_Mode like '" + tmode + "' order by Class,Roll,Student_Name";
             SqlDataAdapter da = new SqlDataAdapter(str,sc);
            DataSet ds1 = new DataSet();
             da.Fill(ds1, "View_studentReport");

             cRptStudentDetail crptdetail = new cRptStudentDetail();
            crptdetail.SetDataSource(ds1.Tables["View_studentReport"]);
            crptdetail.SetParameterValue("dt1", dt1);
            crptdetail.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
          //  crptdetail.PrintToPrinter(1, false, 0, 0);
            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();
        }
    }
}
