using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
    public partial class attendanceDownload : Form
    {
        SqlTransaction tran;
        SqlCommand cmd1;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        string school_id;
        public attendanceDownload()
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
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlConnection scweb = dc.con();
            SqlConnection sc = dc.conweb();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_ATTENDANCE_T where school_id='" + school_id + "' and d_status='0' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                if (dataGridView1.Rows.Count > 0)
                {
                    scweb = dc.con();
                    scweb.Open();
                    sc.Open();
                    tran = scweb.BeginTransaction();
                    try
                    {

                        for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "DOWNLOAD_ATTENDANCE_SOFT";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusATT";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@SAT_STU_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["SAT_STU_ID"].Value));
                            cmd.Parameters.AddWithValue("@ATT_TYPE", (dataGridView1.Rows[z].Cells["ATT_TYPE"].Value));
                            cmd.Parameters.AddWithValue("@SAT_DATE", Convert.ToDateTime(dataGridView1.Rows[z].Cells["SAT_DATE"].Value));
                            cmd.Parameters.AddWithValue("@DAY", Convert.ToInt16(dataGridView1.Rows[z].Cells["DAY"].Value));
                            cmd.Parameters.AddWithValue("@MONTH", Convert.ToInt16(dataGridView1.Rows[z].Cells["MONTH"].Value));
                            cmd.Parameters.AddWithValue("@YEAR", Convert.ToInt16(dataGridView1.Rows[z].Cells["YEAR"].Value));
                            cmd.Parameters.AddWithValue("@USER_ID", (dataGridView1.Rows[z].Cells["USER_ID"].Value));
                            //cmd.Parameters.AddWithValue("@Admission_date", Convert.ToDateTime(dataGridView1.Rows[z].Cells["Adm_Date"].Value));
                          
                           
                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@SAT_ID", Convert.ToInt64(dataGridView1.Rows[z].Cells["SAT_ID"].Value));
                            cmd1.Parameters.AddWithValue("@school_id", school_id);
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        scweb.Close();



                        //Cancil_form();
                        MsgBox.ShowBox("Record Download Successfully.....");




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

        private void button1_Click(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.connWeb();
            str = "select * from STUDENT_ATTENDANCE_T where school_id='"+school_id+"' and d_status='0' ";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }
        }

        private void attendanceDownload_Load(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter adap = new SqlDataAdapter("select school_id from company_t", sc);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                school_id = dt.Rows[0][0].ToString();

            }
          
        }
    }
}
