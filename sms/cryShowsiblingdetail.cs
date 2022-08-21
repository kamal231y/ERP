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
    public partial class cryShowsiblingdetail : Form
    {

          public static string hostel;
          public static string classname;
          public static string yaer;
          public static string noofstu;


          public cryShowsiblingdetail()
        {

            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("insert_siblingtest", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@sibno", noofstu);
           
            DataSet ds1 = new DataSet();
            da.SelectCommand.CommandTimeout = 1500;
            da.Fill(ds1, "tmp_sibling");

            cRptSiblingDetail crptdetail = new cRptSiblingDetail();
            crptdetail.SetDataSource(ds1.Tables["tmp_sibling"]);
            crptdetail.SetParameterValue("sibling", noofstu);
          
            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            crptdetail.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());

            crystalReportViewer1.ReportSource = crptdetail;
            crystalReportViewer1.Refresh();
        }
    }
}
