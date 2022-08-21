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
    public partial class cryReportCardtermWise : Form
    {
        public static string Class;
        public static string exam;
        public static string stuid;
        public static string examT;
        public cryReportCardtermWise()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_TERM_WISE_REPORTCARD where  STC_SEC_ID like '" + Class + "' and stu_id like '" + stuid + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_TERM_WISE_REPORTCARD");//StudentDues 30jan2014
            if (exam == "1")
            {
                cRptEval_1 db = new cRptEval_1();
                db.SetDataSource(ds1.Tables["View_TERM_WISE_REPORTCARD"]);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                db.SetParameterValue("pageheader", ds1.Tables["company_t"].Rows[0]["COM_NAME"].ToString());
                db.SetParameterValue("compayAddress", ds1.Tables["company_t"].Rows[0]["companyaddress"].ToString());

                crystalReportViewer1.ReportSource = db;
                crystalReportViewer1.Refresh();
            }
            if (exam == "2")
            {
                cRptTermWiseReportCard db = new cRptTermWiseReportCard();
                db.SetDataSource(ds1.Tables["View_TERM_WISE_REPORTCARD"]);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                db.SetParameterValue("pageheader", ds1.Tables["company_t"].Rows[0]["COM_NAME"].ToString());
                db.SetParameterValue("compayAddress", ds1.Tables["company_t"].Rows[0]["companyaddress"].ToString());

                crystalReportViewer1.ReportSource = db;
                crystalReportViewer1.Refresh();
            }
            if (exam == "3")
            {
                cRptEVAL_3 db = new cRptEVAL_3();
                db.SetDataSource(ds1.Tables["View_TERM_WISE_REPORTCARD"]);
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                db.SetParameterValue("pageheader", ds1.Tables["company_t"].Rows[0]["COM_NAME"].ToString());
                db.SetParameterValue("compayAddress", ds1.Tables["company_t"].Rows[0]["companyaddress"].ToString());

                crystalReportViewer1.ReportSource = db;
                crystalReportViewer1.Refresh();
            }

        }
    }
}
