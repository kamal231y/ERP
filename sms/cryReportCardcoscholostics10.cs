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
    public partial class cryReportCardcoscholostics10 : Form
    {
        public static string classid;
        public static string stuid;
        public static string type;
        public static string exam;
        public static string examName,session;
        public static int seq;
        public cryReportCardcoscholostics10()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,Testexamid from View_AddmissionTestResult 
            SqlDataAdapter da = new SqlDataAdapter("select * from View_coscholostic10th where stu_adm_no='" + stuid + "' order by stu_adm_no", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AdmitCard10");
            cRptadmitCard10 AdmitCard = new cRptadmitCard10();
            AdmitCard.SetDataSource(ds1.Tables["View_AdmitCard10"]);
            //
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            AdmitCard.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            AdmitCard.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
          //  AdmitCard.SetParameterValue("examn", examn);


            crystalReportViewer1.ReportSource = AdmitCard;
            crystalReportViewer1.Refresh();

                   

                }
           
        }
    }

