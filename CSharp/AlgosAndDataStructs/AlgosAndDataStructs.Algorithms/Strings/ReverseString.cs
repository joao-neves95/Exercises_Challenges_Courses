
using System.Text;

namespace AlgosAndDataStructs.Algorithms.Strings
{
    public static class ReverseString
    {
        /// <summary>
        /// O(n)
        ///
        /// </summary>
        public static string? Reverse(string str)
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

            return string.Join(string.Empty, str.Reverse());
        }
    }
}
