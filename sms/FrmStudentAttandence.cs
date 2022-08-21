using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;
using System.Collections;
using sms.DATALAYER;


namespace sms
{
    public partial class FrmStudentAttandence : Form
    {
        int id;
        SqlCommand com;
        SqlDataReader dr;
        int p;
        string var;
        string VarT;
       
        public FrmStudentAttandence()
        {
            InitializeComponent();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblClassMaster order by CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "CLS_DESC";
            cmbclass.ValueMember = "CLS_ID";
        }
        private void BindSection()
        {
            string StateName;
            if (cmbclass.Text == "Select" || cmbclass.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbclass.Text;
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_section where CLS_DESC = '" + StateName + "'and SEC_AVAILABLE>0 order by SEC_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["SEC_DESC"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbsection.DataSource = dt;
            cmbsection.DisplayMember = "SEC_DESC";
            cmbsection.ValueMember = "SEC_ID";
        }
        private void BindStuName()
        {
            string StateName;
            if (cmbsection.Text == "Select" || cmbsection.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbsection.Text;
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_student_showdetail where SEC_DESC = '" + StateName + "' order by Section", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Student Name"] = "Select";
            row["Sid"] = 0;
            dt.Rows.InsertAt(row, 0);
            CmbStudentName.DataSource = dt;
            CmbStudentName.DisplayMember = "Student Name";
            CmbStudentName.ValueMember = "Sid";
        }
        private void UpdateAttandence(string var)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            com = new SqlCommand("update tbl_StudentDailyPresent set " + var + "='" + p + "' where Stu_id='" + CmbStudentName.SelectedValue + "'", sc);
            sc.Open();
            int i = com.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Updated attandence");
                BindGrid();
            }
            sc.Close();

        }
        private void BindGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from View_AttendanceDetail", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
           
          
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dtpdate.Text, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            com = new SqlCommand("select * from tbl_StudentDailyPresent where stu_id='" + CmbStudentName.SelectedValue + "'", sc);
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                //if (dr["DAY1"].ToString() == "0" || dr["DAY1"].ToString()=="")
                //{


                int j = dt1.Day;
                if (j == 1)
                {
                    var = "Day1";
                    VarT = "Day1";

                    if (ChkPresent.Checked == true)
                    {
                        p = 1;
                       
                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                        
                    }
                    else
                    {
                        p = 3;
                      
                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 2)
                {
                    var = "Day2";
                    VarT = "Day2";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 3)
                {
                    var = "Day3";
                    VarT = "Day3";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 3)
                {
                    var = "Day3";
                    VarT = "Day3";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 4)
                {
                    var = "Day4";
                    VarT = "Day4";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 5)
                {
                    var = "Day5";
                    VarT = "Day5";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 6)
                {
                    var = "Day6";
                    VarT = "Day6";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 7)
                {
                    var = "Day7";
                    VarT = "Day7";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 8)
                {
                    var = "Day8";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 9)
                {
                    var = "Day10";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 11)
                {
                    var = "Day11";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 12)
                {
                    var = "Day12";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 13)
                {
                    var = "Day13";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 14)
                {
                    var = "Day14";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 15)
                {
                    var = "Day15";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 16)
                {
                    var = "Day16";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 17)
                {
                    var = "Day17";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 18)
                {
                    var = "Day18";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 19)
                {
                    var = "Day19";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 20)
                {
                    var = "Day20";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 21)
                {
                    var = "Day21";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 22)
                {
                    var = "Day22";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 23)
                {
                    var = "Day23";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 24)
                {
                    var = "Day24";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 25)
                {
                    var = "Day25";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 26)
                {
                    var = "Day26";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 27)
                {
                    var = "Day27";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 28)
                {
                    var = "Day28";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 29)
                {
                    var = "Day29";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 30)
                {
                    var = "Day30";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else if (j == 31)
                {
                    var = "Day31";
                    VarT = "Day31";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence(var);
                    TotalAttandence(VarT);
                }
                else
                {
                }
            }

            sc.Close();

            
          
            //else if (j == 2)
            //{
 
            //}

            //com = new SqlCommand("update tbl_StudentDailyPresent set  ", sc);

        }
        private void TotalAttandence(string VarT)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            com = new SqlCommand("select  count('" + var + "') as num  from tbl_StudentDailyPresent where stu_id='" + CmbStudentName.SelectedValue + "'", sc);
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                dataclass dc2 = new dataclass();
                SqlConnection sc2 = dc2.con();

