/**
 * @file RotateArray.cpp
 * @author João Neves (https://github.com/joao-neves95)
 * @brief
 Given an array, rotate the array to the right by k steps, where k is non-negative.
 E.g:
    Input: nums = [1,2,3,4,5,6,7], k = 3
    Output: [5,6,7,1,2,3,4]
    Explanation:
      - rotate 1 steps to the right: [7,1,2,3,4,5,6]
      - rotate 2 steps to the right: [6,7,1,2,3,4,5]
      - rotate 3 steps to the right: [5,6,7,1,2,3,4]

 (LeetCode: runtime beats 100.00 % of cpp submissions)

 * @version 1.0.0
 * @copyright Copyright (c) 2021
 * (_SHIVAYL_)
 */
#include <iostream>
#include <vector>
#include <cmath>
using namespace std;

#include "../../Utils.hpp"

class RotateArray {
public:
    void rotate(vector<int>& nums, int k) {
        // this->rotateSlowAF(nums, k);
        this->computeRotation(nums, k);
    }

private:
    void computeRotation(vector<int>& nums, int k) {
        size_t vecSize = nums.size();

        if (vecSize < 2 || k == 0) {
            return;
        }

        vector<int> result(vecSize, 0);

        for (int i = 0; i < vecSize; ++i) {
            result[this->calcNewPositon(i, k, vecSize)] = nums[i];
        }

        nums = result;
    }

    int calcNewPositon(int currentPosition, const int k, const int length) {
        int position = currentPosition + k;

        // To be honest, I've found this pattern by trial and error on a calculator.
        // This is for when k is bigger than the length.
        // Keep subtracting with the length until we are within bounds.
        while (position > length - 1) {
            position = abs(position - length);
        }

        return position;
    }

//     // Brute force. Keep rotating for k times.
//     void rotateSlowAF(vector<int>& nums, const int k) {
//         int rotationCount = 0;
//         int i;
//         int cache;

//         // The idea above is to find the final position, so we don't
//         // do O(n^2) with a write on every iteration.
//         while (rotationCount < k) {
//             for (i = nums.size() - 1; i > 0; --i) {
//                 cache = nums[i];
//                 nums[i] = nums[i - 1];
//                 nums[i - 1] = cache;
//             }

//             ++rotationCount;
//         }
//     }
};

int main()
{
    vector<int> vec = { 1,2,3,4,5,6,7 };

    Utils::logg("Rotate array: ");
    Utils::loggVector(vec);
    Utils::loggNL();

    Utils::loggNL("Expected: [5,6,7,1,2,3,4]");

    Utils::logg("Output: ");
    RotateArray rotateArray;
    rotateArray.rotate(vec , 3);
    Utils::loggVector(vec);
    Utils::loggNL();

    return 0;
}
