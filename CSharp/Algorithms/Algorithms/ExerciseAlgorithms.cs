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
    }
}
