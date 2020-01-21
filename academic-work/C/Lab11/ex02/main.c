#include <stdio.h>
#include <locale.h>

#include "primeNumbers.h"

#define PRIME_ARR_SIZE 10000

int main(int argc, char *argv[]) {
    setlocale( LC_ALL, "" );

    int primeNumsArr[PRIME_ARR_SIZE];

    int primeCount = fillPrimeNumsArr( primeNumsArr, PRIME_ARR_SIZE );
    printPrimeNumsArr( primeNumsArr, primeCount );

    int inputNum = askForNum();

    sequentialSearch( primeNumsArr, primeCount, inputNum );
    binarySearch( primeNumsArr, primeCount, inputNum );

    printf( "\n\n" );
    system( "pause" );
	return 0;
}

int askForNum() {
    int input;

    do {
        printf( "Indique um inteiro entre 1 e 10000 para verificar se é primo: " );
        scanf( "%d", &input );
        
        printf( "\n\n" );

    } while ( input < 1 || input > 10000 );
    
    return input;
}

