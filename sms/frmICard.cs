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
    public partial class frmICard : Form
    {        string classname;
    int A;
        SqlDataReader dr;
        public frmICard()
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
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC +' '+ SEC_DESC) as Class ,* from View_fillfeecollection order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
           
        }
        private void BindSession()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from session_t ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["Class"] = "-Select-";
            //row["SEC_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmbSession.DataSource = ds.Tables[0];
            cmbSession.DisplayMember = "session";
            cmbSession.ValueMember = "id";
           
        }
        private void BindAdmno()
        {
            string ADM;
            if (cmbclass.Text == "-Select-" || cmbclass.Text == "" || cmbclass.Text == "System.Data.DataRowView")
            {
                ADM = "0";
            }
            else
            {
                ADM = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_T where STU_CLS_ID='" + ADM + "' order by stu_adm_no", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Stu_Adm_no"] = "-Select-";
            row["Stu_Adm_no"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbAdmNo.DataSource = ds.Tables[0];
            cmbAdmNo.DisplayMember = "Stu_Adm_no";
            cmbAdmNo.ValueMember = "Stu_Adm_no";

        }
       
        private void bindroll()
        {               
                string rn;
                if (cmbclass.Text == "-Select-" || cmbclass.Text == "" || cmbclass.Text == "System.Data.DataRowView")
                {
                    rn = "0";
                }
                else
                {
                    rn = cmbclass.SelectedValue.ToString();
                }

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from View_AdmitCard3 where SEC_ID='" + rn + "' order by STC_ROLL_NO", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataRow row = dt.NewRow();
                    row["STC_ROLL_NO"] = "0";
                    row["STU_ID"] = 0;
                    dt.Rows.InsertAt(row, 0);
                    cmbRoll.DataSource = dt;
                    cmbRoll.DisplayMember = "STC_ROLL_NO";
                    cmbRoll.ValueMember = "STU_ID";
                }
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

               cryICard.classid = classname;
               string Admno, Roll,session;
               cryICard.session = cmbSession.Text;
               cryICard.type = A;
               cryICard.Admno = cmbAdmNo.SelectedValue.ToString();
               cryICard obj = new cryICard();
               obj.ShowDialog();
           }
       }

       private void bind_setting()
       {
           {
               try
               {
                   dataclass dc = new dataclass();
                   SqlConnection sc = dc.con();
                   SqlDataAdapter da = new SqlDataAdapter("SELECT type FROM SETTING_T ", sc);
                   sc.Open();
                   DataTable dt = new DataTable();
                   da.Fill(dt);

                   A = Convert.ToInt16(dt.Rows[0]["type"].ToString());

               }
               catch (Exception EX) { }

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

        private void cmbclass_Leave(object sender, EventArgs e)
        {
            //bindroll();
            BindAdmno();
        }

        private void frmICard_Load(object sender, EventArgs e)
        {
            bindclass(); 
            //BindAdmno();
            BindSession();

            bind_setting();
        }
      
    }
}