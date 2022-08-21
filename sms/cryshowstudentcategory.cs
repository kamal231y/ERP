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
    public partial class cryshowstudentcategory : Form
    {
          public  static DateTime dt1;
          public  static DateTime dt2;

     public static     string studentname;


     public cryshowstudentcategory()
        {

            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("sp_cat_secwise", sc);                
            sc.Open();
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "TMP_student_DETAIL_T");
            cRptStudentDetailcategorysec rcaw2 = new cRptStudentDetailcategorysec();
           rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
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
