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
    public partial class frmchequebonus : Form
    {
        // string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmchequebonus()
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
            cancel_form();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtp11 = Convert.ToDateTime(dtp1.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
           
                    sc.Open();
                    SqlTransaction tran;
                    tran = sc.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand();                    
                    SqlDataReader dr;
                    cmd1.Transaction = tran;
                    try
                    {
 
                        for (int z = 0; z < dataGridView2.Rows.Count; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "Insert_tblchequebounce";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@CBdate", dtp11.ToString("MM/dd/yyyy"));
                            cmd1.Parameters.AddWithValue("@CBNO",txtamt.Text );
                            cmd1.Parameters.AddWithValue("@REC", dataGridView2.Rows[z].Cells["FRC_NO"].Value);
                            cmd1.Parameters.AddWithValue("@STU_ID", dataGridView2.Rows[z].Cells["FRC_STU_ID"].Value);
                            cmd1.Parameters.AddWithValue("@AMOUNT", dataGridView2.Rows[z].Cells["FRC_PAID_AMT"].Value);
                            cmd1.Parameters.AddWithValue("@Userid",sms.Form1.GlobalLoginUserName.ToString());
                            cmd1.Parameters.AddWithValue("@Cdate", dtp11.ToString("MM/dd/yyyy"));
                            cmd1.Parameters.AddWithValue("@fine", Convert.ToDecimal(textBox3.Text));
                            cmd1.Parameters.AddWithValue("@admno", dataGridView2.Rows[z].Cells["STU_ADM_NO"].Value);
                            cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();
                        }                       
                        tran.Commit();
                        DialogResult dialogResult = MessageBox.Show("Are You Sure To Send SMS  !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                           sms1();
                            
                        }
                        else
                        {
                            
                            MsgBox.ShowBox("Record Created Successfully.....");
                            InitGrid();
                        }

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                    }
                    
      
            
        }
        private void sms1()
        {
                DateTime dtpdate = Convert.ToDateTime(dtp1.Value, System.Globalization.CultureInfo.InvariantCulture);
                //dataclass dc = new dataclass();
                //SqlConnection sc = dc.con();
                //SqlDataAdapter da = new SqlDataAdapter("select stu_adm_no,Student_Name, Class,stc_roll_no,Phone from View_ReceiptSummaryRpt where FRC_DDNO  like '" + txtamt.Text.Trim() + "' and status=''", sc);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells["STU_PHONE"].ToString() != "" || dataGridView2.Rows[i].Cells["STU_PHONE"].ToString() != null)
                    {

                        try
                        {
                            //decimal value = Convert.ToDecimal(dataGridView2.Rows[i].Cells["FRC_PAID_AMT"].Value.ToString() != "");
                            int n = Convert.ToInt32(dataGridView2.Rows[i].Cells["FRC_PAID_AMT"].Value);
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            string msgtxt = "D/P of" + "  " + dataGridView2.Rows[i].Cells["STU_FIRST_NAME"].Value.ToString() + ",Class " + dataGridView2.Rows[i].Cells["Class"].Value.ToString() + " , ID. No." + dataGridView2.Rows[i].Cells["stu_adm_no"].Value.ToString() + ", Fee Amount of Rs. " + n + " has been deposited on " + dtpdate.ToString("dd/MM/yyyy") + ".Thank You.Regards,Principal,SMCS.";
                            SMS.SMS(msgtxt, 0, dataGridView2.Rows[i].Cells["STU_PHONE"].Value.ToString().Trim().Replace(" ", ""), dataGridView2.Rows[i].Cells["STU_FIRST_NAME"].Value.ToString().Trim().Replace(" ", ""));

                        }

                        catch (Exception ex)
                        {
                            //tr.Abort();
                        }
                        finally
                        {

                        }
                    }



                    else
                        MessageBox.Show("There is No PhoneNo for  sending  SMS. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                }
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["CBNO"].Width = 160;
            dataGridView1.Columns["REC"].Width = 80;
            dataGridView1.Columns["STU_ID"].Width = 80;
            dataGridView1.Columns["AMOUNT"].HeaderText = "Amount";           
            dataGridView1.Columns["CBNO"].HeaderText = "Cheque No";
            dataGridView1.Columns["fine"].HeaderText = "Cheque Cancellation Charges";

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblchequebounce ORDER By id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtp11 = Convert.ToDateTime(dtp1.Value, System.Globalization.CultureInfo.InvariantCulture);
            ////System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            SqlCommand com = new SqlCommand("update tblFromMaster set Fdate='" + dtp11.ToString("MM/dd/yyyy") + "',FormFrom=" + txtFrom.Text + ", Fromto=" + txtTo.Text + ",Amount=" + txtamt.Text + ",Modifyby='" + sms.Form1.GlobalLoginUserName.ToString() + "' where id= " + txtid.Text + " ", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Form Updated...");
                    LoadGrid();
                    cancel_form();
                }
                else
                {
                    MsgBox.ShowBox("This Form already exist");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sc.Close();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (dtp1.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblchequebounce WHERE id=" + txtid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Record  Deleted Successfully!!!");
                        cancel_form(); ;
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel_form();
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            InitGrid();
           
        }
        private void cancel_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            //  dtp1.Text = "-Select-";
            txtFrom.Text = "";
            txtTo.Text = "";
            txtamt.Text = "";
            textBox1.Text = "";

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
          btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                //dtp1.Text = dataGridView1.CurrentRow.Cells["Fdate"].Value.ToString();
                txtFrom.Text = dataGridView1.CurrentRow.Cells["rec"].Value.ToString();
                txtTo.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();
                txtid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                txtamt.Text = dataGridView1.CurrentRow.Cells["CBNO"].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells["STU_ID"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["fine"].Value.ToString();


                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btMinimize_Click(object sender, EventArgs e)
        {

        }

        private void btMaximize_Click(object sender, EventArgs e)
        {

        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtamt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtamt_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection conn = dc.con();
            SqlDataAdapter objCmd = new SqlDataAdapter("select STU_PHONE,CLASS,FRC_id,FRC_STU_ID,FRC_NO,STU_ADM_NO,STU_FIRST_NAME,FRC_PAID_AMT from View_ReceiptSummaryRpt where FRC_DDNO='" + txtamt.Text + "' AND STATUS='Paid'", conn);
            DataTable dt = new DataTable();
            objCmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //textBox2.Text = dt.Rows[0]["frc_stu_id"].ToString();
                //txtFrom.Text = dt.Rows[0]["FRC_NO"].ToString();
                //txtTo.Text= dt.Rows[0]["FRC_PAID_AMT"].ToString();
                textBox3.Text = "500";
                dataGridView2.DataSource = dt;
                dataGridView2.Columns["FRC_id"].Visible = false;
                dataGridView2.Columns["FRC_STU_ID"].Visible = false;
                dataGridView2.Columns["CLASS"].Visible = false;
                dataGridView2.Columns["STU_PHONE"].Visible = false;
                dataGridView2.Columns["FRC_NO"].Width = 60;
                dataGridView2.Columns["STU_ADM_NO"].Width = 60;                
                dataGridView2.Columns["FRC_PAID_AMT"].Width = 60;
                dataGridView2.Columns["STU_FIRST_NAME"].Width = 160;
                dataGridView2.Columns["FRC_NO"].HeaderText = "FRC NO";
                dataGridView2.Columns["STU_ADM_NO"].HeaderText = "ADM NO";
                dataGridView2.Columns["FRC_PAID_AMT"].HeaderText = "PAID AMOUNT";
                dataGridView2.Columns["STU_FIRST_NAME"].HeaderText = "NAME ";

                
            }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection conn = dc.con();
            SqlDataAdapter objCmd = new SqlDataAdapter("select stu_adm_no from student_t where stu_id='" + textBox2.Text + "'", conn);
            DataTable dt = new DataTable();
            objCmd.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["stu_adm_no"].ToString();
            }
        }
    }
}
