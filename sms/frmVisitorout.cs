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
    public partial class frmVisitorout : Form
    {
        string classname, Stu_id,phone;
        SqlDataReader dr;
        public frmVisitorout()
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
            SqlDataAdapter da = new SqlDataAdapter("select max(no) from tblvisitorPass ", sc);
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
        
       
        private void Cancil_form()
        {
            btnSave.Enabled = true;
            bindGrpupNo();
          
            txtName.Text = "";
            txtReason.Text = ""; txtrec.Text = "";
            dtdate.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           print();
           
       }
       private void print()
       {

           cryvisitorPass.gatepass = txtno.Text.Trim();
           cryvisitorPass obj = new cryvisitorPass();
               obj.ShowDialog();
           
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
            bindGrpupNo();  BindClerk(); BindClsTeacher(); BindAppTeacher();
            dtdate.Focus();
     
       }
        private void BindClerk()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from department_t ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);        
            cmbClerk.DataSource = ds.Tables[0];
            cmbClerk.DisplayMember = "dept_desc";
            cmbClerk.ValueMember = "dept_id";

        }
        private void BindClsTeacher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DESIGNATION_T where dept_id ='"+cmbClerk.SelectedValue+"' ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbClsTecher.DataSource = ds.Tables[0];
            cmbClsTecher.DisplayMember = "desg_desc";
            cmbClsTecher.ValueMember = "desg_id";
        }
    
        private void BindAppTeacher()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from employee_t where emp_desg_id='" + cmbClsTecher.SelectedValue+ "' ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmbAppTeacher.DataSource = ds.Tables[0];
            cmbAppTeacher.DisplayMember = "emp_name";
            cmbAppTeacher.ValueMember = "emp_id";
        }
        public static byte[] ImageToByteArray(Image img, PictureBox pictureBoxCompanyLogo)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (pictureBoxCompanyLogo.Image != null)
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime dtdob = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand cmd = new SqlCommand("insert_tblvisitorPassout", sc);
                cmd.CommandType = CommandType.StoredProcedure;
                sc.Open();
                cmd.Parameters.AddWithValue("@gatepasNo", Convert.ToInt32(txtno.Text));
                cmd.Parameters.AddWithValue("@outtime", dtdate.Text);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    //string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,A/C:- " + cmbAdmNo.Text.Trim() + " " + " Leave Requested by -  " + "  " + txtName.Text.Trim() + " Student received by-  " + "  " + txtrec.Text.Trim() + "  Please inform to school if you find any discrepancy. Thanks ";
                    //DAL.smsclass SMS = new sms.DAL.smsclass();
                    //SMS.SMS(msgtxt, 0, phone.Trim().Replace(" ", ""), txtName.Text);
                    //DialogResult dialogResult = MessageBox.Show("Are You Sure To Print Gate Pass - No " + txtno.Text + " !!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    print();


                    //}
                    Cancil_form();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }        
       
        private void cmbAdmNo_Leave(object sender, EventArgs e)
        {
            
        }
        private void cmbRoll_Leave(object sender, EventArgs e)
        {
            
           
        }
        private void txtno_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblvisitorPass where no like '" + txtno.Text.Trim().Replace(" ", "") + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtReason.Text = dt.Rows[0]["address"].ToString();
                textBox1.Text = dt.Rows[0]["mobileno"].ToString();
                

            }
            else
            {
                txtReason.Text = "";
                txtName.Text = "";
                btnSave.Enabled = true;
                txtrec.Text = "";
            }

        }

        private void button28_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication7.frmWebcam obj = new WindowsFormsApplication7.frmWebcam();
            obj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Choose Image";
                openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Image img = new Bitmap(openFileDialog1.FileName);
                    pictureBox1.Image = img;// resizeImage(img);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message.ToString());
            }
        }

        private void cmbClerk_Leave(object sender, EventArgs e)
        {
            BindClsTeacher();
        }

        private void cmbClsTecher_Leave(object sender, EventArgs e)
        {
            BindAppTeacher();
        }

        private void dtdate_Leave(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

    }
}