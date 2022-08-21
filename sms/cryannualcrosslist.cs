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
    public partial class cryannualcrosslist : Form
    {
        public static string Class;
        public static string exam;
        public static string stuid;
        public static string examT;
        public cryannualcrosslist()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_AnnualCrossList", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
            da.SelectCommand.Parameters.AddWithValue("@class", Class);
          
            sc.Open();

            DataSet ds1 = new DataSet();
            da.Fill(ds1, "StudentDues");

            crptAnnualCrossList db = new crptAnnualCrossList();
            db.SetDataSource(ds1.Tables["StudentDues"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            db.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            db.SetParameterValue("CompayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //da.SelectCommand.Parameters.AddWithValue("hostelp", hostelp);
          
            crystalReportViewer1.ReportSource = db;
            crystalReportViewer1.Refresh();

        }
    }
}
