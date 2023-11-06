namespace AlgosAndDataStructs.DataStructures
{
    public class uStack<T>
    {
        private readonly uDoublyLinkedList<T> values;

        public uStack()
        {
            values = new uDoublyLinkedList<T>();
        }

        public uStack(IEnumerable<T> initialValues)
        {
            values = new uDoublyLinkedList<T>(initialValues);
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public uStack<T> Push(T value)
        {
            values.Append(value);

            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Pop()
        {
            return values.Pop();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Peek()
        {
            return values.Tail == null
                ? default
                : values.Tail.Value;
        }
    }
}
