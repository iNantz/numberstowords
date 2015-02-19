using Numbers.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numbers.Service
{
    public static class Extensions
    {
        private static Regex moDigits = new Regex(@"[^\d]");
        private static string msConfigFile = ConfigurationManager.AppSettings["jsonfile"] ?? "..\\..\\..\\JSon\\en-US.json"; // configuration file for multilingual
        private static NumberWords moNbrWord = Helpers.JSONStringToNumberWords(msConfigFile) ?? new NumberWords();

        /// <summary>
        /// Get the all the digits
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDigits(this string str)
        {
            return moDigits.Replace(str, "");
        }

        /// <summary>
        /// Convert Number to Words
        /// </summary>
        /// <param name="number">number to be converted to words</param>
        /// <param name="currency">convert it to currency or not</param>
        /// <returns></returns>
        public static string ToWords(this string number, bool currency = false)
        {
            if (string.IsNullOrEmpty(number))
                return number;

            double ldNumber;            

            // check if the number is valid
            if (!double.TryParse(number, NumberStyles.Currency, moNbrWord.Culture, out ldNumber))
                return "Invalid Number";

            if (number.Contains(","))
                number = number.Replace(",", "");

            //  return the converted words
            return string.Format("{0}{1} {2}",
                                ldNumber < 0 ? string.Format("{0} ", moNbrWord.Negative.Trim()) : "",
                                Helpers.ConvertWholeNumbersToWords(number, moNbrWord, currency),
                                Helpers.ConvertDecimalsToWords(number, moNbrWord, currency)).Trim();
        }
    }
}
