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
    public partial class frmLedgerDetail : Form
    {
        decimal Credit;
        decimal Debit;
        decimal value;
        public frmLedgerDetail()
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
            //SqlDataAdapter da = new SqlDataAdapter("select * from View_accountmaster ORDER BY Name", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindGroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblledger", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "-Select-";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbGroup.DataSource = ds.Tables[0];
            cmbGroup.DisplayMember = "Name";
            cmbGroup.ValueMember = "id";
        }

        private void showstudentreport()
        {

            

            if (cmbGroup.Text != "")
            {
                DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("sprocledgerstatement ", sc);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@min1", dt1.ToString("MM/dd/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@max1", dt2.ToString("MM/dd/yyyy"));
                da.SelectCommand.Parameters.AddWithValue("@ledgerid", cmbGroup.SelectedValue);
                sc.Open();
                DataTable dt = new DataTable();
                    da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns["SNo"].Visible = true;
                    dataGridView1.Columns["sno"].Width = 50;
                    dataGridView1.Columns["transactionid"].HeaderText = "VNO";
                    dataGridView1.Columns["transactionid"].Width = 100;
                    //dataGridView1.Columns["Vtype"].Width = 100;
                    dataGridView1.Columns["vdate"].Width = 100;
                    dataGridView1.Columns["Description"].Width = 100;
                    dataGridView1.Columns["Credit"].Width = 150;
                    dataGridView1.Columns["Debit"].Width = 120;
                    dataGridView1.Columns["balance"].Width = 130;
                }

                else
                {
                    dataGridView1.DataSource = DBNull.Value;


                }
               
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string values1=(row.Cells[6].Value).ToString();
                    if (values1.Substring((values1.Length-2),2)=="Cr")
                    {
                        //row.DefaultCellStyle.BackColor = Color.Red;
                        row.Cells[6].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                       // row.DefaultCellStyle.BackColor = Color.Green;
                        row.Cells[6].Style.ForeColor = Color.Green;
                    }
                }
                sc.Close();
            }
        }

           

        

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
                
        }
        
        
        private void frmLedgerName_Load(object sender, EventArgs e)
        {
            
            bindGroup();
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {

            
            crystatement.dt1 = dateTimePicker1.Value;
            crystatement.dt2 = dateTimePicker2.Value;
             crystatement.ledgername =cmbGroup.Text;
                 
            crystatement obj = new crystatement();
            obj.ShowDialog();
        }
    }
}
