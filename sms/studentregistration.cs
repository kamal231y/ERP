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
using System.Collections;
using System.IO;
using System.Reflection;
namespace sms
{
    public partial class studentregistration : Form
    {
        public static string admno;
        
                public static string SName;
        public static string SMobioleNo;
        
       
        BLclass bl = new BLclass();
        validation v1 = new validation();

        string fileName;
        string name = ""; string father = ""; string mother = "";
        byte[] big;
        public studentregistration()
        {
            InitializeComponent();

            cancel_form();
           
        }

      

        private void studentregistration_Load(object sender, EventArgs e)
        {
            txtincome.Text = "0.0";
            // txtappno.Text = dt.Rows[dt.Rows.Count - 1]["Admission_no"].ToString();
            bindsiblingclass();
            bindAdm();
            bindHouse();
            bindCurrentclass();
            BindRoute();
            bindstate();
            bindheight();
            bindreligion();
            bindvisionl();
            bindvisionr();
            bindfoccupation();
            bindmoccupation();
            bindfqualification();
            bindmqualification();
            bindteeth();
            bindweight();
            bindbloodgroup();
            bindstate1();
            bindCategory();
            bindHouse();
            bindheigine();
            bindclass();
            bindSiblingAdm();
            if (SName != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from tblStudentFormRegistration where Formnoid="+SName+"", sc);
                DataTable dt = new DataTable(); da.Fill(dt);
                if (dt.Rows.Count>0)
                {
                    txtfname.Text = dt.Rows[0]["StudentName"].ToString();
                    txtfathername.Text = dt.Rows[0]["FatherName"].ToString();
                    txtmothername.Text = dt.Rows[0]["MotherName"].ToString();
                    txtphone1.Text = dt.Rows[0]["Mobileno"].ToString();
                    txtaddress.Text = dt.Rows[0]["Address"].ToString();
                    dob.Text = dt.Rows[0]["DOB"].ToString();
                    cmbclass.SelectedValue =Convert.ToInt32( dt.Rows[0]["Classid"].ToString());
                    cmbcaste.SelectedValue = Convert.ToInt32( dt.Rows[0]["Classid"].ToString());
                    if (dt.Rows[0]["Gender"].ToString() == "Male")
                        radiomale.Checked = true;
                    else
                        radiofemale.Checked = true;
                }
            }
            if (admno != null)
            {
                binddata(admno);
            }
        }
        private void BindRoute()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from BUS_STOP_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STOP_DESC"] = "Select";
            row["STOP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbtransport.DataSource = ds.Tables[0];
            cmbtransport.DisplayMember = "STOP_DESC";
            cmbtransport.ValueMember = "STOP_ID";
        }
        private void bindAdm()
        {
          
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Bind_AdmNo ", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                 txtappno.Text = dt.Rows[0][0].ToString();
             
                          
        }




