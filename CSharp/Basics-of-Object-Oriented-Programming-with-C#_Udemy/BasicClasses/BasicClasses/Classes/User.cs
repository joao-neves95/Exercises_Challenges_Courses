using System;
using BasicClasses.Enums;

namespace BasicClasses.Classes
{
    class User
    {
        public static int NumberOfUsers;
        // "const": hardcoded; must be known before runtime; it's always static; can't be changed.
        public const string PRIVILEGES = "read";

        // "readonly": it's the value is initialized at runtime; it can't be changed after runtime.
        public readonly int ID;
        private string username;
        private string password;
        public readonly Race RACE;
        public readonly double HEIGHT;

        // Properties:
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (value.Length >= 4 && value.Length <= 10)
                {
                    username = value;
                }
                else
                {
                    Console.WriteLine("The username must have between 4 and 10 characters.");
                }
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (value.Length >= 8 && value.Length <= 25)
                {
                    password = value;
                }
                else
                {
                    Console.WriteLine("The password must have more than 8 characters.");
                }
            }
        }

        // Constructors:
        // (with constructor overloading)
        public User()
        {
            NumberOfUsers++;
            this.ID = NumberOfUsers;
        }

        public User(string username, string password, Race race)
        {
            NumberOfUsers++;
            this.ID = NumberOfUsers;
            this.Username = username;
            this.Password = password;
            this.RACE = race;

            if (race == Race.Eathling)
            {
                this.HEIGHT = 1.50;
            }
            else if (race == Race.Marsian)
            {
                this.HEIGHT = 5.50;
            }
        }

        // Methods:
        // (with method overloading)
        public void SayColorfulUsername()
        {
            ConsoleColor[] colors = new ConsoleColor[] {
                ConsoleColor.Blue,
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.Yellow,
                ConsoleColor.Cyan
            };

            Random random = new Random();
            Utilities.ColorfulWriteLine(this.Username, colors[random.Next(colors.Length)]);
        }

        public void SayColorfulUsername(ConsoleColor color)
        {
            Utilities.ColorfulWriteLine(this.Username, color);
        }
    }
}
