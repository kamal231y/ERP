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
    public partial class cryAttendanceReport : Form
    {
        //public string dt1;
        public static string secID;
        public static DateTime dt1;
        public static DateTime dt2;
        public static string roll,tp,ta,tl,twd;



        public cryAttendanceReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;
            DataSet ds1;
            da = new SqlDataAdapter("Attendence_report", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@dt1",Convert.ToDateTime(dt1.ToString()));
            da.SelectCommand.Parameters.AddWithValue("@dt2", Convert.ToDateTime(dt2.ToString()));
            da.SelectCommand.Parameters.AddWithValue("@cls_id", (secID));
            da.SelectCommand.Parameters.AddWithValue("@stu_id", roll);
            ds1 = new DataSet();
            da.Fill(ds1);
            cRptAttendanceReport crad = new cRptAttendanceReport();
            da = new SqlDataAdapter("select * from Tempatt", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "Tempatt");
            crad.SetDataSource(ds1.Tables["Tempatt"]);
            crad.SetParameterValue("dt1", dt1);
            crad.SetParameterValue("dt2", dt2);
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
