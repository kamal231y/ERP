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
    public partial class frmAttendanceDetailreport : Form
    {
        string da;
        string roll,secID,tp,ta,tl,twd;
        SqlDataReader dr;
        public frmAttendanceDetailreport()
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
            if (cmbClass.Text == "-Select-" || cmbClass.Text == "" || cmbClass.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmbClass.SelectedValue.ToString();
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

        private void BindClass()
        {


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_ID ", sc);
            //SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID, CLS_DESC  as Class  from CLASS_T  order by CLS_Sequence ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "-Select-";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }
       private void Cancil_form()
        {
          
            cmbClass.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {



           if (cmbClass.Text == "" || cmbClass.Text == "Select" || cmbClass.Text == "-Select-")
           {
               secID = "%";
           }
           else
           {
               secID = cmbClass.SelectedValue.ToString();
           }
           if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
           {
               roll = "%";
           }
           else
           {
               roll = cmb_rollno.SelectedValue.ToString();
           }


           cryAttendanceDetail.dt1 = dateTimePicker1.Value;
           cryAttendanceDetail.dt2 = dateTimePicker2.Value;
           cryAttendanceDetail.secID = secID;
           cryAttendanceDetail.roll = roll;
           cryAttendanceDetail.tp = txtTp.Text;
           cryAttendanceDetail.ta = txtTA.Text;
           cryAttendanceDetail.tl = txtTL.Text;
           cryAttendanceDetail.twd = txtTwd.Text;
           cryAttendanceDetail obj = new cryAttendanceDetail();
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
            BindClass();
            
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbVenCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSaleStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            
        }
        private void BindTotals()
        {
            calculateTp();
            calculateTL();
            calculateTA();
            calculateTwd();
        }
        private void calculateTp()
        {
            if (cmbClass.Text == "" || cmbClass.Text == "Select" || cmbClass.Text == "-Select-")
            {
                secID = "%";
            }
            else
            {
                secID = cmbClass.SelectedValue.ToString();
            }
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
            {
                roll = "%";
            }
            else
            {
                roll = cmb_rollno.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  count(att_type) as tp from view_Attendance where cast (sat_date as date) between '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'  and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "'  and  stc_sec_id like '" + secID + "' and stu_id like '" + roll + "'  and att_type='p' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtTp.Text = dt.Rows[0]["tp"].ToString();
            }
        }
        private void calculateTA()
        {
            if (cmbClass.Text == "" || cmbClass.Text == "Select" || cmbClass.Text == "-Select-")
            {
                secID = "%";
            }
            else
            {
                secID = cmbClass.SelectedValue.ToString();
            }
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
            {
                roll = "%";
            }
            else
            {
                roll = cmb_rollno.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  count(att_type) as ta from view_Attendance where cast (sat_date as date) between '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'  and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "'  and  stc_sec_id like '" + secID + "' and stu_id like '" + roll + "'  and att_type='A' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtTA.Text = dt.Rows[0]["ta"].ToString();
            }
        }

        private void calculateTL()
        {
            if (cmbClass.Text == "" || cmbClass.Text == "Select" || cmbClass.Text == "-Select-")
            {
                secID = "%";
            }
            else
            {
                secID = cmbClass.SelectedValue.ToString();
            }
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
            {
                roll = "%";
            }
            else
            {
                roll = cmb_rollno.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  count(att_type) as tL from view_Attendance where cast (sat_date as date) between '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'  and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "'  and  stc_sec_id like '" + secID + "' and stu_id like '" + roll + "' and att_type='L' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtTL.Text = dt.Rows[0]["tL"].ToString();
            }
        }
        private void calculateTwd()
        {
            if (cmbClass.Text == "" || cmbClass.Text == "Select" || cmbClass.Text == "-Select-")
            {
                secID = "%";
            }
            else
            {
                secID = cmbClass.SelectedValue.ToString();
            }
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select" || cmb_rollno.Text == "0")
            {
                roll = "%";
            }
            else
            {
                roll = cmb_rollno.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  count(att_type) as twd from view_Attendance where cast (sat_date as date) between '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'  and '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "'  and  stc_sec_id like '" + secID + "' and stu_id like '" + roll + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtTwd.Text = dt.Rows[0]["twd"].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            BindTotals();
        }

    }
}