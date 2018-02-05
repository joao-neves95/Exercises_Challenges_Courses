using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;

            // Console.WriteLine("Hello World!");
            Console.WriteLine("Type a superhero's name to see his nickname:");
            string userValue = Console.ReadLine();

            if (Enum.TryParse<SuperHero>(userValue, true, out SuperHero value))
            {
                switch (value)
                {
                    case SuperHero.Batman:
                        Console.WriteLine("Caped Crusader");
                        break;
                    case SuperHero.Superman:
                        Console.WriteLine("Man of Steel");
                        break;
                    case SuperHero.GreenLantern:
                        Console.WriteLine("Emerald Knight");
                        break;
                    default:
                        break;
                }
            } else
            {
                Console.WriteLine("Input does not compute");
            }

            Console.ReadLine();
        }
    }

    enum SuperHero
    {
        Batman,
        Superman,
        GreenLantern
    }
}
