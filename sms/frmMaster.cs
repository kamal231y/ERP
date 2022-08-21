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
    public partial class frmMaster : Form
    {
        // string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public frmMaster()
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
            //if (dtp1.Text == "")
            //{
            //    MsgBox.ShowBox("Year should not be blank");
            //    // return;
            //}
            if (txtFrom.Text == "")
            {
                MsgBox.ShowBox("From should not be blank");

            }
            if (txtTo.Text == "")
            {
                MsgBox.ShowBox("To should not be blank");

            }
            if (txtamt.Text == "")
            {
                MsgBox.ShowBox("Amount should not be blank");

            }


            SqlCommand com = new SqlCommand("Insert_tblFromMaster '" + dtp11.ToString("MM/dd/yyyy") + "'," + txtFrom.Text + "," + txtTo.Text + "," + txtamt.Text + " , '" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1.ToString("MM/dd/yyyy") + "','" + cmbsession.Text + "' ", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Form sell is created");
                    cancel_form();
                    //LoadGrid();
                    InitGrid();
                }
                else
                {
                    MsgBox.ShowBox("Sr.To value should be greater than Sr.From");
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
            dataGridView1.Columns["session"].Width = 100;
            dataGridView1.Columns["FormFrom"].Width = 100;
            dataGridView1.Columns["Fromto"].Width = 100;
            dataGridView1.Columns["Amount"].Width = 100;
            dataGridView1.Columns["session"].HeaderText = "Session";
            dataGridView1.Columns["FormFrom"].HeaderText = "Sr. From";
            dataGridView1.Columns["Fromto"].HeaderText = "Sr. To";
            dataGridView1.Columns["Amount"].HeaderText = "Amount";

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id,session,FormFrom,Fromto,Amount from tblFromMaster order by session ", sc);
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


            SqlCommand com = new SqlCommand("update tblFromMaster set Fdate='" + dtp11.ToString("MM/dd/yyyy") + "',FormFrom=" + txtFrom.Text + ", Fromto=" + txtTo.Text + ",Amount=" + txtamt.Text + ",Modifyby='" + sms.Form1.GlobalLoginUserName.ToString() + "',session='"+ cmbsession.Text +"' where id= " + txtid.Text + " ", sc);
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
                    SqlCommand objCmd = new SqlCommand("DELETE_FromMaster " + txtid.Text + "," + txtFrom.Text + "," + txtTo.Text + ",'" + cmbsession.Text + "'", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Form Deleted Successfully!!!");
                        cancel_form(); 
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Form Series Already Used!!!");
                    }
                    ////SqlCommand objCmd = new SqlCommand("DELETE FROM tblFromMaster WHERE id=" + txtid.Text + "", conn);
                    ////SqlDataReader dr;
                    ////objCmd.Connection.Open();
                    ////dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    ////if (dr.RecordsAffected > 0)
                    ////{
                    ////    MsgBox.ShowBox(" Form Deleted Successfully!!!");
                    ////    cancel_form(); ;
                    ////    LoadGrid();
                    ////}
                    ////else
                    ////{
                    ////    MsgBox.ShowBox(" Deleting Error!!!");
                    ////}
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
            bindSession();
            //dataGridView1.Columns["FormFrom"].Width = 220;
            //dataGridView1.Columns["Fromto"].Width = 220;
            //dataGridView1.Columns["id"].Width = 0;
        }
        private void bindSession()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from session_T  order by id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["Session"] = "2015-2016";
            //row["id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "Session";
            cmbsession.ValueMember = "id";

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

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                //dtp1.Text = dataGridView1.CurrentRow.Cells["Fdate"].Value.ToString();
                txtFrom.Text = dataGridView1.CurrentRow.Cells["FormFrom"].Value.ToString();
                txtTo.Text = dataGridView1.CurrentRow.Cells["Fromto"].Value.ToString();
                txtid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                txtamt.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();


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
            v1.digitonly(e);
        }

        private void txtamt_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection conn = dc.con();
            SqlDataAdapter objCmd = new SqlDataAdapter(" select (Max((FromTo)+1)) as FormFrom  from tblFromMaster  ", conn);
            DataTable dt = new DataTable();
            objCmd.Fill(dt);


            txtFrom.Text = dt.Rows[0]["FormFrom"].ToString();





        }

        private void cmbsession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
