using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.DAL;
using sms.BLClass;
using System.Linq;

namespace sms
{
    public partial class frmDailyDynamic : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmDailyDynamic()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;

            MsgBoxRetType.backColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonBackColour = Color.Red;
            MsgBoxRetType.buttonForeColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonStyle = FlatStyle.Flat;
            MsgBoxRetType.titleBackColour = Color.Red;
            MsgBoxRetType.titleForeColour = Color.WhiteSmoke;
            MsgBoxRetType.messageBoxStyle = FlatStyle.Flat;
            MsgBoxRetType.foreColour = Color.Red;
            InitializeComponent();
            //bindstate();
            Cancil_form();

        }
        private void Cancil_form()
        {
            InitGrid();

        }

        public void InitGrid()
        {

            LoadGrid();

        }


        private void LoadGrid()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter sda = new SqlDataAdapter("Select distinct FEE_ALIAS+' '+mop from view_dailydynamic where FEE_ALIAS !='' order by FEE_ALIAS+' '+mop", sc);
            DataTable dat = new DataTable();
            sda.Fill(dat);
            string[] values1 = new string[dat.Rows.Count];
            for (int i = 0; i < dat.Rows.Count; i++)
            {
                values1[i] = dat.Rows[i][0].ToString().ToUpper();
            }
            string value = "[";
            value += values1.Aggregate((a, b) => a + "],[" + b);
            value += "],[CONCESSION],[PREV. DUES],[LATE FINE]";

            sc.Open();
            SqlCommand com = new SqlCommand("sp_dailydynamic", sc);
            com.ExecuteNonQuery();
            sc.Close();
            value = "select date As Year," + value + " from (select date,feetype,amount as date1 from tempyear ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv order by cast(date as int);";
            SqlDataAdapter da = new SqlDataAdapter(value, sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 180;
            }
            label2.Text = "Year";
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                //txtcity.Text = dataGridView1.CurrentRow.Cells["CITY_NAME"].Value.ToString();
                ////cmbState.Text = dataGridView1.CurrentRow.Cells["StateName"].Value.ToString();
                //txtcityid.Text = dataGridView1.CurrentRow.Cells["CITY_ID"].Value.ToString();
                //btnUpdate.Enabled = true;
                //btnDelete.Enabled = true;
                // DataTable ss = DBNull;

                if (label2.Text == "Year")
                {
                    string year = dataGridView1.CurrentRow.Cells["Year"].Value.ToString();
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter sda = new SqlDataAdapter("Select distinct FEE_ALIAS+' '+mop from view_dailydynamic where FEE_ALIAS !='' order by FEE_ALIAS+' '+mop", sc);
                    DataTable dat = new DataTable();
                    sda.Fill(dat);
                    string[] values1 = new string[dat.Rows.Count];
                    for (int i = 0; i < dat.Rows.Count; i++)
                    {
                        values1[i] = dat.Rows[i][0].ToString().ToUpper();
                    }
                    string value = "[";
                    value += values1.Aggregate((a, b) => a + "],[" + b);
                    value += "],[CONCESSION],[PREV. DUES],[LATE FINE]";
                    //SqlDataAdapter da = new SqlDataAdapter("select FEE_ALIAS,Year(cast(frc_date as DATE)) date from view_dailydynamic   group by Year(cast(frc_date as DATE)),FEE_ALIAS ORDER by Year(cast(frc_date as DATE)),FEE_ALIAS", sc);
                    //DataTable ds = new DataTable();
                    //da.Fill(ds);
                    sc.Open();
                    SqlCommand com = new SqlCommand("sp_dailydynamicmonth '" + year + "'", sc);
                    com.ExecuteNonQuery();
                    sc.Close();
                    //value = "select date," + value + " from (select date,feetype,amount as date1 from tempmonth ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv;";
                    value = "select date As Month,Year1 Year,dt Date," + value + " from (select date,Year1,Cast(date as varchar)+'/'+Year1 dt,feetype,amount as date1 from tempmonth ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv order by cast(date as int);";
                    //value = "select date," + value + " from (select date,feetype,amount as date1 from tempyear ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv;";
                    //DataTable dtReturn = GetInversedDataTable(ds, "");
                    // DataTable dtReturn = GetInversedDataTable(ds, "", "", "", " ", true);
                    SqlDataAdapter da = new SqlDataAdapter(value, sc);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    dataGridView1.DataSource = "";
                    dataGridView1.DataSource = ds;
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        dataGridView1.Columns[i].Width = 180;
                    }
                    label2.Text = "Month";
                    // dataGridView1.Columns["Day"].HeaderText = "Day";
                    dataGridView1.Columns["Month"].Visible = false;
                    dataGridView1.Columns["Year"].Visible = false;
                }
                else if (label2.Text == "Month")
                {
                    string year = dataGridView1.CurrentRow.Cells["Year"].Value.ToString();
                    string month = dataGridView1.CurrentRow.Cells["Month"].Value.ToString();

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter sda = new SqlDataAdapter("Select distinct FEE_ALIAS+' '+mop from view_dailydynamic where FEE_ALIAS !='' order by FEE_ALIAS+' '+mop", sc);
                    DataTable dat = new DataTable();
                    sda.Fill(dat);
                    string[] values1 = new string[dat.Rows.Count];
                    for (int i = 0; i < dat.Rows.Count; i++)
                    {
                        values1[i] = dat.Rows[i][0].ToString().ToUpper();
                    }
                    string value = "[";
                    value += values1.Aggregate((a, b) => a + "],[" + b);
                    value += "],[CONCESSION],[PREV. DUES],[LATE FINE]";
                    //SqlDataAdapter da = new SqlDataAdapter("select FEE_ALIAS,Year(cast(frc_date as DATE)) date from view_dailydynamic   group by Year(cast(frc_date as DATE)),FEE_ALIAS ORDER by Year(cast(frc_date as DATE)),FEE_ALIAS", sc);
                    //DataTable ds = new DataTable();
                    //da.Fill(ds);
                    sc.Open();
                    SqlCommand com = new SqlCommand("sp_dailydynamicDay '" + month + "','" + year + "'", sc);
                    com.ExecuteNonQuery();
                    sc.Close();
                    //value = "select date," + value + " from (select date,feetype,amount as date1 from tempmonth ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv;";
                    value = "select date As Day,Month1 Month,Year1 Year,dt Date," + value + " from (select date,Month1,Year1,cast(date as varchar)+'/'+cast(Month1 as varchar)+'/'+cast(Year1 as varchar) dt,feetype,amount as date1 from tempDay ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv order by cast(date as int);";
                    //value = "select date," + value + " from (select date,feetype,amount as date1 from tempyear ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv;";
                    //DataTable dtReturn = GetInversedDataTable(ds, "");
                    // DataTable dtReturn = GetInversedDataTable(ds, "", "", "", " ", true);
                    SqlDataAdapter da = new SqlDataAdapter(value, sc);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    dataGridView1.DataSource = "";
                    dataGridView1.DataSource = ds;
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        dataGridView1.Columns[i].Width = 180;
                    }
                    dataGridView1.Columns["Day"].Visible = false;
                    dataGridView1.Columns["Month"].Visible = false;
                    dataGridView1.Columns["Year"].Visible = false;
                    label2.Text = "Day";
                }
                else if (label2.Text == "Day")
                {
                    string Day = dataGridView1.CurrentRow.Cells["Day"].Value.ToString();
                    string year = dataGridView1.CurrentRow.Cells["Year"].Value.ToString();
                    string month = dataGridView1.CurrentRow.Cells["Month"].Value.ToString();

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter sda = new SqlDataAdapter("Select distinct FEE_ALIAS+' '+mop from view_dailydynamic where FEE_ALIAS !='' order by FEE_ALIAS+' '+mop", sc);
                    DataTable dat = new DataTable();
                    sda.Fill(dat);
                    string[] values1 = new string[dat.Rows.Count];
                    for (int i = 0; i < dat.Rows.Count; i++)
                    {
                        values1[i] = dat.Rows[i][0].ToString().ToUpper();
                    }
                    string value = "[";
                    value += values1.Aggregate((a, b) => a + "],[" + b);
                    value += "],[CONCESSION],[PREV. DUES],[LATE FINE]";
                    //SqlDataAdapter da = new SqlDataAdapter("select FEE_ALIAS,Year(cast(frc_date as DATE)) date from view_dailydynamic   group by Year(cast(frc_date as DATE)),FEE_ALIAS ORDER by Year(cast(frc_date as DATE)),FEE_ALIAS", sc);
                    //DataTable ds = new DataTable();
                    //da.Fill(ds);
                    sc.Open();
                    SqlCommand com = new SqlCommand("sp_dailydynamicstudent '" + Day + "','" + month + "','" + year + "'", sc);
                    com.ExecuteNonQuery();
                    sc.Close();
                    //value = "select date," + value + " from (select date,feetype,amount as date1 from tempmonth ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv;";
                    value = "select date as Student, Day1 As Day,Month1 Month,Year1 Year,dt Date," + value + " from (select date,Day1,Month1,Year1,cast(Day1 as varchar)+'/'+cast(Month1 as varchar)+'/'+cast(Year1 as varchar) dt,feetype,amount as date1 from tempStudent ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv order by date;";
                    //value = "select date," + value + " from (select date,feetype,amount as date1 from tempyear ) src pivot(  max(date1)  for feetype in  (" + value + "  )) piv;";
                    //DataTable dtReturn = GetInversedDataTable(ds, "");
                    // DataTable dtReturn = GetInversedDataTable(ds, "", "", "", " ", true);
                    SqlDataAdapter da = new SqlDataAdapter(value, sc);
                    DataTable ds = new DataTable();
                    da.Fill(ds);
                    dataGridView1.DataSource = "";
                    dataGridView1.DataSource = ds;
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        dataGridView1.Columns[i].Width = 180;
                    }
                    dataGridView1.Columns["Day"].Visible = false;
                    dataGridView1.Columns["Month"].Visible = false;
                    dataGridView1.Columns["Year"].Visible = false;
                    label2.Text = "Student";
                }
            }
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            LoadGrid();
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
    }
}