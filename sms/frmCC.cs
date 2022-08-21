 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;
using System.Collections;     

namespace sms
{
    public partial class frmCC : Form
    {
        string classname;
        string stu_id;
        SqlDataReader dr;
        public frmCC()
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
            
        }



       private void Cancil_form()
        {
          
            txtclass.Text = "";
            txtAdmNo.Text = "";
            txtStuName.Text = "";
            listBox1.Visible = false;

        }

       private void bindCCNo()
       {
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           SqlCommand cmd = new SqlCommand("select max(CC_no)+1 from tbl_CC ", sc);
           sc.Open();
           SqlDataReader dr = cmd.ExecuteReader();
           if (dr.Read())
           {
               txtCCNo.Text = dr[0].ToString();
               if (txtCCNo.Text == "")
               {
                   txtCCNo.Text = "1";
               }
           }
           sc.Close();
       }




       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {
           stu_id = txtAdmNo.Text;

           if (txtAdmNo.Text == "-Select-" || txtAdmNo.Text == "" || txtAdmNo.Text == "0")
               stu_id = "%";
           else
               stu_id = txtAdmNo.Text.ToString();
            if (txtclass.Text == "" || txtclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = txtclass.Text.ToString();
            }
            //  DateTime oDate = Convert.ToDateTime(CCDate.Text);
            DateTime oDate = Convert.ToDateTime(CCDate.Text, System.Globalization.CultureInfo.InvariantCulture);


            String month = oDate.Month.ToString();
            String year = oDate.Year.ToString();

            cryCC.month = month;
            cryCC.year = year;
            cryCC.classid = classname;
            cryCC.Admno = stu_id;
            cryCC.refid = txtCCNo.Text;
            cryCC obj = new cryCC();
            obj.ShowDialog();
            
          
       }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bindCCNo();
            Cancil_form();
            
        }

  

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            bindData();
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBusRouteReport_Load(object sender, EventArgs e)
        {
            bindCCNo();
            listBox1.Visible = false;
        }


        private void cmbclass_Leave(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            try
            {
                DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                SqlConnection sc = dc.con();
                // SqlConnection sc = dc.con();

                ArrayList arr = new ArrayList();
                DateTime dtCCDate = Convert.ToDateTime(CCDate.Value, System.Globalization.CultureInfo.InvariantCulture);
                arr.Add(Convert.ToInt32(txtCCNo.Text));
                arr.Add(txtAdmNo.Text);
                arr.Add(txtStuName.Text);
                arr.Add(txtclass.Text);
                arr.Add(dtCCDate.ToString());
                int n = dc.InsertRows("Insert_CC", arr);
                if (n > 0)
                {
                    MsgBox.ShowBox("Certificate Created Successfully.");
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to Delete Test?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtCCNo.Text == "")
                    {
                        MsgBox.ShowBox("Please fill Certificate No...");
                    }
                    SqlDataAdapter da = new SqlDataAdapter("select * from tbl_CC where CC_NO= " + txtCCNo.Text + " ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        SqlCommand objCmd = new SqlCommand("delete from tbl_CC where CC_NO = " + txtCCNo.Text + "", conn);
                        SqlDataReader dr;
                        objCmd.Connection.Open();
                        dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox("Certificate deleted successfully...");

                        }
                        else
                        {
                            MsgBox.ShowBox("Deleting Error!!!");
                        }
                    }
                }
                catch (Exception ex)
                { }
            }
            else
            {
            }
        }

        private void txtCCNo_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from view_CC where CC_NO='" + txtCCNo.Text + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                btnCreate.Enabled = false;
                txtAdmNo.Text = dr["CC_ADM_NO"].ToString();
                txtStuName.Text = dr["CC_STUDENT_NAME"].ToString();
                txtclass.Text = dr["CC_CLASS"].ToString();
                CCDate.Text = dr["CC_DATE"].ToString();
            }
            sc.Close();
        }

        private void cmbAdmNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            stuname();
        }


        private void stuname()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select Student_Name,class from view_studentreport where  admno='" + txtAdmNo.Text + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtStuName.Text = (dr["Student_Name"].ToString());
                txtclass.Text = (dr["class"]).ToString();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtStuName_TextChanged(object sender, EventArgs e)
        {
            {

                string Name;

                if (txtStuName.Text == "-Select-" || txtStuName.Text == "" || txtStuName.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtStuName.Text.Trim().ToString();
                }

                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and  stu_status='y' order by roll ", sc1);
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
                    listBox1.Top = txtStuName.Top + txtStuName.Height;
                    //listBox1.Width = txtstuno.Width;
                    listBox1.Left = txtStuName.Left;
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
                    txtAdmNo.Text = str[0].ToString();
                    txtStuName.Text = str[1].ToString();
                    txtclass.Text = str[3].ToString();
                    listBox1.Visible = false;
                    

                }
                catch
                { }
            }
        }


        private void txtAdmNo_Leave(object sender, EventArgs e)
        {
            stuname();
        }

       
       
    }
}