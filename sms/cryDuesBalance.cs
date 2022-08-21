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
    public partial class cryDuesBalance : Form
    {
        public static string hostel;

        public static string stu_id;
        public static string classname;
        public static string yaer;
        public static string hostelp;
        public static string hostelstring;
        public static string yaerString;
        public cryDuesBalance()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_DuesBalancePrint", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", Convert.ToInt32(yaer));
            da.SelectCommand.Parameters.AddWithValue("@class", classname);
            da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);
            //da.SelectCommand.Parameters.AddWithValue("@stu_id", stu_id);
            stu_id= "(" + stu_id.Replace("'", "") + ")";
            DataSet ds1 = new DataSet();  
            da.Fill(ds1, "StudentDues1");
            da = new SqlDataAdapter("select * from StudentDues where sr_no in " + stu_id + " order by RollNo ", sc);
            da.Fill(ds1, "StudentDues");
            cRptDuesBalance db = new cRptDuesBalance();
            db.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;

            db.SetDataSource(ds1.Tables["StudentDues"]);
            
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            db.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            db.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //da.SelectCommand.Parameters.AddWithValue("hostelp", hostelp);
            db.SetParameterValue("hostelstring", hostelstring);
            db.SetParameterValue("yaerString", yaerString);
            crystalReportViewer1.ReportSource = db;
            crystalReportViewer1.Refresh();

        }
    }
}
