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
    public partial class frmDemandRegisterReport : Form
    {
       
        string yaer;
        string hostel;
       string classname;
       string MONTH;
       SqlDataReader dr;
       public frmDemandRegisterReport()
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

             if (cmbyear.Text == "Select" || cmbyear.Text == "" || cmbyear.Text == "System.Data.DataRowView")
             {
                 yaer = "0";
             }
             else
             {
                 yaer = cmbyear.SelectedValue.ToString();
             }

             if (cmbclass.Text == "" || cmbclass.Text == "Select" || cmbclass.Text == "System.Data.DataRowView")
             {
                 classname = "0";
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


           
          { 
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_DuesBalance ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FRC_MONTH",Convert.ToInt32( yaer));
            da.SelectCommand.Parameters.AddWithValue("@class", classname);
            da.SelectCommand.Parameters.AddWithValue("@hosteler", hostel);
            da.SelectCommand.Parameters.AddWithValue("@selected", true);
            da.SelectCommand.Parameters.AddWithValue("@greater", true);


            sc.Open();
                    
                DataTable dt = new DataTable();
                da.Fill(dt);
          
                 dataGridView1.DataSource = dt;
                 dataGridView1.Columns["Sr_No"].Visible = false;
                 dataGridView1.Columns["Admission_no"].Width = 110;
                 dataGridView1.Columns["StudentName"].Width = 200;
                 dataGridView1.Columns["Class"].Width = 100;
                 dataGridView1.Columns["RollNo"].Width = 100;
                 dataGridView1.Columns["Father_Name"].Width = 200;
                 dataGridView1.Columns["Contact_No"].Width = 120;
                 dataGridView1.Columns["Balance_Dues"].Width =130;
            
             
            
             sc.Close();
          }
    }
      private void bindclass()
      {

          dataclass dc = new dataclass();
          SqlConnection sc = dc.con();
          SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + ' ' + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_ID ", sc);
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

       
       


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (dataGridView1.Rows.Count > 0)
            {
                studentregistration.admno = dataGridView1.CurrentRow.Cells["Admission_no"].Value.ToString();
                studentregistration obj = new studentregistration();
                obj.ShowDialog();
            }
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

       

      
        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void cmbyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

       

        private void radiohostler_CheckedChanged_2(object sender, EventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string classnameS;
            cryDemandRegisterReport.classname = classname;
              cryDemandRegisterReport.classnameS = cmbclass.Text;
              cryDemandRegisterReport.yaer = yaer;
              cryDemandRegisterReport.hostel = hostel;
                cryDemandRegisterReport.MONTH = cmbyear.Text.Trim();
              
                if (hostel == "%")
                {

                    cryDemandRegisterReport.hostelp = "Day Scholar And Hostlar";
                }
                cryDemandRegisterReport obj = new cryDemandRegisterReport();
               // obj.MdiParent =Form1.;
                obj.ShowDialog();

            }

        private void btnexport_Click(object sender, EventArgs e)
        {

        }
           
       

     
       
        }       
    }

