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
    public partial class cryReceiptDetailReport : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string hostel;
        public static string roll;
        public static string fee;
        public cryReceiptDetailReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_showstudentfeeledger where  FRC_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STU_CLS_ID like '" + classid + "' and status like'" + hostel + "' and stu_id like  '" + roll + "' and fee_desc like '"+fee+"' order by FRC_no";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_showstudentfeeledger");

            cRptReceiptDetailReport ReceiptDetailRep = new cRptReceiptDetailReport();
            ReceiptDetailRep.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            ReceiptDetailRep.SetDataSource(ds1.Tables["View_showstudentfeeledger"]);
            ReceiptDetailRep.SetParameterValue("dt1", dt1.ToString("MM/dd/yyyy"));
            ReceiptDetailRep.SetParameterValue("dt2", dt2.ToString("MM/dd/yyyy"));

            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            ReceiptDetailRep.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            ReceiptDetailRep.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = ReceiptDetailRep;
            crystalReportViewer1.Refresh();
        }
    }
}
