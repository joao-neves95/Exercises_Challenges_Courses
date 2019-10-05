/*

[ 05/10/2019 ]

Two Sum
--------

Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

Example:

Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].

*/
#include <iostream>
#include <ostream>
#include <vector>
using namespace std;

class Solution {
public:
    // Here is the solution.
    vector<int> twoSum(vector<int>& nums, int target) {
        int i;
        int currIndexElement = 0;

        for (i = 0; i < nums.size(); ++i) {

            if ( i != currIndexElement && nums[i] + nums[currIndexElement] == target){
                return std::vector<int> { currIndexElement, i };
            }

            // Go back.
            // No need to check if there are no more elements on the array,
            // because there are always one solution.
            if (i == nums.size() - 1){
                i = 0;
                ++currIndexElement;
            }

        }

        return std::vector<int> { -1 };
    }
};

/*

Solution Results:
-----------------

Runtime: 320 ms, faster than 7.74% of C++ online submissions for Two Sum.
Memory Usage: 9.2 MB, less than 93.19% of C++ online submissions for Two Sum.

https://leetcode.com/submissions/detail/267063330/

*/

string integerVectorToString(vector<int> list, int length = -1) {
    if (length == -1) {
        length = list.size();
    }

    if (length == 0) {
        return "[]";
    }

    string result;
    for(int index = 0; index < length; index++) {
        int number = list[index];
        result += to_string(number) + ", ";
    }
    return "[" + result.substr(0, result.length() - 2) + "]";
}

int main() {
    vector<int> nums = vector<int> { 2,7,11,15 };
    int target = 9;

    vector<int> ret = Solution().twoSum(nums, target);

    cout << integerVectorToString(ret) << endl;
    return 0;
}
