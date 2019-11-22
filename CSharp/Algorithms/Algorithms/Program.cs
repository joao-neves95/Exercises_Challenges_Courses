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

            Console.WriteLine("Hourglass in a 2D array\n");
            Console.WriteLine("Matrix:");
            Console.WriteLine(@"
{
    { -9, -9, -9, 1, 1, 1},
    { 0, -9, 0, 4, 3, 2},
    { -9, -9, -9, 1, 2, 3},
    { 0, 0, 8, 6, 6, 0},
    { 0, 0, 0, -2, 0, 0},
    { 0, 0, 1, 2, 4, 0}
}");
            Console.WriteLine("The result should be 28.");
            Console.Write("Actual result: ");
            Console.Write(ExerciseAlgorithms.HourglassSum(
                new int[][]
                {
                    new int[] { -9, -9, -9, 1, 1, 1},
                    new int[] { 0, -9, 0, 4, 3, 2},
                    new int[] { -9, -9, -9, 1, 2, 3},
                    new int[] { 0, 0, 8, 6, 6, 0},
                    new int[] { 0, 0, 0, -2, 0, 0},
                    new int[] { 0, 0, 1, 2, 4, 0}
                }
            ));
            Console.WriteLine("\n");

            Console.ReadLine();
        }
    }
}
