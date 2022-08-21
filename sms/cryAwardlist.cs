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
    public partial class cryAwardlist : Form
    {
        public static string classid;
        public static string className;
        public static string CATEGORYID;
        public static string CATEGORY; 
        public static string EXAMID;
        public static string ExamName;
        public static string marks1, marks2, marks3, marks4, marks5, marks6;
        public cryAwardlist()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("GREEN_SHEET_SUBJECT_WISE_PRINT", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(classid));
            da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(EXAMID));
            da.SelectCommand.Parameters.AddWithValue("@userid", sms.Form1.GlobalLoginUserName.ToString());
            da.SelectCommand.CommandTimeout = 1500;
            sc.Open();
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AwardList");
            crptstudentreporMarkst rcaw2 = new crptstudentreporMarkst();
                rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                rcaw2.SetDataSource(ds1.Tables["View_AwardList"]);
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
                rcaw2.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
                rcaw2.SetParameterValue("ClassName", className);
                rcaw2.SetParameterValue("TestName", ExamName);
                rcaw2.SetParameterValue("marks1", marks1);
                rcaw2.SetParameterValue("marks2", marks2);
                rcaw2.SetParameterValue("marks3", marks3);
                rcaw2.SetParameterValue("marks4", marks4);
                rcaw2.SetParameterValue("marks5", marks5);
                rcaw2.SetParameterValue("marks6", marks6);
                crystalReportViewer1.ReportSource = rcaw2;
                crystalReportViewer1.Refresh();
            }
           
        }
    }

