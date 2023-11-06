using AlgosAndDataStructs.Algorithms.HashTables;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.HashTables
{
    public class FirstRecurringItemTests
    {
        [Theory]
        [InlineData(new[] { 2, 4, 5, 2, 7, 3, 9 }, 2)]
        [InlineData(new[] { 2, 4, 5, 7, 7, 3, 9 }, 7)]
        [InlineData(new[] { 2, 9, 5, 8, 7, 3, 9 }, 9)]
        [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, default)]
        [InlineData(new int[0], default)]
        public void BruteForceAndOptimized_Int_Passes(
            IEnumerable<int> array,
            int expectedResult)
        {
            FirstRecurringItem<int>.BruteForce(array).Should().Be(expectedResult);
            FirstRecurringItem<int>.Optimized(array).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(new[] { "2", "4", "5", "2", "7", "3", "9" }, "2")]
        [InlineData(new[] { "2", "4", "5", "7", "7", "3", "9" }, "7")]
        [InlineData(new[] { "2", "9", "5", "8", "7", "3", "9" }, "9")]
        [InlineData(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" }, default)]
        [InlineData(new string[0], default)]
        public void BruteForceAndOptimized_String_Passes(
            IEnumerable<string> array,
            string? expectedResult)
        {
            FirstRecurringItem<string>.BruteForce(array).Should().Be(expectedResult);
            FirstRecurringItem<string>.Optimized(array).Should().Be(expectedResult);
        }
    }
}
