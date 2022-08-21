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
    public partial class crystatement : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string ledgername;
       



        public crystatement()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from templedger ", sc);
                                                                 
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "templedger");

            cRptstatement statement = new cRptstatement();
            statement.SetDataSource(ds1.Tables["templedger"]);
            statement.SetParameterValue("dt1", dt1);
            statement.SetParameterValue("dt2", dt2);
           // statement.SetParameterValue("ledgername",ledgername);
        
         


            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            //Daybook.SetParameterValue("PageHeader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            statement.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            statement.SetParameterValue("CompanyAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            statement.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            statement.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());
            //regFrmSale.SetParameterValue("CompanyPhone", "Ph : " + ds1.Tables[0].Rows[0]["companytelno"].ToString() + "," + ds1.Tables[0].Rows[0]["companyfaxno"].ToString());


            crystalReportViewer1.ReportSource =statement;
            crystalReportViewer1.Refresh();


        
        }

       

       
    }
}
