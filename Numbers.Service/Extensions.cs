using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numbers.Service
{
    public static class Extensions
    {
        private static Regex moDigits = new Regex(@"[^\d]");

        public static string GetDigits(this string str)
        {
            return moDigits.Replace(str, "");
        }
    }
}
