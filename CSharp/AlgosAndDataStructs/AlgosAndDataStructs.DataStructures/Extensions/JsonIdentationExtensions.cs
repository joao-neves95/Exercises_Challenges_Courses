using AlgosAndDataStructs.DataStructures.Options;

using Newtonsoft.Json;

namespace AlgosAndDataStructs.DataStructures.Extensions
{
    public static class JsonIdentationExtensions
    {
        public static Formatting ToNewtonsoftFormatting(this JsonIdentation jsonIdentation)
        {
            return jsonIdentation switch
            {
                JsonIdentation.Indented => Formatting.Indented,
                JsonIdentation.None => Formatting.None,

                _ => throw new NotSupportedException()
            };
        }
    }
}
