/*
    Bubble Sort Alghorithm

    1 - Take a pass through array, examining all pairs of adjacent elements (N-1 pairs for array of N elements).
    2 - If for any pair with indexes i and i+1 the condition a[i] <= a[i+1] does not hold, swap these two elements.
    3 - Repeat such passes through array until the new pass will do no swaps at all.
    
    
    Problem Statement

We are going to implement the described version of Bubble-Sort. To test it we will check the amount of passes and amount of swaps made before the given array becomes ordered.

Input data will contain array size in first line and array itself in the second (integer values separated with spaces).
Answer should contain two values - number of passes perfromed and total number of swaps made.

*/
#include <stdio.h>
#include <stdbool.h>

#define ARRAYSIZE(arr) (sizeof(arr) / sizeof(arr)[0])

int main(void) {
    int input[] = { 5, 3, 14, 1, 12, 2, 13, 7, 10, 17, 4, 9, 11, 6, 15, 16, 8 };
    
    int numOfPasses = 0;
    int numOfSwaps = 0;
    bool swaped = false;
    unsigned short i;
    
    do
    {
        swaped = false;
        
        for (i = 0; i < ARRAYSIZE(input); ++i)
        {
           
            if (input[i] > input[i + 1])
            {
                int tempNum;
                tempNum = input[i + 1];
                input[i + 1] = input[i];
                input[i] = tempNum;
                swaped = true;
                ++numOfSwaps;
            }

        }
        ++numOfPasses;
        
    } while(swaped);
    
    for (i = 0; i < ARRAYSIZE(input); ++i)
    {
        printf("%d ", input[i]);
    }
   
    printf("\n%d %d", numOfPasses, numOfSwaps);
    
    return 0;
}

