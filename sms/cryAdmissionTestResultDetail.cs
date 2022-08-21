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
    public partial class cryAdmissionTestResultDetail : Form
    {
        public static string test;
        public cryAdmissionTestResultDetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            SqlDataAdapter da = new SqlDataAdapter("select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNoid,StudentName,resultstatus as Qualified,mobileno,Testexamid,RDate  from View_AddmissionTestResult where testexamid='" + test + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AddmissionTestResult");
            cRptAdmissionTestResultDetail AdmissionDetail = new cRptAdmissionTestResultDetail();
            AdmissionDetail.SetDataSource(ds1.Tables["View_AddmissionTestResult"]);

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            AdmissionDetail.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            AdmissionDetail.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            AdmissionDetail.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            AdmissionDetail.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            AdmissionDetail.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());


            crystalReportViewer1.ReportSource = AdmissionDetail;
            crystalReportViewer1.Refresh();
        }
    }
}
