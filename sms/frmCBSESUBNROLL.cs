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
    public partial class frmCBSESUBNROLL : Form
    {
        validation v1 = new validation();
        public int type1; 
        public frmCBSESUBNROLL()
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
            bindClass();
            InitGrid();
           
        }

        private void LoadGrid()
        {
            string ClassValue;
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblCBSESUBENROLLMENT", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["sub_group"].Width = 175;
            dataGridView1.Columns["sub_group"].HeaderText = "Sub Group";
            dataGridView1.Columns["SUB_CODE"].Width = 200;
            dataGridView1.Columns["SUB_CODE"].HeaderText = "SUB CODE";
            dataGridView1.Columns["medium"].Width = 200;
            dataGridView1.Columns["medium"].HeaderText = "Sub Medium";
          
            //dataGridView1.Columns["FEE_PERIOD"].Width = 150;
            //dataGridView1.Columns["FEE_PERIOD"].HeaderText = "Period";
           

            //dataGridView1.Columns["CLF_FEE_ID"].Visible = false;
            //dataGridView1.Columns["CLF_CLS_ID"].Visible = false;


        }
        private void Cancil_form()
        {

            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            //cmbClass.Text = "-Select-";
            txthostleramt.Text = "";
            txtdayhostleramt.Text = "";
            cmbFeetype.Text = "-select-";
            LoadGrid();

        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            if (txthostleramt.Text == "")
            {
                MessageBox.Show("Please Fill  Group !!");
                return;
            }
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-")
            {
                MessageBox.Show("Please Select Subject No !!");
                return;
            }
            if (cmbFeetype.Text == "" || cmbFeetype.Text == "-Select-")
            {
                MessageBox.Show("Please Select Name ");
                return;

            }
           

                if (rbtnE1.Checked == true)
                {
                    type1 = 1;
                }
                if (rbtnH1.Checked == true)
                {
                    type1 = 0;
                }

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand com = new SqlCommand("Insert_CBSEsUBJECTENROLLMENT '" + txthostleramt.Text + "' ,'" + cmbFeetype.SelectedValue + "'," + type1 + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("CBSE Subject Enroll Created...");

                        LoadGrid();
                        Cancil_form();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Subject already exist");
                    }

                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            }

        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-")
            {
                MessageBox.Show("Please Select Class");
                return;
            }
            if (cmbFeetype.Text == "" || cmbFeetype.Text == "-Select-")
            {
                MessageBox.Show("Please Select FeeType");
                return;

            }
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }

            else
            {
                SqlCommand com = new SqlCommand("update_tblClassFeeSingle '" + txtid.Text + "','" + cmbClass.SelectedValue + "','" + cmbFeetype.SelectedValue + "','" + txtdayhostleramt.Text + "','" + txthostleramt.Text + "'", sc);
                
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Class Fee Updated...");

                        LoadGrid();
                        //Cancil_form();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Class Fee already exist");
                    }

                }
                catch (Exception ex)
                { }
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
                    if (txtid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlDataAdapter da = new SqlDataAdapter("select * FROM CLASS_FEE_T ct,STUDENT_FEE_T st where ct.CLF_FEE_ID=st.SEF_FEE_ID and ct.CLF_ID=" + txtid.Text + " and SEF_CLS_ID="+cmbClass.SelectedValue+"  ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MsgBox.ShowBox(" Class Fee not Deleted due to  already use in Student Fee!!!");
                    }
                    else
                    {
                        SqlCommand objCmd = new SqlCommand("DELETE FROM CLASS_FEE_T WHERE CLF_ID=" + txtid.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox(" Class Fee Deleted Successfully!!!");

                            LoadGrid();
                            //Cancil_form();
                        }
                        else
                        {
                            MsgBox.ShowBox(" Deleting Error!!!");
                        }
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                ////txtid.Text = dataGridView1.CurrentRow.Cells["CLF_ID"].Value.ToString();
                //cmbClass.Text = dataGridView1.CurrentRow.Cells["CLS_DESC"].Value.ToString();
                //cmbFeetype.Text = dataGridView1.CurrentRow.Cells["FEE_DESC"].Value.ToString();
                //txtdayhostleramt.Text = dataGridView1.CurrentRow.Cells["CLF_D_AMOUNT"].Value.ToString();
                //txthostleramt.Text = dataGridView1.CurrentRow.Cells["CLF_H_AMOUNT"].Value.ToString();
                //btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select distinct (sub_type) from CBSE_REG_SUBJECT order by sub_type", sc);
           
        }
        private void BindSubjectName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select sub_id, sub_name from CBSE_REG_SUBJECT  where SUB_type="+cmbClass.Text+" order by sub_name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["sub_name"] = "-Select-";
            row["sub_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbFeetype.DataSource = ds.Tables[0];
            cmbFeetype.DisplayMember = "sub_name";
            cmbFeetype.ValueMember = "sub_id";
        }

        private void frmCBSESUBNROLL_Load(object sender, EventArgs e)
        {
            LoadGrid();
            bindClass();
            BindSubjectName();

           
            //LoadGrid();
           
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbFeetype_Leave(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtdayhostleramt_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txthostleramt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //v1.digitonly(e);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

       

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
           

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

     

        

        private void txtdayhostleramt_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbClass_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            BindSubjectName();
        }

        private void cmbFeetype_Leave_1(object sender, EventArgs e)
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter(" select  sub_code from CBSE_REG_SUBJECT  where sub_id=" + cmbFeetype.SelectedValue + " order by sub_name", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    txtdayhostleramt.Text = dt.Rows[0]["sub_code"].ToString();


            //}
        }

       

      

       

        

     
    }
}
