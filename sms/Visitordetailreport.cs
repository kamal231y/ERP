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
using sms.BLClass;
using System.Collections;
using sms.DATALAYER;
namespace sms
{
    public partial class Visitordetailreport : Form
    {
        string Department;
        string Name;
        string Designation;

        string State;
        string Mobileno;
        string City;



        SqlDataReader dr;
        public Visitordetailreport()
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

            //cmbDepartment.DataSource = ds.Tables[0];
            //cmbDepartment.DisplayMember = "DEPT_DESC";
            //cmbDepartment.ValueMember = "DEPT_ID";
        }
      
        private void BindDesign()
        {
            string str;
            if (cmbDepartment.Text == "" || cmbDepartment.Text == "Select" || cmbDepartment.Text == "System.Data.DataRowView")
                str = "%";
            else
                str = cmbDepartment.SelectedValue.ToString();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DESIGNATION_T where DEPT_ID like '" + str + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DESG_DESC"] = "Select";
            row["DESG_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbDesignation.DataSource = ds.Tables[0];
            cmbDesignation.DisplayMember = "DESG_DESC";
            cmbDesignation.ValueMember = "DESG_ID";
        }
        private void bindstate()
        {
            string emp;
           if (cmbDesignation.Text == "" || cmbDesignation.Text == "Select" || cmbDesignation.Text == "System.Data.DataRowView")
                emp = "%";
            else
                emp = cmbDesignation.SelectedValue.ToString();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from employee_t where emp_desg_id like '" + emp + "' ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["emp_name"] = "Select";
            row["emp_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbState.DataSource = ds.Tables[0];
            cmbState.DisplayMember = "emp_name";
            cmbState.ValueMember = "emp_id";
           
           
        }

       
       

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbDepartment.Text == "Select" || cmbDepartment.Text == "")
            {
                Department = "%";
            }
            else
            {
                Department = cmbDepartment.Text;
            }


            if (cmbDesignation.Text == "Select" || cmbDesignation.Text == "")
            {
                Designation = "%";
            }
            else
            {
                Designation = cmbDesignation.Text;
            }

            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                State = "%";
            }
            else
            {
                State = cmbState.Text;
            }


            cryvisitorDetail.dt1 = dateTimePicker1.Value;
            cryvisitorDetail.dt2 = dateTimePicker2.Value;
            cryvisitorDetail.intime = dtdate.Text;
            cryvisitorDetail.outtime = dateTimePicker3.Text;
            cryvisitorDetail.Department = Department;
            cryvisitorDetail.EmpName = State;
            cryvisitorDetail.Designation = Designation;
            cryvisitorDetail obj = new cryvisitorDetail();
            obj.ShowDialog();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {

            if (cmbDepartment.Text == "Select" || cmbDepartment.Text == "")
            {
                Department = "%";
            }
            else
            {
                Department = cmbDepartment.Text;
            }

           
            if (cmbDesignation.Text == "Select" || cmbDesignation.Text == "")
            {
                Designation = "%";
            }
            else
            {
                Designation = cmbDesignation.Text;
            }

            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                State = "%";
            }
            else
            {
                State = cmbState.Text;
            }
           
           
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Text, System.Globalization.CultureInfo.InvariantCulture);
            //SqlDataAdapter da = new SqlDataAdapter("select * from view_Gatepass where id like '" + txtno.Text.Trim().Replace(" ", "") + "' ", sc);

            string str = "select id as Gate_pass,admno,name,class,date,departuretime,Reasontoleave from view_Gatepass where cast (date as date) between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' order by id desc " ;
            if (str != "")
            {
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    dataGridView1.DataSource = dt;
                else
                {
                    MsgBox.ShowBox("No Records Found....");
                }
            }
            sc.Close();
        }

        private void EmployeeRegistrationDetail1_Load(object sender, EventArgs e)
        {
           
            Binddept();
           
        }

        private void cmbDepartment_Leave(object sender, EventArgs e)
        {
            BindDesign();
        }

        private void cmbState_Leave(object sender, EventArgs e)
        {
            
        }
        private void Cancel_Form()
        {
            
            cmbDepartment.Text = "Select";
            cmbDesignation.Text = "Select";
            cmbState.Text = "Select";
           
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBNull.Value;
            
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void cmbDesignation_Leave(object sender, EventArgs e)
        {
            bindstate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

       
        
    }
}
