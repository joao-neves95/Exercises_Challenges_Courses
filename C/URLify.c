/*

By: João Neves (SHIVAYL)

Write a method to replace all spaces in a string with '%20'.
You may assume that you are given the "true" length of the string.
(Note: If implementing in Java, please use a character array so that you can perform
this operation in place.) 

EXAMPLE Input: "Mr 3ohn Smith" 13 

Output: "Mr%203ohn%20Smith"

*/
#include <stdio.h>
#include <stdlib.h>
#include <stddef.h>
#include <string.h>
#include <locale.h>

#define REPLACER "%20"
#define REPLACER_SIZE 3

char* urlifyStr(char *str, const size_t length) {
    char *res = (char *)malloc(2);
    // Sizeof char is 1. Start with 2.
    size_t resSize = 2;
    size_t resLen = 0;
    
    int i;
    int j;
    for (i = 0; i < length; ++i) {
        if (resLen == resSize || resLen >= resSize - REPLACER_SIZE) {
            res = (char *)realloc(res, resSize * 2 + REPLACER_SIZE);
            resSize = resSize * 2 + REPLACER_SIZE;
        }
        
        if (str[i] == ' ') {
            // res = (char *)realloc(res, resCurrLen + REPLACER_SIZE);
            // This for is to not have any hardcoded values.
            for (j = 0; j < REPLACER_SIZE; ++j) {
                res[resLen + j] = REPLACER[j];    
            }

            resLen += REPLACER_SIZE;
        
        } else {
            // res = (char *)realloc(res, resCurrLen + 1);
            res[resLen] = str[i];
            ++resLen;
        }
    }
    
    str = res;
    free(res);
    res = NULL;
    return str;
}

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "utf8");
    
    char *strInput = "Mr 3ohn Smith";
    size_t inputLen = 13;
    
    printf("The URLified string \"%s\" of size %d is: ", strInput, inputLen);
    fflush(stdin);
    printf("%s\n\n", urlifyStr(strInput, inputLen));
    
    system("pause");
	return 0;
}

