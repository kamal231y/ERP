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
    public partial class Uploadstudentfee : Form
    {
        SqlTransaction tran;
        SqlCommand cmd;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        public Uploadstudentfee()
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
          int School_id = 0;
          SqlConnection sc1 = dc.con();
          sc1.Open();
          SqlCommand cmd2 = new SqlCommand("Select * from company_t", sc1);
          SqlDataReader dr = cmd2.ExecuteReader();
          if (dr.Read())
              School_id = Convert.ToInt16(dr["School_id"].ToString());
          sc1.Close();
        
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
                          cmd2 = new SqlCommand();
                          cmd2.CommandType = CommandType.StoredProcedure;
                          cmd2.CommandText = "check_STUDENT_FEE_T";
                          cmd2.Connection = scweb;
                          cmd2.Parameters.AddWithValue("@SEF_STU_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["SEF_STU_ID"].Value));
                         
                          cmd2.Parameters.AddWithValue("@School_id", School_id);
                          cmd2.Transaction = tran;
                          cmd2.ExecuteNonQuery();

                      
                      }


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
                          cmd.Parameters.AddWithValue("@School_id", School_id);
                         
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
            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
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

        private void button3_Click(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from fee_t where webstatus=0  order by fee_id";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int School_id = 0;
            SqlConnection sc1 = dc.con();
            sc1.Open();
            SqlCommand cmd2 = new SqlCommand("Select * from company_t", sc1);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
                School_id = Convert.ToInt16(dr["School_id"].ToString());
            sc1.Close();

            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from FEE_T where webstatus=0 order by fee_id", sc);
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
                            cmd.CommandText = "Insert_FEE_T";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_feeTstatus";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@FEE_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["FEE_ID"].Value));
                            cmd.Parameters.AddWithValue("@FEE_DESC", dataGridView1.Rows[z].Cells["FEE_DESC"].Value);
                            cmd.Parameters.AddWithValue("@FEE_PERIOD", dataGridView1.Rows[z].Cells["FEE_PERIOD"].Value);
                            cmd.Parameters.AddWithValue("@FEE_REFUND", dataGridView1.Rows[z].Cells["FEE_REFUND"].Value);
                            cmd.Parameters.AddWithValue("@FEE_ACC_ID", dataGridView1.Rows[z].Cells["FEE_ACC_ID"].Value);
                            cmd.Parameters.AddWithValue("@FEE_TYPE", dataGridView1.Rows[z].Cells["FEE_TYPE"].Value);
                            cmd.Parameters.AddWithValue("@FEE_ALIAS", dataGridView1.Rows[z].Cells["FEE_ALIAS"].Value);
                            //cmd.Parameters.AddWithValue("@FEE_GROUP", dataGridView1.Rows[z].Cells["FEE_GROUP"].Value);
                            
                            cmd.Parameters.AddWithValue("@SCHOOL_ID", School_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@stu_id", (dataGridView1.Rows[z].Cells["FEE_ID"].Value));
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

        private void button5_Click(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from fee_detail_t where webstatus=0  ";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            int School_id = 0;
            SqlConnection sc1 = dc.con();
            sc1.Open();
            SqlCommand cmd2 = new SqlCommand("Select * from company_t", sc1);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
                School_id = Convert.ToInt16(dr["School_id"].ToString());
            sc1.Close();

            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from fee_detail_t where webstatus=0 order by fee_id", sc);
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
                            cmd.CommandText = "Insert_FEE_detail_T";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_feedetailTstatus";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@FEE_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["FEE_ID"].Value));
                            cmd.Parameters.AddWithValue("@APR", dataGridView1.Rows[z].Cells["APR"].Value);
                            cmd.Parameters.AddWithValue("@MAY", dataGridView1.Rows[z].Cells["MAY"].Value);
                            cmd.Parameters.AddWithValue("@JUN", dataGridView1.Rows[z].Cells["JUN"].Value);
                            cmd.Parameters.AddWithValue("@JUL", dataGridView1.Rows[z].Cells["JUL"].Value);
                            cmd.Parameters.AddWithValue("@AUG", dataGridView1.Rows[z].Cells["AUG"].Value);
                            cmd.Parameters.AddWithValue("@SEP", dataGridView1.Rows[z].Cells["SEP"].Value);
                            cmd.Parameters.AddWithValue("@OCT", dataGridView1.Rows[z].Cells["OCT"].Value);
                            cmd.Parameters.AddWithValue("@NOV", dataGridView1.Rows[z].Cells["NOV"].Value);
                            cmd.Parameters.AddWithValue("@DEC", dataGridView1.Rows[z].Cells["DEC"].Value);
                            cmd.Parameters.AddWithValue("@JAN", dataGridView1.Rows[z].Cells["JAN"].Value);
                            cmd.Parameters.AddWithValue("@FEB", dataGridView1.Rows[z].Cells["FEB"].Value);
                            cmd.Parameters.AddWithValue("@MAR", dataGridView1.Rows[z].Cells["MAR"].Value);
                            cmd.Parameters.AddWithValue("@APRn", dataGridView1.Rows[z].Cells["APRn"].Value);
                            cmd.Parameters.AddWithValue("@MAYn", dataGridView1.Rows[z].Cells["MAYn"].Value);
                            cmd.Parameters.AddWithValue("@JUNEn", dataGridView1.Rows[z].Cells["JUNEn"].Value);
                            //cmd.Parameters.AddWithValue("@FEE_GROUP", dataGridView1.Rows[z].Cells["FEE_GROUP"].Value);

                            cmd.Parameters.AddWithValue("@SCHOOL_ID", School_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@id", (dataGridView1.Rows[z].Cells["ID"].Value));
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
        }

       
    }

