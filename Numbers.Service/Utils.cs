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
            // call the string extension to convert the number to words
            words = number.ToWords(currency);
        }

        #endregion
    }
}
