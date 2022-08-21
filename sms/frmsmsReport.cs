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
    public partial class frmsmsReport : Form
    {
        string usertype;
       SqlDataReader dr;
       public frmsmsReport()
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
            if (cmbuser.Text == "-Select-" || cmbuser.Text == "")
            {
                usertype = "%";
            }
            else
            {
                usertype = cmbuser.Text;
            }



            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            string str = "select  id,Name,Mobile,Msg,Date  from View_smsreport where status='" + usertype + "' order by id desc ";
            string str1 = "select sum(NOOFMSG) AS TOTAL  from View_smsreport where status='" + usertype + "'  ";
            if (str != "")
            {
                 SqlDataAdapter da1 = new SqlDataAdapter(str1, sc);
                System.Data.DataTable dtb = new System.Data.DataTable();
                da1.Fill(dtb);
                if(dtb.Rows.Count>0)
                {
                    label5.Text = "Total No Of SMS SEND : " + dtb.Rows[0]["Total"].ToString();
                }
                else
                {
                    label5.Text = "Total No Of SMS SEND :0";

                }

                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
               
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
               
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["Name"].Width = 150;
                dataGridView1.Columns["Name"].HeaderText = "Name";
                dataGridView1.Columns["Mobile"].Width = 90;
                dataGridView1.Columns["Mobile"].HeaderText = "Mobile";
                dataGridView1.Columns["Msg"].Width = 650;
                dataGridView1.Columns["Msg"].HeaderText = "Msg";
                dataGridView1.Columns["Date"].Width = 120;
                dataGridView1.Columns["Date"].HeaderText = "Date";
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

                crysmssummary.status= usertype;
                crysmssummary obj = new crysmssummary();

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
            sc.Open();
            SqlCommand cmd = new SqlCommand("select * from tblsmsstatus order by id ", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["status"] = "-Select-";
            row["id"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbuser.DataSource = dt;
            cmbuser.DisplayMember = "status";
            cmbuser.ValueMember = "id";
            sc.Close();
        }

        private void frmRegFrmSaleDetail_Load(object sender, EventArgs e)
        {
           // Binduser();
        }

        private void cmbuser_Leave(object sender, EventArgs e)
        {
            BindGrid();
        }
         
    }  
      
 }

