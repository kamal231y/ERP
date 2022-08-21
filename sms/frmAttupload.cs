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
using System.Collections;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Office.Interop.Excel;
namespace sms
{
    public partial class frmAttupload : Form
    {
        public frmAttupload()
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

        private void Cancil_form()
        {
           
            btn_create.Enabled = true;
            cmbClass.Text = "Select";
            cmbExam.Text = "Select";
            //LoadGrid();
            dataGridView1.DataSource = DBNull.Value;

        }
       
        private void bindmonth()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            if (!Form1.board)
                da = new SqlDataAdapter(" select id,year from Year_T order by id ", sc);
            else
                da = new SqlDataAdapter(" select id,year_up as year from Year_T order by id ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["id"] = 0;
            row["Year"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "Year";
            cmbExam.ValueMember = "id";
        }
        
        
       
        
        private void btn_create_Click(object sender, EventArgs e)
        {
           
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
         }
        private void SubjectMarks_Load(object sender, EventArgs e)
        {
            
            bindmonth(); 
        }

        private void cmbExam_Leave(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSubjectCategory_Leave(object sender, EventArgs e)
        {
            
        }

        private void CMBSUB1_Leave(object sender, EventArgs e)
        {
            
        }

        private void CMBSUB2_Leave(object sender, EventArgs e)
        {
            
        }

        private void CMBCATEGORY_Leave(object sender, EventArgs e)
        {
           
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            //if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    List<string> obj = new List<string>();
                    string fileName = "";
                    string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                    string logoimage = Path.Combine(outPutDirectory, "upload");
                    Uri uri = new Uri(logoimage);
                    OpenFileDialog OpenFD = new OpenFileDialog();
                    OpenFD.Title = "Specify Backup Location...";
                    // OpenFD.Filter = ".xls|*.xlsx";
                    OpenFD.FileName = null;
                    if (OpenFD.ShowDialog() != DialogResult.Cancel)
                    {
                        StringBuilder qu = new StringBuilder();
                        fileName = OpenFD.FileName;
                        System.IO.File.Copy(fileName, uri.LocalPath + "//" + Path.GetFileName(fileName), true);
                        logoimage = Path.Combine(outPutDirectory, "upload\\" + Path.GetFileName(fileName));
                        string imagepath = new Uri(logoimage).LocalPath;
                        string ext = Path.GetExtension(fileName);

                        if (File.Exists(imagepath))
                        {
                            System.Data.DataTable dtdata = GetDataTabletFromCSVFile(imagepath);
                            DateTime newdate; dataclass dc = new dataclass();
                            SqlConnection sc = dc.con();
                            if (dtdata.Rows.Count > 0)
                            {
                                int count = 0; int marks = 0;
                                for (int i = 0; i < dtdata.Rows.Count; i++)
                                {
                                    if (dtdata.Rows[i]["No"].ToString() != "")
                                    {

                                        try
                                        {

                                            SqlDataAdapter da;
                                            da = new SqlDataAdapter("UPLOADteacheratt_EXCEL_SHEET", sc);
                                            da.SelectCommand.CommandTimeout = 300;
                                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            da.SelectCommand.Parameters.AddWithValue("@SAT_STU_ID", dtdata.Rows[i]["EnNo"].ToString());                                                                                     
                                            da.SelectCommand.Parameters.AddWithValue("@SAT_DATE ", dtdata.Rows[i]["DateTime"].ToString());                                 
                                            da.SelectCommand.Parameters.AddWithValue("@YEAR",textBox1.Text);                                          
                                            System.Data.DataTable dt = new System.Data.DataTable();
                                            da.Fill(dt);
                                            obj.Add(dtdata.Rows[i]["No"].ToString());


                                        }

                                        catch (Exception ex)
                                        {
                                           
                                        }
                                       
                                    }
                                }
                               
                                
                            }
                            MsgBox.ShowBox("Attendence Uploded");
                        }
                    }


                }

                catch (Exception ex)
                {
                    MsgBox.ShowBox("Attendence Not  Uploded");
                    MessageBox.Show(ex.ToString());
                }
            }

        }
        private static System.Data.DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            System.Data.DataTable csvData = new System.Data.DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }

        private void btndownload_Click(object sender, EventArgs e)
        {
            
            
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
    }
}
