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
    public partial class cryStockPublisherWise : Form
    {
        public static string classid;
        public static string pubId; 
        public static DateTime dt1;
        public static DateTime dt2;
       
        public cryStockPublisherWise()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_Stock where itm_id like '" + classid + "' and pub_id like '" + pubId + "'  ";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_Stock");

            cRptStockPublisherWise stkPubWis = new cRptStockPublisherWise();
            stkPubWis.SetDataSource(ds1.Tables["View_Stock"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            stkPubWis.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            stkPubWis.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            crystalReportViewer1.ReportSource = stkPubWis;
            crystalReportViewer1.Refresh();
        



        }
    }
}
