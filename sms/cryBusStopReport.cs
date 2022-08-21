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
    public partial class cryBusStopReport : Form
    {
        public static string classid;
        public cryBusStopReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_tblstubusstop where   sec_id like '" + classid + "' ";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tblstubusstop");

            cRptBusStopReport bsr = new cRptBusStopReport();
            bsr.SetDataSource(ds1.Tables["View_tblstubusstop"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            bsr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            bsr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = bsr;
            crystalReportViewer1.Refresh();
        }

        
       
    }
}
