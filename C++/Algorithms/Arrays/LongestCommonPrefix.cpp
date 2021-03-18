/**
 * @file LongestCommonPrefix.cpp
 * @author João Neves (https://github.com/joao-neves95)
 * @brief
 *   Write a function to find the longest common prefix string amongst an array of strings.
 *   If there is no common prefix, return an empty string "".
 *   (LeetCode - Time: 100% faster, Space: 80.07% less)
 *
 * @version 1.0.0
 * @copyright Copyright (c) 2021
 * (_SHIVAYL_)
 */
#include <string>
#include <vector>

#include "../../Utils.hpp"

class Solution {
private:

public:
    static string longestCommonPrefix(const vector<string>& strs) {
        const string EMPTY_STR = "";

        if (strs.size() == 0 || strs[0].size() == 0) {
            return EMPTY_STR;
        }

        // We find the longest string, to be safer.
        int longestIdx = 0;
        string longestStr = strs[0];
        for (int i = 1; i < strs.size(); ++i) {
            if (strs[i].size() > longestStr.size()) {
                longestStr = strs[i];
                longestIdx = i;
            }
        }

        string result = EMPTY_STR;

        // We mirror the longest str.
        for (int i = 0; i < longestStr.size(); ++i) {
            // Let's store it here even if it's not a valid char.
            result += longestStr[i];

            for (int j = 0; j < strs.size(); ++j) {
                if (j == longestIdx) {
                    continue;
                }

                if (i > (strs[j].size() - 1) || strs[j][i] != result.back()) {
                    result.pop_back();
                    return result;
                }
            }
        }

        return result;
    }
};

int main() {
    Utils::loggNL(Solution::longestCommonPrefix( { "flower","flow","flight" } ));
    Utils::loggNL(Solution::longestCommonPrefix( { "flower","flower","flower","flower" } ));

    return 0;
}
