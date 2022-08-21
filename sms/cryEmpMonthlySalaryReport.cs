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
    public partial class cryEmpMonthlySalaryReport : Form
    {
        public static string empName;
        public static string dept,mnth, month;
        public static DateTime dt1;
        public static DateTime dt2;
        public cryEmpMonthlySalaryReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_EmployeeSalary2 where emp_code like '" + empName + "' and emp_dept_id like '" + dept + "' AND mnth like '"+month+"'";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_EmployeeSalary2");

            cRptEmpMonthlySalaryReport esr = new cRptEmpMonthlySalaryReport();
            esr.SetDataSource(ds1.Tables["View_EmployeeSalary2"]);
            esr.SetParameterValue("mnth", mnth);
           // esr.SetParameterValue("dt2", dt2);

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
