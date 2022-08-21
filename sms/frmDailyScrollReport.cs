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
    public partial class frmDailyScrollReport : Form
    {
        string hostel;
        string classname;
        SqlDataReader dr;
        public frmDailyScrollReport()
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
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID AND ct.CLS_DP!='N' order by ct.CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
        }
       private void Cancil_form()
        {
          
            cmbclass.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

            

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           DateTime dt1, dt2;
           dt1 = dateTimePicker1.Value;
           dt2 = dateTimePicker2.Value;

           if (cmbclass.Text == "" || cmbclass.Text == "Select")
           {
               classname = "%";
           }
           else
           {
               classname = cmbclass.SelectedValue.ToString();
           }

           if (rdbothhostel.Checked)
           {
               hostel = "%";
           }
           else
           {
               if (radiohostler.Checked)
               {
                   hostel = "H";

               }
               else
               {
                   hostel = "N";
               }
           }
           dataclass dc = new dataclass();
           SqlConnection sc = dc.con();
           string str = "select   ROW_NUMBER() OVER (ORDER BY frc_no) AS SNO,* from View_DailyScroll where  FRC_DATE  between '" + dt1.ToString("yyyy/MM/dd") + "' and '" + dt2.ToString("yyyy/MM/dd") + "'and STC_SEC_ID like '" + classname + "' and stu_hostler like '" + hostel + "'   order by FRC_no";
           SqlDataAdapter da1 = new SqlDataAdapter("UPDATE_SCROLL", sc);
           da1.SelectCommand.CommandType = CommandType.StoredProcedure;
           DataSet ds1 = new DataSet();
           //da1.Fill(ds1);
           SqlDataAdapter da = new SqlDataAdapter(str, sc);
           da.Fill(ds1, "View_DailyScroll");

           if (ds1.Tables.Count > 0)
           {
               dataGridView1.DataSource = ds1.Tables[0];
               if (ds1.Tables[0].Columns["amt1"] == null || ds1.Tables[0].Columns["amt1"].ToString() == "")
               {
                   dataGridView1.Columns["amt1"].Visible = false;
               }

           }
       }
       private void bindData()
       {

           {
               if (cmbclass.Text=="" ||cmbclass.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbclass.SelectedValue.ToString();
               }


               //if (rbtnAll.Checked)
               //{
               //    classname = "%";
               //}
               //else
               //{
               //    if (rbtnNur2Ukg.Checked)
               //    {
               //        classname = "H";

               //    }
               //    else
               //    {
               //        classname = "N";
               //    }
               //}

               if (rdbothhostel.Checked)
               {
                   hostel = "%";
               }
               else
               {
                   if (radiohostler.Checked)
                   {
                       hostel = "H";

                   }
                   else
                   {
                       hostel = "N";
                   }
               }
               //if (classname == "1" || classname == "2" || classname == "3" || classname == "4" || classname == "5" || classname == "6" || classname == "7" || classname == "8" || classname == "9" || classname == "10")
               //{
                   cryDailyScrollReport.dt1 = dateTimePicker1.Value;
                   cryDailyScrollReport.dt2 = dateTimePicker2.Value;
                   cryDailyScrollReport.classid = classname;
                   cryDailyScrollReport.hostel = hostel;

                   cryDailyScrollReport obj = new cryDailyScrollReport();
                   obj.ShowDialog();
               //}
               //else
               //{
               //}

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

            bindData();
           
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDailyScrollReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.View_DailyScroll' table. You can move, or remove it, as needed.
            //this.view_DailyScrollTableAdapter.Fill(this.dataSet1.View_DailyScroll);
            bindclass();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}