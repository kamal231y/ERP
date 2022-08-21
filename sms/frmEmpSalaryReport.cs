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
    public partial class frmEmpSalaryReport : Form
    {
        string hostel;
        string dept;
        string empName;
        SqlDataReader dr;
        public frmEmpSalaryReport()
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
            cmbDept.ValueMember = "DEPT_DESC";
        }

        private void bindEmp()
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
          
            cmbDept.Text = "-Select-";
         
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
               


               if (rdbothhostel.Checked)
               {
                   hostel = "%";
               }
               else
               {
                   if (radiohostler.Checked)
                   {
                       hostel = "H";

                   }
                   else
                   {
                       hostel = "N";
                   }
               }

               //if (classname == "1" || classname == "2" || classname == "3" || classname == "4" || classname == "5" || classname == "6" || classname == "7" || classname == "8" || classname == "9" || classname == "10")
               //{
                   //cryFeeDetailReport.dt1 = dateTimePicker1.Value;
                   //cryFeeDetailReport.dt2 = dateTimePicker2.Value;
               cryVehicleReport.classid = empName;
                   //cryFeeDetailReport.hostel = hostel;

               cryVehicleReport obj = new cryVehicleReport();
                   
                   obj.ShowDialog();
               //}
               //else
               //{
               //}

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
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            if (cmbDept.Text == "" || cmbDept.Text == "Select")
            {
                dept = "%";
            }
            else
            {
                dept = cmbDept.SelectedValue.ToString();
            }

            if (cmbEmp.Text == "" || cmbEmp.Text == "Select")
            {
                empName = "%";
            }
            else
            {
                empName = cmbEmp.SelectedValue.ToString();
            }

            cryEmpSalaryReport.empName = empName;
            cryEmpSalaryReport.dept = dept;
            cryEmpSalaryReport.dt1 = dt1;
            cryEmpSalaryReport.dt2 = dt2;
            cryEmpSalaryReport obj = new cryEmpSalaryReport();

            obj.ShowDialog();
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpSalaryReport_Load(object sender, EventArgs e)
        {
            bindEmp();
            Binddept();
        }

       
       

       
       
    }
}