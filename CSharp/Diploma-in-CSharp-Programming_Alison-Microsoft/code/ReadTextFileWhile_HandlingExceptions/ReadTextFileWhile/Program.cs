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
                StreamReader myReader = new StreamReader("\\boo\\Values.txt");
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
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Directory not found.\n{e.Message}");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found.\n{e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR, The file could not be read:\n{e.Message}");
            }
            finally
            {
                // Perform any cleanup to roll back the data or close connections to
                // files, database, network, etc.
            }
            Console.ReadLine();
        }
    }
}
