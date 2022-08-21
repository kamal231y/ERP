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
    public partial class cryEmpsal : Form
    {

        public static string empcode;
        public static string mnth;
        public cryEmpsal()
        {
            InitializeComponent();
        }
             
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select  * from View_EmployeeSalary where emp_code like '" + empcode + "' and mnth='" + mnth + "'", sc);

            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_EmployeeSalary");
            cRptEmpsal regEmpsal = new cRptEmpsal();
            regEmpsal.SetDataSource(ds1.Tables["View_EmployeeSalary"]);



            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            regEmpsal.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regEmpsal.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regEmpsal.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            regEmpsal.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            regEmpsal.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());



            crystalReportViewer1.ReportSource = regEmpsal;
            crystalReportViewer1.Refresh();

        }

        
    }
}
