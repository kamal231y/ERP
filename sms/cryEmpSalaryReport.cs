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
    public partial class cryEmpSalaryReport : Form
    {
        public static string empName;
        public static string dept;
        public static DateTime dt1;
        public static DateTime dt2;
        public cryEmpSalaryReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_EmployeeSalary where emp_code like '" + empName + "' OR department like '" + dept + "' AND DATE between '" + dt1.ToString("yyyy/MM/dd") + "' AND '" + dt2.ToString("yyyy/MM/dd") + "'";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_EmployeeSalary");

            cRptEmpSalaryReport esr = new cRptEmpSalaryReport();
            esr.SetDataSource(ds1.Tables["View_EmployeeSalary"]);
            esr.SetParameterValue("dt1", dt1);
            esr.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            esr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            esr.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = esr;
            crystalReportViewer1.Refresh();


        }
    }
}
