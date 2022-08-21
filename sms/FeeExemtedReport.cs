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
    public partial class FeeExemtedReport : Form
    {
       
        
       string classname;
       SqlDataReader dr;
       public FeeExemtedReport()
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
            
          
            
            
        }
        

            
        private void Cancil_form()
        {
            panelsearch.Visible = true;
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
            cmbclass.Text = "Select";
          
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            Cancil_form();
         

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btn_save_Click(object sender, EventArgs e)
        {
          
        
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

   
        private void frmDuesbalance_Load(object sender, EventArgs e)
        {

            bindclass();
          
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void btnPrint_Click_1(object sender, EventArgs e)
        {

            if (cmbclass.Text == "Select" || cmbclass.Text == "" || cmbclass.Text == "System.Data.DataRowView")
            {
                classname = null;
                CryExemptedFeeReport.Class = classname;
                CryExemptedFeeReport obj = new CryExemptedFeeReport();
                obj.ShowDialog();

            }
            else
            {
                classname = cmbclass.SelectedValue.ToString();
                CryExemptedFeeReport.Class = classname;
                CryExemptedFeeReport obj = new CryExemptedFeeReport();
                obj.ShowDialog();

            }
        }

       
      
       
        }       
    }

