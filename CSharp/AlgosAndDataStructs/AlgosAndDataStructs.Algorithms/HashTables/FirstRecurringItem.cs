namespace AlgosAndDataStructs.Algorithms.HashTables
{
    /*
    * Notes:
    * 
    * [2, 1, 6, 2, 8, 7] = 2
    * [7, 8, 4, 1, 3, 8] = 8
    * 
    */
    public static class FirstRecurringItem
    {
        public static T? BruteForce<T>(IEnumerable<T> array)
            where T : IComparable<T>
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

        public static T? Optimized<T>(IEnumerable<T> array)
            where T : IComparable<T>
        {
            var visitedItems = new HashSet<T>();

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
