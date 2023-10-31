using AlgosAndDataStructs.DataStructures.Types;

namespace AlgosAndDataStructs.DataStructures
{
    public class uLinkedList<TValue>
        where TValue : notnull
    {
        public Node<TValue>? Head { get; private set; }

        public Node<TValue>? Tail { get; private set; }

        public uint Count { get; private set; }

        public uLinkedList()
        {
        }

        /// <summary>
        /// O(n)
        /// </summary>
        /// <param name="initialValues"></param>
        public uLinkedList(IEnumerable<TValue> initialValues)
        {
            foreach (var value in initialValues)
            {
                Append(value);
            }
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public uLinkedList<TValue> Append(TValue value)
        {
            var newNode = new Node<TValue>() { Value = value };

            if (Head == null)
            {
                Head = newNode;
                Tail = Head;
            }
            else
            {
                Tail!.NextNode = newNode;
                Tail = newNode;
            }

            ++Count;

            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public uLinkedList<TValue> Prepend(TValue value)
        {
            if (Head == null)
            {
                Append(value);
            }

            Head = new Node<TValue>
            {
                Value = value,
                NextNode = Head,
            };

            ++Count;

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public uLinkedList<TValue> Insert(TValue value, Index index)
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

            var newNode = new Node<TValue>() { Value = value };
            newNode.NextNode = before.NextNode;
            before.NextNode = newNode;

            ++Count;

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public uLinkedList<TValue> Remove(Index index)
        {
            if (Head == null)
            {
                return this;
            }
            else if (index.Value == 0)
            {
                Head = Head.NextNode;
            }
            else
            {
                var before = TraverseTo(index.Value - 1);
                before.NextNode = before.NextNode?.NextNode;

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
        public uLinkedList<TValue> Pop()
        {
            return Remove(new Index((int)Count).Value - 1);
        }

        public uLinkedList<TValue> ReverseNotInPlace()
        {
            var reversedNodes = new Node<TValue>[Count];

            var currentNode = Head;

            for (var i = (int)Count - 1; i >= 0; --i, currentNode = currentNode!.NextNode)
            {
                reversedNodes[i] = currentNode!;
            }

            Head = reversedNodes[0];
            Tail = reversedNodes[Count - 1];
            Tail.NextNode = null;

            currentNode = Head;
            for (var i = 1; i < Count; ++i, currentNode = currentNode!.NextNode)
            {
                currentNode!.NextNode = reversedNodes[i];
            }

            return this;
        }

        /*
            0 -> 1 -> 2 -> 3
            0 <- 1 <- 2 <- 3
            // becomes
            3 -> 2 -> 1 -> 0

            0 -> null, 1 -> 0, 2 -> 1, 3 -> null
        */
        public uLinkedList<TValue> ReverseInPlace()
        {
            if (Head == null)
            {
                return this;
            }

            // 0, 1, 2, etc.
            var currentNodeRef = Head!;
            // null, 0, 1, 2, etc.
            Node<TValue>? previousNodeRef = null;

            for (var i = 0; i < Count; ++i)
            {
                // 1
                var nextNodeRef = currentNodeRef?.NextNode;

                // 0 -> null, 1 -> 0, etc.
                currentNodeRef.NextNode = previousNodeRef;

                // 0
                previousNodeRef = currentNodeRef;
                // 1
                currentNodeRef = nextNodeRef;
            }

            (Tail, Head) = (Head, Tail);

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        private Node<TValue>? TraverseTo(Index index)
        {
            if (index.Value <= 0)
            {
                return Head;
            }
            else if (index.Value >= Count - 1)
            {
                return Tail;
            }

            Node<TValue> currentItem = Head!;

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
