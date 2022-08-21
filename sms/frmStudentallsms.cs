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
    public partial class frmStudentallsms : Form
    {
        DataTable StudentFee = new DataTable();
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader;  
        CheckBox chkbox = new CheckBox();
        DataTable dt;

        String stcsecid="0";
        int cont;
        public frmStudentallsms()
        {
            InitializeComponent();
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

            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "-Select-";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
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
                txtmsg.Text = dt.Rows[0]["msg"].ToString();
            }

        }
        void Cancel_Form()
        {
            cmb_class.Text = "-Select-";
            StudentFee.Rows.Clear();
            cmb_rollno.Text = "";
          

        }

        private void sms1()
        {

           if (txtmsg.Text.Trim() == "")
            {
                MessageBox.Show("Fill Message Body !!!", "Warning..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmsg.Focus(); return;

            }

            else
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select Student_Name,Class,Roll,Phone from View_StudentReport where STU_ID like '" + cmb_rollno.SelectedValue + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                try
                {
                    for (int z = 0; z < dataGridView1.Rows.Count - 1; z++)
                    {
                        if ((dataGridView1.Rows[z].Cells["Mobile"].Value.ToString() != "" || dataGridView1.Rows[z].Cells["Mobile"].Value.ToString() != null) && dataGridView1.Rows[z].Cells["Selected"].Value.ToString().Trim().Replace(" ", "") == "True")
                        {

                            string msg = "|";
                            DAL.smsclass SMS = new sms.DAL.smsclass();
                            if (radiomale.Checked == true)
                                msg = SMS.SMS(txtmsg.Text.Trim(), 0, dataGridView1.Rows[z].Cells["Mobile"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["StudentName"].Value.ToString().Trim().Replace(" ", ""));
                            else
                                msg = SMS.SMS(txtmsg.Text.Trim(), 1, dataGridView1.Rows[z].Cells["Mobile"].Value.ToString().Trim().Replace(" ", ""), dataGridView1.Rows[z].Cells["StudentName"].Value.ToString().Trim().Replace(" ", ""));
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
            
        
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }





      
       
        
       


       

        private void cmb_class_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void InitGrid()
        {
            LoadGrid();
           // dataGridView1.Columns["STU_ID"].Visible = false;
            //dataGridView1.Columns["STU_ADM_NO"].Width = 100;
            //dataGridView1.Columns["STU_ADM_NO"].HeaderText = "ADM. NO.";
            //dataGridView1.Columns["StudentName"].Width = 200;
            //dataGridView1.Columns["StudentName"].HeaderText = "StudentName";
            //dataGridView1.Columns["STC_ROLL_NO"].Width = 100;
            //dataGridView1.Columns["STC_ROLL_NO"].HeaderText = "Strength";
            //dataGridView1.Columns["STU_Mobile"].Width = 140;
            //dataGridView1.Columns["STU_Mobile"].HeaderText = "Mobile";

        }

        private void LoadGrid()
        {
            DataTable ds = new DataTable();
            string str;
            if (cmb_class.Text == "" || cmb_class.Text == "All" || cmb_class.Text == "System.Data.DataRowView")
                str = "%";
            else
                str = cmb_class.SelectedValue.ToString();


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            da = new SqlDataAdapter("select distinct cast('" + chkpartpayment.Checked + "' as bit) as Selected,sr.STU_ID as STU_ID,sr.STU_ADM_NO as Admission_NO ,(sr.STU_FIRST_NAME+sr.STU_MID_NAME +sr.STU_LAST_NAME) as StudentName,ts.STC_ROLL_NO as ROLL_NO,sr.STU_Mobile as Mobile from  STUDENT_T sr,STUDENT_CLASS_T ts where sr.STU_ID=ts.STC_STU_ID and ts.STC_SEC_ID like '" + str + "' and STU_STATUS='Y' AND SR.STU_MSG_ALLOWED=1 order by ts.STC_ROLL_NO", sc);
            //sr.STU_CLS_ID
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                dataGridView1.DataSource = ds;
                label5.Text = "Total No. Of Student: " + dataGridView1.Rows.Count.ToString();
                label5.Visible = true;
                // AddCheckBoxColumn();
            }


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
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "All";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";

           
        }

        private void bindselectclass()
        {
            DataTable ds = new DataTable();
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            da = new SqlDataAdapter("select  st.sec_id, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID", sc);
            //sr.STU_CLS_ID
            da.Fill(ds);
            cont = ds.Rows.Count;
            if (ds.Rows.Count > 0)
            {

                dataGridView2.DataSource = ds;
                dataGridView2.Columns["sec_id"].Visible = false;
               

            }


            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "Select";
            dataGridView2.Columns.Insert(0, checkBoxColumn);



        }
        private void frmStudentindividualsms_Load(object sender, EventArgs e)
        {
            bindClass();
            Bindmsg();
        }

        private void cmbmsgtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtmsg.Text = "";
            Bindmsg();

        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            sms1();
        }

        private void frmStudentindividualsms_Load_1(object sender, EventArgs e)
        {
            bindClass();
            msgtype();
            bindselectclass();
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void chkpartpayment_CheckStateChanged(object sender, EventArgs e)
        {
            LoadGrid();

        }

       

        private void txtmsg_TextChanged_1(object sender, EventArgs e)
        {
            string str;

            str = txtmsg.Text.ToString();
            label9.Text = " Character : " + str.Length;
            label9.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkcls_CheckedChanged(object sender, EventArgs e)
        {
            bindselectclass();
        }

        private void dataGridView2_Leave(object sender, EventArgs e)
        {
            //int i;
            //int[] arr = new int[];
            //for (i = 0;)
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnload_Click(object sender, EventArgs e)
        {
            stcsecid = "0";
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                int stuid1 = Convert.ToInt32(row.Cells["sec_id"].Value);
                if (isSelected)
                {
                    stcsecid = stcsecid + "," + stuid1; 
                }
            }


            DataTable ds = new DataTable();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            da = new SqlDataAdapter("select distinct cast('" + chkpartpayment.Checked + "' as bit) as Selected,sr.STU_ID as STU_ID,sr.STU_ADM_NO as Admission_NO ,(sr.STU_FIRST_NAME+sr.STU_MID_NAME +sr.STU_LAST_NAME) as StudentName,ts.STC_ROLL_NO as ROLL_NO,sr.STU_Mobile as Mobile from  STUDENT_T sr,STUDENT_CLASS_T ts where sr.STU_ID=ts.STC_STU_ID and ts.STC_SEC_ID in  ( " + stcsecid + ") and STU_STATUS='Y' AND SR.STU_MSG_ALLOWED=1 order by ts.STC_ROLL_NO", sc);
            //sr.STU_CLS_ID
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                dataGridView1.DataSource = ds;
                label5.Text = "Total No. Of Student: " + dataGridView1.Rows.Count.ToString();
                label5.Visible = true;
                // AddCheckBoxColumn();
            }

         
        }

      /*  private void akky()
        {

            DataTable ds = new DataTable();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da;

            da = new SqlDataAdapter("select distinct cast('" + chkpartpayment.Checked + "' as bit) as Selected,sr.STU_ID as STU_ID,sr.STU_ADM_NO as Admission_NO ,(sr.STU_FIRST_NAME+sr.STU_MID_NAME +sr.STU_LAST_NAME) as StudentName,ts.STC_ROLL_NO as ROLL_NO,sr.STU_Mobile as Mobile from  STUDENT_T sr,STUDENT_CLASS_T ts where sr.STU_ID=ts.STC_STU_ID and ts.STC_SEC_ID in  ( '" + dataGridView2.Rows[z].Cells["sec_id"].Value + "') and STU_STATUS='Y' AND SR.STU_MSG_ALLOWED=1 order by ts.STC_ROLL_NO", sc);
            //sr.STU_CLS_ID
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                dataGridView1.DataSource = ds;
                label5.Text = "Total No. Of Student: " + dataGridView1.Rows.Count.ToString();
                label5.Visible = true;
                // AddCheckBoxColumn();
            }
        }*/
        

       
       

       

     }
}