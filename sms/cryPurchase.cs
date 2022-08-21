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
    public partial class cryPurchase : Form
    {
        //public string dt1;
        public static string GroupNo;
        public static DateTime dt1;
        public static DateTime dt2;

        public cryPurchase()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //string str = "select  * from View_Sale where  sale_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STU_CLS_ID like '" + classid + "' and stu_hostler like '" + hostel + "' order by FRC_no";
            string str = "select  * from View_Purchase where cast(PUR_DATE as date)  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "' and  PUR_VCR_ID like '" + GroupNo + "' order by PUR_NO ";

            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_Purchase");

           
            cRptPurchase crpur = new cRptPurchase();
            crpur.SetDataSource(ds1.Tables["View_Purchase"]);
            //cris.SetParameterValue("dt1", dt1);
                //cris.SetParameterValue("dt2", dt2);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crpur.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            crpur.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());

            crystalReportViewer1.ReportSource = crpur;
            crystalReportViewer1.Refresh();

        }
    }
}
