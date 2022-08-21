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
    public partial class FrmStudentsmsnamewise : Form
    {
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;
        public FrmStudentsmsnamewise()
        {
            InitializeComponent();
        }

        void LoadGrid()
        {
            

        }
        private void bindClass()
        {
            
        }
        private void bindrollno()
        {

        }


        private void LoadGrid2()
        {
           
        }
        private void Bindname()
        {
           

        }
        void Cancel_Form()
        {
            txtStuname.Text = "";

        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
  

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

             dataclass dd = new dataclass();
             SqlConnection sc = dd.con();
             SqlCommand com = new SqlCommand("TRUNCATE TABLE Tmp_sms", sc);
             SqlDataReader dr;
             sc.Open();
             try
                 {
                      dr = com.ExecuteReader();
                         if (dr.RecordsAffected > 0)
                            {
                               
                            }
                            else
                            {
                            }

                   }
             catch (Exception ex)
             { }
             finally
             {
               sc.Close();
             }
             listBox1.Visible = false;                          
            
        }
        private void msgtype()
        {
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            sms1();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                    BINDGRID();
                    for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
                    {
                        if ((dataGridView1.Rows[z].Cells["Phone"].Value.ToString() != "" || dataGridView1.Rows[z].Cells["Phone"].Value.ToString() != null) && dataGridView1.Rows[z].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        {

                            string msg = "|";
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            
                                msg = SMS.SMS(textBox2.Text.Trim(), 0, dataGridView1.Rows[z].Cells["Phone"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));
                           
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
        
        
     
        private void cmbmsgtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindmsg(); 
        }

        private void Bindmsg()
        {
            
        }

        private void chkpartpayment_CheckStateChanged(object sender, EventArgs e)
        {
            BINDGRID();
            
        }
        private void BINDGRID()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select distinct cast('" + chkpartpayment.Checked + "' as bit) as Selected ,stu_id,admno,student_name,class,ROLL,STC_SEC_ID,phone,com_name,companytelno from View_tmpsms ", sc);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            dataGridView1.Columns["Selected"].Visible = true;
            dataGridView1.Columns["stu_id"].Visible = false;
            dataGridView1.Columns["STC_SEC_ID"].Visible = false;
            dataGridView1.Columns["com_name"].Visible = false;
            dataGridView1.Columns["companytelno"].Visible = false;
            dataGridView1.Columns["admno"].HeaderText = "ADMNO";
            dataGridView1.Columns["admno"].Width = 180;
            dataGridView1.Columns["student_name"].HeaderText = "STUDENT_NAME";
            dataGridView1.Columns["student_name"].Width = 180;
            dataGridView1.Columns["class"].HeaderText = "CLASS";
            dataGridView1.Columns["class"].Width = 180;
            dataGridView1.Columns["ROLL"].HeaderText = "ROLLNUMBER";
            dataGridView1.Columns["ROLL"].Width = 180;
            dataGridView1.Columns["phone"].HeaderText = "MobileNo";
            dataGridView1.Columns["phone"].Width = 180;    
        }

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            BINDGRID();  

        }

        private void txtstuno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStuname_TextChanged(object sender, EventArgs e)
        {
             {

                string Name;

                if (txtStuname.Text == "-Select-" || txtStuname.Text == "" || txtStuname.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtStuname.Text.Trim().ToString();
                }
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '/' + ds1.Tables[0].Rows[j][1].ToString() + '/' + ds1.Tables[0].Rows[j][2].ToString() + '/' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = txtstuno.Top + txtstuno.Height;                    
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }

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
            //crytotaldailyatt.dt1 = todaydate.Value;
            //crytotaldailyatt obj = new crytotaldailyatt();
            //obj.ShowDialog();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    txtstuno.Text = str[0].ToString();
                    txtStuname.Text = "";
                    listBox1.Visible = false;
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    SqlCommand com = new SqlCommand("insert_tmp_sms '" + txtstuno.Text + "','" + sms.Form1.GlobalLoginUserName.ToString() + "'", sc);
                    SqlDataReader dr;
                     sc.Open();
                        try
                        {
                            dr = com.ExecuteReader();
                            if (dr.RecordsAffected > 0)
                            {
                                BINDGRID();
                               
                            }
                            else
                            {
                            }

                        }
                        catch (Exception ex)
                        { }
                        finally
                        {
                            sc.Close();
                        }
                    }                



                
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           
            if (dataGridView1.Rows.Count > 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["STU_ID"].Value.ToString();
                    try
                    {
                        dataclass dc = new dataclass();
                        SqlConnection conn = dc.con();
                        if (textBox1.Text == "")
                        {
                            MsgBox.ShowBox("Please Click to Detail Row!!!");
                            return;
                        }
                        SqlCommand objCmd = new SqlCommand("DELETE FROM tmp_sms WHERE STU_ID=" + textBox1.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                           BINDGRID();
                        }
                        else
                        {
                            MsgBox.ShowBox("Deleting Error!!!");
                        }
                    }
                    catch (Exception ex)
                    { }
                }
               
            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string str;

            str = textBox2.Text.ToString();
            label19.Text = " Character : " + str.Length;
            label19.Visible = true;
        }
        }


    }
