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
using sms.BLClass;

namespace sms
{
    public partial class cryRegistrationReport : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string usertype;
        public cryRegistrationReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNoid,StudentName,resultstatus,mobileno,Testexamid  from View_AddmissionTestResult where testexamid='" + test + "' and resultstatus=1", sc);
            SqlDataAdapter da = new SqlDataAdapter("select * from tblStudentFormRegistration where  RDate>='" + dt1.ToString("MM/dd/yyyy") + "' and RDate<='" + dt2.ToString("MM/dd/yyyy") + "' order by id", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "tblStudentFormRegistration");
            cRptRegReport1 regReport = new cRptRegReport1();
            regReport.SetDataSource(ds1.Tables["tblStudentFormRegistration"]);

            regReport.SetParameterValue("dt1", dt1);
            regReport.SetParameterValue("dt2", dt2);

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            regReport.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regReport.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regReport.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            regReport.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            regReport.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());

            //regReport.SetParameterValue("classn", classn.ToString());
          
            crystalReportViewer1.ReportSource = regReport;
            crystalReportViewer1.Refresh();
        }
    }
}
