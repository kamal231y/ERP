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
    public partial class cryvisitorDetail : Form
    {
        public static DateTime dt1, dt2;
        public static string Department;
        public static string EmpName;
        public static string Designation;
        public static string intime;
        public static string Mobileno;
        public static string outtime;

        public cryvisitorDetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_visitor where cast (vdate as date) between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' AND dept_desc like '" + Department + "' AND EMP_NAME like '" + EmpName + "'  AND DESG_DESC like '" + Designation + "' and intime>='" + intime + "' and outtime>='" + outtime + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_visitor");

            cRptvisitorDetail cryobj = new cRptvisitorDetail();
            cryobj.SetDataSource(ds1.Tables["View_visitor"]);
            cryobj.SetParameterValue("dt1", dt1);
            cryobj.SetParameterValue("dt2", dt2);
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            cryobj.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            cryobj.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            crystalReportViewer1.ReportSource = cryobj;
            crystalReportViewer1.Refresh();
        }
    }
}
