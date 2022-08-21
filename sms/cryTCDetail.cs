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
    public partial class cryTCDetail : Form
    {
        public static string admno, cls, tcno;
        public static DateTime dt1,dt2 ;

        public cryTCDetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str;
            if(cls!=null&&cls!="%")
             str = "select  * from view_tc where  currentclass like '" + cls + "' and  TC_ADM_NO like '" + admno + "' order by tc_no";
            else
                str = "select  * from view_tc where  cast(TC_ISSUE_DATE as date)  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "' and currentclass like '" + cls + "' and  TC_ADM_NO like '" + admno + "' order by tc_no";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tc");

            cRptTcDetail tcrptDe = new cRptTcDetail();

            tcrptDe.SetDataSource(ds1.Tables["View_tc"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            tcrptDe.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            tcrptDe.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = tcrptDe;
            crystalReportViewer1.Refresh();
        }
    }
}
