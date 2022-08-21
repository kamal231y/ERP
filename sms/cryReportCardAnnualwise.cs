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
    public partial class cryReportCardAnnualwise : Form
    {
        public static string classid;
        public static string stuid;
        public static string type;
        public static string exam, ut, session;
        public static int CLSSEQ;
        public cryReportCardAnnualwise()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
           

            if (type == "1")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("Find_StudentFinalReportCrd_FINAL_scholostic_ISS ", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
                da.SelectCommand.Parameters.AddWithValue("@class", classid);
                da.SelectCommand.Parameters.AddWithValue("@EXAM", exam);
                da.SelectCommand.Parameters.AddWithValue("@UT", ut);
                sc.Open();
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "VIEW_FINAL_REPORT_CARD");
                if (exam == "3" && ut == "1")
                {
                    crptUTITermWiseReport rcaw2 = new crptUTITermWiseReport();
                    rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                    rcaw2.SetParameterValue("session", session);
                    rcaw2.SetParameterValue("exam", exam);
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
                else if (exam == "3" && ut == "2")
                {
                    crptUTIITermWiseReport rcaw2 = new crptUTIITermWiseReport();
                    rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                    rcaw2.SetParameterValue("session", session);
                    rcaw2.SetParameterValue("exam", exam);
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
                else if (exam == "3" && ut == "3")
                {
                    crptUTIIITermWiseReport rcaw2 = new crptUTIIITermWiseReport();
                    rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    rcaw2.SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                    rcaw2.SetParameterValue("session", session);
                    rcaw2.SetParameterValue("exam", exam);
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
                else if (exam == "3" && (CLSSEQ!=15||CLSSEQ!=16))
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select * from View_STUDENT_MARKS where stu_id like '" + stuid + "'  and stc_sec_id like '" + classid + "' and str_exam_id in (4) and parent1='PART 2D : CO-SCHOLISTIC' ", sc);
                    DataSet ds2 = new DataSet();
                    da1.Fill(ds2, "View_STUDENT_MARKS");
                    SqlDataAdapter da3 = new SqlDataAdapter("select * from View_STUDENT_MARKS where stu_id like '" + stuid + "'  and stc_sec_id like '" + classid + "' and str_exam_id in (4) and parent1='Discipline' ", sc);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "View_STUDENT_MARKS");
                    crptITermmain rcaw2 = new crptITermmain();
                    rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    rcaw2.Database.Tables["VIEW_FINAL_REPORT_CARD"].SetDataSource(ds1);
                    //rcaw2.Subreports[2].SetDataSource(ds3.Tables["View_STUDENT_MARKS"]);
                    //rcaw2.Subreports[1].SetDataSource(ds2.Tables["View_STUDENT_MARKS"]);
                    //rcaw2.Subreports[0].SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                    rcaw2.SetParameterValue("session", session);
                    rcaw2.SetParameterValue("exam", exam);
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
                else if (exam == "6" && (CLSSEQ != 15 && CLSSEQ != 16))
                {
                    SqlDataAdapter da1 = new SqlDataAdapter("select * from View_STUDENT_MARKS where stu_id like '" + stuid + "'  and stc_sec_id like '" + classid + "' and str_exam_id in (8) and parent1='PART 2D : CO-SCHOLISTIC' ", sc);
                 DataSet ds2 = new DataSet();
                 da1.Fill(ds2, "View_STUDENT_MARKS");
                 SqlDataAdapter da3 = new SqlDataAdapter("select * from View_STUDENT_MARKS where stu_id like '" + stuid + "'  and stc_sec_id like '" + classid + "' and str_exam_id in (8) and parent1='Discipline' ", sc);
                 DataSet ds3 = new DataSet();
                 da3.Fill(ds3, "View_STUDENT_MARKS");
                 crptIITermmain rcaw2 = new crptIITermmain();
                 rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                 rcaw2.Database.Tables["VIEW_FINAL_REPORT_CARD"].SetDataSource(ds1);
                 //rcaw2.Subreports[2].SetDataSource(ds3.Tables["View_STUDENT_MARKS"]);
                 //rcaw2.Subreports[1].SetDataSource(ds2.Tables["View_STUDENT_MARKS"]);
                 //rcaw2.Subreports[0].SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                 rcaw2.SetParameterValue("session", session);
                 rcaw2.SetParameterValue("exam", exam);
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
                else if (CLSSEQ == 15 || CLSSEQ == 16)
                  {
                      SqlDataAdapter da1 = new SqlDataAdapter("select * from View_STUDENT_MARKS where stu_id like '" + stuid + "'  and stc_sec_id like '" + classid + "' and str_exam_id in (8) and parent1='Co-Scholatic Areas' ", sc);
                      DataSet ds2 = new DataSet();
                      da1.Fill(ds2, "View_STUDENT_MARKS");
                      SqlDataAdapter da3 = new SqlDataAdapter("select * from View_STUDENT_MARKS where stu_id like '" + stuid + "'  and stc_sec_id like '" + classid + "' and str_exam_id in (8) and parent1='Discipline' ", sc);
                      DataSet ds3 = new DataSet();
                      da3.Fill(ds3, "View_STUDENT_MARKS");
                      crptannualReport rcaw2 = new crptannualReport();
                 rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                
                 rcaw2.Database.Tables["VIEW_FINAL_REPORT_CARD"].SetDataSource(ds1);
                 rcaw2.Subreports[2].SetDataSource(ds3.Tables["View_STUDENT_MARKS"]);
                 rcaw2.Subreports[1].SetDataSource(ds2.Tables["View_STUDENT_MARKS"]);
                 rcaw2.Subreports[0].SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                 rcaw2.SetParameterValue("session", session);
                 rcaw2.SetParameterValue("exam", exam);
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
                    dataclass dca = new dataclass();
                    SqlConnection sca = dc.con();
                    SqlDataAdapter daa = new SqlDataAdapter("Find_CO_SCHOLATIC", sc);
                    daa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    daa.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
                   
                    DataSet ds2 = new DataSet();
                    daa.Fill(ds2, "CO_SCHOLASTIC");
                    dataclass dcb = new dataclass();
                    SqlConnection scb = dc.con();
                    SqlDataAdapter dab = new SqlDataAdapter("Find_CO_SCHOLATICdescipline", sc);
                    dab.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dab.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
                  
                    DataSet ds3 = new DataSet();
                    dab.Fill(ds3, "CO_SCHOLASTICdecipline");
                    crptannualReportclassviii rcaw2 = new crptannualReportclassviii();
                    rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    rcaw2.Database.Tables["VIEW_FINAL_REPORT_CARD"].SetDataSource(ds1);
                    //rcaw2.Subreports[2].SetDataSource(ds3.Tables["CO_SCHOLASTICdecipline"]);
                    //rcaw2.Subreports[1].SetDataSource(ds2.Tables["CO_SCHOLASTIC"]);
                    //rcaw2.Subreports[0].SetDataSource(ds1.Tables["VIEW_FINAL_REPORT_CARD"]);
                    rcaw2.SetParameterValue("session", session);
                    rcaw2.SetParameterValue("exam", exam);
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
            }
            else
            {
                if (exam == "7")
                    exam = "6";
                    
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from View_STUDENT_MARKS where status =0  and STC_SEC_ID like '" + classid + "' and str_exam_id like '" + exam + "' ", sc);
                     sc.Open();
                DataSet ds1 = new DataSet();
                da.Fill(ds1, "View_STUDENT_MARKS");
                CRPT_STUDENT_REPORTCARD_TERMWISE rcaw2 = new CRPT_STUDENT_REPORTCARD_TERMWISE();
                rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                rcaw2.SetDataSource(ds1.Tables["View_STUDENT_MARKS"]);
                rcaw2.SetParameterValue("session", session);


                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                rcaw2.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                rcaw2.SetParameterValue("schoolcode", ds1.Tables[0].Rows[0]["SCHOOL_CODE"].ToString());
                //rcaw2.SetParameterValue("estd", ds1.Tables[0].Rows[0]["estd"].ToString());
                rcaw2.SetParameterValue("affiliation", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString()); 
                crystalReportViewer1.ReportSource = rcaw2;
                crystalReportViewer1.Refresh();
            
            }
        }
    }
}
