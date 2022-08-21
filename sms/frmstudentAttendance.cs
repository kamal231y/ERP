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
    public partial class frmstudentAttendance : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;
        public frmstudentAttendance()
        {
            InitializeComponent();
        }

        void LoadGrid()
        {
            DateTime dt1;
            dt1 = todaydate.Value;
            DataSet dt = new DataSet();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "-Select-" || cmb_class.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmb_class.SelectedValue.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("BIND_STUDENT_Attendance", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(str));
            da.SelectCommand.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToString("MM/dd/yyyy"));
            da.SelectCommand.Parameters.AddWithValue("@SELECTED", chkpartpayment.Checked);

            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            dataGridView1.Columns["Selected"].Width = 70;
            dataGridView1.Columns["Selected"].HeaderText = "Selected";
            dataGridView1.Columns["Admno"].Width = 70;
            dataGridView1.Columns["Admno"].HeaderText = "Adm.No.";
            dataGridView1.Columns["Roll"].Width = 50;
            dataGridView1.Columns["Roll"].HeaderText = "Roll";
            dataGridView1.Columns["Student_Name"].Width = 170;
            dataGridView1.Columns["Student_Name"].HeaderText = "StudentName";
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
            dataGridView1.Columns["D31"].Width = 30; ;
            dataGridView1.Columns["D31"].HeaderText = "31";

            dataGridView2.DataSource = dt.Tables[1];
            dataGridView2.Columns["P"].Width = 50;
            dataGridView2.Columns["P"].HeaderText = "P";
            dataGridView2.Columns["A"].Width = 50;
            dataGridView2.Columns["A"].HeaderText = "A";
            dataGridView2.Columns["L"].Width = 50;
            dataGridView2.Columns["L"].HeaderText = "L";
            dataGridView2.Columns["OT"].Width = 50;
            dataGridView2.Columns["OT"].HeaderText = "OT";
            dataGridView1.Columns["ID"].Visible = false;

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
            SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where STC_SEC_ID =  '" + StateName + "' and stu_status='Y' order by  ROLL", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["ROLL"] = "0";
            row["STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "ROLL";
            cmb_rollno.ValueMember = "STU_ID";
        }



        private void LoadGrid2()
        {
            DateTime dt1;
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
            SqlDataAdapter da = new SqlDataAdapter("select  * from student_T where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtStuname.Text = dt.Rows[0]["STU_FIRST_NAME"].ToString() + dt.Rows[0]["STU_MID_NAME"].ToString() + dt.Rows[0]["STU_LAST_NAME"].ToString();
                txtstuno.Text = dt.Rows[0]["stu_adm_no"].ToString();
            }

        }
        void Cancel_Form()
        {
            cmb_class.Text = "-Select-";
            cmb_rollno.Text = "";
            txtStuname.Text = "";
            rdabsent.Checked = true;

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
                    for (int z = 0; z < dataGridView1.Rows.Count; z++)
                    {
                        string str11111 = dataGridView1.Rows[z].Cells["Selected"].Value.ToString();
                        if (dataGridView1.Rows[z].Cells["Selected"].Value.ToString() == "True")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("insert_STUDENT_Attendance ", sc);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["ID"].Value.ToString()));
                            da.SelectCommand.Parameters.AddWithValue("@STS_SUB_ID", dt1);
                            da.SelectCommand.Parameters.AddWithValue("@type", att);
                            da.SelectCommand.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                            da.SelectCommand.Parameters.AddWithValue("@USERID", sms.Form1.GlobalLoginUserName.ToString());
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                    //SqlCommand cmd1 = new SqlCommand("insert_STUDENT_Attendance", sc);
                    //cmd1.CommandType = CommandType.StoredProcedure;
                    //cmd1.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(cmb_rollno.SelectedValue));
                    //cmd1.Parameters.AddWithValue("@STS_SUB_ID", dt1);
                    //cmd1.Parameters.AddWithValue("@type", att);
                    //cmd1.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                    //cmd1.Parameters.AddWithValue("@USERID", sms.Form1.GlobalLoginUserName.ToString());
                    ////cmd1.Parameters.AddWithValue("@SESSION", sms.Form1.GlobalFAYear.ToString());

                   // cmd1.ExecuteNonQuery();

                    if (rdabsent.Checked == true)
                    {
                        if (Form1.SMS)
                            sms2();

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


        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {


            txtStuname.Text = "";
            Bindname();
        }

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindrollno();
            LoadGrid();


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
                            SqlDataAdapter da = new SqlDataAdapter("insert_STUDENT_Attendance ", sc);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["ID"].Value.ToString()));
                            da.SelectCommand.Parameters.AddWithValue("@STS_SUB_ID", dt1);
                            da.SelectCommand.Parameters.AddWithValue("@type", att);
                            da.SelectCommand.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                            da.SelectCommand.Parameters.AddWithValue("@USERID", sms.Form1.GlobalLoginUserName.ToString());
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                        }

                        else if (dataGridView1.Rows[z].Cells["Selected"].Value.ToString() != "True")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("insert_STUDENT_Attendance ", sc);
                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                            da.SelectCommand.Parameters.AddWithValue("@STS_STU_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["ID"].Value.ToString()));
                            da.SelectCommand.Parameters.AddWithValue("@STS_SUB_ID", dt1);
                            da.SelectCommand.Parameters.AddWithValue("@type", 'P');
                            da.SelectCommand.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                            da.SelectCommand.Parameters.AddWithValue("@USERID", sms.Form1.GlobalLoginUserName.ToString());
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                        }
                    }

                    ///////////////////////////////////////////////////////////old code classwise////////////
                    //SqlCommand cmd1 = new SqlCommand("insert_Class_Attendance",sc);
                    //cmd1.CommandType = CommandType.StoredProcedure;
                    //cmd1.Parameters.AddWithValue("@STS_SEC", Convert.ToDecimal(cmb_class.SelectedValue));
                    //cmd1.Parameters.AddWithValue("@STS_SUB_ID", dt1.ToShortDateString());
                    //cmd1.Parameters.AddWithValue("@type", att);
                    //cmd1.ExecuteNonQuery();
                    //////////////////////////////////////////////////////////////////////////////////////// 


                    if (rdabsent.Checked == true)
                    {
                        DialogResult dialogResult = MessageBox.Show("Do You Want to send sms !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (Form1.SMS)
                                sms1();
                        }
                        else
                        {
                        }
                    }
                    //if (rdpresent.Checked == true)
                    //{
                    //    if (Form1.SMS)
                    //        sms3();
                    //}
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
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                    if (dataGridView1.Rows[i].Cells["MobileNo"].ToString() != "" || dataGridView1.Rows[i].Cells["MobileNo"].Value != null)
                    {

                        try
                        {
                            string msgtxt = "Dear Parents, Your Ward " + "  " + dataGridView1.Rows[i].Cells["Student_Name"].Value.ToString() + ", " + dataGridView1.Rows[i].Cells["Class"].Value.ToString() + " , Roll No." + dataGridView1.Rows[i].Cells["Roll"].Value.ToString() + " is absent Today. * Thanks";
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            SMS.SMS(msgtxt, 0, dataGridView1.Rows[i].Cells["MobileNo"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[i].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));

                        }


                        catch (Exception ex)
                        {
                            //tr.Abort();
                            MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            //data.Close();
                            //  reader.Close();
                        }
                    }
            }
            MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        private void sms2()
        {
            //dataclass dc1 = new dataclass();
            //SqlConnection sc1 = dc1.connsms();
            //SqlDataAdapter da1 = new SqlDataAdapter("select * from  TBLSMS where userid='" + Form1.SMSKEY + "' and balance>0", sc1);
            //if (sc1 != null && sc1.State == ConnectionState.Closed)
            //{
            //    MsgBox.ShowBox("Please check your Internet Connection For sending the sms ");
            //    return;
            //}
            if (cmb_class.Text.Trim() == "")
            {
                MessageBox.Show("Select Class !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_rollno.Focus();
            }

            else
            {

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        if (dataGridView1.Rows[i].Cells["MobileNo"].ToString() != "" || dataGridView1.Rows[i].Cells["MobileNo"].Value != null)
                        {
                            try
                            {
                                string msgtxt = "Dear Parents, Your Ward " + "  " + dataGridView1.Rows[i].Cells["Student_Name"].Value.ToString() + "," + dataGridView1.Rows[i].Cells["Class"].Value.ToString() + " , Roll No." + dataGridView1.Rows[i].Cells["Roll"].Value.ToString() + " is absent Today. * Thanks";
                                DAL.smsclass SMS = new sms.DAL.smsclass();
                                SMS.SMS(msgtxt, 0, dataGridView1.Rows[i].Cells["MobileNo"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[i].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));



                            }

                            catch (Exception ex)
                            {
                                //tr.Abort();
                                MessageBox.Show("Sending Error.. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {

                            }
                        }
                }


                MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);


            }

        }

        private void sms3()
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

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        if (dataGridView1.Rows[i].Cells["MobileNo"].ToString() != "" || dataGridView1.Rows[i].Cells["MobileNo"].Value != null)
                        {
                            // process p = new process();
                            //Thread tr = new Thread(new ThreadStart(p.processShow));
                            //tr.Start();
                            try
                            {
                                string msgtxt = "Dear Parents, Your Ward " + "  " + dataGridView1.Rows[i].Cells["Student_Name"].Value.ToString() + ", " + dataGridView1.Rows[i].Cells["Class"].Value.ToString() + " , Roll No." + dataGridView1.Rows[i].Cells["Roll"].Value.ToString() + " is present Today. * Thanks";

                                DAL.smsclass SMS = new sms.DAL.smsclass();
                                SMS.SMS(msgtxt, 0, dataGridView1.Rows[i].Cells["MobileNo"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[i].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));


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

                        else
                            MessageBox.Show("SMS sent successfully. !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.None);

                }
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

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where  AdmNo like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
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
                // BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
            else
            {
                cmb_class.SelectedValue = 0;
                cmb_rollno.SelectedValue = 0;
                //Stu_id = "0";
                txtStuname.Text = "";
                //BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }

        }

        private void txtstuno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStuname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            crytotaldailyatt.dt1 = todaydate.Value;
            crytotaldailyatt obj = new crytotaldailyatt();
            obj.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dtdob = Convert.ToDateTime(todaydate.Value, System.Globalization.CultureInfo.InvariantCulture);

            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    //if (button2.Text == "")
                    //{
                    //    MsgBox.ShowBox("Please Click to Detail Row!!!");
                    //    return;
                    //}
                    SqlCommand objCmd = new SqlCommand("DELETE from STUDENT_ATTENDANCE_T where cast (sat_date as date )= '" + dtdob.ToString("MM/dd/yyyy") + "'", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Attendence Deleted Successfully!!!");
                        //Cancil_form();
                        LoadGrid();
                    }
                    //else
                    //{
                    //    MsgBox.ShowBox(" Section Already Used!!!");
                    //}
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

    }

}
