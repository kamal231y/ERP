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
    public partial class cryEmpregpaper : Form
    {
        public static string EMP_ID;
        //public static DateTime dt1;
        //public static DateTime dt2;
        public cryEmpregpaper()
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
            cRptEmpregpaper empexp = new cRptEmpregpaper();
            empexp.SetDataSource(ds1.Tables["View_tblemployee"]);
            //empexp.SetParameterValue("dt1", dt1);
            //empexp.SetParameterValue("dt2", dt2); 

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");




            empexp.SetParameterValue("CompanyName", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            empexp.SetParameterValue("Companyaddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            empexp.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            empexp.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());

            crystalReportViewer1.ReportSource = empexp;
            crystalReportViewer1.Refresh();

        }
    }
}
