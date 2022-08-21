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
using Excel = Microsoft.Office.Interop.Excel;
using MsgBoxRet;
using Microsoft.Office.Interop.Excel;

namespace sms
{
    public partial class Inactive : Form
    {
        string tmode;
        string house;
        string studentname;
        string fathername;
        string Admissionno;
        string accno;
        string rollno;
        string sex;
        string hostel;
        string ward;
        string classname;
        string category;
        string Caste;
        string CategoryType;
        string cityname; decimal min;
        decimal max;
        string deleted;
        SqlDataReader dr;
        public Inactive()
        {
     
            InitializeComponent();
        }
        private void showstudentreport()
        {
            if (checkBox1.Checked == true)
            { deleted = "N"; }
            else
            { deleted = "Y"; }
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            //DateTime dt1 = dateTimePicker1.Value;
           // DateTime dt2 = dateTimePicker2.Value;

            if (textBox1.Text == "" || textBox1.Text == "")
                str = " select AdmNo,AccNo as CardNo, CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Phone,Address from View_StudentReport where STC_SEC_ID like '" + classname + "'  and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "'  and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'  and STU_HOUSE_ID like '" + house + "' and STU_Trans_Mode like '" + tmode + "'   ORDER BY stu_id desc";
            else
                str = " select AdmNo,AccNo as CardNo , CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Phone,Address  from View_StudentReport where STC_SEC_ID like '" + classname + "'  and STU_SIBLING_ID like '" + CategoryType + "' and STU_CASTE_ID like '" + Caste + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'   and STU_HOUSE_ID like '" + house + "' and STU_Trans_Mode like '" + tmode + "' ORDER BY stu_id desc";
            //    str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name";


            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                dataGridView1.DataSource = dt;
                //lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                dataGridView1.Columns["AdmNo"].Width = 100;
                dataGridView1.Columns["cardno"].Width = 80;
                dataGridView1.Columns["DOB"].Width = 75;
                dataGridView1.Columns["Adm_Date"].Width = 80;
                dataGridView1.Columns["Student_Name"].Width = 170;
                dataGridView1.Columns["FatherName"].Width = 150;
                dataGridView1.Columns["MotherName"].Width = 150;
                dataGridView1.Columns["Sex"].Width = 50;
                dataGridView1.Columns["Class"].Width = 70;
                dataGridView1.Columns["Roll"].Width = 50;
                dataGridView1.Columns["Address"].Width = 160;
                dataGridView1.Columns["Phone"].Width = 100;
                //}
                //else
                {
                    dataGridView1.DataSource = dt;
                    //lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    //MsgBox.ShowBox("No Records Found....");

                }
            }

            sc.Close(); 




        }
        private void bindgridd()
        {
            dataclass dc = new dataclass();

            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select   Admno,AccNo,dob as DOB,Adm_date, Student_Name,fathername ,mothername ,sex= case when  sex='M' then 'Male' else 'Female' end, Class ,  Address from View_StudentReport", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        
            
            if (dataGridView1.Rows.Count > 0)
            {
                frmAdmission.admno = dataGridView1.CurrentRow.Cells["AdmNo"].Value.ToString();
                frmAdmission obj = new frmAdmission();
                obj.ShowDialog();
            }
        
        }

        private void Inactive_Load(object sender, EventArgs e)
        {
            bindgridd();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindgridd();
        }

    }
}
