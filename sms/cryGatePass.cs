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
    public partial class cryGatePass : Form
    {
        public static DateTime dt1, dt2;
        public static string gatepass;
      

        public cryGatePass()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
          
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_Gatepass where id like '" + gatepass.Trim().Replace(" ","") + "' ", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "view_Gatepass");
            cRptGatepass gp = new cRptGatepass();
            gp.SetDataSource(ds1.Tables["view_Gatepass"]);
            //
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            gp.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            gp.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            gp.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            // ic.SetParameterValue("session", session);


            crystalReportViewer1.ReportSource = gp;
            crystalReportViewer1.Refresh();

        }
    }
}
