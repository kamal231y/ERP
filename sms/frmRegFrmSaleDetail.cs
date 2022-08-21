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
    public partial class frmRegFrmSaleDetail : Form
    {
        string usertype;
       SqlDataReader dr;
       public frmRegFrmSaleDetail()
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
            //btnupdate.Enabled = false;
            //btn_delete.Enabled = false;
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
             if (cmbuser.Text == "-Select-"||cmbuser.Text=="")
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
            string str = "select session, id,(CAST(Formnoid as varchar)+'/'+cast(YEAR(Bdate)as varchar)) as FormNo,Name,Bdate,Amount,Userid  from tblFormRegistration where session = '" + cmbsession.Text + "'  and Bdate>='" + dt1.ToString("MM/dd/yyyy") + "' and Bdate<='" + dt2.ToString("MM/dd/yyyy") + "'";
            SqlDataAdapter da = new SqlDataAdapter(str,sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            
           

            sc.Close(); 
        
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                
                cryRegFormSale.dt1 = dateTimePicker1.Value;
                cryRegFormSale.dt2 = dateTimePicker2.Value;

                cryRegFormSale.usertype = usertype;
                cryRegFormSale obj = new cryRegFormSale();

                obj.ShowDialog();

            }
            else
            {
                MsgBox.ShowBox("No Records Found to be Print....");
            }
               
         }

       
       
       
        private void label8_Click(object sender, EventArgs e)
        {

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
            row["UserType"] = "-Select-";
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


        private void frmRegFrmSaleDetail_Load(object sender, EventArgs e)
        {
            //Bindsession();
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }
         
    }  
      
 }