        private void bindstate1()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from STATE_T order by STATE_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STATE_NAME"] = "Select";
            row["STATE_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbstate.DataSource = ds.Tables[0];
            cmbstate.DisplayMember = "STATE_NAME";
            cmbstate.ValueMember = "STATE_ID";

        }
        private void bindstate()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from STATE_T order by STATE_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STATE_NAME"] = "Select";
            row["STATE_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbmstate.DataSource = ds.Tables[0];
            cmbmstate.DisplayMember = "STATE_NAME";
            cmbmstate.ValueMember = "STATE_ID";
        }
        private void bindHouse()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from HOUSE_T order by HOUSE_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["HOUSE_DESC"] = "Select";
            row["HOUSE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbHouse.DataSource = dt;
            cmbHouse.DisplayMember = "HOUSE_DESC";
            cmbHouse.ValueMember = "HOUSE_ID";

        }
        private void bindSiblingAdm()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from STUDENT_T order by STU_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["STU_ADM_NO"] = "Select";
            row["STU_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbsibling.DataSource = dt;
            cmbsibling.DisplayMember = "STU_ADM_NO";
            cmbsibling.ValueMember = "STU_ID";

        }
        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CASTE_T order by CASTE_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CASTE_DESC"] = "Select";
            row["CASTE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcategory.DataSource = dt;
            cmbcategory.DisplayMember = "CASTE_DESC";
            cmbcategory.ValueMember = "CASTE_ID";

        }
        private void bindreligion()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from RELIGION_T order by RLG_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["RLG_DESC"] = "Select";
            row["RLG_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbreligion.DataSource = dt;
            cmbreligion.DisplayMember = "RLG_DESC";
            cmbreligion.ValueMember = "RLG_ID";

        }
        private void bindheight()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from HEIGHT_T order by HEIGHT_ID", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["HEIGHT_DESC"] = "Select";
            row["HEIGHT_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbheight.DataSource = dt;
            cmbheight.DisplayMember = "HEIGHT_DESC";
            cmbheight.ValueMember = "HEIGHT_ID";

        }
        private void bindweight()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from WEIGHT_T order by WEIGHT_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["WEIGHT_DESC"] = "Select";
            row["WEIGHT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbweight.DataSource = ds.Tables[0];
            cmbweight.DisplayMember = "WEIGHT_DESC";
            cmbweight.ValueMember = "WEIGHT_ID";

        }
        private void bindteeth()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from TEETH_T order by TEETH_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["TEETH_DESC"] = "Select";
            row["TEETH_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbteeth.DataSource = ds.Tables[0];
            cmbteeth.DisplayMember = "TEETH_DESC";
            cmbteeth.ValueMember = "TEETH_ID";

        }
        private void bindbloodgroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from BG_T order by BG_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["BG_DESC"] = "Select";
            row["BG_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbbgroup.DataSource = ds.Tables[0];
            cmbbgroup.DisplayMember = "BG_DESC";
            cmbbgroup.ValueMember = "BG_ID";

        }
        private void bindvisionl()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from VISION_T order by VISION_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["VISION_DESC"] = "Select";
            row["VISION_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmblvision.DataSource = ds.Tables[0];
            cmblvision.DisplayMember = "VISION_DESC";
            cmblvision.ValueMember = "VISION_ID";


        }
        private void bindvisionr()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from VISION_T order by VISION_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["VISION_DESC"] = "Select";
            row["VISION_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbrvision.DataSource = ds.Tables[0];
            cmbrvision.DisplayMember = "VISION_DESC";
            cmbrvision.ValueMember = "VISION_ID";

        }
        private void bindfoccupation()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from OCCUPATION_T order by OCP_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["OCP_DESC"] = "Select";
            row["OCP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbfoccupation.DataSource = ds.Tables[0];
            cmbfoccupation.DisplayMember = "OCP_DESC";
            cmbfoccupation.ValueMember = "OCP_ID";
            cmbmoccupation.DataSource = ds.Tables[0];
            cmbmoccupation.DisplayMember = "OCP_DESC";
            cmbmoccupation.ValueMember = "OCP_ID";

        }
        private void bindmoccupation()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from OCCUPATION_T order by OCP_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["OCP_DESC"] = "Select";
            row["OCP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbmoccupation.DataSource = ds.Tables[0];
            cmbmoccupation.DisplayMember = "OCP_DESC";
            cmbmoccupation.ValueMember = "OCP_ID";

        }
        private void bindfqualification()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from QUALIFICATION_T order by QUL_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["QUL_DESC"] = "Select";
            row["QUL_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbfqualification.DataSource = ds.Tables[0];
            cmbfqualification.DisplayMember = "QUL_DESC";
            cmbfqualification.ValueMember = "QUL_ID";

        }
        private void bindmqualification()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from QUALIFICATION_T order by QUL_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["QUL_DESC"] = "Select";
            row["QUL_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbmqualification.DataSource = ds.Tables[0];
            cmbmqualification.DisplayMember = "QUL_DESC";
            cmbmqualification.ValueMember = "QUL_ID";

        }
        private void bindheigine()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from OH_T order by OH_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["OH_DESC"] = "Select";
            row["OH_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmboralheigine.DataSource = ds.Tables[0];
            cmboralheigine.DisplayMember = "OH_DESC";
            cmboralheigine.ValueMember = "OH_ID";

        }
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";

        }
        private void bindsiblingclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsiblingclass.DataSource = ds.Tables[0];
            cmbsiblingclass.DisplayMember = "Class";
            cmbsiblingclass.ValueMember = "SEC_ID";

        }

        private void bindCurrentclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbcaste.DataSource = ds.Tables[0];
            cmbcaste.DisplayMember = "Class";
            cmbcaste.ValueMember = "SEC_ID";

        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxsaveasmailing.Checked == true)
            {
                txtmaddress.Text = txtaddress.Text;
                cmbmstate.Text = cmbstate.Text;
                cmbmcity.Text = cmbcity.Text;
                txtmzip.Text = txtzip.Text;
            }
           
        }


        private void cmbstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StateName;
            if (cmbstate.Text == "Select" || cmbstate.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbstate.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from CITY_T ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["CITY_NAME"] = "Select";
            row["CITY_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcity.DataSource = dt;
            cmbcity.DisplayMember = "CITY_NAME";
            cmbcity.ValueMember = "CITY_ID";
        }

        private void cmbmstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string StateName;
            if (cmbmstate.Text == "Select" || cmbmstate.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmbmstate.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from CITY_T ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["CITY_NAME"] = "Select";
            row["CITY_ID"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbmcity.DataSource = dt;
            cmbmcity.DisplayMember = "CITY_NAME";
            cmbmcity.ValueMember = "CITY_ID";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admno = null;
            this.Close();
        }

        private void Btn_create_Click(object sender, EventArgs e)
        {

            if (txtfname.Text == "")
            {
                MessageBox.Show("Please Enter FirstName");
                return;
            }
            else if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                MessageBox.Show("Please Select Admission Class ");
                return;
            }
            else if (cmbcaste.Text == "" || cmbcaste.Text == "Select")
            {
                MessageBox.Show("Please Select Current Class ");
                return;
            }
            else if (txtphone1.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No");
                    return;
                }
            else if (chktransport.Checked == true)
            {
                if (cmbtransport.Text == "" || cmbtransport.Text == "Select")
                {
                    MessageBox.Show("Please Select Transport");
                    return;
                }
            }

            //else
            //{
                string sex;
                string str = "N";
                if (radiomale.Checked == true)
                {
                    sex = "M";
                }
                else
                {
                    sex = "F";
                }
                string type1; string type2;
                if (radioscholor.Checked == true)
                {
                    type1 = "N";
                }
                else
                {
                    type1 = "H";
                }
                if (radionormal.Checked == true)
                {
                    type2 = "N";
                }
                else
                {
                    type2 = "S";
                }

                DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                SqlConnection sc = dc.con();

                ArrayList arr = new ArrayList();
                arr.Add(txtappno.Text);
                arr.Add(dateofadm.Text);
                arr.Add(txtfname.Text);
                arr.Add(txtmname.Text);
                arr.Add(txtlname.Text);
                arr.Add(dob.Text);
                arr.Add(sex);
                arr.Add(Convert.ToInt32(cmbclass.SelectedValue));
                arr.Add(Convert.ToInt32(cmbreligion.SelectedValue));
                arr.Add(Convert.ToInt32(cmbcategory.SelectedValue));
                arr.Add(Convert.ToDouble(txtincome.Text));
                arr.Add(txtphone1.Text);
                arr.Add(txtaddress.Text);
                arr.Add(Convert.ToInt32(cmbstate.SelectedValue));
                arr.Add(Convert.ToInt32(cmbcity.SelectedValue));
                arr.Add(txtzip.Text);
                arr.Add(txtmaddress.Text);
                arr.Add(Convert.ToInt32(cmbmstate.SelectedValue));
                arr.Add(Convert.ToInt32(cmbmcity.SelectedValue));
                arr.Add(txtmzip.Text);
                arr.Add(txtphone.Text);
                arr.Add(type1);
                arr.Add(type2);
                arr.Add(txtfathername.Text);
                arr.Add(Convert.ToInt32(cmbfoccupation.SelectedValue));
                arr.Add(Convert.ToInt32(cmbfqualification.SelectedValue));
                arr.Add(txtmothername.Text);
                arr.Add(Convert.ToInt32(cmbmoccupation.SelectedValue));
                arr.Add(Convert.ToInt32(cmbmqualification.SelectedValue));
                arr.Add(Convert.ToInt32(cmbheight.SelectedValue));
                arr.Add(Convert.ToInt32(cmbweight.SelectedValue));
                arr.Add(Convert.ToInt32(cmbteeth.SelectedValue));
                arr.Add(Convert.ToInt32(cmbbgroup.SelectedValue));
                arr.Add(Convert.ToInt32(cmblvision.SelectedValue));
                arr.Add(Convert.ToInt32(cmbrvision.SelectedValue));
                arr.Add(Convert.ToInt32(cmboralheigine.SelectedValue));
                // arr.Add(txtspecificailment.Text);
                //  arr.Add(bimage);
                arr.Add(Convert.ToInt32(cmbsibling.SelectedValue));
                arr.Add("Y");
                if (chkbirth.Checked == true)
                    arr.Add("1");
                else
                    arr.Add("0");

                if (chktc.Checked == true)
                    arr.Add("2");
                else
                    arr.Add("0");
                if (chkmarksheet.Checked == true)
                    arr.Add("3");
                else
                    arr.Add("0");
                if (chkcharacter.Checked == true)
                    arr.Add("4");
                else
                    arr.Add("0");
                if (chkaffidavit.Checked == true)
                    arr.Add("5");
                else
                    arr.Add("0");
                if (chkadmission.Checked == true)
                    arr.Add("6");
                else
                    arr.Add("0");
                if (chkmigration.Checked == true)
                    arr.Add("7");
                else
                    arr.Add("0");
                if (chkstu.Checked == true)
                    arr.Add("8");
                else
                    arr.Add("0");
                if (chkparent.Checked == true)
                    arr.Add("9");
                else
                    arr.Add("0");
                if (chkother.Checked == true)
                    arr.Add("10");
                else
                    arr.Add("0");
                if (chkfamily.Checked == true)
                    arr.Add("11");
                else
                    arr.Add("0");

                arr.Add(txtfamilyinfo.Text);
                if (txtAccno.Text != "")
                    arr.Add(Convert.ToInt32(txtAccno.Text));
                else
                    arr.Add("0");
                arr.Add(Convert.ToInt32(cmbcaste.SelectedValue));
                arr.Add(Convert.ToInt32(cmbHouse.SelectedValue));
                if (name == "")
                    arr.Add("Noimage.gif");
                else
                    arr.Add(name);
                if (father == "")
                    arr.Add("Noimage.gif");
                else
                    arr.Add(father);
                if (mother == "")
                    arr.Add("Noimage.gif");
                else
                    arr.Add(mother);
                arr.Add(Convert.ToInt32(cmbtransport.SelectedValue));

                // if(chktransport.Checked)
                //arr.Add("1");
                // else
                //     arr.Add("0");

                //arr.Add();
                int n = dc.InsertRows("Insert_student_registration", arr);
                if (n > 0)
                {
                    MsgBox.ShowBox("Student is Registered ");
                    cancel_form();
                }
                else
                {
                    MsgBox.ShowBox("Student Not Registered Pls Fill Valid Phone No.!!!");
                    return;
                }
            //}
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                SqlConnection sc = dc.con();
                ArrayList arr = new ArrayList();
                   arr.Add(txtappno.Text);
                    //if (chktransport.Checked)
                    //    arr.Add("1");
                    //else
                    //    arr.Add("0");

                    //arr.Add();
                    int i = dc.InsertRows("Delete_student_registration", arr);
                    if (i > 0)
                    {     
                                               
              
                   MsgBox.ShowBox("Admission no. Deleted !!!");
                   
                    cancel_form();
                    txtappno.Text = "";
                }
                else
                {
                    MsgBox.ShowBox("No Deletion ,pls Enter Valid Admission no. !!!");
                    return;
                }
                sc.Close();
            }

            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }


        private void txtincome_KeyPress(object sender, KeyPressEventArgs e)
        {
            validation v1 = new validation();
            v1.digitonly(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel_form();
          
        }
        private void cancel_form()
        {
           
            Btn_create.Enabled = true;
            btnupdate.Enabled = false;
            button4.Enabled = false;
            txtappno.Text = "";
            //string imagepath = @" & MapPath & \StudentImage\" + "Noimage.gif";
            //DirectoryInfo path = Server.MapPath("~\StudentImage\" + "Noimage.gif");

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            var logoimage = Path.Combine(outPutDirectory, "StudentImage\\Noimage.gif");

            string imagepath = new Uri(logoimage).LocalPath;

          // var logoImage = new LinkedResource(relLogo)

          //  pictureBox1.Image = System.Drawing.Image.FromFile(imagepath);
            pictureBox1.Image = System.Drawing.Image.FromFile(imagepath);
            pictureBox2.Image = System.Drawing.Image.FromFile(imagepath);
            pictureBox3.Image = System.Drawing.Image.FromFile(imagepath);
            dateofadm.Text = DateTime.Now.ToString();
            dob.Text = DateTime.Now.ToString();

            cmbsibling.Text = "Select";
            txtAccno.Text = "";
            txtincome.Text = "0.0";
            txtphone.Text = "";
            txtphone1.Text = "";
            cmbsiblingclass.Text = "Select";
            txtsiblingname.Text = "";
            cmbclass.Text = "Select";
            cmbcaste.Text = "Select";
            txtmname.Text = "";
            txtlname.Text = "";
            txtaddress.Text = "";
            txtfname.Text = "";
            txtmaddress.Text = "";
            txtmname.Text = "";
            txtmothername.Text = "";
            txtmzip.Text = "";
            txtphone.Text = "";
            txtphone1.Text = "";
            txtspecificailment.Text = "";
            txtlname.Text = "";
            txtzip.Text = "";
            txtfathername.Text = "";
             radiomale.Checked = true;
            radioscholor.Checked = true;
            radionormal.Checked = true;
            cmbstate.Text = "Select";
            cmbmstate.Text = "Select";
            cmbreligion.Text = "";
            cmbrvision.Text = "Select";
            cmbclass.Text = "Select";
            cmboralheigine.Text = "Select";
            cmbheight.Text = "Select";
            cmbweight.Text = "Select";
            cmbteeth.Text = "Select";
            cmbcaste.Text = "Select";
            cmbbgroup.Text = "Select";
            cmbfoccupation.Text = "Select";
            cmbfqualification.Text = "Select";
            cmbmoccupation.Text = "Select";
            cmbmqualification.Text = "Select";
            cmblvision.Text = "Select";
            cmbrvision.Text = "Select";
            cmbcity.Text = "";
            cmbmcity.Text = "";
            txtfamilyinfo.Text = "";
            cmbcategory.Text = "Select";
            cmbHouse.Text = "Select";
            chktransport.Checked = false;
            cmbtransport.Text = "Select";
          
                chkbirth.Checked = false;
                chkbirth.Checked = false;
                chktc.Checked = false;
                 chkmarksheet.Checked = false;
                 chkcharacter.Checked = false;
                chkaffidavit.Checked = false;
                chkadmission.Checked = false;
                  chkstu.Checked = false;
                   chkmigration.Checked = false;
                    chkstu.Checked = false;
                    chkparent.Checked = false;
                      chkother.Checked = false;
                     chkfamily.Checked = false;
            bindAdm();
        }





        private void btnupdate_Click(object sender, EventArgs e)
        {

            if (txtfname.Text == "")
            {
                MessageBox.Show("Please Enter FirstName");
                return;
            }
            else if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                MessageBox.Show("Please Select Admission Class ");
                return;
            }
            else if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                MessageBox.Show("Please Select Current Class ");
                return;
            }
          
               
            else if (txtphone1.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No");
                    return;
                }
            else if (chktransport.Checked == true)
            
                if (cmbtransport.Text == "" || cmbtransport.Text == "Select")
                {
                    MessageBox.Show("Please Select Transport");
                    return;
                }            
                //else
                //{
                    string sex;
                    string str = "N";
                    if (radiomale.Checked == true)
                    {
                        sex = "M";
                    }
                    else
                    {
                        sex = "F";
                    }
                    string type1; string type2;
                    if (radioscholor.Checked == true)
                    {
                        type1 = "N";
                    }
                    else
                    {
                        type1 = "H";
                    }
                    if (radionormal.Checked == true)
                    {
                        type2 = "N";
                    }
                    else
                    {
                        type2 = "S";
                    }

                    DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                    SqlConnection sc = dc.con();

                    ArrayList arr = new ArrayList();
                    arr.Add(txtappno.Text);
                    arr.Add(dateofadm.Text);
                    arr.Add(txtfname.Text);
                    arr.Add(txtmname.Text);
                    arr.Add(txtlname.Text);
                    arr.Add(dob.Text);
                    arr.Add(sex);
                    arr.Add(Convert.ToInt32(cmbclass.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbreligion.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbcategory.SelectedValue));
                    arr.Add(Convert.ToDouble(txtincome.Text));
                    arr.Add(txtphone1.Text);
                    arr.Add(txtaddress.Text);
                    arr.Add(Convert.ToInt32(cmbstate.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbcity.SelectedValue));
                    arr.Add(txtzip.Text);
                    arr.Add(txtmaddress.Text);
                    arr.Add(Convert.ToInt32(cmbmstate.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbmcity.SelectedValue));
                    arr.Add(txtmzip.Text);
                    arr.Add(txtphone.Text);
                    arr.Add(type1);
                    arr.Add(type2);
                    arr.Add(txtfathername.Text);
                    arr.Add(Convert.ToInt32(cmbfoccupation.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbfqualification.SelectedValue));
                    arr.Add(txtmothername.Text);
                    arr.Add(Convert.ToInt32(cmbmoccupation.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbmqualification.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbheight.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbweight.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbteeth.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbbgroup.SelectedValue));
                    arr.Add(Convert.ToInt32(cmblvision.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbrvision.SelectedValue));
                    arr.Add(Convert.ToInt32(cmboralheigine.SelectedValue));
                    // arr.Add(txtspecificailment.Text);
                    arr.Add(name);
                    arr.Add(Convert.ToInt32(cmbsibling.SelectedValue));
                    arr.Add("Y");
                    if (chkbirth.Checked == true)
                        arr.Add("1");
                    else
                        arr.Add("0");

                    if (chktc.Checked == true)
                        arr.Add("2");
                    else
                        arr.Add("0");
                    if (chkmarksheet.Checked == true)
                        arr.Add("3");
                    else
                        arr.Add("0");
                    if (chkcharacter.Checked == true)
                        arr.Add("4");
                    else
                        arr.Add("0");
                    if (chkaffidavit.Checked == true)
                        arr.Add("5");
                    else
                        arr.Add("0");
                    if (chkadmission.Checked == true)
                        arr.Add("6");
                    else
                        arr.Add("0");
                    if (chkmigration.Checked == true)
                        arr.Add("7");
                    else
                        arr.Add("0");
                    if (chkstu.Checked == true)
                        arr.Add("8");
                    else
                        arr.Add("0");
                    if (chkparent.Checked == true)
                        arr.Add("9");
                    else
                        arr.Add("0");
                    if (chkother.Checked == true)
                        arr.Add("10");
                    else
                        arr.Add("0");
                    if (chkfamily.Checked == true)
                        arr.Add("11");
                    else
                        arr.Add("0");

                    arr.Add(txtfamilyinfo.Text);
                    arr.Add(txtAccno.Text);
                    arr.Add(Convert.ToInt32(cmbcaste.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbHouse.SelectedValue));
                    arr.Add(father);
                    arr.Add(mother);
                    arr.Add(Convert.ToInt32(cmbtransport.SelectedValue));
                    //if (chktransport.Checked)
                    //    arr.Add("1");
                    //else
                    //    arr.Add("0");

                    //arr.Add();

                 
                    int i = dc.InsertRows("update_student_registration", arr);
                    if (i > 0)
                    {
                        MsgBox.ShowBox("Record is Updated!!!");
                        cancel_form();
                       
                    }
                    else
                    {
                        MsgBox.ShowBox("None of Record is Updated!!!");
                    }

                //}
        }

        private void txtappno_Leave(object sender, EventArgs e)
        {

            binddata(txtappno.Text);
        }

       private void binddata(string str)
        {
            cmbsibling.Text = "";
            txtsiblingname.Text = "";
            cmbsiblingclass.Text = "Select";
            string stuid="0";

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from STUDENT_T st,STUDENT_ADM_DOC_T sd where st.STU_ID=sd.STU_ID and st.STU_ADM_NO='" + str + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            
            {
                txtappno.Text = str;
                txtfname.Text = dr["STU_FIRST_NAME"].ToString();
                txtlname.Text = dr["STU_LAST_NAME"].ToString();
                txtmname.Text = dr["STU_MID_NAME"].ToString();
                dateofadm.Text = dr["STU_DOA"].ToString();
                if (dr["STU_RLG_ID"].ToString() != "0")
                cmbreligion.SelectedValue = Convert.ToInt32(dr["STU_RLG_ID"].ToString());
                if (dr["STU_CASTE_ID"].ToString() != "0")
                cmbcategory.SelectedValue = Convert.ToInt32(dr["STU_CASTE_ID"].ToString());
                if (dr["STU_ADM_CLS_ID"].ToString() != "0")
                cmbclass.SelectedValue = Convert.ToInt32(dr["STU_ADM_CLS_ID"].ToString());
                if (dr["STU_CLS_ID"].ToString() != "0")
                cmbcaste.SelectedValue = Convert.ToInt32(dr["STU_CLS_ID"].ToString());
                cmbHouse.SelectedValue = dr["STU_HOUSE_ID"].ToString();
                dob.Text = dr["STU_DOB"].ToString();
                txtincome.Text = dr["STU_ANNUAL_INCOME"].ToString();
                txtphone1.Text = dr["STU_PHONE"].ToString();
                txtaddress.Text = dr["STU_PER_ADDR"].ToString();
                if (dr["STU_PER_STATE_ID"].ToString() != "0")
                cmbstate.SelectedValue = Convert.ToInt32(dr["STU_PER_STATE_ID"].ToString());
                if (dr["STU_PER_CITY_ID"].ToString() != "0")
                cmbcity.SelectedValue = Convert.ToInt32(dr["STU_PER_CITY_ID"].ToString());
                txtzip.Text = dr["STU_PER_ZIP"].ToString();
                txtmaddress.Text = dr["STU_MAIL_ADDR"].ToString();
                if (dr["STU_MAIL_STATE_ID"].ToString() != "0")
                cmbmstate.SelectedValue = Convert.ToInt32(dr["STU_MAIL_STATE_ID"].ToString());
                if (dr["STU_MAIL_CITY_ID"].ToString() != "0")
                cmbmcity.SelectedValue = Convert.ToInt32(dr["STU_MAIL_CITY_ID"].ToString());
                txtmzip.Text = dr["STU_MAIL_ZIP"].ToString();
                txtphone.Text = dr["STU_MOBILE"].ToString();
                txtfathername.Text = dr["STU_FATHER_NAME"].ToString();
                if (dr["STU_FATHER_OCP_ID"].ToString() != "0")
                cmbfoccupation.SelectedValue = Convert.ToInt32(dr["STU_FATHER_OCP_ID"].ToString());
                if (dr["STU_FATHER_QUL_ID"].ToString() != "0")
                cmbfqualification.SelectedValue =Convert.ToInt32( dr["STU_FATHER_QUL_ID"].ToString());
                txtmothername.Text = dr["STU_MOTHER_NAME"].ToString();
                if (dr["STU_MOTHER_OCP_ID"].ToString() != "0")
                cmbmoccupation.SelectedValue =Convert.ToInt32( dr["STU_MOTHER_OCP_ID"].ToString());
                if (dr["STU_MOTHER_QUL_ID"].ToString() != "0")
                cmbmqualification.SelectedValue =Convert.ToInt32(dr["STU_MOTHER_QUL_ID"].ToString());
                if (dr["STU_HEIGHT_ID"].ToString() != "0")
                cmbheight.SelectedValue = Convert.ToInt32(dr["STU_HEIGHT_ID"].ToString());
                if (dr["STU_WEIGHT_ID"].ToString() != "0")
                cmbweight.SelectedValue =Convert.ToInt32( dr["STU_WEIGHT_ID"].ToString());
                if (dr["STU_BG_ID"].ToString() != "0")
                cmbbgroup.SelectedValue = Convert.ToInt32(dr["STU_BG_ID"].ToString());
                if (dr["STU_OH_ID"].ToString() != "0")
                cmbteeth.SelectedValue = Convert.ToInt32(dr["STU_TEETH_ID"].ToString());
                if (dr["STU_L_VISION_ID"].ToString() != "0")
                cmblvision.SelectedValue = Convert.ToInt32(dr["STU_L_VISION_ID"].ToString());
                if (dr["STU_R_VISION_ID"].ToString() != "0")
                cmbrvision.SelectedValue = Convert.ToInt32(dr["STU_R_VISION_ID"].ToString());
                if (dr["STU_OH_ID"].ToString()!="0")
                cmboralheigine.SelectedValue =Convert.ToInt32( dr["STU_OH_ID"].ToString());
              //  txtspecificailment.Text = dr["specific_ailment"].ToString();
                if (dr["STU_SIBLING_ID"].ToString() != "0")
                    cmbsibling.SelectedValue = Convert.ToInt32(dr["STU_SIBLING_ID"].ToString());
                 txtAccno.Text=dr["STU_ACC_NO"].ToString();

                string sex = dr["STU_SEX"].ToString();
                if (sex == "F")
                {
                    radiofemale.Checked = true;
                }
                string str1; string str2;
                str1 = dr["STU_HOSTLER"].ToString();
                if (str1 == "H")
                {
                    radiohostler.Checked = true;
                }

                str2 = dr["STU_WARD"].ToString();
                if (str2 == "S")
                {
                    radiostaff.Checked = true;
                }
                Btn_create.Enabled = false;
                btnupdate.Enabled = true;
                button4.Enabled = true;
              


                if (dr["STU_IMAGE_PATH"].ToString() != "" )
                {
                    var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                    var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + dr["STU_IMAGE_PATH"].ToString());

                    string imagepath = new Uri(logoimage).LocalPath;
                   // string imagepath = @"E:\sms\sms\StudentImage\" + dr["STU_IMAGE_PATH"].ToString();
                    pictureBox1.Image = System.Drawing.Image.FromFile(imagepath);
                    // pictureBox1.Image = Image.FromStream(stream);
                }
                else
                {
                     var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                    var logoimage = Path.Combine(outPutDirectory, "StudentImage\\Noimage.gif");

                    string imagepath = new Uri(logoimage).LocalPath;
                    
                    //string imagepath = @"E:\sms\sms\StudentImage\" + "Noimage.gif";
                    pictureBox1.Image = System.Drawing.Image.FromFile(imagepath);

                }
                if (dr["STU_FATHER_IMAGE_PATH"].ToString() != "")
                {
                    var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                    var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + dr["STU_FATHER_IMAGE_PATH"].ToString());

                    string imagepath = new Uri(logoimage).LocalPath;
                    // string imagepath = @"E:\sms\sms\StudentImage\" + dr["STU_IMAGE_PATH"].ToString();
                    pictureBox2.Image = System.Drawing.Image.FromFile(imagepath);
                    // pictureBox1.Image = Image.FromStream(stream);
                }
                if (dr["STU_MOTHER_IMAGE_PATH"].ToString() != "")
                {
                    var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                    var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + dr["STU_MOTHER_IMAGE_PATH"].ToString());

                    string imagepath = new Uri(logoimage).LocalPath;
                    // string imagepath = @"E:\sms\sms\StudentImage\" + dr["STU_IMAGE_PATH"].ToString();
                    pictureBox3.Image = System.Drawing.Image.FromFile(imagepath);
                    // pictureBox1.Image = Image.FromStream(stream);
                }

                if (dr["DOCU_1"].ToString() != "0")
                    chkbirth.Checked = true;
                else
                    chkbirth.Checked = false;
                 if (dr["DOCU_2"].ToString() != "0")
                    chktc.Checked = true;
                else
                    chktc.Checked = false;
                 if (dr["DOCU_3"].ToString() != "0")
                    chkmarksheet.Checked = true;
                else
                    chkmarksheet.Checked = false;
                 if (dr["DOCU_4"].ToString() != "0")
                    chkcharacter.Checked = true;
                else
                    chkcharacter.Checked = false;
                    if (dr["DOCU_5"].ToString() != "0")
                    chkaffidavit.Checked = true;
                else
                    chkaffidavit.Checked = false;
                   if (dr["DOCU_6"].ToString() != "0")
                    chkadmission.Checked = true;
                else
                    chkadmission.Checked = false;
                 chkstu.Checked = false;
                if (dr["DOCU_7"].ToString() != "0")
                    chkmigration.Checked = true;
                else
                    chkmigration.Checked = false;
                  if (dr["DOCU_8"].ToString() != "0")
                    chkstu.Checked = true;
                else
                    chkstu.Checked = false;
                  if (dr["DOCU_9"].ToString() != "0")
                    chkparent.Checked = true;
                else
                    chkparent.Checked = false;
               
              
                if (dr["DOCU_10"].ToString() != "0")
                    chkother.Checked = true;
                else
                    chkother.Checked = false;
              
               if (dr["DOCU_11"].ToString() != "0")
                    chkfamily.Checked = true;
                else
                    chkfamily.Checked = false;

                 txtfamilyinfo.Text = dr["STU_ANNUAL_INCOME"].ToString();
                 stuid = dr["STU_ID"].ToString();
                 chktransport.Checked = false;
                 cmbtransport.SelectedValue = 0;
               
                
            }
            else
            {
               // MsgBox.ShowBox("Pls fill valid Admission No!!!");
            }
            dr.Close();
           sc.Close();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_STOP_T where SS_STU_ID=" + stuid + "", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                chktransport.Checked = true;
                cmbtransport.SelectedValue = dt.Rows[0]["SS_STOP_ID"].ToString();

            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();
            //string str = openFileDialog1.FileName;

            //    lblimage.Text = "Error on File Uploadeding";
            //    ImageButton1.ImageUrl = ImageName;
            //}
            //ImageButton1.ImageUrl = "~/ProfileImage/" + ImageName;
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFD = new OpenFileDialog();
            OpenFD.Title = "Specify Backup Location...";
            OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Gif|*.gif";
            OpenFD.FileName = null;
            if (OpenFD.ShowDialog() != DialogResult.Cancel)
            {
                StringBuilder qu = new StringBuilder();
                fileName = OpenFD.FileName;
                name = Path.GetFileName(fileName);
                txtImgPath.Text = fileName;
                Bitmap b = new Bitmap(fileName);

                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                var logoimage = Path.Combine(outPutDirectory,"StudentImage\\" + name);

                string imagepath = new Uri(logoimage).LocalPath;
               // b.Save(@"\StudentImage\" + name);
                b.Save(imagepath);
                Object refmissing = System.Reflection.Missing.Value;
                try
                {
                    pictureBox1.Visible = true;
                    // show it to picturebox
                    pictureBox1.Load(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message.ToString());
                }
            }
        }

        private void txtzip_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtphone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtmzip_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtfcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtmcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }


   








        private void chkisCurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkisCurrent.Checked == true)
                if (cmbclass.SelectedValue != null)
                    cmbcaste.SelectedValue = cmbclass.SelectedValue;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog OpenFD = new OpenFileDialog();
            OpenFD.Title = "Specify Backup Location...";
            OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Gif|*.gif";
            OpenFD.FileName = null;
            if (OpenFD.ShowDialog() != DialogResult.Cancel)
            {
                StringBuilder qu = new StringBuilder();
                fileName = OpenFD.FileName;
                name = Path.GetFileName(fileName);
                txtImgPath.Text = fileName;
                Bitmap b = new Bitmap(fileName);
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + name);

                string imagepath = new Uri(logoimage).LocalPath;
                // b.Save(@"\StudentImage\" + name);
                b.Save(imagepath);

                Object refmissing = System.Reflection.Missing.Value;
                try
                {
                    pictureBox1.Visible = true;
                    // show it to picturebox
                    pictureBox1.Load(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message.ToString());
                }
            }
        }

        private void chkisCurrent_CheckedChanged_1(object sender, EventArgs e)
        {
            cmbcaste.SelectedValue = cmbclass.SelectedValue;
        }

        private void txtappno_TextChanged(object sender, EventArgs e)
        {
            if (txtappno.Text != "")
            {
                binddata(txtappno.Text);
            }

        }

        private void txtphone_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtphone1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtAccno_KeyPress(object sender, KeyPressEventArgs e)
        {
            //v1.digitonly(e);
        }

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbclass.SelectedValue == "")
            {
                MessageBox.Show("Please Enter FirstName");
                return;
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog OpenFD = new OpenFileDialog();
            OpenFD.Title = "Specify Backup Location...";
            OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Gif|*.gif";
            OpenFD.FileName = null;
            if (OpenFD.ShowDialog() != DialogResult.Cancel)
            {
                StringBuilder qu = new StringBuilder();
                fileName = OpenFD.FileName;
                father = Path.GetFileName(fileName);
                txtImgPath.Text = fileName;
                Bitmap b = new Bitmap(fileName);
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + father);

                string imagepath = new Uri(logoimage).LocalPath;
                // b.Save(@"\StudentImage\" + name);
                b.Save(imagepath);

                Object refmissing = System.Reflection.Missing.Value;
                try
                {
                    pictureBox2.Visible = true;
                    // show it to picturebox
                    pictureBox2.Load(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFD = new OpenFileDialog();
            OpenFD.Title = "Specify Backup Location...";
            OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Gif|*.gif";
            OpenFD.FileName = null;
            if (OpenFD.ShowDialog() != DialogResult.Cancel)
            {
                StringBuilder qu = new StringBuilder();
                fileName = OpenFD.FileName;
                mother = Path.GetFileName(fileName);
                txtImgPath.Text = fileName;
                Bitmap b = new Bitmap(fileName);
                var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + mother);

                string imagepath = new Uri(logoimage).LocalPath;
                // b.Save(@"\StudentImage\" + name);
                b.Save(imagepath);

                Object refmissing = System.Reflection.Missing.Value;
                try
                {
                    pictureBox3.Visible = true;
                    // show it to picturebox
                    pictureBox3.Load(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message.ToString());
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ADM_NO from CARD_T ct,STUDENT_T st where ct.CARD_NO=st.STU_ID",sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
                binddata(dt.Rows[0]["STU_ADM_NO"].ToString());
            else
                cancel_form();
        }

        private void chktransport_CheckedChanged(object sender, EventArgs e)
        {
            if (chktransport.Checked == true)
                cmbtransport.Enabled = true;
            else
                cmbtransport.Enabled = false;
        }

        private void cmbsibling_SelectedValueChanged(object sender, EventArgs e)
        {
            //txtsiblingclass.Enabled = true;
            //txtsiblingname.Enabled = true;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from student_t where STU_ADM_NO='" + cmbsibling.Text + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtsiblingname.Text = dr["STU_FIRST_NAME"].ToString() + dr["STU_MID_NAME"].ToString() + dr["STU_LAST_NAME"].ToString();
                cmbsiblingclass.SelectedValue = dr["STU_CLS_ID"].ToString();

                cmbreligion.SelectedValue = dr["STU_RLG_ID"].ToString();
                cmbcaste.SelectedValue = Convert.ToInt32(dr["STU_CASTE_ID"].ToString());
                txtincome.Text = dr["STU_ANNUAL_INCOME"].ToString();

                txtaddress.Text = dr["STU_PER_ADDR"].ToString();
                cmbstate.SelectedValue = Convert.ToInt32(dr["STU_PER_STATE_ID"].ToString());

                cmbcity.SelectedValue = Convert.ToInt32(dr["STU_PER_CITY_ID"].ToString());
                txtzip.Text = dr["STU_PER_ZIP"].ToString();
                txtmaddress.Text = dr["STU_MAIL_ADDR"].ToString();
                cmbmstate.SelectedValue = Convert.ToInt32(dr["STU_MAIL_STATE_ID"].ToString());
                cmbmcity.SelectedValue = Convert.ToInt32(dr["STU_MAIL_CITY_ID"].ToString());
                txtmzip.Text = dr["STU_MAIL_ZIP"].ToString();
                txtphone.Text = dr["STU_PHONE"].ToString();
                txtfathername.Text = dr["STU_FATHER_NAME"].ToString();
                cmbfoccupation.Text = dr["STU_FATHER_OCP_ID"].ToString();
                cmbfqualification.Text = dr["STU_FATHER_QUL_ID"].ToString();
                txtmothername.Text = dr["STU_MOTHER_NAME"].ToString();
                cmbmoccupation.Text = dr["STU_MOTHER_OCP_ID"].ToString();
                cmbmqualification.Text = dr["STU_MOTHER_QUL_ID"].ToString();

            }

            else
            {
                //MsgBox.ShowBox("Pls fill valid Admission No!!!");
            }
        }

        private void cmbstate_Leave(object sender, EventArgs e)
        { }
    }
}


      
       
      

       
      

      

        

       

       

       

        
      

        

       
       
       
       

       
        

      
       
        



        

       
       
       
       

       
      

       

       
       

       
        
        
            
        

    

