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
    public partial class crydailytime : Form
    {
       
        public static string emp_cardno;
        public static string month;


        public crydailytime()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select sat_id ,day,RIGHT(CONVERT(VARCHAR,SAT_DATE, 100),7) as sat_date  from EMPLOYEE_ATTENDANCE_T where sat_stu_id='" + emp_cardno + "' and month='" + month + "' order by sat_id ", sc);                    
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_smsreport");

            cRptsMSREPORT cryobj = new cRptsMSREPORT();

            cryobj.SetDataSource(ds1.Tables["View_smsreport"]);
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
