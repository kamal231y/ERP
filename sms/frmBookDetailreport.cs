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
    public partial class frmBookDetailreport : Form
    {
        
        string brcode;
        string isbn;
        string booktitle;
        string sub;
       string cls;
       string lang;
       string cat;
       string vol; 
       string pub;      
       SqlDataReader dr;
       public frmBookDetailreport()
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
            panelsearch.Visible = true;
            cmbclass.Text = "Select";
            cmb_publisher.Text = "Select";
            cmb_language.Text = "Select";
            cmb_subject.Text = "Select";
            cmbvlm.Text = "Select";
            txtisbn.Text = "";
            txtbrcode.Text = "";
                       
        }            
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelsearch.Visible = true;
            Cancil_form();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CLASS_T order by cls_sequence ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["cls_desc"] = "All";
            row["cls_id"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbclass.DataSource = dt;
            cmbclass.DisplayMember = "cls_desc";
            cmbclass.ValueMember = "cls_id";

        }
        public void bindlanguage()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from LANGUAGE_T order by LANG_DESC", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["LANG_DESC"] = "Select";
            row["LANG_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_language.DataSource = dt;
            cmb_language.DisplayMember = "LANG_DESC";
            cmb_language.ValueMember = "LANG_ID";
        }

        private void Bindvolume()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select volume from BMaster_T group by volume order by volume", sc);
            System.Data.DataTable dt2 = new System.Data.DataTable();
            da.Fill(dt2);
            DataRow row1 = dt2.NewRow();
            row1["volume"] = "Select";
            row1["volume"] = 0;
            dt2.Rows.InsertAt(row1, 0);
            cmbvlm.DataSource = dt2;
            cmbvlm.DisplayMember = "volume";
            cmbvlm.ValueMember = "volume";
        }
        private void bindsubject()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Select * from SUBJECT_T order by sub_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["sub_DESC"] = "Select";
            row["sub_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_subject.DataSource = ds.Tables[0];
            cmb_subject.DisplayMember = "sub_DESC";
            cmb_subject.ValueMember = "sub_ID";
        }
        private void bindpublisher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Public_T ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Publication_NAME"] = "Select";
            row["Publication_Id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_publisher.DataSource = ds.Tables[0];
            cmb_publisher.DisplayMember = "Publication_NAME";
            cmb_publisher.ValueMember = "Publication_Id";

        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
            showstudentreport();
        
        }
      private void showstudentreport()
         {
             if (txtbrcode.Text == "")
             {
                 brcode  = "%";
             }
             else
             {
                 brcode = txtbrcode.Text;
             }
             if (txtisbn.Text == "")
             {
                 isbn = "%";
             }
             else
             {
                 isbn = txtisbn.Text;
             }
             if (txtbook.Text == "")
             {
                 booktitle = "%";
             }
             else
             {
                 booktitle = txtbook.Text;
             }
             if (cmbclass.Text == "All" || cmbclass.Text == "0" || cmbclass.Text == "System.Data.DataRowView")
             {
                 cls = "%";
             }
             else
             {
                 cls = cmbclass.SelectedValue.ToString();
             }
             if (cmb_subject.Text == "Select" || cmb_subject.Text == "0" || cmb_subject.Text == "System.Data.DataRowView")
             {
                 sub = "%";
             }
             else
             {
                 sub = cmb_subject.SelectedValue.ToString();
             }
             if (cmbvlm.Text == "Select" || cmbvlm.Text == "0" || cmbvlm.Text == "System.Data.DataRowView")
             {
                 vol = "%";
             }
             else
             {
                 vol = cmbvlm.Text;
             }
            
             if (cmb_publisher.Text == "Select" || cmb_publisher.Text == "0" || cmb_publisher.Text == "System.Data.DataRowView")
             {
                 pub = "%";
             }
             else
             {
                 pub = cmb_publisher.SelectedValue.ToString();
             }
            
          
             if (cmb_language.Text == "Select" || cmb_language.Text == "" || cmb_language.Text == "System.Data.DataRowView")
             {
                 lang = "%";
             }
             else
             {
                 lang = cmb_language.SelectedValue.ToString();
             }
             if (cmbcat.Text == "Select" || cmbcat.Text == "" || cmbcat.Text == "System.Data.DataRowView")
             {
                 cat = "%";
             }
             else
             {
                 cat = cmbcat.SelectedValue.ToString();
             }      
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            if (txtisbn.Text == "" || txtisbn.Text == "Select")
                str = " select * from view_bmaster where barcode_no like '" + brcode + "'  and  isbn_no like '" + isbn + "%' and  book_title like '" + booktitle + "%'  and (cls_id like '" + cls + "' or cls_id=0) and subject_name like '" + sub + "'  and language like '" + lang + "'  and p_name like '" + pub + "' and category like '" + cat + "'ORDER BY sub_desc desc";
           else
                str = " select * from view_bmaster where barcode_no like '" + brcode + "'  and isbn_no like '" + isbn + "' and book_title like '" + booktitle + "%' and  (cls_id like '" + cls + "' or cls_id=0) and  subject_name like '" + sub + "'  and language like '" + lang + "' and p_name like '" + pub + "'  and category like '" + cat + "'  ORDER BY sub_desc";
          if(str!="")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                    dataGridView1.DataSource = dt;
                    lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    //dataGridView1.Columns["AdmNo"].Width = 100;
                    //dataGridView1.Columns["cardno"].Width = 80;
                    //dataGridView1.Columns["DOB"].Width = 75;
                    //dataGridView1.Columns["Adm_Date"].Width = 80;
                    //dataGridView1.Columns["Student_Name"].Width = 170;
                    //dataGridView1.Columns["FatherName"].Width = 150;
                    //dataGridView1.Columns["MotherName"].Width = 150;
                    //dataGridView1.Columns["Sex"].Width = 50;
                    //dataGridView1.Columns["Class"].Width = 70;
                    //dataGridView1.Columns["Roll"].Width = 50;
                    //dataGridView1.Columns["Address"].Width = 160;
                    //dataGridView1.Columns["Phone"].Width = 100;
                //}
                //else
                {
                    dataGridView1.DataSource = dt;
                    lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    //MsgBox.ShowBox("No Records Found....");

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
            if (dataGridView1.Rows.Count > 0)
            {
                //cryshowstudentdetail.brcode = brcode;
                //cryshowstudentdetail.isbn = isbn;
                //cryshowstudentdetail.booktitle = booktitle;
                //cryshowstudentdetail.sub = sub;
                //cryshowstudentdetail.cls = cls;

                //cryshowstudentdetail.lang = lang;
                //cryshowstudentdetail.cat = cat;
                //cryshowstudentdetail.vol = vol;
                //cryshowstudentdetail.pub = pub;                
                //cryshowstudentdetail obj = new cryshowstudentdetail();
                
                //obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
               
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

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void chkisCurrent_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbincome_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void panelsearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void cmbsiblingclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbcategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
           
            if (txtbrcode.Text == "")
             {
                 brcode  = "%";
             }
             else
             {
                 brcode = txtbrcode.Text;
             }
             if (txtisbn.Text == "")
             {
                 isbn = "%";
             }
             else
             {
                 isbn = txtisbn.Text;
             }
             if (txtbook.Text == "")
             {
                 booktitle = "%";
             }
             else
             {
                 booktitle = txtbook.Text;
             }
             if (cmbclass.Text == "All" || cmbclass.Text == "0" || cmbclass.Text == "System.Data.DataRowView")
             {
                 cls = "%";
             }
             else
             {
                 cls = cmbclass.SelectedValue.ToString();
             }
             if (cmb_subject.Text == "Select" || cmb_subject.Text == "0" || cmb_subject.Text == "System.Data.DataRowView")
             {
                 sub = "%";
             }
             else
             {
                 sub = cmb_subject.SelectedValue.ToString();
             }
             if (cmbvlm.Text == "Select" || cmbvlm.Text == "0" || cmbvlm.Text == "System.Data.DataRowView")
             {
                 vol = "%";
             }
             else
             {
                 vol = cmbvlm.Text;
             }
            
             if (cmb_publisher.Text == "Select" || cmb_publisher.Text == "0" || cmb_publisher.Text == "System.Data.DataRowView")
             {
                 pub = "%";
             }
             else
             {
                 pub = cmb_publisher.SelectedValue.ToString();
             }
            
          
             if (cmb_language.Text == "Select" || cmb_language.Text == "" || cmb_language.Text == "System.Data.DataRowView")
             {
                 lang = "%";
             }
             else
             {
                 lang = cmb_language.SelectedValue.ToString();
             }
             if (cmbcat.Text == "Select" || cmbcat.Text == "" || cmbcat.Text == "System.Data.DataRowView")
             {
                 cat = "%";
             }
             else
             {
                 cat = cmbcat.SelectedValue.ToString();
             }      
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            if (txtisbn.Text == "" || txtisbn.Text == "Select")
                str = " select * from view_bmaster where barcode_no like '" + brcode + "'  and  isbn_no like '" + isbn + "%' and  book_title like '" + booktitle + "%'  and cls_id like '" + cls + "' and subject_name like '" + sub + "'  and language like '" + lang + "'  and p_name like '" + pub + "' and category like '" + cat + "'ORDER BY sub_desc desc";
           else
                str = " select * from view_bmaster where barcode_no like '" + brcode + "'  and isbn_no like '" + isbn + "' and book_title like '" + booktitle + "%' and  cls_id like '" + cls + "' and  subject_name like '" + sub + "'  and language like '" + lang + "' and p_name like '" + pub + "'  and category like '" + cat + "'  ORDER BY sub_desc";
          if(str!="")
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
                    dataGridView1.DataSource = dt;
                    lblcount.Text = "Total No Of Record : " + "0";
                    //MsgBox.ShowBox("No Records Found....");

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

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnfee_Click(object sender, EventArgs e)
        {
            if (!Form1.board)
            {
               
            }
            else
            {
                
            }
        }

        private void btnattend_Click(object sender, EventArgs e)
        {
           
        }

        private void btncert_Click(object sender, EventArgs e)
        {
            
        }

        private void btnexam_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbtrans_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void frmBookDetailreport_Load(object sender, EventArgs e)
        {
            Bindclass(); bindlanguage(); Bindvolume(); bindsubject(); bindpublisher();
       
        }

        private void txtisbn_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmb_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbvlm_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }
        }       
    }

