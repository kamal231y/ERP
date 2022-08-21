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
    public partial class CryAdmitCard : Form
    {
        public static DateTime dt1, dt2;
        public static string classn;
        public static string examn;
        public CryAdmitCard()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            string str = "select * from dbo.View_AdmitCard3 fromdt  between '" + dt1.ToString("yyyy/MM/dd") + "' and todt '" + dt2.ToString("yyyy/MM/dd");
           
                SqlDataAdapter da = new SqlDataAdapter("select * from dbo.View_AdmitCard3 where sec_id='" + classn + "' order by stc_roll_no", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AdmitCard3");
            cRptadmitCard3 AdmitCard = new cRptadmitCard3();
            AdmitCard.SetDataSource(ds1.Tables["View_AdmitCard3"]);
          
            AdmitCard.SetParameterValue("fromdt", dt1);
            AdmitCard.SetParameterValue("todt", dt2);
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            AdmitCard.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            AdmitCard.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            AdmitCard.SetParameterValue("AFFLIATION", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
            AdmitCard.SetParameterValue("email", ds1.Tables[0].Rows[0]["EMAIL"].ToString());
            AdmitCard.SetParameterValue("examn", examn);
            AdmitCard.SetParameterValue("SESSION", ds1.Tables[0].Rows[0]["SESSION"].ToString());
            crystalReportViewer1.ReportSource = AdmitCard;
            crystalReportViewer1.Refresh();

        }
    }
}
