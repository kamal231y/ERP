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
    public partial class cryStockdetail : Form
    {
        public static string classid;
        public static string pubId;
         
        public static DateTime dt1;
        public static DateTime dt2;

        public cryStockdetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("spstockdetail ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@min1", dt1.ToString("MM/dd/yyyy"));
            da.SelectCommand.Parameters.AddWithValue("@max1", dt2.ToString("MM/dd/yyyy"));
           
            DataSet ds1 = new DataSet();
            da.SelectCommand.CommandTimeout = 1500;
            da.Fill(ds1, "tempstock");
            cRptStockdetail rcaw2 = new cRptStockdetail();
            rcaw2.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            
            rcaw2.SetDataSource(ds1.Tables["tempstock"]);
            rcaw2.SetParameterValue("dt1", dt1);
            rcaw2.SetParameterValue("dt2", dt2);
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            rcaw2.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_vender"].ToString());
            rcaw2.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            crystalReportViewer1.ReportSource = rcaw2;
            crystalReportViewer1.Refresh();
        



        }
    }
}
