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
    public partial class cryTEACHERScheduler : Form
    {
        public static string EMP_NAME;
        public cryTEACHERScheduler()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {




            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string str = "select  * from View_TEACHARScheduler where  EMP_NAME like '" + EMP_NAME + "' ";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_TEACHARScheduler");
            cRptTEACHERScheduler empreceipt = new cRptTEACHERScheduler();
            empreceipt.SetDataSource(ds1.Tables["View_TEACHARScheduler"]);

            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");




            empreceipt.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            empreceipt.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //empreceipt.SetParameterValue("Companyph", ds1.Tables[0].Rows[0]["companytelno"].ToString());
            //empreceipt.SetParameterValue("CompanyPinCode", ds1.Tables[0].Rows[0]["companyPincode"].ToString());

            

            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("View_TEACHARScheduler", sc);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.AddWithValue("@EMP_NAME", EMP_NAME);
            //sc.Open();
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1, "View_TEACHARScheduler");
            //cRptTEACHERScheduler db = new cRptTEACHERScheduler();
            //db.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //db.SetDataSource(ds1.Tables["View_TEACHARScheduler"]);
            //da = new SqlDataAdapter("select * from company_t", sc);
            //ds1 = new DataSet();
            //da.Fill(ds1, "company_t");
            //db.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
           // db.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //ds1 = new DataSet();
            //da = new SqlDataAdapter("select cast(LECTURE_FROM as varchar(5))+ ' To' +cast(LECTURE_TO as varchar(5)) from lecture_t order by lecture_id", sc);
            //da.Fill(ds1, "lecture_t");
            //db.SetParameterValue("time1", ds1.Tables[0].Rows[0][0].ToString());
            //db.SetParameterValue("time2", ds1.Tables[0].Rows[1][0].ToString());
            //db.SetParameterValue("time3", ds1.Tables[0].Rows[2][0].ToString());
            //db.SetParameterValue("time4", ds1.Tables[0].Rows[3][0].ToString());
            //db.SetParameterValue("time5", ds1.Tables[0].Rows[4][0].ToString());
            //db.SetParameterValue("time6", ds1.Tables[0].Rows[5][0].ToString());
            //db.SetParameterValue("time7", ds1.Tables[0].Rows[6][0].ToString());
            //db.SetParameterValue("time8", ds1.Tables[0].Rows[7][0].ToString());
            ////db.SetParameterValue("time9", ds1.Tables[0].Rows[8][0].ToString());
            // db.SetParameterValue("time10", ds1.Tables[0].Rows[9][0].ToString()); 
            crystalReportViewer1.ReportSource = empreceipt;
            crystalReportViewer1.Refresh();

        }
    }
}
