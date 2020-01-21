#include <stdio.h>
#include <stdlib.h>

#include "letters.h"

#define FILE_BUFFER_SIZE 100000
#define LETTER_ARR_LEN 24

int main(int argc, char *argv[]) {
	Letter letterArr[ LETTER_ARR_LEN ];
	initLetterArr( letterArr, LETTER_ARR_LEN );
	
	char fileOutput[ FILE_BUFFER_SIZE ];
	int fileSize = readFile( "input.txt", fileOutput );
	
	if ( fileOutput == NULL )
	{
	    printf( "Erro na leitura do ficheiro. \n\n");
		system("pause");
		return -1;
	}
	
	calcLetterFrequency( fileOutput, fileSize, letterArr );
	printLetterFrequency( letterArr, LETTER_ARR_LEN );
	
	system("pause");
    return 0;
}

