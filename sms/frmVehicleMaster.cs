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
using System.IO;
using System.Reflection;

namespace sms
{
    public partial class vehiclemaster : Form
    {
        BLclass bl = new BLclass();
        static byte[] bimage;
        string fileName;
        string name = "";
       
        public vehiclemaster()
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
            Cancel_Form();
         
        }
        private void UpperCase(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
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
        private void Cancel_Form()
        {
            rddeisel.Checked = true;
            rdown.Checked = true;
            pictureBox1.Image = GetDataToImage(Form1.cancel);
            btRegister.Enabled = true;
            btUpdate.Enabled = false;
            btDelete.Enabled = false;
            txtvehicleNo.Text = "";
            cmbvmodel.Text = "Select";
           cmbvmake.Text="Select";
            cmbvtype.Text = "Select";
            cmbdriver.Text = "Select";
            cmbvmodel.Text = "Select";
            txtvehicleid.Text = "";
            txtseatingcapacity.Text = "";
            txtchasis.Text = "";
            txtengine.Text = "";
            cmbyear.Text = "Select";

        }
        private void BindMake()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from VEHICLE_MAKE_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["VMK_DESC"] = "Select";
            row["VMK_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbvmake.DataSource = ds.Tables[0];
            cmbvmake.DisplayMember = "VMK_DESC";
            cmbvmake.ValueMember = "VMK_ID";
        }
        private void BindModel()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from VEHICLE_MODEL_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["VMO_DESC"] = "Select";
            row["VMO_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbvmodel.DataSource = ds.Tables[0];
            cmbvmodel.DisplayMember = "VMO_DESC";
            cmbvmodel.ValueMember = "VMO_ID";
        }
        private void BindType()
        {
             dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from VEHICLE_TYPE_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["VT_DESC"] = "Select";
            row["VT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbvtype.DataSource = ds.Tables[0];
            cmbvtype.DisplayMember = "VT_DESC";
            cmbvtype.ValueMember = "VT_ID";
        }

        private void BindDriver()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EMPLOYEE_T where emp_desg_id=3 order by EMP_ID ", sc);
            DataSet ds = new DataSet();   
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbdriver.DataSource = ds.Tables[0];
            cmbdriver.DisplayMember = "EMP_NAME";
            cmbdriver.ValueMember = "EMP_ID";
        }
  
        public void InitGrid()
        {
            LoadGrid();
            //dataGridView1.Columns["emp_id"].Visible = false;
            //dataGridView1.Columns["Name"].Width = 100;
            //dataGridView1.Columns["DateOfBirth"].Width = 60;
            //dataGridView1.Columns["Address"].Width = 90;
            //dataGridView1.Columns["City"].Width = 70;
            //dataGridView1.Columns["District"].Width = 70;
            //dataGridView1.Columns["State"].Width = 70;
            //dataGridView1.Columns["Country"].Width = 80;
            //dataGridView1.Columns["PinCode"].Width = 60;
            //dataGridView1.Columns["DateOfJoin"].Width = 60;
            //dataGridView1.Columns["Designation"].Width = 90;
            //dataGridView1.Columns["Salary"].Width = 70;
            //dataGridView1.Columns["Commission"].Width = 70;
            //dataGridView1.Columns["AcademicQualification"].Width = 100;
            //dataGridView1.Columns["ProfessionalQualification"].Width = 100;
            //dataGridView1.Columns["Mobile#"].Width = 60;
            //dataGridView1.Columns["Phone#"].Width = 60;
            //dataGridView1.Columns["EMail"].Width = 60;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
           
           // SqlDataAdapter da = new SqlDataAdapter(" select emp_id,[Name],Address,DateOfBirth,City,State,Father,Department,Designation,PinCode,DateOfJoin,Salary,Commission,Academic as AcademicQualification,Professional as  ProfessionalQualification,Mobile as Mobile#,Phone as Phone#, EMail from tblEmployee", sc);
            SqlDataAdapter da = new SqlDataAdapter("select * from View_ShowVehicle order by VEHICLE_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btRegister_Click(object sender, EventArgs e)
        {
            try
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                if (txtvehicleNo.Text == "")
                {
                    MsgBox.ShowBox("Please Fill Vehicle No!!!");
                    return;
                }
                if (cmbvtype.Text == "" || cmbvtype.Text == "Select")
                {
                    MsgBox.ShowBox("Please Fill Vehicle Type Field!!!");
                    return;
                }
                if (cmbvmake.Text == "")
                {
                    MsgBox.ShowBox("Please Fill Vehicle Make Field!!!");
                    return;
                }
                if (cmbvmodel.Text == "" || cmbvmodel.Text == "Select")
                {
                    MsgBox.ShowBox("Please Fill Vehicle Model Field!!!");
                    return;
                }
                if (cmbdriver.Text == "" || cmbdriver.Text == "Select")
                {
                    MsgBox.ShowBox("Please Fill Vehicle Driver Field!!!");
                    return;

                }
                if (cmbvtype.Text == "" || cmbvtype.Text == "Select")
                {
                    MsgBox.ShowBox("Please Fill Vehicle Type Field!!!");
                    return;
                }
               

            
                ArrayList arr = new ArrayList();
                arr.Add(txtvehicleNo.Text);
                arr.Add(cmbyear.Text);
                arr.Add(Convert.ToInt32(cmbvtype.SelectedValue));
                arr.Add(Convert.ToInt32(cmbvmake.SelectedValue));
                arr.Add(Convert.ToInt32(cmbvmodel.SelectedValue));
                arr.Add(Convert.ToInt32(cmbdriver.SelectedValue));
                arr.Add(Convert.ToInt32(txtseatingcapacity.Text));
                arr.Add(txtchasis.Text);
                arr.Add(txtengine.Text);
                int fueltype = 1;
                int Type = 1;

                if (rdpetrol.Checked == true)
                    fueltype = 2;
                else
                    if (rdlpg.Checked == true)
                        fueltype = 3;
                    else
                        fueltype = 1;

                if (rdrent.Checked == true)
                    Type = 2;
                else
                    Type = 1;

                arr.Add(fueltype);
                arr.Add(Type);
               

                byte[] byteImg = ImageToByteArray(pictureBox1.Image, pictureBox1);
                arr.Add(byteImg);
                arr.Add(textBox2.Text);
                arr.Add(textBox3.Text);
                arr.Add(textBox1.Text);
                arr.Add(textBox4.Text);

                int i = dc.InsertRows("sp_insert_Vehicle", arr);
                if (i > 0)
                {
                    MsgBox.ShowBox("Vehicle Registered !!!");
                }
                else
                {
                    MsgBox.ShowBox("Vehicle Already Registered !!!");
                }

                InitGrid();
                Cancel_Form();
            }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.ToString());
            }
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {

            
            if (dataGridView1.Rows.Count > 0)
            {
                txtvehicleid.Text = dataGridView1.CurrentRow.Cells["VEHICLE_ID"].Value.ToString();
                txtvehicleNo.Text = dataGridView1.CurrentRow.Cells["VEHICLE_NO"].Value.ToString();
                cmbyear.Text = dataGridView1.CurrentRow.Cells["VEHICLE_MAKE_YEAR"].Value.ToString();
                cmbvtype.Text = dataGridView1.CurrentRow.Cells["VT_DESC"].Value.ToString();
                cmbvmake.Text = dataGridView1.CurrentRow.Cells["VMK_DESC"].Value.ToString();
                cmbvmodel.Text = dataGridView1.CurrentRow.Cells["VMO_DESC"].Value.ToString();
                cmbdriver.Text = dataGridView1.CurrentRow.Cells["EMP_NAME"].Value.ToString();
                txtengine.Text = dataGridView1.CurrentRow.Cells["VEHICLE_ENGINE_NO"].Value.ToString();
                txtchasis.Text = dataGridView1.CurrentRow.Cells["VEHICLE_CHASSIS_NO"].Value.ToString();

                //textBox2.Text = dataGridView1.CurrentRow.Cells["VEHICLE_incharge"].Value.ToString();
                //textBox3.Text = dataGridView1.CurrentRow.Cells["VEHICLE_incharge_mob"].Value.ToString();
                //textBox1.Text = dataGridView1.CurrentRow.Cells["VEHICLE_conductor"].Value.ToString();
                //textBox4.Text = dataGridView1.CurrentRow.Cells["VEHICLE_conductor_mob"].Value.ToString();


                txtseatingcapacity.Text = dataGridView1.CurrentRow.Cells["VEHICLE_LOAD_CAPACITY"].Value.ToString();
                if (dataGridView1.CurrentRow.Cells["VEHICLE_FUEL"].Value.ToString() == "2")
                    rdpetrol.Checked = true;
                else
                    if (dataGridView1.CurrentRow.Cells["VEHICLE_FUEL"].Value.ToString() == "3")
                        rdlpg.Checked = true;
                    else
                        rddeisel.Checked = true;
                if (dataGridView1.CurrentRow.Cells["VEHICLE_OWN_RENTAL"].Value.ToString() == "2")
                    rdrent.Checked = true;
                else
                    rdown.Checked = true;
                 
                     byte[] img_arr1 = new byte[0];
                     img_arr1 = (byte[])dataGridView1.CurrentRow.Cells["VEHICLE_PHOTO_PATH"].Value;
                    pictureBox1.Image = GetDataToImage(img_arr1);

                btRegister.Enabled = false;
                btUpdate.Enabled = true;
                btDelete.Enabled = true;

            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                ArrayList arr = new ArrayList();
                arr.Add(txtvehicleNo.Text);
                arr.Add(cmbyear.Text);
                arr.Add(Convert.ToInt32(cmbvtype.SelectedValue));
                arr.Add(Convert.ToInt32(cmbvmake.SelectedValue));
                arr.Add(Convert.ToInt32(cmbvmodel.SelectedValue));
                arr.Add(Convert.ToInt32(cmbdriver.SelectedValue));
                arr.Add(Convert.ToInt32(txtseatingcapacity.Text));
                arr.Add(txtchasis.Text);
                arr.Add(txtengine.Text);
                int fueltype = 1;
                int Type = 1;

                if (rdpetrol.Checked == true)
                    fueltype = 2;
                else
                    if (rdlpg.Checked == true)
                        fueltype = 3;
                    else
                        fueltype = 1;

                if (rdrent.Checked == true)
                    Type = 2;
                else
                    Type = 1;

                arr.Add(fueltype);
                arr.Add(Type);
                byte[] byteImg = ImageToByteArray(pictureBox1.Image, pictureBox1);
                arr.Add(byteImg);
                arr.Add(textBox2.Text);
                arr.Add(textBox3.Text);
                arr.Add(textBox1.Text);
                arr.Add(textBox4.Text);

                int i = dc.InsertRows("sp_Update_Vehicle", arr);
                if (i > 0)
                {
                    MsgBox.ShowBox("Vehicle Updated !!!");
                }
                else
                {
                    MsgBox.ShowBox("Vehicle Not Updated !!!");
                }

                LoadGrid();
                Cancel_Form();
         
            }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.ToString());
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

        private void vehiclemaster_Load(object sender, EventArgs e)
        {
            for (int i = 2000; i < 2022; i++)
            {

                cmbyear.Items.Add(i);
            }

            BindDriver();
            BindMake(); BindModel(); BindType(); BindModel(); LoadGrid();
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

        private void btDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Vehicle!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtvehicleid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM VEHICLE_T WHERE VEHICLE_ID=" + txtvehicleid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Vehicle Deleted Successfully!!!");
                        Cancel_Form();
                        InitGrid();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       

      
     
    
  

      




      

      

       
      
       



    }
}