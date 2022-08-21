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
    public partial class cryRegFormSale : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string usertype;
        public cryRegFormSale()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter( "select id,(CAST(Formnoid as varchar)+'/'+cast(YEAR(Bdate)as varchar)) as FormNo,Name,Bdate,Amount,Userid  from tblFormRegistration where Bdate>='" + dt1 + "' and Bdate<='" + dt2 + "'", sc);
            SqlDataAdapter da = new SqlDataAdapter("select * from tblFormRegistration where Bdate>='" + dt1.ToString("MM/dd/yyyy") + "' and Bdate<='" + dt2.ToString("MM/dd/yyyy") + "' order by id", sc);
                                                                 // and userid='"+usertype+"'
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "tblFormRegistration");
            cRptRegFrmSale regFrmSale = new cRptRegFrmSale();
            regFrmSale.SetDataSource(ds1.Tables["tblFormRegistration"]);
            regFrmSale.SetParameterValue("dt1",dt1);
            regFrmSale.SetParameterValue("dt2", dt2);
          //  regFrmSale.SetParameterValue("userid",usertype);


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
