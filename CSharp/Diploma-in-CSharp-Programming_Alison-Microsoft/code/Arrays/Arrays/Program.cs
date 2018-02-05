using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array1 = { 1, 2, 3 };
            int arrayLength = 0;
            for (var i = 0; i < array1.Length; i++)
            {
                arrayLength++;
            };
            Console.WriteLine(arrayLength);
            Console.ReadLine();
        }
    }
}
