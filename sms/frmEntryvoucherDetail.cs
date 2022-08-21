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
    public partial class frmEntryvoucherDetail : Form
    {
        decimal Credit;
        decimal Debit;
        decimal value;
        public frmEntryvoucherDetail()
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
      

        private void showstudentreport()
        {

            dataGridView1.Visible = true;
            dataGridView2.Visible = false;

           
                DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("sprocledgerstatementdaywise ", sc);
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

           

        

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
                
        }
        
        
        private void frmLedgerName_Load(object sender, EventArgs e)
        {
            
          
           
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
                 
            crystatement obj = new crystatement();
            obj.ShowDialog();
        }

        private void cmbGroup_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;

            int rowIndex = dataGridView1.Rows[e.RowIndex].Index;

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select vdate,VTName,VNo,name,des1,debit,credit from viewvoucherdetail where vdate='" + Convert.ToDateTime( dataGridView1.Rows[rowIndex].Cells[1].Value).ToString("yyyy/MM/dd") + "' and Gid not in (1,6) and Vtype<>3 ", sc);
            sc.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView2.DataSource = dt;
                //dataGridView2.DataBindings();
            }
          
            
             else
                {
                    dataGridView1.DataSource = DBNull.Value;


                }
            
        }
    }
}
