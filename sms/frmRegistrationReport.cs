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
using System.Data.SqlClient;
using MsgBoxRet;
using System.Collections;


namespace sms
{
    public partial class frmRegistrationReport : Form
    {
        string usertype;
        public static DateTime dt1;
        public static DateTime dt2;
        //string test;
        //string classn;
        SqlDataReader dr;
        public frmRegistrationReport()
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
            //cmbbilltype.SelectedIndex = 0;

            Cancil_form();
            Binduser();
            bindSession();
        }

        private void Cancil_form()
        {
            cmbuser.Text = "Select";

            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
            dataGridView1.DataSource = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (cmbuser.Text == "-Select-" || cmbuser.Text == "")
            {
                usertype = "%";
            }
            else
            {
                usertype = cmbuser.SelectedValue.ToString(); ;
            }



            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            string str = "select session,id,Formnoid  as Formnoid,StudentName,Amount,Userid  from tblStudentFormRegistration where session='" + cmbsession.Text + "' and Rdate>='" + dt1.ToString("MM/dd/yyyy") + "' and Rdate<='" + dt2.ToString("MM/dd/yyyy") + "' ";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];



            sc.Close();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


            if (dataGridView1.Rows.Count > 0)
            {
                cryRegistrationReport.dt1 = dateTimePicker1.Value;
                cryRegistrationReport.dt2 = dateTimePicker2.Value;

                cryRegistrationReport.usertype = usertype;
                cryRegistrationReport obj = new cryRegistrationReport();

                obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
        }
        private void Binduser()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            SqlCommand cmd = new SqlCommand("select distinct id, userType from tblRights   order by userType", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["UserType"] = "-Select-";
            row["UserType"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbuser.DataSource = dt;
            cmbuser.DisplayMember = "UserType";
            cmbuser.ValueMember = "UserType";
            sc.Close();
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

        private void frmRegistrationReport_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                frmAdmission.SName = dataGridView1.CurrentRow.Cells["FORMNOID"].Value.ToString();
                frmAdmission obj = new frmAdmission();
                obj.ShowDialog();

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}















































































































