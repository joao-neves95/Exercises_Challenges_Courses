/*

By: João Neves (SHIVAYL)

*/
#include <stdio.h>
int main(void)
{
    char thisSmallChar;
    char thisBigChar;
    char i;
    for(i = 0; i <= 24; i++)
    {
        // Save the char because we will need it multiple times.
        thisSmallChar = 'a' + i;
        thisBigChar = 'A' + i;
        printf("%d: %c, %d %c \n", thisSmallChar, thisSmallChar, thisBigChar, thisBigChar);
    }
    
    return 0;
}

