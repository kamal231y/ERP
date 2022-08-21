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
using System.Net;
namespace sms
{
    public partial class frmAdmission : Form
    {
        string mn;
        string dyWords, yyWords;
        string words = "";
        int yy, dy;
        public static string ADM;
        public static string admno;

        public static string SName;
        public static string SMobioleNo;
        public int xLoad = 1;
        public int Nation = 1;
        public int Category = 1;
        public int Religion = 1;
        public int StateP = 1;
        public int CityP = 1;
        public int StateL = 1;
        public int CityL = 1;
        public int QualificationF = 1;
        public int QualificationM = 1;
        public int OccupationF = 1;
        public int OccupationM = 1;

        //AutoCompleteStringCollection namesCollection1 = new AutoCompleteStringCollection();

        BLclass bl = new BLclass();
        validation v1 = new validation();
        WebClient client = new WebClient();
        Stream data;
        StreamReader reader; 

        string fileName;
        string name = ""; string father = ""; string mother = ""; string Document = "";
        byte[] big;
        public frmAdmission()
        {
            InitializeComponent();
            cancel_form();
        }
        private void studentregistration_Load(object sender, EventArgs e)
        {

            cancel_form(); 
            cmbadclass.Visible = false;
            cmbclass.Visible = true;
            txtincome.Text = "0.0";
            // txtappno.Text = dt.Rows[dt.Rows.Count - 1]["Admission_no"].ToString();
            bindSession();
            bindsclass();
            bindsiblingclass();
            // bindAdm();
            CityPermanent();
            CityLocal();
            bindHouse();
            bindNation();
            bindCurrentclass();
            BindRoute();
            bindstate();
            bindheight();
            bindreligion();
            // bindcaste();
            bindcategory();
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
             bindintroducer(); bindCurrentclass(); bindTransMode();
             bindSiblingAdm();bindclass();
            if (SName != null)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select * from tblStudentFormRegistration where Formnoid=" + SName + "", sc);
                DataTable dt = new DataTable(); da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtfname.Text = dt.Rows[0]["StudentName"].ToString();
                    txtfathername.Text = dt.Rows[0]["FatherName"].ToString();
                    txtmothername.Text = dt.Rows[0]["MotherName"].ToString();
                    txtphone1.Text = dt.Rows[0]["Mobileno"].ToString();
                    txtphone.Text = dt.Rows[0]["Mobileno"].ToString();
                    txtaddress.Text = dt.Rows[0]["Address"].ToString();
                    dob.Text = dt.Rows[0]["DOB"].ToString();
                    cmbclass.SelectedValue = Convert.ToInt32(dt.Rows[0]["Classid"].ToString());
                    cmbcaste.SelectedValue = Convert.ToInt32(dt.Rows[0]["Classid"].ToString());
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
            SqlDataAdapter da = new SqlDataAdapter(" select * from BUS_STOP_T ORDER BY STOP_DESC ", sc);
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
            SqlDataAdapter da = new SqlDataAdapter("Bind_AdmNo", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            
                txtappno.Text = dt.Rows[0][0].ToString();
                txtAccno.Text = dt.Rows[0][0].ToString();
                


        }
        private void bindstate1()
        {
            dataclass dc = new dataclass();
            SqlDataAdapter da;
            SqlConnection sc = dc.con();
            da = new SqlDataAdapter(" select * from STATE_T  order by STATE_NAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
          // row["STATE_NAME"] = "UP";
            //row["STATE_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbstate.DataSource = ds.Tables[0];
            cmbstate.DisplayMember = "STATE_NAME";
            cmbstate.ValueMember = "STATE_ID";
          //  cmbstate.SelectedIndex = 1;
        }
        private void bindstate()
        {
            dataclass dc = new dataclass(); SqlDataAdapter da;
            SqlConnection sc = dc.con();
            da = new SqlDataAdapter(" select * from STATE_T  order by STATE_NAME", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
           // row["STATE_NAME"] = "UP";
            //row["STATE_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            cmbmstate.DataSource = ds.Tables[0];
            cmbmstate.DisplayMember = "STATE_NAME";
            cmbmstate.ValueMember = "STATE_ID";
          //  cmbmstate.SelectedIndex = 1;
        }
        private void bindNation()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from NATION_T order by NATION_DESC", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            //row["HOUSE_DESC"] = "Select";
            //row["HOUSE_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmbnational.DataSource = dt;
            cmbnational.DisplayMember = "NATION_DESC";
            cmbnational.ValueMember = "NATION_ID";

        }
        private void bindHouse()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from HOUSE_T order by HOUSE_DESC", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            //row["HOUSE_DESC"] = "Select";
            //row["HOUSE_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmbHouse.DataSource = dt;
            cmbHouse.DisplayMember = "HOUSE_DESC";
            cmbHouse.ValueMember = "HOUSE_ID";

        }
        private void bindSiblingAdm()
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter(" select * from STUDENT_T order by STU_ID", sc);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //DataRow row = dt.NewRow();
            //row["STU_ADM_NO"] = "Select";
            //row["STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            //cmbsibling.DataSource = dt;
            //cmbsibling.DisplayMember = "STU_ADM_NO";
            //cmbsibling.ValueMember = "STU_ID";

        }

