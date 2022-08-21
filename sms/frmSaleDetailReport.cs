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
    public partial class frmSaleDetailReport : Form
    {
        string da;
        string GroupNo,ss;
        string roll;
        string pubId, itmId;
        SqlDataReader dr;
        public frmSaleDetailReport()
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



        private void bindrollno()
        {
            string StateName;
            if (cmbVenCode.Text == "-Select-" || cmbVenCode.Text == "" || cmbVenCode.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmbVenCode.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID =  '" + StateName + "' order by  STC_ROLL_NO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["STC_ROLL_NO"] = "0";
            row["STC_STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
        }

        private void BindVendor()
        {


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
            //SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID, CLS_DESC  as Class  from CLASS_T  order by CLS_Sequence ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "-Select-";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbVenCode.DataSource = ds.Tables[0];
            cmbVenCode.DisplayMember = "Class";
            cmbVenCode.ValueMember = "SEC_ID";
        }
       private void Cancil_form()
        {
          
            cmbVenCode.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {



           if (cmbVenCode.Text == "" || cmbVenCode.Text == "Select" || cmbVenCode.Text == "-Select-")
           {
               GroupNo = "%";
           }
           else
           {
               GroupNo = cmbVenCode.SelectedValue.ToString();
           }
           if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
           {
               roll = "%";
           }
           else
           {
               roll = cmb_rollno.SelectedValue.ToString();
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

           cryDetailSale.dt1 = dateTimePicker1.Value;
           cryDetailSale.dt2 = dateTimePicker2.Value;
           cryDetailSale.GroupNo = GroupNo;
           cryDetailSale.roll = roll;
           cryDetailSale.ss = ss;
           cryDetailSale.itmId = itmId;
           cryDetailSale obj = new cryDetailSale();
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


        private void frmSaleDetailReport_Load(object sender, EventArgs e)
        {
            BindVendor();
            bindItem();
            BindSaleNo();
            
        }

        private void BindSaleNo()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select  SALE_NO from View_Sale order by SALE_NO", sc);
            // SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID, CLS_DESC  as Class  from CLASS_T  order by CLS_Sequence ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SALE_NO"] = 0;
           // row["SALE_NO"] = "-Select-";

            ds.Tables[0].Rows.InsertAt(row, 0);

            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "SALE_NO";
            comboBox2.ValueMember = "SALE_NO";
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
            bindrollno();
        }        
    }
}