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
    public partial class frmBookReturn : Form
    {
        decimal Credit;
        decimal Debit;
        decimal value;
        public frmBookReturn()
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
            LoadGrid();
            dataGridView1.Columns["Name"].Width = 40;
            dataGridView1.Columns["Description"].Width = 100;
            //dataGridView1.Columns["ClassName"].Width = 60;
            dataGridView1.Columns["Ledgerdate"].Width = 60;
            dataGridView1.Columns["GName"].Width = 60;
            //dataGridView1.Columns["userid"].Width = 60;
            dataGridView1.Columns["date"].Width = 80;
            dataGridView1.Columns["terminal"].Width = 100;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblLedger ORDER BY Name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindGroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblGroup", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["GName"] = "-Select-";
            row["Gid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbGroup.DataSource = ds.Tables[0];
            cmbGroup.DisplayMember = "GName";
            cmbGroup.ValueMember = "Gid";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
        
            if (cmbGroup.Text == "-Select-" || cmbGroup.Text == "")
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
              
                if (Cmbcredit.Text == "Credit")
                {
                    value = Convert.ToDecimal(txtopeningbalance.Text);
                    Debit = 0;
                   // Credit = Convert.ToDecimal(txtopeningbalance.Text);
                }
                
                DateTime dt1 = Convert.ToDateTime(dtpODate.Text);
               // DateTime dt2 = Convert.ToDateTime(dtpODate.Text, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbGroup.SelectedValue.ToString() == "")
                {
                    MsgBox.ShowBox("Type should not be blank");
                    return;
                }
                SqlCommand com = new SqlCommand("sproctblLedgerInsert " + cmbGroup.SelectedValue + ",'" + txthead.Text + "'," + Convert.ToDecimal(txtopeningbalance.Text) + ",'" + dt1 + "','" + value + "','" + txtremarks.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                   // if (dr.RecordsAffected > 0)
                    //{
                        MsgBox.ShowBox("Ledger Created...");
                        Cancil_form();
                        LoadGrid();
                    //}
                    //else
                    //{
                    //    MsgBox.ShowBox("This Ledger Name already exist");
                    //}

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
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (cmbGroup.Text == "-Select-" || cmbGroup.Text == "")
            {
                MessageBox.Show("Please select Group");
                return;
            }
            if (Cmbcredit.Text == "-Select-" || Cmbcredit.Text == "")
            {
                MessageBox.Show("Please select Credit or Debit");
                return;
            }   

            DateTime dt2 = Convert.ToDateTime(dtpODate.Text, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtsubjectid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }

            if (cmbGroup.SelectedValue.ToString() == "")
            {
                MsgBox.ShowBox("State Should not be blank...");
            }
            else
            {
              //  int value;
                if (Cmbcredit.Text == "Credit")
                {
                    value = Convert.ToDecimal(txtopeningbalance.Text);
                }
                else
                {
                    value = Convert.ToDecimal(txtopeningbalance.Text);
                }

                SqlCommand com = new SqlCommand("update_tblLedger '" + txtsubjectid.Text + "','" + txthead.Text + "','" + txtremarks.Text + "','" + cmbGroup.SelectedValue + "', '" + Convert.ToDecimal(txtopeningbalance.Text) + "','" + value+ "','" + dt2 + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Ledger Updated...");
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtsubjectid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblLedger WHERE id=" + txtsubjectid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Ledger Deleted Successfully!!!");
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
           
            txthead.Text = "";
            cmbGroup.Text ="-Select-";
            txtremarks.Text = "";
            txtopeningbalance.Text = "";
            Cmbcredit.Text = "-Select-";
            dtpODate.Text = DateTime.Now.ToString();
                


        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                cmbGroup.Text = dataGridView1.CurrentRow.Cells["GName"].Value.ToString();
                txthead.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["Credit"].Value.ToString() == "0")
                {
                    txtopeningbalance.Text = dataGridView1.CurrentRow.Cells["Credit"].Value.ToString();
                }
                else 
                {
                    txtopeningbalance.Text = dataGridView1.CurrentRow.Cells["Debit"].Value.ToString();
                }
               // Cmbcredit.Text = dataGridView1.CurrentRow.Cells["DRDrType"].Value.ToString();
                txtsubjectid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                txtremarks.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
                dtpODate.Text = dataGridView1.CurrentRow.Cells["LedgerDate"].Value.ToString();
                //if (dataGridView1.CurrentRow.Cells["Type"].Value.ToString() == "Credit")
                //{
                //    Cmbcredit.SelectedValue = dataGridView1.CurrentRow.Cells["Type"].Value.ToString();
                //}
                //else
                //{
                //    Cmbcredit.Text = dataGridView1.CurrentRow.Cells["Type"].Value.ToString();
                //}




                    //rbgrade.Checked =Convert.ToBoolean( dataGridView1.CurrentRow.Cells["SUB_EVO_ID"].Value.ToString());
                    //rbOSub.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_TYPE_ID"].Value.ToString());

            }
        }

        private void frmLedgerName_Load(object sender, EventArgs e)
        {
            LoadGrid();
            bindGroup();
            Cancil_form();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
