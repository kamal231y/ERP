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
    public partial class UploadstudentfeeConsession : Form
    {
        SqlTransaction tran;
        SqlCommand cmd;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        public UploadstudentfeeConsession()
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
          SqlDataAdapter da = new SqlDataAdapter("select * from view_feedetail where  stu_status='Y' and webstatus=0 order by SEF_STU_ID", sc);
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
                          cmd.CommandText = "Insert_STUDENT_FEE_T";
                          cmd.Connection = scweb;
                          cmd1 = new SqlCommand();
                          cmd1.CommandType = CommandType.StoredProcedure;
                          cmd1.CommandText = "insert_feestatus";
                          cmd1.Connection = sc;
                          cmd.Parameters.AddWithValue("@SEF_STU_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["SEF_STU_ID"].Value));
                          cmd.Parameters.AddWithValue("@SEF_CLS_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["SEF_CLS_ID"].Value));
                          cmd.Parameters.AddWithValue("@SEF_FEE_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["SEF_FEE_ID"].Value));
                          cmd.Parameters.AddWithValue("@Amount", Convert.ToInt64(dataGridView1.Rows[z].Cells["SEF_FEE_AMOUNT"].Value));
                          cmd.Transaction = tran;
                          cmd.ExecuteNonQuery();
                          cmd1.Parameters.AddWithValue("@stu_id", (dataGridView1.Rows[z].Cells["SEF_STU_ID"].Value));
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
            int School_id = 0;
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd2 = new SqlCommand("Select * from company_t", sc);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
                School_id = Convert.ToInt16(dr["School_id"].ToString());
            sc.Close();

            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
      
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_FEE_EXEMPTED_T where webstatus=0  order by stu_id", sc);
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
                            cmd.CommandText = "insert_Student_FEE_Exemption";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusexmpt";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@STU_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["stu_id"].Value));
                            cmd.Parameters.AddWithValue("@fee_id", Convert.ToInt32(dataGridView1.Rows[z].Cells["fee_id"].Value));
                            cmd.Parameters.AddWithValue("@Exempted", (dataGridView1.Rows[z].Cells["EXEMPTION"].Value));
                            cmd.Parameters.AddWithValue("@School_id", School_id);
                            
                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@stu_id",Convert.ToInt16(dataGridView1.Rows[z].Cells["stu_id"].Value));
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
            str = "select * from view_feedetail where stu_status='Y' and webstatus=0 order by SEF_STU_ID";

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
            str = "select * from STUDENT_FEE_EXEMPTED_T where webstatus=0  order by stu_id";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }


        }
        }

       
    }

