using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadTextFileWhile
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamReader myReader = new StreamReader("Values.txt");
                string line = "";

                while (line != null)
                {
                    line = myReader.ReadLine();
                    if (line != null)
                    {
                        Console.WriteLine(line);
                    }

                }
                myReader.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR, The file could not be read:");
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
