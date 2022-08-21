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
    public partial class FrmHosteler : Form
    {
        public FrmHosteler()
        {
            InitializeComponent();
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }
    

   

      

      

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmStudentMarks_Load(object sender, EventArgs e)
        {
            bindclass(); 
        }
   

        public void InitGrid()
        {
            LoadGrid();
            //dataGridView1.Columns["SEC_ID"].Width = 40;
            //dataGridView1.Columns["SEC_DESC"].Width = 100;
            ////dataGridView1.Columns["Alias"].Width = 60;
            //dataGridView1.Columns["userid"].Width = 60;
            //dataGridView1.Columns["date"].Width = 80;
            //dataGridView1.Columns["terminalid"].Width = 100;
        }
        private void LoadGrid()
        {
            if (cmbClass.SelectedValue != null)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select distinct ts.*,sr.STU_ADM_NO,(sr.STU_FIRST_NAME+sr.STU_MID_NAME +sr.STU_LAST_NAME) as StudentName from  STUDENT_T sr,HOSTLER_T ts where sr.STU_ID=ts.STU_ID and  sr.STU_HOSTLER='H' and sr.STU_CLS_ID=" + cmbClass.SelectedValue + " order by ts.H_NO", sc);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds;
                }
            }
        }

        private void cmb_rollno_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cmbClass.Text = "Select";
        }

     
    }
}
