/*

By: João Neves (SHIVAYL)

Reverse a number.
*/
#include "./reverseNumber.h"
#include <stdio.h>
#include <locale.h>

int reverseNum(int num) {
    int reversedNum = 0;
    
    while(num != 0) {
        reversedNum = (reversedNum * 10) + (num % 10);
        // It's an int so we do not need to floor it because we already
        // lose the floating part.
        num /= 10;
    }
    
    return reversedNum;    
}

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    printf("Reverse 123\n");
    printf("%d \n\n", reverseNum(123));

    printf("Reverse 684295\n");
    printf("%d \n\n", reverseNum(684295));

    printf("Reverse -852195\n");
    printf("%d \n\n", reverseNum(-684295));
    
    printf("Reverse -1585245\n");
    printf("%d \n\n", reverseNum(-1585245));
    
	return 0;
}

