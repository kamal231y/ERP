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
using sms.BLClass;

namespace sms
{
    public partial class cryRTECCEEcertificate: Form
    {
        public static DateTime dt1, dt2;
        public static string classid;
        public static string Roll, Admno, refid, year, month, session;
        String sDate;


        public cryRTECCEEcertificate()
        {
            InitializeComponent();
        }

      

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            string str;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            if (Admno == "-Select-" || Admno == "")
            {

                str = "select * from View_RTE_Student where stu_status='y' and DATEPART(DAY,STU_DOB)=DATEPART(DAY,GETDATE()) and DATEPART(MONTH,STU_DOB)=DATEPART(MONTH,GETDATE()) order by STU_ADM_NO ";
            }
            else
            {
                str = "select * from View_RTE_Student where stu_status='y' and STU_ADM_NO like '" + Admno + "'order by STU_ADM_NO ";

            }
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "View_RTE_Student");
            cRptRTECCEE cc = new cRptRTECCEE();
            cc.SetDataSource(ds1.Tables["View_RTE_Student"]);
            //
            //if (ds1.Tables["View_RTE_Student"].Rows.Count > 0)
            //{
            //    sDate = Convert.ToDateTime(ds1.Tables["View_RTE_Student"].Rows[0]["STU_DOB"].ToString()).ToString("dd/MM/yyyy");

            //    string[] str11 = sDate.Split('/');
            //    string dy = NumberToWords(Convert.ToInt32(str11[0].ToString()));
            //    string mn = datetotextconvert(Convert.ToInt32(str11[1].ToString()));
            //    string yy = NumberToWords(Convert.ToInt32(str11[2].ToString()));

            //    cc.SetParameterValue("dy", dy);
            //    cc.SetParameterValue("mn", mn);
            //    cc.SetParameterValue("yy", yy);



            //}



            da = new SqlDataAdapter("select * from company_t", sc);
            ds1 = new DataSet();
            da.Fill(ds1, "company_t");
            cc.SetParameterValue("pageheader", ds1.Tables[0].Rows[0]["com_name"].ToString());
            cc.SetParameterValue("compayAddress", ds1.Tables[0].Rows[0]["companyAddress"].ToString());
            cc.SetParameterValue("examn","Phone No.:"+ ds1.Tables[0].Rows[0]["companytelno"].ToString());
            cc.SetParameterValue("affilino", ds1.Tables[0].Rows[0]["AFFLIATION_CODE"].ToString());
            cc.SetParameterValue("email", "Email ID:" + ds1.Tables[0].Rows[0]["EMAIL"].ToString());
            //cc.SetParameterValue("refid", refid + "/" + month + "/" + year);


            da = new SqlDataAdapter("select * from TBLRIGHTS", sc);
            DataSet ds2 = new DataSet();
            da.Fill(ds2, "TBLRIGHTS");
            //cc.SetParameterValue("refid", refid + "/" + month + "/" + year);
            cc.SetParameterValue("refid", ds2.Tables[0].Rows[0]["username"] + "/RTE" + " /" + Admno + " /" + ds1.Tables[0].Rows[0]["SESSION"].ToString() + " /" + refid.ToString());



            //da = new SqlDataAdapter("select * from tbl_Birth_approved", sc);
            //DataSet ds3 = new DataSet();
            //da.Fill(ds3, "tbl_Birth_approved");
            //cc.SetParameterValue("refid", refid + "/" + month + "/" + year);
            //cc.SetParameterValue("approvedby", ds3.Tables[0].Rows[0]["approvedby"].ToString());

          
            
            crystalReportViewer1.ReportSource = cc;
            crystalReportViewer1.Refresh();

        }

        public string datetotextconvert(int mn)
        {

            string month="";
            if (mn == 1)
            {
                month = "January";
            }
            else if (mn == 2)
            {
                month = "February";
            }
            else if (mn == 3)
            {
                month = "March";
            }
            else if (mn == 4)
            {
                month = "April";
            }
            else if (mn == 5)
            {
                month = "May";
            }
            else if (mn == 6)
            {
                month = "June";
            }
            else if (mn == 7)
            {
                month = "July";
            }
            else if (mn == 8)
            {
                month = "August";
            }
            else if (mn == 9)
            {
                month = "September";
            }
            else if (mn == 10)
            {
                month = "October";
            }
            else if (mn == 11)
            {
                month = "November";
            }
            else if (mn == 12)
            {
                month = "December";
            }

              
                return month;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                    //words += unitsMap[number % 10];
                }
            }

            return words;
        }

        private void cryagecertificate_Load(object sender, EventArgs e)
        {

        }




    }
}
