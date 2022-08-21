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
    public partial class crysaleprintrecord : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string rec;
        public crysaleprintrecord()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_saleramraj where Amount is not null order by id", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_saleramraj");
            cRPTsalepaidreport regReport = new cRPTsalepaidreport();
            regReport.SetDataSource(ds1.Tables["View_saleramraj"]);            

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            regReport.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regReport.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regReport.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            regReport.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            regReport.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());

            //regReport.SetParameterValue("classn", classn.ToString());
          
            crystalReportViewer1.ReportSource = regReport;
            crystalReportViewer1.Refresh();
        }
    }
}
