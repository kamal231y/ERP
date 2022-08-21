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

namespace sms
{
    public partial class frmConfirmAdmission : Form
    {
        string test;
         string SName; 
         string SMobioleNo; 
        public frmConfirmAdmission()
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
            Cancil_form();

        }
        private void Cancil_form()
        {
            // btn_create.Enabled = true;
            // cmbterm.Text = "-Select-";
            // txtDescription.Text = "";
            // txtexamsequence.Text = "";
            // txtweight.Text = "";
            // //txtstrength.Text = "";
            //// cmbTeacher.Text = "-Select-";
            // //cmbClass.Text = "-Select-";
            // btnUpdate.Enabled = false;
            // btn_delete.Enabled = false;
            //// txtSectionname.Text = "";

        }
        public void InitGrid()
        {
            LoadGrid();
            //dataGridView1.Columns["FormNo"].Width = 75;
            dataGridView1.Columns["RegNo"].Width = 90;
            dataGridView1.Columns["StudentName"].Width = 160;
            //dataGridView1.Columns["Qualified"].Width = 180;
            dataGridView1.Columns["mobileno"].Width = 100;
            //dataGridView1.Columns["id"].Width = 70;
            dataGridView1.Columns["Formnoid"].Width = 70;
            //dataGridView1.Columns["Formnoid"].Visible = false;
            dataGridView1.Columns["RDate"].Width = 100;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();//  select (CAST(RegNo as varchar)+'/'+cast(YEAR(cdate)as varchar)) as RegNo,(CAST(Formnoid as varchar)+'/'+cast(YEAR(cdate)as varchar)) as FormNo from View_AddmissionTestResult
            SqlDataAdapter da = new SqlDataAdapter("select id,Formnoid,(CAST(Formnoid as varchar)) as RegNo,StudentName,mobileno,RDate from View_AddmissionTestResult where testexamid='" + cmbterm.SelectedValue + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindTest()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblAdmissionSchedule order by Testid", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Name"] = "Select";
            row["Testid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbterm.DataSource = ds.Tables[0];
            cmbterm.DisplayMember = "Name";
            cmbterm.ValueMember = "Testid";
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void frmConfirmAdmission_Load(object sender, EventArgs e)
        {
            bindTest();
        }

        private void cmbterm_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            test = cmbterm.SelectedValue.ToString();
            cryConfirmAdmission.test = test;
            cryConfirmAdmission obj = new cryConfirmAdmission();
            obj.ShowDialog();
        }


        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbterm_KeyUp(object sender, KeyEventArgs e)
        {
            InitGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

          if (dataGridView1.Rows.Count > 0)
            {
                frmAdmission.SName = dataGridView1.CurrentRow.Cells["Formnoid"].Value.ToString();
                frmAdmission obj = new frmAdmission();
                obj.ShowDialog();
               
            }


        }

        private void cmbterm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Leave(object sender, EventArgs e)
        {
        
        }
    }
}
