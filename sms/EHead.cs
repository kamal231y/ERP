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
    public partial class EHead : Form
    {
        public EHead()
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
            cmbterm.Text = "Select";
            txtDescription.Text = "";
            txtexamsequence.Text = "";
            txtweight.Text = "";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
          

        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["EXAM_ID"].Visible=false;
            dataGridView1.Columns["TERM"].Width = 135;
            dataGridView1.Columns["DESCRIPTION"].Width = 230;
            dataGridView1.Columns["DESCRIPTION"].HeaderText = "Exam Description";
            dataGridView1.Columns["EXAM_SEQUENCE"].Width = 190;
            dataGridView1.Columns["EXAM_SEQUENCE"].HeaderText=" Exam Sequence";
            dataGridView1.Columns["EXAM_WEIGHTAGE"].Width = 190;
            dataGridView1.Columns["EXAM_WEIGHTAGE"].HeaderText="Exam Weightage";
        }
        private void LoadGrid()
        {
            string term ;
            if (cmbterm.Text == "" || cmbterm.Text == "Select")
                term = "%";
            else
                term = cmbterm.Text;
           

            dataclass dc = new dataclass();

            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT a.T_DESC as TERM, b.EXAM_DESC as DESCRIPTION, b.EXAM_ID,b.EXAM_SEQUENCE,b.EXAM_WEIGHTAGE FROM EXAMINATION_T b INNER JOIN TERM_T a ON b.EXAM_TERM = a.T_ID and a.T_DESC like'" + term + "' and b.EXAM_DESC like'" + txtDescription.Text + "%'", sc);
            //SqlDataAdapter da = new SqlDataAdapter("select * from EXAMINATION_T ORDER BY EXAM_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindTerm()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TERM_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["T_DESC"] = "Select";
            row["T_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbterm.DataSource = ds.Tables[0];
            cmbterm.DisplayMember = "T_DESC";
            cmbterm.ValueMember = "T_ID";
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txtexamsequence.Text == "")
            {
                txtexamsequence.Text = "0";
            }
            if (txtweight.Text == "")
            {
                txtweight.Text = "0";
            }
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbterm.Text == "" || cmbterm.Text == "Select")
            {
                MsgBox.ShowBox("Pleasae Select Term ..");
                return;
            }
            else if (txtDescription.Text == "")
            {

            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_EXAMINATION_T  '" + txtDescription.Text + "','" + Convert.ToInt16(cmbterm.SelectedValue) + "','" + Convert.ToInt16(txtweight.Text) + "','" + Convert.ToInt16(txtexamsequence.Text) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Exam Head Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Exam Head already exist");
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

        private void btExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();

            if (txtexamid.Text == "")
            {
                MsgBox.ShowBox("Section should  not be blank...");

            }
            else
            {
                SqlCommand com = new SqlCommand("Update_EXAMINATION_T  '"+txtexamid.Text+"', '" + txtDescription.Text + "','" + Convert.ToInt32(cmbterm.SelectedValue) + "','" + Convert.ToInt32(txtweight.Text) + "','" + Convert.ToInt32(txtexamsequence.Text) + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Examination head Updated...");
                        LoadGrid();
                        Cancil_form();
                        
                    }
                    else
                    {
                        MsgBox.ShowBox("This Examination Head already exist");
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
                    if (txtexamid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM EXAMINATION_T WHERE EXAM_ID=" + txtexamid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Examination Head Deleted Successfully!!!");
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void EHead_Load(object sender, EventArgs e)
        {
            bindTerm(); LoadGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {//SELECT a.T_DESC as TERM, b.EXAM_DESC as DESCRIPTION, b.EXAM_ID FROM EXAMINATION_T b INNER JOIN TERM_T a ON b.EXAM_TERM = a.T_ID

                txtexamid.Text = dataGridView1.CurrentRow.Cells["EXAM_ID"].Value.ToString();
                cmbterm.Text = dataGridView1.CurrentRow.Cells["TERM"].Value.ToString();
                txtDescription.Text = dataGridView1.CurrentRow.Cells["DESCRIPTION"].Value.ToString();
                txtweight.Text = dataGridView1.CurrentRow.Cells["EXAM_SEQUENCE"].Value.ToString();
                txtexamsequence.Text = dataGridView1.CurrentRow.Cells["EXAM_WEIGHTAGE"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void cmbterm_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

       
        

  }

        
    }

