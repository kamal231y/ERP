using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using sms.DATALAYER;
using System.Data.SqlClient;
using System.Data.OleDb;
using MsgBoxRet;
using System.Reflection;

namespace sms
{
    public partial class frmEmployeeAttendanceIN : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;
        string att;
        public frmEmployeeAttendanceIN()
        {
            InitializeComponent();
        }

        void LoadGrid()
        {
            DateTime dt1;
            dt1 = todaydate.Value;
            DataSet dt = new DataSet();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "Select" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("BIND_EMPLOYEE_Attendance", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(str));
            da.SelectCommand.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToString("MM/dd/yyyy"));
            da.SelectCommand.Parameters.AddWithValue("@SELECTED", chkpartpayment.Checked);
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            dataGridView1.Columns["Selected"].Width = 70;
            dataGridView1.Columns["Selected"].HeaderText = "Selected";
            dataGridView1.Columns["EMP_CODE"].Width = 60;
            dataGridView1.Columns["EMP_CODE"].HeaderText = "Code";
            dataGridView1.Columns["EMP_NAME"].Width = 200;
            dataGridView1.Columns["EMP_NAME"].HeaderText = "EmployeeName";
            dataGridView1.Columns["D1"].Width = 30;
            dataGridView1.Columns["D1"].HeaderText = "1";
            dataGridView1.Columns["D2"].Width = 30;
            dataGridView1.Columns["D2"].HeaderText = "2";
            dataGridView1.Columns["D3"].Width = 30;
            dataGridView1.Columns["D3"].HeaderText = "3";
            dataGridView1.Columns["D4"].Width = 30;
            dataGridView1.Columns["D4"].HeaderText = "4";
            dataGridView1.Columns["D5"].Width = 30;
            dataGridView1.Columns["D5"].HeaderText = "5";
            dataGridView1.Columns["D6"].Width = 30;
            dataGridView1.Columns["D6"].HeaderText = "6";
            dataGridView1.Columns["D7"].Width = 30;
            dataGridView1.Columns["D7"].HeaderText = "7";
            dataGridView1.Columns["D8"].Width = 30;
            dataGridView1.Columns["D8"].HeaderText = "8";
            dataGridView1.Columns["D9"].Width = 30;
            dataGridView1.Columns["D9"].HeaderText = "9";
            dataGridView1.Columns["D10"].Width = 30;
            dataGridView1.Columns["D10"].HeaderText = "10";
            dataGridView1.Columns["D11"].Width = 30;
            dataGridView1.Columns["D11"].HeaderText = "11";
            dataGridView1.Columns["D12"].Width = 30;
            dataGridView1.Columns["D12"].HeaderText = "12";
            dataGridView1.Columns["D13"].Width = 30;
            dataGridView1.Columns["D13"].HeaderText = "13";
            dataGridView1.Columns["D14"].Width = 30;
            dataGridView1.Columns["D14"].HeaderText = "14";
            dataGridView1.Columns["D15"].Width = 30;
            dataGridView1.Columns["D15"].HeaderText = "15";
            dataGridView1.Columns["D16"].Width = 30;
            dataGridView1.Columns["D16"].HeaderText = "16";
            dataGridView1.Columns["D17"].Width = 30;
            dataGridView1.Columns["D17"].HeaderText = "17";
            dataGridView1.Columns["D18"].Width = 30;
            dataGridView1.Columns["D18"].HeaderText = "18";
            dataGridView1.Columns["D19"].Width = 30;
            dataGridView1.Columns["D19"].HeaderText = "19";
            dataGridView1.Columns["D20"].Width = 30;
            dataGridView1.Columns["D20"].HeaderText = "20";
            dataGridView1.Columns["D21"].Width = 30;
            dataGridView1.Columns["D21"].HeaderText = "21";
            dataGridView1.Columns["D22"].Width = 30;
            dataGridView1.Columns["D22"].HeaderText = "22";
            dataGridView1.Columns["D23"].Width = 30;
            dataGridView1.Columns["D23"].HeaderText = "23";
            dataGridView1.Columns["D24"].Width = 30;
            dataGridView1.Columns["D24"].HeaderText = "24";
            dataGridView1.Columns["D25"].Width = 30;
            dataGridView1.Columns["D25"].HeaderText = "25";
            dataGridView1.Columns["D26"].Width = 30;
            dataGridView1.Columns["D26"].HeaderText = "26";
            dataGridView1.Columns["D27"].Width = 30;
            dataGridView1.Columns["D27"].HeaderText = "27";
            dataGridView1.Columns["D28"].Width = 30;
            dataGridView1.Columns["D28"].HeaderText = "28";
            dataGridView1.Columns["D29"].Width = 30;
            dataGridView1.Columns["D29"].HeaderText = "29";
            dataGridView1.Columns["D30"].Width = 30;
            dataGridView1.Columns["D30"].HeaderText = "30";
            dataGridView1.Columns["D31"].Width = 30;;
            dataGridView1.Columns["D31"].HeaderText = "31";
          
