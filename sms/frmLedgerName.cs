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
    public partial class frmLedgerName : Form
    {
        validation vl = new validation();
        decimal Credit;
        decimal Debit;
        decimal value;
        public frmLedgerName()
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
            dataGridView1.Columns["Name"].Width = 100;
            dataGridView1.Columns["Description"].Width = 200;
            dataGridView1.Columns["Credit"].Width = 40;
            dataGridView1.Columns["Ledgerdate"].Width = 60;
            dataGridView1.Columns["Group"].Width = 150;
            dataGridView1.Columns["Debit"].Width = 60;
            dataGridView1.Columns["id"].Width = 1;
            dataGridView1.Columns["id"].Visible = false;
           
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id,Name,gname as 'Group',Description,Ledgerdate,Credit,Debit from View_tblLedger ORDER BY Name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["Name"].Width = 170;
            dataGridView1.Columns["Description"].Width = 200;
            dataGridView1.Columns["Credit"].Width = 40;
            dataGridView1.Columns["Ledgerdate"].Width = 60;
            dataGridView1.Columns["Group"].Width = 150;
            dataGridView1.Columns["Debit"].Width = 60;
            dataGridView1.Columns["id"].Width = 1;
            dataGridView1.Columns["id"].Visible = false;
        }
        private void bindGroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblGroup ORDER BY GNAME", sc);
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

                if (Cmbcredit.Text == "CR")
                {
                    value = Convert.ToDecimal(txtopeningbalance.Text);
                    Debit = 0;
                    // Credit = Convert.ToDecimal(txtopeningbalance.Text);
                }
                else
                {
                    value = 0;
                    Debit = Convert.ToDecimal(txtopeningbalance.Text);
                }
                System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtp11 = Convert.ToDateTime(dtpODate.Value, System.Globalization.CultureInfo.InvariantCulture);
                //DateTime dt1 = Convert.ToDateTime(dtpODate.Value, System.Globalization.CultureInfo.InvariantCulture);
               // DateTime dt2 = Convert.ToDateTime(dtpODate.Text, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbGroup.SelectedValue.ToString() == "")
                {
                    MsgBox.ShowBox("Type should not be blank");
                    return;
                }
                SqlCommand com = new SqlCommand("sproctblLedgerInsert " + cmbGroup.SelectedValue + ",'" + txthead.Text + "'," + Convert.ToDecimal(value) + "," + Convert.ToDecimal(Debit) + ",'" + dtp11.ToString("MM/dd/yyyy") + "','" + txtremarks.Text + "','" + Form1.GlobalLoginUserName.ToString() + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Ledger Created...");
                        Cancil_form();
                        InitGrid();
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
                    SqlCommand objCmd = new SqlCommand("sproctblLedgerdelete " + txtsubjectid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                    MsgBox.ShowBox(" Ledger Deleted Successfully!!!");
                    InitGrid();
                    Cancil_form();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Ledger Already Used!!!");
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
            txthead.Text = "";
            cmbGroup.Text ="-Select-";
            txtremarks.Text = "";
            txtopeningbalance.Text = "";
            Cmbcredit.Text = "CR";
            dtpODate.Text = DateTime.Now.ToString();
                


        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                cmbGroup.Text = dataGridView1.CurrentRow.Cells["Group"].Value.ToString();
                txthead.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["Credit"].Value.ToString() != "0.00")
                {
                    txtopeningbalance.Text = dataGridView1.CurrentRow.Cells["Credit"].Value.ToString();
                    Cmbcredit.Text = "CR";
                }
                else 
                {
                    txtopeningbalance.Text = dataGridView1.CurrentRow.Cells["Debit"].Value.ToString();
                    Cmbcredit.Text = "DR";
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

                   
                btn_delete.Enabled = true;
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

        private void txtopeningbalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            vl.digitonly(e);
        }

        private void button4_Click(object sender, EventArgs e)
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

                if (Cmbcredit.Text == "CR")
                {
                    value = Convert.ToDecimal(txtopeningbalance.Text);
                    Debit = 0;
                    // Credit = Convert.ToDecimal(txtopeningbalance.Text);
                }
                else
                {
                    value = 0;
                    Debit = Convert.ToDecimal(txtopeningbalance.Text);
                }

                System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtp11 = Convert.ToDateTime(dtpODate.Value, System.Globalization.CultureInfo.InvariantCulture);
                //DateTime dt1 = Convert.ToDateTime(dtpODate.Value, System.Globalization.CultureInfo.InvariantCulture);
                // DateTime dt2 = Convert.ToDateTime(dtpODate.Text, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmbGroup.SelectedValue.ToString() == "")
                {
                    MsgBox.ShowBox("Type should not be blank");
                    return;
                }
                SqlCommand com = new SqlCommand("sproctblLedgerupdate '"+txtsubjectid.Text+"'," + cmbGroup.SelectedValue + ",'" + txthead.Text + "'," + Convert.ToDecimal(value) + "," + Convert.ToDecimal(Debit) + ",'" + txtremarks.Text + "','" + Form1.GlobalLoginUserName.ToString() + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Ledger Updated...");
                        Cancil_form();
                        InitGrid();
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

        private void txthead_TextChanged(object sender, EventArgs e)
        {
            string classname;
            if (txthead.Text == "")
            {
                classname = "%";
            }
            else
            {
                classname = txthead.Text;
            }
        
            string str;

            if (txthead.Text == "" )
                str="select id,Name,gname as 'Group',Description,Ledgerdate,Credit,Debit from View_tblLedger where name like '" + classname + "%'  ORDER BY Name";

            else
                str = "select id,Name,gname as 'Group',Description,Ledgerdate,Credit,Debit from View_tblLedger where name like '" + classname + "%'  ORDER BY Name";
            //    str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name";

            
            if (str != "")
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                
               
        
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["Name"].Width = 170;
            dataGridView1.Columns["Description"].Width = 200;
            dataGridView1.Columns["Credit"].Width = 40;
            dataGridView1.Columns["Ledgerdate"].Width = 60;
            dataGridView1.Columns["Group"].Width = 150;
            dataGridView1.Columns["Debit"].Width = 60;
            dataGridView1.Columns["id"].Width = 1;
            dataGridView1.Columns["id"].Visible = false;

           
            }
        }
    }
}
