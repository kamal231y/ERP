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
using sms.DAL;
using sms.BLClass;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Office.Interop.Excel;

namespace sms
{
    public partial class FeeconcessionExampted : Form
    {  string hostel;
        public static string Stu_id,fee_id;
        System.Windows.Forms.CheckBox chkbox = new System.Windows.Forms.CheckBox();
        System.Data.DataTable dt;
        public FeeconcessionExampted()
        {
           
            InitializeComponent();
        }

        public void InitGrid()
        {
            LoadGrid();
          
           
        }

        void LoadGrid()
        {
            string cls;
            if (textBox1.Text == "" || textBox1.Text == "-Select-")
            {
                cls = "%";
            }
            else
            {
                cls = textBox1.Text;
            }     
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_FEE_concesion_T where AdmNo like '" + cls + "' order by class,student_name", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            
            
              //  label19.Text = "Total No Of Student : " + dt.Rows.Count.ToString();

           
            dataGridView2.DataSource = dt;
            dataGridView2.Columns["stu_id"].Visible = false;
            dataGridView2.Columns["id"].Visible = false;
            SqlDataAdapter daa = new SqlDataAdapter("select Count(Distinct AdmNo) from View_FEE_concesion_T where AdmNo like '" + cls + "'", sc);
            System.Data.DataTable dtt = new System.Data.DataTable();
            daa.Fill(dtt);
            label9.Text = "Total No Of Student : " + dtt.Rows[0].ItemArray.GetValue(0).ToString();
        }
      
