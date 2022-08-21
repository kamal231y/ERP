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
    public partial class cryCC : Form
    {
        public static DateTime dt1, dt2;
        public static string classid;
        public static string Roll, Admno, refid, year, month;

        public cryCC()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (classid == "0")
            { classid = "%"; }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_AdmitCard3 where stu_adm_no like '" + Admno + "' ", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AdmitCard3");
            cRptCC cc = new cRptCC();
            cc.SetDataSource(ds1.Tables["View_AdmitCard3"]);
            //
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            cc.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            cc.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            cc.SetParameterValue("AFFLIATIONCODE", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
            cc.SetParameterValue("examn", "Phone No.:" + ds1.Tables[0].Rows[0]["companytelno"].ToString());
            cc.SetParameterValue("email", "Email ID:" + ds1.Tables[0].Rows[0]["EMAIL"].ToString());
            cc.SetParameterValue("refid", refid + "/" + month + "/" + year);
            crystalReportViewer1.ReportSource = cc;
            crystalReportViewer1.Refresh();

            da = new SqlDataAdapter("select * from TBLRIGHTS", sc);
            DataSet ds2 = new DataSet();
            da.Fill(ds2, "TBLRIGHTS");
            //cc.SetParameterValue("refid", refid + "/" + month + "/" + year);
            cc.SetParameterValue("refid", ds2.Tables[0].Rows[0]["username"] + "/ CC" + " /" + Admno + " /" + ds1.Tables[0].Rows[0]["SESSION"].ToString() + " /" + refid.ToString());


            //}
        }
    }
}
