using AlgosAndDataStructs.DataStructures.Abstractions;
using AlgosAndDataStructs.DataStructures.Traits;
using AlgosAndDataStructs.DataStructures.Types;

namespace AlgosAndDataStructs.DataStructures
{
    internal class uVanillaQueue<T> : IQueue<T>
    {
        private Node<T>? Front;

        private Node<T>? Back;

        public int Count { get; private set; }

        int ICountable.Count()
        {
            return Count;
        }

        public void Clear()
        {
            Front = null;
            Back = null;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public IQueue<T> Enqueue(T value)
        {
            var newNode = new Node<T>() { Value = value };

            if (Front == null)
            {
                Front = newNode;
                Back = newNode;
            }
            else
            {
                Back!.NextNode = newNode;
                Back = newNode;
            }

            ++Count;

            return this;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Dequeue()
        {
            if (Front == null)
            {
                return default;
            }

            var removedItem = Front;
            Front = Front.NextNode;

            --Count;

            return removedItem.Value;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        public T? Peek()
        {
            return Front == null
                ? default
                : Front.Value;
        }
    }
}
