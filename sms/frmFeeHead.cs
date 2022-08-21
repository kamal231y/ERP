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
    public partial class frmFeeHead : Form
    {
        public frmFeeHead()
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

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["FEE_ID"].Visible = false;
            dataGridView1.Columns["FEE_DESC"].Width = 205;
            dataGridView1.Columns["FEE_DESC"].HeaderText = "Description";
            dataGridView1.Columns["FEE_PERIOD"].Width = 100;
            dataGridView1.Columns["FEE_PERIOD"].HeaderText = "Period";
            dataGridView1.Columns["FEE_REFUND"].Width = 75;
            dataGridView1.Columns["FEE_REFUND"].HeaderText = "Refund";
            dataGridView1.Columns["ACC_NAME"].Width = 100;
            dataGridView1.Columns["ACC_NAME"].HeaderText = "Acc. Name";
            dataGridView1.Columns["FEE_TYPE"].Width = 100;
            dataGridView1.Columns["FEE_TYPE"].HeaderText = "Fee Type";
            dataGridView1.Columns["FEE_TYPE"].Visible = true; ;
            dataGridView1.Columns["PRD_ALIAS"].Visible = false; ;
        }

        private void LoadGrid()
        {
            string str;
            if (cmbaccount.Text == "" || cmbaccount.Text == "-Select-")
                str = "%";
            else
                str = cmbaccount.Text;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select fp.PRD_ALIAS,FEE_ID,FEE_DESC,fp.PRD_DESC as FEE_PERIOD,FEE_REFUND,ACC_NAME,FEE_TYPE=case when FEE_TYPE=0 then 'Ragular' else 'Extra' end from FEE_T ft, ACCOUNTS_T at,FEE_PERIOD fp  where ft.fee_id not in(4,10,12) and ft.FEE_ACC_ID=at.ACC_ID and ft.FEE_PERIOD=fp.PRD_Alias and at.ACC_NAME like '" + str + "%' and ft.FEE_DESC like '" + txtDescription.Text + "%' ORDER BY FEE_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }



        private void bindAccountName()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from ACCOUNTS_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["ACC_NAME"] = "-Select-";
            row["ACC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbaccount.DataSource = ds.Tables[0];
            cmbaccount.DisplayMember = "ACC_NAME";
            cmbaccount.ValueMember = "ACC_ID";
        }
        private void bindPeriod()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from FEE_PERIOD order by PRD_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["PRD_DESC"] = "-Select-";
            row["PRD_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbperiod.DataSource = ds.Tables[0];
            cmbperiod.DisplayMember = "PRD_DESC";
            cmbperiod.ValueMember = "PRD_ID";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            if (cmbaccount.Text == "" || cmbaccount.Text == "-Select-")
            {
                MessageBox.Show("Please Select AccountName");
            }
            if (cmbperiod.Text == "" || cmbperiod.Text == "-Select-")
            {
                MessageBox.Show("Please Select Period");
            }
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
        
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (cmbaccount.SelectedValue.ToString() == "")
            {
                MsgBox.ShowBox("Account Name should not be blank");
                return;
            }
            if (cmbperiod.Text == "" || cmbperiod.Text == "-Select-")
            {
                MsgBox.ShowBox("Period should not be blank");
                return;
            }

            int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0;
            if (chkApr.Checked)
                mt1 = 1;
            if (chkMay.Checked)
                mt2 = 1;
            if (chkJune.Checked)
                mt3 = 1;
            if (chkjuly.Checked)
                mt4 = 1;
            if (chkAug.Checked)
                mt5 = 1;
            if (chkSep.Checked)
                mt6 = 1;
            if (chkOct.Checked)
                mt7 = 1;
            if (chkNov.Checked)
                mt8 = 1;
            if (chkDec.Checked)
                mt9 = 1;
            if (chkJan.Checked)
                mt10 = 1;
            if (chkFeb.Checked)
                mt11 = 1;
            if (chkMar.Checked)
                mt12 = 1;
            

            int  value;
            if (rbregularfee.Checked == true)
            {
                value = 0;
            }
            else
            {
                value = 1;
 
            }
            string  refndable;
            if (chkrefundable.Checked == true)
            {
                refndable = "Y";
            }
            else
            {
                refndable = "N";
            }
            SqlCommand com = new SqlCommand("Insert_tblFeeHead '" + txtDescription.Text + "','" + cmbperiod.SelectedValue + "', '" + refndable + "', '" + cmbaccount.SelectedValue + "','" + Convert.ToInt32(value) + "','" + mt1 + "','" + mt2 + "','" + mt3 + "','" + mt4 + "','" + mt5 + "','" + mt6 + "','" + mt7 + "','" + mt8 + "','" + mt9 + "','" + mt10 + "','" + mt11 + "','" + mt12 + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Fee Created...");
                    Cancil_form();
                    InitGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Fee Head  already exist");
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

        private void frmFeeHead_Load(object sender, EventArgs e)
        {
            InitGrid();
            bindAccountName();
            bindPeriod();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbaccount.Text == "" || cmbaccount.Text == "-Select-")
            {
                MessageBox.Show("Please Select AccountName");
            }
            if (cmbperiod.Text == "" || cmbperiod.Text == "-Select-")
            {
                MessageBox.Show("Please Select Period");
            } 
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);

            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
           
           
            int value;
            if (rbregularfee.Checked == true)
            {
                value = 0;
            }
            else
            {
                value = 1;

            }
            if (cmbperiod.Text == "" || cmbperiod.Text == "-Select-")
            {
                MsgBox.ShowBox("Period should not be blank");
                return;
            }

            string refndable;
            if (chkrefundable.Checked == true)
            {
                refndable = "Y";
            }
            else
            {
                refndable = "N";
            }
            int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0, mt13 = 0, mt14 = 0, mt15 = 0;
            if (chkApr.Checked)
                mt1 = 1;
            if (chkMay.Checked)
                mt2 = 1;
            if (chkJune.Checked)
                mt3 = 1;
            if (chkjuly.Checked)
                mt4 = 1;
            if (chkAug.Checked)
                mt5 = 1;
            if (chkSep.Checked)
                mt6 = 1;
            if (chkOct.Checked)
                mt7 = 1;
            if (chkNov.Checked)
                mt8 = 1;
            if (chkDec.Checked)
                mt9 = 1;
            if (chkJan.Checked)
                mt10 = 1;
            if (chkFeb.Checked)
                mt11 = 1;
            if (chkMar.Checked)
                mt12 = 1;
            if (chkMar.Checked)
                mt13 = 1;
            if (chkMar.Checked)
                mt14 = 1;
            if (chkMar.Checked)
                mt15 = 1;


            SqlCommand com = new SqlCommand("update_tblfeehead '" + txtid.Text + "','" + txtDescription.Text + "','" + cmbperiod.SelectedValue + "', '" + refndable + "','" + cmbaccount.SelectedValue + "', '" + value + "','" + mt1 + "','" + mt2 + "','" + mt3 + "','" + mt4 + "','" + mt5 + "','" + mt6 + "','" + mt7 + "','" + mt8 + "','" + mt9 + "','" + mt10 + "','" + mt11 + "','" + mt12 + "','" + mt13 + "','" + mt14 + "','" + mt15 + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Fee Updated...");
                   
                    
                    //Cancil_form();
                    InitGrid();
                }
                else
                {
                    MsgBox.ShowBox("This Fee Head already exist");
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
                    SqlDataAdapter da = new SqlDataAdapter("select * from CLASS_FEE_T where CLF_FEE_ID =" + txtid.Text + "", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MsgBox.ShowBox(" FeeHead not Deleted due to  already use in Class Fee!!!");
                    }
                    else
                    {
                        SqlCommand objCmd = new SqlCommand("DELETE FROM FEE_T WHERE FEE_ID=" + txtid.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox(" FeeHead Deleted Successfully!!!");
                            Cancil_form();
                            InitGrid();
                        }
                        else
                        {
                            MsgBox.ShowBox(" Deleting Error!!!");
                        }
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

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtDescription.Text = "";
            cmbperiod.Text = "-Select-";
            cmbaccount.Text = "-Select-";
            chkApr.Checked = false;
            chkMay.Checked = false;
            chkJune.Checked = false;
            chkjuly.Checked = false;
            chkAug.Checked = false;
            chkSep.Checked = false;
            chkOct.Checked = false;
            chkNov.Checked = false;
            chkDec.Checked = false;
            chkJan.Checked = false;
            chkFeb.Checked = false;
            chkMar.Checked = false;
            panel3.Visible = false;
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            InitGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                //cmbaccount.Text = dataGridView1.CurrentRow.Cells["FEE_ACC_ID"].Value.ToString();
                txtDescription.Text = dataGridView1.CurrentRow.Cells["FEE_DESC"].Value.ToString();
                cmbperiod.Text = dataGridView1.CurrentRow.Cells["FEE_PERIOD"].Value.ToString();
                cmbaccount.Text = dataGridView1.CurrentRow.Cells["ACC_NAME"].Value.ToString();
                txtid.Text = dataGridView1.CurrentRow.Cells["FEE_ID"].Value.ToString();

                if (dataGridView1.CurrentRow.Cells["FEE_TYPE"].Value.ToString() == "Ragular")
                {
                    rbregularfee.Checked = true;
                }
                else
                {
                    rbextrafee.Checked = false;
                }


                //=========================
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select  * from FEE_DETAIL_T where fee_ID like '" + txtid.Text + "'", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    panel3.Visible = true;
                    if (dt.Rows[0]["APR"].ToString() == true.ToString())
                    {
                        chkApr.Checked = true;
                    }
                    else
                    {
                        chkApr.Checked = false;
                    }
                    if (dt.Rows[0]["MAY"].ToString() == true.ToString())
                    {
                        chkMay.Checked = true;
                    }
                    else
                    {
                        chkMay.Checked = false;
                    }
                    if (dt.Rows[0]["JUN"].ToString() == true.ToString())
                    {
                        chkJune.Checked = true;
                    }
                    else
                    {
                        chkJune.Checked = false;
                    }
                    if (dt.Rows[0]["JUL"].ToString() == true.ToString())
                    {
                        chkjuly.Checked = true;
                    }
                    else
                    {
                        chkjuly.Checked = false;
                    }
                    if (dt.Rows[0]["AUG"].ToString() == true.ToString())
                    {
                        chkAug.Checked = true;
                    }
                    else
                    {
                        chkAug.Checked = false;
                    }
                    if (dt.Rows[0]["SEP"].ToString() == true.ToString())
                    {
                        chkSep.Checked = true;
                    }
                    else
                    {
                        chkSep.Checked = false;
                    }
                    if (dt.Rows[0]["OCT"].ToString() == true.ToString())
                    {
                        chkOct.Checked = true;
                    }
                    else
                    {
                        chkOct.Checked = false;
                    }
                    if (dt.Rows[0]["NOV"].ToString() == true.ToString())
                    {
                        chkNov.Checked = true;
                    }
                    else
                    {
                        chkNov.Checked = false;
                    }
                    if (dt.Rows[0]["DEC"].ToString() == true.ToString())
                    {
                        chkDec.Checked = true;
                    }
                    else
                    {
                        chkDec.Checked = false;
                    }
                    if (dt.Rows[0]["JAN"].ToString() == true.ToString())
                    {
                        chkJan.Checked = true;
                    }
                    else
                    {
                        chkJan.Checked = false;
                    }
                    if (dt.Rows[0]["FEB"].ToString() == true.ToString())
                    {
                        chkFeb.Checked = true;
                    }
                    else
                    {
                        chkFeb.Checked = false;
                    }
                    if (dt.Rows[0]["MAR"].ToString() == true.ToString())
                    {
                        chkMar.Checked = true;
                    }
                    else
                    {
                        chkMar.Checked = false;
                    }
                }
                //==============
                if (dataGridView1.CurrentRow.Cells["FEE_REFUND"].Value.ToString() == "Y")
                {
                    chkrefundable.Checked = true;
                }

                if (dataGridView1.CurrentRow.Cells["prd_alias"].Value.ToString() == "MT")
                {
                    panel3.Visible = true;
                }
                else
                    panel3.Visible = false;
                //rbgrade.Checked =Convert.ToBoolean( dataGridView1.CurrentRow.Cells["SUB_EVO_ID"].Value.ToString());
                //rbOSub.Checked = Convert.ToBoolean(dataGridView1.CurrentRow.Cells["SUB_TYPE_ID"].Value.ToString());

                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void cmbaccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void cmbperiod_Leave(object sender, EventArgs e)
        {
            if (cmbperiod.Text == "More Time")
                panel3.Visible = true;
            else           
                panel3.Visible = false;           
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      
    }
}
