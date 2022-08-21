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
using System.Data;



namespace sms
{
    public partial class cryDailyScrollReport : Form
    {
        public static DateTime dt1;
        public static DateTime dt2;
        public static string classid;
        public static string hostel;
        dataclass dc = new dataclass();
        public cryDailyScrollReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            

        }

        private void cryDailyScrollReport_Load(object sender, EventArgs e)
        {
            //if (classid == "%")
            //{

            //    dataclass dc = new dataclass();
            //    SqlConnection sc = dc.con();

            //    string value = "EXEC dailyscroll_dynamic '" + dt1 + "','" + dt2 + "','ALL','" + hostel + "'";

            //    SqlDataAdapter da = new SqlDataAdapter(value, sc);
            //    DataTable ds = new DataTable();
            //    da.Fill(ds);
            //    dataGridView1.DataSource = "";
            //    dataGridView1.DataSource = ds;
            //    for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //    {
            //        dataGridView1.Columns[i].Width = 180;
            //    }
            //    //dataGridView1.Columns["Day"].Visible = false;
            //    //dataGridView1.Columns["Month"].Visible = false;
            //    //dataGridView1.Columns["Year"].Visible = false;
            //    //label2.Text = "Student";
            //}

            //else {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();

                string value = "EXEC dailyscroll_dynamic '" + dt1.ToString("yyyy-MM-dd") + "','" + dt2.ToString("yyyy-MM-dd") +"','" + classid + "','" + hostel + "'";

                SqlDataAdapter da = new SqlDataAdapter(value, sc);
                DataTable ds = new DataTable();
                da.Fill(ds);
                dataGridView1.DataSource = "";
                dataGridView1.DataSource = ds;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Width = 180;
                }
                //dataGridView1.Columns["Day"].Visible = false;
                //dataGridView1.Columns["Month"].Visible = false;
                //dataGridView1.Columns["Year"].Visible = false;
                //label2.Text = "Student";
            
          //  }
        
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + "//Recipt.html");
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<Title> Daily Scroll </Title>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body bgcolor=\"white\">");
                sw.WriteLine("<table  style=\"border-style:ridge;border-color:silver;border-width:0px;font-family:arial;font-size:10px\" bgcolor=\"white\" width=\"600\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\">");
                sw.WriteLine("<tr>");
                sw.WriteLine("<td align=\"right\" width=\"10%\"><td><td  align=\"center\" valign=\"top\"><h1 style=\"font-size:20px;\"><font face=\"Times New Romen\" >DAILY SCROLL</font> </h1></td>");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            string str = " EXEC dailyscroll_dynamic '" + dt1.ToString("yyyy-MM-dd") + "','" + dt2.ToString("yyyy-MM-dd") + "','" + classid + "','" + hostel + "'";
            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataSet dt = new DataSet();
                da.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                    dc.ExportDataSetToExcel(dt);
                }
                else
                {
                    dataGridView1.DataSource = DBNull.Value;
                }
            }

            sc.Close();
        }




        


      

       
    }
}
