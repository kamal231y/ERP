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
    public partial class frmSiblingDetail: Form
    {
       
        string yaer;
        string hostel;
       string classname;
       SqlDataReader dr;
       public frmSiblingDetail()
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
                 
          
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelsearch.Visible = true;
            Cancil_form();
            //dataGridView1.DataSource = null;

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
            classname = cmbclass.Text.ToString(); ;
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
             dataGridView1.Visible = true;
             System.Data.DataTable dt = new System.Data.DataTable();
             SqlDataAdapter da = new SqlDataAdapter("Get_Sibling_Detail", sc);
              da.SelectCommand.CommandType = CommandType.StoredProcedure;
              da.SelectCommand.Parameters.AddWithValue("@admNo", classname.Trim().Replace(" ", ""));
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
       

            sc.Close(); 
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string noofstu;
           
            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString(); ;
            }


            if (dataGridView1.Rows.Count > 0)
            {
                if (textBox2.Text == "")
                {
                    noofstu = "%";
                }
                else
                {
                    noofstu = textBox2.Text.ToString();
                }
                cryShowsiblingdetail.noofstu = noofstu;
                cryShowsiblingdetail obj = new cryShowsiblingdetail();                
                obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
               
         }


        private void frmGrDetail_Load(object sender, EventArgs e)
        {
              Bindclass(); 
        }
   
        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("select distinct stu_sibling_id,stu_sibling_id from student_t where stu_sibling_id not in ('')", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            cmbclass.DataSource = dt;
            cmbclass.DisplayMember = "stu_sibling_id";
            cmbclass.ValueMember = "stu_sibling_id";
            sc.Close();
            showstudentreport();
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

     

        private void btnexport_Click(object sender, EventArgs e)
        {
            //classname = cmbclass.Text.ToString(); ;
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();
            //dataGridView1.Visible = true;
            //System.Data.DataSet dt = new System.Data.DataSet();
            //SqlDataAdapter da = new SqlDataAdapter("Get_Sibling_Detail", sc);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.AddWithValue("@admNo", classname.Trim().Replace(" ", ""));
            //da.Fill(dt);
            //if (dt.Tables[0].Rows.Count > 0)
            //{
            //    ExportDataSetToExcel(dt);
            //}
            //else
            //{
            //    dataGridView1.DataSource = DBNull.Value;
            //    lblcount.Text = "Total No Of Record : " + "0";
            //}


            //sc.Close();
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            string str = "select stu_id,admno,student_name,fathername,class,PhONE from view_studentreport where STU_SIBLING_ID is not null and STU_SIBLING_ID!='' and stu_status='y' or admno in(select STU_SIBLING_ID from student_t) order by FatherName";
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

        private void cmbclass_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }
       
        }       
    }

