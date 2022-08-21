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
    public partial class cryAttendanceDetail : Form
    {
        //public string dt1;
        public static string secID;
        public static DateTime dt1;
        public static DateTime dt2;
        public static string roll,tp,ta,tl,twd;



        public cryAttendanceDetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from view_Attendance where cast (sat_date as date) between '" + dt1.ToString("yyyy/MM/dd") + "'  and '" + dt2.ToString("yyyy/MM/dd") + "'  and  stc_sec_id like '" + secID + "' and stu_id like '" + roll + "'order by stu_id ";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "view_Attendance");
            cRptAttendanceDetail crad = new cRptAttendanceDetail();
            crad.SetDataSource(ds1.Tables["view_Attendance"]);
            crad.SetParameterValue("dt1", dt1);
            crad.SetParameterValue("dt2", dt2);
            crad.SetParameterValue("tp", tp);
            crad.SetParameterValue("ta", ta);
            crad.SetParameterValue("tl", tl);
            crad.SetParameterValue("twd", twd);
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crad.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            crad.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = crad;
            crystalReportViewer1.Refresh();

        }
    }
}
