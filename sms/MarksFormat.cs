using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
    public partial class MarksFormat : Form
    {
       
        public MarksFormat()
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
            bindSubCategory(); bindSubCategory1(); bindSubCategory2();
            btn_create.Enabled = true;
            cmbClass.Text = "Select";
            cmbExam.Text = "Select";
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
        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EXAMINATION_T", sc);
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
        public void InitGrid()
        {
            LoadGrid();


        }
        private void LoadGrid()
        {
            string id;
            id = CMBCATEGORY.SelectedValue.ToString();
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("BIND_STUDENT_SUBJECT_MARKS_ENTRY_SUBJECT_WISE ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
            da.SelectCommand.Parameters.AddWithValue("@CATEGORY", id);
            da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(cmbExam.SelectedValue));
            sc.Open();
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                dataGridView1.Columns["STU_ID"].Visible = false;
                dataGridView1.Columns["Student_Name"].Width = 200;
                dataGridView1.Columns["fullMark"].Width = 150;
                dataGridView1.Columns["fullMark"].HeaderText = "FullMark";
                dataGridView1.Columns["PassMark"].Width = 150;
            }
        }
        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
          //  SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 and subsc_id in (48,78) order by SUBSC_DESC", sc);
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 and subsc_id in (48) order by SUBSC_DESC", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBCATEGORY.DataSource = ds.Tables[0];
            CMBCATEGORY.DisplayMember = "SUBSC_DESC";
            CMBCATEGORY.ValueMember = "SUBSC_ID";
        }
        private void bindSubCategory()
        {
            int id = 0;
            if (CMBCATEGORY.SelectedValue != null)
                id = Convert.ToInt32(CMBCATEGORY.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + "  order by SUBSC_DESC", sc);
       //     SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " and subsc_id not in (80) order by SUBSC_DESC", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSubjectCategory.DataSource = ds.Tables[0];
            cmbSubjectCategory.DisplayMember = "SUBSC_DESC";
            cmbSubjectCategory.ValueMember = "SUBSC_ID";
        }
        private void bindSubCategory1()
        {
            int id = 0;
            if (cmbSubjectCategory.SelectedValue != null)
                id = Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB1.DataSource = ds.Tables[0];
            CMBSUB1.DisplayMember = "SUBSC_DESC";
            CMBSUB1.ValueMember = "SUBSC_ID";
        }
        private void bindSubCategory2()
        {
            int id = 0;
            if (CMBSUB1.SelectedValue != null)
                id = Convert.ToInt32(CMBSUB1.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB2.DataSource = ds.Tables[0];
            CMBSUB2.DisplayMember = "SUBSC_DESC";
            CMBSUB2.ValueMember = "SUBSC_ID";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            string id = "0";
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());
            if (dataGridView1.Rows.Count > 0)
            {
                SqlTransaction tran;
                DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbClass.Text == "Select")
                {
                    MsgBox.ShowBox("class should not be blank");
                    return;
                }
                else if (cmbExam.Text == "Select")
                {
                    MsgBox.ShowBox("Exam should not be blank");
                    return;
                }
                else
                {

                    sc.Open();
                    tran = sc.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand();
                    //SqlCommand cmd1 = new SqlCommand("insert_SUBJECT_MARKS_T '" + cmbClass.SelectedValue + "','" + cmbExam.SelectedValue + "','" + Stuid + "','" + txt_month.Text + "','" + txt_nettotal.Text + "','" + txt_paid.Text + "','" + txt_prevdue.Text + "','" + txt_concession.Text + "','" + cmbmop.SelectedValue + "','" + txtDDNo.Text + "','" + cmbBankName.SelectedValue + "','" + Convert.ToDateTime(dt3) + "','" + cmb_accounts.SelectedValue + "','" + txt_coments.Text + "','" + txt_balance.Text + "','" + txt_totalfee.Text + "','" + Form1.GlobalLoginUserName.ToString() + "','" + sc.WorkstationId + "','" + Convert.ToDateTime(dt1) + "'", sc);
                    //SqlCommand cmd1 = new SqlCommand("select * from View_SUBJECT_MARKS_T  where SBM_EXAM_ID='" + cmbExam.SelectedValue + "' and SBM_CLS_ID='" + cmbClass.SelectedValue + "'", sc);
                    SqlDataReader dr;
                    cmd1.Transaction = tran;
                    try
                    {

                        //if (cmd1.ExecuteNonQuery() > 0)
                        //{

                        for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_Student_SUBJECT_MARKS";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@stu_id", dataGridView1.Rows[z].Cells["STU_ID"].Value);
                            cmd1.Parameters.AddWithValue("@SBM_CLS_ID", cmbClass.SelectedValue);
                            cmd1.Parameters.AddWithValue("@SBM_EXAM_ID", cmbExam.SelectedValue);
                            cmd1.Parameters.AddWithValue("@SBM_SUB_ID", id);
                            if (dataGridView1.Rows[z].Cells["Marks"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", 0);
                            else
                                cmd1.Parameters.AddWithValue("@SBM_MAX_MARK", dataGridView1.Rows[z].Cells["Marks"].Value);
                            cmd1.Parameters.AddWithValue("@FullMarks", dataGridView1.Rows[z].Cells["fullMark"].Value);
                           cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();
                            
                        }
                        //for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        //{
                        //    SqlCommand cmd2 = new SqlCommand();
                        //    cmd2.CommandType = CommandType.StoredProcedure;
                        //    cmd2.Connection = sc;
                        //    cmd2.CommandText = "UPDATE_STUDENT_SUBJECT_MARKS_T";
                        //    cmd2.Parameters.AddWithValue("@stu_id", Convert.ToInt32(dataGridView1.Rows[z].Cells["STU_ID"].Value));
                        //    cmd2.Parameters.AddWithValue("@CLASS", cmbClass.SelectedValue);
                        //    cmd2.Parameters.AddWithValue("@CATEGORY", id);
                        //    cmd2.Parameters.AddWithValue("@EXAM", cmbExam.SelectedValue);
                        //    cmd2.Transaction = tran;
                        //    cmd2.ExecuteNonQuery();
                        //}
                        MsgBox.ShowBox("Subject Marks  Created...");
                        tran.Commit();
                        LoadGrid();

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MsgBox.ShowBox("Subject Marks not Created...");
                    }
                    finally
                    {
                        sc.Close();
                    }


                }
            }
            else
            {
                MsgBox.ShowBox("No Record To Save........");
            }
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
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (cmbClass.Text == "Select")
            {
                MsgBox.ShowBox("You Must Select Class ...!!!");
                return;
            }
            if (cmbExam.Text == "Select")
            {
                MsgBox.ShowBox("You Must select Examination...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_RESULT_T_GradingScale_CoSolatic '" + txtGradeid.Text + "','2','" + cmbExam.Text + "'", sc);

                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Grade Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Grade Name already exist");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
        }
        //private void SubjectMarks_Load(object sender, EventArgs e)
        //{
        //    bindclass();
        //    bindExam(); bindCategory();
        //}

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
            bindSubCategory1();
            LoadGrid();
        }

        private void CMBSUB1_Leave(object sender, EventArgs e)
        {
            bindSubCategory2();
            LoadGrid();
        }

        private void CMBSUB2_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void CMBCATEGORY_Leave(object sender, EventArgs e)
        {
            bindSubCategory();
            LoadGrid();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string id = "0";
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());
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
                            DateTime newdate; dataclass dc = new dataclass();
                            SqlConnection sc = dc.con();
                            if (dtdata.Rows.Count > 0)
                            {
                                int count = 0; int marks = 0;
                                for (int i = 0; i < dtdata.Rows.Count; i++)
                                {
                                    if (dtdata.Rows[i]["Roll"].ToString() != "")
                                    {

                                        try
                                        {

                                            SqlDataAdapter da;
                                            da = new SqlDataAdapter("UPLOADMARKS_EXCEL_SHEET", sc);
                                            da.SelectCommand.CommandTimeout = 300;
                                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            if (dtdata.Rows[i]["Marks"].ToString() == "")
                                                marks = 0;
                                            else
                                                marks = Convert.ToInt32(dtdata.Rows[i]["Marks"].ToString());
                                            //SqlCommand CMD = new SqlCommand("UPLOADMARKS_EXCEL_SHEET " + dtdata.Rows[i]["Roll"].ToString() + "," + cmbClass.SelectedValue + "," + cmbExam.SelectedValue + "," + id + "," + marks + "", sc);
                                            da.SelectCommand.Parameters.AddWithValue("@ROLL", dtdata.Rows[i]["Roll"].ToString());
                                            da.SelectCommand.Parameters.AddWithValue("@SBM_CLS_ID", cmbClass.SelectedValue);
                                            da.SelectCommand.Parameters.AddWithValue("@SBM_EXAM_ID", cmbExam.SelectedValue);
                                            da.SelectCommand.Parameters.AddWithValue("@SBM_SUB_ID", id);
                                            da.SelectCommand.Parameters.AddWithValue("@SBM_MAX_MARK", marks);



                                            System.Data.DataTable dt = new System.Data.DataTable();
                                            da.Fill(dt);
                                            obj.Add(dtdata.Rows[i]["Roll"].ToString());


                                        }

                                        catch (Exception ex)
                                        {

                                        }
                                       
                                    }
                                }
                                LoadGrid();
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
            string id = "48";
            if (cmbSubjectCategory.SelectedValue != null)
                if (Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString()) > 0)
                    id = (cmbSubjectCategory.SelectedValue.ToString());
            if (CMBSUB1.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB1.SelectedValue.ToString()) > 0)
                    id = (CMBSUB1.SelectedValue.ToString());
            if (CMBSUB2.SelectedValue != null)
                if (Convert.ToInt32(CMBSUB2.SelectedValue.ToString()) > 0)
                    id = (CMBSUB2.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select sec_id,sbm_exam_id,sbm_sub_id,stu_id,admno,student_name,sbm_min_mark,sbm_max_mark,'' as obtainmarks from  View_marks_format where sec_id='" + cmbClass.SelectedValue.ToString() + "' and sbm_exam_id='" + cmbExam.SelectedValue.ToString() + "' and sbm_sub_id='" + cmbSubjectCategory.SelectedValue.ToString() + "'", sc);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.AddWithValue("@CLASS", Convert.ToInt32(cmbClass.SelectedValue));
            //da.SelectCommand.Parameters.AddWithValue("@CATEGORY", id);
            //da.SelectCommand.Parameters.AddWithValue("@EXAM", Convert.ToInt32(cmbExam.SelectedValue));
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

        private void MarksFormat_Load(object sender, EventArgs e)
        {
            bindclass();
            bindExam(); bindCategory();
        }
       
    }






   
}
