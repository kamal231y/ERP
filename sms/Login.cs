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
using MsgBoxRet;
using System.Data.SqlClient;

namespace sms
{
    public partial class Login : Form
    {
        int tmpCount;
        public static string system_name = "", passwd = "", userid = "", databasename = "";
        List<CONN_STRING> listdatabase = new List<CONN_STRING>();

        public Login()
        {
            binddatabase();
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




        void binddatabase()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con1st();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  SOF_CONNECTION", sc);
            sc.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                listdatabase.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listdatabase.Add(new CONN_STRING());
                    listdatabase[i].CON_ID = dt.Rows[i]["CON_ID"].ToString();
                    listdatabase[i].CON_NAME = dt.Rows[i]["CON_NAME"].ToString();
                    listdatabase[i].SERVER_NAME = dt.Rows[i]["SERVER_NAME"].ToString();
                    listdatabase[i].DATABASE_NAME = dt.Rows[i]["DATABASE_NAME"].ToString();
                    listdatabase[i].USER_ID = dt.Rows[i]["USERID"].ToString();
                    listdatabase[i].PWD = dt.Rows[i]["PASWD"].ToString();
                    listdatabase[i].ACCDM_YEAR = dt.Rows[i]["acad_year"].ToString();
                    listdatabase[i].ISDEFAULT = dt.Rows[i]["IS_DEFAULT"].ToString();
                }

            }



        }



        private void ChkData()
        {
            if (tmpCount == 1)
            {
                if (txtUserType.Text != "" && listBox1.Visible == false)
                {
                    dataclass dd = new dataclass();
                    SqlConnection sc = dd.con();
                    int i;
                    sc.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select distinct userType from tblRights where userType='" + txtUserType.Text + "'  order by userType", sc);
                    DataSet ds = new DataSet();
                    i = 0;
                    da.Fill(ds);
                    i = Convert.ToInt32(ds.Tables[0].Rows.Count);
                    if (i < 1 && txtUserType.Text != "")
                    {
                        MsgBox.ShowBox("User Type not found!!!");
                        txtUserType.Text = "";
                        listBox1.Visible = false;
                        txtUserType.Focus();
                    }
                }
            }
        }

        private bool Blank()
        {
            if (txtUserType.Text == "")
            {
                MsgBox.ShowBox("User Type Should not Be Blank...");
                txtUserType.Focus();
                return false;

            }
            if (txtUserName.Text == "")
            {
                MsgBox.ShowBox("User Name Should not Be Blank...");
                txtUserName.Focus();
                return false;
            }
            if (txtPwd.Text == "")
            {
                MsgBox.ShowBox("Password Should not Be Blank...");
                txtPwd.Focus();
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            if (Blank())
            { }
            else
                return;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand com = new SqlCommand("select userType,username,password from tblRights where userType='" + txtUserType.Text.Replace("'", "''") + "' and userName='" + txtUserName.Text.Replace("'", "''") + "' and password='" + txtPwd.Text.Replace("'", "''") + "'", sc);
            SqlDataReader dr;
            sc.Open();
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                this.Close();
                Form1.GlobalLoginUserName = txtUserName.Text;
                Form1.GlobalFAYear = cmbFAYear.SelectedValue.ToString();
                Form1.GlobalLoginUserType = txtUserType.Text;
                DATALAYER.dataclass.year = cmbFAYear.SelectedValue.ToString();
                DAL.dataclass.year = cmbFAYear.SelectedValue.ToString();
            }
            else
            {
                MsgBox.ShowBox("Invalid UserName and Password");
                txtUserName.Text = "";
                txtPwd.Text = "";
                txtUserName.Focus();
            }
            dr.Close();
            SqlDataAdapter da = new SqlDataAdapter("select imagename from image_t", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            byte[] img_arr1 = new byte[0];
            img_arr1 = (byte[])dt.Rows[0][0];
            Form1.cancel = img_arr1;
            da = new SqlDataAdapter("select * from setting_t ", sc);
            DataTable setting_t = new DataTable();
            da.Fill(setting_t);
            Form1.fee_reciept = Convert.ToBoolean(setting_t.Rows[0][0].ToString());
            Form1.sale_reciept = Convert.ToBoolean(setting_t.Rows[0][1].ToString());
            Form1.report_reciept = Convert.ToBoolean(setting_t.Rows[0][2].ToString());
            Form1.SMS = Convert.ToBoolean(setting_t.Rows[0][3].ToString());
            Form1.board = Convert.ToBoolean(setting_t.Rows[0][4].ToString());
            Form1.SMSKEY = Convert.ToString(setting_t.Rows[0]["SMSKEY"].ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtUserType_Enter(object sender, EventArgs e)
        {
            ChkData();
            listBox1.Items.Clear();
            listBox1.Visible = false;
            if (tmpCount != 1)
            {
                listBox1.Items.Clear();
                listBox1.Visible = false;
            }
            tmpCount = 1;
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
            //if (e.KeyValue == 27)
            //    this.Close();
        }

        private void txtUserType_Leave(object sender, EventArgs e)
        {
            if (txtUserType.Text != "" && listBox1.Visible == false)
            {
                dataclass dd = new dataclass();
                SqlConnection sc = dd.con();
                int i;
                sc.Open();
                SqlDataAdapter da = new SqlDataAdapter("select distinct userType from tblRights where userType='" + txtUserType.Text + "'  order by userType", sc);
                DataSet ds = new DataSet();
                i = 0;
                da.Fill(ds);
                i = Convert.ToInt32(ds.Tables[0].Rows.Count);
                if (i < 1 && txtUserType.Text != "")
                {
                    MsgBox.ShowBox("User Type not found!!!");
                    listBox1.Visible = false;
                    txtUserType.Text = "";
                    txtUserType.Focus();
                }
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

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (tmpCount != 1)
            {
                listBox1.Items.Clear();
                listBox1.Visible = false;
            }
            tmpCount = 1;
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (tmpCount == 1)
            {
                if (e.KeyValue == 13 && listBox1.Visible == true)
                {
                    try
                    {
                        txtUserType.Text = listBox1.SelectedItem.ToString();
                        listBox1.Visible = false;
                        txtUserType.Focus();
                    }
                    catch
                    {
                    }

                }
                if (e.KeyValue == 38 && listBox1.SelectedIndex == 0)
                {
                    txtUserType.Focus();

                }
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txtPwd.Focus();
            //if (e.KeyValue == 27)
            //    this.Close();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1.Focus();
            //if (e.KeyValue == 27)
            //    this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (tmpCount == 1)
            {
                if (listBox1.Visible == true)
                {
                    txtUserType.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    txtUserType.Focus();

                }
                if (listBox1.SelectedIndex == 0)
                {
                    txtUserType.Focus();

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

        private void Login_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            //txtUserType.Focus();
            listBox1.Visible = false;
        }

        private void txtUserName_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
        }

        private void txtPwd_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            listBox1.Visible = false;
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            listBox1.Visible = false;
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            BLclass bl = new BLclass();
            e.KeyChar = (char)bl.FirstLetterUppercase((int)e.KeyChar, txtUserName.Text);
        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
            //if (listBox1.Items.Count > 0)
            //    txtUserType.Text = listBox1.SelectedItem.ToString();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (listdatabase.Count > 0)
            {
                FAYear();
                txtUserType.Focus();
                BindCompany();
            }
            else {
                MessageBox.Show("Database is not found");
            }
        }
        private void FAYear()
        {
            cmbFAYear.DataSource = listdatabase;
            cmbFAYear.ValueMember = "CON_ID";
            cmbFAYear.DisplayMember = "ACCDM_YEAR";
        }

        private void BindCompany()
        {
           var newarr=listdatabase.Where(p =>p.ISDEFAULT=="1" ).ToList();

           if (newarr.Count > 0)
           {
              system_name = newarr[0].SERVER_NAME ;
               passwd = newarr[0].PWD;
              userid = newarr[0].USER_ID;
              databasename = newarr[0].DATABASE_NAME;

           }
           else {
              system_name = listdatabase[0].SERVER_NAME;
              passwd = listdatabase[0].PWD;
             userid = listdatabase[0].USER_ID;
            databasename = listdatabase[0].DATABASE_NAME;
           }


            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from COMPANY_T", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblCompany.Text = " " + dt.Rows[0]["COM_NAME"].ToString();
            }
            else
            {
                lblCompany.Text = " ";
            }
        }

        private void lblCompany_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbFAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            String conid = cmbFAYear.SelectedValue.ToString();

            var newarr = listdatabase.Where(p => p.CON_ID == conid).ToList();

            if (newarr.Count > 0)
            {
                system_name = newarr[0].SERVER_NAME;
                passwd = newarr[0].PWD;
                userid = newarr[0].USER_ID;
                databasename = newarr[0].DATABASE_NAME;


                FAYear();
                txtUserType.Focus();
             //   BindCompany();

            }
            else
            {
                system_name = listdatabase[0].SERVER_NAME;
                passwd = listdatabase[0].PWD;
                userid = listdatabase[0].USER_ID;
                databasename = listdatabase[0].DATABASE_NAME;

                FAYear();
                txtUserType.Focus();
                //BindCompany();
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