                com = new SqlCommand("update  tbl_StudentPresent set Attendence='" + dr["num"].ToString() + "' where stu_id='" + CmbStudentName.SelectedValue + "'", sc2);
                sc2.Open();
                dr = com.ExecuteReader();
                sc2.Close();
            }
            sc.Close();
        }
        private void cmbclass_Leave(object sender, EventArgs e)
        {
            BindSection();
        }

        private void FrmStudentAttandence_Load(object sender, EventArgs e)
        {
            bindclass();
            BindGrid();
        }

        private void cmbsection_Leave(object sender, EventArgs e)
        {
            BindStuName();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private bool IsValid()
        //{
        //    dataclass dc = new dataclass();
        //    SqlConnection sc = dc.con();
        //    bool ret = true;
        //    foreach (int j in CmbStudentName.SelectedValue.ToString())
        //    {
        //        com = new SqlCommand("Select * from tbl_StudentDailyPresent where Stu_id='" + j + "'", sc);
        //        sc.Open();
        //        dr = com.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            dr.Read();
        //            ret = true;
        //        }
        //        else
        //        {
        //            dataclass dc1 = new dataclass();
        //            SqlConnection sc1 = dc1.con();
        //            com = new SqlCommand("Insert into tbl_StudentDailyPresent (stu_id)values ('" +j + "')", sc1);
        //            sc1.Open();
        //            int i = com.ExecuteNonQuery();
        //            if (i > 0)
        //            {

        //            }
        //            ret = false;
        //            sc1.Close();
        //        }
        //        return ret;
        //        sc.Close();
        //    }
      
        private void CmbStudentName_Leave(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dtpdate.Text, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  bool ret = true;
            if (CmbStudentName.Text == "-Select-" || CmbStudentName.Text == "")
            {
            }
            foreach (int j in CmbStudentName.SelectedValue.ToString())
            {
                com = new SqlCommand("Select * from tbl_StudentDailyPresent where Stu_id='" + CmbStudentName.SelectedValue + "' and Month='" + dt1.Month + "' and year='" + dt1.Year + "'", sc);
                sc.Open();
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //  ret = true;
                }
                else
                {
                    if (CmbStudentName.SelectedValue.ToString() == "0")
                    {
                    }
                    else
                    {
                        dataclass dc1 = new dataclass();
                        SqlConnection sc1 = dc1.con();
                        com = new SqlCommand("Insert into tbl_StudentDailyPresent (stu_id,Month,Year)values ('" + CmbStudentName.SelectedValue + "','" + dt1.Month + "','" + dt1.Year + "')", sc1);
                        sc1.Open();
                        int i = com.ExecuteNonQuery();
                        if (i > 0)
                        {

                        }

                        // ret = false;
                        sc1.Close();

                        com = new SqlCommand("Insert into tbl_StudentPresent (stu_id)values ('" + CmbStudentName.SelectedValue + "')", sc1);
                        sc1.Open();
                        int i1 = com.ExecuteNonQuery();
                        if (i1 > 0)
                        {

                        }
                        sc1.Close();
                    }
                }
                // return ret;
                sc.Close();
            }
        }
        private void UpdateAttandence1(string var)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            com = new SqlCommand("update tbl_StudentDailyPresent set " + var + "='" + p + "'", sc);
            sc.Open();
            int i = com.ExecuteNonQuery();
            sc.Close();
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {

        }

        private void btExit_Click(object sender, EventArgs e)
        {

        }

        private void btMaximize_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // id = Convert.ToInt32( CmbStudentName.SelectedValue);
            //CryStuAttandence.id = id;
            CryStuAttandence obj = new CryStuAttandence();
            obj.ShowDialog();
    
        }

        private void ChkPresent_CheckedChanged(object sender, EventArgs e)
        {
            if (CmbStudentName.Text == "Select")
            {
                MessageBox.Show("Please Select student Name...");
            }
            //ChkPresent.Checked = true;
            ChkAbsent.Checked = false;
            ChkLeave.Checked = false;
        }

        private void ChkAbsent_CheckedChanged(object sender, EventArgs e)
        {
           // ChkAbsent.Checked = true;
            ChkPresent.Checked = false;
            ChkLeave.Checked = false;
        }

        private void ChkLeave_CheckedChanged(object sender, EventArgs e)
        {
            //ChkLeave.Checked = true;
            ChkAbsent.Checked = false;
            ChkPresent.Checked = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dtpdate_Leave(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dtpdate.Text, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            //  bool ret = true;
            if (CmbStudentName.Text == "-Select-" || CmbStudentName.Text == "")
            {
            }
            foreach (int j in CmbStudentName.SelectedValue.ToString())
            {
                com = new SqlCommand("Select * from tbl_StudentDailyPresent where Stu_id='" + CmbStudentName.SelectedValue + "' and Month='" + dt1.Month + "' and year='" + dt1.Year + "'", sc);
                sc.Open();
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    //  ret = true;
                }
                else
                {
                    if (CmbStudentName.SelectedValue.ToString() == "0")
                    {
                    }
                    else
                    {
                        dataclass dc1 = new dataclass();
                        SqlConnection sc1 = dc1.con();
                        com = new SqlCommand("Insert into tbl_StudentDailyPresent (stu_id,Month,Year)values ('" + CmbStudentName.SelectedValue + "','" + dt1.Month + "','" + dt1.Year + "')", sc1);
                        sc1.Open();
                        int i = com.ExecuteNonQuery();
                        if (i > 0)
                        {

                        }

                        // ret = false;
                        sc1.Close();

                        com = new SqlCommand("Insert into tbl_StudentPresent (stu_id)values ('" + CmbStudentName.SelectedValue + "')", sc1);
                        sc1.Open();
                        int i1 = com.ExecuteNonQuery();
                        if (i1 > 0)
                        {

                        }
                        sc1.Close();
                    }
                }
                // return ret;
                sc.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dtpdate.Text, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            com = new SqlCommand("select * from tbl_StudentDailyPresent where stu_id='" + CmbStudentName.SelectedValue + "'", sc);
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {

                //if (dr["DAY1"].ToString() == "0" || dr["DAY1"].ToString()=="")
                //{


                int j = dt1.Day;
                if (j == 1)
                {
                    var = "Day1";
                    VarT = "Day1";

                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;

                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 2)
                {
                    var = "Day2";
                    VarT = "Day2";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 3)
                {
                    var = "Day3";
                    VarT = "Day3";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 3)
                {
                    var = "Day3";
                    VarT = "Day3";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 4)
                {
                    var = "Day4";
                    VarT = "Day4";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 5)
                {
                    var = "Day5";
                    VarT = "Day5";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 6)
                {
                    var = "Day6";
                    VarT = "Day6";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 7)
                {
                    var = "Day7";
                    VarT = "Day7";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 8)
                {
                    var = "Day8";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 9)
                {
                    var = "Day10";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 11)
                {
                    var = "Day11";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 12)
                {
                    var = "Day12";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 13)
                {
                    var = "Day13";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 14)
                {
                    var = "Day14";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 15)
                {
                    var = "Day15";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 16)
                {
                    var = "Day16";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 17)
                {
                    var = "Day17";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 18)
                {
                    var = "Day18";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 19)
                {
                    var = "Day19";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 20)
                {
                    var = "Day20";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 21)
                {
                    var = "Day21";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 22)
                {
                    var = "Day22";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 23)
                {
                    var = "Day23";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 24)
                {
                    var = "Day24";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 25)
                {
                    var = "Day25";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 26)
                {
                    var = "Day26";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 27)
                {
                    var = "Day27";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 28)
                {
                    var = "Day28";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 29)
                {
                    var = "Day29";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 30)
                {
                    var = "Day30";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else if (j == 31)
                {
                    var = "Day31";
                    VarT = "Day31";
                    if (ChkPresent.Checked == true)
                    {
                        p = 1;

                    }
                    else if (ChkAbsent.Checked == true)
                    {
                        p = 2;
                    }
                    else
                    {
                        p = 3;

                    }
                    UpdateAttandence1(var);
                    TotalAttandence(VarT);
                }
                else
                {
                }
            }

            sc.Close();

        }
    }
}
