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

namespace sms
{
    public partial class FeeExampted : Form
    {
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public FeeExampted()
        {
            InitializeComponent();
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView2.Columns["Fee_Id"].Visible = false;
            dataGridView2.Columns["fee"].Width = 250;
            dataGridView2.Columns["fee"].HeaderText = "Fee";
            dataGridView2.Columns["Amount"].Width = 100;
            dataGridView2.Columns["Amount"].HeaderText = "Amount";
            dataGridView2.Columns["Exemption"].Width = 250;
            dataGridView2.Columns["Exemption"].HeaderText = "Exempted";
            
        }

        void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("Find_StudentFeeExempted ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@stu_id", Convert.ToInt32(cmb_rollno.SelectedValue));

            sc.Open();


            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }
        private void Bindname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from STUDENT_T where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
                listBox1.Visible = false;
            }

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
            //SqlDataAdapter da = new SqlDataAdapter(" select * from View__Rollno where class='" + cmb_class.Text + "' order by STC_ROLL_NO", sc);
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
            listBox1.Visible = false;
        }
        void Cancel_Form()
        {
            cmb_class.Enabled =true;
            cmb_class.Text = "-Select-";
            cmb_rollno.Text = "-Select-";
            txtStuname.Text = "";
             txtStuname.Enabled = true;
             cmb_rollno.Enabled = true;
             listBox1.Visible = false;

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
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //cmb_class.Items.Insert(0, "-Select-");
            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "-Select-";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
            


        }

        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitGrid();
            //Bindname();
            //LoadGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlTransaction tran;

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                MsgBox.ShowBox("Please Select CLASS ...");
                return;
            }
            if (cmb_rollno.Text == "")
            {
                MsgBox.ShowBox("Please Select ROLL_NO .... ");
                return;
            }
            sc.Open();
            tran = sc.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand();
            SqlDataReader dr;
            cmd1.Transaction = tran;
            try
            {

                //if (cmd1.ExecuteNonQuery() > 0)
                //{

                for (int z = 0; z < dataGridView2.Rows.Count; z++)
                {
                    cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "insert_Student_FEE_Exemption";
                    cmd1.Connection = sc;
                    //cmd1.Parameters.AddWithValue("@receiptno", Convert.ToInt32(txt_receiptno.Text));
                    cmd1.Parameters.AddWithValue("@STU_ID", cmb_rollno.SelectedValue);
                    cmd1.Parameters.AddWithValue("@FEE_ID", dataGridView2.Rows[z].Cells["Fee_Id"].Value);
                    cmd1.Parameters.AddWithValue("@Exempted", dataGridView2.Rows[z].Cells["Exemption"].Value);
                    cmd1.Parameters.AddWithValue("@Remarks", txtRemarks.Text);

                    cmd1.Transaction = tran;
                    cmd1.ExecuteNonQuery();
                }
                tran.Commit();
                MsgBox.ShowBox("RECORD SAVED...");
                LoadGrid();
                //Cancil_form();
                //}
                //else
                //{
                //    MsgBox.ShowBox("This Subject Marks  already exist");
                //}

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
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  ROLL like '" + cmb_rollno.Text+ "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtStuname.Enabled = false;
                
            }
            else
            {
                cmb_class.SelectedValue = 0;
                cmb_rollno.SelectedValue = "";
               
                txtStuname.Text = "";
                
            }

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            BindAdmNo();
            InitGrid();

        }
        private void BindAdmNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + textBox1.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //Stu_id = dt.Rows[0]["STU_ID"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmb_class.Enabled = false;
                cmb_rollno.Text = dt.Rows[0]["ROLL"].ToString();
                cmb_rollno.Enabled = false;
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtStuname.Enabled = false;
                listBox1.Visible = false;
            }
            else
            {
                cmb_class.SelectedValue = 0;
                cmb_rollno.SelectedValue = 0;
                txtStuname.Text = "";
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

        
        
   