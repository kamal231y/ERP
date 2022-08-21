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
    public partial class cryshowstudentAGEwise : Form
    {
          public  static DateTime dt1;
          public  static DateTime dt2;
         public static string studentname;


         public cryshowstudentAGEwise()
        {

            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("sp_total_AGEwise", sc);                
            sc.Open();
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "TMP_student_DETAIL_T");
            cRptStudentDetailAGEwise rcaw2 = new cRptStudentDetailAGEwise();
           rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
           rcaw2.SetDataSource(ds1.Tables["TMP_student_DETAIL_T"]);           

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            rcaw2.SetParameterValue("session", ds1.Tables[0].Rows[0]["session"].ToString());
            crystalReportViewer1.ReportSource = rcaw2;
            crystalReportViewer1.Refresh();
        }
    }
}
