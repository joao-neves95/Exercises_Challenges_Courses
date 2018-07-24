/*

In cases, when result contains exactly 0.5 as a fraction part,
value should be rounded up (i.e. by adding another 0.5).

Note that for negative values "greater" means "closer to zero".

*/

#include <stdio.h>
#include <math.h>

#define ArrayLen(arr) (sizeof(arr) / sizeof(arr)[0])

int main(void) {

    int input[][2] = {
        {1188933, 354},
        {6863494, 3139195},
        {4805980, 939},
        {16765, 704},
        {-9916666, 2702024},
        {-6703794, 2384020},
        {-2479084, -390358},
        {-3667526, -271584},
        {13955, 488},
        {13423, 954},
        {7177522, 368},
        {-8080686, -2888333},
        {-5471753, 1340533},
        {-7172692, -682445}
    };

    unsigned short i;
    for (i = 0; i < ArrayLen(input); ++i) {
        double currResult = (double)input[i][0] / (double)input[i][1];
        double intPart;
        double floatPart = modf(currResult, &intPart);

        if (currResult > 0) {
            if (floatPart >= (double)0.5)
                printf("%d ", (int)(intPart + 1));
            else
                printf("%d ", (int)(intPart));
        } else {
            if (floatPart <= (double)-0.5)
                printf("%d ", (int)(intPart - 1));
            else
                printf("%d ", (int)(intPart));
        }

        fflush(stdout);
    }

    return 0;
}

