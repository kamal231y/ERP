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
    public partial class frmstuFeestart : Form
    {
        public frmstuFeestart()
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
           
        }

        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from student_receipt_t where sfr_stu_id='"+textBox2.Text+"'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }



      
       
        private void btn_create_Click(object sender, EventArgs e)
        {
          
        }

        private void frmFeeHead_Load(object sender, EventArgs e)
        {
            InitGrid();
            bindclass();
            listBox1.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0;
            if (chkApr.Checked)
                mt1 = 50000;
            if (chkMay.Checked)
                mt2 = 50000;
            if (chkJune.Checked)
                mt3 = 50000;
            if (chkjuly.Checked)
                mt4 = 50000;
            if (chkAug.Checked)
                mt5 = 50000;
            if (chkSep.Checked)
                mt6 = 50000;
            if (chkOct.Checked)
                mt7 = 50000;
            if (chkNov.Checked)
                mt8 = 50000;
            if (chkDec.Checked)
                mt9 = 50000;
            if (chkJan.Checked)
                mt10 = 50000;
            if (chkFeb.Checked)
                mt11 = 50000;
            if (chkMar.Checked)
                mt12 = 50000;

            if (textBox2.Text != "")
            {
                SqlCommand com = new SqlCommand("update_stutblfeehead '" + textBox2.Text + "','" + mt1 + "','" + mt2 + "','" + mt3 + "','" + mt4 + "','" + mt5 + "','" + mt6 + "','" + mt7 + "','" + mt8 + "','" + mt9 + "','" + mt10 + "','" + mt11 + "','" + mt12 + "'", sc);
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
            else
            {
                SqlCommand com = new SqlCommand("update_stutblfeeheadclasswise '" + cmb_class.SelectedValue + "','" + mt1 + "','" + mt2 + "','" + mt3 + "','" + mt4 + "','" + mt5 + "','" + mt6 + "','" + mt7 + "','" + mt8 + "','" + mt9 + "','" + mt10 + "','" + mt11 + "','" + mt12 + "'", sc);
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
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Cancil_form()
        {
            bindclass();
            btn_create.Enabled = true;
            btnUpdate.Enabled = true;
            btn_delete.Enabled = false;
            txtDescription.Text = "";
            textBox1.Text = "";
            panel3.Visible = false;
            listBox1.Visible = false;
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
           
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_SEQUENCE ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "-Select-";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            InitGrid();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

           
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
            panel3.Visible = true;
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
        private void feestatus()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select  * from student_receipt_t where sfr_stu_id like '" + textBox2.Text + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                panel3.Visible = true;
                if (dt.Rows[0]["SFR_APRIL"].ToString() == "50000")
                {
                    chkApr.Checked = true;
                }
                else
                {
                    chkApr.Checked = false;
                }
                if (dt.Rows[0]["SFR_MAY"].ToString() == "50000")
                {
                    chkMay.Checked = true;
                }
                else
                {
                    chkMay.Checked = false;
                }
                if (dt.Rows[0]["SFR_JUNE"].ToString() == "50000")
                {
                    chkJune.Checked = true;
                }
                else
                {
                    chkJune.Checked = false;
                }
                if (dt.Rows[0]["SFR_JULY"].ToString() == "50000")
                {
                    chkjuly.Checked = true;
                }
                else
                {
                    chkjuly.Checked = false;
                }
                if (dt.Rows[0]["SFR_AUGUST"].ToString() == "50000")
                {
                    chkAug.Checked = true;
                }
                else
                {
                    chkAug.Checked = false;
                }
                if (dt.Rows[0]["SFR_SEPTEMBER"].ToString() == "50000")
                {
                    chkSep.Checked = true;
                }
                else
                {
                    chkSep.Checked = false;
                }
                if (dt.Rows[0]["SFR_OCTOBER"].ToString() == "50000")
                {
                    chkOct.Checked = true;
                }
                else
                {
                    chkOct.Checked = false;
                }
                if (dt.Rows[0]["SFR_NOVEMBER"].ToString() == "50000")
                {
                    chkNov.Checked = true;
                }
                else
                {
                    chkNov.Checked = false;
                }
                if (dt.Rows[0]["SFR_DECEMBER"].ToString() == "50000")
                {
                    chkDec.Checked = true;
                }
                else
                {
                    chkDec.Checked = false;
                }
                if (dt.Rows[0]["SFR_JANUARY"].ToString() == "50000")
                {
                    chkJan.Checked = true;
                }
                else
                {
                    chkJan.Checked = false;
                }
                if (dt.Rows[0]["SFR_FEBRUARY"].ToString() == "50000")
                {
                    chkFeb.Checked = true;
                }
                else
                {
                    chkFeb.Checked = false;
                }
                if (dt.Rows[0]["SFR_MARCH"].ToString() == "50000")
                {
                    chkMar.Checked = true;
                }
                else
                {
                    chkMar.Checked = false;
                }
            }
        }
        private void txtDescription_Leave(object sender, EventArgs e)
        {
            BindAdmNo();  
        }
        private void BindAdmNo()
        {
            dataclass dc = new dataclass();
            SqlConnection conn = dc.con();
            SqlDataAdapter objCmd = new SqlDataAdapter(" select stu_id,student_name from view_studentreport where admno='" + txtDescription.Text.Trim() + "'", conn);
            DataTable dt = new DataTable();
            objCmd.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                textBox2.Text = dt.Rows[0]["stu_id"].ToString();
                textBox1.Text = dt.Rows[0]["student_name"].ToString();
                feestatus();
                panel3.Visible = true;
                listBox1.Visible = false;
            }
            else
            {
                MessageBox.Show("This Adm No does Not Exists");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            {

                string Name, CLS;

                if (textBox1.Text == "-Select-")
                {
                    Name = "%";
                }
                else
                {
                    Name = textBox1.Text.Trim().ToString();
                }
                
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '|' + ds1.Tables[0].Rows[j][1].ToString() + '|' + ds1.Tables[0].Rows[j][2].ToString() + '|' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = textBox1.Top + textBox1.Height;
                    //listBox1.Width = txtstuno.Width;
                    listBox1.Left = textBox1.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('|');
                    txtDescription.Text = str[0].ToString();
                    listBox1.Visible = false;
                    BindAdmNo();

                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            
            panel3.Visible = true;
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
           
        }
      
    }
}
