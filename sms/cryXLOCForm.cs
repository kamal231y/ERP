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
    public partial class cryXLOCForm : Form
    {
        public static string classid,sectionname;
        public cryXLOCForm()
        {
           
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
           
            if (classid == "11")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from View_PrintRegForm9 where SEC_DESC='"+sectionname+"' and CLS_ID in (11) order by sec_desc , STC_ROLL_NO", sc);
                DataSet ds1 = new DataSet();       //  select * from View_printLoc where STU_CLS_ID in (12,14)  order by sec_desc , STC_ROLL_NO
                da.Fill(ds1, "View_printLoc");
                cRptXLOCform xloc = new cRptXLOCform();
                xloc.SetDataSource(ds1.Tables["View_printLoc"]);
                
                SqlCommand com = new SqlCommand("select * from Company_T");
                da = new SqlDataAdapter("select * from Company_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "Company_T");
                xloc.SetParameterValue("SchoolName", ds1.Tables[0].Rows[0]["com_name"].ToString() + " , " + ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = xloc;
            }
            else if (classid == "13")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from View_PrintRegForm9 where SEC_DESC='" + sectionname + "' and CLS_ID in (13) order by sec_desc , STC_ROLL_NO", sc);
                DataSet ds1 = new DataSet();       //  select * from View_printLoc where STU_CLS_ID in (12,14)  order by sec_desc , STC_ROLL_NO
                da.Fill(ds1, "View_printLoc");
                cRptXILOCform xiloc = new cRptXILOCform();
                xiloc.SetDataSource(ds1.Tables["View_printLoc"]);

                SqlCommand com = new SqlCommand("select * from Company_T");
                da = new SqlDataAdapter("select * from Company_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "Company_T");
                xiloc.SetParameterValue("SchoolName", ds1.Tables[0].Rows[0]["com_name"].ToString() + " , " + ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                crystalReportViewer1.ReportSource = xiloc;
            }
            
            crystalReportViewer1.Refresh();
        }
    }
}
