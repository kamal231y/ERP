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
    public partial class Form3 : Form
    {

        string classname;
        string Admission;
        string studentname;
        SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
        }
                private void showstudentreport()
                {
                    if (cmbcls.Text == "" || cmbcls.Text == "Select" || cmbcls.Text == "System.Data.DataRowView")
                    {
                        classname = "%";
                    }
                    else
                    {
                        classname = cmbcls.SelectedValue.ToString();
                    }
                    if (cmbadm.Text == "Select" || cmbadm.Text == "0" || cmbadm.Text == "System.Data.DataRowView")
                    {
                        Admission = "%";
                    }
                    else
                    {
                        Admission = cmbadm.Text;
                    }
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    string str;
                    // DateTime dt1 = dateTimePicker1.Value;
                    //DateTime dt2 = dateTimePicker2.Value;
                    if (cmbadm.Text == "" || cmbadm.Text == "Select")
                        str = " select stu_adm_no ,class from View_Studentfee where cls_desc like '" + classname + "'  and student_first_Name like '" + studentname + "%'  and  AdmNo like '" + Admission + "' ORDER BY stu_id desc";
                    else
                        str = " select stu_adm_no ,Class from View_Studentfee where cls_desc like '" + classname + "'  and student_Name like '" + studentname + "%'  and  AdmNo like '" + Admission + "' ORDER BY stu_id desc";
                
       
        
             if(str!="")
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
                    dataGridView1.Columns["paidamt"].Width = 100;
                    dataGridView1.Columns["dues"].Width = 100;
                    //dataGridView1.Columns["cardno"].Width = 80;
                    //dataGridView1.Columns["DOB"].Width = 75;
                    //dataGridView1.Columns["Adm_Date"].Width = 80;
                    dataGridView1.Columns["Student_Name"].Width = 170;
                    //dataGridView1.Columns["FatherName"].Width = 150;
                    //dataGridView1.Columns["MotherName"].Width = 150;
                    //dataGridView1.Columns["Sex"].Width = 50;
                    dataGridView1.Columns["Class"].Width = 70;
                    //ataGridView1.Columns["Roll"].Width = 50;
                    //dataGridView1.Columns["Address"].Width = 160;
                    //dataGridView1.Columns["Phone"].Width = 100;
                //}
                //else
                {
                    dataGridView1.DataSource = dt;
                   // lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    //MsgBox.ShowBox("No Records Found....");

                }
           }

             sc.Close();

                }

private void bindgridd()
        {
            dataclass dc = new dataclass();
        
                   SqlConnection sc = dc.con();
                   SqlDataAdapter da = new SqlDataAdapter("select stu_adm_no,student_name,cls_desc,frc_paid_amt,stu_first_name from View_Studentfee", sc);
                   DataSet ds = new DataSet();
                   da.Fill(ds);
                   dataGridView1.DataSource = ds.Tables[0];
        }
 private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by CLS_SEQUENCE ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbcls.DataSource = dt;
            cmbcls.DisplayMember = "Class";
            cmbcls.ValueMember = "SEC_ID";
            sc.Close();
            showstudentreport();
        }
 private void BindAdmNo()
       {
           string class1;
           if (cmbcls.Text == "Select" || cmbcls.Text == "")
           {
               class1 = "%";
           }
           else
           {
               class1 = cmbcls.SelectedValue.ToString();
           }
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_T  order by STU_ADM_NO", sc);
           System.Data.DataTable dt2 = new System.Data.DataTable();
           da.Fill(dt2);
           DataRow row1 = dt2.NewRow();
           row1["STU_ADM_NO"] = "Select";
           row1["STU_ID"] = 0;
           dt2.Rows.InsertAt(row1, 0);
           cmbadm.DataSource = dt2;
           cmbadm.DisplayMember = "STU_ADM_NO";
           cmbadm.ValueMember = "STU_ID";
           showstudentreport();
       }

          private void frmGrDetail_Load(object sender, EventArgs e)
        {
            
            BindAdmNo(); Bindclass();bindgridd();
            
           
        }
                private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
                {
                    showstudentreport();
                }

                private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
                {

                }

                private void cmbcls_SelectedIndexChanged(object sender, EventArgs e)
                {
                    showstudentreport();
                }
            }
        }
    