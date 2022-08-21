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
    public partial class frmStudentMarksExamwise : Form
    {
        public frmStudentMarksExamwise()
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
            cmbClass.Text = "Select";
            cmbExam.Text = "Select";
          
            //LoadGrid();
 
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            string STR="";
            STR = "select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by CLS_SEQUENCE ";
            SqlDataAdapter da = new SqlDataAdapter(STR, sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "class";
            cmbClass.ValueMember = "SEC_ID";
        }
        private void bindExam()
        {
            string classID = "0";
            if (cmbClass.SelectedValue != null)
                classID = cmbClass.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from examination_t order by EXAM_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EXAM_DESC"] = "Select";
            row["EXAM_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "EXAM_DESC";
            cmbExam.ValueMember = "EXAM_ID";
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
 
        private void SubjectMarks_Load(object sender, EventArgs e)
        {
            bindclass();
            bindExam(); 
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (cmbClass.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Select Class ");
            }
            else if (cmbExam.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Select Exam ");
            }

            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("GREEN_SHEET_SUBJECT_WISE", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
                da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(cmbExam.SelectedValue));
                da.SelectCommand.Parameters.AddWithValue("@userid", sms.Form1.GlobalLoginUserName.ToString());
                sc.Open();
                DataSet dt = new DataSet();
                da.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {

                        ExportDataSetToExcel(dt);
                    }
                    else
                    {
                        MessageBox.Show("No Records");
                    }
                }


                sc.Close();
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

        private void cmbClass_Layout(object sender, LayoutEventArgs e)
        {
           
        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            bindExam();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (cmbClass.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Select Class ");
            }
            else if (cmbExam.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Select Exam ");
            }
            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("GREEN_SHEET_SUBJECT_WISE", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
                da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(cmbExam.SelectedValue));
                da.SelectCommand.Parameters.AddWithValue("@userid", sms.Form1.GlobalLoginUserName.ToString());
                DataSet dt = new DataSet();
                da.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                    dataGridView1.DataSource = dt.Tables[0];
                    dataGridView1.Columns[0].Width = 85;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[2].Width = 100;
                    dataGridView1.Columns[3].Width = 100;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                    if (dataGridView1.ColumnCount > 6)
                        dataGridView1.Columns[6].Width = 100;
                    if (dataGridView1.ColumnCount > 7)
                        dataGridView1.Columns[7].Width = 100;
                    cryAwardlist obj = new cryAwardlist();
                    cryAwardlist.className = cmbClass.Text.ToString();
                    cryAwardlist.classid = cmbClass.SelectedValue.ToString();
                    cryAwardlist.EXAMID = cmbExam.SelectedValue.ToString();
                    cryAwardlist.ExamName = cmbExam.Text.ToString();
                    cryAwardlist.marks1 = dataGridView1.Columns[2].HeaderText;
                    cryAwardlist.marks2 = dataGridView1.Columns[3].HeaderText;
                    cryAwardlist.marks3 = dataGridView1.Columns[4].HeaderText;
                    cryAwardlist.marks4 = dataGridView1.Columns[5].HeaderText;
                    if (dataGridView1.ColumnCount > 6)
                        cryAwardlist.marks5 = dataGridView1.Columns[6].HeaderText;
                    else
                        cryAwardlist.marks5 = "";
                    if (dataGridView1.ColumnCount > 7)
                        cryAwardlist.marks6 = dataGridView1.Columns[7].HeaderText;
                    else
                        cryAwardlist.marks6 = "";
                  
                    obj.ShowDialog();
                }
                else
                {
                    dataGridView1.DataSource = DBNull.Value;
                    
                }
               
            }
 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmbClass.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Select Class ");
            }
            else if (cmbExam.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Select Exam ");
            }

            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("GREEN_SHEET_SUBJECT_WISE", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
                da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(cmbExam.SelectedValue));
                da.SelectCommand.Parameters.AddWithValue("@userid", sms.Form1.GlobalLoginUserName.ToString());
                DataSet dt = new DataSet();
                da.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                    dataGridView1.DataSource = dt.Tables[0];
                    dataGridView1.Columns[0].Width =85 ;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[2].Width = 100;
                    dataGridView1.Columns[3].Width = 100;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                    if (dataGridView1.ColumnCount>6)
                    dataGridView1.Columns[6].Width = 100;
                    if (dataGridView1.ColumnCount > 7)
                    dataGridView1.Columns[7].Width = 100;
                }
                else
                    dataGridView1.DataSource = DBNull.Value;
            }
        }
       
    }
}
