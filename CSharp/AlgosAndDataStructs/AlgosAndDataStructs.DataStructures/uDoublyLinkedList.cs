using AlgosAndDataStructs.DataStructures.Types;

namespace AlgosAndDataStructs.DataStructures
{
    public class uDoublyLinkedList<TValue>
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

        ~uDoublyLinkedList()
        {
            Clear();
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
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
            else
            {
                var newNode = new DoubleNode<TValue>() { Value = value };

                Head.PreviousNode = newNode;
                newNode.NextNode = Head;
                Head = newNode;
                ++Count;
            }

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
        /// Removes the item at index and returns it. Reference type values must be destroyed by the caller.
        /// O(n), or O(1) if removing from the edges.
        /// </summary>
        public TValue? Remove(Index index)
        {
            TValue? deletedValue = default;

            if (Head == null)
            {
                return deletedValue;
            }
            else if (index.Value == 0)
            {
                deletedValue = Head!.Value;
                Head = Head.NextNode;
                if (Head != null) Head.PreviousNode = null;
            }
            else
            {
                var before = TraverseTo(index.Value - 1);

                deletedValue = before?.NextNode == null
                    ? default
                    : before.NextNode.Value;

                before.NextNode = before.NextNode?.NextNode;
                if (before.NextNode != null) before.NextNode.PreviousNode = before;

                if (index.Value == Count - 1)
                {
                    Tail = before;

                    if (Tail != null) Tail.NextNode = null;
                }
            }

            --Count;

            return deletedValue;
        }

        /// <summary>
        /// Removes the first item and returns it. <br />
        /// O(1)
        /// </summary>
        public TValue? PopFront()
        {
            return Remove(0);
        }

        /// <summary>
        /// Removes the last item and returns it. <br />
        /// O(1)
        /// </summary>
        public TValue? Pop()
        {
            return Remove(Count - 1);
        }

        /*
            null <- 0 -> <- 1 -> <- 2 -> <- 3 -> null
            // becomes
            null <- 3 -> <- 2 -> <- 1 -> <- 0 -> null

            Tasks: 1 <- 0 -> null, 2 <- 1 -> 0, 2 -> 1, 3 -> null
        */
        public uDoublyLinkedList<TValue> Reverse()
        {
            var currentNodeRef = Head;
            DoubleNode<TValue>? previousNodeRef = null;

            for (int i = 0; i < Count; ++i)
            {
                var nextNodeRef = currentNodeRef?.NextNode;

                currentNodeRef.NextNode = previousNodeRef;
                currentNodeRef.PreviousNode = nextNodeRef;

                previousNodeRef = currentNodeRef;
                currentNodeRef = nextNodeRef;
            }

            (Head, Tail) = (Tail, Head);

            return this;
        }

        /// <summary>
        /// O(n), or O(1) if traversing to the edges.
        /// </summary>
        private DoubleNode<TValue>? TraverseTo(Index index)
        {
            if (index.Value == 0)
            {
                return Head;
            }
            else if (index.Value >= Count - 2)
            {
                return Tail?.PreviousNode;
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
