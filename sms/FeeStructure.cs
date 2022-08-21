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

namespace sms
{
    public partial class FeeStructure : Form
    {
        int total = 0;
        string classname;
        string stu_id;
        SqlDataReader dr;
        public FeeStructure()
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

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";

        }

        private void bindtuitionfee( )
        {
            textBox1.Text = "0";
            dataclass dc = new dataclass( );
            SqlConnection sc = dc.con( );
            
            SqlDataAdapter da = new SqlDataAdapter("select sef_fee_amount from student_fee_t where sef_fee_id=5 and sef_stu_id= " + cmbAdmNo.SelectedValue + " ", sc);
            DataTable ds = new DataTable( );
            da.Fill(ds);

            if (ds.Rows.Count > 0)
            {

                textBox1.Text = ds.Rows[0]["sef_fee_amount"].ToString( );

            }
            SqlDataAdapter da1 = new SqlDataAdapter("select  sum(frc_month) as tution  from FEE_RECEIPT_T  where  FRC_STU_ID=  " + cmbAdmNo.SelectedValue + " ", sc);
            DataTable ds1 = new DataTable( );
            da1.Fill(ds1);
            string a;
            if (ds1.Rows.Count > 0)
            {
                int b;
                if (ds1.Rows[0]["tution"].ToString() == "" || ds1.Rows[0]["tution"].ToString() == null)
                    a = "0";
                else
                 a = ds1.Rows[0]["tution"].ToString();
                b = Convert.ToInt16(textBox1.Text);
                for (int i = 0; i < Convert.ToInt16(a); i++)
                {
                    total = b + total;
                }

            
            }
        }
        private void bindbusfee()
        {
            textBox2.Text = "0";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select sef_fee_amount from student_fee_t where sef_fee_id=4 and sef_stu_id= " + cmbAdmNo.SelectedValue + " ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                textBox2.Text = ds.Rows[0]["sef_fee_amount"].ToString();

            }


        }

        private void bindanualfee()
        {
            textBox3.Text = "0";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select sef_fee_amount from student_fee_t where sef_fee_id=5 and sef_stu_id= " + cmbAdmNo.SelectedValue + " ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                textBox3.Text = ds.Rows[0]["sef_fee_amount"].ToString();

            }

        }
        private void bindexamfee()
        {
            textBox4.Text = "0";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select sef_fee_amount from student_fee_t where sef_fee_id=7 and sef_stu_id= " + cmbAdmNo.SelectedValue + " ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                textBox4.Text = ds.Rows[0]["sef_fee_amount"].ToString();

            }

        }
        private void bindextrafee()
        {
            textBox5.Text = "0";
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select sum(sef_fee_amount) AS EXTRA_FEE from student_fee_t where sef_fee_id not in(3,5,7,4) and sef_stu_id= " + cmbAdmNo.SelectedValue + " ", sc);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {

                textBox5.Text = ds.Rows[0]["EXTRA_FEE"].ToString();

            }

        }
        private void BindAdmno()
        {

            string class1;
            if (cmbclass.SelectedValue.ToString() == "Select" || cmbclass.SelectedValue.ToString() == "")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID ,Admno from View_studentReport  where STC_SEC_ID ='" + class1 + "' and stu_status='y' order by admno", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STU_ID"] = "0";
            row["Admno"] = "-Select-";
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbAdmNo.DataSource = ds.Tables[0];
            cmbAdmNo.DisplayMember = "Admno";
            cmbAdmNo.ValueMember = "STU_ID";

        }
        private void bindroll()
        {
          
           
            string class1;
            if (cmbclass.SelectedValue.ToString() == "Select" || cmbclass.SelectedValue.ToString() == "")
            {
                class1 = "%";
            }
            else
            {
                class1 = cmbclass.SelectedValue.ToString();
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select AdmNo  from View_studentReport  where STC_SEC_ID ='" + class1 + "' and stu_status='y' order by Roll", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
           
            da.Fill(dt);
            DataRow row = dt.NewRow();
           // row["Roll"] = "0";
            row["AdmNo"] = "-Select-";
            dt.Rows.InsertAt(row, 0);
            cmbAdmNo.DataSource = dt;
          //  cmbRoll.DisplayMember = "Roll";
            cmbRoll.ValueMember = "AdmNo";
        
        }
        private void Cancil_form()
        {

            cmbclass.Text = "-Select-";
            cmbAdmNo.Text = "-Select-";
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            bindData();
        }
        private void bindData()
        {
            stu_id = cmbAdmNo.Text;

            if (cmbAdmNo.Text == "-Select-" || cmbAdmNo.Text == "" || cmbAdmNo.Text == "0")
                stu_id = "%";
            else
                stu_id = cmbAdmNo.SelectedValue.ToString();
            if (cmbclass.Text == "" || cmbclass.Text == "Select")
            {
                classname = "%";
            }
            else
            {
                classname = cmbclass.SelectedValue.ToString();
            }


            if(textBox5.Text=="")
                textBox5.Text="0";
            textBox6.Text = Convert.ToInt16(Convert.ToInt16((textBox5.Text)) + Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox3.Text) + Convert.ToInt16(textBox2.Text) + Convert.ToInt16(textBox1.Text)).ToString();

            cryFeeStructure.Admno = stu_id;
            cryFeeStructure.tuitionfee = total.ToString();
            cryFeeStructure.busfee = textBox2.Text.ToString();
            cryFeeStructure.anualfee = textBox3.Text.ToString();
            cryFeeStructure.examfee = textBox4.Text.ToString();
            cryFeeStructure.extrafee = textBox5.Text.ToString();
            cryFeeStructure.totalfee = textBox6.Text.ToString();
            cryFeeStructure.classid = classname;
            cryFeeStructure obj = new cryFeeStructure();
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

            bindData();

        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBusRouteReport_Load(object sender, EventArgs e)
        {
            textBox5.Text = "0";
            textBox4.Text = "0";
            textBox3.Text = "0";
            textBox2.Text = "0";
            textBox1.Text = "0";
            bindclass(); BindAdmno();

            
        }


        private void cmbclass_Leave(object sender, EventArgs e)
        {
            BindAdmno();// bindroll();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbAdmNo_Leave(object sender, EventArgs e)
        {
            bindextrafee(); bindtuitionfee(); bindbusfee(); bindanualfee(); bindexamfee();
        }

        private void cmbclass_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}