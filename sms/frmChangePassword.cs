using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using sms.DAL;
using MsgBoxRet;

namespace sms
{
    public partial class frmChangePassword : Form
    {
        //MsgBox msg = new MsgBox(Color.Red,Color.WhiteSmoke,Color.WhiteSmoke,Color.Red,Color.Red,Color.WhiteSmoke,FlatStyle.Flat);
        int tmpCount;
        public frmChangePassword()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;

            InitializeComponent();
            tmpCount = 0;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btChage_Click(object sender, EventArgs e)
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("select * from tblRights where usertype='" + txtUserType.Text.Replace("'", "''") + "' and userName='" + txtUserName.Text.Replace("'", "''") + "' and password='" + txtOldPass.Text.Replace("'", "''") + "'", sc);
            sc.Open();
            SqlDataReader dr;
            dr = com.ExecuteReader();
            if (dr.Read() == true)
            {
                sc.Close();
                com = new SqlCommand();
                sc.Open();
                if (txtNewPass.Text == txtConfPass.Text)
                {
                    try
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "sp_updatepwd";
                        com.Connection = sc;
                        com.Parameters.AddWithValue("@userType", txtUserType.Text);
                        com.Parameters.AddWithValue("@userName", txtUserName.Text);
                        com.Parameters.AddWithValue("@password", txtNewPass.Text);
                        int i = com.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MsgBox.ShowBox("Password Changed...");
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
                    MsgBox.ShowBox("Confirm Password not Matched with New Password");
                    txtConfPass.Text = "";
                    txtConfPass.Focus();
                }
            }
            else
            {
                MsgBox.ShowBox("Invalid UserName/Password...");
                txtUserName.Text = "";
                txtOldPass.Text = "";
                txtNewPass.Text = "";
                txtConfPass.Text = "";
                txtUserName.Focus();
            }
            txtUserType.Text = "";
            txtUserName.Text = "";
            txtOldPass.Text = "";
            txtNewPass.Text = "";
            txtConfPass.Text = "";

        }

        private void txtUserType_KeyDown(object sender, KeyEventArgs e)
        {
            tmpCount = 1;
            if (e.KeyValue == 40 && listBox1.Visible == true)
            {
                listBox1.Focus();
                listBox1.Visible = true;
            }
            if (e.KeyValue == 13)
                txtUserName.Focus();
            if (e.KeyValue == 27)
                this.Close();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtOldPass.Focus();
            if (e.KeyValue == 27)
                this.Close();
        }

        private void txtOldPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtNewPass.Focus();
            if (e.KeyValue == 27)
                this.Close();
        }

        private void txtNewPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtConfPass.Focus();
            if (e.KeyValue == 27)
                this.Close();
        }

        private void txtConfPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                btChage.Focus();
            if (e.KeyValue == 27)
                this.Close();
        }

        private void createuser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void btChage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void btCancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void btClose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void txtUserType_DoubleClick(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            int i, j;
            listBox1.Items.Clear();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("select distinct userType from tblRights where userType like '" + txtUserType.Text + "' + '%'  order by userType", sc);
            DataSet ds = new DataSet();
            i = 0;
            j = 0;
            da.Fill(ds);
            i = Convert.ToInt32(ds.Tables[0].Rows.Count);


            while (i > 0 && j < i)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                j = j + 1;
            }
            if (i > 0)
            {
                listBox1.Top = txtUserType.Top + txtUserType.Height;
                listBox1.Width = txtUserType.Width;
                listBox1.Left = txtUserType.Left;
                listBox1.Visible = true;
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Visible = false;
            }
        }

        private void txtUserType_TextChanged(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            int i, j;
            listBox1.Items.Clear();
            sc.Open();
            SqlDataAdapter da = new SqlDataAdapter("select distinct userType from tblRights where userType like '" + txtUserType.Text + "' + '%'  order by userType", sc);
            DataSet ds = new DataSet();
            i = 0;
            j = 0;
            da.Fill(ds);
            i = Convert.ToInt32(ds.Tables[0].Rows.Count);


            while (i > 0 && j < i)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[j][0].ToString());
                j = j + 1;
            }
            if (i > 0)
            {
                listBox1.Top = txtUserType.Top + txtUserType.Height;
                listBox1.Width = txtUserType.Width;
                listBox1.Left = txtUserType.Left;
                listBox1.Visible = true;
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Visible = false;
            }
        }

        private void txtUserType_Enter(object sender, EventArgs e)
        {
            if (tmpCount != 1)
            {
                listBox1.Items.Clear();
                listBox1.Visible = false;
            }
            tmpCount = 1;
        }

        private void txtUserType_Leave(object sender, EventArgs e)
        {
            if (txtUserType.Text != "" && listBox1.Visible == false)
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                int i;
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("select userType from tblRights where userType='" + txtUserType.Text + "'  order by userType", sc);
                DataSet ds = new DataSet();
                i = 0;
                da.Fill(ds);
                i = Convert.ToInt32(ds.Tables[0].Rows.Count);
                if (i < 1 && txtUserType.Text != "")
                {
                    MsgBox.ShowBox("UserType not found...");
                    txtUserType.Text = "";
                }
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (tmpCount == 1)
            {
                if (e.KeyValue == 13 && listBox1.Visible == true)
                {
                    txtUserType.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    txtUserName.Focus();
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (tmpCount == 1)
            {
                if (listBox1.Visible == true)
                {
                    try
                    {
                        txtUserType.Text = listBox1.SelectedItem.ToString();
                        listBox1.Visible = false;
                        txtUserName.Focus();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            txtUserType.Text = "";
            txtUserName.Text = "";
            txtOldPass.Text = "";
            txtNewPass.Text = "";
            txtConfPass.Text = "";
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}