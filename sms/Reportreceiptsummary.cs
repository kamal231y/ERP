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
    public partial class Reportreceiptsummary : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string hostel;
        public static string mode;
        public static string admno;
        public static string usertype;
        public static int concession;
       
        public Reportreceiptsummary()
        {
          InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_ReceiptSummaryRpt where  FRC_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STU_CLS_ID like '" + classid + "' and frc_user  like '" + usertype + "' and status like '" + hostel + "' and mop like '" + mode + "'and stu_adm_no like '" + admno + "' order by FRC_NO,FRC_date";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_ReceiptSummaryRpt");
            if (concession == 1)
            {
                if (mode == "%")
                {
                    reportsummary2014mnps crptdetail = new reportsummary2014mnps();
                    crptdetail.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                    crptdetail.SetDataSource(ds1.Tables["View_ReceiptSummaryRpt"]);
                    crptdetail.SetParameterValue("DT1", dt1);
                    crptdetail.SetParameterValue("DT2", dt2);

                    da = new SqlDataAdapter("select * from COMPANY_T", sc);
                    ds1 = new DataSet();
                    da.Fill(ds1, "company_t");
                    crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                    crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                    crystalReportViewer1.ReportSource = crptdetail;
                    crystalReportViewer1.Refresh();
                }
                if (mode == "Cash")
                {
                    reportsummary2016cash crptdetail = new reportsummary2016cash();
                    crptdetail.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                    crptdetail.SetDataSource(ds1.Tables["View_ReceiptSummaryRpt"]);
                    crptdetail.SetParameterValue("DT1", dt1);
                    crptdetail.SetParameterValue("DT2", dt2);
                    crptdetail.SetParameterValue("mode", mode);

                    da = new SqlDataAdapter("select * from COMPANY_T", sc);
                    ds1 = new DataSet();
                    da.Fill(ds1, "company_t");
                    crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                    crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                    crystalReportViewer1.ReportSource = crptdetail;
                    crystalReportViewer1.Refresh();
                }
                else if (mode != "%" && mode != "Cash")
                {
                    reportsummary2016mode crptdetail = new reportsummary2016mode();
                    crptdetail.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                    crptdetail.SetDataSource(ds1.Tables["View_ReceiptSummaryRpt"]);
                    crptdetail.SetParameterValue("DT1", dt1);
                    crptdetail.SetParameterValue("DT2", dt2);
                    crptdetail.SetParameterValue("mode", mode);

                    da = new SqlDataAdapter("select * from COMPANY_T", sc);
                    ds1 = new DataSet();
                    da.Fill(ds1, "company_t");
                    crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                    crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                    crystalReportViewer1.ReportSource = crptdetail;
                    crystalReportViewer1.Refresh();
                }

            }
            else
            {
                dataclass dc1 = new dataclass();
                SqlConnection sc1 = dc.con();
                string str1 = "select  * from View_ReceiptSummaryRpt where  frc_concession!=0 and FRC_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and frc_user  like '" + usertype + "'and STU_CLS_ID like '" + classid + "' and status like '" + hostel + "' and stu_adm_no like '" + admno + "' order by FRC_NO,FRC_date";

                SqlDataAdapter da1 = new SqlDataAdapter(str1, sc);
                DataSet ds11 = new DataSet();
                da1.Fill(ds11, "View_ReceiptSummaryRpt");

                reportsummary2016concession crptdetail = new reportsummary2016concession();
                crptdetail.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                crptdetail.SetDataSource(ds11.Tables["View_ReceiptSummaryRpt"]);
                crptdetail.SetParameterValue("DT1", dt1);
                crptdetail.SetParameterValue("DT2", dt2);

                da1 = new SqlDataAdapter("select * from COMPANY_T", sc);
                ds11 = new DataSet();
                da1.Fill(ds11, "company_t");
                crptdetail.SetParameterValue("pageheader", ds11.Tables[0].Rows[0]["COM_NAME"].ToString());
                crptdetail.SetParameterValue("address", ds11.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = crptdetail;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
