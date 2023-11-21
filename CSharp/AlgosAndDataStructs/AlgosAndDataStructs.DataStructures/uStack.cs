using AlgosAndDataStructs.DataStructures.Abstractions;
using AlgosAndDataStructs.DataStructures.Traits;

namespace AlgosAndDataStructs.DataStructures
{
    public class uStack<T> : IStack<T>
    {
        private readonly uLinkedList<T> values;

        public int Count { get { return values.Count; } }

        public uStack()
        {
            values = new uLinkedList<T>();
        }

        int ICountable.Count()
        {
            return Count;
        }

        public bool IsEmpty()
        {
            return values.Head == null;
        }

        public void Clear()
        {
            values.Clear();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public IStack<T> Push(T value)
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
