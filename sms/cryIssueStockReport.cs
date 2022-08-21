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
    public partial class cryIssueStockReport : Form
    {
        public static DateTime dt1;
       // public static DateTime dt2;
        public static string classid;
        public static string StuId;
        public static string GroupNo;

        public cryIssueStockReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_issuebook where  issue_no like '" + GroupNo + "' order by issue_no ";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_issuebook");
            da = new SqlDataAdapter("select * from setting_t ", sc);
            DataTable setting_t = new DataTable();
            da.Fill(setting_t);
            if (setting_t.Rows[0][1].ToString() == "True")
            {
                cRptISSUEBOOKDOUBLE cripr = new cRptISSUEBOOKDOUBLE();
                cripr.SetDataSource(ds1.Tables["View_issuebook"]);
                da = new SqlDataAdapter("select * from company_t ", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                cripr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_name"].ToString());
                cripr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = cripr;
                crystalReportViewer1.Refresh();
            }
       
          else
            {
                cRptissuebook cripr = new cRptissuebook();
                cripr.SetDataSource(ds1.Tables["View_issuebook"]);
               
                da = new SqlDataAdapter("select * from company_t ", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                cripr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_name"].ToString());
                cripr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = cripr;
                crystalReportViewer1.Refresh();
            }
    }
    }
}
