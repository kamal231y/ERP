using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//using barcodeArgox;
//using BarcodeTSC_IN_C_Sharp;

namespace sms.BLClass
{

    class BLclass
    {

        private static string _UserName = "Neeraj Chauhan";

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

        public string GenBarcode(DateTime GenerationDateDDMMYYYY, string LastBarcode)
        {


            if (LastBarcode != "")
            {
                LastBarcode = LastBarcode.Substring(0, 2) + GenerationDateDDMMYYYY.Year.ToString().Substring(2, 2) + LastBarcode.Substring(4, 4);
                char[] ch = new char[1];
                for (int i = 1; i <= LastBarcode.Length; i++)
                {
                    if (i == 5)
                        i = i + 2;
                    if (i <= 3)
                    {
                        if (LastBarcode.Substring(LastBarcode.Length - i, 1) == "9")
                        {
                            LastBarcode = LastBarcode.Insert(LastBarcode.Length - i, "0");
                            LastBarcode = LastBarcode.Remove(LastBarcode.Length - i, 1);
                            continue;
                        }
                        else
                        {
                            LastBarcode = LastBarcode.Substring(0, LastBarcode.Length - i) + Convert.ToString(Int32.Parse(LastBarcode.Substring(LastBarcode.Length - i, 1)) + 1) + LastBarcode.Substring(LastBarcode.Length - (i - 1), (i - 1));
                            return LastBarcode;
                        }
                    }
                    else
                    {
                        if (LastBarcode.Substring(LastBarcode.Length - i, 1) == "Z")
                        {
                            LastBarcode = LastBarcode.Insert(LastBarcode.Length - i, "A");
                            LastBarcode = LastBarcode.Remove(LastBarcode.Length - i, 1);
                            continue;
                        }
                        else
                        {
                            ch = (char[])LastBarcode.Substring(LastBarcode.Length - i, 1).ToCharArray();
                            ch[0] = (char)((int)ch[0] + 1);
                            LastBarcode = LastBarcode.Insert(LastBarcode.Length - i, ch[0].ToString());
                            LastBarcode = LastBarcode.Remove(LastBarcode.Length - i, 1);
                            return LastBarcode;
                        }
                    }
                }
            }
            else
                return "JP" + GenerationDateDDMMYYYY.Year.ToString().Substring(2, 2) + "A000";

            return LastBarcode;
        }
        public static string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public string GenBarcodeBox(DateTime GenerationDateDDMMYYYY, string LastBarcode)
        {
            if (LastBarcode != "")
            {
                LastBarcode = LastBarcode.Substring(0, 2) + GenerationDateDDMMYYYY.Year.ToString().Substring(2, 2) + LastBarcode.Substring(4, 4);
                char[] ch = new char[1];
                for (int i = 1; i <= LastBarcode.Length; i++)
                {
                    if (i == 5)
                        i = i + 2;
                    if (i <= 3)
                    {
                        if (LastBarcode.Substring(LastBarcode.Length - i, 1) == "9")
                        {
                            LastBarcode = LastBarcode.Insert(LastBarcode.Length - i, "0");
                            LastBarcode = LastBarcode.Remove(LastBarcode.Length - i, 1);
                            continue;
                        }
                        else
                        {
                            LastBarcode = LastBarcode.Substring(0, LastBarcode.Length - i) + Convert.ToString(Int32.Parse(LastBarcode.Substring(LastBarcode.Length - i, 1)) + 1) + LastBarcode.Substring(LastBarcode.Length - (i - 1), (i - 1));
                            return "BX" + LastBarcode;
                        }
                    }
                    else
                    {
                        if (LastBarcode.Substring(LastBarcode.Length - i, 1) == "Z")
                        {
                            LastBarcode = LastBarcode.Insert(LastBarcode.Length - i, "A");
                            LastBarcode = LastBarcode.Remove(LastBarcode.Length - i, 1);
                            continue;
                        }
                        else
                        {
                            ch = (char[])LastBarcode.Substring(LastBarcode.Length - i, 1).ToCharArray();
                            ch[0] = (char)((int)ch[0] + 1);
                            LastBarcode = LastBarcode.Insert(LastBarcode.Length - i, ch[0].ToString());
                            LastBarcode = LastBarcode.Remove(LastBarcode.Length - i, 1);
                            return "BX" + LastBarcode;
                        }
                    }
                }
            }
            else
                return "BX" + "AA" + GenerationDateDDMMYYYY.Year.ToString().Substring(2, 2) + "A000";

            return LastBarcode;
        }



    }
}
