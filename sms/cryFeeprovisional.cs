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
    public partial class cryFeeprovisional : Form
    {
       public static DateTime dt1, dt2;
        public static string classid;
        public static string Roll, Admno;



        public cryFeeprovisional()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (classid == "0")
            { classid = "%"; }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_tc where TC_ADM_NO like '" + Admno + "' ", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "view_tc");
            CRPTprovisionalStructure cc = new CRPTprovisionalStructure();
            cc.SetDataSource(ds1.Tables["view_tc"]);
          
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            cc.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            cc.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            cc.SetParameterValue("AFFLIATIONCODE", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
            cc.SetParameterValue("examn", ds1.Tables[0].Rows[0]["companytelno"].ToString());


            crystalReportViewer1.ReportSource = cc;
            crystalReportViewer1.Refresh();

        }
    }
}
