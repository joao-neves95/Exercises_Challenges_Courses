
namespace AlgosAndDataStructs.DataStructures
{
    public class DynamicArray<T>
    {
        private T?[] _Data;

        public int Length { get; private set; } = 0;
        public int Size { get; private set; } = 0;

        public DynamicArray()
        {
            Size = 20;
            _Data = new T[Size];
        }

        public DynamicArray(int size)
        {
            Size = size;
            _Data = new T[Size];
        }

        ~DynamicArray()
        {
            _Data = null!;
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        /// <param name="index"></param>
        public T? Get(int index)
        {
            if (index < 0 || index >= Size)
            {
                return default;
            }

            return _Data[index];
        }

        /// <summary>
        /// O(1) || O(n)
        ///
        /// </summary>
        /// <param name="item"></param>
        public DynamicArray<T> Add(T item)
        {
            if (NeedsIncrease())
            {
                IncreaseSize();
            }

            _Data[Length] = item;
            ++Length;

            return this;
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        /// <param name="data"></param>
        public DynamicArray<T> Fill(T data)
        {
            for (int i = 0; i < Size; ++i)
            {
                Add(data);
            }

            return this;
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        public DynamicArray<T> Pop()
        {
            return Delete(Length - 1);
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        public T Peek()
        {
            return _Data[Length - 1]!;
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DynamicArray<T> Delete(int index)
        {
            if (NeedsDecrease())
            {
                DecreaseSize();
            }

            for (int i = index; i < _Data.Length - 1; ++i)
            {
                _Data[i] = _Data[i + 1];
            }

            _Data[Length - 1] = default;
            --Length;

            return this;
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        private bool NeedsIncrease()
        {
            return Length == Size;
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        private void IncreaseSize()
        {
            Resize(Size * 2);
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        private bool NeedsDecrease()
        {
            return Length == Size / 4;
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        private void DecreaseSize()
        {
            Resize(Size / 2);
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        private void Resize(int newSize)
        {
            Size = newSize;
            T?[] oldData = _Data;

            _Data = new T[Size];
            for (int i = 0; i < oldData.Length; ++i)
            {
                _Data[i] = oldData[i];
            }
        }
    }
}
