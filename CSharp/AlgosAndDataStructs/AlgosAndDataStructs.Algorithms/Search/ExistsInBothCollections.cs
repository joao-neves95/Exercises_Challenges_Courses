namespace AlgosAndDataStructs.Algorithms.Search
{
    public sealed class ExistsInBothCollections<T> : IAlgorithm<IEnumerable<T>, IEnumerable<T>, bool>
    {
        /// <summary>
        /// Speed: O(a * b) <br />
        /// Memory: O(1)
        ///
        /// </summary>
        public static bool BruteForce(IEnumerable<T> array1, IEnumerable<T> array2)
        {
            if (!IsInputValid(array1, array2))
            {
                return false;
            }

            // O(a).
            for (int i = 0; i < array1.Count(); ++i)
            {
                // O(b).
                for (int j = 0; j < array2.Count(); ++j)
                {
                    if (array1.ElementAt(i)?.Equals(array2.ElementAt(j)) == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Speed: O(a + b) <br />
        /// Memory: O(MAX(a, b))
        ///
        /// </summary>
        public static bool Optimized(IEnumerable<T> array1, IEnumerable<T> array2)
        {
            if (!IsInputValid(array1, array2))
            {
                return false;
            }

            // Don't do this logic.
            // It adds complexity and this type of optimizations should only be done when needed.
            bool array1IsBigger = array1.Count() >= array2.Count();

            HashSet<T> set = ArrayToHashSet(array1IsBigger ? array1 : array2)!;

            // O(b) + O(1) = O(b).
            return (array1IsBigger ? array2 : array1)
                .Any(item => set.Contains(item));
        }

        private static bool IsInputValid(IEnumerable<T> array1, IEnumerable<T> array2)
        {
            return array1 is not null && array2 is not null && array1.Any() && array2.Any();
        }

        /// <summary>
        /// O(n)
        ///
        /// </summary>
        private static HashSet<T>? ArrayToHashSet(IEnumerable<T> array)
        {
            if (array?.Any() != true)
            {
                return default!;
            }

            HashSet<T> set = new(array.Count());

            // O(n).
            foreach (T item in array)
            {
                if (set.Contains(item))
                {
                    continue;
                }

                set.Add(item);
            }

            return set;
        }
    }
}
