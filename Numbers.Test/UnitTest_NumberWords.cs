using System;
using Numbers.IService;
using Numbers.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;

namespace Numbers.Test
{
    /// <summary>
    /// NumberWords object unit test
    /// </summary>
    [TestClass]
    public class UnitTest_NumberWords
    {
        /// <summary>
        /// Test the Test Invalid NumberWords values
        /// </summary>
        [TestMethod]
        public void Test_ValidNumberWords()
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
        /// Test Invalid NumberWords values
        /// </summary>
        [TestMethod]
        public void Test_InvalidNumberWords()
        {
            // init
            string input = "invalid-culture";
            CultureInfo expected = null;
            NumberWords loNumberWords = new NumberWords();
            loNumberWords.CultureInfoCode = input;

            // assert
            Assert.AreEqual(expected, loNumberWords.Culture);
            Assert.AreEqual(expected, loNumberWords.NumberFormat);
            
            loNumberWords.Ones = null;
            Assert.AreEqual(20, loNumberWords.Ones.Length);
            for (int i = 0; i < loNumberWords.Ones.Length-1; i++)
            {
                Assert.AreEqual(loNumberWords.Ones[i], "N/A");                
            }

            loNumberWords.Ones = new string[1] { "Zero" };
            Assert.AreEqual("Zero", loNumberWords.Ones[0]);
            for (int i = 1; i < loNumberWords.Ones.Length - 1; i++)
            {
                Assert.AreEqual(loNumberWords.Ones[i], "N/A");
            }

            loNumberWords.Tens = null;
            Assert.AreEqual(8, loNumberWords.Tens.Length);
            for (int i = 0; i < loNumberWords.Tens.Length - 1; i++)
            {
                Assert.AreEqual(loNumberWords.Tens[i], "N/A");
            }

            loNumberWords.Tens = new string[1] { "Twenty" };
            Assert.AreEqual("Twenty", loNumberWords.Tens[0]);
            for (int i = 1; i < loNumberWords.Tens.Length - 1; i++)
            {
                Assert.AreEqual(loNumberWords.Tens[i], "N/A");
            }

            loNumberWords.Groups = null;
            Assert.AreEqual(12, loNumberWords.Groups.Length);
            for (int i = 0; i < loNumberWords.Groups.Length - 1; i++)
            {
                Assert.AreEqual(loNumberWords.Groups[i], "N/A");
            }

            loNumberWords.Groups = new string[1] { "Hundred" };
            Assert.AreEqual("Hundred", loNumberWords.Groups[0]);
            for (int i = 1; i < loNumberWords.Groups.Length - 1; i++)
            {
                Assert.AreEqual(loNumberWords.Groups[i], "N/A");
            }
        }
    }
}
