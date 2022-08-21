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
    public partial class tc_form : Form
    {
        public tc_form()
        {
            InitializeComponent();
            cancel_form();
            bindtcstu();
        }
        dataclass dc = new dataclass();
       
        
        private void tc_form_Load(object sender, EventArgs e)
        {
        
            bindclass(); bindAdmclass();
            bindSub1(); bindSub2(); bindSub3(); bindcaste();
            bindSub4(); bindSub5(); bindSub6(); bindSub7(); bindSub8();
            bindAdmNo();
            bindMonth();
            bindCurrentCls2();
            bindExam();
            cmbYear.Text = "Select"; cmbResult.Text = "Select";
            txtTPresentDay.Text = "0"; txtTWorkDay.Text = "0";
        }

        private void bindtcstu()
        {
            DataTable ds = new DataTable();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            da = new SqlDataAdapter("  select TC_NO,TC_ADM_NO,TC_NAME+ '/'  +TC_CURR_CLS as NAME from tc_t order by TC_ID asc", sc);
            //sr.STU_CLS_ID
            da.Fill(ds);
           
            if (ds.Rows.Count > 0)
            {

                dataGridView1.DataSource = ds;
                dataGridView1.Columns["TC_ADM_NO"].Visible = false;
                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[0].Width = 50;
            }

        
        }




        private void bindTcNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select max(tc_no)+1 from tc_t ", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtTcNo.Text = dr[0].ToString();
                if (txtTcNo.Text == "")
                {
                    txtTcNo.Text = "1";
                }
            }
            sc.Close();
        }
        private void cancel_form()
        {
            btn_create.Enabled = true;
            btn_delete.Enabled = false;

            txtStudentName.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            dateofadm.Text = DateTime.Now.ToString();
            cmbSub1.Text = "ENGLISH";
            cmbSub2.Text = "Select";
            cmbSub3.Text = "Select";
            cmbSub4.Text = "Select";
            cmbSub5.Text = "Select";
            cmbSub6.Text = "Select";
            cmbsub7.Text = "Select";
            cmbSub8.Text = "Select";
            txtTcNo.Text = "";
            cmbAdmNo.Text = "Select";
            cmbCast.Text = "Select";
            //dtpAdm.Text=DateTime.Now.ToString();
            cmbAdmClass.Text = "Select";
            dob.Text = DateTime.Now.ToString();
            cmbCurrCls2.Text = "Select";
            cmbExam.Text = "Select";
            cmbYear.Text = "Select";
            cmbResult.Text = "Select";
            chkPromote.Checked = false;
            cmbCurrCls2.Text = "Select";
            cmbMonth.Text = "Select";
            rbtnOnce.Checked = false;
            chkfeeConcession.Checked = false;
            txtConcessionType.Text = "";
            txtTPresentDay.Text = "";
            txtTWorkDay.Text = "";
            chkNCC.Checked = false;
            txtNccDetail.Text = "";
            txtGamePlayed.Text = "";
            txtGenConduct.Text = "GOOD";
            dtpAppliedOn.Text = "";
            dtpIssueOn.Text = "";
            txtReg4Leav.Text = "";
            txtRmk.Text = "";
            txtTWorkDay.Text = "";
            bindTcNo();
        }



        private void bindAdmclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLASS_T  ORDER BY CLS_SEQUENCE ASC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbAdmClass.DataSource = ds.Tables[0];
            cmbAdmClass.DisplayMember = "CLS_DESC";
            cmbAdmClass.ValueMember = "CLS_ID";
        }
        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from tblBoardMaster ", sc);
            //Exam_ID,Exam_desc from EXAMINATION_T
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["boardName"] = "Select";
            row["BoardID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "boardName";
            cmbExam.ValueMember = "BoardID";
        }
        private void bindcaste()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select caste_ID,caste_desc from caste_t ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["caste_desc"] = "Select";
            row["caste_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbCast.DataSource = ds.Tables[0];
            cmbCast.DisplayMember = "caste_desc";
            cmbCast.ValueMember = "caste_ID";
        }
        private void bindCurrentCls2()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" SELECT * FROM CLASS_T  ORDER BY CLS_SEQUENCE ASC ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbCurrCls2.DataSource = ds.Tables[0];
            cmbCurrCls2.DisplayMember = "CLS_DESC";
            cmbCurrCls2.ValueMember = "CLS_ID";
        }
        private void bindAdmNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select stu_id,admno from View_studentReport where stu_status='y' order by AdmNo", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["admno"] = "Select";
            row["admno"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbAdmNo.DataSource = ds.Tables[0];
            cmbAdmNo.DisplayMember = "admno";
            cmbAdmNo.ValueMember = "admno";
        }
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM CLASS_T  ORDER BY CLS_SEQUENCE ASC ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbCurrentClass.DataSource = ds.Tables[0];
            cmbCurrentClass.DisplayMember = "CLS_DESC";
            cmbCurrentClass.ValueMember = "CLS_ID";
        }
        private void bindSub1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "ENGLISH";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub1.DataSource = ds.Tables[0];
            cmbSub1.DisplayMember = "SUB_DESC";
            cmbSub1.ValueMember = "SUB_ID";
        }
        private void bindSub2()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub2.DataSource = ds.Tables[0];
            cmbSub2.DisplayMember = "SUB_DESC";
            cmbSub2.ValueMember = "SUB_ID";
        }
        private void bindSub3()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub3.DataSource = ds.Tables[0];
            cmbSub3.DisplayMember = "SUB_DESC";
            cmbSub3.ValueMember = "SUB_ID";
        }
        private void bindSub4()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub4.DataSource = ds.Tables[0];
            cmbSub4.DisplayMember = "SUB_DESC";
            cmbSub4.ValueMember = "SUB_ID";
        }
        private void bindSub5()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub5.DataSource = ds.Tables[0];
            cmbSub5.DisplayMember = "SUB_DESC";
            cmbSub5.ValueMember = "SUB_ID";
        }
        private void bindSub6()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub6.DataSource = ds.Tables[0];
            cmbSub6.DisplayMember = "SUB_DESC";
            cmbSub6.ValueMember = "SUB_ID";
        }
        private void bindSub7()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsub7.DataSource = ds.Tables[0];
            cmbsub7.DisplayMember = "SUB_DESC";
            cmbsub7.ValueMember = "SUB_ID";
        }
        private void bindSub8()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub8.DataSource = ds.Tables[0];
            cmbSub8.DisplayMember = "SUB_DESC";
            cmbSub8.ValueMember = "SUB_ID";
        }
        private void bindMonth()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from year_t order by ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["year"] = "Select";
            row["ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbMonth.DataSource = ds.Tables[0];
            cmbMonth.DisplayMember = "year";
            cmbMonth.ValueMember = "ID";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                //dataclass dd = new dataclass();
                //SqlConnection sc = dd.con();

                if (cmbAdmNo.Text.ToString() == "")
                {
                    MsgBox.ShowBox("Admission number should not be blank");
                    return;
                }
                if (cmbAdmClass.Text.ToString() == "" || cmbAdmClass.Text.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select  Adm class");
                    return;
                }
                if (cmbCurrentClass.Text.ToString() == "" || cmbCurrentClass.Text.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select  current class");
                    return;
                }
                if (cmbCurrCls2.Text.ToString() == "" || cmbCurrCls2.Text.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select Qualified current class");
                    return;
                }
                //if (cmbSub1.SelectedValue.ToString() == "" || cmbSub1.SelectedValue.ToString() == "0")
                //{
                //    MsgBox.ShowBox("plz. select subject 1");
                //    return;
                //}
                if (cmbSub2.SelectedValue.ToString() == "" || cmbSub2.SelectedValue.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select subject 2");
                    return;
                }
                if (cmbSub3.SelectedValue.ToString() == "" || cmbSub3.SelectedValue.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select subject 3");
                    return;
                }
                if (cmbSub4.SelectedValue.ToString() == "" || cmbSub4.SelectedValue.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select subject 4");
                    return;
                }
                if (cmbSub5.SelectedValue.ToString() == "" || cmbSub5.SelectedValue.ToString() == "0")
                {
                    MsgBox.ShowBox("plz. select subject 5");
                    return;
                }
                //if (cmbSub6.SelectedValue.ToString() == "" || cmbSub6.SelectedValue.ToString() == "0")
                //{
                //    MsgBox.ShowBox("plz. select subject 6");
                //    return;
                //}
                //if (cmbsub7.SelectedValue.ToString() == "" || cmbsub7.SelectedValue.ToString() == "0")
                //{
                //    MsgBox.ShowBox("plz. select subject 7");
                //    return;
                //}
                //if (cmbSub8.SelectedValue.ToString() == "" || cmbSub8.SelectedValue.ToString() == "0")
                //{
                //    MsgBox.ShowBox("plz. select subject 8");
                //    return;
                //}
                //if (cmbExam.Text == "Select" || cmbExam.SelectedValue.ToString() == "0")
                //{
                //    MsgBox.ShowBox("plz. select Board");
                //    return;
                //}
                ////if (cmbYear.Text == "Select" || cmbYear.SelectedValue.ToString() == "0" || cmbYear.SelectedValue.ToString() == null)
                ////{
                ////    MsgBox.ShowBox("plz. select Year");
                ////    return;
                ////}
                //if (cmbResult.Text == "Select" || cmbResult.SelectedValue.ToString() == "0")
                //{
                //    MsgBox.ShowBox("plz. select Result");
                //    return;
                //} 
                if (txtTWorkDay.Text == "" || txtTWorkDay.Text == "0")
                {
                    MsgBox.ShowBox("plz. Enter Working Day");
                    return;
                }
                if (txtTPresentDay.Text == "" || txtTPresentDay.Text == "0")
                {
                    MsgBox.ShowBox("plz. Enter Working Day");
                    return;
                }
                else
                {



                    DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtdoa = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime appon = Convert.ToDateTime(dtpAppliedOn.Value, System.Globalization.CultureInfo.InvariantCulture);
                    DateTime issueon = Convert.ToDateTime(dtpIssueOn.Value, System.Globalization.CultureInfo.InvariantCulture);
                    //SqlCommand com = new SqlCommand();
                    //com.CommandText = "insert_TC_T";
                    //com.Connection = sc;
                    DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                    SqlConnection sc = dc.con();
                    // SqlConnection sc = dc.con();

                    ArrayList arr = new ArrayList();

                    arr.Add(Convert.ToInt32(txtTcNo.Text));
                    arr.Add(cmbAdmNo.Text);
                    arr.Add(txtStudentName.Text);
                    arr.Add(txtMotherName.Text);
                    arr.Add(txtFatherName.Text);
                    arr.Add(cmbNational.Text);
                    arr.Add(cmbCast.Text);
                    arr.Add(dtdoa.ToString());
                    arr.Add(cmbAdmClass.Text);
                    arr.Add(dtdob.ToString());

                    arr.Add(cmbCurrentClass.Text);
                    arr.Add(cmbExam.Text);
                    arr.Add(cmbYear.Text);
                    arr.Add(cmbResult.Text);
                    if (cmbResult.Text == "Fail")
                    {

                        //if (rbtnOnce.Checked == true)
                        //{
                            arr.Add("ONCE");
                        //}
                        //else if (rbtnTwice.Checked == true)
                        //{
                        //    arr.Add("TWICE");
                        //}
                    }
                    else
                    {
                        arr.Add("No");
                    }
                    arr.Add(cmbSub1.Text);
                    arr.Add(cmbSub2.Text);
                    arr.Add(cmbSub3.Text);
                    arr.Add(cmbSub4.Text);
                    arr.Add(cmbSub5.Text);
                    arr.Add(cmbSub6.Text);
                    arr.Add(cmbsub7.Text);
                    arr.Add(cmbSub8.SelectedValue);
                    if (chkPromote.Checked == true)
                    {
                        arr.Add("Y");
                    }
                    else
                    {
                        arr.Add("N");
                    }
                    arr.Add(cmbCurrCls2.Text);
                    arr.Add(Convert.ToInt32(cmbMonth.SelectedValue));
                    arr.Add(cmbYear.Text);
                    if (chkfeeConcession.Checked == true)
                    {
                        arr.Add("Y");
                    }
                    else
                    {
                        arr.Add("N");
                    }
                    arr.Add(txtConcessionType.Text);
                    arr.Add(Convert.ToInt32(txtTPresentDay.Text));
                    arr.Add("USER");
                    if (chkNCC.Checked == true)
                    {
                        arr.Add("Y");
                    }
                    else
                    {
                        arr.Add("N");
                    }
                    arr.Add(txtNccDetail.Text);
                    arr.Add(txtGamePlayed.Text);
                    arr.Add(txtGenConduct.Text);

                    arr.Add(appon.ToString());
                    arr.Add(issueon.ToString());
                    arr.Add(txtReg4Leav.Text);
                    arr.Add(txtRmk.Text);
                    arr.Add(Convert.ToInt32(txtTWorkDay.Text));//not
                     SqlConnection sc11 = dc.con();
                     int n = dc.InsertWithConn("insert_TC_T", arr, sc11);

                   // int n = dc.InsertRows("insert_TC_T", arr);
                    if (n > 0)
                    {
                       // int IN = dc.update_data("update student_t set STU_STATUS='N',TCNO='" + txtTcNo.Text + "' where STU_ADM_NO='" + cmbAdmNo.Text + "'");



                        //sms1();
                        DialogResult dialogResult = MessageBox.Show(" SMS sent successfully. Are You Sure to print Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            print();
                        }

                        //cancel_form();
                    }
                    else
                    {
                        MsgBox.ShowBox("ALREADY EXISTS");
                        return;
                    }

                    //SqlDataReader dr;
                    //sc.Open();

                    //    dr = com.ExecuteReader();
                    //    if (dr.RecordsAffected > 0)
                    //    {
                    //        DialogResult dialogResult = MessageBox.Show("Are You Sure To Print TC Number - " + txtTcNo.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //        if (dialogResult == DialogResult.Yes)
                    //        {
                    //            print();
                    //            cancel_form();
                    //        }
                    //        else
                    //        {
                    //            MsgBox.ShowBox("TC Created...");
                    //            cancel_form();
                }

                //}
                //else
                //{
                //    MsgBox.ShowBox("This tc already exists");
                //}


            }
            // }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.Message);
            }
            
        }



        private void print()
        {


            String sDate = dob.Text;
            string[] str = sDate.ToString().Split('/');
            int dy = Convert.ToInt32(str[0].ToString());
            string mn = str[1].ToString();
            int yy = Convert.ToInt32(str[2].ToString());
            if (mn == "01")
            {
                mn = "January";
            }
            else if (mn == "02")
            {
                mn = "February";
            }
            else if (mn == "03")
            {
                mn = "March";
            }
            else if (mn == "04")
            {
                mn = "April";
            }
            else if (mn == "05")
            {
                mn = "May";
            }
            else if (mn == "06")
            {
                mn = "June";
            }
            else if (mn == "07")
            {
                mn = "July";
            }
            else if (mn == "08")
            {
                mn = "August";
            }
            else if (mn == "09")
            {
                mn = "September";
            }
            else if (mn == "10")
            {
                mn = "October";
            }
            else if (mn == "11")
            {
                mn = "November";
            }
            else if (mn == "12")
            {
                mn = "December";
            }
            cryTC.yyWords = NumberToWords(yy);
            string admno, tcno;
            cryTC.tcno = txtTcNo.Text;
            cryTC.admno = cmbAdmNo.Text;
            cryTC.mn = mn;
            cryTC.dyWords = NumberToWords(dy);
            cryTC obj = new cryTC();
            obj.ShowDialog();
        }


        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                    //words += unitsMap[number % 10];
                }
            }

            return words;
        }



        private void txtTcNo_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from View_tc where TC_NO='" + txtTcNo.Text + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btn_delete.Enabled = true;
                btn_create.Enabled = false;
                cmbAdmClass.Text = dr["admclas"].ToString();
                txtTcId.Text = dr["tc_id"].ToString();
                txtStudentName.Text = dr["tc_Name"].ToString();
                txtMotherName.Text = dr["tc_m_Name"].ToString();
                txtFatherName.Text = dr["tc_f_Name"].ToString();
                txtTcId.Text = dr["currentclass"].ToString();
                cmbCurrentClass.Text = dr["currentclass"].ToString();
                cmbCast.Text = (dr["caste_desc"].ToString());
                dob.Text = (dr["tc_dob"].ToString());
                cmbAdmNo.Text = (dr["tc_adm_no"].ToString());
                cmbCurrCls2.Text = dr["currentclass"].ToString();
                cmbNational.Text = (dr["tc_nation_id"].ToString());
                cmbSub1.Text = (dr["sub1"].ToString());
                cmbSub2.Text = dr["sub2"].ToString();
                cmbSub3.Text = (dr["sub3"].ToString());
                cmbSub4.Text = (dr["sub4"].ToString());
                cmbSub5.Text = dr["sub5"].ToString();
                cmbSub6.Text = (dr["sub6"].ToString());
                cmbsub7.Text = (dr["sub7"].ToString());
                cmbSub8.Text = dr["sub8"].ToString();
                cmbExam.Text = (dr["exam_desc"].ToString());
                cmbYear.Text = (dr["tc_last_exam_year"].ToString());
                cmbResult.Text = dr["tc_result"].ToString();
                //cmbAdmClass.Text = (dr["admclass"].ToString());
                txtTPresentDay.Text = dr["tc_total_cls_attend"].ToString();
                txtTWorkDay.Text = dr["tc_total_stu_attend"].ToString();
                txtNccDetail.Text = dr["tc_ncc_guide_detail"].ToString();
                txtGamePlayed.Text = dr["tc_game_extra"].ToString();
                txtGenConduct.Text = dr["tc_gen_conduct"].ToString();
                txtRmk.Text = dr["tc_remarks"].ToString();
                txtReg4Leav.Text = dr["tc_reason"].ToString();
                if (dr["TC_PROMOT"].ToString() == "Y")
                    chkPromote.Checked = true;
                else
                    chkPromote.Checked = false;
                cmbCurrCls2.Text = dr["PROMOTECLASS"].ToString();
                cmbMonth.Text = dr["FEEPAIDMONTH"].ToString();

                if (dr["TC_FEE_CONCESS"].ToString() == "Y")
                    chkfeeConcession.Checked = true;
                else
                    chkfeeConcession.Checked = false;
                txtConcessionType.Text = dr["TC_FEE_CONCESS_TYPE"].ToString();
                if (dr["TC_ncc_guide"].ToString() == "Y")
                    chkNCC.Checked = true;
                else
                    chkNCC.Checked = false;
            }
            sc.Close();
        }

        private void cmbAdmNo_Leave(object sender, EventArgs e)
        {
            if (cmbAdmNo.Text != "" || cmbAdmNo.Text != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand cmd = new SqlCommand("select * from View_studentReport where admno='" + cmbAdmNo.Text.Trim() + "' and ( TCNO = '') ", sc);
                sc.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtstuid.Text = dr["Stu_id"].ToString();
                    txtStudentName.Text = dr["Student_Name"].ToString();
                    //txtsrno.Text=dr[""].ToString();
                    txtMotherName.Text = dr["MotherName"].ToString();
                    txtFatherName.Text = dr["FatherName"].ToString();
                    //txtCurCls.Text = dr["class"].ToString();
                    cmbCurrentClass.Text = dr["class"].ToString();
                    //txtCaste.Text = (dr["STU_CASTE_ID"].ToString());
                    cmbCast.Text = dr["STU_CASTE_ID"].ToString();
                    dob.Text = (dr["DOB"].ToString());
                    dateofadm.Text = (dr["Adm_date"].ToString());
                    // cmbCurrentClass.Text = dr["STU_CLS_ID"].ToString();
                    cmbAdmClass.Text = (dr["class"].ToString());
                    cmbNational.Text = (dr["STU_National_ID"].ToString());
                }
                else
                {
                    MsgBox.ShowBox("TC already given to this Student. Pls Fill Valid Adm. No.!!!");
                    return;
                }

            }
            else
            {
                MsgBox.ShowBox("Pls Fill Valid Admission No.!!!");
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            print();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel_form();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete TC!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtTcNo.Text == "")
                    {
                        MsgBox.ShowBox("Please select TC No.!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE_TC_T " + txtTcNo.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("TC Deleted Successfully!!!");

                    }
                    else
                    {
                        // MsgBox.ShowBox(" TC Already Used!!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void label73_Click(object sender, EventArgs e)
        {

        }

        private void txtConcessionType_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtTcNo.Text= dataGridView1.CurrentRow.Cells["TC_NO"].Value.ToString();
                this.txtTcNo.TextChanged += new System.EventHandler(this.txtTcNo_Leave);

            }
        }

       




    }
}
