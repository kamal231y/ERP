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
    public partial class cryDetailSale : Form
    {
        //public string dt1;
        public static string GroupNo;
        public static DateTime dt1;
        public static DateTime dt2;
        public static string ss;
        public static string roll;
        public static string itmId;



        public cryDetailSale()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //string str = "select  * from View_Sale where  sale_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STU_CLS_ID like '" + classid + "' and stu_hostler like '" + hostel + "' order by FRC_no";
            string str = "select  * from View_Sale where cast(SALE_DATE as date)  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "' and  CLS_ID like '" + GroupNo + "' and STu_id like '" + roll + "' and saleStatus like '" + ss + "' and sale_item_id like '" + itmId + "' order by SALE_NO ";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_Sale"); 

            cRptSaleDetail crpurDetail = new cRptSaleDetail();
            crpurDetail.SetDataSource(ds1.Tables["View_Sale"]);
            if (ss == "1")
            {
                crpurDetail.SetParameterValue("ss", "Cancelled Sale Report");
            }
            else
            {
                crpurDetail.SetParameterValue("ss", "Sale Report");
            }

            //cris.SetParameterValue("dt1", dt1);
            //cris.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crpurDetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            crpurDetail.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = crpurDetail;
            crystalReportViewer1.Refresh();

        }
    }
}
