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
    public partial class crybustime : Form
    {
       
        public static string ROUTE;

        public crybustime()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (ROUTE != "0")
            {
                SqlDataAdapter da = new SqlDataAdapter("select *  from View_bustime where STOP_ROUTE='" + ROUTE + "'", sc);
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_bustime");

                cRptbustime cryobj = new cRptbustime();

                cryobj.SetDataSource(ds1.Tables["View_bustime"]);
                SqlCommand com = new SqlCommand("select * from COMPANY_T");
                da = new SqlDataAdapter("select * from COMPANY_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "COMPANY_T");
                cryobj.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                cryobj.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                cryobj.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                cryobj.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                cryobj.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());


                crystalReportViewer1.ReportSource = cryobj;
                crystalReportViewer1.Refresh();
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select *  from View_bustime order by STOP_ROUTE", sc);
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_bustime");

                cRptbustime cryobj = new cRptbustime();

                cryobj.SetDataSource(ds1.Tables["View_bustime"]);
                SqlCommand com = new SqlCommand("select * from COMPANY_T");
                da = new SqlDataAdapter("select * from COMPANY_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "COMPANY_T");
                cryobj.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                cryobj.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                cryobj.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                cryobj.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                cryobj.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());


                crystalReportViewer1.ReportSource = cryobj;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
