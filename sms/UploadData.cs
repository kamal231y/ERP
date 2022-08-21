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
    public partial class UploadData : Form
    {
        SqlTransaction tran;
        SqlCommand cmd;
        dataclass dc = new dataclass();
        BLclass bl = new BLclass();
        validation vl = new validation();
        int School_id = 0;


        public UploadData()
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
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd2 = new SqlCommand("Select * from company_t", sc);
            SqlDataReader dr = cmd2.ExecuteReader();
            if (dr.Read())
                School_id = Convert.ToInt16(dr["School_id"].ToString());
            sc.Close();


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
            feereceipt();
            feedetail();
            feedues();
            SqlConnection sc = dc.con();

            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();

            SqlDataAdapter da = new SqlDataAdapter("select * from student_receipt_t where SFR_web=0", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                dataGridView1.DataSource = dt;

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
                        cmd.CommandText = "insert_stu_receiptno";
                        cmd.Connection = scweb;
                        cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.CommandText = "Update_receipt_status";
                        cmd1.Connection = sc;

                        cmd.Parameters.AddWithValue("@stu_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["SFR_STU_ID"].Value));
                        cmd.Parameters.AddWithValue("@once", "0");
                        cmd.Parameters.AddWithValue("@mt1", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_APRIL"].Value));
                        cmd.Parameters.AddWithValue("@mt2", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_MAY"].Value));
                        cmd.Parameters.AddWithValue("@mt3", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_JUNE"].Value));
                        cmd.Parameters.AddWithValue("@mt4", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_JULY"].Value));
                        cmd.Parameters.AddWithValue("@mt5", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_AUGUST"].Value));
                        cmd.Parameters.AddWithValue("@mt6", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_SEPTEMBER"].Value));
                        cmd.Parameters.AddWithValue("@mt7", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_OCTOBER"].Value));
                        cmd.Parameters.AddWithValue("@mt8", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_NOVEMBER"].Value));
                        cmd.Parameters.AddWithValue("@mt9", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_DECEMBER"].Value));
                        cmd.Parameters.AddWithValue("@mt10 ", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_JANUARY"].Value));
                        cmd.Parameters.AddWithValue("@mt11 ", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_FEBRUARY"].Value));
                        cmd.Parameters.AddWithValue("@mt12", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_MARCH"].Value));
                        cmd.Parameters.AddWithValue("@mt13", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_JANA"].Value));
                        cmd.Parameters.AddWithValue("@mt14", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_FEBA"].Value));
                        cmd.Parameters.AddWithValue("@mt15", Convert.ToInt64(dataGridView1.Rows[z].Cells["SFR_MARCHA"].Value));
                        cmd.Parameters.AddWithValue("@School_id", School_id);

                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();
                        cmd1.Parameters.AddWithValue("@stu_id", (dataGridView1.Rows[z].Cells["sfr_Stu_id"].Value));
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

        }
        private void feereceipt()
        {
            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT t1.*,t2.* FROM FEE_RECEIPT_T t1,FEE_RECIEPT_MONTH t2  where t1.webstatus=0 and t1.frc_no=t2.rec_no order by frc_id", sc);
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
                            cmd.CommandText = "Insert_student_FeeReciept_web";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusfeereceipt";
                            cmd1.Connection = sc;

                            cmd.Parameters.AddWithValue("@FRC_NO", Convert.ToInt16(dataGridView1.Rows[z].Cells["FRC_NO"].Value));
                            cmd.Parameters.AddWithValue("@FRC_DATE", Convert.ToDateTime(dataGridView1.Rows[z].Cells["FRC_DATE"].Value));

                            cmd.Parameters.AddWithValue("@FRC_STU_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["FRC_STU_ID"].Value));
                            cmd.Parameters.AddWithValue("@FRC_month", Convert.ToInt16(dataGridView1.Rows[z].Cells["FRC_month"].Value));
                            cmd.Parameters.AddWithValue("@FRC_FEE_AMT", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRC_FEE_AMT"].Value));

                            cmd.Parameters.AddWithValue("@FRC_PAID_AMT", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRC_PAID_AMT"].Value));
                            cmd.Parameters.AddWithValue("@FRC_PREV_DUE", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRC_PREV_DUE"].Value));
                            cmd.Parameters.AddWithValue("@FRC_CONCESSION", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRC_CONCESSION"].Value));
                            cmd.Parameters.AddWithValue("@FRC_MODE", Convert.ToInt32(dataGridView1.Rows[z].Cells["FRC_MODE"].Value));
                            cmd.Parameters.AddWithValue("@FRC_DDNO", (dataGridView1.Rows[z].Cells["FRC_DDNO"].Value));

                            cmd.Parameters.AddWithValue("@FRC_ACC_ID ", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRC_ACC_ID"].Value));
                            cmd.Parameters.AddWithValue("@FRC_COMMENT ", (dataGridView1.Rows[z].Cells["FRC_COMMENT"].Value));
                            cmd.Parameters.AddWithValue("@Balance", "0");
                            cmd.Parameters.AddWithValue("@USERid", (dataGridView1.Rows[z].Cells["FRC_USER"].Value));
                            cmd.Parameters.AddWithValue("@latefine", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRC_LATE_FINE"].Value));
                            cmd.Parameters.AddWithValue("@School_id", School_id);


                            cmd.Parameters.AddWithValue("@FRC_STATUS", Convert.ToInt32(dataGridView1.Rows[z].Cells["FRC_STATUS"].Value));
                            cmd.Parameters.AddWithValue("@monthname", (dataGridView1.Rows[z].Cells["month"].Value));
                            cmd.Parameters.AddWithValue("@studentname", (dataGridView1.Rows[z].Cells["name"].Value));
                            cmd.Parameters.AddWithValue("@FRC_BANK", Convert.ToInt16(dataGridView1.Rows[z].Cells["FRC_BANK"].Value));

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@FRC_NO", (dataGridView1.Rows[z].Cells["FRC_NO"].Value));
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        scweb.Close();
                        Cancil_form();
                        MsgBox.ShowBox("Continue.....");
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
        private void feedetail()
        {
            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from FEE_RECEIPT_DETAILS_T where webstatus=0  order by FRD_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
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
                            cmd.CommandText = "insert_tblinfo_web";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusfeedetail";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@receiptno", Convert.ToInt16(dataGridView1.Rows[z].Cells["FRD_FRC_ID"].Value));
                            cmd.Parameters.AddWithValue("@FEE_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["FRD_FEE_ID"].Value));
                            cmd.Parameters.AddWithValue("@fee", Convert.ToInt64(dataGridView1.Rows[z].Cells["FRD_FEE_AMT"].Value));
                            cmd.Parameters.AddWithValue("@School_id", School_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@FRD_FRC_ID", Convert.ToInt16(dataGridView1.Rows[z].Cells["FRD_FRC_ID"].Value));
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        scweb.Close();
                        Cancil_form();
                        MsgBox.ShowBox("Continue.....");

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
        private void feedues()
        {
            SqlCommand cmd1;
            SqlConnection scweb = dc.conweb();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from student_old_dues_t where webstatus=0  order by stu_id", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
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
                            cmd.CommandText = "insert_tblolddues_web";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusoldfee";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@stu_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["stu_id"].Value));
                            cmd.Parameters.AddWithValue("@Cls_id", Convert.ToInt32(dataGridView1.Rows[z].Cells["Cls_id"].Value));
                            cmd.Parameters.AddWithValue("@Dues", Convert.ToInt64(dataGridView1.Rows[z].Cells["Dues"].Value));
                            cmd.Parameters.AddWithValue("@School_id", School_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@stu_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["stu_id"].Value));
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        sc.Close();
                        scweb.Close();
                        Cancil_form();
                        MsgBox.ShowBox("Continue.....");

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
            str = "SELECT t1.*,t2.* FROM FEE_RECEIPT_T t1,FEE_RECIEPT_MONTH t2  where t1.webstatus=0 and t1.frc_no=t2.rec_no order by frc_id";

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

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from student_old_dues_t where webstatus=0  order by stu_id";

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
            SqlDataAdapter da = new SqlDataAdapter("select * from student_old_dues_t where webstatus=0  order by stu_id", sc);
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
                            cmd.CommandText = "insert_tblolddues_web";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusoldfee";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@stu_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["stu_id"].Value));
                            cmd.Parameters.AddWithValue("@Cls_id", Convert.ToInt32(dataGridView1.Rows[z].Cells["Cls_id"].Value));
                            cmd.Parameters.AddWithValue("@Dues", Convert.ToInt64(dataGridView1.Rows[z].Cells["Dues"].Value));
                            cmd.Parameters.AddWithValue("@School_id", School_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@stu_id", Convert.ToInt16(dataGridView1.Rows[z].Cells["stu_id"].Value));
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

        private void button7_Click(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            str = "select * from FEE_concesion_T where webstatus='0' order by id";

            if (str != "")
            {

                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
            }
        }

        private void button8_Click(object sender, EventArgs e)
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
            SqlDataAdapter da = new SqlDataAdapter("select * from FEE_concesion_T where webstatus=0  ", sc);
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
                            cmd.CommandText = "insert_concesion_web";
                            cmd.Connection = scweb;
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "insert_webstatusconcession";
                            cmd1.Connection = sc;
                            cmd.Parameters.AddWithValue("@stu_Id", Convert.ToInt64(dataGridView1.Rows[z].Cells["stu_id"].Value));
                            cmd.Parameters.AddWithValue("@Fee_id", Convert.ToInt64(dataGridView1.Rows[z].Cells["Fee_id"].Value));
                            cmd.Parameters.AddWithValue("@Amount", Convert.ToInt64(dataGridView1.Rows[z].Cells["Amount"].Value));
                            cmd.Parameters.AddWithValue("@REMARKS", dataGridView1.Rows[z].Cells["REMARKS"].Value);
                            cmd.Parameters.AddWithValue("@APR", Convert.ToInt64(dataGridView1.Rows[z].Cells["APR"].Value));
                            cmd.Parameters.AddWithValue("@MAY", Convert.ToInt64(dataGridView1.Rows[z].Cells["MAY"].Value));
                            cmd.Parameters.AddWithValue("@JUN", Convert.ToInt64(dataGridView1.Rows[z].Cells["JUN"].Value));
                            cmd.Parameters.AddWithValue("@JUL", Convert.ToInt64(dataGridView1.Rows[z].Cells["JUL"].Value));
                            cmd.Parameters.AddWithValue("@AUG", Convert.ToInt64(dataGridView1.Rows[z].Cells["AUG"].Value));


                             cmd.Parameters.AddWithValue("@SEP", Convert.ToInt64(dataGridView1.Rows[z].Cells["SEP"].Value));
                             cmd.Parameters.AddWithValue("@OCT", Convert.ToInt64(dataGridView1.Rows[z].Cells["OCT"].Value));
                            cmd.Parameters.AddWithValue("@NOV", Convert.ToInt64(dataGridView1.Rows[z].Cells["NOV"].Value));
                            cmd.Parameters.AddWithValue("@DEC", Convert.ToInt64(dataGridView1.Rows[z].Cells["DEC"].Value));
                            cmd.Parameters.AddWithValue("@JAN", Convert.ToInt64(dataGridView1.Rows[z].Cells["JAN"].Value));

                            cmd.Parameters.AddWithValue("@FEB", Convert.ToInt64(dataGridView1.Rows[z].Cells["FEB"].Value));
                            cmd.Parameters.AddWithValue("@MAR", Convert.ToInt64(dataGridView1.Rows[z].Cells["MAR"].Value));
    
                            cmd.Parameters.AddWithValue("@School_id", School_id);

                            cmd.Transaction = tran;
                            cmd.ExecuteNonQuery();
                            cmd1.Parameters.AddWithValue("@id", Convert.ToInt16(dataGridView1.Rows[z].Cells["id"].Value));
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

