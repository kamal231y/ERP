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
    public partial class frmAdmissionTestResult : Form
    {
        SqlCommand cmd;
        string test;
        public frmAdmissionTestResult()
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
            dataGridView1.Columns["FormNo"].Width = 90;
            dataGridView1.Columns["RegNo"].Width = 120;
            dataGridView1.Columns["StudentName"].Width = 200;
            dataGridView1.Columns["Qualified"].Width = 180;
            dataGridView1.Columns["mobileno"].Width = 180;
            dataGridView1.Columns["id"].Width = 0;
            dataGridView1.Columns["Testexamid"].Width = 0;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select id,CAST(RegNo as varchar)RegNo,(CAST(Formnoid as varchar)) as FormNo,StudentName,resultstatus as Qualified,mobileno,RDate,Testexamid from View_AddmissionTestResult where testexamid='" + cmbterm.SelectedValue + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindTerm()
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



        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdmissionTestResult_Load(object sender, EventArgs e)
        {
            bindTerm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        //    {
        //        dataclass dc = new dataclass();

        //        SqlConnection sc = dc.con();
        //        sc.Open();
        //        // SqlCommand com = new SqlCommand("Insert_tblGRMaster '" + cmbbranchname.SelectedValue + "','" + dtpdateofissue.Value + "','" + txtgrfrom.Text + "','" + txtGRlastno.Text + "','" + txtRemarks.Text + "','"+Form1.GlobalLoginUserName.ToString()+"','" +dt1+ "','"+sc.WorkstationId+"','" + TrainNo + "','" + LorryNumber + "','" + dt1.Year + "'", sc);
        //        DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
        
        //        //tran = sc.BeginTransaction();
                
        //        try
        //        {                 
        //            for (int grno = 0; grno < dataGridView1.Rows.Count; grno++)
        //            {
        //                cmd = new SqlCommand("update update_tblTestresult set resultstatus=1,modifyby='" + Form1.GlobalLoginUserName.ToString() + "',cdate='" + dt1 + "' where id='" + dataGridView1.Rows[grno].Cells["id"].Value.ToString() + "'", sc);
        //                //cmd.Transaction = tran;
        //                cmd.ExecuteNonQuery();
        //            }
               
        //            //tran.Commit();
        //            sc.Close();
        //            btn_create.Enabled = false;
        //            MsgBox.ShowBox("Admission Test Result Updated Successfully.....");
        //            //btnDelete.Enabled = true;
        //            //btnDelete.Enabled = true;
        //            //Cancil_form();
        //        }
        //        catch (Exception ex)
        //        {
        //            //tran.Rollback();
        //            sc.Close();
        //            MessageBox.Show(ex.Message, "Warning....", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //        finally
        //        { }

        //    }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataclass dc = new dataclass();
                SqlConnection sc=dc.con();
                DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
                this.dataGridView1.Rows[e.RowIndex].Selected = true;
                txtid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                string rs = "False";
                string str;
                rs = dataGridView1.CurrentRow.Cells["Qualified"].Value.ToString();
                if (rs == "False")
                {
                    str = "1";
                }
                else
                {
                    str = "0";
                }
                dateTimePicker1.Text = DateTime.Now.ToString();
                
                DialogResult dialogResult = MessageBox.Show("Are You Sure you want to update Qualified " + txtid.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    cmd = new SqlCommand("update  tblTestresult set resultstatus='"+str+"',modifyby='" + Form1.GlobalLoginUserName.ToString() + "',cdate='" + dt1.ToString("MM/dd/yyyy") + "' where id='" + txtid.Text + "'", sc);     
                    sc.Open();
                    cmd.ExecuteNonQuery();
                    sc.Close();
                    InitGrid();
                }
                else if (dialogResult == DialogResult.No)
                {
                }
                else
                { }
            }
        }

        private void cmbterm_Leave(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            cmbterm.Text = "-Select-";
            dateTimePicker1.Text = "";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            test = cmbterm.SelectedValue.ToString() ;
            cryAdmissionTestResultDetail.test = test;
            cryAdmissionTestResultDetail obj = new cryAdmissionTestResultDetail();
            obj.ShowDialog();
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }





       
    }
}
