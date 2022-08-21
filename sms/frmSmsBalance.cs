using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using sms.DATALAYER;
using System.Data.SqlClient;
using System.Data.OleDb;
using MsgBoxRet;
using System.Reflection;


namespace sms
{
    public partial class frmSmsBalance : Form
    {
        WebClient client = new WebClient();
        dataclass dc = new dataclass();
        // SqlConnection consms = new SqlConnection("Data Source=sqlplesk4.securehostdns.com,1533;Initial Catalog=tblsms;uid=tblsms;pwd=ISSS###santosh@@@ISSS");
        public frmSmsBalance()
        {
            InitializeComponent();
        }





        private void sms1()
        {
            //try
            //{
            //    dataclass dc = new dataclass();
            //    SqlConnection sc = dc.con();
            //    SqlDataAdapter da = new SqlDataAdapter("select userid,password from tblsms ", sc);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    if (dt.Rows.Count > 0)
            //    {
            //        string username = dt.Rows[0][0].ToString();
            //        string pass = dt.Rows[0][1].ToString();

            //        da = new SqlDataAdapter("select  last_balance from tblsmsbalance  where username='" + username + "'", con);
            //        DataTable dt1 = new DataTable();
            //        da.Fill(dt1);
            //        if (dt1.Rows.Count > 0)
            //        {
            //            string baseurl = "http://sms.crossatlantic.net:8080/CreditCheck/checkcredits?username=" + username + "&password=" + pass;
            //            Stream data = client.OpenRead(baseurl);
            //            StreamReader reader = new StreamReader(data);
            //            string s = reader.ReadToEnd();
            //            string[] msg = s.Split(':');
            //            string Value = msg[1].ToString();
            //            int count = Convert.ToInt32((Convert.ToDecimal(Value) + Convert.ToDecimal(dt1.Rows[0][0].ToString())));
            //            label10.Text = "Your Balance:  " + count.ToString() + " SMS";
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    label10.Text = ex.ToString();
            //}


            try
            {
                string name = "";
                dataclass dc1 = new dataclass();
                SqlConnection sc1 = dc1.con();
                SqlDataAdapter da1 = new SqlDataAdapter("select userid from tblsms where name='" + Form1.SMSKEY + "' ", sc1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    name = dt1.Rows[0][0].ToString();
                }
                dataclass dc = new dataclass();
                SqlConnection sc = dc.connsms(); // dc.connWeb();
                SqlDataAdapter da = new SqlDataAdapter("select balance from tblsms where name='" + Form1.SMSKEY + "'  ", sc);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    label10.Text = "Your Balance:  " + dt.Rows[0][0].ToString() + " SMS";
                }
            }
            catch (Exception ex)
            {
                label10.Text = ex.ToString();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSmsBalance_Load(object sender, EventArgs e)
        {
            sms1();
        }












    }
}