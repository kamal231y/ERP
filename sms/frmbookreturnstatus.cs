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
    public partial class frmbookreturnstatus : Form
    {
        string da;
        string GroupNo,ss;
        string roll;
        string pubId, itmId;
        string barcode,SUB;
        SqlDataReader dr;
        public frmbookreturnstatus()
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



        private void bindbarcode()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from BMaster_T  order by  barcode_no", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["barcode_no"] = "-Select-";
            row["barcode_no"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbBarCode.DataSource = dt;
            cmbBarCode.DisplayMember = "barcode_no";
            cmbBarCode.ValueMember = "barcode_no";
        }

        private void BindSubject()
        {
            string barcode;
            if (cmbBarCode.Text == "" || cmbBarCode.Text == "-Select-")
            {
                barcode = "%";
            }
            else
            {
                barcode = cmbBarCode.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_BMaster where  barcode_no like '" + barcode + "' ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["subject_name"] = 0;
            row["sub_desc"] = "-Select-";
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_subject.DataSource = ds.Tables[0];
            cmb_subject.DisplayMember = "sub_desc";
            cmb_subject.ValueMember = "subject_name";
        }
       private void Cancil_form()
        {
          
            cmbBarCode.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {



           if (cmbBarCode.Text == "" || cmbBarCode.Text == "Select")
           {
               barcode = "%";
           }
           else
           {
               barcode = cmbBarCode.Text;
           }
           if (cmb_subject.Text == "" || cmb_subject.Text == "-Select-" || cmb_subject.Text == "0")
           {
               SUB = "%";
           }
           else
           {
               SUB = cmb_subject.SelectedValue.ToString();
           }

           if (comboBox1.Text == "" || comboBox1.Text == "Select" || comboBox1.Text == "-Select-" || comboBox1.Text == "0")
           {
               itmId = "%";
           }
           else
           {
               itmId = comboBox1.SelectedValue.ToString();
           }
           if (cmbSaleStatus.Text == "All" || cmbSaleStatus.Text == "Select")
           {
               ss = "%";
           }
           else if (cmbSaleStatus.Text == "Cancelled")
           {
               ss = "1";
           }
           else
           {
               ss = "0";
           }

           crybookreturnstatus.dt1 = dateTimePicker1.Value;
           crybookreturnstatus.dt2 = dateTimePicker2.Value;
           crybookreturnstatus.barcode = barcode;
           crybookreturnstatus.SUB = SUB;
           crybookreturnstatus.ss = ss;
           crybookreturnstatus.itmId = itmId;
           crybookreturnstatus obj = new crybookreturnstatus();
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
            bindbarcode();
            BindSubject();
            
        }
        private void bindItem()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select  ITM_ID,ITM_DESC from View_Stock order by ITM_DESC", sc);
            // SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID, CLS_DESC  as Class  from CLASS_T  order by CLS_Sequence ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["ITM_ID"] = 0;
            row["ITM_DESC"] = "-Select-";

            ds.Tables[0].Rows.InsertAt(row, 0);

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "ITM_DESC";
            comboBox1.ValueMember = "ITM_ID";
        }
        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            bindData();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbVenCode_Leave(object sender, EventArgs e)
        {
           
        }        
    }
}