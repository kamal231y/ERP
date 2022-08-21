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
    public partial class frmExpense : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmExpense()
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
           
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["Amount"].Width = 160;
            dataGridView1.Columns["Amount"].HeaderText = "Amount";
            dataGridView1.Columns["stu_id"].Width = 160;
            dataGridView1.Columns["stu_id"].HeaderText = "stu_id";



        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblbalancepaid where STU_ID ='" + textBox2.Text + "'order by id desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Amount Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_tblbalance " + textBox2.Text + "," + textBox3.Text + "," + txtstatename.Text + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Record Save...");
                        Cancil_form();
                        LoadGrid();
                        bindReceiptNo();
                    }
                    else
                    {
                        MsgBox.ShowBox("Record not saved");
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
        private void bindReceiptNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("Select max(id)+1 as max from tblbalancepaid", sc);
            sc.Open();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (dr["max"].ToString() == "")
                {
                    textBox4.Text = "1";

                }
                else
                {
                    textBox4.Text = Convert.ToString(Convert.ToInt32(dr["max"]));
                }

            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            
            crysaleprint.rec = textBox4.Text;           
           crysaleprint obj = new crysaleprint();
           obj.ShowDialog();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtstatename.Text = dataGridView1.CurrentRow.Cells["amount"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["SALE_NO"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["stu_id"].Value.ToString();
                btnUpdate.Enabled = true;
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
                MsgBox.ShowBox("State Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_tblbalance '" + txtstateid.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Record Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Record Name already exist");
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

            crysalebalance obj = new crysalebalance();
            obj.ShowDialog();
        }

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void frmStateMaster_Load(object sender, EventArgs e)
        {
            bindReceiptNo(); btn_delete.Enabled = true; button1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_salebal where  AdmNo like '" + textBox1.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                textBox2.Text = dt.Rows[0]["STU_ID"].ToString();
                textBox3.Text = dt.Rows[0]["sale_no"].ToString();
                txtstatename.Text = dt.Rows[0]["balance"].ToString();
                LoadGrid();

            }



        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblbalancepaid where id ='" + textBox4.Text + "'order by id desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crysaleprintrecord obj = new crysaleprintrecord();
            obj.ShowDialog();
        }
    }
}

