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
using MsgBoxRet;
using sms.BLClass;



namespace sms
{
    public partial class FrmStudySchedule: Form
    {
        string Stu_id;
        string ROLL;
        public FrmStudySchedule()
        {
            InitializeComponent();
            Cancil_form();
            InitGrid();
            bindclass();
        }
        private void Cancil_form()
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            cmb_class.Text = "";
            cmbSub1.Text = "";
            cmbSub2.Text = "";
            cmbSub3.Text = "";
            cmbSub4.Text = "";
            cmbSub5.Text = "";
            cmbSub6.Text = "";
            cmbsub7.Text = "";
            cmbSub8.Text = "";
            

  
        }
        private void bindSub1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=1 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub1.DataSource = ds.Tables[0];
            cmbSub1.DisplayMember = "SUB_DESC";
            cmbSub1.ValueMember = "SUB_ID";
            cmbSub1.Enabled = false;
        }
        private void bindSub2()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=3 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub2.DataSource = ds.Tables[0];
            cmbSub2.DisplayMember = "SUB_DESC";
            cmbSub2.ValueMember = "SUB_ID";
            cmbSub2.Enabled = false;
        }
        private void bindSub3()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=4 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub3.DataSource = ds.Tables[0];
            cmbSub3.DisplayMember = "SUB_DESC";
            cmbSub3.ValueMember = "SUB_ID";
            cmbSub3.Enabled = false;
        }
        private void bindSub4()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=5 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub4.DataSource = ds.Tables[0];
            cmbSub4.DisplayMember = "SUB_DESC";
            cmbSub4.ValueMember = "SUB_ID";
            cmbSub4.Enabled = false;
        }
        private void bindSub5()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=2 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub5.DataSource = ds.Tables[0];
            cmbSub5.DisplayMember = "SUB_DESC";
            cmbSub5.ValueMember = "SUB_ID";
            cmbSub5.Enabled = false;
        }
        private void bindSub6()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T  where sub_id=6 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub6.DataSource = ds.Tables[0];
            cmbSub6.DisplayMember = "SUB_DESC";
            cmbSub6.ValueMember = "SUB_ID";
            cmbSub6.Enabled = false;
        }
        private void bindSub7()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=7 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsub7.DataSource = ds.Tables[0];
            cmbsub7.DisplayMember = "SUB_DESC";
            cmbsub7.ValueMember = "SUB_ID";
            cmbsub7.Enabled = false;
        }
        private void bindSub8()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T where sub_id=8 order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["SUB_DESC"] = "Select";
            //row["SUB_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub8.DataSource = ds.Tables[0];
            cmbSub8.DisplayMember = "SUB_DESC";
            cmbSub8.ValueMember = "SUB_ID";
            cmbSub8.Enabled = false;
        }


        public void InitGrid()
        {
            //LoadGrid();
            //dataGridView1.Columns["STU_ID"].Visible = false;
            //dataGridView1.Columns["STU_ADM_NO"].Width = 100;
            //dataGridView1.Columns["StudentName"].Width = 150;
            //dataGridView1.Columns["Class"].Width = 50;
            //dataGridView1.Columns["Roll_No"].Width = 100;
            //dataGridView1.Columns["STOP_DESC"].Width = 100;
            //dataGridView1.Columns["RT_DESC"].Width = 100;
            //dataGridView1.Columns["VEHICLE_NO"].Width = 170;          
            //dataGridView1.Columns["Stop_Fare"].Width = 60;
            //dataGridView1.Columns["STU_FATHER_NAME"].Width = 200;
            //dataGridView1.Columns["date"].Width = 80;
           // dataGridView1.Columns["terminalid"].Width = 100;
        }
        private void LoadGrid()
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select STU_ID,Stu_ADM_NO,(STU_FIRST_NAME+STU_MID_NAME+STU_LAST_NAME) as StudentName,(CLS_DESC+' '+SEC_DESC) as Class,STC_ROLL_NO as Roll_No,STOP_DESC,RT_DESC,VEHICLE_NO,Stop_Fare,STU_FATHER_NAME,SEC_ID  from View_tblstubusstop order by StudentName ", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + sec_desc) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["sec_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "sec_id";
        }
        
        private void Update_Click(object sender, EventArgs e)
        {
            System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtp11 = Convert.ToDateTime(dtpAppliedOn.Value, System.Globalization.CultureInfo.InvariantCulture);
            if (cmb_class.Text == "" || cmb_class.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Class Field!!!");
                return;
            }
            if (cmbSub1.SelectedValue.ToString() == "" || cmbSub1.SelectedValue.ToString() == "0")
            {
                MsgBox.ShowBox("plz. select subject 1");
                return;
            }
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
                        else
                        {
                            dataclass dd = new dataclass();
                            SqlConnection sc = dd.con();
                            string str = sc.DataSource.ToString();
                            SqlCommand com = new SqlCommand("insert study_schedule values('" + dtp11.ToString("MM/dd/yyyy") + "','"+cmb_class.SelectedValue+"','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "')", sc);
                            SqlDataReader dr;
                            sc.Open();

                            try
                            {
                                dr = com.ExecuteReader();
                                if (dr.RecordsAffected > 0)
                                {
                                    MsgBox.ShowBox("Record Created...");
                                    Cancil_form();
                                    LoadGrid();
                                }
                                else
                                {
                                    MsgBox.ShowBox("This Record Name Not exist");
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                sc.Close();
                            }

                        }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
       
        private void FrmStuBusstop_Load(object sender, EventArgs e)
        {
            bindSub1(); bindSub2(); bindSub3(); bindclass(); 
            bindSub4(); bindSub5(); bindSub6(); bindSub7(); bindSub8();
             
        }

        
        private void dataGridView1_Click(object sender, EventArgs e)
        {                    
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (cmb_class.Text == "" || cmb_class.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Class Field!!!");
                return;
            }
            if (cmbSub1.SelectedValue.ToString() == "" || cmbSub1.SelectedValue.ToString() == "0")
            {
                MsgBox.ShowBox("plz. select subject 1");
                return;
            }
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
            if (cmbSub6.SelectedValue.ToString() == "" || cmbSub6.SelectedValue.ToString() == "0")
            {
                MsgBox.ShowBox("plz. select subject 6");
                return;
            }
            if (cmbsub7.SelectedValue.ToString() == "" || cmbsub7.SelectedValue.ToString() == "0")
            {
                MsgBox.ShowBox("plz. select subject 7");
                return;
            }
            if (cmbSub8.SelectedValue.ToString() == "" || cmbSub8.SelectedValue.ToString() == "0")
            {
                MsgBox.ShowBox("plz. select subject 8");
                return;
            }
            else
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                string str = sc.DataSource.ToString();
                SqlCommand com = new SqlCommand("DELETE FROM  study_schedule WHERE ID='"+txtvehicle.Text+"' ", sc);
                SqlDataReader dr;
                sc.Open();

                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Record Deleted...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Record Name Not exist");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }

            }
        }     
        
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }
        private void cmbRoute_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtstuno_Enter(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            
            DateTime dtp11 = Convert.ToDateTime(dtpAppliedOn.Value, System.Globalization.CultureInfo.InvariantCulture);
            DataTable ds = new DataTable();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "All" || cmb_class.Text == "System.Data.DataRowView")
                str = "%";
            else
                str = cmb_class.SelectedValue.ToString();


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_study_schedule where date like '" + dtp11.ToString("yyyy-MM-dd") + "' AND CLS like '" + str + "'AND stu_status='y' ", sc);
            //sr.STU_CLS_ID
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                dataGridView2.DataSource = ds;
                // AddCheckBoxColumn();
            }
          }

            

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dtpAppliedOn.Value, System.Globalization.CultureInfo.InvariantCulture);

            if (dataGridView2.Rows.Count > 0)
            {
                sms1();
            }
             else
            {
                MsgBox.ShowBox("No Records Found to Send Message....");
            }
        }
        private void sms1()
        {

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (dataGridView2.Rows[i].Cells["phONE"].ToString() != "" || dataGridView2.Rows[i].Cells["phONE"].Value != null)
                               
                            {
                          
                            try
                            {
                                //string msgtxt = "Dear Parents, Your Ward's " + "  " + dataGridView1.Rows[i].Cells["StudentName"].Value.ToString() + ", " + dataGridView1.Rows[i].Cells["Class"].Value.ToString() + " , Roll No." + dataGridView1.Rows[i].Cells["Rollno"].Value.ToString() + "  Dues Balance Till Month " + cmbyear.Text.Trim() + " is " + dataGridView1.Rows[i].Cells["Balance_Dues"].Value.ToString().Trim()+ " * Thanks";
                                //string msgtxt = "Today Home Work is " + "  '" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox10.Text + "',  * Thanks";
                                string msgtxt = "Today Home Work is " + "  " + dataGridView2.Rows[i].Cells["SUB1"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB2"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB3"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB4"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB5"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB6"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB7"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["SUB8"].Value.ToString() + ", * Thanks";
                                DAL.smsclass SMS = new sms.DAL.smsclass();
                                SMS.SMS(msgtxt, 0, dataGridView2.Rows[i].Cells["phONE"].Value.ToString().Trim().Replace(" ", ""), dataGridView2.Rows[i].Cells["Student_Name"].Value.ToString().Trim().Replace(" ", ""));
                               
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

        }
        }

    
