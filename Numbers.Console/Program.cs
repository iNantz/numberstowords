using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numbers.IService;
using Numbers.Service;

namespace Numbers.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IUtils loUtils = new Utils();
            Console.WriteLine("Please enter check amount.");
            var lsNumber = Console.ReadLine();
            Console.WriteLine();
            string lsWords = "";
            loUtils.NumberToWords(lsNumber, out lsWords, true);
            Console.WriteLine();
            Console.WriteLine(" --- converted to words --- ");
            Console.WriteLine(lsWords.ToUpper());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();

        }
    }
}
