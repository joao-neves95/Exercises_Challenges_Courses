using System.Diagnostics.CodeAnalysis;

namespace AlgosAndDataStructs.DataStructures
{
    public class HashTable<T>
    {
        private KeyValuePair<string?, T?>?[] Data { get; set; }

        public uint Size { get { return (uint)Data.Length; } }

        public uint Count { get; private set; } = 0;

        public HashTable(int initialSize)
        {
            Data = null!;
            ReallocHashTableMemory(initialSize);
        }

        /// <summary>
        /// Initializes the HashTable with the initial values provided.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="initialSize">If null, the size will be set to <paramref name="data"/>.Length </param>.
        public HashTable([NotNull] IEnumerable<KeyValuePair<string, T>> data, int? initialSize = null)
        {
            Data = data.Select(item => (KeyValuePair<string?, T?>?)item!).ToArray();
            ReallocHashTableMemory(initialSize != null && initialSize > data.Count() ? initialSize.Value : data.Count());
        }

        ~HashTable()
        {
            Data = null!;
        }

        /// <summary>
        /// ~O(1)
        /// </summary>
        public T? Get([NotNull] string key)
        {
            var index = GetExistingKeyIndex(key);

            if (index == null)
            {
                return default;
            }

            return Data[index.Value]!.Value.Value;
        }

        /// <summary>
        /// ~O(1), O(n) in case of a resize.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add([NotNull] string key, [NotNull] T value)
        {
            if (Count == Data.Length)
            {
                ReallocHashTableMemory(Data.Length * 2);
            }

            Data[GetNewAvailableKeyIndex(key)] = new KeyValuePair<string?, T?>(key, value);
            ++Count;
        }

        /// <summary>
        /// ~O(1), O(n) in case of a resize.
        /// </summary>
        public bool Remove([NotNull] string key)
        {
            if (Count == Math.Ceiling(Data.Length / (float)4))
            {
                ReallocHashTableMemory(Data.Length / 2);
            }

            var index = GetExistingKeyIndex(key);

            if (index == null)
            {
                return false;
            }

            // We mark the deleted value as an empty KeyValuePair instead of simply null, so we
            // know that there was an item here, and we can continue to search in case of a collision.
            Data[index.Value] = new KeyValuePair<string?, T?>(null, default!);
            --Count;

            return true;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public IEnumerable<string> Keys()
        {
            foreach (var keyValue in Data)
            {
                if (keyValue == null || keyValue.Value.Key == null)
                {
                    continue;
                }

                yield return keyValue.Value.Key;
            }
        }

        /// <summary>
        /// O(n)
        /// </summary>
        public IEnumerable<T> Values()
        {
            foreach (var keyValue in Data)
            {
                if (keyValue == null || keyValue.Value.Key == null)
                {
                    continue;
                }

                yield return keyValue.Value.Value!;
            }
        }

        /// <summary>
        /// ~O(1)
        /// </summary>
        private int? GetExistingKeyIndex(string key)
        {
            for (int i = 0; i < Data.Length; ++i)
            {
                var item = Data[DoubleHashKey(key, i)];

                if (item == null)
                {
                    return null;
                }
                else if (item.Value.Key == key)
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// ~O(1)
        /// </summary>
        private int GetNewAvailableKeyIndex(string key, KeyValuePair<string?, T?>?[]? target = null)
        {
            target ??= Data;

            for (int i = 0; i < target.Length; ++i)
            {
                var item = target[DoubleHashKey(key, i)];

                if ((item?.Key?.Length ?? 0) == 0)
                {
                    return i;
                }
            }

            return target.Length;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        private int DoubleHashKey(string key, int collisionIndex = 0)
        {
            return (Hash(key) + Hash(key, collisionIndex)) % Data.Length;
        }

        /// <summary>
        /// O(1)
        /// </summary>
        private int Hash(string key, int collisionIndex = 0)
        {
            int hashIndex = 0;

            for (int i = 0; i < key.Length; i++)
            {
                hashIndex += ((key[i] * i) + collisionIndex) % Data.Length;
            }

            return hashIndex;
        }

        /// <summary>
        /// O(n)
        /// </summary>
        /// <param name="newSize"></param>
        private void ReallocHashTableMemory(int newSize)
        {
            var newArrayAlloc = new KeyValuePair<string?, T?>?[newSize];
            Count = 0;

            if ((Data?.Length ?? 0) != 0)
            {
                foreach (KeyValuePair<string?, T?>? item in Data!)
                {
                    if (item?.Key == null)
                    {
                        continue;
                    }

                    var newIndex = GetNewAvailableKeyIndex(item.Value.Key, newArrayAlloc);
                    newArrayAlloc[newIndex] = item;
                    ++Count;
                }
            }

            Data = null!;
            Data = newArrayAlloc;
        }
    }
}
