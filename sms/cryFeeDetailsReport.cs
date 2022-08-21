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
    public partial class cryFeeDetailsReport : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string classSec;
        public static string hostel;
        public cryFeeDetailsReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select   * from View_DailyScroll where  FRC_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STC_SEC_ID like '" + classid + "' and stu_hostler like '" + hostel + "' order by STC_SEC_ID ,FRC_no";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_DailyScroll");

            cRptFeeDetailsReport fdrs = new cRptFeeDetailsReport();
            fdrs.SetDataSource(ds1.Tables["View_DailyScroll"]);
            fdrs.SetParameterValue("dt1", dt1);
            fdrs.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            fdrs.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            fdrs.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            fdrs.SetParameterValue("classSec", classSec);

            crystalReportViewer1.ReportSource = fdrs;
            crystalReportViewer1.Refresh();
        

        }
    }
}
