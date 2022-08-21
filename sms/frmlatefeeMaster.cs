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
    public partial class frmlatefeeMaster : Form
    {
        // string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmlatefeeMaster()
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
        private void bindYear()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;
            da = new SqlDataAdapter(" select id,year from Year_T order by id ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
           
            row["Year"] = "Select";
            row["id"] = 0;

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbyear.DataSource = ds.Tables[0];
            cmbyear.DisplayMember = "Year";
            cmbyear.ValueMember = "id";
        }

      

        private void btn_create_Click(object sender, EventArgs e)
        {
            
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["LateFine"].Width = 100;
            dataGridView1.Columns["CDate"].Width = 100;
            //dataGridView1.Columns["Monthname"].Width = 160;
            dataGridView1.Columns["LateFine"].HeaderText = "Late Fine";
            dataGridView1.Columns["CDate"].HeaderText = "Concession Upto";
            //dataGridView1.Columns["Monthname"].HeaderText = "Monthname";
            dataGridView1.Columns["Monthend"].HeaderText = "Monthend";

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select *from late_fee_t ORDER By id", sc);  //select t1.id,LateFine,CDate,t2.year as monthname,Month,Monthend from late_fee_t t1,tblmonth t2 where t1.month=t2.id ORDER By t1.id
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {


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
            
            bindYear();
           
        }
        private void cancel_form()
        {
            btnUpdate.Enabled = false;
            txtFrom.Text = "";
            txtamt.Text = "";

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtamt.Text = dataGridView1.CurrentRow.Cells["LateFine"].Value.ToString();
                txtid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                cmbyear.SelectedValue = dataGridView1.CurrentRow.Cells["month"].Value.ToString();
                txtFrom.Text = dataGridView1.CurrentRow.Cells["cdate"].Value.ToString();
                textBox1.Text= dataGridView1.CurrentRow.Cells["Monthend"].Value.ToString();
                btnUpdate.Enabled = true;
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

     

        private void button1_Click(object sender, EventArgs e)
        {
            System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtp11 = Convert.ToDateTime(dtp1.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlCommand com = new SqlCommand("exec create_late_fee " + txtamt.Text + "," + txtFrom.Text + ",'"+cmbyear.SelectedValue+"','"+textBox1.Text+"'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Late Fee Updated...");
                    //LoadGrid();
                    cancel_form();
                }
                else
                {
                    MsgBox.ShowBox("This Late Fee Already exist");
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

        private void txtamt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = v1.AllowOnlyNumber(e.KeyChar);


        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt16(txtFrom.Text) > 31)
            {
                MessageBox.Show("Please Select Correct Date ");
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
