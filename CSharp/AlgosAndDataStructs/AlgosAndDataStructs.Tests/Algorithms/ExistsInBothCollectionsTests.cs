using AlgosAndDataStructs.Algorithms.Search;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms
{
    public class ExistsInBothCollectionsTests
    {
        [Fact]
        public void BruteForce_Passes()
        {
            ExistsInBothCollections.BruteForce(GetMockInputData1(), GetMockInputData2())
                .Should()
                .BeTrue();
        }

        [Fact]
        public void BruteForce_False_Passes()
        {
            ExistsInBothCollections.BruteForce(GetMockInputData1(), GetMockInputData3())
                .Should()
                .BeFalse();

            ExistsInBothCollections.BruteForce(GetMockInputData1(), null!)
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Optimized_True_Passes()
        {
            ExistsInBothCollections.Optimized(GetMockInputData1(), GetMockInputData2())
                .Should()
                .BeTrue();

            ExistsInBothCollections.Optimized(GetMockInputData2(), GetMockInputData1())
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Optimized_False_Passes()
        {
            ExistsInBothCollections.Optimized(GetMockInputData1(), GetMockInputData3())
                .Should()
                .BeFalse();

            ExistsInBothCollections.Optimized(GetMockInputData1(), null!)
                .Should()
                .BeFalse();

            ExistsInBothCollections.Optimized(null!, GetMockInputData1())
                .Should()
                .BeFalse();
        }

        private static IEnumerable<string?> GetMockInputData1()
        {
            yield return "Hello";
            yield return "my";
            yield return null;
            yield return "name";
            yield return null;
            yield return "is";
            yield return "Joe";
        }

        private static IEnumerable<string> GetMockInputData2()
        {
            yield return "I'm";
            yield return "Joe";
        }

        private static IEnumerable<string> GetMockInputData3()
        {
            yield return "I'm";
            yield return "João";
        }
    }
}
