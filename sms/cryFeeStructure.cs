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
    public partial class cryFeeStructure : Form
    {
        public static DateTime dt1, dt2;
        public static string classid;
        public static string Roll, Admno;
        public static string tuitionfee, busfee, anualfee;
        public static string examfee, extrafee, totalfee;


        public cryFeeStructure()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (classid == "0")
            { classid = "%"; }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_AdmitCard3 where stu_id like '" + Admno + "' ", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AdmitCard3");
            CRPTFeeStructure cc = new CRPTFeeStructure();
            cc.SetDataSource(ds1.Tables["View_AdmitCard3"]);
            cc.SetParameterValue("tuitionfee", tuitionfee);
            cc.SetParameterValue("busfee", busfee);
            cc.SetParameterValue("anualfee", anualfee);
            cc.SetParameterValue("examfee", examfee);
            cc.SetParameterValue("extrafee", extrafee);
            cc.SetParameterValue("totalfee", totalfee);
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            cc.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            cc.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            cc.SetParameterValue("AFFLIATIONCODE", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
           
            cc.SetParameterValue("examn", "Phone No.:" + ds1.Tables[0].Rows[0]["companytelno"].ToString());
          
            cc.SetParameterValue("email", "Email ID:" + ds1.Tables[0].Rows[0]["EMAIL"].ToString());
            cc.SetParameterValue("SESSION", "SESSION:" + ds1.Tables[0].Rows[0]["SESSION"].ToString());

            crystalReportViewer1.ReportSource = cc;
            crystalReportViewer1.Refresh();

        }
    }
}
