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
       public partial class reportfeereceiptreport : Form
    {
        public static string classid;
        public static string secid;
        public static string value;
        public static string month;
        public static string paid;

        public reportfeereceiptreport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
              dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_feereceiptreport where " + month + " is " + value + " and  class like '" + classid  + "'and section like '" + secid + "' order by STU_ID";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "view_tblbookingdetails");

            CrystalReportfeerecreport crptdetail = new CrystalReportfeerecreport();
            crptdetail.SetDataSource(ds1.Tables["view_tblbookingdetails"]);
            

            da = new SqlDataAdapter("select * from tblCompanyParameters", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "tblCompanyParameters");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["companyname"].ToString());
            crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crptdetail.SetParameterValue("paid", paid);
            crptdetail.SetParameterValue("month",month);

           
            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();

        }
    }
}
