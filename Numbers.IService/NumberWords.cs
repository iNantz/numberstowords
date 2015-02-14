using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Runtime.Serialization;

namespace Numbers.IService
{
    /// <summary>
    /// Number Words Model
    /// </summary>
    public partial class NumberWords
    {
        public NumberWords()
        {
            SetToAustralia();
        }

        // initialized culture info 
        CultureInfo moCultureInfo = null;
        string[] moOnes = null;
        string[] moTens = null;
        string[] moGroups = null;

        /// <summary>
        /// culture info code. eg. en-AU, en-US, fr-FR, etc
        /// </summary>
        public string CultureInfoCode
        {
            get
            {
                return moCultureInfo.Name;
            }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        moCultureInfo = new CultureInfo(value);

                    }
                    catch (Exception)
                    {
                        moCultureInfo = null;
                    }
                }
            }
        }
        /// <summary>
        /// Number Format
        /// </summary>
        public NumberFormatInfo NumberFormat { get { return moCultureInfo != null ? moCultureInfo.NumberFormat : null; } }
        /// <summary>
        /// Culture Info
        /// </summary>
        public CultureInfo Culture { get { return moCultureInfo; } }
        /// <summary>
        /// Negative
        /// </summary>
        public string Negative { get; set; }
        /// <summary>
        /// Dollar
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// Cent
        /// </summary>
        public string CentsName { get; set; }
        /// <summary>
        /// Tens Separator
        /// </summary>
        public string TensSeparator { get; set; }
        /// <summary>
        /// And
        /// </summary>
        public string And { get; set; }
        /// <summary>
        /// list of zero, one, two,.... nineteen
        /// </summary>
        public string[] Ones
        {
            get
            {
                return moOnes;
            }
            set
            {
                moOnes = value;

                if (moOnes == null)
                    moOnes = Enumerable.Repeat("N/A", 20).ToArray();

                if (moOnes.Length < 20)
                {
                    var liMissing = 20 - moOnes.Length;
                    var loValue = moOnes.ToList();
                    loValue.AddRange(Enumerable.Repeat("N/A", liMissing));
                    moOnes = loValue.ToArray();
                }
            }
        }
        /// <summary>
        /// list of twenty, thirty, forty,.... ninety
        /// </summary>
        public string[] Tens {
            get
            {
                return moTens;
            }
            set
            {
                moTens = value;

                if (moTens == null)
                    moTens = Enumerable.Repeat("N/A", 8).ToArray();

                if (moTens.Length < 8)
                {
                    var liMissing = 8 - moTens.Length;
                    var loValue = moTens.ToList();
                    loValue.AddRange(Enumerable.Repeat("N/A", liMissing));
                    moTens = loValue.ToArray();
                }
            }
        }
        /// <summary>
        /// list of hundred, thousands, million, billion.....
        /// </summary>
        public string[] Groups { 
            get
            {
                return moGroups;
            }
            set
            {
                moGroups = value;

                if (moGroups == null)
                    moGroups = Enumerable.Repeat("N/A", 12).ToArray();

                if (moGroups.Length < 12)
                {
                    var liMissing = 12 - moGroups.Length;
                    var loValue = moGroups.ToList();
                    loValue.AddRange(Enumerable.Repeat("N/A", liMissing));
                    moGroups = loValue.ToArray();
                }
            }
        }

        /// <summary>
        /// default all to australian culture info
        /// </summary>
        public void SetToAustralia ()
        {
            CultureInfoCode = "en-AU";
            Negative = "Negative";
            CurrencyName = "Dollar";
            CentsName = "Cent";
            TensSeparator = "-";
            And = " And ";
            Ones = new string[]{ "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
                "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"};
            Tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            Groups = new string[] { "Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion",
                "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion"};
        }
    }
}
