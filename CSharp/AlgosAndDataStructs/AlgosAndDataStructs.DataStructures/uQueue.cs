namespace AlgosAndDataStructs.DataStructures
{
    public class uQueue<T>
    {
        private readonly uLinkedList<T> values;

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
        public uQueue<T> Enqueue(T value)
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
