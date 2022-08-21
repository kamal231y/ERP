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
    public partial class CRYCBSadmform : Form
    {
        public static string ADM;
        public static string Age;
        public static string month;
        public static string day;
        public static string mn;
        public static int yy;
        public static string dy;
        public static string dyWords;
        public static string yyWords;
        public CRYCBSadmform()
        {
            InitializeComponent();
        }

       

        private void CRYCBSadmform_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_ADMFORM where AdmNo='" + ADM + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, " View_ADMFORM");
            cRptCBSAdm CBSAdmform = new cRptCBSAdm();
            CBSAdmform.SetDataSource(ds1.Tables[" View_ADMFORM"]);
            CBSAdmform.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;

            CBSAdmform.SetParameterValue("mn", mn);
            CBSAdmform.SetParameterValue("yy", yyWords);
            CBSAdmform.SetParameterValue("dy", dyWords);
            CBSAdmform.SetParameterValue("Age", Age);
            CBSAdmform.SetParameterValue("month", month);
            CBSAdmform.SetParameterValue("day", day);





            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            CBSAdmform.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            CBSAdmform.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            CBSAdmform.SetParameterValue("TELNO", ds1.Tables[0].Rows[0]["companytelno"].ToString());

            crystalReportViewer1.ReportSource = CBSAdmform;
            crystalReportViewer1.Refresh();


        }
    
    }
}
