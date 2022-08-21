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
    public partial class frmStudentFee : Form
    {
        DataTable StudentFee = new DataTable();
        string stuid,ROLL,Stu_id;
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        int RowIndex = 0;
        int RowIndex2 = 0;
         
        public frmStudentFee()
        {
            InitializeComponent();
        }

        public void InitGrid1()
        {
            LoadGrid();
            dataGridView1.Columns["FEE_ID"].Visible = false;
            dataGridView1.Columns["FEE_DESC"].Width = 200;
            dataGridView1.Columns["FEE_DESC"].HeaderText = "Fees Avilable";
        }

        void LoadGrid()
        {
            DataTable dt = new DataTable();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select FEE_ID,FEE_DESC,CLF_D_AMOUNT as Amount from CLASS_FEE_T cf,FEE_T ft where cf.CLF_FEE_ID=ft.FEE_ID and cf.CLF_CLS_ID in (select SEC_CLS_ID from SECTION_T  where SEC_ID=" + str + ")", sc);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";
        }
        private void bindrollno()
        {
            string StateName;
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID =  '" + StateName + "' order by  STC_ROLL_NO", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "-Select-";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
        }

        public void InitGrid2()
        {
            LoadGrid2();
            dataGridView2.Columns["FEE_DESC"].Width = 200;
            dataGridView2.Columns["FEE_DESC"].HeaderText = "Fees Selected";
            dataGridView2.Columns["FEE_ID"].Visible = false;
        }

        private void LoadGrid2()
        {
            StudentFee.Rows.Clear();
           dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  FEE_ID,FEE_DESC,SEF_FEE_AMOUNT as Amount from View__Rollno5 where  STC_STU_ID like '" + stuid + "'", sc);
            da.Fill(StudentFee);
            dataGridView2.DataSource = StudentFee;


        }
       
       
        void Cancel_Form()
        {
            
            cmb_class.Text = "-Select-";
            StudentFee.Rows.Clear();
            cmb_rollno.Text = "-Select-";
            txtStuname.Text = "";
            cmb_class.Enabled = true;

            cmb_rollno.Enabled = true;

            txtStuname.Enabled = true;
            txtstuno.Text = "";
            dataGridView1.DataSource = DBNull.Value;
            dataGridView2.DataSource = DBNull.Value;
            listBox1.Visible = false;


        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
           
        }





        private void btn_create_Click(object sender, EventArgs e)
        {
            if (stuid != "0")
            {
                DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
                {
                    MsgBox.ShowBox("Please Select Class !!!!!");
                    return;
                }
                //else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
                //{
                //    MsgBox.ShowBox("Please Select RollNo !!!!!");
                //    return;
                //}
                else if (dataGridView2.Rows.Count <= 0)
                {
                    MsgBox.ShowBox("Please Select Fee !!!!!");
                    return;
                }
                else
                {
                    sc.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_FEE_T where SEF_STU_ID=" + stuid + " ", sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        SqlCommand cmd12 = new SqlCommand(" delete from STUDENT_FEE_T where SEF_STU_ID=" + stuid + "", sc);
                        cmd12.ExecuteNonQuery();
                    }
                    SqlTransaction tran;
                    tran = sc.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr;
                    cmd1.Transaction = tran;
                    try
                    {


                        for (int z = 0; z <= dataGridView2.Rows.Count - 1; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "Insert_STUDENT_FEE_T";
                            cmd1.Connection = sc;

                            cmd1.Parameters.AddWithValue("@SEF_STU_ID", Convert.ToInt32(stuid));
                            cmd1.Parameters.AddWithValue("@SEF_CLS_ID", Convert.ToInt32(cmb_class.SelectedValue));
                            cmd1.Parameters.AddWithValue("@SEF_FEE_ID", Convert.ToInt32(dataGridView2.Rows[z].Cells["FEE_ID"].Value));
                            if (dataGridView2.Rows[z].Cells["Amount"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@Amount", 0);
                            else
                                cmd1.Parameters.AddWithValue("@Amount", Convert.ToInt32(dataGridView2.Rows[z].Cells["Amount"].Value));

                            cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        MsgBox.ShowBox("Student Fee Created...");
                        LoadGrid();
                        // Cancil_form();
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

            }
        }

        private void btnGT_Click(object sender, EventArgs e)
        {
            int count = dataGridView2.Rows.Count;
            int ID = (int)dataGridView1.Rows[RowIndex].Cells[0].Value;
            Boolean TF = false;
            for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
            {
                int feeid = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value);
                if (ID == feeid)
                {
                    TF = true;
                    break;

                }
            }
            if (TF == false)
            {

                DataRow dr = StudentFee.NewRow();
                dr["FEE_ID"] = dataGridView1.Rows[RowIndex].Cells[0].Value;
                dr["FEE_DESC"] = dataGridView1.Rows[RowIndex].Cells[1].Value;
                dr["Amount"] = dataGridView1.Rows[RowIndex].Cells[2].Value;
                StudentFee.Rows.Add(dr);
                dataGridView2.DataSource = StudentFee;
                dataGridView2.Columns["FEE_DESC"].Width = 200;
                dataGridView2.Columns["FEE_DESC"].HeaderText = "Fees Selected";
                dataGridView2.Columns["FEE_ID"].Visible = false;


            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                try
                {
                    StudentFee.Rows.RemoveAt(RowIndex2);
                    dataGridView2.DataSource = StudentFee;
                    dataGridView2.Columns["FEE_DESC"].Width = 200;
                    dataGridView2.Columns["FEE_DESC"].HeaderText = "Fees Selected";
                    dataGridView2.Columns["FEE_ID"].Visible = false;

                }
                catch (Exception ex)
                { }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_class.Text != "")
            {
                StudentFee.Rows.Clear();
                int count = dataGridView1.Rows.Count;
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        DataRow dr = StudentFee.NewRow();
                        dr["FEE_ID"] = dataGridView1.Rows[i].Cells[0].Value;
                        dr["FEE_DESC"] = dataGridView1.Rows[i].Cells[1].Value;
                        dr["Amount"] = dataGridView1.Rows[i].Cells[2].Value;

                        StudentFee.Rows.Add(dr);
                    }
                    dataGridView2.DataSource = StudentFee;
                    dataGridView2.Columns["FEE_DESC"].Width = 200;
                    dataGridView2.Columns["FEE_DESC"].HeaderText = "Fees Selected";
                    dataGridView2.Columns["FEE_ID"].Visible = false;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = DBNull.Value;
            StudentFee.Rows.Clear();
        }

        private void frmStudentFee_Load(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            bindClass();

        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex2 = e.RowIndex;

        }

       

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBNull.Value;
            dataGridView2.DataSource = DBNull.Value;
            cmb_rollno.Text = "";
            txtStuname.Text = "";
            InitGrid1();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Student Fee!!!", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
                    {
                        MsgBox.ShowBox("Please Select Class !!!!!");
                        return;
                    }
                    else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
                    {
                        MsgBox.ShowBox("Please Select RollNo !!!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("delete from STUDENT_FEE_T where SEF_STU_ID=" + cmb_rollno.SelectedValue + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Student Fee Deleted Successfully!!!");

                        //  LoadGrid();
                        Cancel_Form();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex2 = e.RowIndex;
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBNull.Value;
            InitGrid1();
            dataGridView2.DataSource = DBNull.Value;
            InitGrid2(); BindRoll();
        }

        private void BindRoll()
        {
            {

                string StateName;
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    StateName = "0";
                }
                else
                {
                    StateName = cmb_class.SelectedValue.ToString();
                }

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where STC_SEC_ID like  '" + StateName + "' and stu_status='y' order by  ROLL", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataRow row = dt.NewRow();
                    row["ROLL"] = "0";
                    row["STU_ID"] = 0;
                    dt.Rows.InsertAt(row, 0);
                    cmb_rollno.DataSource = dt;
                    cmb_rollno.DisplayMember = "ROLL";
                    cmb_rollno.ValueMember = "STU_ID";
                    cmb_rollno.Text = ROLL;
                }
            }
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BindStuFee();
                    listBox1.Visible = false;

                }
                else
                {
                    txtstuno.Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure to create Student Fee for all class Student!!!", "Conferm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
                {
                    MsgBox.ShowBox("Please Select Class !!!!!");
                    return;
                }
                else if (dataGridView2.Rows.Count == 0)
                {
                    MsgBox.ShowBox("Please Select Fee !!!!!");
                    return;
                }
                else
                {
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    sc.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_FEE_T where SEF_cls_ID=" + cmb_class.SelectedValue + " ", sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        SqlCommand cmd12 = new SqlCommand(" delete from STUDENT_FEE_T where SEF_cls_ID=" + cmb_class.SelectedValue + "", sc);
                        cmd12.ExecuteNonQuery();
                    }
                    SqlTransaction tran;
                    tran = sc.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr;
                    cmd1.Transaction = tran;
                    try
                    {


                        for (int z = 0; z <= dataGridView2.Rows.Count - 1; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "Insert_STUDENT_FEE_T_ClassWise";
                            cmd1.Connection = sc;
                            cmd1.Parameters.AddWithValue("@SEF_CLS_ID", Convert.ToInt32(cmb_class.SelectedValue));
                            cmd1.Parameters.AddWithValue("@SEF_FEE_ID", Convert.ToInt32(dataGridView2.Rows[z].Cells["FEE_ID"].Value));
                            if (dataGridView2.Rows[z].Cells["Amount"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@Amount", 0);
                            else
                                cmd1.Parameters.AddWithValue("@Amount", Convert.ToInt32(dataGridView2.Rows[z].Cells["Amount"].Value));
                            cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        MsgBox.ShowBox("Student Fee Created...");
                        LoadGrid();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MsgBox.ShowBox("error...");
                    }
                    finally
                    {
                        sc.Close();
                    }


                }
            }
        }

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                stuid = dt.Rows[0]["stu_id"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                txtstuno.Leave +=new EventHandler(cmb_class_Leave);
                cmb_rollno.Text = dt.Rows[0]["ROLL"].ToString();
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtStuname.Enabled = false;
                dataGridView2.DataSource = DBNull.Value;
                InitGrid2();
                listBox1.Visible = false;
                dataGridView2.Focus();
               
            }
            else
            {
                cmb_class.SelectedValue = 0;
                cmb_rollno.Text ="0";
                txtStuname.Text = "";
                stuid = "0"; dataGridView2.DataSource = DBNull.Value;
                listBox1.Visible = false;

            }

        }

        private void BindStuFee()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  admno like '" + txtstuno.Text + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                stuid = dt.Rows[0]["stu_id"].ToString();
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                txtstuno.Leave += new EventHandler(cmb_class_Leave);
                cmb_rollno.Text = dt.Rows[0]["ROLL"].ToString();
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtStuname.Enabled = false;
                dataGridView2.DataSource = DBNull.Value;
                InitGrid2();

            }
            else
            {
                cmb_class.SelectedValue = 0;
                cmb_rollno.Text = "0";
                txtStuname.Text = "";
                stuid = "0"; dataGridView2.DataSource = DBNull.Value;

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

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    txtstuno.Text = str[0].ToString();
                    
                    BindStuFee();
                    listBox1.Visible = false;
                    dataGridView2.Focus();
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
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BindStuFee();
                    listBox1.Visible = false;
                    dataGridView2.Focus();

                }
                else
                {
                    txtstuno.Text = "";
                    txtStuname.Text = "";

                }
            }
        }

        private void cmb_rollno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BindStuFee();
                    listBox1.Visible = false;

                }
                else
                {
                    txtstuno.Text = "";
                    txtStuname.Text = "";
                }
            }
        }

        private void cmb_rollno_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BindStuFee();
                    listBox1.Visible = false;

                }
                else
                {
                    txtstuno.Text = "";
                    txtStuname.Text = "";
                }
            }
        }

        private void cmb_rollno_MouseClick(object sender, MouseEventArgs e)
        {
            if (cmb_rollno.Text != "-Select-" || cmb_rollno.Text != "" || cmb_rollno.Text != "System.Data.DataRowView")
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;

                if (cmb_rollno.SelectedValue == null)
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + Stu_id + "'  ", sc);

                else
                    da = new SqlDataAdapter("select * from View_studentReport where  Stu_id like '" + cmb_rollno.SelectedValue + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtstuno.Text = dt.Rows[0]["AdmNo"].ToString();
                    Stu_id = dt.Rows[0]["STU_ID"].ToString();
                    //cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                    //cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                    txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
                    BindStuFee();
                    listBox1.Visible = false;

                }
                else
                {
                    txtstuno.Text = "";
                    txtStuname.Text = "";
                }
            }
        }

       
       
    }
}
