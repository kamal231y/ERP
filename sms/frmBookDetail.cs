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
    public partial class frmbookdetail : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;
        public frmbookdetail()
        {
            InitializeComponent();
        }

        void LoadGrid()
        {
            DateTime dt1;
            dt1 = todaydate.Value;
            DataTable dt = new DataTable();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select Roll,Student_Name,d1,d2,d3,d4,d5,d6,d7,d8,d9,d10,d11,d12,d13,d14,d15,d16,d17,d18,d19,d20,d21,d22,d23,d24,d25,d26,d27,d28,d29,d30,d31 from Attendance as atten,View_studentReport as Emp where atten.ID = Emp.STU_ID and Emp.STC_SEC_ID='" + str + "' and atten.Month1='" + dt1.Month + "' and atten.Year1='" + dt1.Year + "' order by emp.Roll ", sc);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns["Roll"].Width = 60;
            dataGridView2.Columns["Roll"].HeaderText = "Roll";
            dataGridView2.Columns["Student_Name"].Width = 200;
            dataGridView2.Columns["Student_Name"].HeaderText = "StudentName";
            dataGridView2.Columns["D1"].Width = 30;
            dataGridView2.Columns["D1"].HeaderText = "1";
            dataGridView2.Columns["D2"].Width = 30;
            dataGridView2.Columns["D2"].HeaderText = "2";
            dataGridView2.Columns["D3"].Width = 30;
            dataGridView2.Columns["D3"].HeaderText = "3";
            dataGridView2.Columns["D4"].Width = 30;
            dataGridView2.Columns["D4"].HeaderText = "4";
            dataGridView2.Columns["D5"].Width = 30;
            dataGridView2.Columns["D5"].HeaderText = "5";
            dataGridView2.Columns["D6"].Width = 30;
            dataGridView2.Columns["D6"].HeaderText = "6";
            dataGridView2.Columns["D7"].Width = 30;
            dataGridView2.Columns["D7"].HeaderText = "7";
            dataGridView2.Columns["D8"].Width = 30;
            dataGridView2.Columns["D8"].HeaderText = "8";
            dataGridView2.Columns["D9"].Width = 30;
            dataGridView2.Columns["D9"].HeaderText = "9";
            dataGridView2.Columns["D10"].Width = 30;
            dataGridView2.Columns["D10"].HeaderText = "10";
            dataGridView2.Columns["D11"].Width = 30;
            dataGridView2.Columns["D11"].HeaderText = "11";
            dataGridView2.Columns["D12"].Width = 30;
            dataGridView2.Columns["D12"].HeaderText = "12";
            dataGridView2.Columns["D13"].Width = 30;
            dataGridView2.Columns["D13"].HeaderText = "13";
            dataGridView2.Columns["D14"].Width = 30;
            dataGridView2.Columns["D14"].HeaderText = "14";
            dataGridView2.Columns["D15"].Width = 30;
            dataGridView2.Columns["D15"].HeaderText = "15";
            dataGridView2.Columns["D16"].Width = 30;
            dataGridView2.Columns["D16"].HeaderText = "16";
            dataGridView2.Columns["D17"].Width = 30;
            dataGridView2.Columns["D17"].HeaderText = "17";
            dataGridView2.Columns["D18"].Width = 30;
            dataGridView2.Columns["D18"].HeaderText = "18";
            dataGridView2.Columns["D19"].Width = 30;
            dataGridView2.Columns["D19"].HeaderText = "19";
            dataGridView2.Columns["D20"].Width = 30;
            dataGridView2.Columns["D20"].HeaderText = "20";
            dataGridView2.Columns["D21"].Width = 30;
            dataGridView2.Columns["D21"].HeaderText = "21";
            dataGridView2.Columns["D22"].Width = 30;
            dataGridView2.Columns["D22"].HeaderText = "22";
            dataGridView2.Columns["D23"].Width = 30;
            dataGridView2.Columns["D23"].HeaderText = "23";
            dataGridView2.Columns["D24"].Width = 30;
            dataGridView2.Columns["D24"].HeaderText = "24";
            dataGridView2.Columns["D25"].Width = 30;
            dataGridView2.Columns["D25"].HeaderText = "25";
            dataGridView2.Columns["D26"].Width = 30;
            dataGridView2.Columns["D26"].HeaderText = "26";
            dataGridView2.Columns["D27"].Width = 30;
            dataGridView2.Columns["D27"].HeaderText = "27";
            dataGridView2.Columns["D28"].Width = 30;
            dataGridView2.Columns["D28"].HeaderText = "28";
            dataGridView2.Columns["D29"].Width = 30;
            dataGridView2.Columns["D29"].HeaderText = "29";
            dataGridView2.Columns["D30"].Width = 30;
            dataGridView2.Columns["D30"].HeaderText = "30;";
            dataGridView2.Columns["D31"].Width = 30;;
            dataGridView2.Columns["D31"].HeaderText = "31";
          
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
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

            DataRow row = dt.NewRow();
            row["STC_ROLL_NO"] = "0";
            row["STC_STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
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
            SqlDataAdapter da = new SqlDataAdapter("select Distinct * from student_T where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
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
            DateTime dt1;
            dt1 = todaydate.Value;
            string att;
            if (rdpresent.Checked == true)
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
            else if (cmb_rollno.Text == "-Select-" || cmb_rollno.Text == "")
            {
                MsgBox.ShowBox("Please Select RollNo !!!!!");
                return;
            }
           
            else
            {
                sc.Open();
              
                try
                {
                    SqlCommand cmd1 = new SqlCommand("insert_STUDENT_Attendance",sc);
                       cmd1.CommandType = CommandType.StoredProcedure;
                       cmd1.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(cmb_rollno.SelectedValue));
                       cmd1.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToShortDateString());
                       cmd1.Parameters.AddWithValue("@type",att );
                       cmd1.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));

                       cmd1.ExecuteNonQuery();
                     LoadGrid();
                    LoadGrid2();
                    if (rdabsent.Checked == true)
                    {
                        sms1();
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
            LoadGrid();
            LoadGrid2();
            
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

                    SqlCommand cmd1 = new SqlCommand("insert_Class_Attendance",sc);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                    cmd1.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToShortDateString());
                    cmd1.Parameters.AddWithValue("@type", att);
                    cmd1.ExecuteNonQuery();
                     
                    LoadGrid();
                    LoadGrid2();
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
            else if (txtmsg1.Text.Trim() == "")
            {
                //MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txtmsg.Focus();
            }
            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select st.STU_mobile from student_T st,STUDENT_CLASS_T sct where st.STU_ID = stc.STC_STU_ID and  st.STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["STU_mobile"].ToString() != "" || dt.Rows[i]["STU_mobile"] != null)
                        {
                            // process p = new process();
                            //Thread tr = new Thread(new ThreadStart(p.processShow));
                            //tr.Start();
                            try
                            {

                                string msgtxt = txtmsg.Text ;
                                string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=cra-santosh13&password=98765&type=0&dlr=1&destination=" + dt.Rows[i]["STU_mobile"].ToString() + "&source=NVSPSH&message=" + msgtxt + "";
                                data = client.OpenRead(baseurl);
                                reader = new StreamReader(data);
                                string s = reader.ReadToEnd();
                                //tr.Abort();


                                MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

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
        private void sms2()
        {
            if (cmb_class.Text.Trim() == "")
            {
                MessageBox.Show("Select Class !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_rollno.Focus();
            }
            else if (txtmsg1.Text.Trim() == "")
            {
                //MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //txtmsg.Focus();
            }
            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select Student_Name,Class,Roll,Phone from View_StudentReport where STC_SEC_ID like '" + cmb_class.SelectedValue + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["STU_mobile"].ToString() != "" || dt.Rows[i]["STU_mobile"] != null)
                        {
                            // process p = new process();
                            //Thread tr = new Thread(new ThreadStart(p.processShow));
                            //tr.Start();
                            try
                            {
                                string msgtxt = "Dear Parents, Your Ward " + "  " + dt.Rows[i]["Student_Name"].ToString() + ", " + dt.Rows[i]["Class"].ToString() + " , Roll No." + dt.Rows[i]["Roll"].ToString() + " is absent Today. * Thanks";

                               // string msgtxt = txtmsg.Text + " From School Demo.";
                                string baseurl = "http://sms.crossatlantic.net:8080/bulksms/bulksms?username=cra-santosh13&password=98765&type=0&dlr=1&destination=" + dt.Rows[i]["Phone"].ToString() + "&source=NVSPSH&message=" + msgtxt + "";
                                data = client.OpenRead(baseurl);
                                reader = new StreamReader(data);
                                string s = reader.ReadToEnd();
                                //tr.Abort();


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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

     }
}