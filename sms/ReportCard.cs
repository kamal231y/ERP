using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.DAL;
using sms.BLClass;
using System.Collections;

namespace sms
{
    public partial class ReportCard : Form
    {
        public string classn;
        public string examn;
        public string RollNo;


        //MsgBox msg = new MsgBox(Color.Red,Color.WhiteSmoke,Color.WhiteSmoke,Color.Red,Color.Red,Color.WhiteSmoke,FlatStyle.Flat);
        //int tmpCount;
        public ReportCard()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;
            InitializeComponent();
            //tmpCount = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //AdmitCDate.dt1 = dateTimePicker1.Value;
            //AdmitCDate.dt2 = dateTimePicker2.Value;
            CryReportCard.classn = cmbClass.SelectedValue.ToString();
            CryReportCard.examn = cmbExam.Text; 
            CryReportCard.RollNo = cmbRollNo.Text;
            CryReportCard podv = new CryReportCard();
            
            podv.ShowDialog();
        }

        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EXAMINATION_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EXAM_DESC"] = "Select";
            row["EXAM_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "EXAM_DESC";
            cmbExam.ValueMember = "EXAM_ID";
        }
        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC +' '+ SEC_DESC) as Class ,* from View_fillfeecollection  ", sc);
            DataSet ds = new DataSet();                                                                                  //where CLS_DESC='" + cmbExam.SelectedIndex+ "'
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
        }
        private void bidRollNo()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblrollno", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["admissionno"] = "Select";
            row["rollid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbRollNo.DataSource = ds.Tables[0];
            cmbRollNo.DisplayMember = "admissionno";
            cmbRollNo.ValueMember = "rollid";
        }
        


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReportCard_Load(object sender, EventArgs e)
        {
            bindExam();
            
            bidRollNo();

        }

        private void cmbExam_Leave(object sender, EventArgs e)
        {
            bindclass();
        }

      
       
       

       
        }
}