using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ArrayDiff\n");
            Console.WriteLine(ExerciseAlgorithms.ArrayDiff(new int[] { 1, 2 }, new int[] { 1 }));
            Console.WriteLine("Should return {2}.\n");
            Console.WriteLine(ExerciseAlgorithms.ArrayDiff(new int[] { 1, 2, 2, 2, 3 }, new int[] { 2 }));
            Console.WriteLine("Should return {1, 3}.\n");
            Console.WriteLine("\n");

            Console.WriteLine("CountBits\n");
            Console.WriteLine(ExerciseAlgorithms.CountBits(0));
            Console.WriteLine(ExerciseAlgorithms.CountBits(4));
            Console.WriteLine(ExerciseAlgorithms.CountBits(7));
            Console.WriteLine(ExerciseAlgorithms.CountBits(9));
            Console.WriteLine(ExerciseAlgorithms.CountBits(1234));
            Console.WriteLine("\n");

            Console.WriteLine("BouncingBall\n");
            Console.WriteLine(ExerciseAlgorithms.BouncingBall(3.0, 0.66, 1.5));
            Console.WriteLine("Should return 3.\n");
            Console.WriteLine(ExerciseAlgorithms.BouncingBall(30.0, 0.66, 1.5));
            Console.WriteLine("Should return 15.\n");
            Console.WriteLine(ExerciseAlgorithms.BouncingBall(2.0, 0.66, 3.1));
            Console.WriteLine("Should return -1.\n");
            Console.WriteLine("\n");

            Console.ReadLine();
        }
    }
}
