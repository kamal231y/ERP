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
    public partial class frmAWRDetail : Form
    {
       
        string yaer;
        string hostel;
       string classname;
       SqlDataReader dr;
       public frmAWRDetail ()
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
        

            
        private void Cancil_form()
        {
            panelsearch.Visible = true;
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbclass.Text = "Select";
            cmbyear.Text = "Select";
                 
          
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelsearch.Visible = true;
            Cancil_form();
            dataGridView1.DataSource = null;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            showstudentreport();
        
        }
      private void showstudentreport()
         {
            
             if (cmbyear.Text == "Select" || cmbyear.Text == "0")
             {
                 yaer = "%";
             }
             else
             {
                 yaer = cmbyear.Text;
             }

            if(cmbclass.Text=="" || cmbclass.Text=="Select")
             {
                 classname = "%";
             }
             else
             {
                 classname = cmbclass.SelectedValue.ToString(); ;
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
        

            
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            string str = " select AdmNo,AccNo as CardNo,CAST(DOB AS DATE) AS  DOB,CAST(Adm_Date AS DATE) AS  Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and STU_HOSTLER like '" + hostel + "'and CAST( Year(Adm_Date) as varchar) like '" + yaer + "' ORDER BY Adm_Date,stu_id";
            if (str != "")
          {
              dataGridView1.Visible = true;
              SqlDataAdapter da = new SqlDataAdapter(str, sc);
              System.Data.DataTable dt = new System.Data.DataTable();
              da.Fill(dt);
              if (dt.Rows.Count > 0)
              {
                  dataGridView1.DataSource = dt;

                  label2.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
              }
              else
              {
                  dataGridView1.DataSource = DBNull.Value;
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

            if (cmbyear.Text == "Select" || cmbyear.Text == "0")
            {
                yaer = "%";
            }
            else
            {
                yaer = cmbyear.Text;
            }

            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString(); ;
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



            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            //string str = " select AdmNo,CONVERT(VARCHAR(10), Adm_Date, 103) as Date_of_Admission,Student_Name as Name_of_Student  ,MotherName as Mother_Name,FatherName,FATHER_OCP_DESC  as Occupation,(STU_PER_ADDR+' , '+CityName) as Permanent_and_Residential,AADHAR_CARD,CAT_DESC as Caste,NATION_DESC,RLG_DESC,Class,CONVERT(VARCHAR(10), DOB, 103) as DOB,SUPPORTING_DOC as Supporting_Documents_with_No_and_Date,Exam_Name as Name_of_the_School_from_Where_the_student_has_come,RollNo as  Class_in_which_previously_studying ,TC_REASON as Withdrawl,cast(TC_NO as varchar)+' , '+cast(cast(TC_ISSUE_DATE as date) as varchar) as No_and_Date_of_Transfer_certificate_Issued ,TC_GEN_CONDUCT as CONDUCT,Rec_sign as Signature_of_Recipient,Principal_Sign as Signature_of_Principal  from View_STUDENT_AWR_REPORT where STC_SEC_ID like '" + classname + "' and CAST( Year(Adm_Date) as varchar) like '" + yaer + "'  ORDER BY Adm_Date,stu_id";
            string str = " select AdmNo,AccNo as CardNo ,CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where  STC_SEC_ID like '" + classname + "' and CAST( Year(Adm_Date) as varchar) like '" + yaer + "'  ORDER BY Adm_Date,stu_id";
            if (str != "")
            {
                //,cast(cast(Adm_Date as date) as varchar)
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataSet  dt = new DataSet();
                da.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                   ExportDataSetToExcel(dt);
                }
                else
                {
                    dataGridView1.DataSource = DBNull.Value;
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
        private void frmGrDetail_Load(object sender, EventArgs e)
        {
            cmbyear.Items.Add("Select");
            for (int i = 2000; i < 2022; i++)
            {

                cmbyear.Items.Add(i);
            }


                Bindclass(); 
          
           // bindgridd();
        }

   
       
     

        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by SEC_ID ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbclass.DataSource = dt;
            cmbclass.DisplayMember = "class";
            cmbclass.ValueMember = "SEC_ID";
            sc.Close();
            showstudentreport();
        }
        
        private void bindAcc()
        {
            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select STU_ACC_NO as AccNo, STU_ID as sid from student_t  order by sid ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["AccNo"] = "0";
            row["sid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbyear.DataSource = ds.Tables[0];
            cmbyear.DisplayMember = "AccNo";
            cmbyear.ValueMember = "sid";
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
            
            if (dataGridView1.Rows.Count > 0)
            {
                frmAdmission.admno = dataGridView1.CurrentRow.Cells["AdmNo"].Value.ToString();
                //studentregistration obj = new studentregistration();
                frmAdmission obj = new frmAdmission();
                obj.ShowDialog();
            }
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

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cryshowstudentsecwise obj = new cryshowstudentsecwise();
            obj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cryshowstudentCLASSwise obj = new cryshowstudentCLASSwise();
            obj.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cryshowstudentcategory obj = new cryshowstudentcategory();
            obj.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cryshowstudentstrength obj = new cryshowstudentstrength();
            obj.ShowDialog();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            cryshowstudentAGEwise obj = new cryshowstudentAGEwise();
            obj.ShowDialog();
        }          
       
        }       
    }

