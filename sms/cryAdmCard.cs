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
    public partial class cryAdmCard : Form
    {
        public static string RegNo;
        public cryAdmCard()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_AdmCardRpt where formnoid='" + RegNo + "'", sc);
            DataSet ds1 = new DataSet();         //where RegNo='" + RegNo + "'
            da.Fill(ds1, "view_AdmCardRpt");
           // cRptAdmCard AdmCrd = new cRptAdmCard();
            rptReg321 AdmCrd = new rptReg321();
            AdmCrd.SetDataSource(ds1.Tables["view_AdmCardRpt"]);

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            AdmCrd.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            AdmCrd.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            AdmCrd.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            AdmCrd.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
         //   AdmCrd.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            //regFrmSale.SetParameterValue("CompanyPhone", "Ph : " + ds1.Tables[0].Rows[0]["companytelno"].ToString() + "," + ds1.Tables[0].Rows[0]["companyfaxno"].ToString());


            crystalReportViewer1.ReportSource = AdmCrd;
            crystalReportViewer1.Refresh();
///////////////////////////////////////////
          
        }
    }
}
