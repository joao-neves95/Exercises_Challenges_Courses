/*
    Alghorithm Functioning:

    - Find the letter (which should be encrypted) in the alphabet;
    - Move K positions further (down the alphabet);
    - Take the new letter from here;
    - If "shifting" encountered the end of the algorithm, continue from its start.

*/
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAXLINELEN 150
#define ALPHABETSIZE 26

#define LEN(arr) ( sizeof(arr) / sizeof(arr[0]) )

char* ceasarShift(int k, char* input);
int findAlphabetIdx(char inputChar);

char alphabet[] = {
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
};

int main(void) {
    const int k = ALPHABETSIZE - 19;
    
    char allLines[6][MAXLINELEN] = { 
        "VTKMATZX FNLM UX WXLMKHRXW.",
        "MAX WXTW UNKR MAXBK HPG WXTW ZKXXGYBXEWL TKX ZHGX GHP MATM TEE FXG TKX VKXTMXW XJNTE.",
        "TGW YHKZBOX NL HNK WXUML.",
        "PAH PTGML MH EBOX YHKXOXK T GBZAM TM MAX HIXKT TGW LH RHN MHH UKNMNL.",
        "TL XTLR TL ERBGZ FXM T PHFTG TM MAX PXEE YHNK LVHKX TGW LXOXG RXTKL TZH.",
        "EXM ABF MAKHP MAX YBKLM LMHGX TKX PHGWXKL FTGR MHEW."
    };
    
    int i;
    
    for (i = 0; i < 6; ++i) {
        printf("%s ", ceasarShift(k, allLines[i]));
    }
    
    return 0;
}

char* ceasarShift(int k, char *input) {
    int idx;
    
    for (idx = 0; idx < strlen(input); ++idx) {
        int shiftIdx = findAlphabetIdx( input[idx] );
        
        if (shiftIdx == -2)
            input[idx] = ' ';
        else if (shiftIdx == -3)
            input[idx] = '.';
        else {
            shiftIdx += k;
            if (shiftIdx > ALPHABETSIZE - 1)
                shiftIdx -= ALPHABETSIZE;

            input[idx] = alphabet[shiftIdx];
        }
    }

    return input;
}

int findAlphabetIdx(char inputChar) {
	switch (inputChar) {
		case ' ':
		    return -2;
		case '.':
			return -3;
	}
	
	int i;
    for (i = 0; i < ALPHABETSIZE; ++i) {
        if(alphabet[i] == inputChar)
            return i;
    }
    
    // ERROR. Unknown char.
    return -1;
}

