namespace AlgosAndDataStructs.Algorithms.Numbers
{
    public sealed class IsPalindrome : IAlgorithm<int, bool>
    {
        public static bool BruteForce(int x)
        {
            var str = x.ToString();

            var iBegin = 0;
            var iEnd = str.Count() - 1;
            for (; iBegin < iEnd; ++iBegin, --iEnd)
            {
                if (str[iBegin] != str[iEnd])
                {
                    return false;
                }
            }

            return true;
        }

        /*
        The equations for each side of the number:
        Left:
            (1234 % 10000) / 1000
            1.234
            (1234 % 1000) / 100
            2.34
            (1234 % 100) / 10
            3.4
            ...
        Right:
            (1234 % 10) / 1
            4
            (1234 % 100) / 10
            3.4
            (1234 % 1000) / 100
            2.34
            ...
        */
        public static bool Optimized(int x)
        {
            // This one is not optimized. It's just the same implementation as above, but without string conversions.
            if (x < 0)
            {
                return false;
            }
            else if (x == 0)
            {
                return true;
            }

            var numLengthInThousands = GetPositiveNumberLengthInThousands(x);

            long leftModulus = numLengthInThousands;
            decimal leftDivider = numLengthInThousands / (decimal)10;
            long rightModulus = 10;
            decimal rightDivider = 1;

            while (leftDivider > rightDivider)
            {
                var leftNum = Math.Floor((x % leftModulus) / leftDivider);
                leftModulus /= 10;
                leftDivider /= 10;

                var rigthNum = Math.Floor((x % rightModulus) / rightDivider);
                rightModulus *= 10;
                rightDivider *= 10;

                if (leftNum != rigthNum)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Return GetPositiveNumberLengthInThousands(123) = 1000
        /// </summary>
        private static long GetPositiveNumberLengthInThousands(int x)
        {
            if (x == 0)
            {
                return 0;
            }

            long lengthInThousands = 10;

            // > 0 in case it overflows.
            while (lengthInThousands > 0 && lengthInThousands <= x)
            {
                lengthInThousands *= 10;
            }

            return lengthInThousands;
        }
    }
}
