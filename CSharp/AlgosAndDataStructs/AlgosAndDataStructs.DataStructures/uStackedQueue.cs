using AlgosAndDataStructs.DataStructures.Abstractions;
using AlgosAndDataStructs.DataStructures.Traits;

namespace AlgosAndDataStructs.DataStructures
{
    internal class uStackedQueue<T> : IQueue<T>
    {
        private readonly uVanillaStack<T> stack;

        public int Count { get { return stack.Count; } }

        public uStackedQueue()
        {
            stack = new uVanillaStack<T>();
        }

        int ICountable.Count()
        {
            return Count;
        }

        public void Clear()
        {
            stack.Clear();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public IQueue<T> Enqueue(T value)
        {
            stack.Push(value);

            return this;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public T? Dequeue()
        {
            return GetFirstIn(true);
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public T? Peek()
        {
            return GetFirstIn(false);
        }

        /// <summary>
        /// O(n)
        /// </summary>
        private T? GetFirstIn(bool pop)
        {
            if (stack.Count == 0)
            {
                return default;
            }
            else if (stack.Count == 1)
            {
                return pop ? stack.Pop() : stack.Peek();
            }

            var reversed = new T[Count];

            var currentValue = stack.Pop();
            for (int i = 0; i < reversed.Length; ++i, currentValue = stack.Pop())
            {
                reversed[i] = currentValue!;
            }

            for (int i = reversed.Length - (pop ? 2 : 1); i >= 0; --i)
            {
                stack.Push(reversed[i]);
            }

            return reversed[reversed.Length - 1];
        }
    }
}