        private void Bindname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from STUDENT_T where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Stu_id = dt.Rows[0]["stu_id"].ToString();
                txtStuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
                textBox1.Text = dt.Rows[0]["stu_adm_no"].ToString();
            }
            listBox1.Visible = false;

        }
        private void bindPeriod()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from FEE_PERIOD where prd_id in (1,4) order by PRD_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            cmbperiod.DataSource = ds.Tables[0];
            cmbperiod.DisplayMember = "PRD_DESC";
            cmbperiod.ValueMember = "PRD_ID";
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select (CLS_DESC + SEC_DESC) as class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "class";
            cmb_class.ValueMember = "SEC_ID";
        }
        private void bindrollno()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select distinct ts.*,sr.STU_ADM_NO,(sr.STU_FIRST_NAME+sr.STU_MID_NAME +sr.STU_LAST_NAME) as StudentName from  STUDENT_T sr,STUDENT_CLASS_T ts where sr.STU_ID=ts.STC_STU_ID and sr.STU_CLS_ID like " + cmb_class.SelectedValue + " order by ts.STC_ROLL_NO", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STU_ADM_NO"] = "-Select-";
            row["STC_ROLL_NO"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_rollno.DataSource = ds.Tables[0];
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STU_ADM_NO";
        }
        private void BindGrid(string month, decimal dues)
        {


            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();

            //SqlDataAdapter da1 = new SqlDataAdapter("select FEE_DESC from View_tblClassFeeSingle where CLF_FEE_ID in (select Fee_Id from tblFeeDetail where " + month + " =1) ", sc);
            //DataSet ds = new DataSet();
            //da1.Fill(ds);
            //dataGridView2.DataSource = ds.Tables[0];

        }
        private void FeeExampted_Load(object sender, EventArgs e)
        {
            bindClass();
            bindPeriod();
            InitGrid();
            listBox1.Visible = false;
        }
        void Cancel_Form()
        {
            listBox1.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "0";
            txtRemarks.Text = "";
            cmbFeetype.Text = "";
            cmb_class.Enabled =true;
            cmb_class.Text = "-Select-";
            cmb_rollno.Text = "-Select-";
            txtStuname.Text = "";
             txtStuname.Enabled = true;
             cmb_rollno.Enabled = true;
             chkApr.Checked = false;
             chkMay.Checked = false;
             chkJune.Checked = false;
             chkjuly.Checked = false;
             chkAug.Checked = false;
             chkSep.Checked = false;
             chkOct.Checked = false;
             chkNov.Checked = false;
             chkDec.Checked = false;
             chkJan.Checked = false;
             chkFeb.Checked = false;
             chkMar.Checked = false;
             panel1.Visible = false;


        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form(); LoadGrid();
        }



        private void btn_create_Click(object sender, EventArgs e)
        {

        }

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StateName;
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da1 = new SqlDataAdapter("select stc_stu_id,stc_roll_no from STUDENT_CLASS_T where STC_stu_ID in (select stu_id from student_t where stu_cls_id like '" + StateName + "' and stu_status='y')  order by  STC_ROLL_NO", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da1.Fill(dt);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
            txtStuname.Enabled = false;
            /*sc.Close();
            sc.Open();
            string cls;
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                cls = "%";
            }
            else
            {
                cls = cmb_class.Text;
            }
            SqlDataAdapter da = new SqlDataAdapter("select * from View_FEE_concesion_T where class like '" + cls + "' order by student_name", sc);
            System.Data.DataTable dt1 = new System.Data.DataTable();
            da.Fill(dt1);
            dataGridView2.DataSource = dt1;*/
        }
        private void feetype()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select distinct FEE_ID ,FEE_DESC ,SEF_FEE_AMOUNT from View_tblClassFeeSingle where sef_stu_id='" + Stu_id + "' order by  fee_desc", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            cmbFeetype.DataSource = dt;
            cmbFeetype.DisplayMember = "fee_desc";
            cmbFeetype.ValueMember = "fee_id";

        }
        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
           // InitGrid();
           // Bindname();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlTransaction tran;

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            if (textBox1.Text == "-Select-" || textBox1.Text == "")
            {
                MsgBox.ShowBox("Please Select Adm No. ...");
                return;
            }
            if (cmbFeetype.SelectedValue == "0" || cmbFeetype.SelectedValue == null)
            {
                fee_id = txtfeeid.Text;
            }
            else
            {
                fee_id = Convert.ToString(cmbFeetype.SelectedValue);
            }
            if (cmbperiod.Text == "" || cmbperiod.Text == "-Select-")
            {
                MsgBox.ShowBox("Period should not be blank");
                return;
            }
            
            int amt=Convert.ToInt16(textBox2.Text);
            int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0;
            if (chkApr.Checked)
                mt1 = amt;
            if (chkMay.Checked)
                mt2 = amt;
            if (chkJune.Checked)
                mt3 = amt;
            if (chkjuly.Checked)
                mt4 = amt;
            if (chkAug.Checked)
                mt5 = amt;
            if (chkSep.Checked)
                mt6 = amt;
            if (chkOct.Checked)
                mt7 = amt;
            if (chkNov.Checked)
                mt8 = amt;
            if (chkDec.Checked)
                mt9 = amt;
            if (chkJan.Checked)
                mt10 = amt;
            if (chkFeb.Checked)
                mt11 = amt;
            if (chkMar.Checked)
                mt12 = amt;
            sc.Open();
            tran = sc.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand();
            SqlDataReader dr;
            cmd1.Transaction = tran;
            try
            {
                    cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "update_tblconcesion";
                    cmd1.Connection = sc;
                    cmd1.Parameters.AddWithValue("@STU_ID", Stu_id);
                    cmd1.Parameters.AddWithValue("@fee_id", fee_id);
                    cmd1.Parameters.AddWithValue("@amount", amt);
                    cmd1.Parameters.AddWithValue("@fee_type", Convert.ToInt16(cmbperiod.SelectedValue));
                    cmd1.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                    cmd1.Parameters.AddWithValue("@mt1", mt1);
                    cmd1.Parameters.AddWithValue("@mt2", mt2);
                    cmd1.Parameters.AddWithValue("@mt3", mt3);
                    cmd1.Parameters.AddWithValue("@mt4", mt4);
                    cmd1.Parameters.AddWithValue("@mt5", mt5);
                    cmd1.Parameters.AddWithValue("@mt6", mt6);
                    cmd1.Parameters.AddWithValue("@mt7", mt7);
                    cmd1.Parameters.AddWithValue("@mt8", mt8);
                    cmd1.Parameters.AddWithValue("@mt9", mt9);
                    cmd1.Parameters.AddWithValue("@mt10", mt10);
                    cmd1.Parameters.AddWithValue("@mt11", mt11);
                    cmd1.Parameters.AddWithValue("@mt12", mt12);
                    cmd1.Transaction = tran;
                    cmd1.ExecuteNonQuery();
                
                tran.Commit();
                MsgBox.ShowBox("RECORD SAVED...");
                LoadGrid();
              

            }
            catch (Exception ex)
            {
                tran.Rollback();
                //MsgBox.ShowBox("Subject Marks  Created...");
            }
            finally
            {
                sc.Close();
            }

        }


        private void txtStuname_TextChanged(object sender, EventArgs e)
        {
            {

                string Name, CLS;

                if (txtStuname.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtStuname.Text.Trim().ToString();
                }
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    CLS = "%";
                }
                else
                {
                    CLS = cmb_class.SelectedValue.ToString();
                }
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_cls_id like'" + CLS + "' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '|' + ds1.Tables[0].Rows[j][1].ToString() + '|' + ds1.Tables[0].Rows[j][2].ToString() + '|' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = txtStuname.Top + txtStuname.Height;
                    //listBox1.Width = txtstuno.Width;
                    listBox1.Left = txtStuname.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            // dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  ROLL like '" + cmb_rollno.Text+ "'  ", sc);
            //System.Data.DataTable dt = new System.Data.DataTable();
            //da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
                
            //    txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
            //    txtStuname.Enabled = false;
            //    detail();
            //    feetype();
                
            //}
            //else
            //{
            //    cmb_class.SelectedValue = 0;
            //    cmb_rollno.SelectedValue = "";
               
            //    txtStuname.Text = "";
                
            //}

        }

        private void detail()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from view_FEE_concesion_T where stu_id like '" + Stu_id + "'", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                panel3.Visible = true;
                //textBox2.Text = dt.Rows[0]["Amount"].ToString();
                if (dt.Rows[0]["APR"].ToString() == false.ToString())
                {
                    chkApr.Checked = false;
                }
                else
                {
                    chkApr.Checked = true;
                }
                if (dt.Rows[0]["MAY"].ToString() == false.ToString())
                {
                    chkMay.Checked = false;
                }
                else
                {
                    chkMay.Checked = true;
                }
                if (dt.Rows[0]["JUN"].ToString() == false.ToString())
                {
                    chkJune.Checked = false;
                }
                else
                {
                    chkJune.Checked = true;
                }
                if (dt.Rows[0]["JUL"].ToString() == false.ToString())
                {
                    chkjuly.Checked = false;
                }
                else
                {
                    chkjuly.Checked = true;
                }
                if (dt.Rows[0]["AUG"].ToString() == false.ToString())
                {
                    chkAug.Checked = false;
                }
                else
                {
                    chkAug.Checked = true;
                }
                if (dt.Rows[0]["SEP"].ToString() == false.ToString())
                {
                    chkSep.Checked = false;
                }
                else
                {
                    chkSep.Checked = true;
                }
                if (dt.Rows[0]["OCT"].ToString() == false.ToString())
                {
                    chkOct.Checked = false;
                }
                else
                {
                    chkOct.Checked = true;
                }
                if (dt.Rows[0]["NOV"].ToString() == false.ToString())
                {
                    chkNov.Checked = false;
                }
                else
                {
                    chkNov.Checked = true;
                }
                if (dt.Rows[0]["DEC"].ToString() == false.ToString())
                {
                    chkDec.Checked = false;
                }
                else
                {
                    chkDec.Checked = true;
                }
                if (dt.Rows[0]["JAN"].ToString() == false.ToString())
                {
                    chkJan.Checked = false;
                }
                else
                {
                    chkJan.Checked = true;
                }
                if (dt.Rows[0]["FEB"].ToString() == false.ToString())
                {
                    chkFeb.Checked = false;
                }
                else
                {
                    chkFeb.Checked = true;
                }
                if (dt.Rows[0]["MAR"].ToString() == false.ToString())
                {
                    chkMar.Checked = false;
                }
                else
                {
                    chkMar.Checked = true;
                }
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            BindAdmNo();
        }
        private void BindAdmNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + textBox1.Text.Trim().Replace(" ", "") + "'  ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmb_class.Enabled = true;
                cmb_rollno.Text = dt.Rows[0]["ROLL"].ToString();
                cmb_rollno.Enabled = false;
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtStuname.Enabled = false;
                detail();
                feetype();
                listBox1.Visible = false;

            }
            else
            {
                cmb_class.SelectedValue = 0;
                cmb_rollno.SelectedValue = 0;
                txtStuname.Text = "";
                listBox1.Visible = false;
            }
        }

        private void cmbperiod_Leave(object sender, EventArgs e)
        {
            if (cmbperiod.Text == "More Time")
                panel1.Visible = true;
            else
            {
                panel1.Focus();
                panel1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cls;
            if (textBox1.Text == "")
            {
                Stu_id = "%";
            }
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-")
            {
                cls = "%";
            }
            else
            {
                cls = cmb_class.SelectedValue.ToString();
            }
         

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  Admno,student_Name,class,Amount from view_FEE_concesion_T where stu_id like '" + Stu_id + "' and class like '"+cls+"' order by class,student_name", sc);
            sc.Open();
            DataSet dt = new DataSet();
            da.Fill(dt);
            if (dt.Tables[0].Rows.Count > 0)
            {
                ExportDataSetToExcel(dt);
            }
            else
            {
                MessageBox.Show("No Records");
            }

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

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                textBox1.Text = dataGridView2.CurrentRow.Cells["AdmNo"].Value.ToString();
                cmb_class.Text = dataGridView2.CurrentRow.Cells["class"].Value.ToString();
                txtStuname.Text = dataGridView2.CurrentRow.Cells["student_name"].Value.ToString();
                cmbFeetype.Text = dataGridView2.CurrentRow.Cells["FEE_DESC"].Value.ToString();
                textBox2.Text = dataGridView2.CurrentRow.Cells["Amount"].Value.ToString();
                txtRemarks.Text = dataGridView2.CurrentRow.Cells["REMARKS"].Value.ToString();
                txtfeeid.Text = dataGridView2.CurrentRow.Cells["fee_id"].Value.ToString();
                txtstuid.Text = dataGridView2.CurrentRow.Cells["stu_id"].Value.ToString();
                Stu_id = txtstuid.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("delete from FEE_concesion_T  where stu_id='"+ txtstuid.Text +"'  and fee_id='"+ txtfeeid.Text +"'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Concession Deleted...");
                    Cancel_Form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Concession not deleted...");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    textBox1.Text = str[0].ToString();
                    listBox1.Visible = false;
                    BindAdmNo();

                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
        }

        private void cmb_rollno_KeyDown(object sender, KeyEventArgs e)
        {
            InitGrid();
            Bindname();
        }

        private void cmb_rollno_KeyUp(object sender, KeyEventArgs e)
        {
            InitGrid();
            Bindname();
        }
       

        }

        }

        
        
   