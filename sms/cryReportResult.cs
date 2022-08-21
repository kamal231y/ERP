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
using System.IO;


namespace sms
{
    public partial class cryReportResult : Form
    {
        public static string Class;
        public static string exam;
        public static string stuid;
        public static string examT;
        public static string radio;
        //public static string prmName;
        //public static string parameterField;
        public cryReportResult()
        {
            InitializeComponent();
        }
        //=====================           
              // public void SetLogo(CrystalDecisions.CrystalReports.Engine.ReportDocument db, System.Drawing.Image img,  string prmName)
              // {
              //      CrystalDecisions.Shared.ParameterField parameterField;
              //      parameterField = db.ParameterFields(prmName);
              //      string f = @"D:\2014virender\bkp\new_schools\stJoshef\sms\sms\StudentImage\Noimage.gif";
              //      if (System.IO.File.Exists(f))
              //      System.IO.File.Delete(f);
              //      parameterField.CurrentValues.AddValue(SetReportLogo(img, f));
              //}
              //  public string SetReportLogo(Image img, string f)
              //  {
              //      if (!System.IO.File.Exists(f))
              //      {
              //          Bitmap oBitmap;
              //          oBitmap = new Bitmap(img);
              //          oBitmap.Save(f, System.Drawing.Imaging.ImageFormat.Jpeg);
              //          oBitmap.Dispose();
              //      }
              //  return f;
              //  }
            //=====================  

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
                     
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_StudentReportCardExamwise", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Stu_id", stuid);
            da.SelectCommand.Parameters.AddWithValue("@class", Class);
            da.SelectCommand.Parameters.AddWithValue("@exam", exam);
            sc.Open();

            DataSet ds1 = new DataSet();
            da.Fill(ds1, "StudentDues");
            if (radio == "0")
            {
                crptStudentResultReport db = new crptStudentResultReport();
                db.SetDataSource(ds1.Tables["StudentDues"]);
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                db.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                db.SetParameterValue("CompayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                //da.SelectCommand.Parameters.AddWithValue("hostelp", hostelp);
                db.SetParameterValue("ExamT", examT);
               // db.SetParameterValue("prmName", parameterField);
                crystalReportViewer1.ReportSource = db;
                crystalReportViewer1.Refresh();
            }
            else
            {
                crptStudentResultReportDetail db = new crptStudentResultReportDetail();
                db.SetDataSource(ds1.Tables["StudentDues"]);
                da = new SqlDataAdapter("select * from company_t", sc);
                ds1 = new DataSet();
                da.Fill(ds1, "company_t");
                db.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
                db.SetParameterValue("CompayAddress", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
                //da.SelectCommand.Parameters.AddWithValue("hostelp", hostelp);
                db.SetParameterValue("ExamT", examT);
               // db.SetParameterValue("prmName", parameterField);
                crystalReportViewer1.ReportSource = db;
                crystalReportViewer1.Refresh();
            }
         }
        }     
    }

