using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using sms.BLClass;
using System.Data.SqlClient;
using MsgBoxRet;

namespace sms
{
    public partial class frmexamschedule: Form
    {
        public frmexamschedule()
        {
            InitializeComponent();
            Cancil_form();
            InitGrid();
        }

        private void Cancil_form()
        {
            btn_create.Enabled = true;
            txtName.Text = "";
            dtp1.Text = "";
            dtp2.Text = "";
            dtp2.Text = "";
            btnUpdate.Enabled = true;
            btn_delete.Enabled = false;
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["CLS_DESC"] = "Select";
            //row["CLS_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }
        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EXAMINATION_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EXAM_DESC"] = "Select";
            row["EXAM_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "EXAM_DESC";
            cmbExam.ValueMember = "EXAM_ID";
        }
        private void bindSubCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=0 and subsc_id in (48,78,79) order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSubjectCategory.DataSource = ds.Tables[0];
            cmbSubjectCategory.DisplayMember = "SUBSC_DESC";
            cmbSubjectCategory.ValueMember = "SUBSC_ID";
        }
        private void bindSubCategory1()
        {
            int id = 0;
            if (cmbSubjectCategory.SelectedValue != null)
                id = Convert.ToInt32(cmbSubjectCategory.SelectedValue.ToString());
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_SUB_CATEGORY_T WHERE SUBSC_CAT_ID=" + id + " order by SUBSC_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUBSC_DESC"] = "-Select-";
            row["SUBSC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            CMBSUB1.DataSource = ds.Tables[0];
            CMBSUB1.DisplayMember = "SUBSC_DESC";
            CMBSUB1.ValueMember = "SUBSC_ID";
        }


        public void InitGrid()
        {
            //LoadGrid();
           
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select sbm_id,(CLS_DESC + SEC_DESC) as Class,exam_desc,subsc_desc,dates,fromdate,todate from View_examschedule1  where SBM_CLS_ID='" + cmbClass.SelectedValue + "' AND EXAM_DESC='" + cmbExam.Text + "' ORDER BY SBM_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }



        private void frmAdmissinTestSchedule_Load(object sender, EventArgs e)
        {
            bindSubCategory1(); bindSubCategory(); bindExam(); bindClass();

        }

        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            //if (txtName.Text == "")
            //{
            //    MsgBox.ShowBox("Name should not be blank");
            //    return;
            //}
            //SqlCommand com = new SqlCommand("isnert_tblAdmessionSchedule '" + dtp1.Text + "', '" + txtName.Text + "','" + dtp2.Text + "','" + dtp3.Text + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
            SqlCommand com = new SqlCommand("insert_exam_schedule '" + cmbClass.SelectedValue + "', '" + cmbExam.SelectedValue + "','" + CMBSUB1.SelectedValue + "','" + dtp1.Text + "','" + dtp2.Text + "','" + dtp3.Text + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Exam schedule Created...");
                    //Cancil_form();
                    //LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Exam schedule  already exist");
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
            LoadGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                //txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
               
                txtAdmissionid.Text = dataGridView1.CurrentRow.Cells["SBM_ID"].Value.ToString();
                dtp1.Text = dataGridView1.CurrentRow.Cells["Dates"].Value.ToString();
                dtp2.Text = dataGridView1.CurrentRow.Cells["FromDate"].Value.ToString();
                dtp3.Text = dataGridView1.CurrentRow.Cells["Todate"].Value.ToString();

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }

        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();
            //if (txtAdmissionid.Text == "")
            //{
            //    MsgBox.ShowBox("Please Click to Detail Row!!!");
            //    return;
            //}
            //if (txtName.Text == "")
            //{
            //    MsgBox.ShowBox("Test Nmae Should not be blank...");
            //}
            //else
            //{
            //    SqlCommand cmd = new SqlCommand("update tblAdmissionSchedule set Dates='"+dtp1.Text+"',Name='"+txtName.Text+"',FromDate='"+dtp2.Text+"',ToDate='"+dtp3.Text+"'  where Testid='" + txtAdmissionid.Text + "'",sc);
            //   SqlDataReader dr; 
            //sc.Open();
            //try
            //{
            //    dr = cmd.ExecuteReader();
            //    if (dr.RecordsAffected > 0)
            //    {
            //        MsgBox.ShowBox("Form Updated...");
            //        LoadGrid();
            //        Cancil_form();
            //    }
            //    else
            //    {
            //        MsgBox.ShowBox("This Form already exist");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    sc.Close();
            //}
            //}



            CryExamschedule.classn = cmbClass.SelectedValue.ToString();
            CryExamschedule.examn = cmbExam.Text;
            CryExamschedule.classname = cmbClass.Text;
            CryExamschedule podv = new CryExamschedule();

            podv.ShowDialog();


        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (dtp1.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM exam_schedule WHERE SBM_ID=" + txtAdmissionid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Exam schedule  Deleted Successfully!!!");
                        Cancil_form();
                        LoadGrid();
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

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form(); bindSubCategory(); bindSubCategory1(); bindExam(); 
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSubjectCategory_Leave(object sender, EventArgs e)
        {
            bindSubCategory1();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cmbSubjectCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbExam_Leave(object sender, EventArgs e)
        {
            LoadGrid();

        }
    }
}
