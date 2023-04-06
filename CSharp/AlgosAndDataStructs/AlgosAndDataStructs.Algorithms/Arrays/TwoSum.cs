
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
         *   - Theres exactly one solution.
         *   - Return the index of the numbers that add up to the target.
         *   - Cannot use the same elem twice.
         *   - Solution in any order.
         */
        /// <summary>
        /// O(n^2)
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
