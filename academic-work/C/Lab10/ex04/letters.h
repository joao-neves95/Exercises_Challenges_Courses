#include <stdlib.h>

typedef struct
{
	char letter;
	int quantity;

} Letter;

void initLetterArr( Letter *letterArr, const size_t letterArrLength );

int readFile( const char *fileName, char *fileOutput );

void calcLetterFrequency( const char *inputText, const int inputLength, Letter *letterArr );

void printLetterFrequency( const Letter *letterArr, const size_t letterArrLength );

