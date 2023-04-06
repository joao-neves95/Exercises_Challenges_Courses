using AlgosAndDataStructs.Algorithms.Strings;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.Strings
{
    public class ReverseStringTests
    {
        [Fact]
        public void Reverse_Passes()
        {
            ReverseString.Reverse("hello world").Should().Be("dlrow olleh");
        }

        [Fact]
        public void LinqReverse_Passes()
        {
            ReverseString.LinqReverse("hello world").Should().Be("dlrow olleh");
        }
    }
}
