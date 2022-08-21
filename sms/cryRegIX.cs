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
    public partial class cryRegIX : Form
    {
        public static string classid;
        public static int rollno;
        public cryRegIX()
        {
           
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_printRegIX where STU_CLS='" + classid + "' and STU_ADM_NO='" + rollno + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_printRegIX");
            if (classid == "12")
            {
                cRptRegIX regix = new cRptRegIX();
                regix.SetDataSource(ds1.Tables["View_printRegIX"]);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                regix.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                regix.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = regix;
            }
            else
            {
                cRptRegXI regxi = new cRptRegXI();
                regxi.SetDataSource(ds1.Tables["View_printRegIX"]);

                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                regxi.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                regxi.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = regxi;
            }           
            crystalReportViewer1.Refresh();
        }
    }
}
