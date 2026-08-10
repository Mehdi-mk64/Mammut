using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class PersianUtilities
    {
        public static string ToPersianDate(this DateTime englishDate)
        {
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();

            return $"{calendar.GetYear(englishDate).ToString().GetFarsiNumber()}/{calendar.GetMonth(englishDate).ToString().PadLeft(2, '0').GetFarsiNumber()}/{calendar.GetDayOfMonth(englishDate).ToString().PadLeft(2, '0').GetFarsiNumber()}";
        }
        public static string ToPersianDate(this DateTime? date) => (date.HasValue) ? ToPersianDate(date.Value) : string.Empty;


        public static string ToLetterPersinaDate(this DateTime englishDate)
        {
            System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();

            return $"{calendar.GetDayOfMonth(englishDate).ToString().GetFarsiNumber()}/{calendar.GetMonth(englishDate).ToString().PadLeft(2, '0').GetFarsiNumber()}/{calendar.GetYear(englishDate).ToString().GetFarsiNumber()}";
        }
        public static string GetPersianNumber(this string englishNumber)
        {
            string persianNumber = "";
            foreach (char ch in englishNumber)
            {
                persianNumber += (char)(1776 + char.GetNumericValue(ch));
            }
            return persianNumber;
        }

        public static string GetEnglishNumber(this string persianNumber)
        {
            string englishNumber = "";
            foreach (char ch in persianNumber)
            {
                switch (ch)
                {
                    case '۰':
                        englishNumber += '0';
                        break;
                    case '۱':
                        englishNumber += '1';
                        break;
                    case '۲':
                        englishNumber += '2';
                        break;
                    case '۳':
                        englishNumber += '3';
                        break;
                    case '۴':
                        englishNumber += '4';
                        break;
                    case '۵':
                        englishNumber += '5';
                        break;
                    case '۶':
                        englishNumber += '6';
                        break;
                    case '۷':
                        englishNumber += '7';
                        break;
                    case '۸':
                        englishNumber += '8';
                        break;
                    case '۹':
                        englishNumber += '9';
                        break;
                    default:
                        englishNumber += ch;
                        break;
                }
            }

            return englishNumber;
        }

        public static string GetFarsiNumber(this string englishNumber)
        {
            string persianNumber = "";

            foreach (char ch in englishNumber)
            {
                switch (ch)
                {
                    case '0':
                        persianNumber += '۰';
                        break;
                    case '1':
                        persianNumber += '۱';
                        break;
                    case '2':
                        persianNumber += '۲';
                        break;
                    case '3':
                        persianNumber += '۳';
                        break;
                    case '4':
                        persianNumber += '۴';
                        break;
                    case '5':
                        persianNumber += '۵';
                        break;
                    case '6':
                        persianNumber += '۶';
                        break;
                    case '7':
                        persianNumber += '۷';
                        break;
                    case '8':
                        persianNumber += '۸';
                        break;
                    case '9':
                        persianNumber += '۹';
                        break;
                    default:
                        persianNumber += ch;
                        break;
                }
                
                
            }

            return persianNumber;


        }

        public static string EnglishPersionDateToFA(this string englishDate)
        {
            return $" {englishDate.Substring(8, 2).GetFarsiNumber():00}/{englishDate.Substring(5, 2).GetFarsiNumber():00}/{englishDate.Substring(0, 4).GetFarsiNumber()} ";
        }





    }
}
