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
    public partial class cryvoucherbank : Form
    {
        public static string vtype;
        public static string vno;
        public static string onparticular;

        public cryvoucherbank()
        {
            InitializeComponent();
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from VIEW_JOURNAL where vtype ='" + vtype + "' and vno='" + vno + "' and ledgerid!=1", sc);
                                                                 
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "VIEW_JOURNAL");

            cRptvoucher Daybook = new cRptvoucher();
            Daybook.SetDataSource(ds1.Tables["VIEW_JOURNAL"]);          

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            Daybook.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            Daybook.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            Daybook.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            Daybook.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            Daybook.SetParameterValue("ledgerp", onparticular);
            crystalReportViewer1.ReportSource = Daybook;
            crystalReportViewer1.Refresh();
        }
    }
}
