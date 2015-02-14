using System;
using Numbers.IService;
using Numbers.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Numbers.Test
{
    /// <summary>
    /// Number To Words test cases
    /// </summary>
    [TestClass]
    public class UnitTest_NumberToWords
    {
        /// <summary>
        /// Test valid input
        /// </summary>
        [TestMethod]
        public void Test_ValidInput()
        {
            // init
            string input = "123.45";
            string expected = "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Tes invalid input
        /// </summary>
        [TestMethod]
        public void Test_InValidInput()
        {
            // init
            string input = "123.34ox";
            string expected = "INVALID NUMBER";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test the maximum value of decimal in words
        /// </summary>
        [TestMethod]
        public void Test_MaxDecimalValue()
        {
            // init
            string input = decimal.MaxValue.ToString();
            string expected = "SEVENTY-NINE OCTILLION TWO HUNDRED AND TWENTY-EIGHT SEPTILLION ONE HUNDRED AND SIXTY-TWO SEXTILLION FIVE HUNDRED AND FOURTEEN QUINTILLION TWO HUNDRED AND SIXTY-FOUR QUADRILLION THREE HUNDRED AND THIRTY-SEVEN TRILLION FIVE HUNDRED AND NINETY-THREE BILLION FIVE HUNDRED AND FORTY-THREE MILLION NINE HUNDRED AND FIFTY THOUSAND THREE HUNDRED AND THIRTY-FIVE DOLLARS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Check the minimum valud of decimal in words
        /// </summary>
        [TestMethod]
        public void Test_MinDecimalValue()
        {
            // init
            string input = decimal.MinValue.ToString();
            string expected = "NEGATIVE SEVENTY-NINE OCTILLION TWO HUNDRED AND TWENTY-EIGHT SEPTILLION ONE HUNDRED AND SIXTY-TWO SEXTILLION FIVE HUNDRED AND FOURTEEN QUINTILLION TWO HUNDRED AND SIXTY-FOUR QUADRILLION THREE HUNDRED AND THIRTY-SEVEN TRILLION FIVE HUNDRED AND NINETY-THREE BILLION FIVE HUNDRED AND FORTY-THREE MILLION NINE HUNDRED AND FIFTY THOUSAND THREE HUNDRED AND THIRTY-FIVE DOLLARS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test One Cent 
        /// </summary>
        [TestMethod]
        public void Test_OneCent()
        {
            // init
            string input = ".01";
            string expected = "ONE CENT";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test More Cents
        /// </summary>
        [TestMethod]
        public void Test_MoreCents()
        {
            // init
            string input = ".21";
            string expected = "TWENTY-ONE CENTS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test One Dollar
        /// </summary>
        [TestMethod]
        public void Test_OneDollar()
        {
            // init
            string input = "1";
            string expected = "ONE DOLLAR";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test More Dollars
        /// </summary>
        [TestMethod]
        public void Test_MoreDollars()
        {
            // init
            string input = "55";
            string expected = "FIFTY-FIVE DOLLARS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test One Dollar and One Cent
        /// </summary>
        [TestMethod]
        public void Test_OneDollarOneCent()
        {
            // init
            string input = "1.01";
            string expected = "ONE DOLLAR AND ONE CENT";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test One Dollar and More Cents
        /// </summary>
        [TestMethod]
        public void Test_OneDollarMoreCent()
        {
            // init
            string input = "1.51";
            string expected = "ONE DOLLAR AND FIFTY-ONE CENTS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test More Dollars and Cents
        /// </summary>
        [TestMethod]
        public void Test_MoreDollarsCents()
        {
            // init
            string input = "21.51";
            string expected = "TWENTY-ONE DOLLARS AND FIFTY-ONE CENTS";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test More Dollars and One Cent
        /// </summary>
        [TestMethod]
        public void Test_MoreDollarsOneCent()
        {
            // init
            string input = "12.01";
            string expected = "TWELVE DOLLARS AND ONE CENT";
            IUtils loUtils = new Utils();
            string actual = string.Empty;

            loUtils.NumberToWords(input, out actual, true);

            // assert
            Assert.AreEqual(expected, actual.ToUpper());
        }

        /// <summary>
        /// Test Null NumberWords Object
        /// </summary>
        [TestMethod]
        public void Test_NullNumberWords()
        {
            // init
            string input = "23.40";
            string expected = "Twenty-Three Dollars";
            IUtils loUtils = new Utils();

            // assert
            Assert.AreEqual(expected, Numbers.Service.Helpers.ConvertWholeNumbersToWords(input, null));
        }
    }
}
