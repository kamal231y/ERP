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
    public partial class EmpExp : Form
    {
        string stu_id;
        SqlDataReader dr;
        public EmpExp()
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
        private void BindAdmno()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from EMPLOYEE_T emp_code", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["emp_code"] = "0";
            row["emp_code"] = "-Select-";
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbAdmNo.DataSource = ds.Tables[0];
            cmbAdmNo.DisplayMember = "emp_code";
            cmbAdmNo.ValueMember = "emp_code";

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
                SqlDataAdapter da = new SqlDataAdapter("select AdmNo ,Roll  from View_studentReport  where STC_SEC_ID ='" + rn + "' order by Roll", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                  row["Roll"] = "0";
                row["AdmNo"] = "-Select-";
                dt.Rows.InsertAt(row, 0);
                         cmbRoll.DataSource = dt;
                    cmbRoll.DisplayMember = "Roll";
                    cmbRoll.ValueMember = "AdmNo";
           
        }
       private void Cancil_form()
        {
          
            cmbclass.Text = "-Select-";
            cmbAdmNo.Text = "-Select-";
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {
           DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
           DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
           stu_id = cmbAdmNo.Text;
           cryEmpExp.EMP_ID = cmbAdmNo.Text;
           cryEmpExp.dt1 = dateTimePicker1.Value;
            
           cryEmpExp.dt2 = dateTimePicker2.Value;
           cryEmpExp obj = new cryEmpExp();
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

        private void frmBusRouteReport_Load(object sender, EventArgs e)
        {
            bindclass(); BindAdmno();
        }


        private void cmbclass_Leave(object sender, EventArgs e)
        {
            bindroll();
        }

       
       
    }
}