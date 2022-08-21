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
    public partial class crySchedulerforabsent : Form
    {
        public static string classname;
        public static DateTime dt1;
        public crySchedulerforabsent()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_absent_schedule where cast(sch_date as date)='" + dt1 + "' order by class", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_absent_schedule");
            cRptabsentschedule regReport = new cRptabsentschedule();
            regReport.SetDataSource(ds1.Tables["View_absent_schedule"]);
            regReport.SetParameterValue("dt1", dt1);

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            regReport.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regReport.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regReport.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            regReport.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            regReport.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());

            crystalReportViewer1.ReportSource = regReport;
            crystalReportViewer1.Refresh();

        }
    }
}
