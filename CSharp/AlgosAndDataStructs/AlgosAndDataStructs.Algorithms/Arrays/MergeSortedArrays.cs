
namespace AlgosAndDataStructs.Algorithms.Arrays
{
    public static class MergeSortedArrays
    {
        // Notes:
        // -----
        // - Input:  [0, 3, 4, 31], [4, 6, 30]
        // - Result: [0, 3, 4, 4, 6, 30, 31]
        // ---
        // - Input:  [4, 3, 4, 31], [0, 6, 30, 35]
        // - Result: [0, 3, 4, 4, 6, 30, 31, 35]
        // ---
        // - We have to combine the arrays sorted.
        // - Nothing tells us that the first array starts with the smallest item, so we have to always compare both.
        // - Condition that makes it possible to insert.
        // - Let's slide two pointers on each array.
        /// <summary>
        /// O(n)
        ///
        /// </summary>
        public static IEnumerable<T> BruteForce<T>(IEnumerable<T> array1, IEnumerable<T> array2)
            where T : IComparable<T>
        {
            if (array1?.Any() != true)
            {
                return array2;
            }

            if (array2?.Any() != true)
            {
                return array1;
            }

            int array1Idx = 0;
            var array1Item = array1.ElementAtOrDefault(array1Idx);
            int array2Idx = 0;
            var array2Item = array2.ElementAtOrDefault(array2Idx);

            bool IsArray1End()
            {
                return array1Idx >= array1.Count();
            }

            bool IsArray2End()
            {
                return array2Idx >= array2.Count();
            }

            bool DoAddArray1()
            {
                // For e.g., ints become 0 after the end (default value), so we need to check each edge.
                return IsArray2End() || (!IsArray1End() && array1Item?.CompareTo(array2Item) < 0);
            }

            bool DoAddArray2()
            {
                return IsArray1End() || (!IsArray2End() && array2Item?.CompareTo(array1Item) <= 0);
            }

            var response = new List<T>(array1.Count() + array2.Count());

            while (!IsArray1End() || !IsArray2End())
            {
                if (DoAddArray1())
                {
                    response.Add(array1Item);
                    ++array1Idx;
                    array1Item = array1.ElementAtOrDefault(array1Idx);
                }
                else if (DoAddArray2())
                {
                    response.Add(array2Item);
                    ++array2Idx;
                    array2Item = array2.ElementAtOrDefault(array2Idx);
                }
            }

            return response;
        }
    }
}
