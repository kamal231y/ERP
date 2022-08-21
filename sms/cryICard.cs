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
    public partial class cryICard : Form
    {
        public static DateTime dt1, dt2;
        public static string classid;
        public static string Roll, Admno, session;
        public static int type;

        public cryICard()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {




            if (Roll == "0" ||Roll==null)
            { Roll = "%"; }
            if (Admno == "0")
            { Admno = "%"; }
            if (classid == "0")
            { classid = "%"; }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            SqlDataAdapter da = new SqlDataAdapter("select * from View_AdmitCard3 where stu_adm_no like '" + Admno + "' and SEC_ID like '" + classid + "' and stu_id like '" + Roll + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AdmitCard3");



            if (type == 1)
            {
                cRptICard ic = new cRptICard();
                ic.SetDataSource(ds1.Tables["View_AdmitCard3"]);
                //
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                ic.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                ic.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                ic.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                //  ic.SetParameterValue("sign", ds1.Tables[0].Rows[0]["SINGATURE"].ToString());
                ic.SetParameterValue("session", session);


                crystalReportViewer1.ReportSource = ic;
                crystalReportViewer1.Refresh();
           }

            if (type == 2)
            {
                cRptICard1 ic = new cRptICard1();
                ic.SetDataSource(ds1.Tables["View_AdmitCard3"]);
                //
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                ic.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                ic.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                ic.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                //  ic.SetParameterValue("sign", ds1.Tables[0].Rows[0]["SINGATURE"].ToString());
                ic.SetParameterValue("session", session);


                crystalReportViewer1.ReportSource = ic;
                crystalReportViewer1.Refresh();
            }

            if (type == 3)
            {
                cRptICard ic = new cRptICard();
                ic.SetDataSource(ds1.Tables["View_AdmitCard3"]);
              
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                ic.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                ic.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                ic.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                //  ic.SetParameterValue("sign", ds1.Tables[0].Rows[0]["SINGATURE"].ToString());
                ic.SetParameterValue("session", session);


                crystalReportViewer1.ReportSource = ic;
                crystalReportViewer1.Refresh();
            }

            if (type == 4)
            {
                cRptICard ic = new cRptICard();
                ic.SetDataSource(ds1.Tables["View_AdmitCard3"]);
              
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                ic.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                ic.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                ic.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                //  ic.SetParameterValue("sign", ds1.Tables[0].Rows[0]["SINGATURE"].ToString());
                ic.SetParameterValue("session", session);


                crystalReportViewer1.ReportSource = ic;
                crystalReportViewer1.Refresh();
            }
            if (type == 5)
            {
                cRptICard ic = new cRptICard();
                ic.SetDataSource(ds1.Tables["View_AdmitCard3"]);
               
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                ic.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                ic.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                ic.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                //  ic.SetParameterValue("sign", ds1.Tables[0].Rows[0]["SINGATURE"].ToString());
                ic.SetParameterValue("session", session);


                crystalReportViewer1.ReportSource = ic;
                crystalReportViewer1.Refresh();
            }

        }
    }
}
