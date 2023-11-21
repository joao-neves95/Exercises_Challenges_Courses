using AlgosAndDataStructs.DataStructures.Extensions;
using AlgosAndDataStructs.DataStructures.Options;
using AlgosAndDataStructs.DataStructures.Traits;

using Newtonsoft.Json;

namespace AlgosAndDataStructs.Tests
{
    internal static class TestUtils
    {
        public static string ToJson(IJsonSerializable target, JsonIdentation identation = JsonIdentation.None)
        {
            return ToJson(target.GetJsonObject(), identation);
        }

        public static string ToJson(object target, JsonIdentation identation = JsonIdentation.None)
        {
            return JsonConvert.SerializeObject(
                target,
                identation.ToNewtonsoftFormatting());
        }
    }
}
