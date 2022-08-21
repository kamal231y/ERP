using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace sms.DAL
{
    class dataclass
    {
        public string system_name =Login.system_name, passwd = Login.passwd, userid = Login.userid, databasename = Login.databasename;

        public static string year;
        public SqlConnection con()
        {
            SqlConnection _cn;

            String constr = "server=" + system_name + ";password=" + passwd + ";user id=" + userid + ";database=" + databasename + ";";

            _cn = new SqlConnection(constr);
            
            return _cn;
        }

        public SqlConnection con1st()
        {
            SqlConnection _cn;
            _cn = new SqlConnection(ConfigurationSettings.AppSettings["Connection1"]);
            return _cn;
        }


        public int InsertRows(string sqlSP, ref ArrayList arrFields, bool returnValue)
        {
            SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            arrFields.Clear();
            arrFields.Add(com.Parameters[0].Value.ToString());
            sc.Dispose();
            return j;
        }
        public int InsertSMSUpload(string sqlsp, ArrayList arrFields)
        {
            SqlConnection sc = connsms();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlsp;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;
        }

        public  int yearname(int month, int year, bool isboard)
        {
            int jan=1, feb=2, mar=3, apr=4, may=5, jun=6, jul=7, aug=8, sep=9, oct=10, nov=11, dec=12;
            int yearnew = year;
            if (!isboard)
            {

                if (month > 9)
                    {
                        yearnew = year + 1;  
                    }
                    else
                    {
                       
                    }
            }
            else {
               
                    if (month > 6)
                    {
                        yearnew = year + 1;
                    }
                    else
                    {
                       
                    }
                
            }

            return yearnew;

        }




        public int InsertWebUpload(string sqlsp, ArrayList arrFields)
        {
            SqlConnection sc = conWeb();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlsp;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;
        }

        public int InsertRows1(string sqlSP, ref ArrayList arrFields, bool returnValue)
        {
            SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            arrFields.Clear();
            arrFields.Add(com.Parameters[0].Value.ToString());
            sc.Dispose();
            return j;
        }
        public char AllowOnlyNumber(char Key)
        {
            switch (Key)
            {
                case '9':
                    return '9';
                case '0':
                    return '0';
                case '1':
                    return '1';
                case '2':
                    return '2';
                case '3':
                    return '3';
                case '4':
                    return '4';
                case '5':
                    return '5';
                case '6':
                    return '6';
                case '7':
                    return '7';
                case '8':
                    return '8';
                case '\b':
                    return '\b';
                default:
                    return '\0';
            }
        }
        public char AllowNumberWithDecimal(char Key, string ObjectValue)
        {
            if (Key == '.')
            {
                if (ObjectValue.Contains("."))
                    return '\0';
            }
            switch (Key)
            {
                case '9':
                    return '9';
                case '0':
                    return '0';
                case '1':
                    return '1';
                case '2':
                    return '2';
                case '3':
                    return '3';
                case '4':
                    return '4';
                case '5':
                    return '5';
                case '6':
                    return '6';
                case '7':
                    return '7';
                case '8':
                    return '8';
                case '.':
                    return '.';
                case '\b':
                    return '\b';
                default:
                    return '\0';
            }
        }
        public string RoundStringToNumber(string Number, int DecimalPlace)
        {
            if (Number == "" || Number == ".")
                return "0.00";
            else
            {
                double tmp = Math.Round(Convert.ToDouble(Number), DecimalPlace);
                return tmp.ToString("N2");
            }
        }
        public string RoundNumber(double number, int DecimalPlace)
        {
            number = Math.Round(number, DecimalPlace);
            return number.ToString("N2");
        }

        public int uppercase(int Key)
        {
            if (Key >= 97 && Key <= 122)
                Key = Key - 32;
            return Key;
        }

        public int FirstLetterUppercase(int Key, string ControlValue)
        {
            if (Key >= 97 && Key <= 122 && ControlValue == "")
                Key = Key - 32;
            return Key;
        }
        //public SqlConnection conweb()
        //{
        //    SqlConnection _cn;
        //    {
        //        _cn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionWeb"]);
        //    }
        //    return _cn;
        //}
        public SqlConnection connWeb()
        {
            SqlConnection _cn = new SqlConnection("Data Source=tcp:eprincipal.database.windows.net;Initial Catalog=issaap;uid=eprincipal;pwd=ISS@infotech#1");
            return _cn;
        }
        public SqlConnection conWeb()
        {
            SqlConnection _cn = new SqlConnection("Data Source=tcp:eprincipal.database.windows.net;Initial Catalog=issaap;uid=eprincipal;pwd=ISS@infotech#1");
            return _cn;
        }
        public SqlConnection conweb()
        {
            SqlConnection _cn = new SqlConnection("Data Source=tcp:eprincipal.database.windows.net;Initial Catalog=issaap;uid=eprincipal;pwd=ISS@infotech#1");
            return _cn;
        }
        //public SqlConnection conWeb()
        //{
        //    SqlConnection _cn = new SqlConnection("Data Source=sqlplesk4.securehostdns.com,1533;Initial Catalog=issnmpublicschool;uid=issnmpublicschool;pwd=ISSS###santosh@@@ISSS");
        //    return _cn;
        //}


        public SqlConnection connsms()
        {
            SqlConnection _cn = new SqlConnection("Data Source=103.133.215.8;Initial Catalog=isssms;uid=isssms;pwd=ISS@infotech#1");
            return _cn;
        }


        internal void ExportDataSetToExcel(DataSet dt)
        {
            throw new NotImplementedException();
        }
    }
}
