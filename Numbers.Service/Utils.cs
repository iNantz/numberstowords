using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Numbers.IService;
using System.Globalization;
using System.Runtime.Serialization.Json;
using System.Configuration;

namespace Numbers.Service
{
    public partial class Utils : IUtils
    {
        #region Implementation

        /// <summary>
        /// Convert the number to words
        /// </summary>
        /// <param name="number">number to be converted</param>
        /// <param name="currency">determine if convertion for currency or not</param>
        /// <param name="words">converted numbers to word as output parameter</param>
        /// <returns>the number in words</returns>
        public void NumberToWords(string number, out string words, bool currency = false)
        {
            double ldNumber;
            var loNbrWord = new NumberWords();

            // check if the number is valid
            if (!double.TryParse(number, NumberStyles.Currency, loNbrWord.Culture, out ldNumber))
            {
                words =  "Invalid Number";
                return;
            }

            if (number.Contains(","))
                number = number.Replace(",", "");

                //  return the converted words
            words =  string.Format("{0}{1} {2}",
                                ldNumber < 0 ? string.Format("{0} ", loNbrWord.Negative.Trim()) : "",
                                Helpers.ConvertWholeNumbersToWords(number, loNbrWord, currency),
                                Helpers.ConvertDecimalsToWords(number, loNbrWord, currency)).Trim();
        }

        #endregion
    }
}
