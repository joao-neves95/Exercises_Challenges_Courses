using System;
using BasicClasses.Classes;

namespace BasicClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Point point = new Point { x = 1234, y = 574 };
            point.X = 1;
            Console.WriteLine($"point.X = {point.X}");

            Point otherPoint = new Point(546, 68);

            Console.WriteLine($"Regular point: x = {point.x}, y = {point.y}, x + y = {point.x + point.y}.\n");
            Console.WriteLine($"Constructed point: x = { otherPoint.x}, y = { otherPoint.y}, x + y = { otherPoint.x + otherPoint.y}.\n");
            */

            User user1 = new User("gisfgf", "qergrdgdra", Enums.Race.Eathling);

            Console.WriteLine(user1.Username);
            Console.WriteLine(user1.RACE);
            Console.WriteLine(user1.HEIGHT);
            Console.WriteLine(user1.ID);
            Console.WriteLine(User.NumberOfUsers);

            User user2 = new User("retger", "tgbsrtghsrthtrgteg", Enums.Race.Marsian);

            Console.WriteLine(user2.Username);
            Console.WriteLine(user2.RACE);
            Console.WriteLine(user2.HEIGHT);
            Console.WriteLine(user2.ID);
            Console.WriteLine(User.NumberOfUsers);
            Console.WriteLine(User.PRIVILEGES);

            Utilities.ColorfulWriteLine($"The user1 is {user1.Username}", ConsoleColor.Green);

            user1.SayColorfulUsername();

            Console.ReadLine();
        }
    }
}
