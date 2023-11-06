using AlgosAndDataStructs.Algorithms.Strings;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.Strings
{
    public class ReverseStringTests
    {
        [Fact]
        public void Reverse_Passes()
        {
            const string input = "hello world";
            const string output = "dlrow olleh";

            ReverseString.BruteForce(input).Should().Be(output);
            ReverseString.LinqReverse(input).Should().Be(output);
            ReverseString.Optimized(input).Should().Be(output);
        }
    }
}
