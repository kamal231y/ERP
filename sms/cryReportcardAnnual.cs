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
    public partial class cryReportcardAnnual : Form
    {
        public static string Class;
        public static string exam;
        public static string stuid;
        public static string examT;

        public cryReportcardAnnual()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_FA1 where  STU_ID like '" + stuid + "' and STR_CLS_ID like '" + Class + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_FA1");


         

          
          
            SqlDataAdapter da1 = new SqlDataAdapter("select * from View_FA2 where  STU_ID like '" + stuid + "' and STR_CLS_ID like '" + Class + "'", sc);
            da.Fill(ds1, "View_FA2");

            cRptReportCardAnnual rca = new cRptReportCardAnnual();
            rca.SetDataSource(ds1.Tables["View_FA1"]);
            ////cRptReportCardAnnual rca = new cRptReportCardAnnual();
            //rca.SetDataSource(ds1.Tables["View_FA2"]);
            // rca.SetParameterValue("examT", examT);

            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            rca.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            rca.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = rca;
            crystalReportViewer1.Refresh();
        

        }
    }
}
