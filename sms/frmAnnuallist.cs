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
using System.Collections;

namespace sms
{
    public partial class frmAnnuallist : Form
    {
        string stuid;
        public frmAnnuallist()
        {
            InitializeComponent();
            bindclass(); 
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "class";
            cmbClass.ValueMember = "SEC_ID";
        }
    

      



        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbClass.SelectedValue = 0;
       
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            //bindclass(); bindExam();
        }
  
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StateName;
            if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmbClass.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
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

      

      

        private void button1_Click(object sender, EventArgs e)
        {

             if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            {
                MsgBox.ShowBox("Please Select class ");
                return;
            }
                     
            else
            {
            string str1; string str2;
            string str3;
            lblclass.Text = "0";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
                str1 = "%";
            else
                str1 = cmbClass.SelectedValue.ToString();
           
            if (cmb_rollno.Text == "" || cmb_rollno.Text == "0" || cmb_rollno.Text == "System.Data.DataRowView")
                str3 = "%";
            else
                str3 = cmb_rollno.SelectedValue.ToString();

            DataTable dt = new DataTable();
         
                        
                string examT;
                cryannualcrosslist.Class = str1;
                cryannualcrosslist.stuid = str3;

                cryannualcrosslist obj = new cryannualcrosslist();             
                obj.ShowDialog();
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

   

   




    }
         
}
