using AlgosAndDataStructs.DataStructures.Abstractions;

namespace AlgosAndDataStructs.DataStructures
{
    public class uQueue<T> : IQueue<T>
    {
        private readonly uLinkedList<T> values;

        public uint Count { get { return values.Count; } }

        public uQueue()
        {
            values = new uLinkedList<T>();
        }

        public void Clear()
        {
            values.Clear();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public IQueue<T> Enqueue(T value)
        {
            values.Append(value);

            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Dequeue()
        {
            return values.PopFront();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Peek()
        {
            return values.Head == null
                ? default
                : values.Head.Value;
        }
    }
}
