namespace AlgosAndDataStructs.DataStructures.Abstractions
{
    public interface IStack<T>
    {
        public uint Count { get; }

        public void Clear();

        public bool IsEmpty();

        public IStack<T> Push(T value);

        public T? Pop();

        public T? Peek();
    }
}
