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
    public partial class CryReportCard : Form
    {
        public static string classn;
        public static string examn;
        public static string RollNo;

        public CryReportCard()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            SqlDataAdapter da = new SqlDataAdapter("select * from View_ReportCard where sec_id='" + classn + "' and Admission_no='" + RollNo + "'", sc);
            DataSet ds1 = new DataSet();                                                                   
            da.Fill(ds1, "View_ReportCard");
            cRptReportCard ReportCard = new cRptReportCard();
            ReportCard.SetDataSource(ds1.Tables["View_ReportCard"]);
            //
            da = new SqlDataAdapter("select * from tblCompanyParameters", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "tblCompanyParameters");
            ReportCard.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["companyname"].ToString());
            ReportCard.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            //ReportCard.SetParameterValue("examn", examn);


            crystalReportViewer1.ReportSource = ReportCard;
            crystalReportViewer1.Refresh();


        }
    }
}
