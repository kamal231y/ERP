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
    public partial class cryBusRouteReport : Form
    {
        public static string classid;
        public static string classid1;
        public cryBusRouteReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (classid != "")
            {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
           
                string str = "select  * from View_tblstubusstop where Stop_Route like'" + classid + "' ";
           

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tblstubusstop");

            cRptBusRouteReport brr = new cRptBusRouteReport();
            brr.SetDataSource(ds1.Tables["View_tblstubusstop"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            brr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            brr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = brr;
            crystalReportViewer1.Refresh();
            }
            if (classid1 != "")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();

                string str = "select  * from View_tblstubusstop where Vehicle_id like'" + classid1 + "' ";


                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_tblstubusstop");

                cRptBusRouteReport brr = new cRptBusRouteReport();
                brr.SetDataSource(ds1.Tables["View_tblstubusstop"]);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                brr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                brr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = brr;
                crystalReportViewer1.Refresh();
            }
            

        }
    }
}
