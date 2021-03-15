/**
 * @file ArrayPairSumPartition.cpp
 * @author João Neves (https://github.com/joao-neves95)
 * @brief
    Given an integer array nums of 2n integers, group these integers into n pairs
    (a1, b1), (a2, b2), ..., (an, bn) such that the sum of min(ai, bi) for all i is maximized.
    Return the maximized sum.

    Input: nums = [1,4,3,2]
    Output: 4
    Explanation: All possible pairings (ignoring the ordering of elements) are:
    1. (1, 4), (2, 3) -> min(1, 4) + min(2, 3) = 1 + 2 = 3
    2. (1, 3), (2, 4) -> min(1, 3) + min(2, 4) = 1 + 2 = 3
    3. (1, 2), (3, 4) -> min(1, 2) + min(3, 4) = 1 + 3 = 4
    So the maximum possible sum is 4.
 * @version 1.0.0
 * @copyright Copyright (c) 2021
 * (_SHIVAYL_)
 */

#include <vector>
#include <algorithm>
using namespace std;

#include "../Utils.hpp"

class Solution {
public:
    int arrayPairSum(vector<int>& nums) {
        // Using the STL sort here.
        sort(nums.begin(), nums.end());

        int sumOfMinPairs = 0;
        for (int i = 0; i < nums.size() - 1; i += 2) {
            sumOfMinPairs += nums[i];
        }

        return sumOfMinPairs;
    }

    // TOO SLOW for inputs of possibly 10^4.
    void insertionSort(vector<int>& nums) {
        for (int i = 1; i < nums.size(); ++i) {
            for (int j = i; j > 0 && nums[j - 1] > nums[j]; --j) {
                // Swap for the previous until there are no bigger nums.
                nums[j - 1] = nums[j - 1] + nums[j];
                nums[j] = nums[j - 1] - nums[j];
                nums[j - 1] = nums[j - 1] - nums[j];
            }
        }
    }
};

int main() {
    vector<int> nums = { 1,4,3,2 };
    Solution solution;

    Utils::logg("Input: ");
    Utils::loggVectorNL( nums );
    Utils::logg("Output: ");
    Utils::loggNL(solution.arrayPairSum(nums));
}
