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
    public partial class crytotalMNTHLYREPORT : Form
    {
          public  static DateTime dt1;
          public  static DateTime dt2;
         public static string studentname;
         public static string hostel;
         public static string classname;
         public static string yaer;
         public static string month;


         public crytotalMNTHLYREPORT()
        {

            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("monthly_fee_report", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", Convert.ToInt32(yaer));
            da.SelectCommand.Parameters.AddWithValue("@class", classname);
            da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);
            da.SelectCommand.Parameters.AddWithValue("@selected", "1");
            sc.Open();
            DataSet ds1 = new DataSet();
            da.SelectCommand.CommandTimeout = 3000;
            da.Fill(ds1, "tmp_fee_monthly_report");
            cRpttotalMNTHLYREPORT rcaw2 = new cRpttotalMNTHLYREPORT();
           rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
           rcaw2.SetDataSource(ds1.Tables["tmp_fee_monthly_report"]);
           rcaw2.SetParameterValue("month", month);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            crystalReportViewer1.ReportSource = rcaw2;
            crystalReportViewer1.Refresh();
        }
    }
}
