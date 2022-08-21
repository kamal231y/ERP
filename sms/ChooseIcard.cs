using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.DAL;
using sms.BLClass;
using System.Collections;
using System.IO;
using System.Reflection;

namespace sms
{
    public partial class ChooseIcard : Form
    {
       

        int A;
        public ChooseIcard()
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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            //cryICard.classid = classid;
            //string Admno, Roll,session;
            //cryICard.session=session.ToString();
            //cryICard.Admno = Admno.ToString();
            //cryICard obj = new cryICard();
            //obj.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (D1.Checked==true)
            {
                A = 1;
            }
            else if (D2.Checked == true)
            {
                A = 2;
            }
            else if (D3.Checked == true)
            {
                A = 3;
            }
            else if (D4.Checked == true)
            {
                A = 4;
            }
            else
            {
                A = 0;
            }

            try
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Update_SettingICard", sc);
                da.SelectCommand.CommandTimeout = 300;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@icard", A);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                MsgBox.ShowBox("Data Save Successfully");
                
            }
            catch (Exception ex) { }
         
        }


        private void bind_data()
        {
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SETTING_T ", sc);
                    sc.Open();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    
                   int A = Convert.ToInt16(dt.Rows[0]["type"].ToString());
                    if(A==1)
                    {
                        D1.Checked = true;
                    }
                    else if (A == 2)
                    {
                        D2.Checked = true;
                    }
                    else if (A == 3)
                    {
                        D3.Checked = true;
                    }
                  else  if (A == 4)
                    {
                        D4.Checked = true;
                    }

                    else if (A == 5)
                    {
                        D5.Checked = true;
                    }
                    else if (A == 6)
                    {
                        D6.Checked = true;
                    }

                    else if (A == 7)
                    {
                        D7.Checked = true;
                    }


                    else if (A == 8)
                    {
                        D8.Checked = true;
                    }

                    else if (A == 9)
                    {
                        D9.Checked = true;
                    }

                    else if (A == 10)
                    {
                        D10.Checked = true;
                    }
                }
                catch (Exception EX) { }

            }
            
        }

        private void ChooseIcard_Load(object sender, EventArgs e)
        {
            bind_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

    }
}
