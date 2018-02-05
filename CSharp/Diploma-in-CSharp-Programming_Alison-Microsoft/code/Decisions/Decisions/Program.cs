using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decisions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to start? (Y/N)");
            string userValue = Console.ReadLine();
            if (userValue == "Y" || userValue == "y")
            {
                Console.WriteLine("What's your name?");
                string userName = Console.ReadLine();
                Console.WriteLine("How old are you?");
                string userAge = Console.ReadLine();

                Console.WriteLine($"Ok! So, your name is {userName}, and you have {userAge} yoars old. In 5 years you will have {int.Parse(userAge) + 5} years old.");
            }
            else if (userValue == "N" || userValue == "n")
            {
                Console.WriteLine("Bye!");
                // Sleep for 3 seconds:
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Wrong input!" + Environment.NewLine + "Do you want to exit? (Y/N)");
                string userInput = Console.ReadLine();
                if (userInput == "Y" || userInput == "y")
                {
                    Environment.Exit(0);
                }
                else if (userInput == "N" || userInput == "n")
                {
                    Program.Main(args);
                }
                else
                {
                    Console.WriteLine("Wrong input!");
                    System.Threading.Thread.Sleep(2000);
                    Program.Main(args);
                }
            }
        }
    }
}
