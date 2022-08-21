using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;
using System.Collections;
using sms.DATALAYER;


namespace sms
{
    public partial class CryStuAttandence : Form
    {
        public static int id;
        public CryStuAttandence()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_AttendanceDetail ", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AttendanceDetail");

            CrPtStuAttendance crptdetail = new CrPtStuAttendance();
            crptdetail.SetDataSource(ds1.Tables["View_AttendanceDetail"]);
            //crptdetail.SetParameterValue("dt1", dt1);
            //crptdetail.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from tblCompanyParameters", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "tblCompanyParameters");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["companyname"].ToString());

            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();
        }
    }
}
