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
using System.Collections;

namespace sms
{
    public partial class frmStudentOldDuesClassWise : Form
    {
        //int rollNo = 0;
        //int stuid = 0; int RowIndex = -1;
        Boolean TF = false;
        public string classname;   
        public frmStudentOldDuesClassWise()
        {
            InitializeComponent();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            bindclass();
            LoadGrid();
            listBox1.Visible = false;
        }


        public void InitGrid()
        {
            LoadGrid();        
         
        }
        private void LoadGrid()
        {
            
            DataTable ds = new DataTable();
            string str="";
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-" || cmbClass.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmbClass.SelectedValue.ToString().Trim();
                      
               dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (str != "")
            {
                SqlDataAdapter da = new SqlDataAdapter("select stu_id,ADM_NO, StudentName,Roll_No,Dues,UPDATE_DATE,[USER] from View_StudentOldDues where STC_SEC_ID =" + str + "  order by Roll_no", sc);
                   da.Fill(ds);
                    dataGridView1.DataSource = ds;
                    dataGridView1.Columns["stu_id"].Visible = false;
                    dataGridView1.Columns["ADM_NO"].Width = 100;
                    dataGridView1.Columns["StudentName"].Width = 200;
                    dataGridView1.Columns["Roll_No"].Width = 100;
                    dataGridView1.Columns["Dues"].Width = 100;
                    dataGridView1.Columns["UPDATE_DATE"].Width = 100;
                    dataGridView1.Columns["USER"].Width = 100;
            }
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }


        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbClass.Text = "-Select-";
            txtappno.Text = "";
            txtStuname.Text = "";
            LoadGrid();
            listBox1.Visible = false;

        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
           InitGrid();
        }
     
        private void btnupdate_Click(object sender, EventArgs e)
        {
         
            if (dataGridView1.Rows.Count > 0)
            {
                cryStudentOldDuesClassWise.classname = cmbClass.SelectedValue.ToString();
                cryStudentOldDuesClassWise.classnameT = cmbClass.Text;
                cryStudentOldDuesClassWise obj = new cryStudentOldDuesClassWise();
                obj.ShowDialog();
            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
        }
       
      
        private void btnupdate_Click()
        {
            SqlTransaction tran;
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            //if (cmbClass.Text == "-Select-")
            //{
            //    MsgBox.ShowBox("class should not be blank");
            //    return;
            //}
            sc.Open();
            tran = sc.BeginTransaction();
            SqlCommand cmd1 = new SqlCommand();

            SqlDataReader dr;
            cmd1.Transaction = tran;
            try
            {
                for (int z = 0; z < dataGridView1.Rows.Count; z++)
                {
                    cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "Update_OldDues";
                    cmd1.Connection = sc;
                    if (dataGridView1.Rows[z].Cells["DUES"].Value.ToString() == "")
                        cmd1.Parameters.AddWithValue("@rollNo", 0);
                    else
                        cmd1.Parameters.AddWithValue("@rollNo", dataGridView1.Rows[z].Cells["DUES"].Value);
                    cmd1.Parameters.AddWithValue("@stuid", dataGridView1.Rows[z].Cells["STU_ID"].Value);
                    cmd1.Parameters.AddWithValue("@USER", Form1.GlobalLoginUserName.ToString());
                    cmd1.Parameters.AddWithValue("@DTupdate", dt1);
                    cmd1.Transaction = tran;
                    cmd1.ExecuteNonQuery();
                    
                }
                MsgBox.ShowBox("Dues Updated...");
                tran.Commit();
                LoadGrid();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MsgBox.ShowBox("Dues Not Updated...");
            }
            finally
            {
                sc.Close();
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtappno_Leave(object sender, EventArgs e)
        {
            BindLoad(); BindAdmNo();
        }

        private void BindLoad()
        {
            DataTable ds = new DataTable();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            {
                SqlDataAdapter da = new SqlDataAdapter("select stu_id,ADM_NO, StudentName,Roll_no,Dues from View_StudentOldDues where ADM_NO like '" + txtappno.Text.Trim().Replace(" ", "") + "%'  order by Roll_no", sc);
                da.Fill(ds);
                dataGridView1.DataSource = ds;


            }
        }

        private void BindAdmNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + txtappno.Text.Trim().Replace(" ", "") + "'  ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtappno.Text = dt.Rows[0]["Admno"].ToString();
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
               
                cmbClass.Text = dt.Rows[0]["Class"].ToString();

            }
            else
            {
                cmbClass.SelectedValue = 0;
                txtStuname.Text = "";

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
                btnupdate_Click();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            {

                string Name, CLS;

                if (txtStuname.Text == "-Select-" || cmbClass.Text == "" || cmbClass.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtStuname.Text.Trim().ToString();
                }
                if (cmbClass.Text == "-Select-" || cmbClass.Text == "" || cmbClass.Text == "System.Data.DataRowView")
                {
                    CLS = "%";
                }

                else
                {
                    CLS = cmbClass.SelectedValue.ToString();
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

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    txtappno.Text = str[0].ToString();
                    listBox1.Visible = false;
                    BindAdmNo(); BindLoad();
                    //listBox1.Visible = false;
                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
        }


      
    }
}
