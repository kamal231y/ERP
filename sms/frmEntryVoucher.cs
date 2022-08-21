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

namespace sms
{
    public partial class frmEntryVoucher : Form
    {
        int vtypename;
        int ledgertype;
        public frmEntryVoucher()
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
        public void InitGrid()
        {
            //LoadGrid();
            //dataGridView1.Columns["Name"].Width = 40;
            //dataGridView1.Columns["Description"].Width = 100;
            ////dataGridView1.Columns["ClassName"].Width = 60;
            //dataGridView1.Columns["Ledgerdate"].Width = 60;
            //dataGridView1.Columns["GName"].Width = 60;
            ////dataGridView1.Columns["userid"].Width = 60;
            //dataGridView1.Columns["date"].Width = 80;
            //dataGridView1.Columns["terminal"].Width = 100;
        }
        private void LoadGrid()
        {
            showstudentreport();
        }
        private void bindGroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("sprocGetvno ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@vtype",4);



            sc.Open();

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtvdes.Text = dt.Rows[0][1].ToString();
                txtvno.Text = dt.Rows[0][0].ToString();

                txtvno.Focus();
            }
           
        }
        private void bindBankLedger()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TBLLEDGER where gid<>2   order by name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "-Select-";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbaccount.DataSource = ds.Tables[0];
            cmbaccount.DisplayMember = "Name";
            cmbaccount.ValueMember = "id";
        }

        private void bindLedger()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TBLLEDGER where gid=2  order by name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "-Select-";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbledger.DataSource = ds.Tables[0];
            cmbledger.DisplayMember = "Name";
            cmbledger.ValueMember = "id";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            decimal Credit;
            decimal Debit;
            vtypename = 4;
            if (cmbaccount.Text == "-Select-" || cmbaccount.Text == "")
            {
                MessageBox.Show("Please select Account");
                return;
            }



            if (rbbank.Checked == true)
            {

                if (cmbledger.Text == "-Select-" || cmbledger.Text == "")
                {
                    MessageBox.Show("Please select Bank");
                    return;
                }
                ledgertype = Convert.ToInt32(cmbledger.SelectedValue);
                if (txtcashdeposite.Text != "" )
                {
                    Credit = 0;
                    Debit = Convert.ToDecimal(txtcashdeposite.Text);
                }

                else
                {
                //    string value;
                    if (txtinwardbank.Text == "")
                    {
                        //value = "Credit";
                        Debit = 0;
                        Credit = Convert.ToDecimal(txtoutwardbank.Text);
                    }
                    else
                    {
                        //value ="Debit";
                        Credit = 0;
                        Debit = Convert.ToDecimal(txtinwardbank.Text);
                    }
                }
            }
            else
            {
                ledgertype = 1;
                if (txtinwardcash.Text == "")
                {
                    //value = "Credit";
                    Debit = 0;
                    Credit = Convert.ToDecimal(txtoutwardcash.Text);
                }
                else
                {
                    //value ="Debit";
                    Credit = 0;
                    Debit = Convert.ToDecimal(txtinwardcash.Text);
                }
            
            }
                //DateTime dt2 = Convert.ToDateTime(dtpODate.Text, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt2 = Convert.ToDateTime((dtpODate.Text).ToString());
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();

                SqlCommand com = new SqlCommand("sp_insertjVoucher " + vtypename + "," + txtvno.Text + ",'" + dt2.ToString("MM/dd/yyyy") + "'," + cmbaccount.SelectedValue + "," + ledgertype + ", '" + Credit + "','" + Debit + "','" + txtremarks.Text + "','" + Form1.GlobalLoginUserName.ToString() + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Voucher Created...");
                         Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("Voucher not Created...");
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
           
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
                
        }
        private void Cancil_form()
        {
            rbbank.Checked = false;
            rbcash.Checked = true;
            grpbank.Enabled = false;
            grpcash.Enabled = true;
            bindBankLedger();
            bindLedger();
            bindGroup();
            txtcashdeposite.Text = "";
            txtinwardbank.Text = "";
            txtinwardcash.Text = "";
            txtoutwardbank.Text = "";
            txtoutwardcash.Text = "";
            txtremarks.Text = "";
           
        }
        
        private void frmLedgerName_Load(object sender, EventArgs e)
        {
            rbbank.Checked = false;
            rbcash.Checked = true;
            grpbank.Enabled = false;
            grpcash.Enabled = true;
            bindBankLedger();
            bindLedger();
            bindGroup();
            showstudentreport();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtopeningbalance_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbvouchertype_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtvdes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvno_Leave(object sender, EventArgs e)
        {
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbledger_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmbcredit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbcash_CheckedChanged(object sender, EventArgs e)
        {
            grpcash.Enabled = true;
           grpbank.Enabled = false;
          //  rbbank.Checked = true;
           // rbcash.Checked = true;
        }

        private void rbbank_Click(object sender, EventArgs e)
        {
            grpcash.Enabled = false;
            grpbank.Enabled = true;
            //rbcash.Checked = false;
            //rbbank.Checked = true;

        }
        private void showstudentreport()
        {

            dataGridView1.Visible = true;
          


            DateTime dt1 = Convert.ToDateTime(dtpODate.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dtpODate.Value, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("sprocvoucherentry ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@min1", dt1.ToString("MM/dd/yyyy"));
            da.SelectCommand.Parameters.AddWithValue("@max1", dt2.ToString("MM/dd/yyyy"));
            sc.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["SNo"].Visible = true;
                dataGridView1.Columns["sno"].Width = 40;
                dataGridView1.Columns["sdate"].HeaderText = "Date";
                dataGridView1.Columns["sdate"].Width = 100;
                dataGridView1.Columns["sparticular"].HeaderText = "Particulers";
                dataGridView1.Columns["sparticular"].Width = 100;
                dataGridView1.Columns["saccount"].HeaderText = "Accounts";
                dataGridView1.Columns["saccount"].Width = 100;
                dataGridView1.Columns["vouchertype"].HeaderText = "Vtype";
                dataGridView1.Columns["vouchertype"].Width = 100;
                dataGridView1.Columns["voucherno"].HeaderText = "Vno";
                dataGridView1.Columns["voucherno"].Width = 100;
                dataGridView1.Columns["cashin"].HeaderText = "Cash (In)";
                dataGridView1.Columns["cashin"].Width = 80;
                dataGridView1.Columns["cashout"].HeaderText = "Cash(Out)";
                dataGridView1.Columns["cashout"].Width = 80;
                dataGridView1.Columns["bankin"].HeaderText = "Bank(In)";
                dataGridView1.Columns["bankin"].Width = 90;
                dataGridView1.Columns["bankout"].HeaderText = "Bank(Out)";
                dataGridView1.Columns["bankout"].Width = 90;
                dataGridView1.Columns["balancec"].HeaderText = "Balance(Cash)";
                dataGridView1.Columns["balancec"].Width = 100;
                dataGridView1.Columns["balanceb"].HeaderText = "Balance(Bank)";
                dataGridView1.Columns["balanceb"].Width = 100;

            }

            else
            {
                dataGridView1.DataSource = DBNull.Value;


            }

            //for (int i = 0; i < dataGridView1.ColumnCount ; i++)
            //{
            //   string values1 = (dataGridView1.Rows[i].Cells[6].Value).ToString();
            //    if (values1.Substring((values1.Length - 2), 2) == "Cr")
            //    {
            //        //row.DefaultCellStyle.BackColor = Color.Red;
            //        dataGridView1.Rows[i].Cells[6].Style.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        // row.DefaultCellStyle.BackColor = Color.Green;
            //        dataGridView1.Rows[i].Cells[6].Style.ForeColor = Color.Green;
            //    }  
            //}


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string values1 = (row.Cells[6].Value).ToString();
                if (values1.Substring((values1.Length - 2), 2) == "Cr")
                {
                    //row.DefaultCellStyle.BackColor = Color.Red;
                    row.Cells[6].Style.BackColor = Color.Red;
                }
                else
                {
                    // row.DefaultCellStyle.BackColor = Color.Green;
                    row.Cells[6].Style.BackColor = Color.Green;
                }


                //string values2 = (row.Cells[11].Value).ToString();
                //if (values2.Substring((values2.Length - 2), 2) == "Cr")
                //{
                //    //row.DefaultCellStyle.BackColor = Color.Red;
                //    row.Cells[11].Style.ForeColor = Color.Red;
                //}
                //else
                //{
                //    // row.DefaultCellStyle.BackColor = Color.Green;
                //    row.Cells[11].Style.ForeColor = Color.Green;
                //}

            }
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    string values1 = (row.Cells[11].Value).ToString();
            //    if (values1.Substring((values1.Length - 2), 2) == "Cr")
            //    {
            //        //row.DefaultCellStyle.BackColor = Color.Red;
            //        row.Cells[11].Style.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        // row.DefaultCellStyle.BackColor = Color.Green;
            //        row.Cells[11].Style.ForeColor = Color.Green;
            //    }
            //}
            sc.Close();

        }

        private void txtinwardcash_Leave(object sender, EventArgs e)
        {
            if (txtremarks.Text == "")
            {
                txtremarks.Text = cmbaccount.Text + "/" + "Cash A/C";
            }
        }

        private void txtoutwardcash_Leave(object sender, EventArgs e)
        {
            if (txtremarks.Text == "")
            {
                txtremarks.Text = cmbaccount.Text + "/" + "Cash A/C";
            }
        }

        private void txtinwardbank_TextChanged(object sender, EventArgs e)
        {
            if (txtremarks.Text == "")
            {
                txtremarks.Text = cmbaccount.Text + "/" + cmbledger.Text;
            }
        }

        private void txtoutwardbank_TextChanged(object sender, EventArgs e)
        {
            if (txtremarks.Text == "")
            {
                txtremarks.Text = cmbaccount.Text + "/" + cmbledger.Text;
            }
        }

      

        private void txtcashdeposite_Leave(object sender, EventArgs e)
        {
            if (txtremarks.Text == "")
            {
                txtremarks.Text = cmbaccount.Text + "/" + cmbledger.Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cryEntryvoucherDetail obj = new cryEntryvoucherDetail();
           
            obj.ShowDialog();

        }
    }
}
