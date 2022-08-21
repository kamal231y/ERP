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
    public partial class frmStudentReportInExcel : Form
    {
        string hostel;
        string classname;
        SqlDataReader dr;
        public frmStudentReportInExcel()
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
            Cancil_form();
            
        }

        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND SEC_ID IN (11,13) order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
        }
       private void Cancil_form()
        {
          
            cmbclass.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {

           {
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbclass.SelectedValue.ToString();
               }


               if (rdbothhostel.Checked)
               {
                   hostel = "%";
               }
               else
               {
                   if (radiohostler.Checked)
                   {
                       hostel = "H";

                   }
                   else
                   {
                       hostel = "N";
                   }
               }
               //string str = "SELECT * FROM STUDENT_T ORDER BY STU_ID ";
               string str = " select AdmNo as SCH_NO,CAST(SER_NO AS VARCHAR) as SERIAL,REG_NO as IREGDNO,CAT_ALIAS as CAT,Student_Name AS CNAME,MotherName AS MNAME,FatherName AS FNAME,STU_SEX AS SEX,CAT_ALIAS AS CASTE,ALIAS AS HANDICAP ,CAST(CODE1 AS VARCHAR) AS SUB1,MED1,CAST(CODE2 AS VARCHAR(4)) AS SUB2,MED2,CAST(CODE3 AS VARCHAR) AS SUB3,MED3,CAST(CODE4 AS VARCHAR) AS SUB4,MED4,CAST(CODE5 AS VARCHAR) AS SUB5,MED5,CAST(CODE6 AS VARCHAR) AS SUB6,MED6,CONVERT(VARCHAR(10), DOB, 103) as D_O_B,STU_ANNUAL_INCOME as ANNUAL_INC,(case when ONLY_CHILD=1 then 'Y' else 'N' end) as ONLY_CHILD from View_STUDENT_9TH_REGISTRATION where STC_SEC_ID like '" + classname + "'  ORDER BY SER_NO";
               if (str != "")
               {
                   //,cast(cast(Adm_Date as date) as varchar)
                   dataclass dc = new dataclass();
                   SqlConnection sc = dc.con();
                   SqlDataAdapter da = new SqlDataAdapter(str, sc);
                   DataSet dt = new DataSet();
                   da.Fill(dt);
                   if (dt.Tables.Count > 0)
                   {
                       ExportDataSetToExcel(dt);
                   }
                   
               }


           }
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
          
            Cancil_form();
            
        }

  

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            bindData();
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReceiptDetailReport_Load(object sender, EventArgs e)
        {
            bindclass();
        }

       

       
    }
}