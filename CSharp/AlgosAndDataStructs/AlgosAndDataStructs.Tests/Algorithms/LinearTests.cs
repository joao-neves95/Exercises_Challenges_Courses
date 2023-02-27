
using AlgosAndDataStructs.Algorithms;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms
{
    public sealed class LinearTests
    {
        [Fact]
        public void Linear_Find_Passes()
        {
            var input = new[] { "Hello", "my", "name", "is", "Joe Neves" };

            input.LinearFind("name").Should().Be(2);
        }
    }
}
