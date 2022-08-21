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

namespace sms
{
    public partial class uploadData1: Form
    {
        SqlTransaction tran;
        SqlCommand cmd;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        public uploadData1()
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
            //bindstate();
            Cancil_form();
        }
        private void Cancil_form()
        {
            btnCreate.Enabled = true;
        }
      
        
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        public static byte[] ImageToByteArray(Image img, PictureBox pictureBoxCompanyLogo)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (pictureBoxCompanyLogo.Image != null)
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
        }
         
     private void btnCreate_Click(object sender, EventArgs e)   
      {         
          SqlCommand cmd1;
          SqlConnection scweb = dc.conweb();
          SqlConnection sc = dc.con();
          SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where  stu_status='y' order by stu_id", sc);
          DataTable dt = new DataTable();
          da.Fill(dt);
          if (dt.Rows.Count > 0)
              if (dataGridView1.Rows.Count > 0)
              {
                  scweb = dc.conweb();
                  scweb.Open();
                  sc.Open();
                  tran = scweb.BeginTransaction();
                  try
                  {

                      for (int z = 0; z < dataGridView1.Rows.Count; z++)
                      {
                          cmd = new SqlCommand();
                          cmd.CommandType = CommandType.StoredProcedure;
                          cmd.CommandText = "Insert_student_registration_web1";
                          cmd.Connection = scweb;
                          cmd1 = new SqlCommand();
                          cmd1.CommandType = CommandType.StoredProcedure;
                          cmd1.CommandText = "insert_webstatus";
                          cmd1.Connection = sc;
                          cmd.Parameters.AddWithValue("@STU_ID",Convert.ToInt16(dataGridView1.Rows[z].Cells["STU_ID"].Value));
                          cmd.Parameters.AddWithValue("@Roll", (dataGridView1.Rows[z].Cells["Roll"].Value));
                          cmd.Parameters.AddWithValue("@STU_SIBLING_ID",dataGridView1.Rows[z].Cells["STU_SIBLING_ID"].Value);
                          cmd.Parameters.AddWithValue("@Admission_No", (dataGridView1.Rows[z].Cells["AdmNo"].Value));
                          cmd.Parameters.AddWithValue("@Acc_No", (dataGridView1.Rows[z].Cells["AccNo"].Value));
                          //cmd.Parameters.AddWithValue("@Admission_date", Convert.ToDateTime(dataGridView1.Rows[z].Cells["Adm_Date"].Value));
                          cmd.Parameters.AddWithValue("@Firstname", (dataGridView1.Rows[z].Cells["Student_Name"].Value));
                          cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dataGridView1.Rows[z].Cells["DOB"].Value));
                          cmd.Parameters.AddWithValue("@Fathername", (dataGridView1.Rows[z].Cells["FatherName"].Value));
                          cmd.Parameters.AddWithValue("@Mothername", (dataGridView1.Rows[z].Cells["MotherName"].Value));

                          cmd.Parameters.AddWithValue("@adm_class", Convert.ToInt16(dataGridView1.Rows[z].Cells["STU_ADM_CLS_ID"].Value));
                          cmd.Parameters.AddWithValue("@Current_class", Convert.ToInt16(dataGridView1.Rows[z].Cells["STU_CLS_ID"].Value));
                          cmd.Parameters.AddWithValue("@Class", (dataGridView1.Rows[z].Cells["Class"].Value));
                          cmd.Parameters.AddWithValue("@Nationality ", (dataGridView1.Rows[z].Cells["STU_NATIONAL_ID"].Value));
                          cmd.Parameters.AddWithValue("@Caste ", (dataGridView1.Rows[z].Cells["stu_CASTE_id"].Value));
                          cmd.Parameters.AddWithValue("@Religion", (dataGridView1.Rows[z].Cells["stu_rlg_id"].Value));

                          cmd.Parameters.AddWithValue("@phone", (dataGridView1.Rows[z].Cells["Mother_MOB"].Value));
                          cmd.Parameters.AddWithValue("@Mobile", (dataGridView1.Rows[z].Cells["PhONE"].Value));
                          cmd.Parameters.AddWithValue("@sex", (dataGridView1.Rows[z].Cells["stu_sex"].Value));
                          cmd.Parameters.AddWithValue("@doa", Convert.ToDateTime(dataGridView1.Rows[z].Cells["Adm_Date"].Value));
                          cmd.Parameters.AddWithValue("@stu_status", 'Y');
                          cmd.Parameters.AddWithValue("@session", dataGridView1.Rows[z].Cells["STU_SESSION"].Value);
                          cmd.Parameters.AddWithValue("@email", dataGridView1.Rows[z].Cells["email1"].Value);
                          cmd.Parameters.AddWithValue("@STU_FEE_ATTACHED", Convert.ToInt16(dataGridView1.Rows[z].Cells["STU_FEE_ATTACHED"].Value));

                          cmd.Parameters.AddWithValue("@sec_cls_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["STC_SEC_ID"].Value));
                          cmd.Parameters.AddWithValue("@School_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["School_id"].Value));
                        
                         
                          cmd.Transaction = tran;
                          cmd.ExecuteNonQuery();
                          cmd1.Parameters.AddWithValue("@Admission_No", (dataGridView1.Rows[z].Cells["AdmNo"].Value));
                          cmd1.ExecuteNonQuery();
                      }
                      tran.Commit();
                      sc.Close();
                      scweb.Close();



                      Cancil_form();
                      MsgBox.ShowBox("Record uploaded  Successfully.....");




                  }

                  catch (Exception ex)
                  {
                      tran.Rollback();
                      sc.Close();
                      MessageBox.Show(ex.Message, "Warning....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }
                  finally
                  { }
              }
              else
              {
                  MessageBox.Show("No Record To upload .......");

              }
            }     
      
       private void btnCancel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_STOP_T where webstatus=0  order by ss_id", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                if (dataGridView1.Rows.Count > 0)
                {
                    scweb = dc.conweb();
                    scweb.Open();
                    sc.Open();
                    tran = scweb.BeginTransaction();
                    try
                    {

                        for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insert_StudentBusStop";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusstop";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@STU_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["ss_stu_id"].Value));
                            cmd.Parameters.AddWithValue("@busstop",Convert.ToInt32 (dataGridView1.Rows[z].Cells["ss_stop_id"].Value));
                            cmd.Parameters.AddWithValue("@vehicle", Convert.ToInt32(dataGridView1.Rows[z].Cells["ss_vh_id"].Value));
                            cmd.Parameters.AddWithValue("@TYPE", (dataGridView1.Rows[z].Cells["ss_type"].Value));
                           
                           
                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@stu_id",Convert.ToInt16(dataGridView1.Rows[z].Cells["ss_stu_id"].Value));
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        scweb.Close();



                        Cancil_form();
                        MsgBox.ShowBox("Record uploaded  Successfully.....");




                    }

                    catch (Exception ex)
                    {
                        tran.Rollback();
                        sc.Close();
                        MessageBox.Show(ex.Message, "Warning....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    finally
                    { }
                }
                else
                {
                    MessageBox.Show("No Record To upload .......");

                } 
        }        
            
      private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                
            }
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
           
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtcity_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str;            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from view_studentreport where  stu_status='y' and webstatus=0 order by stu_id";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from STUDENT_STOP_T where webstatus=0 order by ss_id";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void txtcityid_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblcount_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        }

       
    }

