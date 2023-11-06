using AlgosAndDataStructs.Algorithms.Numbers;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.Algorithms.Numbers
{
    public class IsPalindromeTests
    {
        [Fact]
        public void BruteForce_Passes()
        {
            IsPalindrome.BruteForce(121).Should().Be(true);
            IsPalindrome.BruteForce(-121).Should().Be(false);
            IsPalindrome.BruteForce(10).Should().Be(false);
            IsPalindrome.BruteForce(1874994781).Should().Be(true);
            IsPalindrome.BruteForce(1000000001).Should().Be(true);
        }

        [Fact]
        public void Optimized_Passes()
        {
            IsPalindrome.Optimized(121).Should().Be(true);
            IsPalindrome.Optimized(-121).Should().Be(false);
            IsPalindrome.Optimized(10).Should().Be(false);
            IsPalindrome.Optimized(1874994781).Should().Be(true);
            IsPalindrome.Optimized(1000000001).Should().Be(true);
        }
    }
}
