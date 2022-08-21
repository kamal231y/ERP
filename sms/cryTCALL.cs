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
    public partial class cryTCALL : Form
    {
        public static string cls;
        public static string tcno;

        public cryTCALL()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_tc where tc_adm_no like '" + tcno + "' and currentclass like '" + cls + "'  ";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tc");

            cRptTC tcrptDe = new cRptTC();
            tcrptDe.SetDataSource(ds1.Tables["View_tc"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            tcrptDe.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            tcrptDe.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = tcrptDe;
            crystalReportViewer1.Refresh();
        }
    }
}
