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
    public partial class frmGroupMaster : Form

    {
        CheckBox chkbox = new CheckBox();
        DataTable dt;
        public frmGroupMaster()
        {
            InitializeComponent();
            Binddept();
        }
        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblmastergroup ORDER BY MNAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Mname"] = "Select";
            row["MID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "Mname";
            cmbDept.ValueMember = "MID";
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView2.Columns["GID"].Width = 50;
            dataGridView2.Columns["GNAME"].Width = 200;
            dataGridView2.Columns["Description"].Width = 250;
            dataGridView2.Columns["MNAME"].Width = 200;
            dataGridView2.Columns["MID"].Width = 100;

            dataGridView2.Columns["MID"].Visible = false;
           
        }

        private void LoadGrid()
        {
            DateTime ddt1;
            //ddt1 = dtattendance.Value;

            //string month;
            //string Year;
            //month = ddt1.Month.ToString();
            //Year = ddt1.Year.ToString();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select g.GID,g.GNAME,g.Description,m.MID,m.MNAME,g.type from  tblgroup g,tblmastergroup m where g.mid=m.mid", sc);


                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       
        void Cancel_Form()
        {
           

        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtattendance_ValueChanged(object sender, EventArgs e)
        {
            //LoadGrid();
            InitGrid();
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void frmGroupMaster_Load(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            string type = "";
            /*Asset
Lability
Income
Expence*/
            if (cmbtype.Text == "Asset")
                type = "A";
            else if (cmbtype.Text == "Lability")
                type = "L";
            else if (cmbtype.Text == "Income")
                type = "I";
            else if (cmbtype.Text == "Expence")
                type = "E";
            if (cmbDept.SelectedValue.ToString() == "0" || cmbDept.Text == "")
            {
                MsgBox.ShowBox("Please Select Group...");
            }
            else if (txtGroupN.Text == "")
            {
                MsgBox.ShowBox("Group Name Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("sprocGrouplnsert '" + Convert.ToInt32(cmbDept.SelectedValue) + "' ,'" + txtGroupN.Text + "','" + Form1.GlobalLoginUserName.ToString() + "','" + type + "'", sc);
                SqlDataReader dr;
                //@PGrouptype int	,@GroupName  nvarchar(50),@userId smallint      ,'" + dt1 + "','" + sc.WorkstationId + "'
                sc.Open();
                try
                {
                    int i = com.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MsgBox.ShowBox("Group Name Created...");
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Group Name Name already exist");
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

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            if (dataGridView2.Rows.Count > 0)
            {
                // txtSectionname.Text = dataGridView1.CurrentRow.Cells["SEC_DESC"].Value.ToString();
                //txtAlias.Text = dataGridView1.CurrentRow.Cells["alias"].Value.ToString();
                txtSectionid.Text = dataGridView2.CurrentRow.Cells["GID"].Value.ToString();
                cmbDept.Text = dataGridView2.CurrentRow.Cells["MNAME"].Value.ToString();
                txtGroupN.Text = dataGridView2.CurrentRow.Cells["GNAME"].Value.ToString();
                string type =dataGridView2.CurrentRow.Cells["type"].Value.ToString();
                if ( type == "A")
                    cmbtype.Text = "Asset";
                else if ( type == "L")
                    cmbtype.Text = "Lability";
                else if (type == "I")
                    cmbtype.Text= "Income";
                else if (type == "E")
                    cmbtype.Text = "Expence";
                btnupdate.Enabled = true;
                btndelete.Enabled = true;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string type = "";
            /*Asset
Lability
Income
Expence*/
            if (cmbtype.Text == "Asset")
                type = "A";
            else if (cmbtype.Text == "Lability")
                type = "L";
            else if (cmbtype.Text == "Income")
                type = "I";
            else if (cmbtype.Text == "Expence")
                type = "E";
            if (txtGroupN.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (cmbDept.Text == "")
            {
                MsgBox.ShowBox("Group Type Should not be blank...");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select * from tblgroup where Gid='" + txtSectionid.Text + "'", sc);
                sc.Open();
                SqlDataReader dr1 = cmd.ExecuteReader();

                if (dr1.Read())
                {
                    
                        dr1.Close();
                        sc.Close();
                        SqlCommand com = new SqlCommand("sprocGroupUpdate '" + txtSectionid.Text + "','" + txtGroupN.Text + "', '" + cmbDept.SelectedValue + "','" + type + "'", sc);
                        SqlDataReader dr;
                        sc.Open();
                        try
                        {
                           dr = com.ExecuteReader();
                            if (dr.RecordsAffected > 0)
                            {
                                MsgBox.ShowBox("Group Updated...");
                                InitGrid();
                            }
                            else
                            {
                                MsgBox.ShowBox("This Group Name already exist");
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
                    else
                    {
                        MsgBox.ShowBox("This Group Name Not Updated Because it is used");
                    }
                }

            
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Group!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtSectionid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("sprocGroupdelete " + txtSectionid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Group Deleted Successfully!!!");
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Group Already Used!!!");
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

        
    }
}
