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
    public partial class cryStudentOldDuesClassWise : Form
    {
        public static string classname;
        public static string classnameT;
        public cryStudentOldDuesClassWise()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("select ADM_NO, StudentName,Roll_No,Dues,STU_FATHER_NAME,STU_MOBILE from View_StudentOldDues where STC_SEC_ID ='" + classname + "' and dues>0", sc);
            SqlDataAdapter da = new SqlDataAdapter("select ADM_NO, StudentName,Roll_No,Dues,STU_FATHER_NAME,STU_MOBILE from View_StudentOldDues where STC_SEC_ID ='" + classname + "' and dues>0", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_StudentOldDues");

            cRptStudentOldDuesClassWise sodcw = new cRptStudentOldDuesClassWise();
            sodcw.SetDataSource(ds1.Tables["View_StudentOldDues"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            sodcw.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            sodcw.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            sodcw.SetParameterValue("classnameT", classnameT);       
            crystalReportViewer1.ReportSource = sodcw;
            crystalReportViewer1.Refresh();
        }
    }
}
