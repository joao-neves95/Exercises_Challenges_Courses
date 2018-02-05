using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SuperSecretFormula("João", "Neves"));
            Console.ReadLine();
        }

        private static string SuperSecretFormula()
        {
            return "This is a method.";
        }
        /*
        or:

        private static string SuperSecretFormula() => "This is a method.";
         */

        // Method overloading:

        private static string SuperSecretFormula(string name)
        {
            return String.Format($"Hello, {name}");
        }

        private static string SuperSecretFormula(string firstname, string lastname)
        {
            return string.Format($"hello, {firstname} {lastname}");
        }
    }
}
