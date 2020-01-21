/*
______________________________________________________

Prime Numbers:
--------------

- Must be greater than 1.
- It is only divisable by 1 and the number itself.
______________________________________________________
*/
#include <stdio.h>

#include "primeNumbers.h"

int fillPrimeNumsArr( int *primeNumsArr, const int max ) {
    int count = 0;
    int currentNum;
    int hasDivisor;
    
    int i;
    for ( currentNum = 2; currentNum <= max; ++currentNum ) {
        hasDivisor = 0;
        
        // The number can't be divisible by any number other
        //  than 1 or itself.
        for ( i = 2; i < currentNum; ++i ) {
            if ( currentNum % i == 0 ) {
                hasDivisor = 1;
                break;
            }
        }
        
        if ( !hasDivisor ) {
            primeNumsArr[count] = currentNum;
            ++count;
        }
    }
    
    return count;
}

void printPrimeNumsArr( const int *primeNumsArr, const size_t arrLength ) {
    printf( "Os números primos de 0 a 10000 são: \n" );
    
    int i;
    for (i = 0; i < arrLength; ++i) {
        printf( "%d ", primeNumsArr[i] );
    }
    
    printf( "\n\n" );
}

/*
Finds the input number on the array, printing "*" for each
iteration.

It returns its index or -1 in case its not found.
*/
int sequentialSearch( const int *primeNumsArr, const size_t arrLength, const int inputNum ) {
    printf( "Pesquisa Sequêncial: \n" );
    
    // Must be greater than 1 (>= 2).
    if (inputNum < 2) {
        return 0;
    }
    
    int i;
    for (i = 0; i < arrLength; ++i) {
        printf( "* " );
        
        if ( primeNumsArr[i] == inputNum ) {
            printf( "\n%d é primo. \n\n", inputNum );
            return i;
        }
    }
    
    printf( "\n%d não é primo. \n\n", inputNum );
    return -1;
}

/*
Finds the input number on the array using binary search
printing "*" for each iteration.

It returns its index or -1 in case its not found.
*/
int binarySearch( const int *primeNumsArr, const size_t arrLength, const int inputNum ) {
    printf( "Pesquisa Binária: \n" );
    
    // Must be greater than 1 (>= 2).
    if (inputNum < 2) {
        return 0;
    }

    int left = 0;
    int right = arrLength - 1;
    int middle;
    
    int currentNum;
    while ( left <= right ) {
        printf( "* " );
        
        middle = (left + right) / 2;
        currentNum = primeNumsArr[middle];
        
        if ( currentNum == inputNum ) {
            printf( "\n%d é primo. \n\n", inputNum );
            return middle;
        
        } else if ( inputNum < currentNum ) {
            right = middle -1;

        } else {
            left = middle + 1;
        }
    }
    
    printf( "\n%d não é primo. \n\n", inputNum );
    return -1;
}

