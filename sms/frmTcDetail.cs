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
using sms.BLClass;       

namespace sms
{
    public partial class frmTcDetail : Form
    {
        string hostel;
        string classname;
        SqlDataReader dr;
        public frmTcDetail()
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

        //private void bindAdmNo()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    DataRow row = ds.Tables[0].NewRow();
        //    row["SEC_ID"] = 0;
        //    row["Class"] = "Select";
        //    ds.Tables[0].Rows.InsertAt(row, 0);
        //    cmbclass.DataSource = ds.Tables[0];
        //    cmbclass.DisplayMember = "Class";
        //    cmbclass.ValueMember = "SEC_ID";
        //}
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select distinct TC_curr_cls  from TC_T order by TC_curr_cls ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["TC_curr_cls"] = 0;
           row["TC_curr_cls"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "TC_curr_cls";
            cmbclass.ValueMember = "TC_curr_cls";
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
           string ADM ,tc;
           if (cmbclass.Text == "" || cmbclass.Text == "Select")
           {
               classname = "%";
           }
           else
           {
               classname = cmbclass.SelectedValue.ToString();
           }
           if (cmbAdmNo.Text == "" || cmbAdmNo.Text == "Select")
           {
               ADM = "%";
           }
           else
           {
               ADM = cmbAdmNo.Text.ToString();
           }
           if (txtTCno.Text == "" || txtTCno.Text == "Select")
           {
               tc = "%";
           }
           else
           {
               tc = txtTCno.Text.Trim();
           }
                  cryTCDetail.dt1 = dateTimePicker1.Value;
                  cryTCDetail.dt2 = dateTimePicker2.Value;
                  cryTCDetail.tcno = tc;
                  cryTCDetail.admno = ADM;
                 cryTCDetail.cls = classname;
                 cryTCDetail obj = new cryTCDetail();
           obj.ShowDialog();
       }
       private void bindData1()
       {
           string ADM;
           if (cmbclass.Text == "" || cmbclass.Text == "Select")
           {
               classname = "%";
           }
           else
           {
               classname = cmbclass.SelectedValue.ToString();
           }
           if (cmbAdmNo.Text == "" || cmbAdmNo.Text == "Select")
           {
               ADM = "%";
           }
           else
           {
               ADM = cmbAdmNo.SelectedValue.ToString();
           }
           
          
          cryTCALL.tcno = ADM;
          cryTCALL.cls = classname;
           cryTCALL obj = new cryTCALL();
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
        private void frmTcDetail_Load(object sender, EventArgs e)
        {
            bindclass();
        }

        private void cmbAdmNo_Leave(object sender, EventArgs e)
        {
            

        }

        private void cmbclass_Leave(object sender, EventArgs e)
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
            SqlDataAdapter da = new SqlDataAdapter("select * from TC_T where TC_CURR_CLS='" + ADM + "' order by TC_ADM_NO", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["TC_ADM_NO"] = "0";
            row["TC_ADM_NO"] = "Select";
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbAdmNo.DataSource = ds.Tables[0];
            cmbAdmNo.DisplayMember = "TC_ADM_NO";
            cmbAdmNo.ValueMember = "TC_ADM_NO";
        }
        }  
    }
