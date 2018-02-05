using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scope
{
    class Program
    {
        private static string k = "k";

        static void Main(string[] args)
        {
            // Using the "StringBuilder" class is more optimized than concatinating strings with "+".
            StringBuilder j = new StringBuilder("");

            for (int i = 0; i < 10; i++)
            {
                j.Append(i.ToString());
                k = i.ToString();
                Console.WriteLine(i);
            }
            // "i" is out of scope:
            //
            // Console.WriteLine(i);

            //Console.WriteLine($"Outside of the for: {j.ToString()}");
            //Console.WriteLine($"k: {k}");
            //HelperMethod();

            Car car = new Car();
            // The "DoSomething()" is the only publicly acessible method.
            // The "HelperMethod" is not acessible outside the "Car" class.
            //
            car.DoSomething();

            Console.ReadLine();
        }

        static void HelperMethod()
        {
            Console.WriteLine($"k from the helper method: {k}");
        }
    }

    class Car
    {
        public void DoSomething()
        {
            Console.WriteLine(helperMethod());
        }

        private string helperMethod()
        {
            return "Hello World!";
        }
    }
}
