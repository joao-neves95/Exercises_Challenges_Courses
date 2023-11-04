namespace AlgosAndDataStructs.DataStructures
{
    public class uQueue<T>
    {
        private readonly uDoublyLinkedList<T> values;

        public uQueue()
        {
            values = new uDoublyLinkedList<T>();
        }

        public uQueue(IEnumerable<T> initialValues)
        {
            values = new uDoublyLinkedList<T>(initialValues);
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
            return values.Remove(0);
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
