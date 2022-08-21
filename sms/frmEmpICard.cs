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
    public partial class frmEmpICard : Form
    {        string classname;
        SqlDataReader dr;
        public frmEmpICard()
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
        //private void bindclass()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC +' '+ SEC_DESC) as Class ,* from View_fillfeecollection order by SEC_ID ", sc);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    DataRow row = ds.Tables[0].NewRow();
        //    row["Class"] = "-Select-";
        //    row["SEC_ID"] = 0;
        //    ds.Tables[0].Rows.InsertAt(row, 0);

        //    cmbDept.DataSource = ds.Tables[0];
        //    cmbDept.DisplayMember = "Class";
        //    cmbDept.ValueMember = "SEC_ID";
           
        //}
       
        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "DEPT_DESC";
            cmbDept.ValueMember = "DEPT_ID";
        }

        private void BindName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select EMP_CODE, EMP_NAME  from EMPLOYEE_T order by EMP_NAME ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_CODE"] = 0;
            row["EMP_NAME"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbEmp.DataSource = ds.Tables[0];
            cmbEmp.DisplayMember = "EMP_NAME";
            cmbEmp.ValueMember = "EMP_CODE";
        }

        private void Cancil_form()
        {

            cmbEmp.Text = "-Select-";

           

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
           
       }
       private void bindData()
       {

           {
               if (cmbDept.Text == "" || cmbDept.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbDept.SelectedValue.ToString();
               }

               cryEmpICard.Dept = classname;
               string Admno, Roll, session;
               cryEmpICard.Admno = cmbEmp.SelectedValue.ToString();
               cryEmpICard obj = new cryEmpICard();
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

            //bindData();
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDept_Leave(object sender, EventArgs e)
        {
            //bindroll();
            //Binddept();
            //BindName();
        }

        private void frmICard_Load(object sender, EventArgs e)
        {
            //bindclass(); 
            Binddept();
            BindName();
        }

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
      
    }
}