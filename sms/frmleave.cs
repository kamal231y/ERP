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
    public partial class frmleave : Form
    {
        // string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmleave()
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
            DateTime dtfrm = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtto = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtholiday.Text == "")
            {
                MsgBox.ShowBox("Please provide Reason");

            }
            if (dateTimePicker1.Text == "")
            {

            }
            if (dateTimePicker2.Text == "")
            {

            }
            if (cmbmnth.Text == "")
            {
                MsgBox.ShowBox("Adm No. should not be blank");

            }

            SqlCommand com = new SqlCommand("Insert_tblleave '" + cmbmnth.Text + "','" + txtholiday.Text + "','" + dtfrm.ToString("yyyy/MM/dd") + "','" + dtto.ToString("yyyy/MM/dd") + " ','" + sms.Form1.GlobalFAYear.ToString() + "'  ", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    if (Form1.SMS)
                        sms2();
                    MsgBox.ShowBox("Leave  created");

                    cancel_form();
                    InitGrid();
                    listBox1.Visible = false;
                }
                else
                {
                    MsgBox.ShowBox("Record Not Save");
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
        private void sms2()
        {
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            try
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string msgtxt = "Dear Parents, Your Ward " + "  " + textBox1.Text.Trim() + " leave has sensation from " + dt1.ToString("dd/MM/yyyy") + " To " + dt2.ToString("dd/MM/yyyy") + ".* Thanks";
                DAL.smsclass SMS = new sms.DAL.smsclass();
                SMS.SMS(msgtxt, 0, textBox2.Text.Trim(), textBox1.Text);
            }

            catch (Exception ex)
            {

                MessageBox.Show("SMS not send... !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }


        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["stu_id"].Width = 100;
            dataGridView1.Columns["reason"].Width = 160;
            dataGridView1.Columns["fromdate"].Width = 140;
            dataGridView1.Columns["todate"].Width = 140;
            dataGridView1.Columns["todate"].HeaderText = "To date";
            dataGridView1.Columns["Fromdate"].HeaderText = "From date";

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from VIEW_LEAVE where sessionid='" + sms.Form1.GlobalFAYear.ToString() + "' ORDER By id ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime dtfrm = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtto = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);


            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            SqlCommand com = new SqlCommand("update tblleave set stu_id ='" + cmbmnth.Text + "',reason='" + txtholiday.Text + "', Fromdate='" + dtfrm.ToString() + "',todate='" + dtto.ToString() + " ' where id= " + txtid.Text + " and sessionid='" + sms.Form1.GlobalFAYear.ToString() + "' ", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Leave Updated...");
                    LoadGrid();
                    cancel_form();
                }
                else
                {
                    MsgBox.ShowBox("This Leave already exist");
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
                    if (cmbmnth.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblleave WHERE id=" + txtid.Text + " and sessionid='" + sms.Form1.GlobalFAYear.ToString() + "'", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Leave Deleted Successfully!!!");
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
            InitGrid();
            listBox1.Visible = false;
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            bindYear();
            InitGrid();
            listBox1.Visible = false;

        }
        private void cancel_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            cmbmnth.Text = "";
            txtholiday.Text = "";
            listBox1.Visible = false;
            textBox1.Text = "";
            cmbmnth.Text = "";

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                cmbmnth.Text = dataGridView1.CurrentRow.Cells["stu_adm_no"].Value.ToString();
                txtholiday.Text = dataGridView1.CurrentRow.Cells["reason"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["FROMDATE"].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["TODATE"].Value.ToString();


                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
            listBox1.Visible = false;
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

            e.KeyChar = v1.AllowOnlyNumber(e.KeyChar);

        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = v1.AllowOnlyNumber(e.KeyChar);
        }

        private void txtamt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtamt_Leave(object sender, EventArgs e)
        {





        }
        private void bindYear()
        {


        }

        private void cmbmnth_Leave(object sender, EventArgs e)
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from view_leave where stu_id='" + cmbmnth.SelectedValue + "'ORDER By id ", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
            //dataGridView1.Columns["id"].Visible = false;

            //dataGridView1.Columns["reason"].Width = 160;
            //dataGridView1.Columns["stu_id"].Width = 100;
            //dataGridView1.Columns["fromdate"].Width = 140;
            //dataGridView1.Columns["todate"].Width = 140;
            //dataGridView1.Columns["todate"].HeaderText = "To date";
            //dataGridView1.Columns["Fromdate"].HeaderText = "From date";
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where stu_id='" + cmbmnth.SelectedValue + "'ORDER By stu_id ", sc);
            //DataTable ds = new DataTable();
            //da.Fill(ds);
            //if (ds.Rows.Count > 0)
            //{
            //    textBox1.Text = ds.Rows[0]["student_name"].ToString();
            //    textBox2.Text = ds.Rows[0]["phone"].ToString();
            //}


        }
        private void bindstuadmleave()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where admno='" + cmbmnth.Text + "'ORDER By stu_id ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                textBox1.Text = ds.Rows[0]["student_name"].ToString();
                textBox2.Text = ds.Rows[0]["phone"].ToString();
            }
        }

        private void cmbmnth_SelectedValueChanged(object sender, EventArgs e)
        {



        }

        private void cmbmnth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            {

                string Name;

                if (textBox1.Text == "-Select-" || textBox1.Text == "" || textBox1.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = textBox1.Text.Trim().ToString();
                }
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '/' + ds1.Tables[0].Rows[j][1].ToString() + '/' + ds1.Tables[0].Rows[j][2].ToString() + '/' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = cmbmnth.Top + cmbmnth.Height;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    cmbmnth.Text = "";
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    cmbmnth.Text = str[0].ToString();
                    listBox1.Visible = false;
                  bindstuadmleave();
                    listBox1.Visible = false;



                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }

        }

        private void btn_delete_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbmnth_TextChanged(object sender, EventArgs e)
        {
            bindstuadmleave();

        }
    }
}

