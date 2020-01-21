#include <stdio.h>
#include <string.h>

#include "letters.h"

int readFile( const char *fileName, char *fileOutput )
{
	int outputLength = 0;
	FILE *filePtr;
	filePtr = fopen( fileName, "r" );
	
	if (filePtr == NULL)
	{
		fileOutput = NULL;
		outputLength = -1;
	}
	else
	{
		int i;
		char currentLine[124] = "";
		
		while ( !feof( filePtr ) )
		{
			currentLine[0] = '\0';
			fscanf( filePtr, "%s", currentLine );
			
			// Isto é devido ao facto das strings serem imutáveis e em C
			// não se poder concatena-las.
			for ( i = 0; currentLine[i] != '\0'; ++i, ++outputLength ) {
				fileOutput[outputLength] = currentLine[i];
			}
		}
	}
	
	fclose( filePtr );
	return outputLength;
}

void initLetterArr( Letter *letterArr, const size_t letterArrLength )
{
    int i;
    for (i = 0; i < letterArrLength; ++i) {
        letterArr[i].letter = 'a' + i;
        letterArr[i].quantity = 0;
    }
}

void calcLetterFrequency( const char *inputText, const int inputLength, Letter *letterArr )
{
    int i;
    for (i = 0; i < inputLength; ++i)
    {
    	// Lowercase.
	    if ( inputText[i] >= 97 && inputText[i] <= 122 )
  	    {
  	    	++letterArr[ inputText[i] - 97 ].quantity;
        }
        // Uppercase.
        else if ( inputText[i] >= 65 && inputText[i] <= 90 )
        {
        	++letterArr[ (inputText[i] + 32) - 97 ].quantity;
	    }
	    // else, ignore.
    }
}

void printLetterFrequency( const Letter *letterArr, const size_t letterLength )
{
	int i;
	for (i = 0; i < letterLength; ++i)
	{
		if ( letterArr[i].quantity > 0 )
		{
		    printf( "%c: %d \n", letterArr[i].letter, letterArr[i].quantity );
		}
	}
}

