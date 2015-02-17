using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Runtime.Serialization.Json;
using Numbers.IService;

namespace Numbers.Service
{
    public static class Helpers
    {
        /// <summary>
        /// Convert the whole numbers to words
        /// </summary>
        /// <param name="nbr">the number to be convereted</param>
        /// <param name="nbrWords">the definition of number words</param>
        /// <param name="currency">determine if convertion for currency or not</param>
        /// <returns>the number in words</returns>
        public static string ConvertWholeNumbersToWords(string nbr, NumberWords nbrWords, bool currency = true)
        {
            if (nbrWords == null)
                nbrWords = new NumberWords();

            var loNumbers = nbr.Split(new string[] { nbrWords.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.None);

            if (string.IsNullOrWhiteSpace(loNumbers[0]))
                return string.Empty;

            // group the numbers in 3 digit
            var lo3DigitGroups = Helpers.moGet3DigitGroups(loNumbers[0], nbrWords);

            if (lo3DigitGroups == null)
                return string.Empty;

            // reverse digits
            lo3DigitGroups = lo3DigitGroups.Reverse().ToArray();

            // add the word number in 3 digit group
            var lo3DigitGroupsWithWord = lo3DigitGroups.Select((r, idx) =>
                                                        new KeyValuePair<string, int>(nbrWords.Groups[idx], r))
                                                        .Reverse();

            // get the coverted words
            var lo3DigitWords = lo3DigitGroupsWithWord.Where(r => r.Value > 0).Select(r =>
                                                       string.Format("{0} {1}",
                                                            msConvert3DigitsToWords(r.Value, nbrWords),
                                                            r.Key == nbrWords.Groups[0] ? "" : r.Key));

            // set the currency name
            string currencyName = string.Format("{0}{1}", nbrWords.CurrencyName, lo3DigitGroups[0] > 1 ? "s" : "");

            if (!currency)
                currencyName = string.Empty;

            return string.Format("{0} {1}",
                lo3DigitWords.Aggregate((r, j) => string.Format("{0}, {1}", r, j)).Trim(),
                currencyName).Trim();
        }

        /// <summary>
        /// convert the decimal part in currency words
        /// </summary>
        /// <param name="nbr">the number to be convereted</param>
        /// <param name="nbrWords">the definition of number words</param>
        /// <param name="currency">determine if convertion for currency or not</param> 
        /// <returns>the number in words</returns>
        public static string ConvertDecimalsToWords(string nbr, NumberWords nbrWords, bool currency = true)
        {
            if (nbrWords == null)
                nbrWords = new NumberWords();

            double ldWholeNbr = 0;
            double ldDecNbr = 0;
            double ldNbr = 0;

            double.TryParse(nbr, NumberStyles.Currency, nbrWords.Culture, out ldNbr);

            // will always make the number in positive
            if (ldNbr < 0)
                nbr = nbr.Replace("-", "");

            // save orig currency symbol
            var lsOldCurrencySymbol = nbrWords.NumberFormat.CurrencySymbol;

            // remove the currency symbol
            nbrWords.NumberFormat.CurrencySymbol = "";

            // split number by its decimal separator
            var loNumbers = nbr.Split(new string[] { nbrWords.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.None);

            // return empty if no decimal
            if (loNumbers.Length == 1)
                return string.Empty;

            // get the whole number
            double.TryParse(loNumbers[0].GetDigits(), out ldWholeNbr);

            // get the decimal
            if (loNumbers.Length > 1)
                double.TryParse(loNumbers[1].GetDigits(), out ldDecNbr);

            // return empty if 0
            if( ldDecNbr == 0)
                return string.Empty;

            // back the original currency symbol
            nbrWords.NumberFormat.CurrencySymbol = lsOldCurrencySymbol;

            if (currency)
            {
                // convert the decimal string to money and get only the rounded off cents
                var loDecimal = double.Parse(string.Format(".{0}", loNumbers[1].GetDigits())).ToString("C", nbrWords.Culture).Split(new string[] { nbrWords.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.None);             

                // return cents in words
                return string.Format("{0}{1} {2}{3}",
                    ldWholeNbr > 0 ? nbrWords.And : "",
                    msConvertTensToWords(int.Parse(loDecimal[1]), nbrWords),
                    nbrWords.CentsName,
                    ldDecNbr > 1 ? "s" : "").Trim();
            }
            else
            {
                loNumbers = nbr.ToString().Split(new string[] { nbrWords.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.None);

                var lsNumber1 = loNumbers[1].GetDigits().TrimEnd(new Char[] { '0' });
                var loGroups = moGet3DigitGroups(lsNumber1, nbrWords);
                var lsDecimal = nbrWords.Groups[loGroups.Length];

                switch (moGroupNumbers(lsNumber1, 3).FirstOrDefault().ToString().Length)
                {
                    case 1:
                        lsDecimal = "Tenths";
                        if (lsNumber1.Length > 2)
                        {
                            lsDecimal = nbrWords.Groups[loGroups.Length - 1];
                            lsDecimal = string.Format("Ten-{0}ths", lsDecimal);
                        }
                        break;
                    case 2:
                        lsDecimal = "Hundredths";
                        if (lsNumber1.Length > 2)
                        {
                            lsDecimal = nbrWords.Groups[loGroups.Length - 1];
                            lsDecimal = string.Format("Hundred-{0}ths", lsDecimal);
                        }
                            
                        break;
                    default:
                        lsDecimal = string.Format("{0}ths", lsDecimal);
                        break;
                }

                // return cents in words
                return string.Format("{0}{1} {2}",
                    ldWholeNbr > 0 ? nbrWords.And : "",
                    ConvertWholeNumbersToWords(lsNumber1, nbrWords, false),
                    lsDecimal).Trim();
            }

        }

        /// <summary>
        /// get the list of 3 digit number
        /// </summary>
        /// <param name="nbr">the number to be convereted</param>
        /// <returns>the list of numbers in 3 digit</returns>
        private static int[] moGet3DigitGroups(string nbr, NumberWords nbrWords)
        {
            double ldNbr = double.Parse(nbr);

            // will always make the number in positive
            if (ldNbr < 0)
                nbr = nbr.Replace("-", "");

            // set to current number format
            if (nbrWords == null)
                return null;

            // save orig currency symbol
            var lsOldCurrencySymbol = nbrWords.NumberFormat.CurrencySymbol;

            // remove the currency symbol
            nbrWords.NumberFormat.CurrencySymbol = "";

            // split number by its decimal separator
            var loNumbers = nbr.Split(new string[] { nbrWords.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.RemoveEmptyEntries);

            // back the original currency symbol
            nbrWords.NumberFormat.CurrencySymbol = lsOldCurrencySymbol;

            if (double.Parse(loNumbers[0], nbrWords.Culture) == 0)
                return null;

            // get the numbers by group of 3
            return moGroupNumbers(loNumbers[0], 3).Select(r => int.Parse(r)).ToArray();
        }

        /// <summary>
        /// will get only the whole number and group it by specified number
        /// </summary>
        /// <param name="nbrs">numbers to be grouped</param>
        /// <param name="groupby">group by number of character</param>
        /// <returns>array of grouped integer</returns>
        private static IEnumerable<string> moGroupNumbers(string nbrs, int groupby = 3)
        {
            // get the remainder 
            var ldRem = nbrs.Length % groupby;
            var lsNbr = nbrs;

            // get the remainder string
            if (ldRem > 0)
                lsNbr = nbrs.Substring(ldRem);

            // group the numbers
            var loNbrs = lsNbr.Select((r, j) => j)
                .Where(r => r % groupby == 0)
                .Select(r => lsNbr.Substring(r, lsNbr.Length - r >= groupby ? groupby : lsNbr.Length - r)).ToList();

            // insert the remainder string
            if (ldRem > 0)
                loNbrs.Insert(0,  nbrs.Substring(0, ldRem));

            return loNbrs;
        }

        /// <summary>
        /// convert the 3 digit number into words
        /// </summary>
        /// <param name="hundred">the number to be converted</param>
        /// <param name="nbrWords">the definition of number words</param>
        /// <returns>the number in words</returns>
        private static string msConvert3DigitsToWords(int hundred, NumberWords nbrWords)
        {

            var liHundreds = hundred / 100;
            var liTens = hundred % 100;
            var lsHundred = "";

            // set the hundred number word
            if (liHundreds > 0)
                lsHundred = string.Format("{0} {1}", nbrWords.Ones[liHundreds], nbrWords.Groups[0]);

            return string.Format("{0} {1}",
                lsHundred,
                msConvertTensToWords(liTens, nbrWords)
                ).Trim();
        }

        /// <summary>
        /// Convert tens in Words
        /// </summary>
        /// <param name="tens">the tens number to be converted</param>
        /// <param name="nbrWords">the definition of number words</param>
        /// <returns></returns>
        private static string msConvertTensToWords(int tens, NumberWords nbrWords)
        {
            var lsTens = "";

            // set the tens number word
            if (tens > 0)
            {
                if (tens < 20)
                    lsTens = string.Format("{0}", nbrWords.Ones[tens]);
                else
                {
                    var liOnes = tens % 10;
                    tens = tens / 10;

                    lsTens = string.Format("{0}", nbrWords.Tens[tens - 2]);

                    // set the ones number word
                    if (liOnes > 0)
                        lsTens = string.Format("{0}{1}{2}", lsTens, nbrWords.TensSeparator, nbrWords.Ones[liOnes]);
                }
            }

            return lsTens;
        }

        /// <summary>
        /// Convert the JsonString to NumberWords object
        /// </summary>
        /// <param name="jsonstrpath"></param>
        /// <returns></returns>
        public static NumberWords JSONStringToNumberWords(string jsonstrpath)
        {
            NumberWords loNumberWords = null;

            if(File.Exists(jsonstrpath))
            {
                try
                {
                    var loJsonSer = new DataContractJsonSerializer(typeof(NumberWords));

                    using (var loStream = new MemoryStream(File.ReadAllBytes(jsonstrpath)))
                    {
                        loNumberWords = (NumberWords)loJsonSer.ReadObject(loStream);
                    }
                }
                catch (Exception)
                {
                    // string can't be converted
                    loNumberWords = null;
                }
            }

            return loNumberWords;            
        }
    }
}
