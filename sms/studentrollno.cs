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
    public partial class FrmStudentrollno : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public FrmStudentrollno()
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
            //bindadmissionno();
            bindclass();
            Cancel_form();
            //InitGrid();

        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["Admission_No"].Width = 120;
            dataGridView1.Columns["Roll_No"].Width = 90;
            dataGridView1.Columns["Class"].Width = 90;
            dataGridView1.Columns["Section"].Width = 90;
            
        }
        private void LoadGrid()
        {
            string class1; string section1;
            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                class1 = "%";
            }
            else
            {
                class1=cmbclass.Text;
            }
            if (cmbsection.Text == "" || cmbsection.Text == "Select")
            {
                section1 = "%";
            }
            else
            {
                section1 = cmbsection.Text;
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  rollno as Roll_No,admissionno as Admission_No,CLS_DESC as Class,SEC_DESC as Batch from View_showrollno where CLS_DESC Like '" + class1 + "'and SEC_DESC like '"+section1+"'  ORDER BY rollno", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void Cancel_form()

        {
            btnDelete.Enabled = false;
            
            btnCreate.Enabled = true;
            cmbclass.Text = "Select";
            cmbadmno.Text = "";
            cmbsection.Text = "";
            txtstrengh.Text = "";
            txtavailable.Text = "";
        }
        private void studentrollno_Load(object sender, EventArgs e)
        {

        }
        private void bindadmissionno()
        { int a=0;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from View_rollnoassign where stu_status=" + a + " and CLS_DESC='" + cmbclass.Text + "' ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Admission_no"] = "Select";
            row["sid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbadmno.DataSource = ds.Tables[0];
            cmbadmno.DisplayMember = "Admission_no";
            cmbadmno.ValueMember = "sid";
        }
        private void bindclass()
        {
            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblClassMaster order by CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "Select";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "CLS_DESC";
            cmbclass.ValueMember = "CLS_ID";
        }

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string StateName;
            //if (cmbclass.Text == "Select" || cmbclass.Text == "")
            //{
            //    StateName = "%";
            //}
            //else
            //{
            //    StateName = cmbclass.Text;
            //}
            //bindadmissionno(); LoadGrid();
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter("select * from View_section where CLS_DESC = '" + StateName + "'and SEC_AVAILABLE>0 ", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //DataRow row = dt.NewRow();
            //row["SEC_DESC"] = "Select";
            //row["SEC_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            //cmbsection.DataSource = dt;
            //cmbsection.DisplayMember = "SEC_DESC";
            //cmbsection.ValueMember = "SEC_ID";
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbsection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
            string StateName;
            if (cmbsection.Text == "Select" || cmbsection.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbsection.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from View_section where SEC_DESC = '" + StateName + "' and CLS_DESC='" + cmbclass.Text + "' ", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtstrengh.Text = dr["SEC_STRENGTH"].ToString();
                txtavailable.Text = dr["SEC_AVAILABLE"].ToString();
            }
            
        }
       //private void LoadGrid1()
       // {

       //     string class1;
       //     if (cmbsection.Text == "" || cmbsection.Text == "Select")
       //     {
       //         class1 = "%";
       //     }
       //     else
       //     {
       //         class1 = cmbsection.Text;
       //     }
       //     dataclass dc = new dataclass();
       //     SqlConnection sc = dc.con();
       //     SqlDataAdapter da = new SqlDataAdapter("select  rollno as Roll_No,admissionno as Admission_No,CLS_DESC as Class,SEC_DESC as Section from View_showrollno where CLS_DESC = '" + class1 + "' and ORDER BY rollno", sc);
       //     DataSet ds = new DataSet();
       //     da.Fill(ds);
       //     dataGridView1.DataSource = ds.Tables[0];
       // }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cmbadmno.Text == "" || cmbadmno.Text == "Select")
            {
                MsgBox.ShowBox("Pls Select Admission No. !!!!!");
                return;
            }
                if (cmbsection.Text == "" || cmbsection.Text == "Select")
                {
                    MsgBox.ShowBox("Pls Select Batch. !!!!!");
                    return;
                }
            
            else
            {
                int rollno = Convert.ToInt32(txtstrengh.Text) - Convert.ToInt32(txtavailable.Text);
                int nextrollno = rollno + 1;

                DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                SqlConnection sc = dc.con();

                ArrayList arr = new ArrayList();
                arr.Add(nextrollno);

                arr.Add(Convert.ToInt32(cmbclass.SelectedValue));
                arr.Add(Convert.ToInt32(cmbsection.SelectedValue));
                arr.Add(cmbadmno.Text);
                int i = dc.InsertRows("Insert_student_rollno", arr);
                LoadGrid();
                Cancel_form();
                MsgBox.ShowBox("Student Roll No. is generated !!");

            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                cmbadmno.Text = dataGridView1.CurrentRow.Cells["Admission_No"].Value.ToString();
                cmbclass.Text = dataGridView1.CurrentRow.Cells["Class"].Value.ToString();
                //cmbsection.Text = dataGridView1.CurrentRow.Cells["Section"].Value.ToString();
                cmbsection.Text = dataGridView1.CurrentRow.Cells["Roll_No"].Value.ToString();


                
                btnDelete.Enabled = true; ;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel_form();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (cmbadmno.Text=="Select" || cmbadmno.Text=="")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblrollno WHERE admissionno='" +cmbadmno.Text  + "'", conn);
                    SqlCommand cmd = new SqlCommand("update tblSectionMaster set SEC_AVAILABLE=SEC_AVAILABLE+1 where SEC_DESC='" + cmbsection.Text + "'and SEC_CLS_ID="+Convert.ToInt32(cmbclass.SelectedValue)+"", conn);
                    
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                   
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Batch Deleted Successfully!!!");
                        Cancel_form();
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
                    }
                }

                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void cmbclass_Leave(object sender, EventArgs e)
        
        {
            string StateName;
            if (cmbclass.Text == "Select" || cmbclass.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbclass.Text;
            }
            bindadmissionno(); LoadGrid();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_section where CLS_DESC = '" + StateName + "'and SEC_AVAILABLE>0 order by SEC_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["SEC_DESC"] = "Select";
            row["SEC_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbsection.DataSource = dt;
            cmbsection.DisplayMember = "SEC_DESC";
            cmbsection.ValueMember = "SEC_ID";
            
        
        }

        private void cmbclass_Validated(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void cmbadmno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StateName;
            if (cmbadmno.Text == "Select" || cmbadmno.Text == "")
            {
                StateName = "%";
              
            }
            else
            {
                StateName =cmbadmno.Text;
            }
          LoadGrid();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_bindrollnotable where CLS_DESC = '" + cmbclass.Text + "'and SEC_AVAILABLE>0 and Admission_no='" + StateName + "' order by SEC_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

           
            cmbsection.DataSource = dt;
            cmbsection.DisplayMember = "SEC_DESC";
            cmbsection.ValueMember = "SEC_ID";
        }
        }

       

        
    }

