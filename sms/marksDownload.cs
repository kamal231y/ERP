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
    public partial class marksDownload : Form
    {
      SqlTransaction tran;
       // SqlCommand cmd;
     
        SqlCommand cmd1;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        int cont;
        string school_id;
        public marksDownload()
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
    



        private void button1_Click(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.connWeb();
            str = "select top(500)* from marks_School where school_id='" + school_id + "' and status='0'";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void lblcount_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlConnection scweb = dc.con();
            SqlConnection sc = dc.conweb();
            SqlDataAdapter da = new SqlDataAdapter("select top(500)* from marks_School where school_id='" + school_id + "' and status='0' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                if (dataGridView1.Rows.Count > 0)
                {
                    cont = dt.Rows.Count;
                    scweb = dc.con();
                    scweb.Open();
                    sc.Open();
                    tran = scweb.BeginTransaction();
                    try
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = cont;
                        progressBar1.Step = 1;
                        progressBar1.Value = 0;
                        for (int z = 0; z < dataGridView1.Rows.Count; z++)
                        {
                            progressBar1.Value = z;
                           // cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusMARK";
                            cmd1.Connection = sc;
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "insert_Student_SUBJECT_MARKSweb";
                            cmd.Connection = scweb;
                            cmd.Parameters.AddWithValue("@stu_id", dataGridView1.Rows[z].Cells["sbm_stu_id"].Value);
                            cmd.Parameters.AddWithValue("@SBM_CLS_ID", dataGridView1.Rows[z].Cells["sbm_cls_id"].Value);
                            cmd.Parameters.AddWithValue("@SBM_EXAM_ID", dataGridView1.Rows[z].Cells["sbm_exam_id"].Value);
                            cmd.Parameters.AddWithValue("@SBM_SUB_ID", dataGridView1.Rows[z].Cells["sbm_sub_id"].Value);
                            if (dataGridView1.Rows[z].Cells["sbm_max_mark"].Value.ToString() == "")
                                cmd.Parameters.AddWithValue("@SBM_MAX_MARK", 0);
                            else
                                cmd.Parameters.AddWithValue("@SBM_MAX_MARK", dataGridView1.Rows[z].Cells["sbm_max_mark"].Value);
                            cmd.Parameters.AddWithValue("@FullMarks", dataGridView1.Rows[z].Cells["full_marks"].Value);
                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();



                            cmd1.Parameters.AddWithValue("@SAT_mark_ID", Convert.ToInt64(dataGridView1.Rows[z].Cells["markid"].Value));
                            cmd1.Parameters.AddWithValue("@school_id", school_id);
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        scweb.Close();



                        //Cancil_form();
                        MsgBox.ShowBox("Record Download Successfully.....");
                        progressBar1.Visible = false;



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

        private void txtcityid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void marksDownload_Load(object sender, EventArgs e)
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

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
