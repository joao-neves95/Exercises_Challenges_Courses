#include <stdio.h>
#include <string.h>

int main(void)
{
    char input[] = "end supper shelf moon where cocoa set clown";

    unsigned short i;
    unsigned short j;
    char temp;
    for (i = 0, j = strlen(input) - 1; i < strlen(input) / 2; ++i, --j) {
        temp = input[i];
        input[i] = input[j];
        input[j] = temp;
    }
    
    printf("%s", input);
}

