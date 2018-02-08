using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameProject.Enums;

namespace GameProject
{
    class Program
    {
        static Random random = new Random();

        static void Main()
        {
            Worrior hero = new Worrior("Hero", Faction.Hero);
            Worrior villain = new Worrior("Villain", Faction.Villain);

            while (hero.IsAlive && villain.IsAlive)
            {
                // 50% chance.
                if (random.Next(0, 10) < 5)
                {
                    hero.Attack(villain);
                }
                else
                {
                    villain.Attack(hero);
                }

                Thread.Sleep(100);
            }

            Console.WriteLine("Play again? (YES / Y)");
            string keyPressed = Console.ReadLine();
            string normalizedInput = keyPressed.ToUpper().Trim();
            if (normalizedInput == "Y" || normalizedInput == "YES")
            {
                Console.WriteLine("Let the games begin!");
                Thread.Sleep(1000);
                Program.Main();
            }
            else
            {
                Console.WriteLine("Bye!");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }
    }
}
