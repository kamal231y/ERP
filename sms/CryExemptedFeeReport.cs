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
using sms.BLClass;

namespace sms
{
    public partial class CryExemptedFeeReport : Form
    {
        public static string Class;
            

        public CryExemptedFeeReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (Class == null)
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from View_FeeExemptedReport order by STU_ADM_NO", sc);

                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_FeeExemptedReport");
                crptFeeExemptedReport ReportCard = new crptFeeExemptedReport();
                ReportCard.SetDataSource(ds1.Tables["View_FeeExemptedReport"]);
                //
                da = new SqlDataAdapter("select * from COMPANY_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "COMPANY_T");
                ReportCard.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                ReportCard.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());


                crystalReportViewer1.ReportSource = ReportCard;
                crystalReportViewer1.Refresh();
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from View_FeeExemptedReport where  STC_SEC_ID=" + Class + " order by STU_ADM_NO", sc);

                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_FeeExemptedReport");
                crptFeeExemptedReport ReportCard = new crptFeeExemptedReport();
                ReportCard.SetDataSource(ds1.Tables["View_FeeExemptedReport"]);
                //
                da = new SqlDataAdapter("select * from COMPANY_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "COMPANY_T");
                ReportCard.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                ReportCard.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());


                crystalReportViewer1.ReportSource = ReportCard;
                crystalReportViewer1.Refresh();
            }

        }
    }
}
