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
    public partial class frmvoucherDetail : Form
    {
        string usertype,vtype;
       SqlDataReader dr;
       public frmvoucherDetail()
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
             if (sms.Form1.GlobalLoginUserName.ToString() == "infotech")
             {
                 if (cmbuser.Text == "-Select-" || cmbuser.Text == "")
                 {
                     usertype = "%";
                 }
                 else
                 {
                     usertype = cmbuser.Text.ToString();
                 }

             }
             else
             {
                 usertype = sms.Form1.GlobalLoginUserName.ToString();
             }
             if (cmbvouchertype.Text == "-Select-" || cmbvouchertype.Text == "")
             {
                 vtype = "%";
             }
             else
             {
                 vtype = cmbvouchertype.SelectedValue.ToString();
             }
            

            
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            string str = "select VTYPE,VNO,NAME,DES1,CREDIT,DEBIT from viewVoucherDetail where  vtype='" + vtype + "' and vdate between '" + dt1.ToString("MM/dd/yyyy") + "' and '" + dt2.ToString("MM/dd/yyyy") + "'AND LEDGERID!=1";
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
                if (sms.Form1.GlobalLoginUserName.ToString() == "infotech")
                {
                    if (cmbuser.Text == "-Select-" || cmbuser.Text == "")
                    {
                        usertype = "%";
                    }
                    else
                    {
                        usertype = cmbuser.Text.ToString();
                    }

                }
                else
                {
                    usertype = sms.Form1.GlobalLoginUserName.ToString();
                }
                if (cmbvouchertype.Text == "-Select-" || cmbvouchertype.Text == "")
                {
                    vtype = "%";
                }
                else
                {
                    vtype = cmbvouchertype.SelectedValue.ToString();
                }

                cryvoucherdetail.dt1 = dateTimePicker1.Value;
                cryvoucherdetail.dt2 = dateTimePicker2.Value;

                cryvoucherdetail.usertype = usertype;
                cryvoucherdetail.vtype = vtype;
                cryvoucherdetail obj = new cryvoucherdetail();

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
            SqlCommand cmd = new SqlCommand("select id,UserName from tblRights   order by UserName", sc);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow row = dt.NewRow();
            row["UserName"] = "-Select-";
            row["id"] = 0;
            dt.Rows.InsertAt(row, 0);

            cmbuser.DataSource = dt;
            cmbuser.DisplayMember = "UserName";
            cmbuser.ValueMember = "id";
            sc.Close();
        }
        private void bindGroup()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblVoucherType", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["vtname"] = "-Select-";
            row["vtid"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbvouchertype.DataSource = ds.Tables[0];
            cmbvouchertype.DisplayMember = "VTName";
            cmbvouchertype.ValueMember = "VTid";

        }
        private void frmRegFrmSaleDetail_Load(object sender, EventArgs e)
        {
           Binduser();
           bindGroup();
           if (sms.Form1.GlobalLoginUserName.ToString() == "infotech")
           {
               label8.Visible = true;
               cmbuser.Visible = true;
           }
           else
           {
               label8.Visible = false;
               cmbuser.Visible = false;
           }
        }
         
    }  
      
 }

