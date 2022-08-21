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
using Microsoft.Office.Interop.Excel;

namespace sms
{
    public partial class cryEntryvoucherDetail : Form
    {
        decimal Credit;
        decimal Debit;
        decimal value;
        public cryEntryvoucherDetail()
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
        }
        public void InitGrid()
        {
            //LoadGrid();
            //dataGridView1.Columns["Name"].Width = 40;
            //dataGridView1.Columns["Description"].Width = 100;
            ////dataGridView1.Columns["ClassName"].Width = 60;
            //dataGridView1.Columns["Ledgerdate"].Width = 60;
            //dataGridView1.Columns["GName"].Width = 60;
            ////dataGridView1.Columns["userid"].Width = 60;
            //dataGridView1.Columns["date"].Width = 80;
            //dataGridView1.Columns["terminal"].Width = 100;
        }
        private void LoadGrid()
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from View_accountmaster ORDER BY Name", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
        }
      

        private void showstudentreport()
        {

            dataGridView1.Visible = true;
           

           
                DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("sprocvoucherentry ", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@min1", dt1.ToString("MM/dd/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@max1", dt2.ToString("MM/dd/yyyy"));
                sc.Open();
                DataSet dt = new DataSet();
                    da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt.Tables[0];
                    dataGridView1.Columns["SNo"].Visible = true;
                    dataGridView1.Columns["sno"].Width = 40;
                    dataGridView1.Columns["sdate"].HeaderText = "Date";
                    dataGridView1.Columns["sdate"].Width = 100;
                    dataGridView1.Columns["sparticular"].HeaderText = "Particulers";
                    dataGridView1.Columns["sparticular"].Width = 100;
                    dataGridView1.Columns["saccount"].HeaderText = "Accounts";
                    dataGridView1.Columns["saccount"].Width = 100;
                    dataGridView1.Columns["vouchertype"].HeaderText = "Vtype";
                    dataGridView1.Columns["vouchertype"].Width = 100;
                    dataGridView1.Columns["voucherno"].HeaderText = "Vno";
                    dataGridView1.Columns["voucherno"].Width = 100;
                    dataGridView1.Columns["cashin"].HeaderText = "Cash (In)";
                    dataGridView1.Columns["cashin"].Width = 80;
                    dataGridView1.Columns["cashout"].HeaderText = "Cash(Out)";
                    dataGridView1.Columns["cashout"].Width = 80;
                    dataGridView1.Columns["bankin"].HeaderText = "Bank(In)";
                    dataGridView1.Columns["bankin"].Width = 90;
                    dataGridView1.Columns["bankout"].HeaderText = "Bank(Out)";
                    dataGridView1.Columns["bankout"].Width = 90;
                    dataGridView1.Columns["balancec"].HeaderText = "Balance(Cash)";
                    dataGridView1.Columns["balancec"].Width = 100;
                    dataGridView1.Columns["balanceb"].HeaderText = "Balance(Bank)";
                    dataGridView1.Columns["balanceb"].Width = 100;
                   
                }

                else
                {
                    dataGridView1.DataSource = DBNull.Value;


                }
               
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string values1=(row.Cells[6].Value).ToString();
                    if (values1.Substring((values1.Length-2),2)=="Cr")
                    {
                        //row.DefaultCellStyle.BackColor = Color.Red;
                        row.Cells[6].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                       // row.DefaultCellStyle.BackColor = Color.Green;
                        row.Cells[6].Style.ForeColor = Color.Green;
                    }
                }
                sc.Close();
            
        }

           

        

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
                
        }
        
        
        private void frmLedgerName_Load(object sender, EventArgs e)
        {
            
          
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {


            //cRptACvoucher.dt1 = dateTimePicker1.Value;
            //cRptACvoucher.dt2 = dateTimePicker2.Value;

           //cRptACvoucher obj = new cRptACvoucher();
            
        }

        private void cmbGroup_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("sprocvoucherentry ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@min1", dt1.ToString("MM/dd/yyyy"));
            da.SelectCommand.Parameters.AddWithValue("@max1", dt2.ToString("MM/dd/yyyy"));
            sc.Open();
            DataSet dt = new DataSet();
            da.Fill(dt);

            ExportDataSetToExcel(dt);

        }
        private void ExportDataSetToExcel(DataSet ds)
        {
            try
            {
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

                // Loop over DataTables in DataSet.
                DataTableCollection collection = ds.Tables;

                for (int i = collection.Count; i > 0; i--)
                {
                    Sheets xlSheets = null;
                    Worksheet xlWorksheet = null;
                    //Create Excel Sheets
                    xlSheets = ExcelApp.Sheets;
                    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                                   Type.Missing, Type.Missing, Type.Missing);

                    System.Data.DataTable table = collection[i - 1];
                    xlWorksheet.Name = table.TableName;

                    for (int j = 1; j < table.Columns.Count + 1; j++)
                    {
                        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    }

                    // Storing Each row and column value to excel sheet
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        for (int l = 0; l < table.Columns.Count; l++)
                        {
                            ExcelApp.Cells[k + 2, l + 1] =
                            table.Rows[k].ItemArray[l].ToString();
                        }
                    }
                    ExcelApp.Columns.AutoFit();
                }
                ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
                ExcelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
    }
}
