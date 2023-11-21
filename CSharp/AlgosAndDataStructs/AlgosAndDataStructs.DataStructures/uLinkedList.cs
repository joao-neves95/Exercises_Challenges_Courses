using AlgosAndDataStructs.DataStructures.Traits;
using AlgosAndDataStructs.DataStructures.Types;

using System.Collections;

namespace AlgosAndDataStructs.DataStructures
{
    public class uLinkedList<TValue> : ICollection<TValue>, ICountable
    {
        public Node<TValue>? Head { get; private set; }

        public Node<TValue>? Tail { get; private set; }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

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

        ~uLinkedList()
        {
            Clear();
        }

        int ICountable.Count()
        {
            return Count;
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
            else
            {
                Head = new Node<TValue>
                {
                    Value = value,
                    NextNode = Head,
                };

                ++Count;
            }

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
            else
            {
                var before = TraverseTo(index.Value - 1);

                if (before == null)
                {
                    Append(value);
                }

                var newNode = new Node<TValue>() { Value = value };
                newNode.NextNode = before.NextNode;
                before.NextNode = newNode;
                ++Count;
            }

            return this;
        }

        /// <summary>
        /// Removes the item at index and returns it. Reference type values must be destroyed by the caller. <br />
        /// O(n) if removing from the middle or the end,
        /// or O(1) if removing from the front.
        /// </summary>
        public TValue? RemoveAt(Index index)
        {
            TValue? deletedValue = default;

            if (Head == null)
            {
                return deletedValue;
            }
            else if (index.Value == 0)
            {
                deletedValue = Head.Value;
                Head = Head.NextNode;
            }
            else
            {
                var before = TraverseTo(index.Value - 1);

                deletedValue = before?.NextNode == null
                    ? default
                    : before.NextNode.Value;

                before!.NextNode = before.NextNode?.NextNode;

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
            return RemoveAt(0);
        }

        /// <summary>
        /// Removes the last item and returns it. <br />
        /// O(n)
        /// </summary>
        public TValue? Pop()
        {
            return RemoveAt(new Index(Count).Value - 1);
        }

        public uLinkedList<TValue> ReverseNotInPlace()
        {
            var reversedNodes = new Node<TValue>[Count];

            var currentNode = Head;

            for (var i = Count - 1; i >= 0; --i, currentNode = currentNode!.NextNode)
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
        /// O(n), or O(1) if traversing to the edges.
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

        #region ICollection

        public void Add(TValue item)
        {
            Append(item);
        }

        public bool Contains(TValue item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TValue item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion ICollection
    }
}
