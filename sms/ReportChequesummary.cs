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
    public partial class ReportChequesummary : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string hostel;

        public ReportChequesummary()
        {
          InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_chequebounce where  CBdate  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'order by id";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_chequebounce");

            reportchequebounce crptdetail = new reportchequebounce();
            crptdetail.SetDataSource(ds1.Tables["View_chequebounce"]);
            crptdetail.SetParameterValue("DT1", dt1);
            crptdetail.SetParameterValue("DT2", dt2);

            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "tblCompanyParameters");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            crptdetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());           
            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();
        }
    }
}
