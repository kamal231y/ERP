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
    public partial class cryReceiptDetailReporttotal : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string hostel;
        public static string roll;
        public static string fee;
        public cryReceiptDetailReporttotal()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_totalfee", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@dateto", dt1);
            da.SelectCommand.Parameters.AddWithValue("@date", dt2);
            da.SelectCommand.Parameters.AddWithValue("@fee", fee); 
            DataSet ds1 = new DataSet();
            da.SelectCommand.CommandTimeout = 1500;
            da.Fill(ds1,"tbl_totalfee");
            cRptReceiptDetailReporttotal ReceiptDetailRep = new cRptReceiptDetailReporttotal();
            
            ReceiptDetailRep.SetDataSource(ds1.Tables["tbl_totalfee"]);
            ReceiptDetailRep.SetParameterValue("dt1", dt1.ToString("MM/dd/yyyy"));
            ReceiptDetailRep.SetParameterValue("dt2", dt2.ToString("MM/dd/yyyy"));

            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1,"COMPANY_T");
            ReceiptDetailRep.SetParameterValue("pageheader",ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            ReceiptDetailRep.SetParameterValue("address",ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = ReceiptDetailRep;
            crystalReportViewer1.Refresh();
        }
    }
}
