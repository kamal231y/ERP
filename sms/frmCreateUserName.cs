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
    public partial class frmCreateUserName : Form
    {
       // MsgBox msg = new MsgBox(Color.Red,Color.WhiteSmoke,Color.WhiteSmoke,Color.Red,Color.Red,Color.WhiteSmoke,FlatStyle.Flat);
        int tmpCount;
        public frmCreateUserName()
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

        private void btCreate_Click(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("select userType,username from tblRights where  usertype='" + txtUserType.Text.Replace("'", "''") + "' and username='" + txtUserName.Text.Replace("'", "''") + "'", sc);
            SqlDataReader dr;
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                MsgBox.ShowBox("This UserType/UserName already Exist...");
                txtUserName.Text = "";
                txtPwd.Text = "";
                txtUserName.Focus();
            }
            else
            {
                sc.Close();
                com = new SqlCommand();
                sc.Open();
                try
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "sp_CreteUser";
                    com.Connection = sc;
                    com.Parameters.AddWithValue("@userType", txtUserType.Text);
                    com.Parameters.AddWithValue("@userName", txtUserName.Text);
                    com.Parameters.AddWithValue("@password", txtPwd.Text);
                    int i=com.ExecuteNonQuery();
                    if (i>0)
                    {
                        
                        MsgBox.ShowBox("User Name Created...");
                        txtUserName.Text = "";
                        txtPwd.Text = "";
                        txtUserName.Focus();
                    }
                    else
                    {
                        MsgBox.ShowBox("This User allready exist");
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

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            //txtUserType.Text = "";
            txtUserName.Text = "";
            txtPwd.Text = "";
        }

        private void txtUserType_KeyDown(object sender, KeyEventArgs e)
        {
            
            //tmpCount = 1;
            //if (e.KeyValue == 40 && listBox1.Visible == true)
            //{
            //    listBox1.Focus();
            //    listBox1.Visible = true;
            //}
            //if (e.KeyValue == 13)
            //    txtUserName.Focus();
            //if (e.KeyValue == 27)
            //    this.Close();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtPwd.Focus();
            if (e.KeyValue == 27)
                this.Close();
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

            //if (txtUserType.Text != "" && listBox1.Visible == false)
            //{
            //    dataclass dd = new dataclass();
            //    SqlConnection sc = dd.con();
            //    int i;
            //    sc.Open();
            //    SqlDataAdapter da = new SqlDataAdapter("select userType from tblLogin where userType='" + txtUserType.Text + "'  order by userType", sc);
            //    DataSet ds = new DataSet();
            //    i = 0;
            //    da.Fill(ds);
            //    i = Convert.ToInt32(ds.Tables[0].Rows.Count);
            //    if (i < 1 && txtUserType.Text != "")
            //    {
            //        MsgBox.ShowBox("UserType not found...");
            //        txtUserType.Text = "";
            //    }
            //}
        }

        private void txtUserType_TextChanged(object sender, EventArgs e)
        {
            //dataclass dd = new dataclass();
            //SqlConnection sc = dd.con();
            //int i, j;
            //listBox1.Items.Clear();
            //sc.Open();
            //SqlDataAdapter da = new SqlDataAdapter("select distinct userType from tblLogin where userType like '" + txtUserType.Text + "' + '%'  order by userType", sc);
            //DataSet ds = new DataSet();
            //i = 0;
            //j = 0;
            //da.Fill(ds);
            //i = Convert.ToInt32(ds.Tables[0].Rows.Count);


            //while (i > 0 && j < i)
            //{
            //    listBox1.Items.Add(ds.Tables[0].Rows[j][0].ToString());
            //    j = j + 1;
            //}
            //if (i > 0)
            //{
            //    listBox1.Top = txtUserType.Top + txtUserType.Height;
            //    listBox1.Width = txtUserType.Width;
            //    listBox1.Left = txtUserType.Left;
            //    listBox1.Visible = true;
            //}
            //else
            //{
            //    listBox1.Items.Clear();
            //    listBox1.Visible = false;
            //}

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (tmpCount == 1)
            //{
            //    if (e.KeyValue == 13 && listBox1.Visible == true)
            //    {
            //        txtUserType.Text = listBox1.SelectedItem.ToString();
            //        listBox1.Visible = false;
            //        txtUserName.Focus();
            //    }
            //}
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void CreateUserName_Load(object sender, EventArgs e)
        {
            txtUserType.Text = "USER";
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                btCreate.Focus();
            if (e.KeyValue == 27)
                this.Close();
        }

        private void CreateUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
                this.Close();
        }

        private void btCreate_KeyDown(object sender, KeyEventArgs e)
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

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (tmpCount == 1)
            {
                if ( listBox1.Visible == true)
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

       
    }
}