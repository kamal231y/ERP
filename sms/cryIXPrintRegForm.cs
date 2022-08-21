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
    public partial class cryIXPrintRegForm : Form
    {
        public static string classid;
        public cryIXPrintRegForm()
        {
           
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_PrintRegForm9 where CLS_ID='" + classid + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_PrintRegForm9");
            if (classid == "14")
            {
                cRptIXPRegForm ixPRegF = new cRptIXPRegForm();
                ixPRegF.SetDataSource(ds1.Tables["View_PrintRegForm9"]);

                SqlCommand com = new SqlCommand("select * from Company_T");
                da = new SqlDataAdapter("select * from Company_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "Company_T");
                ixPRegF.SetParameterValue("SchoolName", ds1.Tables[0].Rows[0]["com_name"].ToString());
                crystalReportViewer1.ReportSource = ixPRegF;
            }
            else
            {
                cRptXiRegForm ixPRegF = new cRptXiRegForm();
                ixPRegF.SetDataSource(ds1.Tables["View_PrintRegForm9"]);

                SqlCommand com = new SqlCommand("select * from Company_T");
                da = new SqlDataAdapter("select * from Company_T", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "Company_T");
                ixPRegF.SetParameterValue("SchoolName", ds1.Tables[0].Rows[0]["com_name"].ToString() );
                 crystalReportViewer1.ReportSource = ixPRegF;
            }


            
            crystalReportViewer1.Refresh();
        }
    }
}
