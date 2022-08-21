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
    public partial class cryConfirmAdmission : Form
    {
        public static string test;
        public cryConfirmAdmission()
            {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id,(CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNoid,StudentName,resultstatus,mobileno,Testexamid,RDate,TestName  from View_AddmissionTestResult where testexamid='" + test + "' and resultstatus=1", sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_AddmissionTestResult");
            cRptConfirmAdmission ConfirmAdmission = new cRptConfirmAdmission();
            ConfirmAdmission.SetDataSource(ds1.Tables["View_AddmissionTestResult"]);
           
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            ConfirmAdmission.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            ConfirmAdmission.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //da.SelectCommand.Parameters.AddWithValue("hostelp", hostelp);
            //db.SetParameterValue("hostelstring", hostelstring);
            //db.SetParameterValue("yaerString", yaerString);
            crystalReportViewer1.ReportSource = ConfirmAdmission;
            crystalReportViewer1.Refresh();

        }
    }
}
