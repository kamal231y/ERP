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
    public partial class frmJournalVoucher : Form
    {
        public frmJournalVoucher()
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
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from View_tblLedger ORDER BY Name", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindGroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblVoucherType", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["vtname"] = "-Select-";
            row["vtid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbvouchertype.DataSource = ds.Tables[0];
            cmbvouchertype.DisplayMember = "VTName";
            cmbvouchertype.ValueMember = "VTid";
           
        }
        private void bindBankLedger()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TBLLEDGER  order by name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "-Select-";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbBankLedger.DataSource = ds.Tables[0];
            cmbBankLedger.DisplayMember = "Name";
            cmbBankLedger.ValueMember = "id";
        }

        private void bindLedger()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TBLLEDGER  order by name", sc);
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
            if (cmbledger.Text == "-Select-" || cmbledger.Text == "")
            {
                MessageBox.Show("Please select Group");
                return;
            }
            if (Cmbcredit.Text == "-Select-" || Cmbcredit.Text == "")
            {
                MessageBox.Show("Please select Credit or Debit");
                return;
            }
            else
            {
                string value;
                if (Cmbcredit.Text == "Cr")
                {
                    //value = "Credit";
                    Debit = 0;
                    Credit = Convert.ToDecimal(txtopeningbalance.Text);
                }
                else
                {
                    //value ="Debit";
                    Credit = 0;
                    Debit=Convert.ToDecimal(txtopeningbalance.Text);
                }
                //DateTime dt2 = Convert.ToDateTime(dtpODate.Text, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt2 = Convert.ToDateTime((dtpODate.Text).ToString());
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();

                SqlCommand com = new SqlCommand("sp_insertjVoucher " + cmbvouchertype.SelectedValue + "," + txtvno.Text + ",'" + dt2.ToString("MM/dd/yyyy") + "'," + cmbBankLedger.SelectedValue + "," + cmbledger.SelectedValue + ", '" + Credit + "','" + Debit + "','" + txtremarks.Text + "','" + Form1.GlobalLoginUserName.ToString() + "'", sc);
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
                        MsgBox.ShowBox("This Ledger Name already exist");
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
        }
       

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Voucher !!!", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    //if (txtsubjectid.Text == "")
                    //{
                    //    MsgBox.ShowBox("Please Click to Detail Row!!!");
                    //    return;
                    //}
                    SqlCommand objCmd = new SqlCommand("DELETE FROM TBLVOUCHER WHERE vno = " + txtvno.Text + " and vtype = " + cmbvouchertype.SelectedValue + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Voucher Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
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
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
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
            btn_create.Enabled = true;
            btn_delete.Enabled = false;
           // txthead.Text = "";
            //cmbVoucherType.Text ="-Select-";
            txtremarks.Text = "";
            txtopeningbalance.Text = "";
            Cmbcredit.Text = "Dr";
            dtpODate.Text = DateTime.Now.ToString();
                


        }
        
        private void frmLedgerName_Load(object sender, EventArgs e)
        {
            //LoadGrid();
            bindLedger();
            bindGroup();
            bindBankLedger();
            Cancil_form();
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
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("sprocGetvno ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@vtype", Convert.ToInt32(cmbvouchertype.SelectedValue));
            


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

        private void txtvdes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvno_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblvoucher where  vno = " + txtvno.Text + " and vtype = " + cmbvouchertype.SelectedValue + " and ledgerid != 1 ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                cmbledger.SelectedValue = dt.Rows[0]["ledgerid"].ToString();
                if (Convert.ToDecimal(dt.Rows[0]["credit"].ToString()) > 0)
                {
                    txtopeningbalance.Text = dt.Rows[0]["credit"].ToString();
                    Cmbcredit.Text = "Cr";
                }
                else
                {
                    txtopeningbalance.Text = dt.Rows[0]["Debit"].ToString();
                    Cmbcredit.Text = "Dr";
                }
                dtpODate.Text = dt.Rows[0]["vdate"].ToString();
                txtremarks.Text = dt.Rows[0]["description"].ToString();
                btn_create.Enabled = false;
                btn_delete.Enabled = true;
            }
            else
            {
                txtremarks.Text = "";
                txtopeningbalance.Text = "0.00";
                cmbledger.SelectedValue = 0;
                btn_create.Enabled = true;
                btn_delete.Enabled = false ;
                
            }

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
    }
}
