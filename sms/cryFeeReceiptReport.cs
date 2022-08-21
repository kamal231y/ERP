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
    public partial class cryFeeReceiptReport : Form
    {
        public static string Month;
        public static string yaer;
        public static string classid;
        public static string hostel;
        public cryFeeReceiptReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str;
            if (hostel == "Not Paid")
            {
                str = "select * from View_StudentFeeStatus where " + yaer + "=0 and STC_SEC_ID=" + classid + "  ";
            }
            else
            {
                str = "select * from View_StudentFeeStatus where " + yaer + "!=0 and STC_SEC_ID=" + classid + "  ";
            }
           
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
           
            da.Fill(ds1, "View_StudentFeeStatus");

            cRptFeeReceiptReport Frr = new cRptFeeReceiptReport();
            Frr.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            Frr.SetDataSource(ds1.Tables["View_StudentFeeStatus"]);
            Frr.SetParameterValue("Month", Month);
            Frr.SetParameterValue("hostel", hostel);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            Frr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            Frr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = Frr;
            crystalReportViewer1.Refresh();

        }
    }
}
