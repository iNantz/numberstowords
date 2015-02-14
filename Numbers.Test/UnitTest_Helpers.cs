using System;
using System.IO;
using Numbers.IService;
using Service = Numbers.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numbers.Test
{
    /// <summary>
    /// Test Cases for Helpers
    /// </summary>
    [TestClass]
    public class UnitTest_Helpers
    {
        NumberWords moNbrWord = new NumberWords();

        /// <summary>
        /// Test the whole numbers in words
        /// </summary>
        [TestMethod]
        public void Test_WholeNumbers()
        {
            // init
            string input = "123.45";
            string expected = "ONE HUNDRED AND TWENTY-THREE DOLLARS";

            // default to AU
            if (moNbrWord.Culture == null)
                moNbrWord.SetToAustralia();

            // assert
            Assert.AreEqual(expected, Service.Helpers.ConvertWholeNumbersToWords(input, moNbrWord).ToUpper());
        }

        /// <summary>
        /// Test the whole number and decimal numbers in word
        /// </summary>
        [TestMethod]
        public void Test_WholeAndDecimalNumbers()
        {
            // init
            string input = "123.45";
            string expected = "AND FORTY-FIVE CENTS";

            // default to AU
            if (moNbrWord.Culture == null)
                moNbrWord.SetToAustralia();

            // assert
            Assert.AreEqual(expected, Service.Helpers.ConvertCurrencyDecimalsToWords(input, moNbrWord).ToUpper());
        }

        /// <summary>
        /// Test the decimal numbers only
        /// </summary>
        [TestMethod]
        public void Test_DecimalNumbers()
        {
            // init
            string input = "0.45";
            string expected = "FORTY-FIVE CENTS";

            // default to AU
            if (moNbrWord.Culture == null)
                moNbrWord.SetToAustralia();

            // assert
            Assert.AreEqual(expected, Service.Helpers.ConvertCurrencyDecimalsToWords(input, moNbrWord).ToUpper());
        }

        /// <summary>
        /// Test the generated object from json string
        /// </summary>
        [TestMethod]
        public void Test_JSonStringToNumberWordsPropTest()
        {
            // init
            string input = "{\"CultureInfoCode\": \"en-AU\", \"Negative\": \"Negative\",\"CurrencyName\": \"Dollar\",\"CentsName\": \"Cent\",\"TensSeparator\": \"-\",\"And\": \" And \",\"Ones\": [\"Zero\", \"One\", \"Two\", \"Three\", \"Four\", \"Five\", \"Six\", \"Seven\", \"Eight\", \"Nine\", \"Ten\", \"Eleven\", \"Twelve\", \"Thirteen\", \"Fourteen\", \"Fifteen\", \"Sixteen\", \"Seventeen\", \"Eighteen\", \"Nineteen\"],\"Tens\": [\"Twenty\", \"Thirty\", \"Forty\", \"Fifty\", \"Sixty\", \"Seventy\", \"Eighty\", \"Ninety\"],\"Groups\": [\"Hundred\", \"Thousand\", \"Million\", \"Billion\", \"Trillion\", \"Quadrillion\", \"Quintillion\", \"Sextillion\", \"Septillion\", \"Octillion\", \"Nonillion\", \"Decillion\"]}";
            string jsonFile = Path.GetTempFileName();
            File.WriteAllText(jsonFile, input);

            NumberWords expected = new NumberWords();
            expected.SetToAustralia();

            NumberWords actual = Service.Helpers.JSONStringToNumberWords(jsonFile);

            File.Delete(jsonFile);

            // assert
            Assert.AreEqual(expected.CultureInfoCode, actual.CultureInfoCode);
            Assert.AreEqual(expected.And, actual.And);
            Assert.AreEqual(expected.CentsName, actual.CentsName);
            Assert.AreEqual(expected.CurrencyName, actual.CurrencyName);
            Assert.AreEqual(expected.Negative, actual.Negative);
            Assert.AreEqual(expected.TensSeparator, actual.TensSeparator);
            Assert.AreEqual(expected.Ones.Length, actual.Ones.Length);
            for (int i = 0; i < expected.Ones.Length - 1; i++)
            {
                Assert.AreEqual(expected.Ones[i], actual.Ones[i]);
            }
            Assert.AreEqual(expected.Tens.Length, actual.Tens.Length);
            for (int i = 0; i < expected.Tens.Length - 1; i++)
            {
                Assert.AreEqual(expected.Tens[i], actual.Tens[i]);
            }
            Assert.AreEqual(expected.Groups.Length, actual.Groups.Length);
            for (int i = 0; i < expected.Groups.Length - 1; i++)
            {
                Assert.AreEqual(expected.Groups[i], actual.Groups[i]);
            }
        }

        /// <summary>
        /// Test the invalid jsong string conversion
        /// </summary>
        [TestMethod]
        public void Test_InvalidJSonStringToNumberWords()
        {
            // init
            string input = "{\"CultureInfoCode\": \"en-AU\", \"Negative\": \"Negative\",\"CurrencyName\": \"Dollar\",\"CentsName\": \"Cent\",\"TensSeparator\": \"-\",\"And\": \" And \",\"Ones\": \"Zero\", \"One\", \"Two\", \"Three\", Four\", \"Five\", \"Six\", \"Seven\", \"Eight\", \"Nine\", \"Ten\", \"Eleven\", \"Twelve\", \"Thirteen\", \"Fourteen\", \"Fifteen\", \"Sixteen\", \"Seventeen\", \"Eighteen\", \"Nineteen\"],\"Tens\": [\"Twenty\", \"Thirty\", \"Forty\", \"Fifty\", \"Sixty\", \"Seventy\", \"Eighty\", \"Ninety\"],\"Groups\": [\"Hundred\", \"Thousand\", \"Million\", \"Billion\", \"Trillion\", \"Quadrillion\", \"Quintillion\", \"Sextillion\", \"Septillion\", \"Octillion\", \"Nonillion\", \"Decillion\"]}";
            string jsonFile = Path.GetTempFileName();
            File.WriteAllText(jsonFile, input);

            NumberWords expected = null;
            NumberWords actual = Service.Helpers.JSONStringToNumberWords(jsonFile);

            File.Delete(jsonFile);

            // assert
            Assert.AreEqual(expected, actual);            
        }
    }
}