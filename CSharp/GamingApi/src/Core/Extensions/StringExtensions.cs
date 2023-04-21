using System.Text.RegularExpressions;

namespace GamingApi.WebApi.Core.Extensions
{
    public static class StringExtensions
    {
        public static int GetStartIntNumber(this string str)
        {
            // https://stackoverflow.com/a/4734164/8222993
            if (!int.TryParse(Regex.Match(str, @"\d+").Value, out var number))
            {
                return 0;
            }

            return number;
        }
    }
}
