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
    public partial class uploadbusstop: Form
    {
        SqlTransaction tran;
        SqlCommand cmd;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        String school_id;
        public uploadbusstop()
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
            BindCompany();
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


        private void BindCompany()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from COMPANY_T", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                school_id = " " + dt.Rows[0]["school_id"].ToString();
            }
            else
            {
                school_id = " ";
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
                            cmd.Parameters.AddWithValue("@school_id", school_id);
                           
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
            str = "select * from view_studentreport where webstatus=0 and stu_status='y' order by stu_id";

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

        private void button4_Click(object sender, EventArgs e)
        {

            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from BUS_STOP_T where webstatus=0 order by stop_id";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from bus_STOP_T where webstatus=0 ", sc);
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
                            cmd.CommandText = "insert_StudentBusStopfee";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusstopbus";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@STOP_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["STOP_ID"].Value));
                            cmd.Parameters.AddWithValue("@STOP_DESC",dataGridView1.Rows[z].Cells["STOP_DESC"].Value);
                            cmd.Parameters.AddWithValue("@STOP_ROUTE", dataGridView1.Rows[z].Cells["STOP_ROUTE"].Value);
                            cmd.Parameters.AddWithValue("@STOP_KM", (dataGridView1.Rows[z].Cells["STOP_KM"].Value));

                            cmd.Parameters.AddWithValue("@STOP_FARE", dataGridView1.Rows[z].Cells["STOP_FARE"].Value);
                            cmd.Parameters.AddWithValue("@INTIME", dataGridView1.Rows[z].Cells["INTIME"].Value);
                            cmd.Parameters.AddWithValue("@Outtime",dataGridView1.Rows[z].Cells["Outtime"].Value);
                            cmd.Parameters.AddWithValue("@Return_arrival", (dataGridView1.Rows[z].Cells["Return_arrival"].Value));
                            cmd.Parameters.AddWithValue("@Return_depart", (dataGridView1.Rows[z].Cells["Return_depart"].Value));
                           
                            cmd.Parameters.AddWithValue("@schl_id", school_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@stu_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["STOP_ID"].Value));
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

