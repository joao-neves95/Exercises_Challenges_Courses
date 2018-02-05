using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            int x;
            int y;

            x = 7;
            y = x + 3;
            Console.WriteLine(y);
            Console.ReadLine();
            */

            //string myString = "Bob";
            //Console.WriteLine(myString);

            int x = 7;
            // string y = "Bob";
            string y = "2345";
            string theString = x + y;
            // int mySecondTry = x + y;
            int secondString = x + int.Parse(y);

            Console.WriteLine(secondString);

            Console.ReadLine();
        }
    }
}
