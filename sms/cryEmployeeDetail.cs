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
    public partial class cryEmployeeDetail : Form
    {
        public static DateTime dt1, dt2;
        public static string Department;
        public static string EmpName;
        public static string Designation;
        public static string State;
        public static string Mobileno;
        public static string City;

        public cryEmployeeDetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //SqlCommand com = new SqlCommand("select * from View_tblemployee where EMP_DOJ between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' and DEPT_DESC like '" + Department + "'and EMP_NAME like '" + EmpName + "'  and DEPT_DESC like '" + Designation + "'and STATE_NAME like '" + State + "' and CITY_NAME like '" + City + "' order by EMP_DOJ desc", sc);
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblemployee ", sc);
            //select * from View_tblemployee order by emp_id           
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tblemployee");

            cRptEmployeeDetail cryobj = new cRptEmployeeDetail();
            //CrystalReport1 cryDetails = new CrystalReport1();
            cryobj.SetDataSource(ds1.Tables["View_tblemployee"]);
            cryobj.SetParameterValue("dt1", dt1);
            cryobj.SetParameterValue("dt2", dt2);


            crystalReportViewer1.ReportSource = cryobj;
            crystalReportViewer1.Refresh();
        }
    }
}
