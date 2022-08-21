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
using Excel = Microsoft.Office.Interop.Excel;
using MsgBoxRet;
using Microsoft.Office.Interop.Excel;

namespace sms
{
    public partial class Download : Form
    {
       
        string yaer;
        string hostel, school_id;
       string classname;
       SqlDataReader dr;
       public Download()
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
            //cmbbilltype.SelectedIndex = 0;
            
            Cancil_form();
            
            
        }

       private void BindCompany()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter("select * from COMPANY_T", sc);
           System.Data.DataTable dt = new System.Data.DataTable();
           da.Fill(dt);
           if (dt.Rows.Count > 0)
           {
               school_id = dt.Rows[0]["school_id"].ToString();
           }
           else
           {
               school_id = " ";
           }
       }
            
        private void Cancil_form()
        {
            panelsearch.Visible = true;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            showstudentreport();
        
        }
      private void showstudentreport()
         {
           DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
           DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

           dataclass dd = new dataclass();
           SqlConnection sc = dd.conweb();

           string str = "select t2.stu_adm_no,t2.stu_first_name, FRC_NO,FRC_DATE,FRC_MONTH,FRC_PREV_DUE,FRC_FEE_AMT,FRC_LATE_FINE,FRC_CONCESSION,FRC_PAID_AMT,( (FRC_FEE_AMT+FRC_LATE_FINE+FRC_PREV_DUE)-(FRC_CONCESSION+FRC_PAID_AMT)) as Balance,(case when frc_status=0 then 'Paid' else 'Cancel' end) as Status from FEE_RECEIPT_Tweb t1 ,student_master t2 WHERE  t1.frc_stu_id=t2.stu_id  and CAST (FRC_DATE AS DATE) BETWEEN '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' and school_id='" + school_id + "' and FRC_BANK='" + cmbMode.SelectedValue + "'   order by FRC_DATE,FRC_NO desc";
          if (str != " ")
          {
              dataGridView1.Visible = true;
              SqlDataAdapter da = new SqlDataAdapter(str, sc);
              System.Data.DataTable dt = new System.Data.DataTable();
              da.Fill(dt);
              if (dt.Rows.Count > 0)
              {
                  dataGridView1.DataSource = dt;
                  lblcount.Text = "Total No Of Record : "+ dt.Rows.Count.ToString();
              }
              else
              {
                  dataGridView1.DataSource = DBNull.Value;
                  lblcount.Text = "Total No Of Record : " + "0";
              }
          }

            sc.Close(); 
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void frmGrDetail_Load(object sender, EventArgs e)
        {
            BindCompany(); Bindmode();
        }

        private void Bindmode()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.connWeb();
            SqlDataAdapter da = new SqlDataAdapter("select id,mop from tblmoponline where school_id='" + school_id + "'",sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["mop"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbMode.DataSource = ds.Tables[0];
            cmbMode.DisplayMember = "mop";
            cmbMode.ValueMember = "id";
        }


        private void txtfname_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void txtfathername_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbroll_Leave(object sender, EventArgs e)
        {
            showstudentreport();  
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            
        }

        private void cmbadmno_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

       
        private void radioscholor_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void radiohostler_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothhostel_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radionormal_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiostaff_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothward_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiomale_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiofemale_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdsexboth_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void panelsearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.conweb();
            string str = " select t2.stu_adm_no,t2.stu_acc_no,t2.stu_first_name,t2.stu_father_name, FRC_NO,FRC_DATE,FRC_MONTH,FRC_PREV_DUE,FRC_FEE_AMT,FRC_LATE_FINE,FRC_CONCESSION,FRC_PAID_AMT,( (FRC_FEE_AMT+FRC_LATE_FINE+FRC_PREV_DUE)-(FRC_CONCESSION+FRC_PAID_AMT)) as Balance,(case when frc_status=0 then 'Paid' else 'Cancel' end) as Status from FEE_RECEIPT_Tweb t1 ,student_master t2 WHERE  t1.frc_stu_id=t2.stu_id  and CAST (FRC_DATE AS DATE) BETWEEN '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' and school_id='" + school_id + "' and FRC_BANK='" + cmbMode.SelectedValue + "'   order by FRC_DATE,FRC_NO desc  ";
            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataSet dt = new System.Data.DataSet();
                da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ExportDataSetToExcel(dt);
                }
                else
                {
                    dataGridView1.DataSource = DBNull.Value;
                    lblcount.Text = "Total No Of Record : " + "0";
                }
            }

            sc.Close();

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Cancil_form();
        }
       
        }       
    }

