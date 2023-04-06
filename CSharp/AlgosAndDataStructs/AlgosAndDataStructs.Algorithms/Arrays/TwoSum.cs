
namespace AlgosAndDataStructs.Algorithms.Arrays
{
    public static class TwoSum
    {
        /**
         * Notes:
         * -----
         * --
         * - Input: nums = [2,7,11,15], target = 9
         * - Output: [0,1]
         * --
         * - Input: nums = [3,2,4], target = 6
         * - Output: [1,2]
         * --
         * - Input: nums = [3,3], target = 6
         * - Output: [0,1]
         * --
         * - Constraints:
         *       - Theres exactly one solution.
         *       - Return the index of the numbers that add up to the target.
         *       - Cannot use the same elem twice.
         *       - Solution in any order.
         */
        /// <summary>
        /// Time: O(n^2)
        /// Space: O(1)
        ///
        /// </summary>
        public static IEnumerable<int> BruteForce(IEnumerable<int> nums, int target)
        {
            if (!IsInputValid(nums))
            {
                yield break;
            }

            for (int i = 0; i < nums.Count(); ++i)
            {
                for (int j = 0; j < nums.Count(); ++j)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (nums.ElementAt(i) + nums.ElementAt(j) == target)
                    {
                        yield return i;
                        yield return j;
                        yield break;
                    }
                }
            }
        }

        /**
         * - We need to make it faster than O(n^2).
         * - Faster means that we can only pass the input array one time.
         * - So, we have to keep track of the items already visited.
         * - This will increase our memory. RAM is more scalable than time ;).
         * - But we cannot loop that new visit elem list again, because that would still be O(n).
         * - We have to visit it 1 time (constant time) to get the answer.
         * - Therefore, we need to:
         *     - Find the answer for the current number.
         *     - Check if we have the answer in the answer list.
         *         - IF NOT: Save that answer in a map (we need a map to return the index of the answer).
         *         - [get back to first]
         * - Calculate the answer:
         *     - Let target = t
         *     - Let current item = c
         *     - Let answer = x
         *         - t = c + x
         *         - x = ?
         *         - x = t - c
         */
        /// <summary>
        /// Time: O(n)
        /// Space: O(n)
        ///
        /// </summary>
        public static IEnumerable<int> Optimized(IEnumerable<int> nums, int target)
        {
            if (!IsInputValid(nums))
            {
                yield break;
            }

            var visitedElemMap = new Dictionary<int, int>();

            for (int i = 0; i < nums.Count(); ++i)
            {
                var answer = CalculateAnswer(target, nums.ElementAt(i));

                if (visitedElemMap.TryGetValue(answer, out int index))
                {
                    yield return i;
                    yield return index;
                    yield break;
                }
                else
                {
                    if (!visitedElemMap.ContainsKey(nums.ElementAt(i)))
                    {
                        visitedElemMap.Add(nums.ElementAt(i), i);
                    }
                }
            }
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        private static int CalculateAnswer(int target, int currentNum)
        {
            return target - currentNum;
        }

        /// <summary>
        /// O(1)
        ///
        /// </summary>
        private static bool IsInputValid(IEnumerable<int> nums)
        {
            return nums?.Count() >= 2;
        }
    }
}
