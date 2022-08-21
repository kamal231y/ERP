using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using sms.BLClass;
using System.Data.SqlClient;
using MsgBoxRet;

namespace sms
{
    public partial class frmmsgtypemaster: Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public frmmsgtypemaster()
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
           // InitGrid();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Cancil_form()
        {
            btnUpdate.Enabled = false;
            btn_create.Enabled = true;
            btn_delete.Enabled = false;
            txtmsg.Text = "";
            txtstatename.Text = "";
            //txtAlias.Text = "";
            InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["msgtype"].Width = 200;
            dataGridView1.Columns["msgtype"].HeaderText = "msgtype";
            dataGridView1.Columns["msg"].Width = 300;
            dataGridView1.Columns["msg"].HeaderText = "MSG";
            
          
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select *  from tblmsgtype where msgtype like '" + txtstatename.Text + "%' order by msgtype", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            //dataGridView1.Columns["id"].Visible = false;
            //dataGridView1.Columns["msgtype"].Width = 120;
            //dataGridView1.Columns["msgtype"].HeaderText = "msgtype";
            //dataGridView1.Columns["msg"].Width = 600;
            //dataGridView1.Columns["msg"].HeaderText = "MSG";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Msg. Type  Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("insert into tblmsgtype values( '" + txtstatename.Text + "','" + txtmsg.Text+ "')", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Msg. Type Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Msg. Type already exist");
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtmsg.Text = dataGridView1.CurrentRow.Cells["MSG"].Value.ToString();
                txtstatename.Text = dataGridView1.CurrentRow.Cells["msgtype"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                btnUpdate.Enabled=true;
                btn_delete.Enabled = true;
           
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtstateid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtstatename.Text == "")
            {
                MsgBox.ShowBox("Msg Type Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update  tblmsgtype set msgtype= '" + txtstatename.Text + "',MSG='" + txtmsg.Text + "'where id='" + txtstateid.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Msg Type Updated...");
                        Cancil_form();
                        
                    }
                    else
                    {
                        MsgBox.ShowBox("Updating Error !!!");
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Msg. Type!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtstateid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM tblmsgtype WHERE ID=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Msg. Type Deleted Successfully!!!");
                        Cancil_form();
                        
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

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
           // LoadGrid();
        }

        private void frmStateMaster_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtstatename_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

       
       
    }
}
