/**
 * @file PascalTriangle.cpp
 * @author João Neves (https://github.com/joao-neves95)
 * @brief
 *   Given an integer numRows, return the first numRows of Pascal's triangle.
 *   In Pascal's triangle, each number is the sum of the two numbers directly above it.
 * @version 1.0.0
 * @copyright Copyright (c) 2021
 * (_SHIVAYL_)
 */

#include <vector>

#include "../Utils.hpp"

class PascalTriangle
{
private:
public:
    static vector<vector<int>> generate(int rowsNum) {
        vector<vector<int>> triangle;

        if (rowsNum == 0) {
            return triangle;
        }

        vector<int> row;
        row.push_back(1);
        triangle.push_back(row);

        if (rowsNum == 1) {
            return triangle;
        }

        for (int iRow = 1; iRow < rowsNum; ++iRow) {
            vector<int> row;
            row.push_back(1);

            int sum = 0;
            int sumCount = 0;
            for (int iCol = 0; iCol < iRow; ++iCol) {
                sum += triangle[iRow - 1][iCol];
                ++sumCount;

                if (sumCount == 2) {
                    row.push_back(sum);
                    sum = 0;
                    sumCount = 0;
                    // We go one column back.
                    --iCol;
                }
            }

            row.push_back(1);
            triangle.push_back(row);
        }

        return triangle;
    }
};

int main()
{
    Utils::loggVector(PascalTriangle::generate(5));
    Utils::loggNL();
    Utils::loggVector(PascalTriangle::generate(10));

    return 0;
}
