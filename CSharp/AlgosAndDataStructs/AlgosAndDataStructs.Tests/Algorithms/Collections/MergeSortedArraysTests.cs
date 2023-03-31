
using AlgosAndDataStructs.Algorithms.Collections;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.Collections
{
    public class MergeSortedArraysTests
    {
        [Theory]
        [InlineData(new int[0], new[] { 4, 6, 30, 35 }, new[] { 4, 6, 30, 35 })]
        [InlineData(new[] { 0, 3, 4, 31 }, new int[0], new[] { 0, 3, 4, 31 })]
        [InlineData(new[] { 0, 3, 4, 31 }, new[] { 4, 6, 30, 35 }, new[] { 0, 3, 4, 4, 6, 30, 31, 35 })]
        [InlineData(new[] { 0, 3, 4, 31 }, new[] { 4, 30, 35 }, new[] { 0, 3, 4, 4, 30, 31, 35 })]
        [InlineData(new[] { 0, 3, 31 }, new[] { 4, 6, 30, 35 }, new[] { 0, 3, 4, 6, 30, 31, 35 })]
        [InlineData(new[] { 4, 3, 4, 31 }, new[] { 0, 6, 30, 35 }, new[] { 0, 3, 4, 4, 6, 30, 31, 35 })]
        public void BruteForce_Int_Passes(
            IEnumerable<int> array1,
            IEnumerable<int> array2,
            IEnumerable<int> expectedResult)
        {
            MergeSortedArrays
                .BruteForce(array1, array2)
                .Should()
                .BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData(new char[0], new[] { 'B', 'E', 'F', 'G' }, new[] { 'B', 'E', 'F', 'G' })]
        [InlineData(new[] { 'A', 'C', 'F', 'Z' }, new char[0], new[] { 'A', 'C', 'F', 'Z' })]
        [InlineData(new[] { 'A', 'C', 'F', 'Z' }, new[] { 'B', 'E', 'F', 'G' }, new[] { 'A', 'B', 'C', 'E', 'F', 'F', 'G', 'Z' })]
        [InlineData(new[] { 'A', 'C', 'Z' }, new[] { 'B', 'E', 'F', 'G' }, new[] { 'A', 'B', 'C', 'E', 'F', 'G', 'Z' })]
        [InlineData(new[] { 'A', 'C', 'F', 'Z' }, new[] { 'B', 'E', 'G' }, new[] { 'A', 'B', 'C', 'E', 'F', 'G', 'Z' })]
        public void BruteForce_String_Passes(
            IEnumerable<char> array1,
            IEnumerable<char> array2,
            IEnumerable<char> expectedResult)
        {
            MergeSortedArrays
                .BruteForce(array1, array2)
                .Should()
                .BeEquivalentTo(expectedResult);
        }
    }
}
