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
    public partial class crybookreturnstatus : Form
    {
        //public string dt1;
        public static string barcode;
        public static string SUB;
        public static DateTime dt1;
        public static DateTime dt2;
        public static string ss;
        public static string roll;
        public static string itmId;



        public crybookreturnstatus()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  *  from View_issueRETURNbook where cast(issue_date as date)  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "' and subject_name like '" + SUB + "'  and barcode_no like '" + barcode +  "' order by issue_NO ";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_issueRETURNbook");

            cRptbookReturnstatus crpur = new cRptbookReturnstatus();
            crpur.SetDataSource(ds1.Tables["View_issueRETURNbook"]);
            if (ss == "1")
            {
                crpur.SetParameterValue("ss", "Book Return Status");
            }
            else
            {
                crpur.SetParameterValue("ss", "Book Return Status");
            }

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crpur.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            crpur.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = crpur;
            crystalReportViewer1.Refresh();

        }
    }
}
