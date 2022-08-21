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
    public partial class cryDemandRegisterReport : Form
    {
        public static string hostel;
        public static string classname;
        public static string yaer;
        public static string hostelp;
        public static string MONTH;
        public static string classnameS;
        public cryDemandRegisterReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {






            //cRptDemandRegisterReport drr = new cRptDemandRegisterReport();
            //drr.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //drr.SetDataSource(ds1.Tables["view_DEMAND_T"]);

            //da = new SqlDataAdapter("select * from company_t", sc);
            //ds1 = new DataSet();
            //da.Fill(ds1, "company_t");
            //drr.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["COM_NAME"].ToString());
            //drr.SetParameterValue("Address", ds1.Tables[0].Rows[0]["companyaddress"].ToString());
            //drr.SetParameterValue("MONTH", MONTH);
            //drr.SetParameterValue("classnameS", classnameS);
            //crystalReportViewer1.ReportSource = drr;
            //crystalReportViewer1.Refresh();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + "//Recipt.html");
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<Title> Demand Register </Title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body bgcolor=\"white\">");
                sw.WriteLine("<table  style=\"border-style:ridge;border-color:silver;border-width:0px;font-family:arial;font-size:10px\" bgcolor=\"white\" width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\">");
                sw.WriteLine("<tr>");
                sw.WriteLine("<td align=\"right\" width=\"10%\"><td><td  align=\"center\" valign=\"top\"><h1 style=\"font-size:20px;\"><font face=\"Times New Romen\" >Demand Register</font> </h1></td>");
                sw.WriteLine("</tr>");
                sw.WriteLine("</table><hr>");
                sw.WriteLine("<table style=\"border-style:ridge;border-color:silver;border-width:0px;\" bgcolor=\"white\" width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\">");
                //  sw.WriteLine("<tr><td align=\"center\" width=\"100%\"><b>LIST OF PURCHASE TAX INVOICE FROM </b>" + dateTimePicker1.Text + " To " + dateTimePicker2.Text + "</td></td>");
                sw.WriteLine("</table><hr>");
                sw.WriteLine("<table style=\"border-style:ridge;border-color:silver;border-width:1px;font-family:arial;font-size:11px;\" bgcolor=\"gray\" width=\"100%\" border=\"0\" cellpadding=\"4\" cellspacing=\"0\" align=\"center\">");
                sw.WriteLine("<tr>");
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Visible == true)
                        sw.WriteLine("<td bgcolor=\"silver\" align=\"left\"><b>" + dataGridView1.Columns[i].HeaderText + "</b></td>");
                }
                //decimal TotelNetAmount = 0;

                sw.WriteLine("</tr>");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sw.WriteLine("<tr>");
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView1[j, i].Value == null)
                            dataGridView1[j, i].Value = "";
                        if (dataGridView1.Columns[j].Visible == true)
                        {
                            if (dataGridView1[j, i].Value.ToString().Contains("`"))
                            {
                                //  sw.WriteLine("<td bgcolor=\"white\" style=\"border-top:1px solid silver;\" align=\"left\" ><b>" + dataGridView3[j, i].Value.ToString().Replace("12:00:00 AM", "") + "</b></td>");
                            }
                            else
                            {
                                sw.WriteLine("<td bgcolor=\"white\" style=\"border:0px solid silver;\" align=\"left\" ><b>" + dataGridView1[j, i].Value.ToString().Replace("12:00:00 AM", "") + "</b></td>");
                            }
                        }
                    }
                    sw.WriteLine("</tr>");
                }

                sw.WriteLine("</table><hr>");
                sw.WriteLine("<div align=\"center\"><b>*** End Of Report ***</b></div>");
                sw.Close();
                PrintforBig dlg = new PrintforBig();
                dlg.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }


        private void cryDemandRegisterReport_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_DEMAND", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH", Convert.ToInt32(yaer));
            da.SelectCommand.Parameters.AddWithValue("@class", classname);
            da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);

            sc.Open();
            DataSet ds1 = new DataSet();
            da.Fill(ds1);



            string value = "EXEC demand_dynamic ";

            SqlDataAdapter da1 = new SqlDataAdapter(value, sc);
            DataTable ds = new DataTable();
            da1.Fill(ds);
            dataGridView1.DataSource = "";
            dataGridView1.DataSource = ds;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 180;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
