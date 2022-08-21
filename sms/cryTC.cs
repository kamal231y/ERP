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
    public partial class cryTC : Form
    {
        public static string admno;
        public static string tcno;
        public static string mn;
        public static int yy;
        public static string dy;
        public static string dyWords;
        public static string yyWords;
        
        public cryTC()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (Form1.TC_FORMAT == 0)
            {
                if (admno == "0")
                {
                    admno = "%";
                }
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string str = "select  * from View_tc where  tc_adm_no like '" + admno + "' and tc_no like '" + tcno + "'";
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_tc");

                cRptTC tcrpt = new cRptTC();
                tcrpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                tcrpt.SetDataSource(ds1.Tables["View_tc"]);
                tcrpt.SetParameterValue("mn", mn);
                tcrpt.SetParameterValue("yy", yyWords);
                tcrpt.SetParameterValue("dy", dyWords);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                tcrpt.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                tcrpt.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                tcrpt.SetParameterValue("AFFLIATIONCODE", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
                tcrpt.SetParameterValue("SCHOOLCODE", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());
                tcrpt.SetParameterValue("EMAIL", ds1.Tables[0].Rows[0]["EMAIL"].ToString());
                tcrpt.SetParameterValue("TELNO", ds1.Tables[0].Rows[0]["companytelno"].ToString());


                crystalReportViewer1.ReportSource = tcrpt;
                crystalReportViewer1.Refresh();
            }
            else {

                if (admno == "0")
                {
                    admno = "%";
                }
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string str = "select  * from View_tc where  tc_adm_no like '" + admno + "' and tc_no like '" + tcno + "'";
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_tc");

                cRptTCHINDI tcrpt = new cRptTCHINDI();
                tcrpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                tcrpt.SetDataSource(ds1.Tables["View_tc"]);
                tcrpt.SetParameterValue("mn", mn);
                tcrpt.SetParameterValue("yy", yyWords);
                tcrpt.SetParameterValue("dy", dyWords);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                tcrpt.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                tcrpt.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                tcrpt.SetParameterValue("AFFLIATIONCODE", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
                tcrpt.SetParameterValue("SCHOOLCODE", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());
                tcrpt.SetParameterValue("EMAIL", ds1.Tables[0].Rows[0]["EMAIL"].ToString());
                tcrpt.SetParameterValue("TELNO", ds1.Tables[0].Rows[0]["companytelno"].ToString());


                crystalReportViewer1.ReportSource = tcrpt;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
