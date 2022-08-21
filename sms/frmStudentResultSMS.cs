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
using System.Net;
namespace sms
{  
    public partial class frmStudentResultSMS : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader; 
        public frmStudentResultSMS()
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
            //LoadGrid();
            dataGridView1.DataSource = DBNull.Value;

        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
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
    
        public void InitGrid()
        {
            if (cmbClass.SelectedValue!=null)
            LoadGrid();


        }
        private void LoadGrid()
        {
          
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("BIND_STUDENT_Result", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
             sc.Open();
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["AdmNo"].Width = 100;
            dataGridView1.Columns["Student_Name"].Width = 200;
            dataGridView1.Columns["Student_Name"].HeaderText = "StudentName";
            dataGridView1.Columns["PhONE"].Width = 100;
            dataGridView1.Columns["MM_MARKS"].Width = 80;
            dataGridView1.Columns["MM_MARKS"].HeaderText = "MM";
            dataGridView1.Columns["OB_MARKS"].HeaderText = "OB";
            dataGridView1.Columns["OB_MARKS"].Width = 80;
            dataGridView1.Columns["PERCENTAGE"].Width = 100;
            dataGridView1.Columns["PERCENTAGE"].HeaderText = "(%)Marks";
            dataGridView1.Columns["GRADE"].Width = 100;

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            sms1();
        }
        private void sms1()
        {
              if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        if (dataGridView1.Rows[z].Cells["phone"].Value.ToString() != "")
                        {
                            string msgtxt = "Dear Parents, Your Student  " +
                             dataGridView1.Rows[z].Cells["Student_Name"].Value.ToString() + " " + "Has Obtained Marks " +
                             dataGridView1.Rows[z].Cells["OB_MARKS"].Value.ToString() + "/" +
                             dataGridView1.Rows[z].Cells["MM_MARKS"].Value.ToString() + " and Percentage "
                             + dataGridView1.Rows[z].Cells["PERCENTAGE"].Value.ToString() + "%  and Grade  " +
                             dataGridView1.Rows[z].Cells["Grade"].Value.ToString() + "  ,Thanks";
                            string msg = "|";
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            msg = SMS.SMS(msgtxt.Trim(), 0, dataGridView1.Rows[z].Cells["phone"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));

                            string[] str = msg.Split('|');
                            if (str[0].ToString().Trim().Replace(" ", "") == "error")
                            {
                                //   MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                }

            }
             else
                MessageBox.Show("There is No Marks for  sending  SMS. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

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
            
        }

        private void cmbExam_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSubjectCategory_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        

        private void btnUpload_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.Rows.Count > 0)
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
                            dataGridView1.DataSource = dtdata;
                            DateTime newdate; dataclass dc = new dataclass();
                            SqlConnection sc = dc.con();
                            if (dtdata.Rows.Count > 0)
                            {
                                int count = 0; int marks = 0;
                                for (int i = 0; i < dtdata.Rows.Count; i++)
                                {
                                    if (dtdata.Rows[i]["AdmNo"].ToString() != "")
                                    {

                                        try
                                        {

                                            //SqlDataAdapter da;
                                            //da = new SqlDataAdapter("UPLOADMARKS_STUDENT_RESULT", sc);
                                            //da.SelectCommand.CommandTimeout = 300;
                                            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            //if (dtdata.Rows[i]["MM_MARKS"].ToString() == "")
                                            //{
                                            //    int MM = 0, OB = 0;
                                            //    MM = Convert.ToInt32(dtdata.Rows[i]["MM_MARKS"].ToString());
                                            //    if (dtdata.Rows[i]["OB_MARKS"].ToString() == "")
                                            //        OB = Convert.ToInt32(dtdata.Rows[i]["OB_MARKS"].ToString());
                                            //    da.SelectCommand.Parameters.AddWithValue("@ADM_NO", dtdata.Rows[i]["AdmNo"].ToString());
                                            //    da.SelectCommand.Parameters.AddWithValue("@SBM_MIN_MARK", OB);
                                            //    da.SelectCommand.Parameters.AddWithValue("@SBM_MAX_MARK", MM);
                                            //    da.SelectCommand.Parameters.AddWithValue("@SBM_MAX_MARK", marks);
                                            //    System.Data.DataTable dt = new System.Data.DataTable();
                                            //    da.Fill(dt);

                                            //}
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                       
                                    }
                                }
                                //LoadGrid();
                            }

                        }
                    }


                }

                catch (Exception ex)
                {
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
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("BIND_STUDENT_Result", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
            sc.Open();
            DataSet dt = new DataSet();
            da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ExportDataSetToExcel(dt);
                }
                else
                {
                    MessageBox.Show("No Records");
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

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }
       
    }
}
