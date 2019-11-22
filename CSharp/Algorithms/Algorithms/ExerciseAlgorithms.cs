using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    class ExerciseAlgorithms
    {
        /*
          Your goal in this kata is to implement a difference function, which subtracts one list from another and returns the result.

          It should remove all values from list a, which are present in list b.

          Kata.ArrayDiff(new int[] {1, 2}, new int[] {1}) => new int[] {2}

          If a value is present in b, all of its occurrences must be removed from the other:

          Kata.ArrayDiff(new int[] {1, 2, 2, 2, 3}, new int[] {2}) => new int[] {1, 3}
 
        */
        public static int[] ArrayDiff(int[] a, int[] b)
        {
            return Array.FindAll(a, num => Array.IndexOf(b, num) == -1);
        }

        /*
            Write a function that takes an (unsigned) integer as input, and returns the number of bits that are equal to one in the binary representation of that number.

            Example: The binary representation of 1234 is 10011010010, so the function should return 5 in this case
        */
        public static int CountBits(int n)
        {
            int bitsEqualToOne = 0;

            while (n > 0)
            {
                if (n % 2 == 1)
                    bitsEqualToOne++;
                n = n / 2;
            }

            return bitsEqualToOne;
        }

        /*
            Bouncing Balls:
            ---------------

            A child is playing with a ball on the nth floor of a tall building. The height of this floor, h, is known.

            He drops the ball out of the window. The ball bounces (for example), to two-thirds of its height (a bounce of 0.66).

            His mother looks out of a window 1.5 meters from the ground.

            How many times will the mother see the ball pass in front of her window (including when it's falling and bouncing?
            Three conditions must be met for a valid experiment:

                Float parameter "h" in meters must be greater than 0
                Float parameter "bounce" must be greater than 0 and less than 1
                Float parameter "window" must be less than h.

            If all three conditions above are fulfilled, return a positive integer, otherwise return -1.

            Note: The ball can only be seen if the height of the rebounding ball is stricty greater than the window parameter.

            Example:

                h = 3, bounce = 0.66, window = 1.5, result is 3

                h = 3, bounce = 1, window = 1.5, result is -1 (Condition 2) not fulfilled).
        */
        public static int BouncingBall(double h, double bounce, double window)
        {
            if (!ValidRules(h, bounce, window))
                return -1;

            int bounces = 0;

            while (h > window)
            {
                ++bounces;
                h *= bounce;
                if (h > window)
                    ++bounces;
            }

            return bounces;
        }

        private static bool ValidRules(double h, double bounce, double window)
        {
            if (h > 0 && (bounce > 0 && bounce < 1) && window < h)
                return true;

            return false;
        }

        /*
            Given a 2D Array,

            1 1 1 0 0 0
            0 1 0 0 0 0
            1 1 1 0 0 0
            0 0 0 0 0 0
            0 0 0 0 0 0
            0 0 0 0 0 0

            We define an hourglass in to be the  graphical representation:

            a b c
              d
            e f g

            There are 16 hourglasses in the array, and an hourglass sum is the 
            sum of an hourglass' values.
            Calculate the hourglass sum for every hourglass in the array, then
            print the maximum hourglass sum.
.
         */
        /*
           [ SOLUTION LOGIC NOTES ]
           Matrix:

           j [0 <= j,i <= 5]
           |
        i- 1 1 1 0 0 0
           0 1 0 0 0 0
           1 1 1 0 0 0
           0 0 2 4 4 0
           0 0 0 2 0 0
           0 0 1 2 4 0

           Hourglass:
           1 1 1
             1
           1 1 1

           Facts:
           - Matrix:
             - In each row of hourglasses we have 4 hourglasses.
             - In each collumn of hourglasses we have 4 hourglasses.
           - Hourglass:
             - Each hourglass has 3 rows and 3 collumns.
             - The middle hourglass row has only the center node.

           END: When i and j == 5
        */

        private const int HG_SIZE = 3;
        private const int MAX_GLASS_NUM = 4;

        // Complete the hourglassSum function below.
        public static int HourglassSum(int[][] arr)
        {
            // -63, the minimum possible sum.
            int maxSum = -9 * 7;
            int thisSum = 0;
            int thisMatrixColIdx = 0;
            int thisMatrixRowIdx = 0;
            int thisRowGlassNum = 0;

            int i = 0;
            int j = 0;
            while (thisMatrixRowIdx <= HG_SIZE &&
                   thisMatrixColIdx <= HG_SIZE)
            {

                for (i = thisMatrixRowIdx; i < thisMatrixRowIdx + HG_SIZE; ++i)
                {
                    // Middle row.
                    if (i == thisMatrixRowIdx + 1)
                    {
                        thisSum += arr[i][thisMatrixColIdx + 1];
                        continue;
                    }

                    // Normal rows.
                    for (j = thisMatrixColIdx; j < thisMatrixColIdx + HG_SIZE; ++j)
                    {
                        thisSum += arr[i][j];
                    }
                }

                if (thisSum > maxSum)
                {
                    maxSum = thisSum;
                }

                thisSum = 0;
                ++thisRowGlassNum;

                // End of row.
                if (thisRowGlassNum == MAX_GLASS_NUM)
                {
                    ++thisMatrixRowIdx;
                    thisMatrixColIdx = 0;
                    thisRowGlassNum = 0;

                    // Continue row.
                }
                else
                {
                    ++thisMatrixColIdx;
                }

            }

            return maxSum;
        }
    }
}
