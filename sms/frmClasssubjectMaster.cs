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
    public partial class frmClasssubjectMaster : Form
    {
        public frmClasssubjectMaster()
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
           InitGrid();
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;

            cmbClass.SelectedValue = 0;
            cmbSubject.SelectedValue = 0;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            
           
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["CSB_ID"].Visible = false; ;
            dataGridView1.Columns["ClassName"].Width = 150;
            dataGridView1.Columns["SubjectName"].Width = 150;
            dataGridView1.Columns["SUB_EVO_ID"].Width = 75;
            dataGridView1.Columns["SUB_EVO_ID"].HeaderText = "Grade";
            dataGridView1.Columns["CSB_OPTIONAL"].Width = 75;
            dataGridView1.Columns["CSB_OPTIONAL"].HeaderText = "Optional";




        }
        private void LoadGrid()
        {


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select CSB_ID, CLS_DESC as ClassName,SUB_DESC as SubjectName,SUB_EVO_ID,CSB_OPTIONAL from View_tblClassSubject where CLS_DESC = '" + cmbClass.Text + "' ORDER BY CSB_ID", sc);
            //SqlDataAdapter da = new SqlDataAdapter("select CSB_ID, CLS_DESC as ClassName,SUB_DESC as SubjectName,CSB_OPTIONAL from View_tblClassSubject ORDER BY CSB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }


        private void btn_create_Click(object sender, EventArgs e)
        {
            if (cmbClass.Text == "" || cmbClass.Text == "-select-")
            {
                MessageBox.Show("Please Select Class");
                return;
            }
            if (cmbSubject.Text == "" || cmbSubject.Text == "-Select-")
            {
                MessageBox.Show("Please Select Subject");
                return;
            }

            ////
            bool value = true;
            if (rbmarks.Checked == true)
            {
                value = false;

            }
            if (rbgrade.Checked == true)
            {
                value = true;
            }

            ////
            bool value1 = true;

            if (rbMain.Checked == true)
            {
                value1 = false;
            }
            if (rbOSub.Checked == true)
            {
                value1 = true;
            }


            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbSubject.SelectedValue.ToString() == "")
            {
                MsgBox.ShowBox("Subject should not be blank");
                return;
            }
            //SqlCommand com = new SqlCommand("Insert_Subject '" + cmbSubject.SelectedValue + "', '"+cmbClass.SelectedValue+"','" +Convert.ToInt32( value)+ "','" + Convert.ToInt32( value1)+ "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
            SqlCommand com = new SqlCommand("Insert_CLASS_SUBJECT_T '" + cmbSubject.SelectedValue + "', '" + cmbClass.SelectedValue + "','" + Convert.ToInt32(value) + "','" + Convert.ToInt32(value1) + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Class Subject Created...");
                    Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Class Subject Name already exist");
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbClass.Text == "" || cmbClass.Text == "-select-")
            {
                MessageBox.Show("Please Select Class");
                return;
            }
            if (cmbSubject.Text == "" || cmbSubject.Text == "-Select-")
            {
                MessageBox.Show("Please Select Subject");
                return;
            }
            bool value = true;
            if (rbmarks.Checked == true)
            {
                value = false;

            }
            if (rbgrade.Checked == true)
            {
                value = true;
            }

            ////
            bool value1 = true;

            if (rbMain.Checked == true)
            {
                value1 = false;
            }
            if (rbOSub.Checked == true)
            {
                value1 = true;
            }
         
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtsubjectid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (cmbSubject.SelectedValue.ToString() == "")
            {
                MsgBox.ShowBox("Subject Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_CLASS_SUBJECT_T '" + txtsubjectid.Text + "','" + cmbSubject.SelectedValue + "', '" + cmbClass.SelectedValue + "', '" + Convert.ToInt32(value) + "', '" + Convert.ToInt32(value1) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Class Subject Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Class Subject Name already exist");
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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtsubjectid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM CLASS_SUBJECT_T WHERE CSB_ID=" + txtsubjectid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Class Subject Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
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
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                cmbClass.Text = dataGridView1.CurrentRow.Cells["ClassName"].Value.ToString();
                cmbSubject.Text = dataGridView1.CurrentRow.Cells["SubjectName"].Value.ToString();
                txtsubjectid.Text = dataGridView1.CurrentRow.Cells["CSB_ID"].Value.ToString();

                if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_EVO_ID"].Value))
                {
                    rbgrade.Checked = true;
                }
                else
                {
                    rbmarks.Checked = true;
                }
                ////rbMain.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_TYPE_ID"].Value.ToString());
                if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells["CSB_OPTIONAL"].Value))
                {
                    rbOSub.Checked = true;
                }
                else
                {
                    rbMain.Checked = true;
                }
                ////rbgrade.Checked =Convert.ToBoolean( dataGridView1.CurrentRow.Cells["SUB_EVO_ID"].Value.ToString());
                ////rbOSub.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_TYPE_ID"].Value.ToString());

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }
        private void bindSubject()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Subject_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "-Select-";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbSubject.DataSource = ds.Tables[0];
            cmbSubject.DisplayMember = "SUB_DESC";
            cmbSubject.ValueMember = "SUB_ID";
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from class_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "-Select-";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "CLS_DESC";
            cmbClass.ValueMember = "CLS_ID";
        }

        private void frmSectionMaster_Load(object sender, EventArgs e)
        {
            bindClass();
            bindSubject();
            
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }
    }
}
