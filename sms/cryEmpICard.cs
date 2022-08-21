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
    public partial class cryEmpICard : Form
    {
        public static DateTime dt1, dt2;
        public static string Dept;
        public static string Roll, Admno, session;

        public cryEmpICard()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //if (Roll == "0" ||Roll==null)
            //{ Roll = "%"; }
            if (Admno == "0")
            { Admno = "%"; }
            if (Dept == "0")
            { Dept = "%"; }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblemployee where EMP_CODE like '" + Admno + "' and DEPT_ID like '" + Dept + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tblemployee");
            cRptEmpICard ic = new cRptEmpICard();
            ic.SetDataSource(ds1.Tables["View_tblemployee"]);
            //
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            ic.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            ic.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            ic.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            //ic.SetParameterValue("sign", ds1.Tables[0].Rows[0]["SINGATURE"].ToString());
            //ic.SetParameterValue("session", session);


            crystalReportViewer1.ReportSource = ic;
            crystalReportViewer1.Refresh();

        }
    }
}
