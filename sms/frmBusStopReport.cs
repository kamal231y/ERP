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
    public partial class frmBusStopReport : Form
    {
        string hostel;
        string classname;
        SqlDataReader dr;
        public frmBusStopReport()
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
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
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
           bindData();
       }
       private void bindData()
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
                   //cryFeeDetailReport.dt1 = dateTimePicker1.Value;
                   //cryFeeDetailReport.dt2 = dateTimePicker2.Value;
                   cryBusStopReport.classid = classname;
                   //cryFeeDetailReport.hostel = hostel;

                   cryBusStopReport obj = new cryBusStopReport();
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

        private void frmBusStopReport_Load(object sender, EventArgs e)
        {
            bindclass();
        }

       
    }
}