using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datetime
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime value = DateTime.Now;
            //Console.WriteLine(value.ToString());
            //Console.WriteLine(value.ToShortDateString());
            //Console.WriteLine(value.ToShortTimeString());
            //Console.WriteLine(value.ToLongDateString());
            //Console.WriteLine(value.ToLongTimeString());
            //Console.WriteLine(value.Second);

            //Console.WriteLine(value.AddDays(3).ToLongDateString());
            //Console.WriteLine($"Today is {value.ToString()}, and in 1 month, 15 days and 53 minutes, it will be {value.AddMonths(1).AddDays(15).AddMinutes(53).ToString()}.");

            DateTime exactBirth = new DateTime(1995, 07, 11, 00, 18, 00);

            DateTime myBirth = DateTime.Parse(exactBirth.ToString());
            TimeSpan myAge = DateTime.Now.Subtract(myBirth);

            Console.WriteLine(myBirth);
            Console.WriteLine(myAge.TotalDays);
            Console.ReadLine();
            
        }
    }
}
