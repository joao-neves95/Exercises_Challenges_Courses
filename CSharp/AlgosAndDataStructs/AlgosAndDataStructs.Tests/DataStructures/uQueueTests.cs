using AlgosAndDataStructs.DataStructures;
using AlgosAndDataStructs.DataStructures.Abstractions;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class uQueueTests
    {
        [Fact]
        public void Create_Passes()
        {
            new IQueue<int>[] { new uQueue<int>(), new uVanillaQueue<int>() }
                .Should()
                .AllSatisfy(stack => stack.Peek().Should().Be(default));
        }

        [Fact]
        public void DeEnQueuing_Passes()
        {
            var all = new IQueue<int>[] { new uQueue<int>(), new uVanillaQueue<int>(), new uStackedQueue<int>() };
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(default));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(0));

            Array.ForEach(all, queue => queue.Enqueue(0));
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(0));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(1));

            Array.ForEach(all, queue => queue.Enqueue(1));
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(0));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(2));

            all.Should().AllSatisfy(queue => queue.Dequeue().Should().Be(0));
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(1));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(1));

            Array.ForEach(all, queue => queue.Enqueue(2));
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(1));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(2));

            all.Should().AllSatisfy(queue => queue.Dequeue().Should().Be(1));
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(2));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(1));

            all.Should().AllSatisfy(queue => queue.Dequeue().Should().Be(2));
            all.Should().AllSatisfy(queue => queue.Peek().Should().Be(default));
            all.Should().AllSatisfy(queue => queue.Count.Should().Be(0));
        }
    }
}
