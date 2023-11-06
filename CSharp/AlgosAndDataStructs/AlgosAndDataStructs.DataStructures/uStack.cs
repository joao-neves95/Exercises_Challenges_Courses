namespace AlgosAndDataStructs.DataStructures
{
    public class uStack<T>
    {
        private readonly uLinkedList<T> values;

        public uStack()
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
        public uStack<T> Push(T value)
        {
            values.Prepend(value);

            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Pop()
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
