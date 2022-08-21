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
using System.Reflection;
using System.IO;

namespace sms
{
    public partial class frmSalary : Form
    {
        string fileName;
        string name = "";
        string empcode;
        string mnth;
        BLclass bl = new BLclass();
        //MsgBox msg = new MsgBox(Color.Red, Color.WhiteSmoke, Color.WhiteSmoke, Color.Red, Color.Red, Color.WhiteSmoke, FlatStyle.Flat);
        //MsgBoxRetType mg = new MsgBoxRetType(Color.Red, Color.WhiteSmoke, Color.WhiteSmoke, Color.Red, Color.Red, Color.WhiteSmoke, FlatStyle.Flat);
        int tmpCount;
        public frmSalary()
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
            Cancel_Form();

            tmpCount = 0;
        }
        private void UpperCase(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }
        private void Cancel_Form()
        {
            name = ""; fileName = "";
            btRegister.Enabled = true;
            btUpdate.Enabled = false;
            btDelete.Enabled = false;
            txtName.Text = "";
            txtRemark.Text = "";
            txtfather.Text = "";
            cmbEmpCode.Text = "Select";
            txtPinCode.Text = "0";
            txtfather.Text = "";
            txtBasicSalary.Text = "0";
            txtTDeduction.Text = "0";
            txtBasicSalary.Text = "0";
            txtOthers.Text = "0";
            txtNetSal.Text = "0";
            txtpfno.Text = "0";
            txtesino.Text = "0";
            txtPfPercent.Text = "0";
            txtEsiPercent.Text = "0";
            txtNoOfDaysWrk.Text = "0";
            txtDept.Text = "";
            txtDesig.Text = "";

            txtTA.Text = "0";
            txtDA.Text = "0";
            txtHra.Text = "0";
            txtAdv.Text = "0";
            txtTotalWages.Text = "0";
            txtSpAllowance.Text = "0";
            txtTEarning.Text = "0";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";


        }
        private void Cancel_Form3()
        {
            //btRegister.Enabled = true;
            //btUpdate.Enabled = false;
            //btDelete.Enabled = false;
            //txtRemark.Text = "";
            //txtTDeduction.Text = "0";
            //txtOthers.Text = "0";
            //txtNetSal.Text = "0";
            //txtpfno.Text = "0";
            //txtesino.Text = "0";
            //txtPfPercent.Text = "";
            //txtEsiPercent.Text = "";

            //txtTA.Text = "0";
            //txtDA.Text = "0";
            //txtHra.Text = "0";
            //txtAdv.Text = "0";
            //txtSpAllowance.Text = "0";
            //txtTEarning.Text = "0";

        }

