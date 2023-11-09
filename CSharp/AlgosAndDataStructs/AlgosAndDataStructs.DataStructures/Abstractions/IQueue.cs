namespace AlgosAndDataStructs.DataStructures.Abstractions
{
    public interface IQueue<T>
    {
        public uint Count { get; }

        public void Clear();

        public IQueue<T> Enqueue(T value);

        public T? Dequeue();

        public T? Peek();
    }
}
