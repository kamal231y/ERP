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
    public partial class CryExamschedule : Form
    {
        public static DateTime dt1, dt2;
        public static string classn;
        public static string examn;
        public static string classname;
        public CryExamschedule()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            SqlDataAdapter da = new SqlDataAdapter("select * from View_examschedule where SBM_CLS_ID='" + classn + "' and EXAM_DESC='" + examn + "' ORDER BY Dates", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_examschedule");
            cRptexamschedule AdmitCard = new cRptexamschedule();
            AdmitCard.SetDataSource(ds1.Tables["View_examschedule"]);
            //
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            AdmitCard.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            AdmitCard.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            AdmitCard.SetParameterValue("AFFLIATION", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
            AdmitCard.SetParameterValue("examn", classname);


            crystalReportViewer1.ReportSource = AdmitCard;
            crystalReportViewer1.Refresh();

        }
    }
}
