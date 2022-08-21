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
    public partial class cryEmpReceipt1 : Form
    {
        public static string EMP_ID;
        public cryEmpReceipt1()
        {
            InitializeComponent();
        }

      
        private void cryEmpReceipt_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_tblemployee where  EMP_id like '" + EMP_ID+ "' ";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_tblemployee");
            cRptEmpReceipt1 empreceipt = new cRptEmpReceipt1();
            empreceipt.SetDataSource(ds1.Tables["View_tblemployee"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");




            empreceipt.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            empreceipt.SetParameterValue("Companyaddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            empreceipt.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            empreceipt.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());

            crystalReportViewer1.ReportSource = empreceipt;
            crystalReportViewer1.Refresh();

        }
    }
}
