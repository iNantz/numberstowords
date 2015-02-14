using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numbers.IService
{
    public partial interface IUtils
    {
       void  NumberToWords(string number, out string words, bool currency = false);
    }
}
