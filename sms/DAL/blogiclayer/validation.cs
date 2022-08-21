using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;

namespace sms
{
    class validation
    {
        public void digitonly(KeyPressEventArgs e)
        {
            try
            {
                if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsPunctuation(e.KeyChar)))//|| char.IsSurrogatePair(e.KeyChar)))
                {

                    MessageBox.Show("Enter only digit and decimal point.", "Alert!");
                    e.Handled = true;
                }
                //if (char.IsLetter(e.KeyChar) == false && Convert.ToInt32(e.KeyChar) != 32)
                //{
                //    MessageBox.Show("Enter alphabet only");
                //    e.Handled = true;
                //}

            }
            catch { }
        }
        public void charonly(KeyPressEventArgs e)
        {
            //if (Char.IsNumber(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            //{
            //    MessageBox.Show("Only Char are allowed");
            //    e.Handled = true;
            //}


            if (char.IsLetter(e.KeyChar) == false && Convert.ToInt32(e.KeyChar) != 32 && Convert.ToInt32(e.KeyChar) != 8)
            {
                MessageBox.Show("Enter alphabet only");
                e.Handled = true;
            }



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


    }
}
