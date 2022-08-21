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
    public partial class cryFeeDefaulter : Form
    {
        public static string hostel;
        public static string classname;
        public static string yaer;
        public static string month;
        public static string stu_id;
        public static DateTime dt;
        public cryFeeDefaulter()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;
            DataSet ds1;
            da = new SqlDataAdapter("Find_defaulterprint", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@date", dt.ToString("yyyy/MM/dd"));
            da.SelectCommand.Parameters.AddWithValue("@class", classname);
            da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);
            ds1 = new DataSet();
            da.Fill(ds1);
            stu_id = "(" + stu_id.Replace("'", "") + ")";
            CrPTReminder fr = new CrPTReminder();
            da = new SqlDataAdapter("select * from View_FeeRemainder where stu_id in " + stu_id + " order by stu_roll ", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "View_FeeRemainder");
            fr.SetDataSource(ds1.Tables["View_FeeRemainder"]);
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            fr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            fr.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            fr.SetParameterValue("month", month);
            crystalReportViewer1.ReportSource = fr;
            crystalReportViewer1.Refresh();

        }
    }
}