            dataGridView2.DataSource = dt.Tables[1];
            dataGridView2.Columns["P"].Width = 50;
            dataGridView2.Columns["P"].HeaderText = "P";
            dataGridView2.Columns["LW"].Width = 50;
            dataGridView2.Columns["LW"].HeaderText = "LW";
            dataGridView2.Columns["H"].Width = 50;
            dataGridView2.Columns["H"].HeaderText = "H";
            dataGridView2.Columns["A"].Width = 50;
            dataGridView2.Columns["A"].HeaderText = "A";
            dataGridView2.Columns["L"].Width = 50;
            dataGridView2.Columns["L"].HeaderText = "L";
            dataGridView2.Columns["HL"].Width = 50;
            dataGridView2.Columns["HL"].HeaderText = "HL";
            
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "DEPT_DESC";
            cmb_class.ValueMember = "DEPT_ID";
        }
        private void bindrollno()
        {
            string StateName;
            if (cmb_class.Text == "Select" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
            {
                StateName = "0";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from Employee_t where EMP_DEPT_id = '" + StateName + "' order by  EMP_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["EMP_ID"] = "0";
            row["EMP_CODE"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "EMP_CODE";
            cmb_rollno.ValueMember = "EMP_ID";
        }
       
     

        private void LoadGrid2()
        {   DateTime dt1;
            dt1 = todaydate.Value;
           string str;
            string str1;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
          
           dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select sum(P) as P,sum(A) as A,sum(L) as L, sum(OT) as OT from Attendancesum where Month1=" + dt1.Month + " and Year1=" + dt1.Year + " and classsecid=" + str + " group by ID  ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns["P"].Width = 50;
            dataGridView2.Columns["P"].HeaderText = "P";
            dataGridView2.Columns["A"].Width = 50;
            dataGridView2.Columns["A"].HeaderText = "A";
            dataGridView2.Columns["L"].Width = 50;
            dataGridView2.Columns["L"].HeaderText = "L";
            dataGridView2.Columns["OT"].Width = 50;
            dataGridView2.Columns["OT"].HeaderText = "OT";
        }
        private void Bindname()
        {
            txtStuname.Text = "";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from employee_T where emp_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["emp_NAME"].ToString();
            }

        }
        void Cancel_Form()
        {
            cmb_class.Text = "-Select-";
            cmb_rollno.Text = "";
            txtStuname.Text = "";

        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            INSERTEMP();   
        }

        private void INSERTEMP()
        {
            DateTime dt1 = Convert.ToDateTime(todaydate.Value, System.Globalization.CultureInfo.InvariantCulture);
            //---------------------
            dataclass dd1 = new dataclass();
            SqlConnection sc1 = dd1.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblattendancemaster", sc1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                DateTime dt2 = Convert.ToDateTime(dt.Rows[0]["INTIME"].ToString());
                DateTime dt3 = Convert.ToDateTime(dt.Rows[0]["GRASSTIME"].ToString());
                DateTime dt4 = Convert.ToDateTime(dt.Rows[0]["HALFTIME"].ToString());


                if (dt1 < dt2)
                    att = "P";
                else if ( dt1 < dt3)
                    att = "LW";
                else if (dt1 < dt4)
                    att = "H";
                else
                    att = "A";

            }

            //-----------------------
            //string att;
            //if (rdpresent.Checked == true)
            //    att = "P";
            //else if (rdabsent.Checked == true)
            //    att = "A";
            //else
            //    att = "L";


            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                MsgBox.ShowBox("Please Select Class !!!!!");
                return;
            }
            else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "" || cmb_rollno.Text == "0")
            {
                MsgBox.ShowBox("Please Select RollNo !!!!!");
                return;
            }

            else
            {
                sc.Open();

                try
                {
                    SqlCommand cmd1 = new SqlCommand("insert_Employee_Attendance", sc);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(cmb_rollno.SelectedValue));
                    cmd1.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToString("MM/dd/yyyy hh:mm:ss"));
                    cmd1.Parameters.AddWithValue("@type", att);
                    cmd1.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));

                    cmd1.ExecuteNonQuery();
                    LoadGrid();

                    if (rdabsent.Checked == true)
                    {
                        sms2();
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    sc.Close();
                }
            }

        }
    
        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            txtStuname.Text = "";
            Bindname();
        }

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindrollno();
            //LoadGrid();
           
            
        }

        private void frmStudentSubject_Load(object sender, EventArgs e)
        {
            bindClass(); msgtype();
        }
        private void msgtype()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select id,msgtype  from tblmsgtype order by msgtype ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["msgtype"] = "-Select-";
            row["id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbmsgtype.DataSource = ds.Tables[0];
            cmbmsgtype.DisplayMember = "msgtype";
            cmbmsgtype.ValueMember = "id";
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            DateTime dt1;
            dt1 = todaydate.Value;
            string att;
            if(rdpresent.Checked==true)
            att = "P";
            else if (rdabsent.Checked == true)
             att = "A";
            else
             att = "L";

       
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                MsgBox.ShowBox("Please Select Class !!!!!");
                return;
            }
            else
            {
                sc.Open();
               
                          
                try
                {
                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        string str11111 = dataGridView1.Rows[z].Cells["Selected"].Value.ToString();
                        if (dataGridView1.Rows[z].Cells["Selected"].Value.ToString() == "True")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("insert_EMPLOYEE_ALL_Attendance", sc);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                            da.SelectCommand.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToString("MM/dd/yyyy"));
                            da.SelectCommand.Parameters.AddWithValue("@type", att);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                    if (rdabsent.Checked == true)
                    {
                        if (Form1.SMS)
                            sms1();
                    }
                    LoadGrid();
                   
                   
                }
                catch (Exception ex)
                {
                  
                }
                finally
                {
                    sc.Close();
                }


            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
            {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport tt, CARD_T cc where tt.STU_ID=cc.STUDENT_ID and cc.CARD_NO='" + txtcardNo.Text + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmb_rollno.SelectedValue = dt.Rows[0]["Roll"].ToString();
                txtStuname.Text = dt.Rows[0]["Student_Name"].ToString();
            }
        }


        private void sms1()
        {

            if (cmb_class.Text.Trim() == "")
            {
                MessageBox.Show("Select Class !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_rollno.Focus();
            }
            //else if (txtmsg1.Text.Trim() == "")
            // {
            //MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //txtmsg.Focus();
            // }
            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from  TBLSMS", sc);
                DataTable dtsms = new DataTable();
                da.Fill(dtsms);
                if (dtsms.Rows.Count > 0)
                {
                    string msgid = dtsms.Rows[0]["userid"].ToString().Trim();
                    string msgname = dtsms.Rows[0]["Name"].ToString().Trim();
                    string msgpwd = dtsms.Rows[0]["password"].ToString().Trim();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                            if (dataGridView1.Rows[i].Cells["MobileNo"].ToString() != "" || dataGridView1.Rows[i].Cells["MobileNo"].Value != null)
                            {

                                try
                                {
                                    string msgtxt = "Dear , Your Ward " + "  " + dataGridView1.Rows[i].Cells["EMP_NAME"].Value.ToString() + ", Emp Code." + dataGridView1.Rows[i].Cells["EMP_CODE"].Value.ToString() + " is absent Today. * Thanks";
                                    string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=" + msgid + "&password=" + msgpwd + "&type=0&dlr=1&destination=" + dataGridView1.Rows[i].Cells["MobileNo"].Value.ToString().Trim() + "&source=" + msgname + "&message=" + msgtxt + "";
                                    data = client.OpenRead(baseurl);
                                    reader = new StreamReader(data);
                                    string s = reader.ReadToEnd();
                                }

                                catch (Exception ex)
                                {
                                    //tr.Abort();
                                    MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    data.Close();
                                    reader.Close();
                                }
                            }
                    }
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                }
            }
        }
        private void sms2()
        {
            if (cmb_class.Text.Trim() == "")
            {
                MessageBox.Show("Select Class !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_rollno.Focus();
            }

            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select Emp_Name,EMP_CODE,dept_desc,desg_desc,EMP_Mobile from View_tblemployee where EMP_ID = '" + cmb_rollno.SelectedValue + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["EMP_Mobile"].ToString() != "" || dt.Rows[i]["EMP_Mobile"] != null)
                        {
                            // process p = new process();
                            //Thread tr = new Thread(new ThreadStart(p.processShow));
                            //tr.Start();
                            try
                            {
                                string msgtxt = "Dear  " + dt.Rows[i]["Emp_Name"].ToString() + ", Emp code  " + dt.Rows[i]["Emp_Code"].ToString() + ", " + dt.Rows[i]["desg_desc"].ToString() + " of " + dt.Rows[i]["dept_desc"].ToString() + " is absent Today. * Thanks";

                                da = new SqlDataAdapter("select * from  TBLSMS", sc);
                                DataTable dtsms = new DataTable();
                                da.Fill(dtsms);
                                if (dtsms.Rows.Count > 0)
                                {
                                    string msgid = dtsms.Rows[0]["userid"].ToString().Trim();
                                    string msgname = dtsms.Rows[0]["Name"].ToString().Trim();
                                    string msgpwd = dtsms.Rows[0]["password"].ToString().Trim();
                                    //string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=msgname&password=msgpwd&type=0&dlr=1&destination=" + dt.Rows[i]["Phone"].ToString() + "&source=msgid&message=" + msgtxt + "";
                                    string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=" + msgid + "&password=" + msgpwd + "&type=0&dlr=1&destination=" + dt.Rows[i]["EMP_Mobile"].ToString().Trim() + "&source=" + msgname + "&message=" + msgtxt + "";

                                    data = client.OpenRead(baseurl);
                                    reader = new StreamReader(data);
                                    string s = reader.ReadToEnd();
                                    //tr.Abort();
                                }


                                //MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                            }

                            catch (Exception ex)
                            {
                                //tr.Abort();
                                MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                data.Close();
                                reader.Close();
                            }
                        }
                    }
                    MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                }
                else
                    MessageBox.Show("There is No PhoneNo for  sending  SMS. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);


            }
        }
        private void cmbmsgtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindmsg(); txtmsg1.Text = "";
        }

        private void Bindmsg()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select msg from tblmsgtype where ID like '" + cmbmsgtype.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtmsg1.Text = dt.Rows[0]["msg"].ToString();
            }

        }

        private void chkpartpayment_CheckStateChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtcardNo_TextChanged(object sender, EventArgs e)
        {
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();
            //SqlDataAdapter da = new SqlDataAdapter("select Emp_Name,EMP_id,dept_id,EMP_Mobile from View_tblemployee where Emp_Zip='" + txtcardNo.Text + "'", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    cmb_class.SelectedValue = dt.Rows[0]["dept_id"].ToString();
            //    cmb_rollno.SelectedValue = dt.Rows[0]["EMP_id"].ToString();
            //    txtStuname.Text = dt.Rows[0]["Emp_Name"].ToString();
            //    INSERTEMP();
            //}
            //txtcardNo.Text = "";
        }

        private void txtcardNo_Leave(object sender, EventArgs e)
        {
            if (txtcardNo.Text.Trim() != "")
            {
                //string abc = (txtcardNo.Text).Substring(2, 16);
                string abc = (txtcardNo.Text);
                txtcardNo.Text = abc;
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                SqlDataAdapter da = new SqlDataAdapter("select Emp_Name,EMP_id,dept_id,EMP_Mobile from View_tblemployee where EMP_ZIP ='" + abc + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmb_class.SelectedValue = dt.Rows[0]["dept_id"].ToString();
                    cmb_rollno.SelectedValue = dt.Rows[0]["EMP_id"].ToString();
                    txtStuname.Text = dt.Rows[0]["Emp_Name"].ToString();
                    INSERTEMP();
                }
                txtcardNo.Text = "";
            }
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            bindrollno();
            LoadGrid();
        }

        

        
        

     }
}
