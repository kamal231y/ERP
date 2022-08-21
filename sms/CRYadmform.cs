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
    public partial class CRYadmform : Form
    {
        public static string ADM;
        public static string mn;
        public static int yy;
        public static string dy;
        public static string dyWords;
        public static string yyWords;
        public CRYadmform()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_STUDENTREPORT where AdmNo='" + ADM + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, " View_STUDENTREPORT");
            cRptAdm Admform = new cRptAdm();
            Admform.SetDataSource(ds1.Tables[" View_STUDENTREPORT"]);
            Admform.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            
            Admform.SetParameterValue("mn", mn);
            Admform.SetParameterValue("yy", yyWords);
            Admform.SetParameterValue("dy", dyWords); 





            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            Admform.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            Admform.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            Admform.SetParameterValue("AFFLIATIONCODE", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
            Admform.SetParameterValue("SCHOOLCODE", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());
            Admform.SetParameterValue("EMAIL", ds1.Tables[0].Rows[0]["EMAIL"].ToString());
            Admform.SetParameterValue("TELNO", ds1.Tables[0].Rows[0]["companytelno"].ToString());
           
            crystalReportViewer1.ReportSource = Admform;
            crystalReportViewer1.Refresh();

        }

        private void CRYadmform_Load(object sender, EventArgs e)
        {

        }
    
    }
}
