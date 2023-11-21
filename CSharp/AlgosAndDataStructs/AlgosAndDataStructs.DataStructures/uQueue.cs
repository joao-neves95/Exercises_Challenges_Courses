using AlgosAndDataStructs.DataStructures.Abstractions;
using AlgosAndDataStructs.DataStructures.Traits;

namespace AlgosAndDataStructs.DataStructures
{
    public class uQueue<T> : IQueue<T>
    {
        private readonly uLinkedList<T> values;

        public int Count { get { return values.Count; } }

        public uQueue()
        {
            values = new uLinkedList<T>();
        }

        int ICountable.Count()
        {
            return Count;
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