        private void bindCategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CASTE_T order by CASTE_DESC", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CASTE_DESC"] = "Select";
            row["CASTE_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbsiblingclass.DataSource = dt;
            cmbsiblingclass.DisplayMember = "CASTE_DESC";
            cmbsiblingclass.ValueMember = "CASTE_ID";

        }
        private void bindDocument()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Document_t order by Document_NAME", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["Document_NAME"] = "Select";
            row["ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbdocument.DataSource = dt;
            cmbdocument.DisplayMember = "Document_NAME";
            cmbdocument.ValueMember = "ID";

        }
        private void BindBank()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from Student_Bank_Detail_T order by BANK_NAME", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["BANK_NAME"].ToString());

            }
            dReader.Close();

            txtbankname.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtbankname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtbankname.AutoCompleteCustomSource = namesCollection;

        }
        private void Qexam_Load()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from Student_Qualification_T order by Exam_Name", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["Exam_Name"].ToString());

            }
            dReader.Close();

            txtqexam.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtqexam.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtqexam.AutoCompleteCustomSource = namesCollection;

        }
        private void QBoard_Load()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from Student_Qualification_T order by Board_Name", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["Board_Name"].ToString());

            }
            dReader.Close();

            txtboard.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtboard.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtboard.AutoCompleteCustomSource = namesCollection;

        }
        private void exam_Load()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();

            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from Entrance_Exam_T order by Exam_Name", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["Exam_Name"].ToString());

            }
            dReader.Close();

            txtexam.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtexam.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtexam.AutoCompleteCustomSource = namesCollection;

        }
        private void bindreligion()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from RELIGION_T order by RLG_DESC", sc);
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
        private void bindcategory()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from CATEGORY_T order by CAT_DESC", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow row = dt.NewRow();
            row["CAT_DESC"] = "Select";
            row["CAT_ID"] = 0;
            dt.Rows.InsertAt(row, 0);
            cmbcategory.DataSource = dt;
            cmbcategory.DisplayMember = "CAT_DESC";
            cmbcategory.ValueMember = "CAT_ID";

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
            SqlDataAdapter da = new SqlDataAdapter(" select * from BG_T order by BG_DESC", sc);
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
        private void CityPermanent()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from city_t order by city_name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["city_name"] = "NOIDA";
            //row["city_id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbcity.DataSource = ds.Tables[0];
            cmbcity.DisplayMember = "city_name";
            cmbcity.ValueMember = "city_id";
           // cmbcity.SelectedIndex = 17;

        }
        private void CityLocal()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from city_t order by city_name", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["city_name"] = "NOIDA";
            //row["city_id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbmcity.DataSource = ds.Tables[0];
            cmbmcity.DisplayMember = "city_name";
            cmbmcity.ValueMember = "city_id";
           // cmbmstate.SelectedIndex = 17;

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
            SqlDataAdapter da = new SqlDataAdapter(" select * from OCCUPATION_T order by OCP_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["OCP_DESC"] = "Select";
            row["OCP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbfoccupation.DataSource = ds.Tables[0];
            cmbfoccupation.DisplayMember = "OCP_DESC";
            cmbfoccupation.ValueMember = "OCP_ID";
          

        }
        private void bindmoccupation()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from OCCUPATION_T order by OCP_DESC", sc);
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
            SqlDataAdapter da = new SqlDataAdapter(" select * from QUALIFICATION_T order by QUL_DESC", sc);
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
            SqlDataAdapter da = new SqlDataAdapter(" select * from QUALIFICATION_T order by QUL_DESC", sc);
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
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by CLS_SEQUENCE ", sc);
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
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by CLS_SEQUENCE ", sc);
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
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by CLS_SEQUENCE ", sc);
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
        private void bindintroducer()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EMPLOYEE_T  order by EMP_NAME ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EMP_NAME"] = "Select";
            row["EMP_CODE"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbintroducer.DataSource = ds.Tables[0];
            cmbintroducer.DisplayMember = "EMP_NAME";
            cmbintroducer.ValueMember = "EMP_CODE";

        }
        private void bindTransMode()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Transport_Mode_T order by trnsport_id  ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["trnsport_name"] = "Select";
            row["trnsport_id"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbtransmode.DataSource = ds.Tables[0];
            cmbtransmode.DisplayMember = "trnsport_name";
            cmbtransmode.ValueMember = "trnsport_id";

        }
        private void bindSession()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from session_T  order by id desc ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            //row["Session"] = "2015-2016";
            //row["id"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsession.DataSource = ds.Tables[0];
            cmbsession.DisplayMember = "Session";
            cmbsession.ValueMember = "id";

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtmaddress.Text = txtaddress.Text;
            cmbmstate.Text = cmbstate.Text;
            cmbmcity.Text = cmbcity.Text;
            txtmzip.Text = txtzip.Text;
        }
        private void cmbstate_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        private void print()
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtdoa = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);

            int dy = dtdob.Day;
            String mn = dtdob.Month.ToString();
            int yy = dtdob.Year;
            if (mn == "1")
            {
                mn = "January";
            }
            else if (mn == "2")
            {
                mn = "February";
            }
            else if (mn == "3")
            {
                mn = "March";
            }
            else if (mn == "4")
            {
                mn = "April";
            }
            else if (mn == "5")
            {
                mn = "May";
            }
            else if (mn == "6")
            {
                mn = "June";
            }
            else if (mn == "7")
            {
                mn = "July";
            }
            else if (mn == "8")
            {
                mn = "August";
            }
            else if (mn == "9")
            {
                mn = "September";
            }
            else if (mn == "10")
            {
                mn = "October";
            }
            else if (mn == "11")
            {
                mn = "November";
            }
            else if (mn == "12")
            {
                mn = "December";
            }
            showstudentreport();
            CRYadmform.ADM = ADM;
            int number = dy;
            NumberToWords(number);
            CRYadmform.yyWords = NumberToWords(number);
            CRYadmform.mn = mn;
            number = yy;
            NumberToWords(number);
            CRYadmform.dyWords = NumberToWords(number);

            CRYadmform obj = new CRYadmform();
            obj.ShowDialog();

        }
        private void showstudentreport()
        {
            if (txtappno.Text == "" || txtappno.Text == "Select")
            {
                ADM = "%";
            }
            else
            {
                ADM = txtappno.Text;
            }
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                    //words += unitsMap[number % 10];
                }
            }

            return words;
        }
        private void Btn_create_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime dtdob = Convert.ToDateTime( dob.Value,System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtdoa = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);

                if (txtappno.Text == "")
                {
                    MessageBox.Show("Please Enter Enrollment No");
                    return;
                }
                else if (cmbsession.Text == "" || cmbsession.Text == "Select")
                {
                    MessageBox.Show("Please Select Session ");
                    return;
                }
                else if (txtfname.Text == "")
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


                //else if (txtphone.Text == "")
                //{
                //    MessageBox.Show("Please Enter Contact No");
                //    return;
                //}
                
                else
                {
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
                    string type1; string type2; string type3;
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
                    else if (RTE.Checked == true)
                    {
                        type2 = "R";
                    }
                    else
                    {
                        type2 = "S";
                    }
                    if (rdregular.Checked == true)
                    {
                        type3 = "R";
                    }
                    else if (rdEx.Checked == true)
                    {
                        type3 = "E";
                    }
                    else
                    {
                        type3 = "RE";
                    }
                    if (chktransport.Checked == true)
                    {
                        if (cmbtransport.Text == "" || cmbtransport.Text == "Select")
                            {
                                 MessageBox.Show("Please Select Transport");
                                 return;
                            }
                     }
                    else if (chktransport.Checked == false)
                        {

                         }
                    DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                    SqlConnection sc = dc.con();

                    ArrayList arr = new ArrayList();
                    arr.Add(txtappno.Text.Trim().Replace(" ", ""));
                    //arr.Add(dateofadm.Text);
                    arr.Add(dtdoa.ToString());
                    arr.Add(txtfname.Text);
                    arr.Add(txtmname.Text);
                    arr.Add(txtlname.Text);
                    //arr.Add(dob.Text);
                    arr.Add(dtdob.ToString());
                    arr.Add(sex);
                    arr.Add(Convert.ToInt32(cmbclass.SelectedValue));
                    arr.Add(cmbreligion.Text);
                    arr.Add(cmbsiblingclass.Text);
                    arr.Add(Convert.ToDouble(txtincome.Text));
                    arr.Add(txtphone1.Text);
                    arr.Add(txtaddress.Text);
                    arr.Add(Convert.ToInt32(cmbstate.SelectedValue));
                    arr.Add((cmbcity.Text.Trim()));
                    arr.Add(txtzip.Text);
                    arr.Add(txtmaddress.Text);
                    arr.Add(Convert.ToInt32(cmbmstate.SelectedValue));
                    arr.Add((cmbmcity.Text.Trim()));

                    arr.Add(txtmzip.Text);
                    arr.Add(txtphone.Text);
                    arr.Add(type1);
                    arr.Add(type2);
                    arr.Add(txtfathername.Text);
                    arr.Add(cmbfoccupation.Text);
                    arr.Add(Convert.ToInt32(cmbfqualification.SelectedValue));
                    arr.Add(txtmothername.Text);
                    arr.Add(cmbmoccupation.Text);
                    arr.Add(Convert.ToInt32(cmbmqualification.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbheight.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbweight.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbteeth.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbbgroup.SelectedValue));
                    arr.Add(Convert.ToInt32(cmblvision.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbrvision.SelectedValue));
                    arr.Add(Convert.ToInt32(cmboralheigine.SelectedValue));
                    arr.Add(cmbtransmode.SelectedValue);
                    arr.Add(textBox15.Text);
                    arr.Add(textBox16.Text);
                    arr.Add(textBox17.Text);

                    //  arr.Add(bimage);
                    arr.Add((cmbsibling.Text));
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
                    arr.Add(cmbHouse.Text);
                    arr.Add(cmbnational.Text);
                    byte[] byteImg1 = ImageToByteArray(pictureBox1.Image, pictureBox1);
                    arr.Add(byteImg1);
                    byte[] byteImg2 = ImageToByteArray(pictureBox2.Image, pictureBox2);
                    arr.Add(byteImg2);
                    byte[] byteImg3 = ImageToByteArray(pictureBox3.Image, pictureBox3);
                    arr.Add(byteImg3);
                    arr.Add(Convert.ToInt32(cmbtransport.SelectedValue));
                    arr.Add((txtgrandfather.Text));
                    arr.Add(cmbcategory.Text);
                    arr.Add((cmbsession.Text));
                    
                    arr.Add((type3));
                    if (CHKFEEATTACH.Checked == true)
                        arr.Add(("1"));
                    else
                        arr.Add(("0"));
                    arr.Add(txtaclass.Text);
                    arr.Add((txtasec.Text));
                    arr.Add(txtfaadhar.Text);
                    arr.Add((txtmaadhar.Text));
                    arr.Add(cmbnational.Text);
                    // if(chktransport.Checked)
                    //arr.Add("1");
                    // else
                    //     arr.Add("0");

                    //arr.Add();
                    int n = dc.InsertRows("Insert_student_registration", arr);
                    if (n > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show(" Are You Sure to print Receipt!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            print();
                        }
                        DialogResult dialogResult1 = MessageBox.Show(" Are You Sure to send an sms!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult1 == DialogResult.Yes)
                        {
                            sms1();
                            MessageBox.Show("Message sent successfully.");
                        }
                    }
                    else
                    {
                        MsgBox.ShowBox("Student already Registered !!!");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.ToString());
            }

            //cancel_form();
            //City_Load();

        }
        private void sms1()
        {

            try
            {
                //dataclass dc1 = new dataclass();
                //SqlConnection sc1 = dc1.connsms();
                //SqlDataAdapter da1 = new SqlDataAdapter("select * from  TBLSMS where userid='" + Form1.SMSKEY + "' and balance>0", sc1);
                //if (sc1 != null && sc1.State == ConnectionState.Closed)
                //{
                //    MsgBox.ShowBox("Please check your Internet Connection For sending the sms ");
                //    return;
                //}
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                string msgtxt = "Dear Parents, Welcome to " + Form1.schname + " ,Your Ward, " + "  " + txtfname.Text + txtlname.Text + " is enroll in Class " + cmbclass.Text + "  .His/her Adm. No. is " + txtappno.Text.Trim() + ". Thanks ";
                DAL.smsclass SMS = new sms.DAL.smsclass();
                SMS.SMS(msgtxt, 0, txtphone.Text.Trim().Replace(" ", ""),txtfname.Text);
            }

            catch (Exception ex)
            {
                //tr.Abort();
                MessageBox.Show("SMS not send... !!!", "Delivery Report...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //data.Close();
                //reader.Close();
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure InActive Student!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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


                    MsgBox.ShowBox("Admission no. Updated !!!");

                    cancel_form();
                    txtappno.Text = "";
                }
                else
                {
                    MsgBox.ShowBox("No Updation ,pls Enter Valid Admission no. !!!");
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
            //City_Load();


        }
        private void cancel_form()
        {
            chktransport.Checked = false;
            cmbtransport.Text = "Select";
            cmbtransport.Enabled = false;
            cmbclass.Enabled = true;
            pictureBox1.Image = GetDataToImage(Form1.cancel);
            pictureBox2.Image = GetDataToImage(Form1.cancel);
            pictureBox3.Image = GetDataToImage(Form1.cancel);
            City_Load(); mCity_Load(); bindTransMode();
            Btn_create.Enabled = true;
            btnupdate.Enabled = false;
            button4.Enabled = false;
            txtappno.Text = "";
            dateofadm.Text = "";
            dob.Text = "";
            chkisCurrent.Checked=false;
            cmbsibling.Text = "";
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
            cmbreligion.Text = "Select";
            cmbnational.Text = "Select";
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
            txtfamilyinfo.Text = ""; cmbsession.Text = "2018-2019";
            cmbsiblingclass.Text = "Select";
            cmbHouse.Text = "Select"; cmbintroducer.Text = "Select";
            chktransport.Checked = false;
            cmbtransport.Text = "Select";
            CHKFEEATTACH.Checked = false;
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
            txtfaadhar.Text = "";
            txtmaadhar.Text = "";
            textBox17.Text = "";
            bindAdm();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dtdoa = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);

                if (txtappno.Text == "")
                {
                    MessageBox.Show("Please Enter Enrollment No");
                    return;
                }
                else if (cmbsession.Text == "" || cmbsession.Text == "Select")
                {
                    MessageBox.Show("Please Select Session ");
                    return;
                }
                else if (txtfname.Text == "")
                {

                    MessageBox.Show("Please Enter FirstName");
                    return;
                }
                //else if (cmbclass.Text == "" || cmbclass.Text == "Select")
                //{
                //    MessageBox.Show("Please Select Admission Class ");
                //    return;
                //}
                //else if (cmbcaste.Text == "" || cmbcaste.Text == "Select")
                //{
                //    MessageBox.Show("Please Select Current Class ");
                //    return;
                //}


                else if (txtphone.Text == "")
                {
                    MessageBox.Show("Please Enter Contact No");
                    return;
                }
                else if (chktransport.Checked == true && (cmbtransport.Text == "Select" || cmbtransport.Text == ""))
                {

                    MessageBox.Show("Please Select Transport");
                    return;

                }
                else
                {
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
                    string type1; string type2; string type3;
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
                    else if (RTE.Checked == true)
                    {
                        type2 = "R";
                    }
                    else
                    {
                        type2 = "S";
                    }
                    if (rdregular.Checked == true)
                    {
                        type3 = "R";
                    }
                    else if (rdEx.Checked == true)
                    {
                        type3 = "E";
                    }
                    else
                        type3 = "RE";
                    DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                    SqlConnection sc = dc.con();

                    ArrayList arr = new ArrayList();
                    arr.Add(txtappno.Text.Trim().Replace(" ", ""));
                    //arr.Add(dateofadm.Text);
                    arr.Add(dtdoa.ToString());
                    arr.Add(txtfname.Text);
                    arr.Add(txtmname.Text);
                    arr.Add(txtlname.Text);
                    //arr.Add(dob.Text);
                    arr.Add(dtdob.ToString());
                    arr.Add(sex);
                    arr.Add(Convert.ToInt32(cmbclass.SelectedValue));
                    arr.Add(cmbreligion.Text);
                    arr.Add(cmbsiblingclass.Text);
                    arr.Add(Convert.ToDouble(txtincome.Text));
                    arr.Add(txtphone1.Text);
                    arr.Add(txtaddress.Text);
                    arr.Add(Convert.ToInt32(cmbstate.SelectedValue));
                    arr.Add((cmbcity.Text.Trim()));

                    arr.Add(txtzip.Text);
                    arr.Add(txtmaddress.Text);
                    arr.Add(Convert.ToInt32(cmbmstate.SelectedValue));
                    arr.Add((cmbmcity.Text.Trim()));

                    arr.Add(txtmzip.Text);
                    arr.Add(txtphone.Text);
                    arr.Add(type1);
                    arr.Add(type2);
                    arr.Add(txtfathername.Text);
                    arr.Add(cmbfoccupation.Text);
                    arr.Add(Convert.ToInt32(cmbfqualification.SelectedValue));
                    arr.Add(txtmothername.Text);
                    arr.Add(cmbmoccupation.Text);
                    arr.Add(Convert.ToInt32(cmbmqualification.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbheight.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbweight.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbteeth.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbbgroup.SelectedValue));
                    arr.Add(Convert.ToInt32(cmblvision.SelectedValue));
                    arr.Add(Convert.ToInt32(cmbrvision.SelectedValue));
                    arr.Add(Convert.ToInt32(cmboralheigine.SelectedValue));
                    arr.Add(cmbtransmode.SelectedValue);
                    arr.Add(textBox15.Text);
                    arr.Add(textBox16.Text);
                    arr.Add(textBox17.Text);
                    byte[] byteImg1 = ImageToByteArray(pictureBox1.Image, pictureBox1);
                    arr.Add(byteImg1);
                    arr.Add((cmbsibling.Text));
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
                    arr.Add(cmbHouse.Text);
                    arr.Add(cmbnational.Text);
                    byte[] byteImg2 = ImageToByteArray(pictureBox2.Image, pictureBox2);
                    arr.Add(byteImg2);
                    byte[] byteImg3 = ImageToByteArray(pictureBox3.Image, pictureBox3);
                    arr.Add(byteImg3);
                    arr.Add(Convert.ToInt32(cmbtransport.SelectedValue));
                    arr.Add(txtgrandfather.Text);
                    arr.Add((cmbcategory.Text));
                    arr.Add((cmbsession.Text));
                    arr.Add((type3));
                    if (CHKFEEATTACH.Checked == true)
                        arr.Add(("1"));
                    else
                        arr.Add(("0"));
                     arr.Add(txtfaadhar.Text);
                     arr.Add((txtmaadhar.Text));
                    //if (chktransport.Checked)
                    //    arr.Add("1");
                    //else
                    //    arr.Add("0");

                    //arr.Add();


                    int i = dc.InsertRows("update_student_registration", arr);
                    if (i > 0)
                    {
                        MsgBox.ShowBox("Record is Updated!!!");
                        //cancel_form();

                    }
                    else
                    {
                        MsgBox.ShowBox("None of Record is Updated!!!");
                    }

                }
            }
            catch (Exception ex) { MessageBox.Show("Error" + ex.Message.ToString()); }
        }
        private void txtappno_Leave(object sender, EventArgs e)
        {

            binddata(txtappno.Text.Trim().Replace(" ", ""));
        }
        private void binddata(string str)
        {
            string stuid = "0";

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from View_StudentAdmission where STU_ADM_NO='" + str + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cmbclass.Visible = false;
                cmbadclass.Visible = true;
                cmbsibling.Text = "";
                txtsiblingname.Text = "";
                cmbsiblingclass.Text = "Select";

                txtappno.Text = str;
                if (dr["STU_status"].ToString().Trim().Replace(" ", "") == "Y")
                {
                    button4.Text = "InActive";
                    button4.Enabled = true;
                }
                else
                {
                    button4.Text = "Active";
                    button4.Enabled = true;
                }
                    cmbtransmode.SelectedValue = Convert.ToInt32(dr["STU_Trans_Mode"].ToString());
                txtfname.Text = dr["STU_FIRST_NAME"].ToString();
                txtlname.Text = dr["STU_LAST_NAME"].ToString();
                txtmname.Text = dr["STU_MID_NAME"].ToString();
                dateofadm.Text = dr["STU_DOA"].ToString();
                cmbreligion.Text = (dr["STU_RLG_ID"].ToString());
                cmbsiblingclass.Text = (dr["STU_CASTE_ID"].ToString());
                cmbclass.Enabled = true;
                cmbclass.SelectedValue = Convert.ToInt32(dr["STU_ADM_CLS_ID"].ToString());
                cmbclass.Enabled = false;
                if (dr["admclass"].ToString().Trim().Replace(" ", "") == "" || dr["admclass"].ToString().Trim().Replace(" ", "") == null) 
                cmbadclass.Text = "";
                else
                cmbadclass.Text = (dr["admclass"].ToString());

                cmbadclass.Enabled = false;
                cmbcaste.SelectedValue = Convert.ToInt32(dr["STU_CLS_ID"].ToString());
                cmbHouse.Text = (dr["STU_house_ID"].ToString());
                cmbnational.Text = (dr["STU_national_ID"].ToString());
                dob.Text = dr["STU_DOB"].ToString();
                txtincome.Text = dr["STU_ANNUAL_INCOME"].ToString();
                txtphone1.Text = dr["STU_PHONE"].ToString();
                txtaddress.Text = dr["STU_PER_ADDR"].ToString();
                cmbstate.SelectedValue = Convert.ToInt32(dr["STU_PER_STATE_ID"].ToString());
                cmbcity.Text = (dr["PCITY_NAME"].ToString());
                txtzip.Text = dr["STU_PER_ZIP"].ToString();
                txtmaddress.Text = dr["STU_MAIL_ADDR"].ToString();
                cmbmstate.SelectedValue = Convert.ToInt32(dr["STU_MAIL_STATE_ID"].ToString());
                cmbmcity.Text = (dr["MCITY_NAME"].ToString());
                txtmzip.Text = dr["STU_MAIL_ZIP"].ToString();
                txtphone.Text = dr["STU_MOBILE"].ToString();
                txtfathername.Text = dr["STU_FATHER_NAME"].ToString();
                cmbfoccupation.Text = (dr["STU_FATHER_OCP_ID"].ToString());
                cmbfqualification.SelectedValue = Convert.ToInt32(dr["STU_FATHER_QUL_ID"].ToString());
                txtmothername.Text = dr["STU_MOTHER_NAME"].ToString();
                cmbmoccupation.Text = (dr["STU_MOTHER_OCP_ID"].ToString());
                cmbmqualification.SelectedValue = Convert.ToInt32(dr["STU_MOTHER_QUL_ID"].ToString());
                cmbheight.SelectedValue = Convert.ToInt32(dr["STU_HEIGHT_ID"].ToString());
                cmbweight.SelectedValue = Convert.ToInt32(dr["STU_WEIGHT_ID"].ToString());
                cmbbgroup.SelectedValue = Convert.ToInt32(dr["STU_BG_ID"].ToString());
                cmbteeth.SelectedValue = Convert.ToInt32(dr["STU_TEETH_ID"].ToString());
                cmblvision.SelectedValue = Convert.ToInt32(dr["STU_L_VISION_ID"].ToString());
                cmbrvision.SelectedValue = Convert.ToInt32(dr["STU_R_VISION_ID"].ToString());
                cmboralheigine.SelectedValue = Convert.ToInt32(dr["STU_OH_ID"].ToString());
                // txtspecificailment.Text = dr["specific_ailment"].ToString();
                cmbsibling.Text = (dr["STU_SIBLING_ID"].ToString());
                txtappno.Leave += new EventHandler(cmbsibling_Leave);
                txtAccno.Text = dr["STU_ACC_NO"].ToString();
                cmbsession.Text = dr["STU_SESSION"].ToString();
                cmbcategory.Text = dr["STU_INTRODUCER_CODE"].ToString();
                txtgrandfather.Text = dr["STU_GRANDFATHER_NAME"].ToString();
                textBox17.Text = dr["email3"].ToString();
                textBox16.Text = dr["email2"].ToString();
                textBox15.Text = dr["email1"].ToString();
                textBox22.Text = dr["school_name"].ToString();
                textBox23.Text = dr["sr_no"].ToString();
                textBox24.Text = dr["board_rollno"].ToString();
                textBox25.Text = dr["board_name"].ToString();
                dateTimePicker1.Text = dr["date"].ToString();

                string sex = dr["STU_SEX"].ToString();
                if (sex == "F")
                {
                    radiofemale.Checked = true;
                }
                else
                    radiomale.Checked = true;

                string str1; string str2; string str3;
                str1 = dr["STU_HOSTLER"].ToString();
                if (str1 == "H")
                    radiohostler.Checked = true;
                else
                    radioscholor.Checked = true;

                str2 = dr["STU_WARD"].ToString();
                if (str2 == "S")
                {
                    radiostaff.Checked = true;
                }
                else if (str2 == "R")
                {
                    RTE.Checked = true;
                }
                else
                    radionormal.Checked = true;

                str3 = dr["STU_COURSE_STATUS"].ToString().Trim().Replace(" ", "");
                if (str3 == "R")
                    rdregular.Checked = true;
                else if (str3 == "E")
                    rdEx.Checked = true;
                else
                    rdre.Checked = true;

                Btn_create.Enabled = false;

                btnupdate.Enabled = true;

                if (dr["STU_FEE_ATTACHED"].ToString() == "1")
                    CHKFEEATTACH.Checked = true;
                else
                    CHKFEEATTACH.Checked = false;

                ///////////////image upload ////////////////////////////////////
                byte[] img_arr1 = new byte[0];
                img_arr1 = (byte[])dr["STU_IMAGE_PATH"];
                pictureBox1.Image = GetDataToImage(img_arr1);
                byte[] img_arr2 = new byte[0];
                img_arr2 = (byte[])dr["STU_FATHER_IMAGE_PATH"];
                pictureBox2.Image = GetDataToImage(img_arr2);
                byte[] img_arr3 = new byte[0];
                img_arr3 = (byte[])dr["STU_MOTHER_IMAGE_PATH"];
                pictureBox3.Image = GetDataToImage(img_arr3);
                /////////////////////////////////////////////////////////////////////

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
                txtfaadhar.Text = dr["faadhar"].ToString();
                txtmaadhar.Text = dr["maadhar"].ToString();
                txtappno.Leave += new EventHandler(cmbsibling_Leave);
               


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
            {
                if (cmbclass.SelectedValue != null)
                    cmbcaste.SelectedValue = cmbclass.SelectedValue;
              
            }
            

               

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

                var logoimage = Path.Combine(outPutDirectory, "StudentImage\\" + "S" + txtappno.Text.Trim().Replace(" ", "") + name);

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
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Choose Image";
                openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Image img = new Bitmap(openFileDialog1.FileName);
                    pictureBox2.Image = img;// resizeImage(img);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message.ToString());
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Choose Image";
                openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Image img = new Bitmap(openFileDialog1.FileName);
                    pictureBox3.Image = img;// resizeImage(img);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message.ToString());
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ADM_NO from CARD_T ct,STUDENT_T st where ct.CARD_NO=st.STU_ID", sc);
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
        private void cmbsibling_load()
        {
            //txtsiblingclass.Enabled = true;
            //txtsiblingname.Enabled = true;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from View_StudentAdmission where STU_ADM_NO='" + cmbsibling.Text.Trim() + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtsiblingname.Text = dr["STU_FIRST_NAME"].ToString() + dr["STU_MID_NAME"].ToString() + dr["STU_LAST_NAME"].ToString();
                // cmbsiblingclass.SelectedValue = dr["STU_CLS_ID"].ToString();
                cmbcategory.SelectedValue = dr["STU_INTRODUCER_CODE"].ToString();
                cmbreligion.SelectedValue = Convert.ToInt32(dr["STU_RLG_ID"].ToString());
                cmbsiblingclass.SelectedValue = Convert.ToInt32(dr["STU_CASTE_ID"].ToString());
                txtincome.Text = dr["STU_ANNUAL_INCOME"].ToString();

                txtaddress.Text = dr["STU_PER_ADDR"].ToString();
                cmbstate.SelectedValue = Convert.ToInt32(dr["STU_PER_STATE_ID"].ToString());

                cmbcity.Text = (dr["PCITY_NAME"].ToString());
                txtzip.Text = dr["STU_PER_ZIP"].ToString();
                txtmaddress.Text = dr["STU_MAIL_ADDR"].ToString();
                cmbmstate.SelectedValue = Convert.ToInt32(dr["STU_MAIL_STATE_ID"].ToString());
                cmbmcity.Text = (dr["MCITY_NAME"].ToString());
                txtmzip.Text = dr["STU_MAIL_ZIP"].ToString();
                txtphone.Text = dr["STU_PHONE"].ToString();
                txtfathername.Text = dr["STU_FATHER_NAME"].ToString();
                cmbfoccupation.SelectedValue = dr["STU_FATHER_OCP_ID"].ToString();
                cmbfqualification.SelectedValue = dr["STU_FATHER_QUL_ID"].ToString();
                txtmothername.Text = dr["STU_MOTHER_NAME"].ToString();
                cmbmoccupation.SelectedValue = dr["STU_MOTHER_OCP_ID"].ToString();
                cmbmqualification.SelectedValue = dr["STU_MOTHER_QUL_ID"].ToString();

            }

            else
            {
                //MsgBox.ShowBox("Pls fill valid Admission No!!!");
            }
        }
        private void btnEntranceSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtexam.Text == "")
                {
                    MessageBox.Show("Please Fill Entrance Exam. ");
                    return;
                }
                else if (txtexamroll.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Roll ");
                    return;
                }
                else
                {
                    string sex;
                    if (rdyes.Checked == true)
                    {
                        sex = "1";
                    }
                    else
                    {
                        sex = "0";
                        txtrank.Text = "";
                    }
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("inser_Entrance_Exam_T '" + txtappno.Text.Trim().Replace(" ", "") + "','" + txtexam.Text.Trim() + "','" + txtexamroll.Text.Trim().Replace(" ", "") + "'," + sex + ",'" + txtrank.Text.Trim().Replace(" ", "") + "'", sc);
                    SqlDataReader dr;
                    sc.Open();

                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Entrance_Exam Created...");
                        Cancil_Entrance();
                        LoadGridEntrance();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Entrance_Exam Name already exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }
        private void Cancil_Entrance()
        {
            btnEntranceSave.Enabled = true;
            txtexam.Text = "";
            txtexamroll.Text = "";
            txtrank.Text = "";

            btnentranceupdate.Enabled = false;
            btnEntranceDelete.Enabled = false;
        }
        private void Cancil_Bank()
        {
            txtbankname.Text = "";
            txtbankcityname.Text = "";
            txtscholarshipNo.Text = "";
            rdnotapplied.Checked = true;
            rdnotapprove.Checked = true;
            txtaccountno.Text = "";
            txtbranchname.Text = "";
            txtscholarshipNo.Enabled = false;
        }
        private void Cancil_Qualification()
        {
            btnqsave.Enabled = true;
            txtentranceid.Text = "";
            txtqexam.Text = "";
            txtqroll.Text = "";
            txtboard.Text = "";
            txtpassyear.Text = "";
            txtphysics.Text = "";
            txtchem.Text = "";
            txtmath.Text = "";
            txteng.Text = "";
            txtcom.Text = ""; txtsubjectmarks6.Text = "";
            txttotalpcm.Text = "";
            txtsubject1.Text = "";
            txtsubject2.Text = "";
            txtsubject3.Text = "";
            txtsubject4.Text = ""; txtsubject5.Text = ""; txtsubject6.Text = "";
            btnqdelete.Enabled = true;
            btnqupdate.Enabled = true;
            LoadGridQualification();

        }
        private void LoadGridEntrance()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select *,Qualified=(case when Qualified=1 then 'Qualified' else 'NoT Qualified' end)  from Entrance_Exam_T where Stu_Adm_No='" + txtappno.Text.Trim().Replace(" ", "") + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridentrance.DataSource = dt;
            gridentrance.Columns["Id"].Visible = false;
            gridentrance.Columns["Stu_Adm_No"].Visible = false;
            gridentrance.Columns["Stu_Adm_No"].Visible = false;
            gridentrance.Columns["Exam_Name"].Width = 200;
            gridentrance.Columns["Exam_Name"].HeaderText = "SchoolName";
            gridentrance.Columns["RollNo"].Width = 100;
            gridentrance.Columns["RollNo"].HeaderText = "Class";
            gridentrance.Columns["Qualified1"].Width = 200;
            gridentrance.Columns["Qualified1"].HeaderText = "Qualified or Not";
            gridentrance.Columns["Qualified"].Visible = false;
            gridentrance.Columns["Rank"].Width = 100;


        }
        private void Loadbankdetail()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select *  from Student_Bank_Detail_T where Stu_Adm_No='" + txtappno.Text.Trim().Replace(" ", "") + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                txtbankname.Text = dt.Rows[0]["BANK_NAME"].ToString();
                txtbankcityname.Text = dt.Rows[0]["REMARK"].ToString();
                txtscholarshipNo.Text = dt.Rows[0]["SCHOLARSHIP_NO"].ToString();
                if (dt.Rows[0]["SCHOLARSHIP_APPROVED"].ToString() == "True")
                    rdapprove.Checked = true;
                else
                    rdnotapprove.Checked = true;
                if (dt.Rows[0]["SCHOLARSHIP_APPROVED"].ToString() == "True")
                    rdapplied.Checked = true;
                else
                    rdnotapplied.Checked = true;

                txtaccountno.Text = dt.Rows[0]["ACCOUNT_NO"].ToString();
                txtbranchname.Text = dt.Rows[0]["BRANCH"].ToString();



            }
        }
        private void LoadGridEnclosure()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select *  from View_Student_Document where Stu_Adm_No='" + txtappno.Text.Trim().Replace(" ", "") + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridenclosure.DataSource = dt;
            gridenclosure.Columns["Stu_Adm_No"].Visible = false;
            gridenclosure.Columns["Id"].Visible = false;
            gridenclosure.Columns["Document_ID"].Visible = false;
            gridenclosure.Columns["Document_Name"].Width = 400;
            gridenclosure.Columns["ImageUrl"].Visible = false;




        }
        private void LoadGridQualification()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from Student_Qualification_T where Stu_Adm_No='" + txtappno.Text.Trim().Replace(" ", "") + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gridqualification.DataSource = dt;
                gridqualification.Columns["Id"].Visible = false;
                gridqualification.Columns["Stu_Adm_No"].Visible = false;
                gridqualification.Columns["Exam_Name"].Width = 200;
                gridqualification.Columns["Board_Name"].Width = 200;
                gridqualification.Columns["Roll_No"].Width = 100;
                gridqualification.Columns["Passing_Year"].Width = 100;
                gridqualification.Columns["Subject1_Marks"].Width = 100;
                gridqualification.Columns["Subject1_Marks"].HeaderText = "Marks";
                gridqualification.Columns["Subject2_Marks"].Width = 100;
                gridqualification.Columns["Subject2_Marks"].HeaderText = "Marks";

                gridqualification.Columns["Subject3_Marks"].Width = 100;
                gridqualification.Columns["Subject3_Marks"].HeaderText = "Marks";
                gridqualification.Columns["Subject4_Marks"].Width = 100;
                gridqualification.Columns["Subject4_Marks"].HeaderText = "Marks";
                gridqualification.Columns["Subject5_Marks"].Width = 100;
                gridqualification.Columns["Subject5_Marks"].HeaderText = "Marks";
                gridqualification.Columns["Subject6_Marks"].Width = 100;
                gridqualification.Columns["Subject6_Marks"].HeaderText = "Marks";

                gridqualification.Columns["Total_Marks"].Width = 100;
                gridqualification.Columns["Total_Marks"].HeaderText = "TotalMarks(%)";


                gridqualification.Columns["subject1"].Width = 100;
                gridqualification.Columns["subject2"].Width = 100;
                gridqualification.Columns["subject3"].Width = 100;
                gridqualification.Columns["subject4"].Width = 100;
                gridqualification.Columns["subject5"].Width = 100;
                gridqualification.Columns["subject6"].Width = 100;

            }
            else
                gridentrance.DataSource = DBNull.Value; ;
        }
        private void btnentranceupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtexam.Text == "")
                {
                    MessageBox.Show("Please Fill Entrance Exam. ");
                    return;
                }
                else if (txtexamroll.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Roll ");
                    return;
                }
              
                else
                {
                    string sex;
                    if (rdyes.Checked == true)
                    {
                        sex = "1";
                    }
                    else
                    {
                        sex = "0";
                        txtrank.Text = "";
                    }
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("Update_Entrance_Exam_T  " + txtentranceid.Text + ", '" + txtappno.Text.Trim().Replace(" ", "") + "','" + txtexam.Text.Trim() + "','" + txtexamroll.Text.Trim().Replace(" ", "") + "'," + sex + ",'" + txtrank.Text.Trim().Replace(" ", "") + "'", sc);
                    SqlDataReader dr;
                    sc.Open();

                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Entrance_Exam Created...");
                        Cancil_Entrance();
                        LoadGridEntrance();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Entrance_Exam Name already exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }

        private void gridentrance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEntranceSave.Enabled = false;
            if (gridentrance.Rows.Count > 0)
            {
                txtentranceid.Text = gridentrance.CurrentRow.Cells["Id"].Value.ToString();
                txtexam.Text = gridentrance.CurrentRow.Cells["Exam_Name"].Value.ToString();
                txtexamroll.Text = gridentrance.CurrentRow.Cells["RollNo"].Value.ToString();
                txtrank.Text = gridentrance.CurrentRow.Cells["Rank"].Value.ToString();
                if (gridentrance.CurrentRow.Cells["Qualified"].Value.ToString() == "1")
                    rdyes.Checked = true;
                else
                    rdno.Checked = true;

                btnentranceupdate.Enabled = true;
                btnEntranceDelete.Enabled = true;
            }
        }

        private void btnEntranceCancel_Click(object sender, EventArgs e)
        {
            Cancil_Entrance();
            LoadGridEntrance();
        }

        private void btnEntranceDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Entrance Exam!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtentranceid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM Entrance_Exam_T WHERE Id=" + txtentranceid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Entrance Exam Deleted Successfully!!!");
                        Cancil_Entrance();
                        LoadGridEntrance();
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

        private void btnEntranceclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnqsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtqexam.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Name");
                    return;
                }
                else if (txtboard.Text == "")
                {
                    MessageBox.Show("Please Fill  board Name ");
                    return;
                }
                else if (txtqroll.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Roll ");
                    return;
                }
                else if (txtpassyear.Text == "")
                {
                    MessageBox.Show("Please Fill  passyear ");
                    return;
                }
                else
                {
                    if (txtphysics.Text == "")
                        txtphysics.Text = "0";
                    if (txtchem.Text == "")
                        txtchem.Text = "0";
                    if (txtmath.Text == "")
                        txtmath.Text = "0";
                    if (txteng.Text == "")
                        txteng.Text = "0";
                    if (txtcom.Text == "")
                        txtcom.Text = "0";
                    if (txttotalpcm.Text == "")
                        txttotalpcm.Text = "0";
                    if (txtsubjectmarks6.Text == "")
                        txtsubjectmarks6.Text = "0";
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("inser_Student_Qualification_T '" + txtappno.Text.Trim().Replace(" ", "") + "','" + txtqexam.Text.Trim() + "','" + txtboard.Text.Trim() + "','" + txtqroll.Text.Trim().Replace(" ", "") + "','" + txtpassyear.Text.Trim().Replace(" ", "") + "'," + txtphysics.Text + "," + txtchem.Text + "," + txtmath.Text + "," + txttotalpcm.Text + "," + txteng.Text + "," + txtcom.Text + ",'" + txtsubject1.Text.Trim() + "','" + txtsubject2.Text.Trim() + "','" + txtsubject3.Text.Trim() + "','" + txtsubject4.Text.Trim() + "','" + txtsubject5.Text.Trim() + "','" + txtsubject6.Text.Trim() + "'," + txtsubjectmarks6.Text.Trim() + "", sc);
                    SqlDataReader dr;
                    sc.Open();

                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Qualification Created...");
                        Cancil_Qualification();
                        LoadGridQualification();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Qualification  already exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }

        private void btnqupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtqexam.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Name");
                    return;
                }
                else if (txtboard.Text == "")
                {
                    MessageBox.Show("Please Fill  board Name ");
                    return;
                }
                else if (txtqroll.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Roll ");
                    return;
                }
                else if (txtpassyear.Text == "")
                {
                    MessageBox.Show("Please Fill  Exam. Roll ");
                    return;
                }
                else
                {
                    if (txtphysics.Text == "")
                        txtphysics.Text = "0";
                    if (txtchem.Text == "")
                        txtchem.Text = "0";
                    if (txtmath.Text == "")
                        txtmath.Text = "0";
                    if (txteng.Text == "")
                        txteng.Text = "0";
                    if (txtcom.Text == "")
                        txtcom.Text = "0";
                    if (txttotalpcm.Text == "")
                        txttotalpcm.Text = "0";
                    if (txtsubjectmarks6.Text == "")
                        txtsubjectmarks6.Text = "0";

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("Update_Student_Qualification_T " + txtqid.Text + ",'" + txtappno.Text.Trim().Replace(" ", "") + "','" + txtqexam.Text.Trim() + "','" + txtboard.Text.Trim() + "','" + txtqroll.Text.Trim().Replace(" ", "") + "','" + txtpassyear.Text.Trim().Replace(" ", "") + "'," + txtphysics.Text + "," + txtchem.Text + "," + txtmath.Text + "," + txttotalpcm.Text + "," + txteng.Text + "," + txtcom.Text + ",'" + txtsubject1.Text.Trim() + "','" + txtsubject2.Text.Trim() + "','" + txtsubject3.Text.Trim() + "','" + txtsubject4.Text.Trim() + "','" + txtsubject5.Text.Trim() + "','" + txtsubject6.Text.Trim() + "'," + txtsubjectmarks6.Text.Trim() + "", sc);
                    SqlDataReader dr;
                    sc.Open();

                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Qualification Updated...");
                        Cancil_Qualification();
                        LoadGridQualification();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Qualification  Not exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }

        private void gridqualification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnqsave.Enabled = false;
            if (gridqualification.Rows.Count > 0)
            {
                txtqid.Text = gridqualification.CurrentRow.Cells["Id"].Value.ToString();
                txtqexam.Text = gridqualification.CurrentRow.Cells["Exam_Name"].Value.ToString();
                txtqroll.Text = gridqualification.CurrentRow.Cells["Roll_No"].Value.ToString();
                txtboard.Text = gridqualification.CurrentRow.Cells["Board_Name"].Value.ToString();
                txtpassyear.Text = gridqualification.CurrentRow.Cells["Passing_Year"].Value.ToString();
                txtphysics.Text = gridqualification.CurrentRow.Cells["Subject1_Marks"].Value.ToString();
                txtchem.Text = gridqualification.CurrentRow.Cells["Subject2_Marks"].Value.ToString();
                txtmath.Text = gridqualification.CurrentRow.Cells["Subject3_Marks"].Value.ToString();
                txteng.Text = gridqualification.CurrentRow.Cells["Subject4_Marks"].Value.ToString();
                txtcom.Text = gridqualification.CurrentRow.Cells["Subject5_Marks"].Value.ToString();
                txttotalpcm.Text = gridqualification.CurrentRow.Cells["Total_Marks"].Value.ToString();
                txtsubject1.Text = gridqualification.CurrentRow.Cells["subject1"].Value.ToString();
                txtsubject2.Text = gridqualification.CurrentRow.Cells["subject2"].Value.ToString();
                txtsubject3.Text = gridqualification.CurrentRow.Cells["subject3"].Value.ToString();
                txtsubject4.Text = gridqualification.CurrentRow.Cells["subject4"].Value.ToString();
                txtsubject5.Text = gridqualification.CurrentRow.Cells["subject5"].Value.ToString();
                txtsubject6.Text = gridqualification.CurrentRow.Cells["subject6"].Value.ToString();
                txtsubjectmarks6.Text = gridqualification.CurrentRow.Cells["Subject6_Marks"].Value.ToString();
                btnqdelete.Enabled = true;
                btnqupdate.Enabled = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Cancil_Qualification();
            LoadGridQualification();

        }

        private void btnqdelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Qualification Exam!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtqid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM Student_Qualification_T WHERE Id=" + txtqid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Qualification Deleted Successfully!!!");
                        Cancil_Entrance();
                       
                        LoadGridQualification();
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

        private void button17_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from student_t where stu_ADM_NO='" + txtappno.Text.Trim().Replace(" ", "") + "' ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count < 1)
            {
                tabControl1.SelectedIndex = 0;
            }
            else
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    txtappno.Enabled = false;
                    Cancil_Entrance();
                    exam_Load(); LoadGridEntrance();
                }
                if (tabControl1.SelectedIndex == 2)
                {
                    txtappno.Enabled = false;

                    Cancil_Qualification(); QBoard_Load();
                    BindSUBJECT1(); BindSUBJECT2(); BindSUBJECT3(); BindSUBJECT4(); BindSUBJECT5(); BindSUBJECT6();

                    Qexam_Load(); LoadGridQualification();
                }
                if (tabControl1.SelectedIndex == 3)
                {
                    txtappno.Enabled = false;

                    Cancil_Enclosure();
                    bindDocument(); LoadGridEnclosure();
                }
                if (tabControl1.SelectedIndex == 4)
                {
                    txtappno.Enabled = false;

                    Cancil_Bank();
                    BindBank(); Loadbankdetail();
                }
                if (tabControl1.SelectedIndex == 0)
                {
                    txtappno.Enabled = true;


                }
            }
        }

        private void txtpassyear_KeyPress(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);
        }

        private void txtphone1_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            v1.digitonly(e);

        }



        private void OnlyCapital(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)bl.uppercase((int)e.KeyChar);
        }


        private void btnuploadEnclosure_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Choose Image";
                //openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;.pdf";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Image img = new Bitmap(openFileDialog1.FileName);
                    pictureenclosure.Image = img;// resizeImage(img);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message.ToString());
            }
        }

        private void btnsaveenclosure_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbdocument.Text == "" || cmbdocument.Text == "Select")
                {
                    MessageBox.Show("Please Select Document ");
                    return;
                }

                else
                {
                    byte[] byteImg1 = ImageToByteArray(pictureenclosure.Image, pictureenclosure);

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("inser_Document_T", sc);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Stu_Adm_No", txtappno.Text.Trim().Replace(" ", ""));
                    com.Parameters.AddWithValue("@doc_id", cmbdocument.SelectedValue);
                    com.Parameters.AddWithValue("@ImageUrl", byteImg1);
                    SqlDataReader dr;
                    sc.Open();
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Document Created...");
                        Cancil_Enclosure();
                        LoadGridEnclosure();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Entrance_Exam Name already exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }
        private void Cancil_Enclosure()
        {
            pictureenclosure.Image = GetDataToImage(Form1.cancel);
            btnsaveenclosure.Enabled = true;
            Updateenclosure.Enabled = false;
            Deleteenclosure.Enabled = false;
            Document = "";

        }
        private void Updateenclosure_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbdocument.Text == "" || cmbdocument.Text == "Select")
                {
                    MessageBox.Show("Please Select Document ");
                    return;
                }
                if (txtenclosureid.Text == "")
                {
                    MessageBox.Show("Please Fill ID. ");
                    return;
                }

                else
                {
                    byte[] byteImg1 = ImageToByteArray(pictureenclosure.Image, pictureenclosure);

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("Update_Student_Document_T", sc);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@id", txtenclosureid.Text.Trim().Replace(" ", ""));
                    com.Parameters.AddWithValue("@Stu_Adm_No", txtappno.Text.Trim().Replace(" ", ""));
                    com.Parameters.AddWithValue("@doc_id", cmbdocument.SelectedValue);
                    com.Parameters.AddWithValue("@ImageUrl", byteImg1);
                    SqlDataReader dr;
                    sc.Open();

                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Document Updated...");
                        Cancil_Enclosure();
                        LoadGridEnclosure();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Document Name Not exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }

        private void Cancelenclosure_Click(object sender, EventArgs e)
        {
            Cancil_Enclosure(); LoadGridEnclosure();
        }

        private void Closeenclosure_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Deleteenclosure_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Document!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtentranceid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM STUDENT_DOCUMENT_T WHERE Id=" + txtenclosureid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Document Deleted Successfully!!!");
                        Cancil_Enclosure();
                        LoadGridEnclosure();
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

        private void btnbanksave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtaccountno.Text == "")
                {
                    MessageBox.Show("Please Fill  AccountNo");
                    return;
                }
                else if (txtbankname.Text == "")
                {
                    MessageBox.Show("Please Fill  Bank Name ");
                    return;
                }
                else if (txtbranchname.Text == "")
                {
                    MessageBox.Show("Please Fill Branch Roll ");
                    return;
                }

                else
                {
                    bool applied, approve;
                    if (rdapplied.Checked == true)
                        applied = true;
                    else
                        applied = false;
                    if (rdapprove.Checked == true)
                        approve = true;
                    else
                        approve = false;
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("insert_Student_Bank_Detail_T '" + txtappno.Text.Trim().Replace(" ", "") + "'," + applied + "," + approve + ",'" + txtscholarshipNo.Text.Trim().Replace(" ", "") + "','" + txtaccountno.Text.Trim().Replace(" ", "") + "','" + txtbankname.Text + "','" + txtbranchname.Text + "','" + txtbankcityname.Text + "'", sc);
                    SqlDataReader dr;
                    sc.Open();

                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Bank_Detail Created...");
                        //Cancil_Bank();

                    }
                    else
                    {
                        MsgBox.ShowBox("This Bank_Detail  already exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }

        private void btnbankcancel_Click(object sender, EventArgs e)
        {
            Cancil_Bank();
        }

        private void rdapplied_CheckedChanged(object sender, EventArgs e)
        {
            if (rdapplied.Checked == true)
                txtscholarshipNo.Enabled = true;
            else
            {
                txtscholarshipNo.Text = "";
                txtscholarshipNo.Enabled = false;
            }
        }

        private void btnbank_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnbankdelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Document!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtappno.Text == "")
                    {
                        MsgBox.ShowBox("Please Enter Enrollment No!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM STUDENT_BANK_DETAIL_T WHERE stu_adm_no=" + txtappno.Text.Trim().Replace(" ", "") + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" STUDENT_BANK_DETAIL Deleted Successfully!!!");
                        Cancil_Bank();
                        LoadGridEnclosure();
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

        private void gridenclosure_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnsaveenclosure.Enabled = false;
                if (gridenclosure.Rows.Count > 0)
                {
                    txtenclosureid.Text = gridenclosure.CurrentRow.Cells["Id"].Value.ToString();
                    cmbdocument.Text = gridenclosure.CurrentRow.Cells["Document_Name"].Value.ToString();
                    Updateenclosure.Enabled = true;
                    Deleteenclosure.Enabled = true;
                    byte[] img_arr1 = new byte[0];
                    img_arr1 = (byte[])gridenclosure.CurrentRow.Cells["ImageUrl"].Value;
                    pictureenclosure.Image = GetDataToImage(img_arr1);

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }

        private void checkBoxsaveasmailing_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxsaveasmailing.Checked == true)
            {
                if (cmbstate.SelectedValue != null)
                    cmbmstate.SelectedValue = cmbstate.SelectedValue;
                if (cmbcity.SelectedValue != null)
                    cmbmcity.SelectedValue = cmbcity.SelectedValue;
                txtmaddress.Text = txtaddress.Text;
                txtmzip.Text = txtzip.Text;
                cmbmcity.Text = cmbcity.Text;

            }
        }


        private void BindSUBJECT1()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select DISTINCT SUBJECT from QUALIFICATION_SUBJECT_T WHERE SUBJECT LIKE '" + txtsubject1.Text + "%' order by SUBJECT", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["SUBJECT"].ToString());

            }
            dReader.Close();

            txtsubject1.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsubject1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsubject1.AutoCompleteCustomSource = namesCollection;

        }
        private void BindSUBJECT2()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select DISTINCT SUBJECT from QUALIFICATION_SUBJECT_T WHERE SUBJECT LIKE '" + txtsubject2.Text + "%' order by SUBJECT", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["SUBJECT"].ToString());

            }
            dReader.Close();

            txtsubject2.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsubject2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsubject2.AutoCompleteCustomSource = namesCollection;

        }
        private void BindSUBJECT3()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select DISTINCT SUBJECT from QUALIFICATION_SUBJECT_T WHERE SUBJECT LIKE '" + txtsubject3.Text + "%' order by SUBJECT", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["SUBJECT"].ToString());

            }
            dReader.Close();

            txtsubject3.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsubject3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsubject3.AutoCompleteCustomSource = namesCollection;

        }
        private void BindSUBJECT4()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select DISTINCT SUBJECT from QUALIFICATION_SUBJECT_T WHERE SUBJECT LIKE '" + txtsubject4.Text + "%' order by SUBJECT", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["SUBJECT"].ToString());

            }
            dReader.Close();

            txtsubject4.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsubject4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsubject4.AutoCompleteCustomSource = namesCollection;

        }
        private void BindSUBJECT5()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select DISTINCT SUBJECT from QUALIFICATION_SUBJECT_T WHERE SUBJECT LIKE '" + txtsubject5.Text + "%' order by SUBJECT", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["SUBJECT"].ToString());

            }
            dReader.Close();

            txtsubject5.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsubject5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsubject5.AutoCompleteCustomSource = namesCollection;

        }
        private void BindSUBJECT6()
        {
            AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
            SqlDataReader dReader;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand("select DISTINCT SUBJECT from QUALIFICATION_SUBJECT_T WHERE SUBJECT LIKE '" + txtsubject6.Text + "%' order by SUBJECT", sc);

            sc.Open();
            dReader = cmd.ExecuteReader();
            if (dReader.HasRows == true)
            {
                while (dReader.Read())
                    namesCollection.Add(dReader["SUBJECT"].ToString());

            }
            dReader.Close();

            txtsubject6.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsubject6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtsubject6.AutoCompleteCustomSource = namesCollection;

        }
        private void City_Load()
        {
            //cmbcity.SelectedText = "";

            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //sc.Open();
            //SqlCommand cmd = new SqlCommand("select distinct City_Name  from CITY_T where City_Name like '" + cmbcity.Text + "%'", sc);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //IList<string> lstFirst = new List<string>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    lstFirst.Add(row.Field<string>("City_Name"));
            //}
            //this.cmbcity.Items.AddRange(lstFirst.ToArray<string>());
            //this.cmbcity.AutoCompleteMode = AutoCompleteMode.Suggest;
            //this.cmbcity.AutoCompleteSource = AutoCompleteSource.ListItems;
            //sc.Close();
        }

        private void mCity_Load()
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //sc.Open();
            //SqlCommand cmd = new SqlCommand("select distinct City_Name  from CITY_T where City_Name like '" + cmbmcity.Text + "%'", sc);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //IList<string> lstFirst = new List<string>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    lstFirst.Add(row.Field<string>("City_Name"));
            //}
            //this.cmbmcity.Items.AddRange(lstFirst.ToArray<string>());
            //this.cmbmcity.AutoCompleteMode = AutoCompleteMode.Suggest;
            //this.cmbmcity.AutoCompleteSource = AutoCompleteSource.ListItems;
            //sc.Close();
        }

        private void cmbstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void tabEnclosures_Click(object sender, EventArgs e)
        {

        }
        public void ImportGISFiles(string sourcePath, string destinationPath, string destinationFolder)
        {

            if (Directory.Exists(destinationPath + destinationFolder))
                Directory.Delete(destinationPath + destinationFolder, true);
            Directory.CreateDirectory(destinationPath + "\\" + destinationFolder);
            string[] fileArray = Directory.GetFiles(sourcePath);
            string fileName = null; string destFile = null;
            foreach (string s in fileArray)
            {
                try
                {
                    string destinationpath = destinationPath + "\\" + destinationFolder;
                    // fileName = Path.GetFileName(fileArray[i].ToString());
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(destinationpath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
                catch (Exception ex) { }
            }
        }
        private void btndownload_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog openFile = new FolderBrowserDialog();
                openFile.ShowDialog();
                string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                string logoimage = Path.Combine(outPutDirectory, "DocumentImage//" + txtappno.Text.Trim().Replace(" ", ""));
                Uri uri = new Uri(logoimage);
                if (Directory.Exists(uri.LocalPath))
                {
                    ImportGISFiles(uri.LocalPath, openFile.SelectedPath, txtappno.Text.Trim().Replace(" ", ""));
                    MessageBox.Show("Document Downloaded ....");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void groupBox24_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void bindsclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID and ct.CLS_DP!='N'  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbsclass.DataSource = ds.Tables[0];
            cmbsclass.DisplayMember = "Class";
            cmbsclass.ValueMember = "SEC_ID";

        }
  

       

       

        private void cmbHouse_TextChanged(object sender, EventArgs e)
        {
            if (Nation == 0)
            {
                Nation = 1;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter(" select * from HOUSE_T order by HOUSE_ID", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                //row["HOUSE_DESC"] = "Select";
                //row["HOUSE_ID"] = 0;
                //dt.Rows.InsertAt(row, 0);
                cmbHouse.DataSource = dt;
                cmbHouse.DisplayMember = "HOUSE_DESC";
                cmbHouse.ValueMember = "HOUSE_ID";
            }
        }

        private void cmbHouse_Leave(object sender, EventArgs e)
        {
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();

            //SqlDataAdapter da = new SqlDataAdapter(" select * from HOUSE_T where house_desc='" + cmbHouse.Text + "' ", sc);
            //DataTable dt = new DataTable();

            //da.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{

            //}

            //else
            //{

            //    cmbHouse.Text = "";
            //    MsgBox.ShowBox("Nationality Not Found !!!!");
            //    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Nationality ??"))
            //    {
            //        xLoad = 0;
            //        Religionmaster obj = new Religionmaster();
            //        obj.ShowDialog();
            //        cmbHouse.Focus();
            //    }
            //}


        }

        private void cmbreligion_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from RELIGION_T where RLG_DESC='" + cmbreligion.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbreligion.Text != "Select")
                {
                    cmbreligion.Text = "";
                    MsgBox.ShowBox("Religion Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Religion ??"))
                    {
                        Religion = 0;
                        Religionmaster obj = new Religionmaster();
                        obj.ShowDialog();
                        cmbreligion.Focus();
                    }
                }
            }


        }

        private void cmbreligion_TextChanged(object sender, EventArgs e)
        {
            if (Religion == 0)
            {
                Religion = 1;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter(" select * from RELIGION_T order by RLG_ID", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row["HOUSE_DESC"] = "Select";
                row["HOUSE_ID"] = 0;
                dt.Rows.InsertAt(row, 0);
                cmbHouse.DataSource = dt;
                cmbHouse.DisplayMember = "RLG_DESC";
                cmbHouse.ValueMember = "RLG_ID";
            }
        }

        private void cmbcategory_TextChanged(object sender, EventArgs e)
        {
            if (Category == 0)
            {
                Category = 1;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter(" select * from CATEGORY_T order by CAT_ID", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row["CAT_DESC"] = "Select";
                row["CAT_ID"] = 0;
                dt.Rows.InsertAt(row, 0);
                cmbcategory.DataSource = dt;
                cmbcategory.DisplayMember = "CAT_DESC";
                cmbcategory.ValueMember = "CAT_ID";
            }
        }

        private void cmbcategory_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from CATEGORY_T where CAT_DESC='" + cmbcategory.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbcategory.Text != "Select")
                {
                    cmbcategory.Text = "";
                    MsgBox.ShowBox("Category Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Category ??"))
                    {
                        Category = 0;
                        Categorymaster obj = new Categorymaster();
                        obj.ShowDialog();
                        cmbcategory.Focus();
                    }
                }
            }

        }

        private void cmbstate_TextChanged(object sender, EventArgs e)
        {

            if (StateP == 0)
            {
                StateP = 1;
                dataclass dc = new dataclass(); SqlDataAdapter da;
                SqlConnection sc = dc.con();
                da = new SqlDataAdapter(" select * from STATE_T  order by STATE_NAME", sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].NewRow();
                //row["STATE_NAME"] = "Select";
                //row["STATE_ID"] = 0;
                //ds.Tables[0].Rows.InsertAt(row, 0);
                cmbstate.DataSource = ds.Tables[0];
                cmbstate.DisplayMember = "STATE_NAME";
                cmbstate.ValueMember = "STATE_ID";
            }
        }

        private void cmbstate_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from STATE_T where STATE_NAME='" + cmbstate.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbstate.Text != "Select")
                {
                    cmbstate.Text = "";
                    MsgBox.ShowBox("State Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New State ??"))
                    {
                        StateP = 0;
                        frmStateMaster obj = new frmStateMaster();
                        obj.ShowDialog();
                        cmbstate.Focus();
                    }
                }
            }

        }



        private void cmbmstate_TextChanged(object sender, EventArgs e)
        {
            if (StateL == 0)
            {
                StateL = 1;
                dataclass dc = new dataclass(); SqlDataAdapter da;
                SqlConnection sc = dc.con();
                da = new SqlDataAdapter(" select * from STATE_T  order by STATE_NAME", sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].NewRow();
                //row["STATE_NAME"] = "Select";
                //row["STATE_ID"] = 0;
                //ds.Tables[0].Rows.InsertAt(row, 0);

                cmbmstate.DataSource = ds.Tables[0];
                cmbmstate.DisplayMember = "STATE_NAME";
                cmbmstate.ValueMember = "STATE_ID";
            }
        }

        private void cmbmstate_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from STATE_T where STATE_NAME='" + cmbmstate.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbmstate.Text != "Select")
                {
                    cmbmstate.Text = "";
                    MsgBox.ShowBox("State Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New State ??"))
                    {
                        StateL = 0;
                        frmStateMaster obj = new frmStateMaster();
                        obj.ShowDialog();
                        cmbmstate.Focus();
                    }
                }
            }


        }

 

        private void cmbfqualification_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from QUALIFICATION_T where QUL_DESC='" + cmbfqualification.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbfqualification.Text != "Select")
                {
                    cmbfqualification.Text = "";
                    MsgBox.ShowBox("QUALIFICATION Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New QUALIFICATION ??"))
                    {
                        QualificationF = 0;
                        Qualification obj = new Qualification();
                        obj.ShowDialog();
                        bindfqualification();
                        cmbfqualification.Focus();
                    }
                }
            }

        }

        private void cmbmqualification_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from QUALIFICATION_T where QUL_DESC='" + cmbmqualification.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbmqualification.Text != "Select")
                {
                    cmbmqualification.Text = "";
                    MsgBox.ShowBox("QUALIFICATION Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New QUALIFICATION ??"))
                    {
                        QualificationM = 0;
                        Qualification obj = new Qualification();
                        obj.ShowDialog();
                        bindmqualification();
                        cmbmqualification.Focus();
                    }
                }
            }

        }

        private void cmbfoccupation_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from OCCUPATION_T where OCP_DESC='" + cmbfoccupation.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbfoccupation.Text != "Select")
                {
                cmbfoccupation.Text = "";
                MsgBox.ShowBox("Occupation Not Found !!!!");
                if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Occupation ??"))
                {
                    OccupationF = 0;
                    occupation obj = new occupation();
                    obj.ShowDialog();
                    bindfoccupation();
                    cmbfoccupation.Focus();
                }
                }
            }


        }

        private void cmbmoccupation_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from OCCUPATION_T where OCP_DESC='" + cmbmoccupation.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmbmoccupation.Text != "Select")
                {
                cmbmoccupation.Text = "";
                MsgBox.ShowBox("Occupation Not Found !!!!");
                if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Occupation ??"))
                {
                    OccupationM = 0;
                    occupation obj = new occupation();
                    obj.ShowDialog();
                    bindmoccupation();
                    cmbmoccupation.Focus();
                }
                }
            }



        }

        private void cmbcity_TextChanged(object sender, EventArgs e)
        {
            if (CityP == 0)
            {
                CityP = 1;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter(" select * from city_t order by city_id", sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].NewRow();
                //row["city_name"] = "Select";
                //row["city_id"] = 0;
                //ds.Tables[0].Rows.InsertAt(row, 0);
                cmbcity.DataSource = ds.Tables[0];
                cmbcity.DisplayMember = "city_name";
                cmbcity.ValueMember = "city_id";
            }

        }

        private void cmbcity_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from city_t where city_name='" + cmbcity.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {

                cmbcity.Text = "";
                MsgBox.ShowBox("City Not Found !!!!");
                if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New City ??"))
                {
                    CityP = 0;
                    frmCityMaster obj = new frmCityMaster();
                    obj.ShowDialog();
                    cmbcity.Focus();
                }
            }


        }

        private void cmbmcity_TextChanged(object sender, EventArgs e)
        {
            if (CityL == 0)
            {
                CityL = 1;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter(" select * from city_t order by city_id", sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].NewRow();
                //row["city_name"] = "Select";
                //row["city_id"] = 0;
                //ds.Tables[0].Rows.InsertAt(row, 0);
                cmbmcity.DataSource = ds.Tables[0];
                cmbmcity.DisplayMember = "city_name";
                cmbmcity.ValueMember = "city_id";
            }


        }

        private void cmbmcity_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from city_t where city_name='" + cmbmcity.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {

                cmbmcity.Text = "";
                MsgBox.ShowBox("City Not Found !!!!");
                if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New City ??"))
                {
                    CityL = 0;
                    frmCityMaster obj = new frmCityMaster();
                    obj.ShowDialog();
                    cmbmcity.Focus();
                }
            }


        }

        private void chkisCurrent_CheckedChanged_2(object sender, EventArgs e)
        {

        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbsibling_Leave(object sender, EventArgs e)
        {
            {
                //txtsiblingclass.Enabled = true;
                //txtsiblingname.Enabled = true;
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlCommand cmd = new SqlCommand("select * from View_StudentAdmission where STU_ADM_NO='" + cmbsibling.Text + "'", sc);
                sc.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtsiblingname.Text = dr["STU_FIRST_NAME"].ToString() + dr["STU_MID_NAME"].ToString() + dr["STU_LAST_NAME"].ToString();
                    cmbsclass.SelectedValue = dr["STU_CLS_ID"].ToString();

                    cmbreligion.Text = dr["STU_RLG_ID"].ToString();
                    cmbHouse.Text = dr["STU_National_id"].ToString();
                    cmbcaste.Text = dr["STU_CASTE_ID"].ToString();
                    txtincome.Text = dr["STU_ANNUAL_INCOME"].ToString();

                    txtaddress.Text = dr["STU_PER_ADDR"].ToString();
                    cmbstate.SelectedValue = Convert.ToInt32(dr["STU_PER_STATE_ID"].ToString());

                    cmbcity.SelectedValue = Convert.ToInt32(dr["STU_PER_CITY_ID"].ToString());
                    txtzip.Text = dr["STU_PER_ZIP"].ToString();
                    txtmaddress.Text = dr["STU_MAIL_ADDR"].ToString();
                    cmbmstate.SelectedValue = Convert.ToInt32(dr["STU_MAIL_STATE_ID"].ToString());
                    cmbmcity.SelectedValue = Convert.ToInt32(dr["STU_MAIL_CITY_ID"].ToString());
                    txtmzip.Text = dr["STU_MAIL_ZIP"].ToString();
                    txtphone.Text = dr["STU_mobile"].ToString();
                    txtfathername.Text = dr["STU_FATHER_NAME"].ToString();
                    cmbfoccupation.Text = dr["STU_FATHER_OCP_ID"].ToString();
                    cmbfqualification.SelectedValue = Convert.ToInt32(dr["STU_FATHER_QUL_ID"].ToString());
                    txtmothername.Text = dr["STU_MOTHER_NAME"].ToString();
                    cmbmoccupation.Text = dr["STU_MOTHER_OCP_ID"].ToString();
                    cmbmqualification.SelectedValue = Convert.ToInt32(dr["STU_MOTHER_QUL_ID"].ToString());

                }

                //else
                //{
                //    txtsiblingname.Text = "";
                //    cmbsclass.SelectedValue = 0;

                //    cmbreligion.SelectedValue = 0;
                //    // cmbcaste.SelectedValue = Convert.ToInt32(dr["STU_CASTE_ID"].ToString());
                //    txtincome.Text = "0";

                //    txtaddress.Text = "";
                //    cmbstate.SelectedValue = 0;

                //    cmbcity.SelectedValue = 0;
                //    txtzip.Text = "";
                //    txtmaddress.Text = "";
                //    cmbmstate.SelectedValue = 0;
                //    cmbmcity.SelectedValue = 0;
                //    txtmzip.Text = "";
                //    txtphone.Text = "";
                //    txtfathername.Text = "";
                //    cmbfoccupation.SelectedValue = 0;
                //    cmbfqualification.SelectedValue = 0;
                //    txtmothername.Text = "";
                //    cmbmoccupation.SelectedValue = 0;
                //    cmbmqualification.SelectedValue = 0;
                //    cmbsibling.SelectedValue = 0;
                //}

            }

        }

        private void tabPersonal_Click(object sender, EventArgs e)
        {

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            print();
        }

        private void txtzip_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {
            cbsprint();

        }
        private void cbsprint()
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dtdoa = Convert.ToDateTime(dateofadm.Value, System.Globalization.CultureInfo.InvariantCulture);

            int dy = dtdob.Day;
            String mn = dtdob.Month.ToString();
            int yy = dtdob.Year;
            if (mn == "1")
            {
                mn = "January";
            }
            else if (mn == "2")
            {
                mn = "February";
            }
            else if (mn == "3")
            {
                mn = "March";
            }
            else if (mn == "4")
            {
                mn = "April";
            }
            else if (mn == "5")
            {
                mn = "May";
            }
            else if (mn == "6")
            {
                mn = "June";
            }
            else if (mn == "7")
            {
                mn = "July";
            }
            else if (mn == "8")
            {
                mn = "August";
            }
            else if (mn == "9")
            {
                mn = "September";
            }
            else if (mn == "10")
            {
                mn = "October";
            }
            else if (mn == "11")
            {
                mn = "November";
            }
            else if (mn == "12")
            {
                mn = "December";
            }
            showstudentreport();
            CRYCBSadmform.Age = textBox12.Text;
            CRYCBSadmform.month = textBox13.Text;
            CRYCBSadmform.day = textBox14.Text;
            CRYCBSadmform.ADM = ADM;
            int number = dy;
            NumberToWords(number);
            CRYCBSadmform.yyWords = NumberToWords(number);
            CRYCBSadmform.mn = mn;
            number = yy;
            NumberToWords(number);
            CRYCBSadmform.dyWords = NumberToWords(number);

            CRYCBSadmform obj = new CRYCBSadmform();
            obj.ShowDialog();

        }

        private void button27_Click(object sender, EventArgs e)
        {
            showstudentreport();
            
            CRYofcadmform.ADM = ADM;
            CRYofcadmform obj = new CRYofcadmform();
            obj.ShowDialog();

        }

        private void dob_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan age = DateTime.Now - dob.Value;
            int years = DateTime.Now.Year - dob.Value.Year;
            if (dob.Value.AddYears(years) > DateTime.Now) years--;
            textBox12.Text = years.ToString();
            int Months= DateTime.Now.Month - dob.Value.Month;
            if (dob.Value.AddMonths(Months) > DateTime.Now)
            
                Months--;
                int b = Convert.ToInt32(Months.ToString());
                if (b < 0)
                {
                    textBox13.Text = (b + 12).ToString();
                }
                
                else
                {
                textBox13.Text = Months.ToString();
                }
              int DAYS = DateTime.Now.Day - dob.Value.Day;
              if (dob.Value.AddDays(DAYS) > DateTime.Now)
                 
                     DAYS--;
              int c = Convert.ToInt32(DAYS.ToString());
              if (c < 0)
              {
                  textBox14.Text = (c + 30).ToString();
              }
              else
              {
                  textBox14.Text = DAYS.ToString();
              }
                    

                 


            }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
            WindowsFormsApplication7.frmWebcam obj = new WindowsFormsApplication7.frmWebcam();
            obj.ShowDialog();
        }

        private void Update_Click(object sender, EventArgs e)
        {
          
            if (textBox22.Text == "")
            {
                MsgBox.ShowBox("Please Fill school Name!!!");
                return;
            }
            else
                if (textBox23.Text == "")
                {
                    MsgBox.ShowBox("Please Fill Sr.No!!!");
                    return;

                }
                else
                {
    
                    DateTime doa = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("Insert_transfer_school '" + txtappno.Text + "','" + textBox22.Text + "' ,'" + textBox23.Text + "','" + textBox24.Text + "','" + textBox25.Text + "','" + doa.ToString() + "'", sc);
                    SqlDataReader dr;
                    sc.Open();
                    try
                    {
                        dr = com.ExecuteReader();
                        if (dr.RecordsAffected > 0)
                        {
                            MsgBox.ShowBox("Record created...");
                            
                            
                        }
                        else
                        {
                            MsgBox.ShowBox("Record already exist");
                        }

                    }
                    catch (Exception ex)
                    { }
                    finally
                    {
                        sc.Close();
                    }
                }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Record!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtappno.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM transfer_school WHERE stu_adm_no=" + txtappno.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Record Deleted Successfully!!!");
                        Cancil_Entrance();
                        LoadGridEntrance();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        private void button39_Click(object sender, EventArgs e)
        {
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbclass_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_section_t where  Sec_id like '" + cmbclass.SelectedValue + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //phone = dt.Rows[0]["phone"].ToString();
                //Stu_id = dt.Rows[0]["STU_ID"].ToString();
                //cmbclass.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                //bindroll();
                // cmb_rollno.Text = dt.Rows[0]["Roll"].ToString();
                txtaclass.Text = dt.Rows[0]["cls_Desc"].ToString();
                txtasec.Text = dt.Rows[0]["sec_Desc"].ToString();
                // BIND_DATA();
                //btn_create.Focus();
            }
            else
            {
                //Stu_id = "0";
                // stuname.Text = "";
                txtaclass.Text = "";
                txtasec.Text = "";
                //BIND_DATA();
            }
        }

        private void cmbHouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbclass_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox27_Enter(object sender, EventArgs e)
        {

        }

        private void tabEntrance_Click(object sender, EventArgs e)
        {

        }

        private void tabQualification_Click(object sender, EventArgs e)
        {

        }

        

        
            
        }


         
      
    } 
            
        

    

