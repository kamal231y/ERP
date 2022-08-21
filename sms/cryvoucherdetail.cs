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
    public partial class cryvoucherdetail : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string usertype;
        public static string vtype;
        public cryvoucherdetail()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from viewVoucherDetail where  vtype='" + vtype + "' and vdate between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' AND LEDGERID!=1", sc);
                                                                 
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "viewVoucherDetail");
            cRptvoucherdetail regFrmSale = new cRptvoucherdetail();
            regFrmSale.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            regFrmSale.SetDataSource(ds1.Tables["viewVoucherDetail"]);
            regFrmSale.SetParameterValue("dt1",dt1);
            regFrmSale.SetParameterValue("dt2", dt2);
            regFrmSale.SetParameterValue("user",usertype);


            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            regFrmSale.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regFrmSale.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            regFrmSale.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            regFrmSale.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            regFrmSale.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            //regFrmSale.SetParameterValue("CompanyPhone", "Ph : " + ds1.Tables[0].Rows[0]["companytelno"].ToString() + "," + ds1.Tables[0].Rows[0]["companyfaxno"].ToString());
            

            crystalReportViewer1.ReportSource = regFrmSale;
            crystalReportViewer1.Refresh();


        
        }

        private void cryEmpsal_Load(object sender, EventArgs e)
        {

        }                                                                                                                                                                                                                   

    }
}
