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
    public partial class ReportCardtermwiseNew : Form
    {
        public static string Class;
        public static string exam;
        public static string stuid;
        public static string examT;

        public ReportCardtermwiseNew()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_totalmarks where  STU_EXAM_ID like '" + exam + "' and STU_CLS_ID like '" + Class + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_totalmarks");

            StudentReportCardTermWiseNew srctwNew = new StudentReportCardTermWiseNew();
            srctwNew.SetDataSource(ds1.Tables["View_totalmarks"]);
            srctwNew.SetParameterValue("examT", examT);

            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            srctwNew.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            srctwNew.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = srctwNew;
            crystalReportViewer1.Refresh();
        }

        
    }
}
