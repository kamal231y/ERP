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
    public partial class ReceiptDetailReport : Form
    {
        string hostel;
        string classname; string roll; string fee;
        SqlDataReader dr;
        public ReceiptDetailReport()
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

        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
        }
       private void Cancil_form()
        {
          
            cmbclass.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {

           {
               
               
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbclass.SelectedValue.ToString();
               }

               if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
               {
                   roll = "%";
               }
               else
               {
                   roll = cmb_rollno.SelectedValue.ToString();
               }

               if (combofee_type.Text == "" || combofee_type.Text == "Select" || combofee_type.Text == "0")
               {
                   fee = "%";
               }
               else
               {
                   fee = combofee_type.Text;
               }
               if (rdbothhostel.Checked)
               {
                   hostel = "%";
               }
               else
               {
                   if (radiohostler.Checked)
                   {
                       hostel = "Paid";

                   }
                   else
                   {
                       hostel = "Cancel";
                   }
               }
               cryReceiptDetailReport.dt1 = dateTimePicker1.Value;
               cryReceiptDetailReport.dt2 = dateTimePicker2.Value;
               cryReceiptDetailReport.classid = classname;
               cryReceiptDetailReport.hostel = hostel;
               cryReceiptDetailReport.roll = roll;
               cryReceiptDetailReport.fee = fee;
               cryReceiptDetailReport obj = new cryReceiptDetailReport();
               obj.ShowDialog();

           }
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

        private void ReceiptDetailReport_Load(object sender, EventArgs e)
        {
            bindclass(); bindfee();
        }

        private void cmbclass_Leave(object sender, EventArgs e)
        {
            bindrollno();
        }

        private void bindrollno()
        {
            string StateName;
            if (cmbclass.Text == "-Select-" || cmbclass.Text == "" || cmbclass.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmbclass.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where stc_sec_id =  '" + StateName + "' order by  ADMNO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["ADMNO"] = "0";
            row["STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "ADMNO";
            cmb_rollno.ValueMember = "STU_ID";
        }

        private void bindfee()
        {
           

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from fee_t order by fee_id desc", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["fee_desc"] = "0";
            row["fee_desc"] = 0;
            dt.Rows.InsertAt(row, 0);
            combofee_type.DataSource = dt;
            combofee_type.DisplayMember = "fee_desc";
            combofee_type.ValueMember = "fee_desc";
        }

        
       
    }
}