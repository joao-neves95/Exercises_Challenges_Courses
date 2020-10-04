#include <iostream>
#include <vector>

using namespace std;

#include "../Utils.hpp"

class Solution {
public:
    vector<int> findDiagonalOrder(vector<vector<int>>& matrix) {
        vector<int> result;

        if (matrix.size() == 0) {
            return result;
        }

        int rowSize = matrix.size();
        int colSize = matrix[0].size();
        int matrixLen = rowSize * colSize;

        int iLen = 0;
        bool goUp = true;
        bool justShifted = false;

        int row = 0;
        int col = 0;

        while (iLen < matrixLen) {
            result.push_back(matrix[row][col]);

            if (!justShifted) {
                if (goUp && (row == 0 || col == colSize - 1)) {
                    if (col == colSize - 1) {
                        ++row;

                    } else {
                        ++col;
                    }

                    goUp = false;
                    justShifted = true;

                } else if (!goUp && (col == 0 || row == rowSize - 1)) {
                    if (row == rowSize - 1) {
                        ++col;

                    } else {
                        ++row;
                    }

                    goUp = true;
                    justShifted = true;
                }
            }

            if(!justShifted && goUp) {
                ++col;
                --row;

            } else if (!justShifted && !goUp) {
                --col;
                ++row;
            }

            justShifted = false;
            ++iLen;
        }

        return result;
    }
};

int main() {
    vector<vector<int>> input {{1,2,3},{4,5,6},{7,8,9}};

    Solution solution;
    vector<int> output = solution.findDiagonalOrder(input);

    Utils::loggVectorNL(output);

    system("pause");
    return 0;
}
