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
    public partial class cryClassFeeReport : Form
    {
        public static string classname;
        public cryClassFeeReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (classname == "0")
                    classname = "%";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_FeeReport where CLF_CLS_ID like '" + classname + "'", sc);
            DataSet ds1 = new DataSet();         //where RegNo='" + RegNo + "'
            da.Fill(ds1, "view_FeeReport");
            cRptFeeReport frpt = new cRptFeeReport();
            frpt.SetDataSource(ds1.Tables["view_FeeReport"]);

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            frpt.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            frpt.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            frpt.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            frpt.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            frpt.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            //regFrmSale.SetParameterValue("CompanyPhone", "Ph : " + ds1.Tables[0].Rows[0]["companytelno"].ToString() + "," + ds1.Tables[0].Rows[0]["companyfaxno"].ToString());


            crystalReportViewer1.ReportSource = frpt;
            crystalReportViewer1.Refresh();
        }

        private void cryClassFeeReport_Load(object sender, EventArgs e)
        {

        }
    }
}
