using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;

namespace sms
{
    public partial class frmdailytym : Form
    {
        string usertype;
       SqlDataReader dr;
       public frmdailytym()
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
            //cmbbilltype.SelectedIndex = 0;
            
            Cancil_form();
            Binduser();
            
        }
      
        private void Cancil_form()
        {
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbuser.Text = "Select";
           
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            Cancil_form();
            dataGridView1.DataSource = null;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

       
        private void btn_save_Click(object sender, EventArgs e)
         {
             BindGrid();
        
        }
        private void BindGrid()
        {
           
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            string str = "select sat_id ,day,RIGHT(CONVERT(VARCHAR,SAT_DATE, 100),7) as sat_date  from EMPLOYEE_ATTENDANCE_T where sat_stu_id='" + cmbuser.SelectedValue + "' and month='" + comboBox1.SelectedValue + "' order by sat_id ";
            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["sat_id"].Visible = false;
                dataGridView1.Columns["day"].Width = 150;
                dataGridView1.Columns["day"].HeaderText = "Date";
                dataGridView1.Columns["sat_date"].Width = 90;
                dataGridView1.Columns["sat_date"].HeaderText = "Time";
              
                sc.Close();





            }

        }     


        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {

                crydailytime.month =comboBox1.SelectedValue.ToString();
                crydailytime.emp_cardno = cmbuser.SelectedValue.ToString();
                crydailytime obj = new crydailytime();

                obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
               
         }

       
       
       
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void Binduser()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.EMPLOYEE_T order by emp_code", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["emp_code"] = "Select";
            row["emp_cardno"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbuser.DataSource = ds.Tables[0];
            cmbuser.DisplayMember = "emp_code";
            cmbuser.ValueMember = "emp_cardno";
        }

        private void frmRegFrmSaleDetail_Load(object sender, EventArgs e)
        {
            bindYear();
        }

        private void cmbuser_Leave(object sender, EventArgs e)
        {
           //BindGrid();
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

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "Year";
            comboBox1.ValueMember = "id";
        }
         
    }  
      
 }

