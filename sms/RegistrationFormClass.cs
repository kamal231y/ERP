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
    public partial class RegistrationFormClass : Form
    {
        DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);

        //string imgae = "";
        BLclass bl = new BLclass();
        validation v1 = new validation();
        public RegistrationFormClass()
        {
            InitializeComponent();
        }

       
        private void RegistrationFormClass_Load(object sender, EventArgs e)
        {
            bindgroup();
            Bindclass();
        //   BindRNo();
           bindSub1();
           bindSub2();
           bindSub3();
           bindSub4();
           bindSub5();
           bindSub6();
           BindHandicap();
           
        }
        private void Bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID and cls_id in (14,12) order by SEC_ID   ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbClass1.DataSource = dt;
            cmbClass1.DisplayMember = "class";
            cmbClass1.ValueMember = "SEC_ID";
            sc.Close();
           
        }
        private void bindgroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select distinct(sub_group)  from tblCBSESUBENROLLMENT ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            DataRow row = ds.NewRow();
             row["sub_group"] = "Select";
             row["sub_group"] = "Select";
             ds.Rows.InsertAt(row, 0);
            cmbGroup.DataSource = ds;
            cmbGroup.DisplayMember = "sub_group";
            cmbGroup.ValueMember = "sub_group";
            
        }
        private void BindRNo()
        {
            try
            {
                string class1;
                if (cmbClass1.Text == "Select" || cmbClass1.Text == "")
                {
                    class1 = "%";
                }
                else
                {
                    class1 = cmbClass1.SelectedValue.ToString();
                }
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_T where STU_CLS_ID =  " + cmbClass1.SelectedValue + "   order by  STU_DOA", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row["STU_ADM_NO"] = "Select";
                row["STU_ID"] = 0;
                dt.Rows.InsertAt(row, 0);
                cmbroll.DataSource = dt;
                cmbroll.DisplayMember = "STU_ADM_NO";
                cmbroll.ValueMember = "STU_ID";
            }
            catch (Exception ex) { }
        }

       private void bindSub1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select  SUB_CODE,SUB_NAME from CBSE_REG_SUBJECT WHERE SUB_TYPE=1 order by SUB_NAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_NAME"] = "Select";
            row["SUB_CODE"] = "Select";
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub1.DataSource = ds.Tables[0];
            cmbSub1.DisplayMember = "SUB_NAME";
            cmbSub1.ValueMember = "SUB_CODE";
       }
       private void bindSub2()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter(" select  SUB_CODE,SUB_NAME from CBSE_REG_SUBJECT WHERE SUB_TYPE=2 order by SUB_NAME", sc);
           DataSet ds = new DataSet();
           da.Fill(ds);
           DataRow row = ds.Tables[0].NewRow();
           row["SUB_NAME"] = "Select";
           row["SUB_CODE"] = "Select";
           ds.Tables[0].Rows.InsertAt(row, 0);
           cmbSub2.DataSource = ds.Tables[0];
           cmbSub2.DisplayMember = "SUB_NAME";
           cmbSub2.ValueMember = "SUB_CODE";
       }
       private void bindSub3()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter(" select  SUB_CODE,SUB_NAME from CBSE_REG_SUBJECT WHERE SUB_TYPE=3 order by SUB_NAME", sc);
           DataSet ds = new DataSet();
           da.Fill(ds);
           DataRow row = ds.Tables[0].NewRow();
           row["SUB_NAME"] = "Select";
           row["SUB_CODE"] = "Select";
           ds.Tables[0].Rows.InsertAt(row, 0);
           cmbSub3.DataSource = ds.Tables[0];
           cmbSub3.DisplayMember = "SUB_NAME";
           cmbSub3.ValueMember = "SUB_CODE";
       }
       private void bindSub4()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter(" select  SUB_CODE,SUB_NAME from CBSE_REG_SUBJECT WHERE SUB_TYPE=4 order by SUB_NAME", sc);
           DataSet ds = new DataSet();
           da.Fill(ds);
           DataRow row = ds.Tables[0].NewRow();
           row["SUB_NAME"] = "Select";
           row["SUB_CODE"] = "Select";
           ds.Tables[0].Rows.InsertAt(row, 0);
           cmbSub4.DataSource = ds.Tables[0];
           cmbSub4.DisplayMember = "SUB_NAME";
           cmbSub4.ValueMember = "SUB_CODE";
       }
       private void bindSub5()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter(" select  SUB_CODE,SUB_NAME from CBSE_REG_SUBJECT WHERE SUB_TYPE=5 order by SUB_NAME", sc);
           DataSet ds = new DataSet();
           da.Fill(ds);
           DataRow row = ds.Tables[0].NewRow();
           row["SUB_NAME"] = "Select";
           row["SUB_CODE"] = "Select";
           ds.Tables[0].Rows.InsertAt(row, 0);
           cmbSub5.DataSource = ds.Tables[0];
           cmbSub5.DisplayMember = "SUB_NAME";
           cmbSub5.ValueMember = "SUB_CODE";
       }
       private void bindSub6()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlDataAdapter da = new SqlDataAdapter(" select  SUB_CODE,SUB_NAME from CBSE_REG_SUBJECT WHERE SUB_TYPE=6 order by SUB_NAME", sc);
           DataSet ds = new DataSet();
           da.Fill(ds);
           DataRow row = ds.Tables[0].NewRow();
           row["SUB_NAME"] = "Select";
           row["SUB_CODE"] = "Select";
           ds.Tables[0].Rows.InsertAt(row, 0);
           cmbSub6.DataSource = ds.Tables[0];
           cmbSub6.DisplayMember = "SUB_NAME";
           cmbSub6.ValueMember = "SUB_CODE";
       }

        private void BindHandicap()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblHandicapped order by Handicap_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Handicap_DESC"] = "Select";
            row["HandicapId"] = "0";
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbHandicap.DataSource = ds.Tables[0];
            cmbHandicap.DisplayMember = "Handicap_DESC";
            cmbHandicap.ValueMember = "HandicapId";
        }


        private void cmbClass1_Leave(object sender, EventArgs e)
        {
          BindRNo();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbroll_Leave(object sender, EventArgs e)
        {
            
                if (cmbroll.Text != "" || cmbroll.Text != null)
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                   // SqlCommand cmd = new SqlCommand("select * from View_STUDENT_9TH_REGISTRATION WHERE STU_ID='" + cmbroll.SelectedValue + "'", sc);
                    SqlCommand cmd = new SqlCommand("select * from View_STUDENT_9TH_REGISTRATION WHERE STU_ID='" + cmbroll.SelectedValue + "'", sc);
                    sc.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtstuid.Text = dr["Stu_id"].ToString();
                        txtStudentName.Text = dr["Student_Name"].ToString();
                        txtMotherName.Text = dr["MotherName"].ToString();
                        txtFatherName.Text = dr["FatherName"].ToString();
                        txtAccNo.Text = dr["AccNo"].ToString();
                        txtAdmNo.Text = dr["AdmNo"].ToString();
                        txtCaste.Text = (dr["STU_CASTE_ID"].ToString());
                        txtGender.Text = (dr["STU_SEX"].ToString());
                        dtp1.Text = (dr["Adm_Date"].ToString());
                        txtRegno.Text = dr["REG_NO"].ToString();
                        dtpDOB.Text = (dr["DOB"].ToString());
                        txtAmount.Text = (dr["STU_ANNUAL_INCOME"].ToString());
                        string str = dr["STU_HANDICAPPED"].ToString();
                        if (dr["STU_HANDICAPPED"] == "")
                        {
                            cmbHandicap.SelectedValue = Convert.ToInt32(dr["STU_HANDICAPPED"].ToString());
                        }
                        if (dr["REG_TYPE"] != null)
                        {
                            cmbGroup.SelectedValue = dr["REG_TYPE"].ToString();
                        }
                        if (dr["ONLY_CHILD"] != null)
                        {
                            if (dr["ONLY_CHILD"].ToString() == "1")
                                rdbyes.Checked = true;
                            else
                                rdbNo.Checked = true;
                        }
                        if (dr["CODE1"] != null)
                        {
                            cmbSub1.SelectedValue = dr["CODE1"].ToString();
                            txtSub1.Text = dr["CODE1"].ToString();

                            if (dr["MED1"].ToString() == "1")
                                rbtnE1.Checked = true;
                            else
                                rbtnH1.Checked = true;
                        }
                        if (dr["CODE2"] != null)
                        {
                            cmbSub2.SelectedValue = dr["CODE2"].ToString();
                            txtSub2.Text = dr["CODE2"].ToString();
                            if (dr["MED2"].ToString() == "1")
                                rbtnE2.Checked = true;
                            else
                                rbtnH2.Checked = true;
                        }
                        if (dr["CODE3"] != null)
                        {
                            cmbSub3.SelectedValue = dr["CODE3"].ToString();
                            txtSub3.Text = dr["CODE3"].ToString();
                            if (dr["MED3"].ToString() == "1")
                                rbtnE3.Checked = true;
                            else
                                rbtnH3.Checked = true;
                        }
                        if (dr["CODE4"] != null)
                        {
                            cmbSub4.SelectedValue = dr["CODE4"].ToString();
                            txtSub4.Text = dr["CODE4"].ToString();
                            if (dr["MED4"].ToString() == "1")
                                rbtnE4.Checked = true;
                            else
                                rbtnH4.Checked = true;
                        }
                        if (dr["CODE5"] != null)
                        {
                            cmbSub5.SelectedValue = dr["CODE5"].ToString();
                            txtSub5.Text = dr["CODE5"].ToString();
                            if (dr["MED5"].ToString() == "1")
                                rbtnE5.Checked = true;
                            else
                                rbtnH5.Checked = true;
                        }
                        if (dr["CODE6"] != null)
                        {
                            cmbSub6.SelectedValue = dr["CODE6"].ToString();
                            txtSub6.Text = dr["CODE6"].ToString();
                            if (dr["MED6"].ToString() == "1")
                                rbtnE6.Checked = true;
                            else
                                rbtnH6.Checked = true;
                        }
                     }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("select TOP 1 REG_NO from STUDENT_CBSE_REG_T where STATUS=0  AND STU_ID1='" + cmbroll.SelectedValue + "' ORDER BY SER_NO", sc);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            txtStudentName.Text = dr["Student_Name"].ToString();
                        }
                    }   
               }
        }

        private void cmbSub1_Leave(object sender, EventArgs e)
        {        
                txtSub1.Text = cmbSub1.SelectedValue.ToString();
         
          }

        private void cmbSub2_Leave(object sender, EventArgs e)
        {
            txtSub2.Text = cmbSub2.SelectedValue.ToString();

        }

        private void cmbSub3_Leave(object sender, EventArgs e)
        {
            txtSub3.Text = cmbSub3.SelectedValue.ToString();

        }

        private void cmbSub4_Leave(object sender, EventArgs e)
        {
            txtSub4.Text = cmbSub4.SelectedValue.ToString();

        }

        private void cmbSub5_Leave(object sender, EventArgs e)
        {
            txtSub5.Text = cmbSub5.SelectedValue.ToString();

        }

        private void cmbSub6_Leave(object sender, EventArgs e)
        {
            txtSub6.Text = cmbSub6.SelectedValue.ToString();

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            if (cmbClass1.Text == "" || cmbClass1.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Class");
                return;
            }
            else if (cmbroll.Text == "" || cmbroll.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Adm No.");
                return;
            }

            else if (cmbGroup.Text == "" || cmbGroup.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Group");
                return;
            }
            else if (cmbHandicap.Text == "" || cmbHandicap.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Handicapped");
                return;
            }  
            else
            {
                SqlCommand com = new SqlCommand();

                com.CommandText = "SP_STUDENT_CBSE_REG_T";
                com.Connection = sc;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Reg_no", txtRegno.Text);
                com.Parameters.AddWithValue("@stu_id",Convert.ToInt32( cmbroll.SelectedValue));
                com.Parameters.AddWithValue("@STU_CLS", Convert.ToInt32(cmbClass1.SelectedValue));
                com.Parameters.AddWithValue("@STU_HANDICAPPED", Convert.ToInt32(cmbHandicap.SelectedValue));
                com.Parameters.AddWithValue("@SUB1", cmbSub1.Text);
                com.Parameters.AddWithValue("@SUB2", cmbSub2.Text);
                com.Parameters.AddWithValue("@SUB3", cmbSub3.Text);
                com.Parameters.AddWithValue("@SUB4", cmbSub4.Text);
                com.Parameters.AddWithValue("@SUB5", cmbSub5.Text);
                com.Parameters.AddWithValue("@SUB6", cmbSub6.Text);
                com.Parameters.AddWithValue("@CODE1", (txtSub1.Text));
                com.Parameters.AddWithValue("@CODE2",(txtSub2.Text));
                com.Parameters.AddWithValue("@CODE3", (txtSub3.Text));
                com.Parameters.AddWithValue("@CODE4", (txtSub4.Text));
                com.Parameters.AddWithValue("@CODE5", (txtSub5.Text));
                com.Parameters.AddWithValue("@CODE6",(txtSub6.Text));

                int type1; int type2; int type3; int type4; int type5; int type6;

                if (rbtnE1.Checked == true)
                {
                    type1 = 1;
                }
                else
                {
                    type1 = 0;
                }
                if (rbtnE2.Checked == true)
                {
                    type2 = 1;
                }
                else
                {
                    type2 = 0;
                }
                if (rbtnE3.Checked == true)
                {
                    type3 = 1;
                }
                else
                {
                    type3 = 0;
                }
                if (rbtnE4.Checked == true)
                {
                    type4 = 1;
                }
                else
                {
                    type4 = 0;
                }
                if (rbtnE5.Checked == true)
                {
                    type5 = 1;
                }
                else
                {
                    type5 = 0;
                }
                if (rbtnE6.Checked == true)
                {
                    type6 = 1;
                }
                else
                {
                    type6 = 0;
                }


                com.Parameters.AddWithValue("@MED1", type1);
                com.Parameters.AddWithValue("@MED2", type2);
                com.Parameters.AddWithValue("@MED3", type3);
                com.Parameters.AddWithValue("@MED4", type4);
                com.Parameters.AddWithValue("@MED5", type5);
                com.Parameters.AddWithValue("@MED6", type6);
                //for (int z = 0; z < com.Parameters.Count; z++)
                //{
                //    label56.Text =label56.Text+","+ com.Parameters[z].Value.ToString();
                //}
                //MessageBox.Show(label56.Text);
                //com.Parameters.AddWithValue("@LAST_EXAM", txtExam.Text);
                //com.Parameters.AddWithValue("@LAST_EXAM_ROLL", txtRollNo.Text);
                //com.Parameters.AddWithValue("@LAST_EXAM_YEAR", txtYear.Text);
                //com.Parameters.AddWithValue("@LAST_EXAM_BOARD", txtBoard.Text);
                int ss = 0;
                if (rdbyes.Checked == true)
                {
                    ss = 1;

                }
                else 
                {
                    ss = 0;

                }
                com.Parameters.AddWithValue("@ONLY_CHILD", ss);
                com.Parameters.AddWithValue("@REG_Type", cmbGroup.SelectedValue.ToString().Trim());
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr =com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Class Reg Created...");
                        cancel_form();
                        
                        
                    }
                    else
                    {
                        MsgBox.ShowBox("This Reg  already exist");
                    }

                }
                catch (Exception ex)
                {
                    MsgBox.ShowBox(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }

        }
        private void cancel_form()
        {
            btn_create.Enabled = true;
            btn_delete.Enabled = false;
            cmbClass1.Text = "-Select-";
            cmbroll.Text = "-Select-";
            txtAccNo.Text = "";
            txtAdmNo.Text = "";
            txtStudentName.Text = "";
            txtFatherName.Text = "";
            txtMotherName.Text = "";
            txtCaste.Text = "";
            txtGender.Text = "";
            dtp1.Text = "";
            cmbHandicap.Text = "-Select-";
            cmbSub1.Text = "-Select-";
            cmbSub2.Text = "-Select-";
            cmbSub3.Text = "-Select-";
            cmbSub4.Text = "-Select-";
            cmbSub5.Text = "-Select-";
            cmbSub6.Text = "-Select-";
            txtSub1.Text = "";
            txtSub2.Text = "";
            txtSub3.Text = "";
            txtSub4.Text = "";
            txtSub5.Text = "";
            txtSub6.Text = "";

            txtAmount.Text = "";
            dtpDOB.Text = "";
            txtoldRegNo.Text = "";
            txtExam.Text = "";
            txtBoard.Text = "";
            txtRollNo.Text = "";
            txtYear.Text = "";
             }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel_form();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string rollno;
            string classid;
            if (cmbClass1.Text == "" || cmbClass1.Text == "-Select-" || cmbClass1.Text == "System.Data.DataRowView")
                classid = "%";
            else
                classid = cmbClass1.SelectedValue.ToString();


            cryRegIX.classid=classid;
            cryRegIX.rollno = Convert.ToInt32(cmbroll.Text);
            cryRegIX obj = new cryRegIX();
            obj.ShowDialog();
        }

        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            if (cmbClass1.SelectedValue != null)
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataAdapter da = new SqlDataAdapter("select top 1 * from STUDENT_CBSE_REG_T where REG_TYPE='" + cmbGroup.Text.ToString().Trim() + "' and STU_CLS="+cmbClass1.SelectedValue+" ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmbSub1.SelectedValue = dt.Rows[0]["CODE1"].ToString();
                    txtSub1.Text = dt.Rows[0]["CODE1"].ToString();
                    if (dt.Rows[0]["MED1"].ToString() == "1")
                        rbtnE1.Checked = true;
                    else
                        rbtnH1.Checked = true;
                    cmbSub2.SelectedValue = dt.Rows[0]["CODE2"].ToString();
                    txtSub2.Text = dt.Rows[0]["CODE2"].ToString();
                    if (dt.Rows[0]["MED2"].ToString() == "1")
                        rbtnE2.Checked = true;
                    else
                        rbtnH2.Checked = true;
                    cmbSub3.SelectedValue = dt.Rows[0]["CODE3"].ToString();
                    txtSub3.Text = dt.Rows[0]["CODE3"].ToString();
                    if (dt.Rows[0]["MED3"].ToString() == "1")
                        rbtnE3.Checked = true;
                    else
                        rbtnH3.Checked = true;
                    cmbSub4.SelectedValue = dt.Rows[0]["CODE4"].ToString();
                    txtSub4.Text = dt.Rows[0]["CODE4"].ToString();
                    if (dt.Rows[0]["MED4"].ToString() == "1")
                        rbtnE4.Checked = true;
                    else
                        rbtnH4.Checked = true;
                    cmbSub5.SelectedValue = dt.Rows[0]["CODE5"].ToString();
                    txtSub5.Text = dt.Rows[0]["CODE5"].ToString();
                    if (dt.Rows[0]["MED5"].ToString() == "1")
                        rbtnE5.Checked = true;
                    else
                        rbtnH5.Checked = true;
                    cmbSub6.SelectedValue = dt.Rows[0]["CODE6"].ToString();
                    txtSub6.Text = dt.Rows[0]["CODE6"].ToString();
                    if (dt.Rows[0]["MED6"].ToString() == "1")
                        rbtnE6.Checked = true;
                    else
                        rbtnH6.Checked = true;
                }
            }
        }

        private void txtAccNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAdmNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRegno_TextChanged(object sender, EventArgs e)
        {

        }
        }     
    }

