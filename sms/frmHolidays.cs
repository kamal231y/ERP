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
    public partial class frmHolidays : Form
    {
        // string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmHolidays()
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
            //if (dtp1.Text == "")
            //{
            //    MsgBox.ShowBox("Year should not be blank");
            //    // return;
            //}
            if (txtholiday.Text == "")
            {
                MsgBox.ShowBox("Please provide Holiday type");

            }
            if (dateTimePicker1.Text == "")
            {
                //MsgBox.ShowBox("To should not be blank");

            }
            if (dateTimePicker2.Text == "")
            {
                //MsgBox.ShowBox("Amount should not be blank");

            }
            if (cmbmnth.Text == "")
            {
                MsgBox.ShowBox("Month should not be blank");

            }


            SqlCommand com = new SqlCommand("Insert_tblholiday '" + cmbmnth.SelectedValue + "','" + txtholiday.Text + "','" + dtfrm.ToString("yyyy/MM/dd") + "','" + dtto.ToString("yyyy/MM/dd") + " '  ", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Holiday is created");
                    cancel_form();
                    //LoadGrid();
                    InitGrid();
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
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["month"].Visible = false;
            dataGridView1.Columns["holiday_type"].Width = 160;
            dataGridView1.Columns["YEAR"].Width = 100;
            dataGridView1.Columns["fromdate"].Width = 140;
            dataGridView1.Columns["todate"].Width = 140;
            dataGridView1.Columns["todate"].HeaderText = "To date";
            dataGridView1.Columns["Fromdate"].HeaderText = "From date";
            dataGridView1.Columns["holiday_type"].HeaderText ="Holiday Name";
            dataGridView1.Columns["YEAR"].HeaderText = "Month";
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from VIEW_holiday ORDER By id ", sc);
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


            SqlCommand com = new SqlCommand("update tblholiday set MONTH ='" + cmbmnth.SelectedValue + "',Holiday_type='" + txtholiday.Text + "', Fromdate='" + dtfrm.ToString() + "',todate='" + dtto.ToString() + " ' where id= " + txtid.Text + " ", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Holiday Updated...");
                    LoadGrid();
                    cancel_form();
                }
                else
                {
                    MsgBox.ShowBox("This Holiday already exist");
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblFromMaster WHERE id=" + txtid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Holiday Deleted Successfully!!!");
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
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            bindYear();
            InitGrid();
            //dataGridView1.Columns["FormFrom"].Width = 220;
            //dataGridView1.Columns["Fromto"].Width = 220;
            //dataGridView1.Columns["id"].Width = 0;
        }
        private void cancel_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            //  dtp1.Text = "-Select-";
            //txtFrom.Text = "";
            cmbmnth.Text = "";
            txtholiday.Text = "";

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                cmbmnth.Text = dataGridView1.CurrentRow.Cells["YEAR"].Value.ToString();
                txtholiday.Text = dataGridView1.CurrentRow.Cells["HOLIDAY_TYPE"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["FROMDATE"].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["TODATE"].Value.ToString();


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
            //vl.all(e);
            //vl.d = 
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

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            if (!Form1.board)
                da = new SqlDataAdapter(" select id,year from Year_T order by id ", sc);
            else
                da = new SqlDataAdapter(" select id,year_up as year from Year_T order by id ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["id"] = 0;
            row["Year"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbmnth.DataSource = ds.Tables[0];
            cmbmnth.DisplayMember = "Year";
            cmbmnth.ValueMember = "id";
        }

        private void cmbmnth_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from VIEW_holiday where month='" + cmbmnth.SelectedValue + "'ORDER By id ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["month"].Visible = false;
            dataGridView1.Columns["holiday_type"].Width = 160;
            dataGridView1.Columns["YEAR"].Width = 100;
            dataGridView1.Columns["fromdate"].Width = 140;
            dataGridView1.Columns["todate"].Width = 140;
            dataGridView1.Columns["todate"].HeaderText = "To date";
            dataGridView1.Columns["Fromdate"].HeaderText = "From date";
            dataGridView1.Columns["holiday_type"].HeaderText = "Holiday Name";
            dataGridView1.Columns["YEAR"].HeaderText = "Month";
        }    
    }
}
