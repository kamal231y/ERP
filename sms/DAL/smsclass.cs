using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Net;
using System.IO;
using MsgBoxRet;
using System.Net.NetworkInformation;


namespace sms.DAL
{
    class smsclass
    {
        string msgname = "";
        string msgid = "";
        WebRequest request = null;
        HttpWebResponse response = null; string result = "";




       
        public string SMS(string msgtxt, int mode, string Mobile, string Name)
        {
            try
            {


                bool i = true ; //checkNetworkConnection();
                if (i == true)
                {

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.connsms();
                    SqlDataAdapter da = new SqlDataAdapter("select * from  TBLSMS where name='" + Form1.SMSKEY + "' and balance>0", sc);
                 //   SqlDataAdapter da = new SqlDataAdapter("select * from  setting_t where SMSKEY='" + Form1.SMSKEY + "'", sc);

                    DataTable dtsms = new DataTable();
                    da.Fill(dtsms);
                    if (dtsms.Rows.Count > 0)
                    {
                        msgid = dtsms.Rows[0]["userid"].ToString().Trim();
                        msgname = dtsms.Rows[0]["Name"].ToString().Trim();
                        string msgpwd = dtsms.Rows[0]["password"].ToString().Trim();
                        if (mode == 0)
                        {
  
                            String baseurl =
                                "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + msgpwd + "&message=" + msgtxt.TrimEnd().TrimStart() + " &senderId=" + msgid + "&routeId=1&mobileNos=" + Mobile + "&smsContentType=english";
                        // "http://isssms.msg4all.com/GatewayAPI/rest?method=sendMessage&send_to=" + Mobile + "&msg=" + msgtxt.TrimEnd().TrimStart() + " &loginid=" + msgid + "&password=" + msgpwd + "&v=1.1&msg_type=text&auth_scheme=PLAIN&mask=" + msgname + "";
                            request = WebRequest.Create(baseurl);
                        }
                        else
                        {
                            String baseurl =
                                "http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + msgpwd + "&message=" + msgtxt.TrimEnd().TrimStart() + " &senderId=" + msgid + "&routeId=1&mobileNos=" + Mobile + "&smsContentType=Unicode";
                            //  "http://isssms.msg4all.com/GatewayAPI/rest?method=sendMessage&send_to=" + Mobile + "&msg=" + msgtxt.TrimEnd().TrimStart() + " &loginid=" + msgid + "&password=" + msgpwd + "&v=1.1&msg_type=unicode_text&auth_scheme=PLAIN&mask=" + msgname + "";
                            request = WebRequest.Create(baseurl);
                        }
                        response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new
                        System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        string[] msgarray = result.Split('"');
                        if (msgarray.Length > 0)
                        {
                          //  if (msgarray[0].ToString().Trim() == "success")
                            if (msgarray[3].ToString().Trim() == "3001")
                            {
                                SaveSMS(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "1",mode);
                                SaveSMSweb(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "1",msgid,mode);
                            }
                            else
                            {
                                SaveSMS(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "2",mode);
                                SaveSMSweb(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "2", msgid,mode);
                            }

                        }
                        else
                        {
                            SaveSMS(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "2",mode);
                            SaveSMSweb(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "2", msgid,mode);
                        }

                    }
                    else
                    {
                        MsgBox.ShowBox("Don't have enough funds in your SMS balance");

                    }


                }

                else
                {
                   // MsgBox.ShowBox("There is No Internet Connection");

                }
            }
            catch (Exception ex)
            {
                SaveSMS(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "2",mode);
                SaveSMSweb(Mobile, Name, msgtxt.TrimEnd().TrimStart(), "2", msgid,mode);

            }
            return result;

        }

        private bool checkNetworkConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void SaveSMS(string name, string mobile, string status, string msg, int mode)
        {
            try
            {
                dataclass dc = new dataclass();
                SqlConnection conn = dc.con();
                SqlCommand objCmd = new SqlCommand("Insert_tblsmsreport '" + name + "','" + mobile + "',N'" + status + "','" + msg + "'," + mode + "", conn);

                SqlDataReader dr;
                objCmd.Connection.Open();
                objCmd.ExecuteNonQuery();
                objCmd.Connection.Close();
            }
            catch (Exception ex)
            {
            }
        }
        private void SaveSMSweb(string name, string mobile, string status, string msg, string msgid, int mode)
        {
            try
            {

                dataclass dc = new dataclass();
                SqlConnection conn = dc.connsms();
                SqlCommand objCmd = new SqlCommand("Insert_tblsmsreport '" + name + "','" + mobile + "',N'" + status + "','" + msg + "','" + msgname + "'," + mode + "", conn);
                SqlDataReader dr;
                objCmd.Connection.Open();
                objCmd.ExecuteNonQuery();
                objCmd.Connection.Close();



            }

            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.ToString());
            }
        }

    }
}

