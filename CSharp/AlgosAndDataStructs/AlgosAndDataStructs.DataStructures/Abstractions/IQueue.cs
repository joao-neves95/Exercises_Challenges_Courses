using AlgosAndDataStructs.DataStructures.Traits;

namespace AlgosAndDataStructs.DataStructures.Abstractions
{
    public interface IQueue<T> : ICountable
    {
        public new int Count { get; }

        public void Clear();

        public IQueue<T> Enqueue(T value);

        public T? Dequeue();

        public T? Peek();
    }
}
