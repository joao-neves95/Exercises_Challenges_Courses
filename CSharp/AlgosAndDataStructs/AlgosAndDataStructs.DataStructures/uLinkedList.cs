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

            var newNode = new Node<TValue>() { Value = value };
            newNode.NextNode = Head;
            Head = newNode;
            ++Count;

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public uLinkedList<TValue> Insert(TValue value, uint index)
        {
            if (Head == null || index >= Count)
            {
                Append(value);
            }

            var before = TraverseTo(index - 1);

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
        public uLinkedList<TValue> Remove(uint index)
        {
            var before = TraverseTo(index - 1);

            if (before == null)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                before.NextNode = before.NextNode?.NextNode;

                if (index >= Count - 1)
                {
                    Tail = before;
                    Tail.NextNode = null;
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
            return Remove(Count - 1);
        }

        public uLinkedList<TValue> Reverse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// O(n)
        /// </summary>
        private Node<TValue>? TraverseTo(uint index)
        {
            if (index <= 0)
            {
                return Head;
            }
            else if (index >= Count - 1)
            {
                return Tail;
            }

            Node<TValue> currentItem = Head!;

            for (var i = 0; currentItem != null; ++i)
            {
                if (i == index)
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
