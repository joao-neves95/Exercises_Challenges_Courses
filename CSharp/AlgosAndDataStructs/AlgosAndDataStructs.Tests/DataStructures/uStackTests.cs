using AlgosAndDataStructs.DataStructures;
using AlgosAndDataStructs.DataStructures.Abstractions;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class uStackTests
    {
        [Fact]
        public void Create_Passes()
        {
            var stack = new uStack<int>();
            var vanillaStack = new uVanillaStack<int>();

            new IStack<int>[] { stack, vanillaStack }
                .Should()
                .AllSatisfy(stack => stack.Peek().Should().Be(default));
        }

        [Fact]
        public void DeEnQueuing_Passes()
        {
            var both = new IStack<int>[] { new uStack<int>(), new uVanillaStack<int>() };
            both.Should().AllSatisfy(stack => stack.Peek().Should().Be(default));
            both.Should().AllSatisfy(stack => stack.Count.Should().Be(0));

            Array.ForEach(both, stack => stack.Push(0));
            Array.ForEach(both, stack => stack.Push(1));
            both.Should().AllSatisfy(stack => stack.Peek().Should().Be(1));
            both.Should().AllSatisfy(stack => stack.Count.Should().Be(2));

            both.Should().AllSatisfy(stack => stack.Pop().Should().Be(1));
            both.Should().AllSatisfy(stack => stack.Peek().Should().Be(0));
            both.Should().AllSatisfy(stack => stack.Count.Should().Be(1));

            Array.ForEach(both, stack => stack.Push(2));
            both.Should().AllSatisfy(stack => stack.Peek().Should().Be(2));
            both.Should().AllSatisfy(stack => stack.Count.Should().Be(2));

            both.Should().AllSatisfy(stack => stack.Pop().Should().Be(2));
            both.Should().AllSatisfy(stack => stack.Peek().Should().Be(0));
            both.Should().AllSatisfy(stack => stack.Count.Should().Be(1));

            both.Should().AllSatisfy(stack => stack.Pop().Should().Be(0));
            both.Should().AllSatisfy(stack => stack.Peek().Should().Be(default));
            both.Should().AllSatisfy(stack => stack.Count.Should().Be(0));
        }
    }
}
