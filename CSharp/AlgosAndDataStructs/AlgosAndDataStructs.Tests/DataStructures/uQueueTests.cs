using AlgosAndDataStructs.DataStructures;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class uQueueTests
    {
        [Fact]
        public void Create_Passes()
        {
            var queue = new uQueue<int>();
            queue.Peek().Should().Be(default);

            queue = new uQueue<int>(new[] { 0, 1, 2 });
            queue.Peek().Should().Be(0);
        }

        [Fact]
        public void DeEnQueuing_Passes()
        {
            var queue = new uQueue<int>();
            queue.Peek().Should().Be(default);

            queue.Enqueue(0);
            queue.Enqueue(1);
            queue.Peek().Should().Be(0);
            queue.Dequeue().Should().Be(0);
            queue.Peek().Should().Be(1);
            queue.Enqueue(2);
            queue.Peek().Should().Be(1);
            queue.Dequeue().Should().Be(1);
            queue.Dequeue().Should().Be(2);
        }
    }
}
