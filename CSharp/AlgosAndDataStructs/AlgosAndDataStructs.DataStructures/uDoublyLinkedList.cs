using AlgosAndDataStructs.DataStructures.Types;

namespace AlgosAndDataStructs.DataStructures
{
    public class uDoublyLinkedList<TValue>
        where TValue : notnull
    {
        public DoubleNode<TValue>? Head { get; private set; }

        public DoubleNode<TValue>? Tail { get; private set; }

        public int Count { get; private set; }

        public uDoublyLinkedList()
        {
        }

        public uDoublyLinkedList(IEnumerable<TValue> initialValues)
        {
            foreach (var value in initialValues)
            {
                Append(value);
            }
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public uDoublyLinkedList<TValue> Append(TValue value)
        {
            var newNode = new DoubleNode<TValue>() { Value = value };

            if (Head == null)
            {
                Head = newNode;
                Tail = Head;
            }
            else
            {
                Tail!.NextNode = newNode;
                newNode.PreviousNode = Tail;
                Tail = newNode;
            }

            ++Count;
            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public uDoublyLinkedList<TValue> Prepend(TValue value)
        {
            if (Head == null)
            {
                Append(value);
            }

            var newNode = new DoubleNode<TValue>() { Value = value };

            Head.PreviousNode = newNode;
            newNode.NextNode = Head;
            Head = newNode;
            ++Count;

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public uDoublyLinkedList<TValue> Insert(TValue value, Index index)
        {
            if (Head == null || index.Value >= Count)
            {
                Append(value);
            }

            var before = TraverseTo(index.Value - 1);

            if (before == null)
            {
                Append(value);
            }

            var newNode = new DoubleNode<TValue>
            {
                Value = value,
                NextNode = before.NextNode
            };

            before.NextNode.PreviousNode = newNode;

            newNode.PreviousNode = before;
            before.NextNode = newNode;

            ++Count;

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public uDoublyLinkedList<TValue> Remove(Index index)
        {
            if (Head == null)
            {
                return this;
            }
            else if (index.Value == 0)
            {
                Head = Head.NextNode;
                if (Head != null) Head.PreviousNode = null;
            }
            else
            {
                var before = TraverseTo(index.Value - 1);

                before.NextNode = before.NextNode?.NextNode;
                if (before.NextNode != null) before.NextNode.PreviousNode = before;

                if (index.Value == Count - 1)
                {
                    Tail = before;

                    if (Tail != null) Tail.NextNode = null;
                }
            }

            --Count;

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public uDoublyLinkedList<TValue> Pop()
        {
            return Remove(Count - 1);
        }

        public uDoublyLinkedList<TValue> Reverse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// O(n)
        /// </summary>
        private DoubleNode<TValue>? TraverseTo(Index index)
        {
            if (index.Value == 0)
            {
                return Head;
            }
            else if (index.Value >= Count - 1)
            {
                return Tail;
            }

            DoubleNode<TValue> currentItem = Head!;

            for (var i = 0; currentItem != null; ++i)
            {
                if (i == index.Value)
                {
                    return currentItem;
                }
                else
                {
                    currentItem = currentItem.NextNode!;
                }
            }

            return null;
        }
    }
}
