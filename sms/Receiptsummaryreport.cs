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
    public partial class Receiptsummaryreport : Form
    {
        string hostel;
        string classname, mode;
        SqlDataReader dr;
        public Receiptsummaryreport()
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
        private void bindAccount()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblmop order by mop", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["id"] = 0;
            row["mop"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_mode.DataSource = ds.Tables[0];
            cmb_mode.DisplayMember = "mop";
            cmb_mode.ValueMember = "id";
            
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
           string adm, usertype;
           int concession;
           {
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbclass.SelectedValue.ToString();
               }
               if (cmbadm.Text == "" || cmbadm.Text == "Select")
               {
                   adm = "%";
               }
               else
               {
                   adm = cmbadm.SelectedValue.ToString();
               }
               if (cmb_mode.Text == "" || cmb_mode.Text == "Select")
               {
                   mode = "%";
               }
               else
               {
                   mode = cmb_mode.Text.ToString();
               }
               if (comboBox1.Text == "All" || comboBox1.Text == "")
               {
                   concession = 1;
               }
               else
               {
                   concession = 2;
               }
               if (sms.Form1.GlobalLoginUserType.ToString() == "ADMINISTRATOR")
               {
                   if (cmbuser.Text == "-Select-" || cmbuser.Text == "")
                   {
                       usertype = "%";
                   }
                   else
                   {
                       usertype = cmbuser.Text.ToString();
                   }

               }
               else
               {
                   usertype = sms.Form1.GlobalLoginUserName.ToString();
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
               Reportreceiptsummary.dt1 = dateTimePicker1.Value;
               Reportreceiptsummary.dt2 = dateTimePicker2.Value;
               Reportreceiptsummary.classid = classname;
               Reportreceiptsummary.hostel = hostel;
               Reportreceiptsummary.mode = mode;
               Reportreceiptsummary.admno = adm;
               Reportreceiptsummary.usertype = usertype;
               Reportreceiptsummary.concession = concession;
               Reportreceiptsummary obj = new Reportreceiptsummary();
               obj.ShowDialog();

           }
       }

        private void btnCancel_Click(object sender, EventArgs e)
        {
          
            Cancil_form();
            
        }
        private void Binduser()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd = new SqlCommand("select id, username from tblRights   order by username", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["username"] = "-Select-";
            row["id"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbuser.DataSource = dt;
            cmbuser.DisplayMember = "username";
            cmbuser.ValueMember = "id";
            sc.Close();
        }
        private void BindAdmno()
        {
            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where stu_status='y' and stu_cls_id like '" + classname + "'  order by admno", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Admno"] = "0";
            row["Admno"] = "Select";
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbadm.DataSource = ds.Tables[0];
            cmbadm.DisplayMember = "Admno";
            cmbadm.ValueMember = "Admno";

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

        private void btMaximize_Click(object sender, EventArgs e)
        {
            
        }

        private void Receiptsummaryreport_Load(object sender, EventArgs e)
        {
            bindclass(); bindAccount();
            if (sms.Form1.GlobalLoginUserType.ToString() == "ADMINISTRATOR")
            {
                label6.Visible = true;
                cmbuser.Visible = true;
                Binduser();
            }
            else
            {
                label6.Visible = false;
                cmbuser.Visible = false;
            }
        }

        private void cmbclass_Leave(object sender, EventArgs e)
        {
            BindAdmno();
        }
    }
}