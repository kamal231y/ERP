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
    public partial class frmSMSFORCUSTOMISE : Form
    {
        int age = 0;
        string classnamenew;
        string class1;
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
        public frmSMSFORCUSTOMISE()
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
            cmbroll.Text = "Select";
            cmbCategory.Text = "Select";
            cmbcity.Text = "Select";
            cmbsession.Text = "Select";
            cmbadmno.Text = "Select";
            txtfathername.Text = "";
            txtfname.Text = "";
            //showstudentreport();
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();
        }


        private void bindSession()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select Session,id from session_T  order by id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["Session"] = "2015-2016";
            //row["id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "Session";
            cmbsession.ValueMember = "id";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panelsearch.Visible = true;
            Cancil_form();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CASTE_DESC,CASTE_ID from CASTE_T order by CASTE_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CASTE_DESC"] = "Select";
            row["CASTE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbsiblingclass.DataSource = dt;
            cmbsiblingclass.DisplayMember = "CASTE_DESC";
            cmbsiblingclass.ValueMember = "CASTE_ID";

        }
        private void bindTransportMode()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select trnsport_name,trnsport_id from Transport_Mode_T ORDER BY trnsport_id", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["trnsport_name"] = "Select";
            row["trnsport_id"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbtrans.DataSource = dt;
            cmbtrans.DisplayMember = "trnsport_name";
            cmbtrans.ValueMember = "trnsport_id";

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            showstudentreport();

        }
        private void showstudentreport()
        {
            if (cmbHouse.Text == "" || cmbHouse.Text == "Select" || cmbHouse.Text == "System.Data.DataRowView")
            {
                house = "%";
            }
            else
            {
                house = cmbHouse.SelectedValue.ToString();
            }
            if (cmbtrans.Text == "" || cmbtrans.Text == "0" || cmbtrans.Text == "Select" || cmbtrans.Text == "System.Data.DataRowView")
            {
                tmode = "%";
            }
            else
            {
                tmode = cmbtrans.SelectedValue.ToString();
            }

            if (txtfname.Text == "")
            {
                studentname = "%";
            }
            else
            {
                studentname = txtfname.Text;
            }
            if (txtfathername.Text == "")
            {
                fathername = "%";
            }
            else
            {
                fathername = txtfathername.Text;
            }
            if (cmbsession.Text == "Select" || cmbsession.Text == "0" || cmbsession.Text == "System.Data.DataRowView")
            {
                accno = "%";
            }
            else
            {
                accno = cmbsession.Text;
            }
            if (cmbadmno.Text == "Select" || cmbadmno.Text == "0" || cmbadmno.Text == "System.Data.DataRowView")
            {
                Admissionno = "%";
            }
            else
            {
                Admissionno = cmbadmno.Text;
            }
            if (cmbclass.Text == "" || cmbclass.Text == "Select" || cmbclass.Text == "System.Data.DataRowView")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString();
            }
            if (cmbclassnew.Text == "" || cmbclassnew.Text == "Select" || cmbclassnew.Text == "System.Data.DataRowView")
            {
                classnamenew = "%";
            }
            else
            {
                classnamenew = cmbclassnew.SelectedValue.ToString();
            }
            if (cmbclass.Text == "" || cmbclass.Text == "Select" || cmbclass.Text == "System.Data.DataRowView")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbclass.Text.ToString();
            }
            if (cmbroll.Text == "Select" || cmbroll.Text == "0" || cmbroll.Text == "System.Data.DataRowView")
            {
                rollno = "%";
            }
            else
            {
                rollno = cmbroll.SelectedText;
            }
            if (cmbCategory.Text == "Select" || cmbCategory.Text == "" || cmbCategory.Text == "System.Data.DataRowView")
            {
                category = "%";
            }
            else
            {
                category = cmbCategory.SelectedValue.ToString();
            }
            if (cmbcategory1.Text == "Select" || cmbcategory1.Text == "" || cmbcategory1.Text == "System.Data.DataRowView")
            {
                CategoryType = "%";
            }
            else
            {
                CategoryType = cmbcategory1.SelectedValue.ToString();
            }
            if (cmbsiblingclass.Text == "Select" || cmbsiblingclass.Text == "" || cmbsiblingclass.Text == "System.Data.DataRowView")
            {
                Caste = "%";
            }
            else
            {
                Caste = cmbsiblingclass.SelectedValue.ToString();
            }
            if (cmbcity.Text == "Select" || cmbcity.Text == "" || cmbcity.Text == "System.Data.DataRowView")
            {
                cityname = "%";
            }
            else
            {
                cityname = cmbcity.Text;
            }

            if (rdsexboth.Checked)
            {
                sex = "%";
            }
            else
            {
                if (radiomale.Checked)
                {
                    sex = "Male";
                }
                else
                {
                    sex = "Female";
                }
            }

            if (textBox1.Text == "" || textBox1.Text == "select" || textBox1.Text == "0")
            {
                age = 100;
            }
            else
            {
                age = Convert.ToInt16((textBox1.Text).ToString());
            }


            if (cmbincome.Text == "" || cmbincome.Text == "Select")
            {
                min = 0;
                max = 0;

            }
            else if (cmbincome.Text == "Less than 50,000")
            {
                min = 0; max = 50000;
            }
            else if (cmbincome.Text == "1,00000 to 3,00000")
            {
                min = 100000; max = 300000;
            }
            else if (cmbincome.Text == "3,00000 to 5,00000")
            {
                min = 300000; max = 500000;
            }
            else
            {
                min = 500000; max = 99999999999;
            }
            if (chkisCurrent.Checked == true)
                deleted = "N";
            else
                deleted = "Y";

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

            if (cmbincome.Text == "" || cmbincome.Text == "Select")
                str = " select cast('" + checkBox1.Checked + "' as bit) as Selected,AdmNo,Student_Name,FatherName,Class,Roll,Phone from View_StudentReport where DATEDIFF(hour,DOB,GETDATE())/8766  <='" + age + "' and STC_SEC_ID like '" + classname + "'  and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "'  and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "'  and STU_HOUSE_ID like '" + house + "' and STU_Trans_Mode like '" + tmode + "' and sec_cls_id like '" + classnamenew + "' and stu_caste_id like '" + Caste + "' ORDER BY stu_id desc";
            else
                str = " select cast('" + checkBox1.Checked + "' as bit) as Selected,AdmNo,Student_Name,FatherName,Class,Roll,Phone from View_StudentReport where DATEDIFF(hour,DOB,GETDATE())/8766  <='" + age + "' and  STC_SEC_ID like '" + classname + "'  and STU_SIBLING_ID like '" + CategoryType + "' and STU_CASTE_ID like '" + Caste + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and stu_session like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and stu_caste_id like '" + Caste + "'   and STU_HOUSE_ID like '" + house + "' and STU_Trans_Mode like '" + tmode + "' and sec_cls_id like '" + classnamenew + "' ORDER BY stu_id desc";
            //    str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name";


            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    dataGridView1.DataSource = dt;
                }

                {
                    lblcount.Text = "Total No Of Record : " + dt.Rows.Count.ToString();
                    dataGridView1.DataSource = dt;


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


        }
        private void sms1()
        {
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus(); return;

            }
            else
            {
                //dataclass dc = new dataclass();
                //SqlConnection sc = dc.con();
                //SqlDataAdapter da = new SqlDataAdapter("select Student_Name,Class,Roll,Phone from View_StudentReport where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
                //System.Data.DataTable dt = new System.Data.DataTable();
                //da.Fill(dt);
                try
                {
                    showstudentreport();
                    for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
                    {
                        if ((dataGridView1.Rows[z].Cells["Phone"].Value.ToString() != "" || dataGridView1.Rows[z].Cells["Phone"].Value.ToString() != null) && dataGridView1.Rows[z].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        {

                            string msg = "|";
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            //if (radiomale.Checked == true)
                                msg = SMS.SMS(textBox2.Text.Trim(), 0, dataGridView1.Rows[z].Cells["Phone"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));
                            //else
                                //msg = SMS.SMS(textBox2.Text.Trim(), 1, dataGridView1.Rows[z].Cells["Phone"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));
                            string[] str = msg.Split('|');
                            if (str[0].ToString().Trim().Replace(" ", "") == "error")
                            {
                                //   MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }

                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                }

                catch (Exception ex)
                {

                    MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                }
            }
        }
        
        private void bindcategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select CAT_DESC,CAT_ID from CATEGORY_T order by CAT_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CAT_DESC"] = "Select";
            row["CAT_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcategory1.DataSource = dt;
            cmbcategory1.DisplayMember = "CAT_DESC";
            cmbcategory1.ValueMember = "CAT_ID";

        }

        private void frmGrDetail_Load(object sender, EventArgs e)
        {
            Bindclass(); bindcity(); bindreligion(); bindAcc(); BindRNo();
            BindAdmNo(); bindHouse();
            bindCategory();
            bindcategory(); bindTransportMode(); Bindclassnew(); bindSession();

        }
        private void bindHouse()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select HOUSE_DESC,HOUSE_ID from HOUSE_T order by HOUSE_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["HOUSE_DESC"] = "Select";
            row["HOUSE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbHouse.DataSource = dt;
            cmbHouse.DisplayMember = "HOUSE_DESC";
            cmbHouse.ValueMember = "HOUSE_ID";

        }
        
        private void bindcity()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select CITY_NAME,CITY_ID from CITY_T order by  CITY_NAME", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["CITY_NAME"] = "Select";
            row["CITY_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcity.DataSource = dt;
            cmbcity.DisplayMember = "CITY_NAME";
            cmbcity.ValueMember = "CITY_ID";
        }
        private void bindreligion()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select RLG_DESC,RLG_ID from RELIGION_T order by RLG_ID", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["RLG_DESC"] = "Select";
            row["RLG_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbCategory.DataSource = dt;
            cmbCategory.DisplayMember = "RLG_DESC";
            cmbCategory.ValueMember = "RLG_ID";

        }
        private void Bindclassnew()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from class_t  order by cls_id ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["cls_desc"] = "Select";
            row["cls_id"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbclassnew.DataSource = dt;
            cmbclassnew.DisplayMember = "cls_desc";
            cmbclassnew.ValueMember = "cls_id";
            sc.Close();
            //showstudentreport();
        }

        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,SEC_ID from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbclass.DataSource = dt;
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
            sc.Close();
            //showstudentreport();
        }

        private void bindAcc()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select STU_ACC_NO, STU_ID  from STUDENT_T  order by STU_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STU_ACC_NO"] = "0";
            row["STU_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "STU_ACC_NO";
            cmbsession.ValueMember = "STU_ID";
        }

        private void cmbclass_Leave(object sender, EventArgs e)
        {
            BindRNo();
        }
        private void BindRNo()
        {
            string class1;
            if (cmbclass.SelectedValue.ToString() == "Select" || cmbclass.SelectedValue.ToString() == "")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STC_ROLL_NO,STC_STU_ID from STUDENT_CLASS_T where STC_SEC_ID =  " + class1 + " order by  STC_ROLL_NO", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["STC_ROLL_NO"] = "0";
            row["STC_STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbroll.DataSource = dt;
            cmbroll.DisplayMember = "STC_ROLL_NO";
            cmbroll.ValueMember = "STC_STU_ID";
        }
        private void BindAdmNo()
        {
            string class1;
            if (cmbclass.Text == "Select" || cmbclass.Text == "")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ADM_NO,STU_ID from STUDENT_T  order by STU_ADM_NO", sc);
            System.Data.DataTable dt2 = new System.Data.DataTable();
            da.Fill(dt2);
            DataRow row1 = dt2.NewRow();
            row1["STU_ADM_NO"] = "Select";
            row1["STU_ID"] = 0;
            dt2.Rows.InsertAt(row1, 0);
            cmbadmno.DataSource = dt2;
            cmbadmno.DisplayMember = "STU_ADM_NO";
            cmbadmno.ValueMember = "STU_ID";
           // showstudentreport();
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

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {
           showstudentreport();
        }

        private void chkisCurrent_CheckedChanged(object sender, EventArgs e)
        {
          showstudentreport();
        }

        private void cmbincome_SelectedIndexChanged(object sender, EventArgs e)
        {
         showstudentreport();
        }

        private void cmbroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           showstudentreport();
        }

        private void cmbHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
           showstudentreport();
        }

       

        private void cmbcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void cmbsiblingclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void cmbcategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void btnexport_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void cmbclassnew_SelectedIndexChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }
        private void AddCheckBoxColumn()
        {
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.Name = "Select";
            doWork.HeaderText = "Select";
            doWork.FalseValue = 0;
            doWork.TrueValue = 1;
            dataGridView1.Columns.Insert(1, doWork);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sms1();

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            showstudentreport();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string str;

            str = textBox2.Text.ToString();
            label19.Text = " Character : " + str.Length;
            label19.Visible = true;

        }

        private void cmbsession_SelectedIndexChanged(object sender, EventArgs e)
        {
           showstudentreport();
        }

        private void panelsearch_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}