        private void BindEmpCode()
        {
            txtyear.Text = "2020";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from EMPLOYEE_T order by emp_code", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["emp_code"] = "Select";
            row["emp_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbEmpCode.DataSource = ds.Tables[0];
            cmbEmpCode.DisplayMember = "emp_code";
            cmbEmpCode.ValueMember = "emp_ID";
        }


        private void btRegister_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbEmpCode.Text == "" || cmbEmpCode.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Employee code...");
                return;
            }
            if (txtPfPercent.Text == "" || txtPfPercent.Text == "Select")
            {
                MsgBox.ShowBox("PF percent should not be blank...");
                return;
            }
            if (txtEsiPercent.Text == "" || txtEsiPercent.Text == "Select")
            {
                MsgBox.ShowBox("ESI percent should not be blank...");
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insert_tblSalary";
                cmd.Connection = sc;
                cmd.Parameters.AddWithValue("@Emp_Code", cmbEmpCode.Text);
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DOL.Value, System.Globalization.CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@SAL_BASIC", Convert.ToDecimal((txtBasicSalary.Text)));
                cmd.Parameters.AddWithValue("@WorkingDays", Convert.ToInt32((txtNoOfDaysWrk.Text)));
                cmd.Parameters.AddWithValue("@TotalSalary", Convert.ToDecimal(txtTotalWages.Text));
                cmd.Parameters.AddWithValue("@SAL_EPF", Convert.ToDecimal(txtpfno.Text));
                cmd.Parameters.AddWithValue("@SAL_ESI", Convert.ToDecimal((txtesino.Text)));
                cmd.Parameters.AddWithValue("@Advance", Convert.ToDecimal((txtAdv.Text)));
                cmd.Parameters.AddWithValue("@OtherDeductions", Convert.ToDecimal(txtOthers.Text));

                cmd.Parameters.AddWithValue("@TA", Convert.ToDecimal(txtTA.Text));
                cmd.Parameters.AddWithValue("@DA", Convert.ToDecimal(txtDA.Text));
                cmd.Parameters.AddWithValue("@HRA", Convert.ToDecimal(txtHra.Text));
                cmd.Parameters.AddWithValue("@SpecialAllowance", Convert.ToDecimal(txtSpAllowance.Text));
                cmd.Parameters.AddWithValue("@TotalEarnings", Convert.ToDecimal(txtTEarning.Text));

                cmd.Parameters.AddWithValue("@TotalDeduction", Convert.ToDecimal(txtTDeduction.Text));
                cmd.Parameters.AddWithValue("@NetSalary", Convert.ToDecimal(txtNetSal.Text));
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                cmd.Parameters.AddWithValue("@month", cmbyear.SelectedValue);
                cmd.Parameters.AddWithValue("@year", txtyear.Text);
                //cmd.Transaction = tran;
                sc.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MsgBox.ShowBox("Salary created successfully...");
                    
                    if (Form1.SMS)
                        sms1();
                    DialogResult dialogResult = MessageBox.Show("SMS sent successfully");

                    LoadGrid();
                    Cancel_Form();
                }
                else
                { MsgBox.ShowBox("Salary already created of this month..."); }
                sc.Close();
                LoadGrid();
            }
        }
        private void sms1()
        {

            try
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string msgtxt = "Dear Sir, " + Form1.schname + " ,Your Salary is Created for the month of " + cmbyear.Text.Trim() + " and Amount is " + txtNetSal.Text + ". further enquiry  Contact " + textBox8.Text + "," + textBox9.Text.Trim().Replace(" ", "") + ".Thanks ";
                DAL.smsclass SMS = new sms.DAL.smsclass();
                SMS.SMS(msgtxt, 0, textBox1.Text.Trim(),txtName.Text);
            }

            catch (Exception ex)
            {
                //tr.Abort();
                MessageBox.Show("SMS not send... !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //data.Close();
                //reader.Close();
            }

        }
        private void btUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (cmbEmpCode.Text == "")
            {
                MsgBox.ShowBox("Please select Employee Code!!!");
                return;
            }
            if (txtPfPercent.Text == "")
            {
                MsgBox.ShowBox("PF Percent Should not be blank...");
            }
            if (txtEsiPercent.Text == "")
            {
                MsgBox.ShowBox("ESI Percent Should not be blank...");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "update_tblSalary2";
                cmd.Connection = sc;
                cmd.Parameters.AddWithValue("@sal_id", Convert.ToInt32(txtSalaryId.Text));
                cmd.Parameters.AddWithValue("@Emp_Code", cmbEmpCode.Text);
                cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(DOL.Value, System.Globalization.CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@SAL_BASIC", Convert.ToDecimal((txtBasicSalary.Text)));

                cmd.Parameters.AddWithValue("@WorkingDays", Convert.ToInt32((txtNoOfDaysWrk.Text)));
                cmd.Parameters.AddWithValue("@SAL_EPF", Convert.ToDecimal(txtpfno.Text));
                cmd.Parameters.AddWithValue("@SAL_ESI", Convert.ToDecimal((txtesino.Text)));
                cmd.Parameters.AddWithValue("@OtherDeductions", Convert.ToDecimal(txtOthers.Text));
                cmd.Parameters.AddWithValue("@TotalSalary", Convert.ToDecimal(txtTotalWages.Text));
                cmd.Parameters.AddWithValue("@TotalDeduction", Convert.ToDecimal(txtTDeduction.Text));
                cmd.Parameters.AddWithValue("@NetSalary", Convert.ToDecimal(txtNetSal.Text));
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                //cmd.Parameters.AddWithValue("@month1", cmbyear.Text);
                //cmd.Parameters.AddWithValue("@year1", txtyear.Text);
                //cmd.Transaction = tran;
                sc.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MsgBox.ShowBox("Salary updated successfully...");
                    Cancel_Form();
                }
                else
                { MsgBox.ShowBox("Salary updating error..."); }
                sc.Close();
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }


        private void btDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtSalaryId.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblsalary WHERE sal_id=" + txtSalaryId.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" salary Deleted Successfully!!!");
                        Cancel_Form();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
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
            LoadGrid();
        }

        private void frmSalary_Load_1(object sender, EventArgs e)
        {
            BindEmpCode();
            LoadGrid();
            bindYear();
        }
        private void bindYear()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            if (!Form1.board)
                da = new SqlDataAdapter(" select id,year from Year_T order by id ", sc);
            else
                da = new SqlDataAdapter(" select id,year_up as year from Year_T order by id ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["id"] = 0;
            row["Year"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbyear.DataSource = ds.Tables[0];
            cmbyear.DisplayMember = "Year";
            cmbyear.ValueMember = "id";
        }

        private void showstudentreport()
        {
            string empcode;
            if (cmbEmpCode.Text == "" || cmbEmpCode.Text == "Select")
            {
                empcode = "%";
            }
            else
            {
                empcode = cmbEmpCode.Text.ToString();
            }
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str;
            if (cmbEmpCode.Text == "" || cmbEmpCode.Text == "Select")

                str = " select * from tblsalary where emp_CODE like '" + empcode + "%'";

            else
                str = " select * from tblsalary where emp_CODE like '" + empcode + "'";
            //    str = " select AdmNo,AccNo,DOB,Adm_Date,Student_Name,FatherName,MotherName,Sex,Class,Roll,Address,Phone  from View_StudentReport where STC_SEC_ID like '" + classname + "' and  Student_Name like '" + studentname + "%' and  FatherName like '" + fathername + "%'  and Sex like '" + sex + "' and STU_WARD like '" + ward + "'  and STU_HOSTLER like '" + hostel + "'  and CITY_NAME like '" + cityname + "' and AdmNo like '" + Admissionno + "' and STU_ANNUAL_INCOME between " + min + " and " + max + " and STU_RLG_ID like '" + category + "'  and AccNo like '" + accno + "' and Roll like '" + rollno + "' and STU_STATUS='" + deleted + "' and (Adm_Date between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "') order by Class,Roll,Student_Name";


            if (str != "")
            {
                dataGridView1.Visible = true;
                SqlDataAdapter da = new SqlDataAdapter(str, sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                {
                    dataGridView1.DataSource = dt;



                }
            }

            sc.Close();

        }

        private void LoadGrid()
        {
            string salId;
            if (cmbEmpCode.Text == "Select" || cmbEmpCode.Text == "")
            {
                salId = "%";
            }
            else
            {
                salId = cmbEmpCode.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_EmployeeSalary ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void cmbEmpCode_Leave(object sender, EventArgs e)
        {
            BindData();
            BindPf();
            BindEsi();
            BindHRA();
            BindSplAllowance();
            BindTA();
            BindDA();
            calculation();
            cmnyname();
         
            //calculateTDeduction();
            //calculateTEarning();
            //claculateNetSalary();

        }
        private void calculation()
        {
            txtTotalWages.Text = Convert.ToString((Convert.ToInt32(txtNoOfDaysWrk.Text) * Convert.ToInt32(txtBasicSalary.Text)) / 30);
            calculateTDeduction();
            calculateTEarning();
            claculateNetSalary();
            txtPinCode.Text = txtTotalWages.Text;


        }
        private void calculateTDeduction()
        {
            txtpfno.Text = Convert.ToString((Convert.ToDecimal(txtPfPercent.Text) * Convert.ToDecimal(txtTotalWages.Text)) / 100);
            txtesino.Text = Convert.ToString((Convert.ToDecimal(txtEsiPercent.Text) * Convert.ToDecimal(txtTotalWages.Text)) / 100);

            txtTDeduction.Text = Convert.ToString(Convert.ToDecimal(txtpfno.Text) + Convert.ToDecimal(txtesino.Text) + Convert.ToDecimal(txtAdv.Text) + Convert.ToDecimal(txtOthers.Text));


        }
        private void calculateTEarning()
        {
        //    txtTA.Text = Convert.ToString(Convert.ToDecimal(txtBasicSalary.Text) * Convert.ToDecimal(txtTAperc.Text) / 100);
        //    txtDA.Text = Convert.ToString(Convert.ToDecimal(txtBasicSalary.Text) * Convert.ToDecimal(txtDAperc.Text) / 100);
        //    txtHra.Text = Convert.ToString(Convert.ToDecimal(txtTotalWages.Text) * Convert.ToDecimal(txtHraPerc.Text) / 100);

        //    txtTEarning.Text = Convert.ToString(Convert.ToDecimal(txtTA.Text) + Convert.ToDecimal(txtDA.Text) + Convert.ToDecimal(txtSpAllowance.Text) + Convert.ToDecimal(txtHra.Text));
        }
        private void claculateNetSalary()
        {
            txtNetSal.Text = Convert.ToString(Convert.ToDecimal(txtTotalWages.Text) + Convert.ToDecimal(txtTEarning.Text) - Convert.ToDecimal(txtTDeduction.Text));
        }

        private void BindData()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_salary where  emp_id like '" + cmbEmpCode.SelectedValue + "'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                // emp_id = dt.Rows[0]["emp_id"].ToString();
                txtName.Text = dt.Rows[0]["emp_name"].ToString();
                txtfather.Text = dt.Rows[0]["emp_relative_name"].ToString();
                txtDept.Text = dt.Rows[0]["dept_desc"].ToString();
                txtDesig.Text = dt.Rows[0]["desg_desc"].ToString();
                txtBasicSalary.Text = dt.Rows[0]["emp_basic_salary"].ToString();
                textBox1.Text = dt.Rows[0]["emp_mobile"].ToString();

                txtPfPercent.Text = dt.Rows[0]["emp_pf_no"].ToString();
                txtEsiPercent.Text = dt.Rows[0]["emp_esi_no"].ToString();
                txtTAperc.Text = dt.Rows[0]["emp_esi_no"].ToString();
                textBox2.Text = dt.Rows[0]["emp_cardno"].ToString();

                calculatePresentDays();
                Cancel_Form3();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
            else
            {
                //    cmb_class.SelectedValue = 0;
                //    ROLL = "0";
                //    Stu_id = "0";
                // stuname.Text = "";
                // BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }

        }


        private void cmnyname()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from company_t", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textBox8.Text = dt.Rows[0]["com_name"].ToString();
                textBox9.Text = dt.Rows[0]["companytelno"].ToString();
            }
        }
        private void BindPf()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ALLOWANCES_T where at_desc like 'Pf' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtPfPercent.Text = dt.Rows[0]["at_value"].ToString();
            }


        }


        private void BindEsi()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ALLOWANCES_T where at_desc like 'Esi' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtEsiPercent.Text = dt.Rows[0]["at_value"].ToString();
            }



        }

        private void BindHRA()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ALLOWANCES_T where at_desc like 'Hra' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtHraPerc.Text = dt.Rows[0]["at_value"].ToString();
            }

        }

        private void BindSplAllowance()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ALLOWANCES_T where at_desc like 'Spl. allowence' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtSpAllowance.Text = dt.Rows[0]["at_value"].ToString();
            }

        }

        private void BindTA()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ALLOWANCES_T where at_desc like 'ta'  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtTAperc.Text = dt.Rows[0]["at_value"].ToString();
            }

        }
        private void BindDA()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ALLOWANCES_T where at_desc like 'da' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtDAperc.Text = dt.Rows[0]["at_value"].ToString();
            }

        }





        private void chkData()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblSalary where  emp_code like '" + cmbEmpCode.Text + "' and date like '" + DOL.Value + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtSalaryId.Text = dt.Rows[0]["sal_id"].ToString();

                btUpdate.Enabled = true;
            }
            else
            {
                //    cmb_class.SelectedValue = 0;
                //    ROLL = "0";
                //    Stu_id = "0";
                // stuname.Text = "";
                // BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }

        }

        private void calculatePresentDays()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select COUNT(ATT_TYPE) as workingDays from EMPLOYEE_ATTENDANCE_T where  sat_stu_id = '" + cmbEmpCode.SelectedValue + "' and att_type like 'P' AND MONTH ='" + cmbyear.SelectedValue + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                textBox3.Text = dt.Rows[0]["workingDays"].ToString();
            }
            dataclass dcl = new dataclass();
            SqlConnection sc1 = dc.con();
            SqlDataAdapter dl = new SqlDataAdapter("select COUNT(ATT_TYPE) as workingDays from EMPLOYEE_ATTENDANCE_T where  sat_stu_id = '" + cmbEmpCode.SelectedValue + "' and att_type like 'LW' AND MONTH ='" + cmbyear.SelectedValue + "' ", sc);
            DataTable dtl = new DataTable();
            dl.Fill(dtl);
            if (dtl.Rows.Count > 0)
            {
                textBox4.Text = dtl.Rows[0]["workingDays"].ToString();
            }
            dataclass dch = new dataclass();
            SqlConnection sch = dc.con();
            SqlDataAdapter dh = new SqlDataAdapter("select COUNT(ATT_TYPE) as workingDays from EMPLOYEE_ATTENDANCE_T where  sat_stu_id = '" + cmbEmpCode.SelectedValue + "' and att_type like 'H' AND MONTH ='" + cmbyear.SelectedValue + "' ", sc);
            DataTable dth = new DataTable();
            dh.Fill(dth);
            if (dth.Rows.Count > 0)
            {
                textBox5.Text = dth.Rows[0]["workingDays"].ToString();
            }


            txtTotalWages.Text = Convert.ToString((Convert.ToInt32(txtNoOfDaysWrk.Text) * Convert.ToInt32(txtBasicSalary.Text)) / 30);
            txtPinCode.Text = txtTotalWages.Text;
        }

        private void txtOthers_Leave(object sender, EventArgs e)
        {
            calculation();
            //if(txtesino.Text=="")
            //         txtesino.Text="0";
            //if(txtpfno.Text=="")
            //         txtpfno.Text="0";
            //if (txtOthers.Text=="")
            //    txtOthers.Text = "0";
            //txtTDeduction.Text = Convert.ToString((Convert.ToDecimal(txtesino.Text) + Convert.ToDecimal(txtpfno.Text) + Convert.ToDecimal(txtOthers.Text)));
            //txtNetSal.Text = Convert.ToString((Convert.ToDecimal(txtTotalWages.Text) - Convert.ToDecimal(txtTDeduction.Text)));

        }


        private void cmbEmpCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();

        }


        private void DOL_Leave(object sender, EventArgs e)
        {
            chkData();
        }

        private void txtAdv_Leave(object sender, EventArgs e)
        {
            calculation();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            btRegister.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtSalaryId.Text = dataGridView1.CurrentRow.Cells["sal_id"].Value.ToString();
                txtAdv.Text = dataGridView1.CurrentRow.Cells["Advance"].Value.ToString();
                txtOthers.Text = dataGridView1.CurrentRow.Cells["OtherDeductions"].Value.ToString();
                cmbEmpCode.Text = dataGridView1.CurrentRow.Cells["emp_code"].Value.ToString();
                cmbyear.Text = dataGridView1.CurrentRow.Cells["month1"].Value.ToString();

                //txtvalue.Text = dataGridView1.CurrentRow.Cells["AT_VALUE"].Value.ToString();
                btUpdate.Enabled = true;
                btDelete.Enabled = true;
            }
        }

        private void txtNoOfDaysWrk_Leave(object sender, EventArgs e)
        {
            calculation();
        }

        private void txtSpAllowance_Leave(object sender, EventArgs e)
        {
            calculation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cryEmpsal.empcode = cmbEmpCode.Text;
            cryEmpsal.mnth = cmbyear.SelectedValue.ToString();

            cryEmpsal obj = new cryEmpsal();
            obj.ShowDialog();
        }

        private void cmbEmpCode_TextChanged(object sender, EventArgs e)
        {
            showstudentreport();
        }

        private void TOTALPRESENT()
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select COUNT(ATT_TYPE) AS ATTENDANCE from EMPLOYEE_ATTENDANCE_T where MONTH ='" + cmbyear.SelectedValue + "' and ATT_TYPE='p' and SAT_STU_ID='" + cmbEmpCode.SelectedValue + "' ", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    txtNoOfDaysWrk.Text = dt.Rows[0]["ATTENDANCE"].ToString();

                
            //}



        }

        private void cmbyear_Leave(object sender, EventArgs e)
         {
            calculatePresentDays();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {

            textBox7.Text = Convert.ToString(Convert.ToInt16(Convert.ToDecimal(textBox3.Text) + ((Convert.ToDecimal(textBox4.Text) - (Convert.ToDecimal(textBox4.Text) / 8))) + ((Convert.ToDecimal(textBox5.Text)-(Convert.ToDecimal(textBox5.Text) / 2))) + Convert.ToDecimal(textBox6.Text)));


            }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            txtNoOfDaysWrk.Text=Convert.ToString(Convert.ToInt16(textBox7.Text));

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }


        }
    }
