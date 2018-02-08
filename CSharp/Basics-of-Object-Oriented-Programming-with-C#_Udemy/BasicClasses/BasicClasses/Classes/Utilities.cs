using System;

namespace BasicClasses.Classes
{
    // "static" classes can't have constructors; it's not possible to create
    // an instance of them.
    static class Utilities
    {
        public static void ColorfulWriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
