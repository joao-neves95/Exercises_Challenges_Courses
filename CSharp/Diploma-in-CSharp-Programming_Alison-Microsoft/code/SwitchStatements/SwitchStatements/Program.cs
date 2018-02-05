using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwitchStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type in a super hero's name to see his nickname:");
            String userValue = Console.ReadLine();

            switch (userValue.ToUpper())
            {
                case "BATMAN":
                    Console.WriteLine("Caped Crusader\n");
                    break;
                case "SUPERMAN":
                    Console.WriteLine("Man of Steel\n");
                    break;
                case "GREENLANTERN":
                case "GREEN LANTERN":
                    Console.WriteLine("Emerald Knight\n");
                    break;
                case "CLOSE":
                    Console.WriteLine("Bye!\n");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Does not compute\n");
                    break;
            }

            Program.Main(args);
        }
    }
}
