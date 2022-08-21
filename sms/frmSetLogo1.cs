using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;
using System.Collections;
using sms.DATALAYER;
using System.Reflection;
using System.IO;

namespace sms
{
    public partial class frmSetLogo1 : Form
    {
        string fileName;
        string name = "";
        BLclass bl = new BLclass();
        //MsgBox msg = new MsgBox(Color.Red, Color.WhiteSmoke, Color.WhiteSmoke, Color.Red, Color.Red, Color.WhiteSmoke, FlatStyle.Flat);
        //MsgBoxRetType mg = new MsgBoxRetType(Color.Red, Color.WhiteSmoke, Color.WhiteSmoke, Color.Red, Color.Red, Color.WhiteSmoke, FlatStyle.Flat);
        int tmpCount;
        public frmSetLogo1()
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
            Binddept();
            
            BiindState();
            BindCity();
            Cancel_Form();
          LoadGrid();
            tmpCount = 0;
        }
        private void UpperCase(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }
        private void Cancel_Form()
        {
            pictureBox1.Image = GetDataToImage(Form1.cancel);
            name = ""; fileName = "";
            btRegister.Enabled = true;
            btUpdate.Enabled = true;
            btDelete.Enabled = true;
            txtName.Text = "";
            txtAddress.Text = "";
            cmbCity.Text = "Select";
            txtfather.Text = "";
            cmbDept.Text="Select";
            cmbDesignation.Text = "Select";
            cmbState.Text = "Select";
            txtPinCode.Text = "";
            txtfather.Text = "";
            txtSalary.Text = "";                   
            txtMobile.Text = "";
            txtPhone.Text = "";        
            txtSalary.Text = "0";
            txtnotice.Text = "";
            txtpanno.Text = "";
            txtpfno.Text = "";
             txtesino.Text = "";
             txtaccno.Text = "";
             txtbank.Text = "";
            
            LoadGrid();
        }
        private void Binddept()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DEPARTMENT_T order by DEPT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DEPT_DESC"] = "Select";
            row["DEPT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbDept.DataSource = ds.Tables[0];
            cmbDept.DisplayMember = "DEPT_DESC";
            cmbDept.ValueMember = "DEPT_ID";
        }
        private void BindDesign()
        {
            string str;
            if (cmbDept.Text == "" || cmbDept.Text == "-Select-" || cmbDept.Text == "System.Data.DataRowView")
                str = "0";
            else
                str = cmbDept.SelectedValue.ToString();

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from DESIGNATION_T where DEPT_ID='" +str + "'", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["DESG_DESC"] = "Select";
            row["DESG_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
             cmbDesignation.DataSource = ds.Tables[0];
             cmbDesignation.DisplayMember = "DESG_DESC";
             cmbDesignation.ValueMember = "DESG_ID";
        }
        private void BiindState()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from STATE_T order by STATE_NAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STATE_NAME"] = "Select";
            row["STATE_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbState.DataSource = ds.Tables[0];
            cmbState.DisplayMember = "STATE_NAME";
            cmbState.ValueMember = "STATE_ID"; 
        }
        private void BindCity()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CITY_T order by CITY_NAME ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CITY_NAME"] = "Select";
            row["CITY_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbCity.DataSource = ds.Tables[0];
            cmbCity.DisplayMember = "CITY_NAME";
            cmbCity.ValueMember = "CITY_ID";
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["emp_id"].Visible = false;
            dataGridView1.Columns["EMP_NAME"].Width = 100;
            dataGridView1.Columns["EMP_DOB"].Width = 60;
            dataGridView1.Columns["EMP_DOJ"].Width = 60;
            dataGridView1.Columns["EMP_ADDRESS"].Width = 90;
            dataGridView1.Columns["CITY_NAME"].Width = 70;
            dataGridView1.Columns["STATE_NAME"].Width = 70;
            dataGridView1.Columns["EMP_ZIP"].Width = 60;
            dataGridView1.Columns["DEPT_DESC"].Width = 90;
            dataGridView1.Columns["DESG_DESC"].Width = 90;
            dataGridView1.Columns["EMP_BASIC_SALARY"].Width = 70;
            dataGridView1.Columns["EMP_MOBILE"].Width = 60;
            dataGridView1.Columns["EMP_PHONE"].Width = 60;
            dataGridView1.Columns["EMP_ACC_NO"].Width = 60;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
           
           // SqlDataAdapter da = new SqlDataAdapter(" select emp_id,[Name],Address,DateOfBirth,City,State,Father,Department,Designation,PinCode,DateOfJoin,Salary,Commission,Academic as AcademicQualification,Professional as  ProfessionalQualification,Mobile as Mobile#,Phone as Phone#, EMail from tblEmployee", sc);
            SqlDataAdapter da = new SqlDataAdapter("select * from View_tblemployee where EMP_NAME like '"+txtName.Text+"%' order by EMP_ID desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btRegister_Click(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (txtName.Text == "")
            {
                MsgBox.ShowBox("Please Fill Employee Name!!!");
                return;
            }
            else if (cmbDept.Text == "" || cmbDept.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Department Field!!!");
                return;
            }
            else if (cmbDesignation.Text == "" || cmbDesignation.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Designation Field!!!");
                return;
            }
            else if (txtMobile.Text == "")
            {
                MsgBox.ShowBox("Please Fill Mobile No Field!!!");
                return;
            }
            else if (txtSalary.Text == "")
            {
                MsgBox.ShowBox("Please Fill Salary Field!!!");
                return;
            }
            else
            {
                try
                {
                    ArrayList arr = new ArrayList();
                    arr.Add(txtName.Text);
                    if(radiofemale.Checked)
                          arr.Add("F");
                    else
                        arr.Add("M");
                    if (rbmarried.Checked)
                        arr.Add("M");
                    else
                        arr.Add("U");
                    arr.Add(dtBirth.Text);
                    arr.Add(dtJoin.Text);
                    arr.Add(txtAddress.Text);
                    arr.Add(Convert.ToInt16(cmbCity.SelectedValue));
                    arr.Add(Convert.ToInt16(cmbState.SelectedValue));
                    arr.Add(txtPinCode.Text);
                    arr.Add(txtPhone.Text);
                    arr.Add(txtMobile.Text);
                    arr.Add(Convert.ToInt32(cmbDept.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbDesignation.SelectedValue));
                    arr.Add(Convert.ToDecimal(txtSalary.Text));
                    byte[] byteImg = ImageToByteArray(pictureBox1.Image, pictureBox1);
                    arr.Add(byteImg);
                    //arr.Add(txtSalary.Text);
                    arr.Add(txtfather.Text);
                    arr.Add(DOL.Text);
                    arr.Add(txtnotice.Text);
                    arr.Add(txtpfno.Text);
                    arr.Add(txtesino.Text);
                    arr.Add(txtaccno.Text);
                    arr.Add(txtbank.Text);
                    arr.Add(txtpanno.Text);
                     int i = dc.InsertRows("sp_insert_Employee", arr);
                    if(i>0)
                         MsgBox.ShowBox("Employee Registered !!!");
                    else
                        MsgBox.ShowBox("Employee Already Registered !!!");
                    InitGrid();
                    Cancel_Form();
                }
                catch (Exception ex)
                {
                   // throw ex;
                }
            }
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
        public Image GetDataToImage(byte[] pData)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(pData) as Image;
            }
            catch (Exception ex)
            {
                //MsgBox.Show(ex.Message, "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                return null;
            }
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
                       
            if (dataGridView1.Rows.Count > 0)
            {
              
                txtEmpId.Text = dataGridView1.CurrentRow.Cells["EMP_ID"].Value.ToString();
                txtName.Text = dataGridView1.CurrentRow.Cells["EMP_NAME"].Value.ToString();
                txtAddress.Text = dataGridView1.CurrentRow.Cells["EMP_ADDRESS"].Value.ToString();
                dtBirth.Text = dataGridView1.CurrentRow.Cells["EMP_DOB"].Value.ToString();
                 DOL.Text = dataGridView1.CurrentRow.Cells["EMP_DOL"].Value.ToString();
                cmbCity.Text = dataGridView1.CurrentRow.Cells["CITY_NAME"].Value.ToString();
                cmbState.Text = dataGridView1.CurrentRow.Cells["STATE_NAME"].Value.ToString();
                 listBox1.Visible = false;
                txtfather.Text = dataGridView1.CurrentRow.Cells["EMP_RELATIVE_NAME"].Value.ToString();
                cmbDept.Text = dataGridView1.CurrentRow.Cells["DEPT_DESC"].Value.ToString();
                cmbDesignation.Text = dataGridView1.CurrentRow.Cells["DESG_DESC"].Value.ToString();
                txtPinCode.Text = dataGridView1.CurrentRow.Cells["EMP_ZIP"].Value.ToString();
                dtJoin.Text = dataGridView1.CurrentRow.Cells["EMP_DOJ"].Value.ToString();
                txtSalary.Text = dataGridView1.CurrentRow.Cells["EMP_BASIC_SALARY"].Value.ToString();
                txtMobile.Text = dataGridView1.CurrentRow.Cells["EMP_MOBILE"].Value.ToString();
                txtPhone.Text = dataGridView1.CurrentRow.Cells["EMP_PHONE"].Value.ToString();
                txtnotice.Text = dataGridView1.CurrentRow.Cells["EMP_NOTICE_PERIOD"].Value.ToString();
                txtaccno.Text = dataGridView1.CurrentRow.Cells["EMP_ACC_NO"].Value.ToString();
                txtbank.Text = dataGridView1.CurrentRow.Cells["EMP_BANK_NAME"].Value.ToString();
                txtpfno.Text = dataGridView1.CurrentRow.Cells["EMP_PF_NO"].Value.ToString();
                txtesino.Text = dataGridView1.CurrentRow.Cells["EMP_ESI_NO"].Value.ToString();
                txtpanno.Text = dataGridView1.CurrentRow.Cells["EMP_PAN_NO"].Value.ToString();

               
                if (dataGridView1.CurrentRow.Cells["EMP_SEX"].Value.ToString() == "F")
                    radiofemale.Checked = true;
                else
                    radiomale.Checked = true;

                if (dataGridView1.CurrentRow.Cells["EMP_MARITAL_STATUS"].Value.ToString() == "U")
                    rbUnMarried.Checked = true;
                else
                    rbmarried.Checked = true;
                btRegister.Enabled = false;
                btUpdate.Enabled = true;
                btDelete.Enabled = true;
                     byte[] img_arr1 = new byte[0];
                     img_arr1 = (byte[])dataGridView1.CurrentRow.Cells["EMP_PHOTO"].Value;
                    pictureBox1.Image = GetDataToImage(img_arr1);
                    
                  

            }
        }
        private void btUpdate_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    ArrayList arr = new ArrayList();
                   
                    byte[] byteImg = ImageToByteArray(pictureBox1.Image, pictureBox1);
                    arr.Add(byteImg);

                    int i = dc.InsertRows("Update_Logo1", arr);
                    MsgBox.ShowBox("Logo Updated !!!");
                    InitGrid();
                    Cancel_Form();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Cancel_Form();
        }
  
        private void EmployeeRegistration_Load(object sender, EventArgs e)
        {
      
            LoadGrid();
        }       

        private void btDelete_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtEmpId.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM EMPLOYEE_T WHERE emp_id=" + txtEmpId.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Employee Deleted Successfully!!!");
                        Cancel_Form();
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
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

        private void cmbState_Leave(object sender, EventArgs e)
        {
            BindCity();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDesign();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void radiomale_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}