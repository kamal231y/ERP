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
    public partial class frmStockReport : Form
    {
        string da;
        string classname;
        string pubId;
        SqlDataReader dr;
        public frmStockReport()
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
            SqlDataAdapter da = new SqlDataAdapter(" select  ITM_ID,ITM_DESC from tblitem order by ITM_DESC", sc);
           // SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID, CLS_DESC  as Class  from CLASS_T  order by CLS_Sequence ", sc);

            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["ITM_ID"] = 0;
            row["ITM_DESC"] = "-Select-";

            ds.Tables[0].Rows.InsertAt(row, 0);

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "ITM_DESC";
            comboBox1.ValueMember = "ITM_ID";
        }

        private void bindPublisher()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblPublisher ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Pub_id"] = 0;
            row["Pub_Name"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Pub_Name";
            cmbclass.ValueMember = "Pub_id";
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
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
               {
                   pubId = "%";
               }
               else
               {
                   pubId = cmbclass.SelectedValue.ToString();
               }
               if (comboBox1.Text == "" || comboBox1.Text == "-Select-")
               {
                   classname = "%";
               }
               else
               {
                   classname = comboBox1.SelectedValue.ToString();
               }
               
              cryStockPublisherWise.dt1 = dateTimePicker1.Value;
              cryStockPublisherWise.dt2 = dateTimePicker2.Value;
              cryStockPublisherWise.pubId = pubId;
              cryStockPublisherWise.classid = classname;
              cryStockPublisherWise obj = new cryStockPublisherWise();                   
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


        private void frmStockReport_Load(object sender, EventArgs e)
        {
            bindclass();
            bindPublisher();
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            bindData();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }     
    }
}