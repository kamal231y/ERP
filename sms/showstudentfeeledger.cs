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

namespace sms
{
    public partial class showstudentfeeledger : Form
    {
        public static int stu_id;
        public static int rec_no;
        public static string  balance;
        public static string Fee;
        public static string total;
        public static string Prev;
        public static decimal concession;
        public static string Paid;
        public static string comment;
        public static string monthname;
        public static int mode;
        public static string cheqno;
        public static int modename;
        DataSet ds1;
        public showstudentfeeledger()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_showstudentfeeledger where  FRC_NO=" + rec_no + " ", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "View_showstudentfeeledger");

             da = new SqlDataAdapter("select * from setting_t ", sc);
            DataTable setting_t = new DataTable();
             da.Fill(setting_t);
             if (setting_t.Rows[0][0].ToString() != "True")
            {
                CrystalReportshowfeeledgerSingle crptdetail = new CrystalReportshowfeeledgerSingle();
                //crptdetail.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.portrait;
                
                 //crptdetail.PrintOptions
                 crptdetail.SetDataSource(ds1.Tables["View_showstudentfeeledger"]);
                crptdetail.SetParameterValue("balance", balance);
                crptdetail.SetParameterValue("Fee", Fee);
                crptdetail.SetParameterValue("total", total);
                crptdetail.SetParameterValue("Prevdue", Prev);
                crptdetail.SetParameterValue("Concession", concession);
                crptdetail.SetParameterValue("paid", Paid);
                crptdetail.SetParameterValue("Comment", comment);
                if (modename == 2)
                {
                    crptdetail.SetParameterValue("cheqno", cheqno);
                }
                else
                {
                    crptdetail.SetParameterValue("cheqno", " ");

                }
                crptdetail.SetParameterValue("Month", monthname);
                da = new SqlDataAdapter("select * from COMPANY_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "tblCompanyParameters");
                crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //    crptdetail.SetParameterValue("address1", ds1.Tables[0].Rows[0]["companypincode"].ToString());
                crptdetail.SetParameterValue("mob", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                crystalReportViewer1.ReportSource = crptdetail;
                crystalReportViewer1.Refresh();
                 if(mode==1)
             
                crptdetail.PrintToPrinter(1, false, 1, 1);
            }
            //else
            //{
            //    if (concession != 0)
            //    {
            //        CrystalReportshowfeeledgerSingleconcession crptdetail2 = new CrystalReportshowfeeledgerSingleconcession();
            //       // crptdetail2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //        crptdetail2.SetDataSource(ds1.Tables["View_showstudentfeeledger"]);
            //        crptdetail2.SetParameterValue("balance", balance);
            //        crptdetail2.SetParameterValue("Fee", Fee);
            //        crptdetail2.SetParameterValue("total", total);
            //        crptdetail2.SetParameterValue("Prevdue", Prev);
            //        crptdetail2.SetParameterValue("Concession", concession);
            //        crptdetail2.SetParameterValue("paid", Paid);
            //        crptdetail2.SetParameterValue("Comment", comment);
            //        if (modename == 2)
            //        {
            //            crptdetail2.SetParameterValue("cheqno", cheqno);
            //        }
            //        else
            //        {
            //            crptdetail2.SetParameterValue("cheqno", " ");

            //        }
            //        crptdetail2.SetParameterValue("Month", monthname);
            //        da = new SqlDataAdapter("select * from COMPANY_T", sc);
            //        ds1 = new DataSet();
            //        da.Fill(ds1, "tblCompanyParameters");
            //        crptdetail2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            //        crptdetail2.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //        crptdetail2.SetParameterValue("mob", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            //        crystalReportViewer1.ReportSource = crptdetail2;
            //        crystalReportViewer1.Refresh();
            //        if (mode == 1)
            //            crptdetail2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //            crptdetail2.PrintToPrinter(1, false, 1, 1);
            //    }
                else
                {

                    CrystalReportshowfeeledger crptdetail1 = new CrystalReportshowfeeledger();
                   // crptdetail1.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.portrait;
                    crptdetail1.SetDataSource(ds1.Tables["View_showstudentfeeledger"]);
                    crptdetail1.SetParameterValue("balance", balance);
                    crptdetail1.SetParameterValue("Fee", Fee);
                    crptdetail1.SetParameterValue("total", total);
                    crptdetail1.SetParameterValue("Prevdue", Prev);
                    crptdetail1.SetParameterValue("Concession", concession);

                    crptdetail1.SetParameterValue("paid", Paid);
                    crptdetail1.SetParameterValue("Comment", comment);
                    if (modename == 2)
                    {
                        crptdetail1.SetParameterValue("cheqno", cheqno);
                    }
                    else
                    {
                        crptdetail1.SetParameterValue("cheqno", " ");

                    }
                    crptdetail1.SetParameterValue("Month", monthname);
                    da = new SqlDataAdapter("select * from COMPANY_T", sc);
                    ds1 = new DataSet();
                    da.Fill(ds1, "tblCompanyParameters");
                    crptdetail1.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                    crptdetail1.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                    //crptdetail1.SetParameterValue("address1", ds1.Tables[0].Rows[0]["companypincode"].ToString());
                    crptdetail1.SetParameterValue("mob", ds1.Tables[0].Rows[0]["companytelno"].ToString());
                    crystalReportViewer1.ReportSource = crptdetail1;
                    crystalReportViewer1.Refresh();
                    if (mode == 1)
                        crptdetail1.PrintToPrinter(1, false, 1, 1);
                }
            //}
            }
        }
       
    }

