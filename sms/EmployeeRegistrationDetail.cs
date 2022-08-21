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
using Microsoft.Office.Interop.Excel;
namespace sms
{
    public partial class EmployeeRegistrationDetail1 : Form
    {
        string Department;
        string Name;
        string Designation;
        string State;
        string Mobileno;
        string City;



        SqlDataReader dr;
        public EmployeeRegistrationDetail1()
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

            cmbDepartment.DataSource = ds.Tables[0];
            cmbDepartment.DisplayMember = "DEPT_DESC";
            cmbDepartment.ValueMember = "DEPT_ID";
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

            cmbName.DataSource = ds.Tables[0];
            cmbName.DisplayMember = "EMP_NAME";
            cmbName.ValueMember = "EMP_CODE";
        }
        private void BindDesign()
        {
            string str;
            if (cmbDesignation.Text == "" || cmbDesignation.Text == "-Select-" || cmbDesignation.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmbDesignation.SelectedValue.ToString();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DESIGNATION_T where DEPT_ID='" + cmbDepartment.SelectedValue + "'", sc);
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
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from STATE_T order by STATE_NAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STATE_NAME"] = "Select";
            row["STATE_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbState.DataSource = ds.Tables[0];
            cmbState.DisplayMember = "STATE_NAME";
            cmbState.ValueMember = "STATE_ID";
        }

        private void bindCity()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CITY_T order by CITY_NAME ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CITY_NAME"] = "Select";
            row["CITY_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbCity.DataSource = ds.Tables[0];
            cmbCity.DisplayMember = "CITY_NAME";
            cmbCity.ValueMember = "CITY_ID";
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

            if (cmbName.Text == "-Select-")
            {
                Name = "%";
            }
            else
            {
                Name = cmbName.Text;
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
            
            if (cmbCity.Text == "Select" || cmbCity.Text == "")
            {
                City = "%";
            }
            else
            {
                City = cmbCity.Text;
            }

           
            cryEmployeeDetail.dt1 = dateTimePicker1.Value;
            cryEmployeeDetail.dt2 = dateTimePicker2.Value;
            cryEmployeeDetail.Department = Department;
            cryEmployeeDetail.EmpName = Name;
            cryEmployeeDetail.Designation = Designation;
            cryEmployeeDetail.State = State;
            cryEmployeeDetail.Mobileno = Mobileno;
            cryEmployeeDetail.City = City;
            cryEmployeeDetail obj = new cryEmployeeDetail();
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

            if (cmbName.Text == "-Select-")
            {
                Name = "%";
            }
            else
            {
                Name = cmbName.Text;
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
           
            if (cmbCity.Text == "Select" || cmbCity.Text == "")
            {
                City = "%";
            }
            else
            {
                City = cmbCity.Text;
            }
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Text, System.Globalization.CultureInfo.InvariantCulture);
           // string str = "select * from View_tblemployee ";

            string str = "select * from View_tblemployee where EMP_doj between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' and DEPT_DESC like '" + Department + "' or EMP_NAME like '" + Name + "'  or DESG_DESC like '" + Designation + "' or STATE_NAME like '" + State + "'  or CITY_NAME like '" + City + "'";
            if (str != "")
            {
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
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
            bindstate();
            Binddept();
            BindName();
        }

        private void cmbDepartment_Leave(object sender, EventArgs e)
        {
            BindDesign();
        }

        private void cmbState_Leave(object sender, EventArgs e)
        {
            bindCity();
        }
        private void Cancel_Form()
        {
            cmbCity.Text = "Select";
            cmbDepartment.Text = "Select";
            cmbDesignation.Text = "Select";
            cmbState.Text = "Select";
            cmbName.Text = "Select";
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
        private void ExportDataSetToExcel(DataSet ds)
        {
            try
            {
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

              
                DataTableCollection collection = ds.Tables;

                for (int i = collection.Count; i > 0; i--)
                {
                    Sheets xlSheets = null;
                    Worksheet xlWorksheet = null;
                   
                    xlSheets = ExcelApp.Sheets;
                    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                                   Type.Missing, Type.Missing, Type.Missing);

                    System.Data.DataTable table = collection[i - 1];
                    xlWorksheet.Name = table.TableName;

                    for (int j = 1; j < table.Columns.Count + 1; j++)
                    {
                        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    }

                    // Storing Each row and column value to excel sheet
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        for (int l = 0; l < table.Columns.Count; l++)
                        {
                            ExcelApp.Cells[k + 2, l + 1] =
                            table.Rows[k].ItemArray[l].ToString();
                        }
                    }
                    ExcelApp.Columns.AutoFit();
                }
                ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
                ExcelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbDepartment.Text == "Select" || cmbDepartment.Text == "")
            {
                Department = "%";
            }
            else
            {
                Department = cmbDepartment.Text;
            }

            if (cmbName.Text == "-Select-")
            {
                Name = "%";
            }
            else
            {
                Name = cmbName.Text;
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

            if (cmbCity.Text == "Select" || cmbCity.Text == "")
            {
                City = "%";
            }
            else
            {
                City = cmbCity.Text;
            }
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Text, System.Globalization.CultureInfo.InvariantCulture);
            // string str = "select * from View_tblemployee ";

            string str = "select * from View_tblemployee where EMP_doj between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "' and DEPT_DESC like '" + Department + "' or EMP_NAME like '" + Name + "'  or DESG_DESC like '" + Designation + "' or STATE_NAME like '" + State + "'  or CITY_NAME like '" + City + "'";
            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataSet dt = new System.Data.DataSet();
                da.Fill(dt);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    ExportDataSetToExcel(dt);
                }
                else
                {
                    dataGridView1.DataSource = dt;
                   

                }
            }

            sc.Close();

        }

      

       
        
    }
}
