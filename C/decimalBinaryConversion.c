/*

By: João Neves (SHIVAYL)

Decimal to Binary
Binary to Decimal

*/
#include <stdio.h>
#include <locale.h>

int reverseNum(int num);

int powInt(int base, int exp) {
    if (exp == 0) {
        return 1;

    } else if (exp == 1) {
        return base;
    }
    
    int result = base;
    int i;
    for (i = 1; i < exp; ++i) {
        result *= base;
    }
    
    return result;
}

// [i= 0 to n] E (n * 2^i)
int binaryToDecimal(int num) {
    int decimal = 0;
    
    int i = 0;
    while(num != 0) {
        decimal += (num % 10) * powInt( 2, i );
        num /= 10;
        ++i;
    }
    
    return decimal;
}

/*
1 - Divide number by 2 ;
2 - Get reminder;
3 - Repeat until the result of the division is 0 or 1 and join the reminders;
4 - The result is the reverse of the joined reminders.
*/
int decimalToBinary(int num) {
    int binary = 0;
    
    while( num > 0 || num > 1 ) {
        binary *= 10;
        binary += num % 2;
        num /= 2;
    }
    
    return reverseNum(binary);
}

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");

    printf("Binary to Decimal\n\n");
    
    printf("Binary 111010, should be a 58 decimal \n");
    printf("Function Result: %d \n\n", binaryToDecimal(111010));
    
    printf("Binary 1101, should be a 13 decimal \n");
    printf("Function Result: %d \n\n", binaryToDecimal(1101));

    printf("Binary 101001, should be a 41 decimal \n");
    printf("Function Result: %d \n\n", binaryToDecimal(101001));

    //---------------------------------------------------------//
    printf("\n\n\n");
    //---------------------------------------------------------//

    printf("Decimal to Binary\n\n");
    
    printf("Decimal 29, should be a 11101 binary\n");
    printf("Function Result: %d \n\n", decimalToBinary(29));
    
    printf("Decimal 13, should be a 1101 binary\n");
    printf("Function Result: %d \n\n", decimalToBinary(13));

    printf("Decimal 41, should be a 101001 binary\n");
    printf("Function Result: %d \n\n", decimalToBinary(41));

	return 0;
}

/**
 * This function was copyed from ./reverseNumber.c
 */
int reverseNum(int num) {
    int reversedNum = 0;
    
    while(num != 0) {
        reversedNum = (reversedNum * 10) + (num % 10);
        num /= 10;
    }
    
    return reversedNum;    
}

