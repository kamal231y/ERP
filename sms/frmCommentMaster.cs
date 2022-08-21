using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using sms.BLClass;
using System.Data.SqlClient;
using MsgBoxRet;

namespace sms
{
    public partial class frmCommentMaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public string rply, phone,id;
        public frmCommentMaster()
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
            Cancil_form();
            InitGrid();
        }
        private void OnlyCapital(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Cancil_form()
        {
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtstatename.Text = "";
            lblC.Text = "";
            LoadGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            //dataGridView1.Columns["ID"].Visible = false;
            //dataGridView1.Columns["admno"].Width = 60;
            //dataGridView1.Columns["comment"].Width = 400;
            //dataGridView1.Columns["comment"].HeaderText = "comment";
            
          
        }
        private void LoadGrid()
        {
            if (radioButton1.Checked == true)
            {
                rply = "0";
            }
            else
            {
                rply = "1";
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id,a.Admno,b.stu_first_name as Name,b.stu_mobile as Mobile ,a.Comment,a.reply,a.Date,a.ReplyDate from tblComment a, student_t b where a.AdmNo=b.STU_ADM_NO and a.status='" + rply + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            ////DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            ////dataclass dd = new dataclass();
            ////SqlConnection sc = dd.con();
            ////string str= sc.DataSource.ToString();
            ////if (txtstatename.Text == "")
            ////{
            ////    MsgBox.ShowBox("Vehicle Model not be blank...");
            ////}
            ////else
            ////{
            ////    SqlCommand com = new SqlCommand("insert_VEHICLE_MODEL_T '" + txtstatename.Text + "'", sc);
            ////    SqlDataReader dr;
            ////    sc.Open();
            ////    try
            ////    {
            ////        dr = com.ExecuteReader();
            ////        if (dr.RecordsAffected > 0)
            ////        {
            ////            MsgBox.ShowBox("Vehicle Model Created...");
            ////            Cancil_form();
            ////            LoadGrid();
            ////        }
            ////        else
            ////        {
            ////            MsgBox.ShowBox("This Vehicle Model already exist");
            ////        }
                    
            ////    }
            ////    catch (Exception ex)
            ////    { }
            ////    finally
            ////    {
            ////        sc.Close();
            ////    }
            ////}
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                lblC.Text = dataGridView1.CurrentRow.Cells["comment"].Value.ToString();
                txtstatename.Text = dataGridView1.CurrentRow.Cells["reply"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["admno"].Value.ToString();
                phone = dataGridView1.CurrentRow.Cells["Mobile"].Value.ToString();
                id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();

                btnUpdate.Enabled=true;
                btn_delete.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtstateid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Reply Comment Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("updatetblComment '" + id + "','" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Replyed to comment ...");
                        string complainno = id.Trim();
                        string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,your Problem No-. " + complainno + " has been resolved. If not satisfied please call on 9914310080 or 01752360691.Thanks";
                        DAL.smsclass SMS = new sms.DAL.smsclass();
                        string str = SMS.SMS(msgtxt, 0, phone.Trim().Replace(" ", ""), dataGridView1.Rows[0].Cells["Name"].Value.ToString().Trim().Replace(" ", ""));
                   
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Comment already Replyed");
                    }

                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    try
            //    {
            //        dataclass dc = new dataclass();
            //        SqlConnection conn = dc.con();
            //        if (txtstateid.Text == "")
            //        {
            //            MsgBox.ShowBox("Please Click to Detail Row!!!");
            //            return;
            //        }
            //        SqlCommand objCmd = new SqlCommand("DELETE FROM tblcomment WHERE admno=" + txtstateid.Text + "", conn);
            //        SqlDataReader dr;
            //        objCmd.Connection.Open();
            //        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
            //        if (dr.RecordsAffected > 0)
            //        {
            //            MsgBox.ShowBox(" comment Deleted Successfully!!!");
            //            Cancil_form();
            //            InitGrid();
            //        }
            //        else
            //        {
            //            MsgBox.ShowBox(" Deleting Error!!!");
            //        }
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            //else if (dialogResult == DialogResult.No)
            //{
            //    //do something else
            //}
        }

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void frmCommentMaster_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {

        }

        private void btMaximize_Click(object sender, EventArgs e)
        {

        }

        private void txtstatename_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

      
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
