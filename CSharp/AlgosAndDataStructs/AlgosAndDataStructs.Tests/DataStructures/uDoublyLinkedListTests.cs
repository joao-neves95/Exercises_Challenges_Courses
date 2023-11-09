using AlgosAndDataStructs.DataStructures;

using FluentAssertions;

namespace AlgosAndDataStructs.Tests.DataStructures
{
    public class uDoublyLinkedListTests
    {
        [Fact]
        public void Create_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>();
            linkedList.Head.Should().Be(null);
            linkedList.Tail.Should().Be(null);

            linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2 });

            TestLinkedValues(linkedList, new[] { 0, 1, 2 });
        }

        [Fact]
        public void Insert_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2, 3 });
            linkedList.Insert(100, 2);

            TestLinkedValues(linkedList, new[] { 0, 1, 100, 2, 3 });
        }

        [Fact]
        public void Append_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2 });
            linkedList.Append(3);

            TestLinkedValues(linkedList, new[] { 0, 1, 2, 3 });
        }

        [Fact]
        public void Prepend_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2 });
            linkedList.Prepend(100);

            TestLinkedValues(linkedList, new[] { 100, 0, 1, 2 });
        }

        [Fact]
        public void Delete_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 100, 3 });
            linkedList.Remove(2);
            TestLinkedValues(linkedList, new[] { 0, 1, 3 });

            linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2, 3 });
            linkedList.Remove(0);
            TestLinkedValues(linkedList, new[] { 1, 2, 3 });

            linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2 });
            linkedList.Remove(2);
            TestLinkedValues(linkedList, new[] { 0, 1 });
        }

        [Fact]
        public void Pop_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 100, 3 });
            linkedList.Pop();

            TestLinkedValues(linkedList, new[] { 0, 1, 100 });
        }

        [Fact]
        public void PopFront_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 100, 0, 1, 2, 3 });
            linkedList.PopFront();

            TestLinkedValues(linkedList, new[] { 0, 1, 2, 3 });
        }

        [Fact]
        public void Reverse_Passes()
        {
            var linkedList = new uDoublyLinkedList<int>(new[] { 0, 1, 2, 3 });
            linkedList.Reverse();
            TestLinkedValues(linkedList, new[] { 3, 2, 1, 0 });

            linkedList = new uDoublyLinkedList<int>(new[] { 0 });
            linkedList.Reverse();
            TestLinkedValues(linkedList, new[] { 0 });
        }

        private static void TestLinkedValues(uDoublyLinkedList<int> linkedList, IEnumerable<int> values)
        {
            linkedList.Count.Should().Be(values.Count());
            linkedList.Head?.PreviousNode.Should().Be(null);
            linkedList.Head?.Value.Should().Be(values.First());

            var curentNode = linkedList.Head;
            for (var i = 0; i < linkedList.Count; ++i, curentNode = curentNode.NextNode)
            {
                curentNode!.Value.Should().Be(values.ElementAt(i));

                if (i != linkedList.Count - 1)
                {
                    curentNode!.NextNode!.Value.Should().Be(values.ElementAt(i + 1));
                    curentNode!.PreviousNode?.Value.Should().Be(GetElementAtOrNull(values, i - 1));
                }
                else
                {
                    curentNode!.NextNode.Should().Be(null);
                }
            }

            linkedList.Tail?.PreviousNode?.Value.Should().Be(GetElementAtOrNull(values, values.Count() - 2));
            linkedList.Tail?.Value.Should().Be(values.Last());
            linkedList.Tail?.NextNode.Should().Be(null);
        }

        private static int? GetElementAtOrNull(IEnumerable<int> values, int index)
        {
            try
            {
                return values.ElementAt(index);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
