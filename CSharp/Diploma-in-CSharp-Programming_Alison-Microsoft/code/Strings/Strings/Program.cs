using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            // string text = "Go to your C:\\ drive.";
            // string text = "My \"so called\" life.";
            // string text = "This is: \na new line.";
            /*
            string text = "";
            for (int i = 0; i <= 100; i++) {
                text += "..." + i.ToString();
            }
            Console.WriteLine(text);
            Console.ReadLine();
            */

            /*
            StringBuilder text = new StringBuilder();
            for (int i = 0; i <= 100; i++)
            {
                text.Append("...");
                text.Append(i.ToString());
            }
            */

            string text = " That summer we took threes across the board. ";
            // text = text.Substring(5, 14);
            // text = text.ToUpper();
            // text = text.Replace("summer", "winter");
            // text = text.Replace(" ", "-");
            string answer = String.Format($"Length before: {text.Length} -- After: {text.Trim().Length}");
            string answer2 = $"Withouth any spaces --> Before: {text.Length} - After: {text.Replace(" ", "").Length}";
            Console.WriteLine(answer);
            Console.WriteLine(answer2);
            Console.ReadLine();
        }
    }
}
