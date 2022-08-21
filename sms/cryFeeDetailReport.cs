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
    public partial class cryFeeDetailReport : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string hostel;
        public cryFeeDetailReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_showstudentfeeledger where  FRC_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STU_CLS_ID like '" + classid + "' and stu_hostler like '" + hostel + "' order by FRC_no";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_showstudentfeeledger");

            cRptFeeDetailReport FDR = new cRptFeeDetailReport();

            FDR.SetDataSource(ds1.Tables["View_showstudentfeeledger"]);
            FDR.SetParameterValue("dt1", dt1); 
            FDR.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from company_t",sc);
            ds1 = new DataSet();
            da.Fill(ds1,"company_t");
            FDR.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            FDR.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = FDR;
            crystalReportViewer1.Refresh();
        

        }
    }
}
