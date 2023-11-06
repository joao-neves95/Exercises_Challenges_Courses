using System.Text;

namespace AlgosAndDataStructs.Algorithms.Strings
{
    public sealed class ReverseString : IAlgorithm<string, string?>
    {
        /// <summary>
        /// O(n)
        ///
        /// </summary>
        public static string? BruteForce(string str)
        {
            if (str == default || str.Length < 2)
            {
                return default;
            }

            var newString = new StringBuilder(str.Length);
            for (int i = str.Length - 1; i >= 0; --i)
            {
                newString.Append(str[i]);
            }

            return newString.ToString();
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        public static string? LinqReverse(string str)
        {
            if (str == default || str.Length < 2)
            {
                return default;
            }

            return new string(str.Reverse().ToArray());
        }

        public static string? Optimized(string str)
        {
            if (str == default || str.Length < 2)
            {
                return default;
            }

            var allChars = str.ToCharArray();

            int iFront = 0;
            int iBack = allChars.Length - 1;
            for (; iFront < iBack; ++iFront, --iBack)
            {
                (allChars[iFront], allChars[iBack]) = (allChars[iBack], allChars[iFront]);
            }

            return new string(allChars);
        }
    }
}
