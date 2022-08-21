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
    public partial class cryExpense: Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string opening;
        public static string pay;
        public static string total;
        public static string close;




        public cryExpense()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tempdaybook where  description2 is not null and debit!=0.00", sc);
                                                                 
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "tempdaybook");

            cRptDaybookexpense Daybook = new cRptDaybookexpense();
            Daybook.SetDataSource(ds1.Tables["tempdaybook"]);
            Daybook.SetParameterValue("dt1", dt1);
            Daybook.SetParameterValue("dt2", dt2);
            Daybook.SetParameterValue("opening", opening);
            Daybook.SetParameterValue("pay", pay);
            Daybook.SetParameterValue("total", total);
            Daybook.SetParameterValue("close", close);
           
         


            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            //Daybook.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            Daybook.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            Daybook.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            Daybook.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            Daybook.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            //regFrmSale.SetParameterValue("CompanyPhone", "Ph : " + ds1.Tables[0].Rows[0]["companytelno"].ToString() + "," + ds1.Tables[0].Rows[0]["companyfaxno"].ToString());


            crystalReportViewer1.ReportSource = Daybook;
            crystalReportViewer1.Refresh();


        
        }

       
    }
}
