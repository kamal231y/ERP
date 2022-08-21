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
    public partial class CRYofcadmform : Form
    {
        public static string ADM;
        
        public CRYofcadmform()
        {
            InitializeComponent();
        }

    
        private void CRYCBSadmform_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from View_admformofc  WHERE ADMNO='" + ADM + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, " View_admformofc");
            cRptOFCAdm CBSAdm = new cRptOFCAdm();
            CBSAdm.SetDataSource(ds1.Tables[" View_admformofc"]);
            CBSAdm.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;





            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            CBSAdm.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            CBSAdm.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            CBSAdm.SetParameterValue("TELNO", ds1.Tables[0].Rows[0]["companytelno"].ToString());

            crystalReportViewer1.ReportSource = CBSAdm;
            crystalReportViewer1.Refresh(); 
        }
    
    }
}
