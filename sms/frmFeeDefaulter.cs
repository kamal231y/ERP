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
using MsgBoxRet;
using System.Net;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace sms
{
    public partial class frmFeeDefaulter : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;  
        string yaer;
        string hostel;
       string classname;
       SqlDataReader dr;
       public frmFeeDefaulter()
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
            
          
            
            
        }
        

            
        private void Cancil_form()
        {
            panelsearch.Visible = true;
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbclass.Text = "Select";
            cmbyear.Text = "Select";
            label2.Text = "";
                 
          
            
        }

        private void bindcmpname()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from company_t", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["com_name"].ToString();
                textBox27.Text = dt.Rows[0]["companytelno"].ToString();
            }

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
             string mnth;
             DateTime dt2 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);

             if (cmbyear.Text == "Select" || cmbyear.Text == "" || cmbyear.Text == "System.Data.DataRowView")
             {
                 yaer = "0";
             }
             else
             {
                 yaer = cmbyear.SelectedValue.ToString();
             }

             if (cmbclass.Text == "" || cmbclass.Text == "All")
             {
                 classname = "%";
             }
             else
             {
                 classname = cmbclass.SelectedValue.ToString() ;
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

             if (textBox2.Text == "" || textBox2.Text == " ")
             {
                 mnth = "%";
             }
             else
             {
                 mnth = textBox2.Text;
             }


             if (cmbclass.Text != "Select" && cmbclass.Text != "Select" && cmbyear.Text != "Select")
          { 
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_Defaulter ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@date", dt2.ToString("yyyy/MM/dd"));
            da.SelectCommand.Parameters.AddWithValue("@class", classname);
            da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);
            da.SelectCommand.Parameters.AddWithValue("@selected", chkpartpayment.Checked);
            da.SelectCommand.Parameters.AddWithValue("@mnth", mnth);

                 
            sc.Open();

            DataSet dt = new DataSet();
            da.Fill(dt);
            if (dt.Tables.Count> 0)
             {
                 dataGridView1.DataSource = dt.Tables[0];
                 dataGridView1.Columns["Sr_No"].Visible = false;
                 dataGridView1.Columns["tot"].Width = 20;
                 dataGridView1.Columns["Admission_no"].Width = 110;
                 dataGridView1.Columns["StudentName"].Width = 200;
                 dataGridView1.Columns["Class"].Width = 100;
                 dataGridView1.Columns["RollNo"].Width = 100;
                 dataGridView1.Columns["Father_Name"].Width = 150;
                 dataGridView1.Columns["Contact_No"].Width = 120;
                 dataGridView1.Columns["Balance_Dues"].Width =130;
                 
                 label2.Text = "Total Dues Balance: " + dt.Tables[1].Rows[0]["total_balance"].ToString();
             }
             
               else
              {
                  dataGridView1.DataSource = DBNull.Value;
                 

              }
             sc.Close();
          }
    }
      private void bindclass()
      {

          dataclass dc = new dataclass();
          SqlConnection sc = dc.con();
          SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_ID ", sc);
          DataSet ds = new DataSet();
          da.Fill(ds);
          DataRow row = ds.Tables[0].NewRow();
          row["Class"] = "All";
          row["SEC_ID"] = 0;
          ds.Tables[0].Rows.InsertAt(row, 0);

          cmbclass.DataSource = ds.Tables[0];
          cmbclass.DisplayMember = "Class";
          cmbclass.ValueMember = "SEC_ID";
      }

      private void bindYear()
                {

          dataclass dc = new dataclass();
          SqlConnection sc = dc.con();
          SqlDataAdapter da ;

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

          cmbyear.DataSource = ds.Tables[0];
          cmbyear.DisplayMember = "Year";
          cmbyear .ValueMember = "id";
      }    
        private void frmDuesbalance_Load(object sender, EventArgs e)
        {

            bindclass(); bindYear(); bindcmpname();
          
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
            DateTime dt2 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);

            if (dataGridView1.Rows.Count > 0)
            {
                cryFeeDefaulter.dt = dateTimePicker1.Value;
                cryFeeDefaulter.classname = classname;
                cryFeeDefaulter.yaer = yaer;
                cryFeeDefaulter.hostel = hostel;
                cryFeeDefaulter.month = "[ " + cmbyear.Text.Trim() + "- " + "2016" + " ]";
                cryFeeDefaulter.stu_id = dataGridView1.CurrentRow.Cells["Sr_No"].Value.ToString();
                cryFeeDefaulter obj = new cryFeeDefaulter();
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
            //showstudentreport();
        }

        private void rdsexboth_CheckedChanged(object sender, EventArgs e)
        {
            //showstudentreport();
        }

      
        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void cmbyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiohostler_CheckedChanged_1(object sender, EventArgs e)
        {
            //showstudentreport();
        }

        private void radioscholor_CheckedChanged_1(object sender, EventArgs e)
        {
            //showstudentreport();
        }

        private void rdbothhostel_CheckedChanged_1(object sender, EventArgs e)
        {
            //showstudentreport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
    {
        DateTime dt2 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);

        string month;
        if (dataGridView1.Rows.Count > 0)
        {
            cryFeeDefaulter.classname = classname;
            cryFeeDefaulter.dt = dateTimePicker1.Value;
            cryFeeDefaulter.hostel = hostel;
            cryFeeDefaulter.month = "[ " + cmbyear.Text.Trim() + "- " + "2016" + " ]";
            string stu_id = "0";
            for (int z = 0; z < dataGridView1.Rows.Count; z++)
            {
                string str11111 = dataGridView1.Rows[z].Cells["Selected"].Value.ToString();
                if (dataGridView1.Rows[z].Cells["Selected"].Value.ToString() == "True")
                    stu_id = stu_id + "," + dataGridView1.Rows[z].Cells["Sr_No"].Value.ToString();
            }
            cryFeeDefaulter.stu_id = stu_id;
            cryFeeDefaulter obj = new cryFeeDefaulter();
            obj.ShowDialog();

        }
        else
        {
            MsgBox.ShowBox("No Records Found to be Print....");
        }
        }

        private void btnmsg_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(txt_date.Value, System.Globalization.CultureInfo.InvariantCulture);

            if (dataGridView1.Rows.Count > 0)
            {
                sms1();
            }
             else
            {
                MsgBox.ShowBox("No Records Found to Send Message....");
            }
        }
        private void sms1()
        {
            DateTime dtpdate = Convert.ToDateTime(txt_date.Value, System.Globalization.CultureInfo.InvariantCulture);
          
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                         if (dataGridView1.Rows[i].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                            if (dataGridView1.Rows[i].Cells["Contact_No"].ToString() != "" || dataGridView1.Rows[i].Cells["Contact_No"].Value != null)
                                if (dataGridView1.Rows[i].Cells["Balance_Dues"].ToString() != "0")
                            {
                          
                            try
                            {
                                string msgtxt = "Dear Parents, Your Ward " + "  " + dataGridView1.Rows[i].Cells["StudentName"].Value.ToString() + ", " + dataGridView1.Rows[i].Cells["Class"].Value.ToString() + " , Adm No." + dataGridView1.Rows[i].Cells["Admission_no"].Value.ToString() + "  Dues Balance Till Month " + cmbyear.Text.Trim() + " is " + dataGridView1.Rows[i].Cells["Balance_Dues"].Value.ToString().Trim() + ". kindly pay before " + dtpdate.ToString("dd/MM/yyyy") + ". further enquiry  Contact " + textBox1.Text + "," + textBox27.Text.Trim().Replace(" ", "") + "* Thanks";
                                DAL.smsclass SMS = new sms.DAL.smsclass();
                                SMS.SMS(msgtxt, 0, dataGridView1.Rows[i].Cells["Contact_No"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[i].Cells["StudentName"].Value.ToString().Trim().Replace(" ", ""));           
                             
                            }


                            catch (Exception ex)
                            {
                               
                                MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                               
                            }
                        }
                                                                     }
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

        private void chkpartpayment_CheckStateChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void panelsearch_Paint(object sender, PaintEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt2 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);

            string mnth;
            if (cmbyear.Text == "Select" || cmbyear.Text == "" || cmbyear.Text == "System.Data.DataRowView")
            {
                yaer = "0";
            }
            else
            {
                yaer = cmbyear.SelectedValue.ToString();
            }

            if (cmbclass.Text == "" || cmbclass.Text == "All")
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
            if (textBox2.Text == "" || textBox2.Text == " ")
            {
                mnth = "%";
            }
            else
            {
                mnth = textBox2.Text;
            }



            if (cmbclass.Text != "Select" && cmbclass.Text != "Select" && cmbyear.Text != "Select" )
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("Find_Defaulter ", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@date", dt2.ToString("yyyy/MM/dd"));
                da.SelectCommand.Parameters.AddWithValue("@class", classname);
                da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);
                da.SelectCommand.Parameters.AddWithValue("@selected", chkpartpayment.Checked);
                da.SelectCommand.Parameters.AddWithValue("@mnth", mnth);


                sc.Open();

                DataSet dt = new DataSet();
                da.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                    
                            ExportDataSetToExcel(dt);
                        
                    
                }

                else
                {
                    //dataGridView1.DataSource = dt;

                    label2.Text = "Total No Of Record : " + "0";

                }
                sc.Close();
            }
        }

        private void textBox27_Leave(object sender, EventArgs e)
        {
           

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }


      


          

        }       
    }

