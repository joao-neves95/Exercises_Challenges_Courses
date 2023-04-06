using AlgosAndDataStructs.Algorithms.Arrays;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.Arrays
{
    public class TwoSumTests
    {
        [Theory]
        [InlineData(new[] { 4, -2, 5, 0, 6, 3, 2, 7 }, 1, new[] { 5, 1 })]
        [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
        [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
        [InlineData(new[] { 3, 2, 4 }, 4, null)]
        [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
        [InlineData(new[] { 1 }, 3, null)]
        [InlineData(new int[0], 3, null)]
        [InlineData(null, 3, null)]
        public void BruteForce_Passes(IEnumerable<int> nums, int target, IEnumerable<int> expectedResult)
        {
            TwoSum
                .BruteForce(nums, target)
                .Should()
                .BeEquivalentTo(expectedResult ?? Enumerable.Empty<int>());
        }

        [Theory]
        [InlineData(new[] { 4, -2, 5, 0, 6, 3, 2, 7 }, 1, new[] { 5, 1 })]
        [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
        [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
        [InlineData(new[] { 3, 2, 4 }, 4, null)]
        [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
        [InlineData(new[] { 1 }, 3, null)]
        [InlineData(new int[0], 3, null)]
        [InlineData(null, 3, null)]
        public void Optimized_Passes(IEnumerable<int> nums, int target, IEnumerable<int> expectedResult)
        {
            TwoSum
                .Optimized(nums, target)
                .Should()
                .BeEquivalentTo(expectedResult ?? Enumerable.Empty<int>());
        }
    }
}
