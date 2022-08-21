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
    public partial class cryReportCardAnnualwise1 : Form
    {
        public static string classid;
        public static string stuid;
        public static string type;
        public static string exam;
        public static string examName,session;
        public static int seq;
        public cryReportCardAnnualwise1()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
        
            if (type == "1")
            {
               
                   
                        dataclass dc = new dataclass();
                        SqlConnection sc = dc.con();
                        SqlDataAdapter da = new SqlDataAdapter("Find_StudentReportCard_Grade", sc);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
                        da.SelectCommand.Parameters.AddWithValue("@class", classid);
                        da.SelectCommand.Parameters.AddWithValue("@EXAM", exam);
                        DataSet ds1 = new DataSet();
                        da.SelectCommand.CommandTimeout = 1500;
                        da.Fill(ds1, "VIEW_FINAL_REPORT_CARD");
                        crptITermWiseReportGrade rcaw2 = new crptITermWiseReportGrade();
                        rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                        rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                        rcaw2.SetParameterValue("exam", examName);


                        rcaw2.SetParameterValue("session", session);

                        da = new SqlDataAdapter("select * from company_t", sc);
                        ds1 = new DataSet();
                        da.Fill(ds1, "company_t");
                        rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                        rcaw2.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                        rcaw2.SetParameterValue("schoolcode", ds1.Tables[0].Rows[0]["Email"].ToString());
                        rcaw2.SetParameterValue("estd", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                        rcaw2.SetParameterValue("affiliation", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
                        crystalReportViewer1.ReportSource = rcaw2;
                        crystalReportViewer1.Refresh();

                    }
                else
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("Find_StudentFinalReportCrd_FINAL_scholostic_ISS", sc);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
                    da.SelectCommand.Parameters.AddWithValue("@EXAM", exam);
                    da.SelectCommand.Parameters.AddWithValue("@class", classid);

                    DataSet ds1 = new DataSet();
                    da.Fill(ds1, "VIEW_FINAL_REPORT_CARD");
                    if (classid == "7")
                    {
                        crptFirstTermWiseReport_vir rcaw2 = new crptFirstTermWiseReport_vir();
                        rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                        rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                        da = new SqlDataAdapter("select * from company_t", sc);
                        ds1 = new DataSet();
                        da.Fill(ds1, "company_t");
                        rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                        rcaw2.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                        rcaw2.SetParameterValue("schoolcode", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());

                        rcaw2.SetParameterValue("affiliation", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
                        crystalReportViewer1.ReportSource = rcaw2;
                        crystalReportViewer1.Refresh();
                    }
                    else if (classid == "3")
                    {
                        crptFirstTermWiseReport_vir1a rcaw2 = new crptFirstTermWiseReport_vir1a();
                        rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                        rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                        da = new SqlDataAdapter("select * from company_t", sc);
                        ds1 = new DataSet();
                        da.Fill(ds1, "company_t");
                        rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                        rcaw2.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                        rcaw2.SetParameterValue("schoolcode", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());

                        rcaw2.SetParameterValue("affiliation", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
                        crystalReportViewer1.ReportSource = rcaw2;
                        crystalReportViewer1.Refresh();
                    }
                    else
                    {
                        crptFirstTermWiseReport rcaw2 = new crptFirstTermWiseReport();
                        rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                        rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                        da = new SqlDataAdapter("select * from company_t", sc);
                        ds1 = new DataSet();
                        da.Fill(ds1, "company_t");
                        rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                        rcaw2.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                        rcaw2.SetParameterValue("schoolcode", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());
                        
                        rcaw2.SetParameterValue("affiliation", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
                        crystalReportViewer1.ReportSource = rcaw2;
                        crystalReportViewer1.Refresh();

                    }

                }
           
        }
    }
}
