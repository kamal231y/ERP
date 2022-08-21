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
    public partial class frmstockPurchaseReport : Form
    {
        string da;
        string GroupNo;

        string pubId;
        SqlDataReader dr;
        public frmstockPurchaseReport()
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
            
        }

        

        

        private void BindVendor()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblledger", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbVenCode.DataSource = ds.Tables[0];
            cmbVenCode.DisplayMember = "Name";
            cmbVenCode.ValueMember = "id";
        }
       private void Cancil_form()
        {
          
            cmbVenCode.Text = "Select";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {

           if (cmbVenCode.Text == "" || cmbVenCode.Text == "Select")
           {
               GroupNo = "%";
           }
           else
           {
               GroupNo = cmbVenCode.SelectedValue.ToString();
           }


           crystockPurchase.dt1 = dateTimePicker1.Value;
           crystockPurchase.dt2 = dateTimePicker2.Value;
           crystockPurchase.GroupNo = GroupNo;
           crystockPurchase obj = new crystockPurchase();
           obj.ShowDialog();





          

               
              
                         
           }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
          
            Cancil_form();
            
        }

  

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            bindData();
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmStockReport_Load(object sender, EventArgs e)
        {
            BindVendor();
            
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            bindData();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }     
    }
}