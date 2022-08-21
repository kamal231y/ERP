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
        

namespace sms
{
    public partial class frmGatePass : Form
    {
        string classname, Stu_id,phone;
        SqlDataReader dr;
        public frmGatePass()
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
        private void bindGrpupNo()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select max(id)+1 from tblGatePass ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if(dt.Rows[0][0].ToString()!="")
                txtno.Text = dt.Rows[0][0].ToString();
                else
                    txtno.Text = "1";
            }
            else
                txtno.Text = "1";

        }
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC +' '+ SEC_DESC) as Class ,* from View_fillfeecollection order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
           
        }
        private void BindAdmno()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_studentreport where stu_status='y' order by admno", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Admno"] = "-Select-";
            row["Admno"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbAdmNo.DataSource = ds.Tables[0];
            cmbAdmNo.DisplayMember = "Admno";
            cmbAdmNo.ValueMember = "Admno";

        }
        private void bindroll()
        {
                string cls;
                if (cmbclass.Text == "-Select-" || cmbclass.Text == "" || cmbclass.Text == "System.Data.DataRowView")
                {
                    cls = "0";
                }
                else
                {
                    cls = cmbclass.SelectedValue.ToString();
                }

                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + cls + "' order by  STC_ROLL_NO", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    DataRow row = dt.NewRow();
                    row["STC_ROLL_NO"] = "0";
                    row["STC_STU_ID"] = 0;
                    dt.Rows.InsertAt(row, 0);
                    cmbRoll.DataSource = dt;
                    cmbRoll.DisplayMember = "STC_ROLL_NO";
                    cmbRoll.ValueMember = "STC_STU_ID";
                   // cmbRoll.Text = ROLL;
                }
        }
        private void Cancil_form()
        {
            btnSave.Enabled = true;
            bindGrpupNo();
            cmbAdmNo.Text = "";
            cmbRoll.Text = "";
            txtName.Text = "";
            txtReason.Text = ""; txtrec.Text = "";
            cmbclass.SelectedValue = "0";
            dtdate.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           print();
           
       }
       private void print()
       {

           {
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbclass.SelectedValue.ToString();
               }

             
               cryGatePass.gatepass = txtno.Text.Trim();
               cryGatePass obj = new cryGatePass();
               obj.ShowDialog();
           }
       }
        private void btnCancel_Click(object sender, EventArgs e)
        {          
            Cancil_form();
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            print();
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbclass_Leave(object sender, EventArgs e)
        {
        }
        private void frmGatePass_Load(object sender, EventArgs e)
        {
            bindGrpupNo(); bindclass(); BindAdmno(); BindClerk(); BindClsTeacher(); BindAppTeacher(); ;
     
       }
        private void BindClerk()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from employee_t ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["Class"] = "-Select-";
            //row["SEC_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClerk.DataSource = ds.Tables[0];
            cmbClerk.DisplayMember = "emp_name";
            cmbClerk.ValueMember = "emp_id";

        }
        private void BindClsTeacher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from employee_t ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbClsTecher.DataSource = ds.Tables[0];
            cmbClsTecher.DisplayMember = "emp_name";
            cmbClsTecher.ValueMember = "emp_id";
        }
    
        private void BindAppTeacher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from employee_t ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbAppTeacher.DataSource = ds.Tables[0];
            cmbAppTeacher.DisplayMember = "emp_name";
            cmbAppTeacher.ValueMember = "emp_id";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Stu_id == "0")
                {
                    MessageBox.Show("Student AdmNo Not Exist ....");
                    return;

                }
                else if (txtReason.Text == "")
                {
                    MessageBox.Show("Reason Can't Be Blank ....");
                    return;

                }
                else
                {
                    DateTime dtdob = Convert.ToDateTime(dtdate.Value, System.Globalization.CultureInfo.InvariantCulture);
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand cmd = new SqlCommand("insert_tblGatePass", sc);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sc.Open();
                    cmd.Parameters.AddWithValue("@gatepasNo", Convert.ToInt32(txtno.Text));
                    cmd.Parameters.AddWithValue("@AdmNo", cmbAdmNo.Text.Trim().Replace(" ",""));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@ReasonToLeave", txtReason.Text);
                    cmd.Parameters.AddWithValue("@DepartureTime", dtdob.ToString());
                    cmd.Parameters.AddWithValue("@ReceivedBy", txtrec.Text.Trim());
                    cmd.Parameters.AddWithValue("@Clerk", cmbClerk.SelectedValue);
                    cmd.Parameters.AddWithValue("@ClassTeacher", cmbClsTecher.SelectedValue);
                    cmd.Parameters.AddWithValue("@ApprovalTeacher", cmbAppTeacher.SelectedValue);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,A/C:- " + cmbAdmNo.Text.Trim() + " " + " Leave Requested by -  " + "  " + txtName.Text.Trim() + " Student received by-  " + "  " + txtrec.Text.Trim() + "  Please inform to school if you find any discrepancy. Thanks ";
                        DAL.smsclass SMS = new sms.DAL.smsclass();
                        SMS.SMS(msgtxt, 0, phone.Trim().Replace(" ", ""), txtName.Text);
                        DialogResult dialogResult = MessageBox.Show("Are You Sure To Print Gate Pass - No " + txtno.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            print();

                                                  
                        }
                         Cancil_form();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void bindata()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_studentReport where admno like '" + cmbAdmNo.Text.Trim().Replace(" ", "") + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["student_name"].ToString();
                cmbclass.Text = dt.Rows[0]["class"].ToString();
                cmbRoll.Text = dt.Rows[0]["roll"].ToString();
                Stu_id = dt.Rows[0]["stu_id"].ToString();
                cmbAdmNo.Text = dt.Rows[0]["admno"].ToString();
                cmbRoll.Enabled = false;
                cmbclass.Enabled = false;
                phone = dt.Rows[0]["phone"].ToString();
            }
            else
            {
                txtName.Text = "";
                cmbclass.Text = "";
                cmbRoll.Text = "";
                Stu_id = "0";
                cmbAdmNo.Text = "";

                cmbRoll.Enabled = false;
                cmbclass.Enabled = false;
            }
        
        }
        private void cmbAdmNo_Leave(object sender, EventArgs e)
        {
            bindata();
        }
        private void cmbRoll_Leave(object sender, EventArgs e)
        {
            bindata();
           
        }
        private void txtno_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from view_Gatepass where id like '" + txtno.Text.Trim().Replace(" ", "") + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                cmbclass.Text = dt.Rows[0]["class"].ToString();
                cmbRoll.Text = dt.Rows[0]["STC_ROLL_NO"].ToString();
                cmbAdmNo.Text = dt.Rows[0]["AdmNo"].ToString();
                txtReason.Text = dt.Rows[0]["ReasonToLeave"].ToString();
                txtrec.Text = dt.Rows[0]["ReceivedBy"].ToString();
                btnSave.Enabled = false;
                cmbClerk.Text = dt.Rows[0]["Clerk"].ToString();
                cmbClsTecher.Text = dt.Rows[0]["ClassTeacher"].ToString();
                cmbAppTeacher.Text = dt.Rows[0]["ApprovalTeacher"].ToString();

            }
            else
            {
                txtReason.Text = "";
                txtName.Text = "";
                cmbclass.Text = "";
                cmbRoll.Text = "";
                Stu_id = "0";
                cmbAdmNo.Text = "";
                btnSave.Enabled = true;
                txtrec.Text = "";
            }

        }

         private void listBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click_1(object sender, EventArgs e)
        {

        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            {
                string Name;
                if (txtName.Text == "-Select-" || txtName.Text == "" || txtName.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtName.Text.Trim().ToString();
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
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '/' + ds1.Tables[0].Rows[j][1].ToString() + '/' + ds1.Tables[0].Rows[j][2].ToString() + '/' + ds1.Tables[0].Rows[j][3].ToString());
                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = cmbAdmNo.Top + cmbAdmNo.Height;                    
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }

            }

        }

        private void listBox1_DoubleClick_1(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    cmbAdmNo.Text = "";
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    cmbAdmNo.SelectedText = str[0].ToString();
                    bindata(); 
                    listBox1.Visible = false;
                    
                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
            listBox1.Visible = false;
        }
        }
        }

    
