using AlgosAndDataStructs.DataStructures.Abstractions;
using AlgosAndDataStructs.DataStructures.Traits;
using AlgosAndDataStructs.DataStructures.Types;

namespace AlgosAndDataStructs.DataStructures
{
    internal class uVanillaStack<T> : IStack<T>
    {
        private Node<T>? Top;

        public int Count { get; private set; }

        int ICountable.Count()
        {
            return Count;
        }

        public void Clear()
        {
            Top = null;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public bool IsEmpty()
        {
            return Top == null;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public IStack<T> Push(T value)
        {
            var newNode = new Node<T> { Value = value };

            if (IsEmpty())
            {
                Top = newNode;
            }
            else
            {
                newNode.NextNode = Top;
                Top = newNode;
            }

            ++Count;

            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Pop()
        {
            if (IsEmpty())
            {
                return default;
            }

            var topRef = Top;
            Top = Top?.NextNode;

            --Count;

            return topRef!.Value;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Peek()
        {
            if (IsEmpty())
            {
                return default;
            }

            return Top!.Value;
        }
    }
}
