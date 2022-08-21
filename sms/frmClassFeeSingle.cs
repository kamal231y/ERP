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
    public partial class frmClassFeeSingle : Form
    {
        validation v1 = new validation();
         int RowIndex2 = 0;
        public string classname;
        public frmClassFeeSingle()
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
            InitGrid();
           
        }

        private void LoadGrid1()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select FEE_ID,FEE_DESC,FEE_PERIOD,0 AS DAYSCHOLAR,0 AS HOSTLER FROM FEE_T WHERE FEE_ID not in (12,10,4) ORDER BY FEE_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void LoadGrid()
        {
            string ClassValue;
            if (cmbClass.Text == "-Select-" || cmbClass.Text == "")
            {
                ClassValue = "%";
            }
            else
            {
                ClassValue = cmbClass.SelectedValue.ToString();
            }
            string Feetype;
            if (cmbFeetype.Text == "-Select-" || cmbFeetype.Text == "")
            {
                Feetype = "%";
            }
            else
            {
                Feetype = cmbFeetype.SelectedValue.ToString();
            }
           
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
          //  SqlDataAdapter da = new SqlDataAdapter("select CLF_ID,FEE_DESC,FEE_PERIOD, CLF_D_AMOUNT AS DAYSCHOLAR,CLF_H_AMOUNT AS HOSTLER ,CLF_FEE_ID,CLF_CLS_ID  from   CLASS_FEE_T ct,FEE_T ft,CLASS_T tt where ct.CLF_FEE_ID=ft.FEE_ID and tt.CLS_ID=ct.CLF_CLS_ID and  ct.CLF_CLS_ID Like '" + ClassValue + "' and CLF_FEE_ID like '" + Feetype + "' and  CLF_FEE_ID not in (12,10,4) ORDER BY CLS_DESC,FEE_DESC", sc);
            SqlDataAdapter da = new SqlDataAdapter("SELECT class_fee_T.CLF_ID,fee_t.FEE_DESC,fee_t.FEE_ID as CLF_FEE_ID ,fee_t.FEE_PERIOD, CASE WHEN CLF_CLS_ID IS NULL THEN ' ' ELSE CLF_CLS_ID END AS CLF_CLS_ID, CASE WHEN CLF_D_AMOUNT IS NULL THEN '0' ELSE CLF_D_AMOUNT END AS DAYSCHOLAR, CASE WHEN CLF_H_AMOUNT IS NULL THEN '0' ELSE CLF_H_AMOUNT END AS HOSTLER FROM  fee_t LEFT OUTER JOIN class_fee_T ON fee_t.FEE_ID = class_fee_T.CLF_FEE_ID and  class_fee_T.CLF_CLS_ID Like '" + ClassValue + "' and class_fee_T.CLF_FEE_ID not in (12,10,4) ORDER BY fee_t.FEE_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["CLF_CLS_ID"].Visible = false;
            dataGridView1.Columns["CLF_FEE_ID"].Visible = false;
            dataGridView1.Columns["CLF_ID"].Visible = false;
            dataGridView1.Columns["FEE_DESC"].Width = 200;
            dataGridView1.Columns["FEE_DESC"].HeaderText = "Fees";
            dataGridView1.Columns["DAYSCHOLAR"].Width = 200;
            dataGridView1.Columns["DAYSCHOLAR"].HeaderText = "Day Scholar Amount";
            dataGridView1.Columns["HOSTLER"].Width = 180;
            dataGridView1.Columns["HOSTLER"].HeaderText = "Hostler Amount";
            dataGridView1.Columns["FEE_PERIOD"].Width = 150;
            dataGridView1.Columns["FEE_PERIOD"].HeaderText = "Period";
       



        }

        public void InitGrid1()
        {
            LoadGrid1();
            dataGridView1.Columns["FEE_DESC"].Width = 200;
            dataGridView1.Columns["FEE_DESC"].HeaderText = "Fees";
            dataGridView1.Columns["DAYSCHOLAR"].Width = 200;
            dataGridView1.Columns["DAYSCHOLAR"].HeaderText = "Day Scholar Amount";
            dataGridView1.Columns["HOSTLER"].Width = 180;
            dataGridView1.Columns["HOSTLER"].HeaderText = "Hostler Amount";
            dataGridView1.Columns["FEE_PERIOD"].Width = 150;
            dataGridView1.Columns["FEE_PERIOD"].HeaderText = "Period";
            dataGridView1.Columns["FEE_ID"].Visible = false;



        }
        private void Cancil_form()
        {

            btn_create.Enabled = true;
            //btnUpdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbClass.Text = "-Select-";
          
            cmbFeetype.Text = "-select-";
            InitGrid1();

        }
       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ClassValue;
            if (cmbClass.Text == "" || cmbClass.Text == "-Select-")
            {
                MessageBox.Show("Please Select Class");
                return;
            }
            else
            {
                ClassValue = cmbClass.SelectedValue.ToString();
            }
            
                   dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    sc.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select * from CLASS_FEE_T where CLF_CLS_ID=" + cmbClass.SelectedValue + " ", sc);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //SqlCommand cmd12 = new SqlCommand(" delete from CLASS_FEE_T where CLF_CLS_ID=" + cmbClass.SelectedValue + "", sc);
                        //cmd12.ExecuteNonQuery();
                    }
                    SqlTransaction tran;
                    tran = sc.BeginTransaction();
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr;
                    cmd1.Transaction = tran;
                    try
                    {


                        for (int z = 0; z <= dataGridView1.Rows.Count - 1; z++)
                        {
                            cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "update_tblClassFeeSingle";
                            cmd1.Connection = sc;
                        //    cmd1.Parameters.AddWithValue("@CLF_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["CLF_ID"].Value));
                            cmd1.Parameters.AddWithValue("@CLF_CLS_ID", Convert.ToInt32(ClassValue));
                            cmd1.Parameters.AddWithValue("@CLF_FEE_ID", Convert.ToInt32(dataGridView1.Rows[z].Cells["CLF_FEE_ID"].Value));
                            if (dataGridView1.Rows[z].Cells["DAYSCHOLAR"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@CLF_D_AMOUNT", 0);
                            else
                                cmd1.Parameters.AddWithValue("@CLF_D_AMOUNT", Convert.ToInt32(dataGridView1.Rows[z].Cells["DAYSCHOLAR"].Value));
                            if (dataGridView1.Rows[z].Cells["HOSTLER"].Value.ToString() == "")
                                cmd1.Parameters.AddWithValue("@CLF_H_AMOUNT", 0);
                            else
                                cmd1.Parameters.AddWithValue("@CLF_H_AMOUNT", Convert.ToInt32(dataGridView1.Rows[z].Cells["HOSTLER"].Value));

                            cmd1.Transaction = tran;
                            cmd1.ExecuteNonQuery();
                        }
                        tran.Commit();
                        MsgBox.ShowBox("Class Fee Updated...");
                        LoadGrid();
                        // Cancil_form();
                        //}
                        //else
                        //{
                        //    MsgBox.ShowBox("This Subject Marks  already exist");
                        //}

                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        //MsgBox.ShowBox("Subject Marks  Created...");
                    }
                    finally
                    {
                        sc.Close();
                    }

            }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex2 = e.RowIndex;
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
                txtid.Text = dataGridView1.CurrentRow.Cells["CLF_ID"].Value.ToString();
                cmbClass.Text = dataGridView1.CurrentRow.Cells["CLS_DESC"].Value.ToString();
                cmbFeetype.Text = dataGridView1.CurrentRow.Cells["FEE_DESC"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CLASS_T WHERE CLS_DP!='N' order by CLS_SEQUENCE", sc);
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
        private void bindFeeType()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from FEE_T where fee_id not in (12,10,4) order by FEE_desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["FEE_DESC"] = "-Select-";
            row["FEE_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbFeetype.DataSource = ds.Tables[0];
            cmbFeetype.DisplayMember = "FEE_DESC";
            cmbFeetype.ValueMember = "FEE_ID";
        }
        private void frmClassFeeSingle_Load(object sender, EventArgs e)
        {
            LoadGrid();
            bindClass();
            bindFeeType();
           
            //LoadGrid();
           
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
            v1.digitonly(e);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmbFeetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void frmClassFeeSingle_Load_1(object sender, EventArgs e)
        {
            InitGrid1();
            bindClass();
            bindFeeType();
        }

        private void btn_close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbClass_Leave_1(object sender, EventArgs e)
        {
           
            InitGrid();
        }

        

        private void btn_delete_Click_1(object sender, EventArgs e)
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
                    SqlDataAdapter da = new SqlDataAdapter("select * FROM CLASS_FEE_T ct,STUDENT_FEE_T st where ct.CLF_FEE_ID=st.SEF_FEE_ID and ct.CLF_ID=" + txtid.Text + " and SEF_CLS_ID=" + cmbClass.SelectedValue + "  ", conn);
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

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            if (cmbClass.Text == "" || cmbClass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbClass.SelectedValue.ToString();
            }
            cryClassFeeReport.classname = classname;
            cryClassFeeReport obj = new cryClassFeeReport();
            obj.ShowDialog();
        }

        private void cmbFeetype_Leave_1(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbClass_KeyPress(object sender, KeyPressEventArgs e)
        {
            InitGrid();
        }

        private void cmbClass_KeyDown(object sender, KeyEventArgs e)
        {
            InitGrid();
        }

        private void cmbClass_KeyUp(object sender, KeyEventArgs e)
        {
            InitGrid();
        }

       

        

     
    }
}
