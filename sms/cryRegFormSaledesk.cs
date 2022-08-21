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
    public partial class cryRegFormSaledesk : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string school;
        public static string partgroup;
        public static string Class1, Center1;
        public cryRegFormSaledesk()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_StudentReport where  class LIKE'" + Class1 + "' order by stu_id", sc);
                                                                 
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_StudentReport");
            CrptAttandance regFrmSale = new CrptAttandance();
            regFrmSale.SetDataSource(ds1.Tables["View_StudentReport"]);
            regFrmSale.SetParameterValue("Center", Class1);
            SqlCommand com = new SqlCommand("select * from COMPANY_T");
            da = new SqlDataAdapter("select * from COMPANY_T", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "COMPANY_T");
            crystalReportViewer1.ReportSource = regFrmSale;
            crystalReportViewer1.Refresh();

        }

        private void cryEmpsal_Load(object sender, EventArgs e)
        {

        }                                                                                                                                                                                                                   

    }
}
