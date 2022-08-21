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
    public partial class Reportfeedetail1 : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string secid;
      
        public static string stutype1;
        public static string stutype2;
        public Reportfeedetail1()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_showstudentfeeledger where FRC_DATE between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "'and class like '" + classid + "'and section like '" + secid + "' and stu_type1 like '" + stutype1 + "' and stu_type2 like '" + stutype2 + "'order by FRC_no";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "view_tblbookingdetails");

            CrystalReportfeedetail1 crptdetail = new CrystalReportfeedetail1();
            crptdetail.SetDataSource(ds1.Tables["view_tblbookingdetails"]);
            crptdetail.SetParameterValue("dt1", dt1);
            crptdetail.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from tblCompanyParameters", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "tblCompanyParameters");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["companyname"].ToString());
            crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            da = new SqlDataAdapter("select* from View_showfeedetailreport where FRC_DATE between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "'", sc);
            da.Fill(ds1,"feedetail");
            crptdetail.SetParameterValue("feedesc", ds1.Tables["feedetail"].Rows[0]["feeDescription"].ToString());
            crptdetail.SetParameterValue("Amount", ds1.Tables["feedetail"].Rows[0]["Amount"].ToString());




            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();
        }
    }
}
