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
    public partial class cryRegReceipt : Form
    {
        public static string RegNo;
        public cryRegReceipt()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblstudentformregistration  where regno='" + RegNo + "'", sc);
            DataSet ds1 = new DataSet();         //where RegNo='" + RegNo + "'
            da.Fill(ds1, "tblstudentformregistration");
           // cRptAdmCard AdmCrd = new cRptAdmCard();
            cRptRegReceipt AdmCrd = new cRptRegReceipt();
            AdmCrd.SetDataSource(ds1.Tables["tblstudentformregistration"]);

            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            AdmCrd.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            AdmCrd.SetParameterValue("address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            AdmCrd.SetParameterValue("CompanyPh", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            //AdmCrd.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //AdmCrd.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
         //   AdmCrd.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            //regFrmSale.SetParameterValue("CompanyPhone", "Ph : " + ds1.Tables[0].Rows[0]["companytelno"].ToString() + "," + ds1.Tables[0].Rows[0]["companyfaxno"].ToString());


            crystalReportViewer1.ReportSource = AdmCrd;
            crystalReportViewer1.Refresh();
///////////////////////////////////////////


        }
    }
}
