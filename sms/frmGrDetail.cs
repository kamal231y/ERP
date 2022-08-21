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
    public partial class frmGrDetail : Form
    {

        string yaer;
        string hostel;
        string classname;
        SqlDataReader dr;
        public frmGrDetail()
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
            //cmbbilltype.SelectedIndex = 0;

            Cancil_form();


        }



        private void Cancil_form()
        {
            panelsearch.Visible = true;
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbclass.Text = "Select";
            cmbyear.Text = "Select";



        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelsearch.Visible = true;
            Cancil_form();
            //dataGridView1.DataSource = null;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            showstudentreport();

        }
        private void showstudentreport()
        {

            if (cmbyear.Text == "Select" || cmbyear.Text == "0")
            {
                yaer = "%";
            }
            else
            {
                yaer = cmbyear.Text;
            }

            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString(); ;
            }


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



            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            string str = " select stu_session as SESSION,AdmNo,AccNo as CardNo, CONVERT(VARCHAR(10),DOB, 103 )  AS  DOB,CONVERT(VARCHAR(10),Adm_Date, 103 )  AS  Adm_Date ,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and STU_HOSTLER like '" + hostel + "'and STU_SESSION like '" + yaer + "' ORDER BY stu_id desc";
            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                }
                else
                {
                    dataGridView1.DataSource = DBNull.Value;
                    lblcount.Text = "Total No Of Record : " + "0";
                }
            }

            sc.Close();

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbyear.Text == "Select" || cmbyear.Text == "0")
            {
                yaer = "%";
            }
            else
            {
                yaer = cmbyear.Text;
            }

            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString(); ;
            }


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

            if (dataGridView1.Rows.Count > 0)
            {
                cryShowAdmissionDetail.classname = classname;
                cryShowAdmissionDetail.yaer = yaer;
                cryShowAdmissionDetail.hostel = hostel;
                cryShowAdmissionDetail obj = new cryShowAdmissionDetail();

                obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }

        }


        private void frmGrDetail_Load(object sender, EventArgs e)
        {
            //cmbyear.Items.Add("Select");
            //for (int i = 2000; i < 2020; i++)
            //{

            //    cmbyear.Items.Add(i);
            //}


            Bindclass();
            bindSession();
            // bindgridd();
        }
        private void bindSession()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from session_T  order by id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Session"] = "Select";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbyear.DataSource = ds.Tables[0];
            cmbyear.DisplayMember = "Session";
            cmbyear.ValueMember = "id";

        }




        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by CLS_SEQUENCE ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbclass.DataSource = dt;
            cmbclass.DisplayMember = "class";
            cmbclass.ValueMember = "SEC_ID";
            sc.Close();
            showstudentreport();
        }

        private void bindAcc()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select STU_ACC_NO as AccNo, STU_ID as sid from student_t  order by sid ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["AccNo"] = "0";
            row["sid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbyear.DataSource = ds.Tables[0];
            cmbyear.DisplayMember = "AccNo";
            cmbyear.ValueMember = "sid";
        }





        private void txtfname_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void txtfathername_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbroll_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                frmAdmission.admno = dataGridView1.CurrentRow.Cells["AdmNo"].Value.ToString();
                //studentregistration obj = new studentregistration();
                frmAdmission obj = new frmAdmission();
                obj.ShowDialog();
            }
        }

        private void cmbadmno_Leave(object sender, EventArgs e)
        {
            showstudentreport();
        }


        private void radioscholor_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void radiohostler_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothhostel_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radionormal_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiostaff_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdbothward_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiomale_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void radiofemale_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void rdsexboth_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void panelsearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnexport_Click(object sender, EventArgs e)
        {

            if (cmbyear.Text == "Select" || cmbyear.Text == "0")
            {
                yaer = "%";
            }
            else
            {
                yaer = cmbyear.Text;
            }

            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString(); ;
            }


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



            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();


            string str = " select stu_session as SESSION,AdmNo,AccNo as CardNo,CAST(DOB AS DATE) AS  DOB,CAST(Adm_Date AS DATE) AS  Adm_Date ,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and STU_HOSTLER like '" + hostel + "'and STU_SESSION like '" + yaer + "' ORDER BY CLS_SEQUENCE,Roll";
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
                    dataGridView1.DataSource = DBNull.Value;
                    lblcount.Text = "Total No Of Record : " + "0";
                }
            }

            sc.Close();

        }
        private void ExportDataSetToExcel(DataSet ds)
        {
            try
            {
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

                // Loop over DataTables in DataSet.
                DataTableCollection collection = ds.Tables;

                for (int i = collection.Count; i > 0; i--)
                {
                    Sheets xlSheets = null;
                    Worksheet xlWorksheet = null;
                    //Create Excel Sheets
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

    }
}

