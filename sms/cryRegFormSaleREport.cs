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
    public partial class cryRegFormSaleREport : Form
    {
        //public static DateTime dt1;
        //public static DateTime dt2;
        public static string Sname;
        public static string FNo;
        public static string word;
        public cryRegFormSaleREport()
        {
            InitializeComponent();
        }

       
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_PROSPECTUS where name='" + Sname + "' and FormNoid='" + FNo + "'", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_PROSPECTUS");
            cRPTRegFromSaleReport regFrmSale = new cRPTRegFromSaleReport();
            regFrmSale.SetDataSource(ds1.Tables["View_PROSPECTUS"]);
            regFrmSale.SetParameterValue("word", word);
            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            da.Fill(ds1, "tblCompanyParameters");
            regFrmSale.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regFrmSale.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regFrmSale.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            regFrmSale.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            regFrmSale.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
          


            crystalReportViewer1.ReportSource = regFrmSale;
            crystalReportViewer1.Refresh();



        }                                                                                                                                                                                                                   
    }
}
