using AlgosAndDataStructs.Algorithms.Search;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.Search
{
    public sealed class LinearSearchTests
    {
        [Fact]
        public void LinearSearch_Find_Passes()
        {
            new[] { "Hello", "my", "name", "is", "Joe Neves" }
                .Find("name")
                .Should()
                .Be(2);
        }
    }
}
