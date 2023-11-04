using AlgosAndDataStructs.DataStructures;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class uStackTests
    {
        [Fact]
        public void Create_Passes()
        {
            var stack = new uStack<int>();
            stack.Peek().Should().Be(default);

            stack = new uStack<int>(new[] { 0, 1, 2 });
            stack.Peek().Should().Be(2);
        }

        [Fact]
        public void DeEnQueuing_Passes()
        {
            var stack = new uStack<int>();
            stack.Peek().Should().Be(default);

            stack.Push(0);
            stack.Push(1);
            stack.Peek().Should().Be(1);
            stack.Pop().Should().Be(1);
            stack.Peek().Should().Be(0);
            stack.Push(2);
            stack.Peek().Should().Be(2);
            stack.Pop().Should().Be(2);
            stack.Pop().Should().Be(0);
        }
    }
}
