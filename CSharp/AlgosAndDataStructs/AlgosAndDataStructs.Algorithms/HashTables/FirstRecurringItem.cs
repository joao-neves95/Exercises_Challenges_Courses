namespace AlgosAndDataStructs.Algorithms.HashTables
{
    /*
    * Notes:
    * 
    * [2, 1, 6, 2, 8, 7] = 2
    * [7, 8, 4, 1, 3, 8] = 8
    * 
    */
    public sealed class FirstRecurringItem<TValue> : IAlgorithm<IEnumerable<TValue>, TValue>
        where TValue : IComparable<TValue>
    {
        public static TValue? BruteForce(IEnumerable<TValue> array)
        {
            for (var i = 0; i < array.Count(); ++i)
            {
                var testItem = array.ElementAt(i);
                for (var j = i + 1; j < array.Count(); ++j)
                {
                    if (testItem.Equals(array.ElementAt(j)))
                    {
                        return testItem;
                    }
                }
            }

            return default;
        }

        public static TValue? Optimized(IEnumerable<TValue> array)
        {
            var visitedItems = new HashSet<TValue>();

            foreach (var item in array)
            {
                if (visitedItems.Contains(item))
                {
                    return item;
                }
                else
                {
                    visitedItems.Add(item);
                }
            }

            return default;
        }
    }
}
