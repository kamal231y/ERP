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
    public partial class frmGrEntryByExcelsheet : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmGrEntryByExcelsheet()
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
            bindstate();
        }
      
        private void bindstate()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbState.DataSource=ds.Tables[0];
            cmbState.DisplayMember = "Class";
            cmbState.ValueMember = "SEC_ID";
        }
 
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            INSERT_DATA_BY_CSV();
        }
     
        private void INSERT_DATA_BY_CSV()
       {
            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                MessageBox.Show("Pls Select Class ..");
                return;
            }
            else
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
                            string MOB = ""; string PHONE = "";
                            DateTime dob, doa, tcdate, Inacdate;
                            dataclass dc = new dataclass();
                            SqlConnection sc = dc.con();
                            if (dtdata.Rows.Count > 0)
                            {
                                int count = 0;
                                for (int i = 0; i < dtdata.Rows.Count; i++)
                                {
                                    if (dtdata.Rows[i]["NAME"].ToString() != "")
                                    {

                                        try
                                        {
                                            SqlDataAdapter da;
                                            da = new SqlDataAdapter("UploadStudent", sc);
                                            da.SelectCommand.CommandTimeout = 300;
                                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            da.SelectCommand.Parameters.AddWithValue("@stu_class", cmbState.SelectedValue);
                                            da.SelectCommand.Parameters.AddWithValue("@fname", dtdata.Rows[i]["NAME"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@father_name", dtdata.Rows[i]["FATHERNAME"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@mother_name", dtdata.Rows[i]["MOTHERNAME"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@father_occupation", dtdata.Rows[i]["FATHEROCCUPATION"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@mother_occupation", dtdata.Rows[i]["MOTHEROCCUPATION"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@caste_name", dtdata.Rows[i]["CASTENAME"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@religion_name", dtdata.Rows[i]["RELIGIONNAME"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@MOBILE", dtdata.Rows[i]["MOBILENO"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@MOBILE2", dtdata.Rows[i]["MOBILENO2"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@AADHAR", dtdata.Rows[i]["AADHAR"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@NATIONALITY", dtdata.Rows[i]["NATIONALITY"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@ADMISSIONCLASS", dtdata.Rows[i]["ADMISSIONCLASS"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@ADMISSIONSECTION", dtdata.Rows[i]["ADMISSIONSECTION"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@SESSION", dtdata.Rows[i]["SESSION(YYYY-YYYY)"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@EMAIL", dtdata.Rows[i]["EMAIL"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@ANNUALINCOME", dtdata.Rows[i]["ANNUALINCOME"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@INTRODUCER", dtdata.Rows[i]["CATEGORY"].ToString().Trim());
                                            //da.SelectCommand.Parameters.AddWithValue("@phone", PHONE);
                                            da.SelectCommand.Parameters.AddWithValue("@ROLL", dtdata.Rows[i]["ROLLNUMBER"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@LADDRESS", dtdata.Rows[i]["LADDRESS"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@PADDRESS", dtdata.Rows[i]["PADDRESS"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@TCNO", dtdata.Rows[i]["TCNO"].ToString().Trim());

                                            if (dtdata.Rows[i]["GENDER(M/F)"].ToString().Trim() == "")
                                                da.SelectCommand.Parameters.AddWithValue("@GENDER", "M");
                                            else
                                                da.SelectCommand.Parameters.AddWithValue("@GENDER", dtdata.Rows[i]["GENDER(M/F)"].ToString().Trim());
                                            if (dtdata.Rows[i]["DOB(MM/DD/yyyy)"].ToString().Trim() == "")
                                                da.SelectCommand.Parameters.AddWithValue("@DOB", "");
                                            else
                                            {
                                                try
                                                {
                                                    string[] msg = dtdata.Rows[i]["DOB(MM/DD/YYYY)"].ToString().Trim().Split('/');
                                                    dob = Convert.ToDateTime(msg[1].ToString() + "/" + msg[0].ToString() + "/" + msg[2].ToString());
                                                    da.SelectCommand.Parameters.AddWithValue("@DOB", dob.ToString("MM/dd/yyyy"));
                                                }
                                                catch (Exception ex)
                                                {
                                                    string[] msg = dtdata.Rows[i]["DOB(MM/DD/YYYY)"].ToString().Trim().Split('/');
                                                    dob = Convert.ToDateTime(msg[0].ToString() + "/" + msg[1].ToString() + "/" + msg[2].ToString());
                                                    da.SelectCommand.Parameters.AddWithValue("@DOB", dob.ToString("MM/dd/yyyy"));
                                           
                                                }
                                            }
                                            if (dtdata.Rows[i]["DOA(MM/DD/YYYY)"].ToString().Trim() == "")
                                                da.SelectCommand.Parameters.AddWithValue("@DOA", "");
                                            else
                                            {
                                                try
                                                {
                                                    string[] msg = dtdata.Rows[i]["DOA(MM/DD/YYYY)"].ToString().Trim().Split('/');
                                                    doa = Convert.ToDateTime(msg[1].ToString() + "/" + msg[0].ToString() + "/" + msg[2].ToString());
                                                    da.SelectCommand.Parameters.AddWithValue("@DOA", doa.ToString("MM/dd/yyyy"));
                                                }
                                                catch (Exception ex)
                                                {
                                                    string[] msg = dtdata.Rows[i]["DOA(MM/DD/YYYY)"].ToString().Trim().Split('/');
                                                    doa = Convert.ToDateTime(msg[0].ToString() + "/" + msg[1].ToString() + "/" + msg[2].ToString());
                                                    da.SelectCommand.Parameters.AddWithValue("@DOA", doa.ToString("MM/dd/yyyy"));

                                                }
                                            }
                                            if (dtdata.Rows[i]["TCDATE(MM/DD/YYYY)"].ToString().Trim() == "")
                                                da.SelectCommand.Parameters.AddWithValue("@TCDATE", "");
                                            else
                                            {
                                                string[] msg = dtdata.Rows[i]["TCDATE(MM/DD/YYYY)"].ToString().Trim().Split('/');
                                                tcdate = Convert.ToDateTime(msg[1].ToString() + "/" + msg[0].ToString() + "/" + msg[2].ToString());
                                                da.SelectCommand.Parameters.AddWithValue("@TCDATE", tcdate.ToString("MM/dd/yyyy"));
                                            }
                                            if (dtdata.Rows[i]["IDATE(MM/DD/YYYY)"].ToString().Trim() == "")
                                                da.SelectCommand.Parameters.AddWithValue("@IDATE", "");
                                            else
                                            {
                                                string[] msg = dtdata.Rows[i]["IDATE(MM/DD/YYYY)"].ToString().Trim().Split('/');
                                                Inacdate = Convert.ToDateTime(msg[1].ToString() + "/" + msg[0].ToString() + "/" + msg[2].ToString());
                                                da.SelectCommand.Parameters.AddWithValue("@IDATE", Inacdate.ToString("MM/dd/yyyy"));
                                            }
                                            da.SelectCommand.Parameters.AddWithValue("@ADMNO", dtdata.Rows[i]["ADMNO"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@SRNO", dtdata.Rows[i]["SRNO"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@auto_adm", Convert.ToInt32(chkadm.Checked));
                                            da.SelectCommand.Parameters.AddWithValue("@auto_roll", Convert.ToInt32(chkroll.Checked));


                                            System.Data.DataTable dt = new System.Data.DataTable();
                                            da.Fill(dt);



                                        }
                                        catch (Exception ex)
                                        {
                                            obj.Add(dtdata.Rows[i]["NAME"].ToString());

                                        }
                                    }
                                }
                            }

                        }
                    }
                    System.Data.DataTable name = new System.Data.DataTable();
                    name.Columns.Add("NAME");
                    for (int i = 0; i < obj.Count; i++)
                    {
                        name.Rows.Add();
                        name.Rows[i]["NAME"] = obj[i].ToString();
                    }
                    dataGridView1.DataSource = name;

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
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btndown_Click(object sender, EventArgs e)
        {
            try
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM STUDENT_FORMAT ", sc);
                sc.Open();
                DataSet dt = new DataSet();
                da.Fill(dt);

                ExportDataSetToExcel(dt);


            }
            catch( Exception EX){}

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
