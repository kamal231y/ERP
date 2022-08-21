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

namespace sms
{
    public partial class frmFeeRecieptReport : Form
    {
       
        string yaer;
        string paid;
       string classname;
       SqlDataReader dr;
       public frmFeeRecieptReport()
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

            if (cmbyear.Text.Trim() == "APRIL")
                     yaer = "SFR_APRIL";
                 if (cmbyear.Text.Trim() == "MAY")
                     yaer = "SFR_MAY";
                 if (cmbyear.Text.Trim() == "JUNE")
                     yaer = "SFR_JUNE";
                 if (cmbyear.Text.Trim() == "JULY")
                     yaer = "SFR_JULY";
                 if (cmbyear.Text.Trim() == "AUGUST")
                     yaer = "SFR_AUGUST";
                 if (cmbyear.Text.Trim() == "SEPTEMBER")
                     yaer = "SFR_SEPTEMBER";
                 if (cmbyear.Text.Trim() == "OCTOBER")
                     yaer = "SFR_OCTOBER";
                 if (cmbyear.Text.Trim() == "NOVEMBER")
                     yaer = "SFR_NOVEMBER";
                 if (cmbyear.Text.Trim() == "DECEMBER")
                     yaer = "SFR_DECEMBER";
                 if (cmbyear.Text.Trim() == "JANUARY")
                     yaer = "SFR_JANUARY";
                 if (cmbyear.Text.Trim() == "FEBRUARY")
                     yaer = "SFR_FEBRUARY";
                 if (cmbyear.Text.Trim() == "MARCH")
                     yaer = "SFR_MARCH";
               
                  


             if (cmbclass.Text == "" || cmbclass.Text == "Select" )
             {
                 classname = "%";
             }
             else
             {
                 classname = cmbclass.SelectedValue.ToString() ;
             }
               

                  if (rbdnotpaid.Checked)
                 {
                     paid = "0";

                 }
                  else
                      paid = "%";




          if (cmbclass.Text != "Select" && cmbclass.Text != "System.Data.DataRowView" && cmbyear.Text != "Select" && cmbyear.Text != "System.Data.DataRowView")
          {
              SqlDataAdapter da;
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (paid == "0")
                da = new SqlDataAdapter("select * from View_StudentFeeStatus where " + yaer + " = 0 and STC_SEC_ID=" + classname + " ", sc);
            else
                da = new SqlDataAdapter("select * from View_StudentFeeStatus where " + yaer + " != 0 and STC_SEC_ID=" + classname + "  ", sc);

                  sc.Open();            
                    
                DataTable dt = new DataTable();
                da.Fill(dt);
             if(dt.Rows.Count>0)
             {
                 dataGridView1.DataSource = dt;
                 //dataGridView1.Columns["Sr_No"].Visible = false;
                 dataGridView1.Columns["Adm_No"].Width = 100;
                 dataGridView1.Columns["StudentName"].Width = 200;
                 dataGridView1.Columns["Class"].Width = 100;
                 dataGridView1.Columns["RollNo"].Width = 100;
                 dataGridView1.Columns["FatherName"].Width = 200;
                 dataGridView1.Columns["Address"].Width = 250;
                // dataGridView1.Columns["STU_MOBILE"].Width = 100;
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
          row["SEC_ID"] = 0;
          row["Class"] = "Select";

          ds.Tables[0].Rows.InsertAt(row, 0);

          cmbclass.DataSource = ds.Tables[0];
          cmbclass.DisplayMember = "Class";
          cmbclass.ValueMember = "SEC_ID";
      }

      private void bindYear()
                {

          dataclass dc = new dataclass();
          SqlConnection sc = dc.con();
          SqlDataAdapter da = new SqlDataAdapter(" select * from Year_T order by id ", sc);
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

            bindclass(); bindYear();
          
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                //cryShowAdmissionDetail.classname = classname;
                //cryShowAdmissionDetail.year = yaer;
                //cryShowAdmissionDetail.stutype1 = hostel;
                //cryShowAdmissionDetail obj = new cryShowAdmissionDetail();
                
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
                studentregistration.admno = dataGridView1.CurrentRow.Cells["Admission_no"].Value.ToString();
                studentregistration obj = new studentregistration();
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
            showstudentreport();
        }

        private void radioscholor_CheckedChanged_1(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothhostel_CheckedChanged_1(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            showstudentreport();
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
                    {
                        classname = "%";
                    }
                    else
                    {
                        classname = cmbclass.SelectedValue.ToString();
                    }

               if (rbdnotpaid.Checked)
               {
                   paid = "Not Paid";

               }
               else
                   paid = "Paid";

               string Month;
                    //cryFeeDetailReport.dt1 = dateTimePicker1.Value;
               cryFeeReceiptReport.yaer = yaer;
               cryFeeReceiptReport.Month = cmbyear.Text.Trim();
               cryFeeReceiptReport.classid = classname;
               cryFeeReceiptReport.hostel = paid;

               cryFeeReceiptReport obj = new cryFeeReceiptReport();
                    obj.ShowDialog();
             
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

      
       
        }       
    }

