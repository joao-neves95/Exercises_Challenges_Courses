/*

By: João Neves (SHIVAYL)

WARNING: THIS USES ASCII ENCODING ONLY.

Notas/Factos:
  (Estes factos são no encoding ASCII. Não se aplicam a UTF-8, etc.)
  - Notas de pensamento em português, código e comentários **sempre** em Inglês.
  - ASCII table: https://www.asciitable.com/
  
  1- Cada letra maiuscula encontra-se a menos 32 caracteres da sua letra minuscula
    (e.g.: (código de 'k') 107 - 32 = 75 (código de 'K'))
    (e.g.: (código de 'K') 75 + 32 = 107 (código de 'k')).
    
  2- O index de uma letra mais (+) 97 (o codigo de 'a') dá o código da letra nesse index
    (e.g.: (index de 'b') 1 + 97 = 98 (codigo de 'b') ).
    
  3- O codigo da letra minuscula menos (-) 97 (código de 'a') dá o index dessa letra
    (e.g.: (código de 'b') 98 - 97 = 1)

*/
#include <stdio.h>
#include <locale.h>
#include <stdbool.h>

typedef struct
{
    char letra;
    int quantidade;

} TipoLetra;

void *getInputAndCharFrequency(char *inputStr, TipoLetra *v_letra);
void printFrequencyToFile(char *filePathName, char *inputStr, TipoLetra *v_letra);

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    TipoLetra v_letra[24];
    
    int i;
    for (i = 0; i < 24; ++i) {
        v_letra[i].letra = 'a' + i;
        v_letra[i].quantidade = 0;
    }
    
    char inputStr[100];
    getInputAndCharFrequency(inputStr, v_letra);
    printf("\n\n");
    printFrequencyToFile("./characterFrequency.txt", inputStr, v_letra);

    printf("\n\n");
    system("pause");
	return 0;
}

void *getInputAndCharFrequency(char *inputStr, TipoLetra *v_letra) {
    printf("Insira uma frase: ");
    
    // Lets start at -1, for the first iteration be 0.
    int i = -1;
    do {
        ++i; // Increment before everything, to check the current char.
        scanf("%c", &inputStr[i]);
        
        // Verify if the char is big or small. Ignore everything else.
        // Small char.
        if (inputStr[i] >= 97 && inputStr[i] <= 122) {
                            // Facto 3.
            ++v_letra[ inputStr[i] - 97 ].quantidade;
            
        // Big char.
        } else if (inputStr[i] >= 65 && inputStr[i] <= 90) {
                        // Facto 1.    // Facto 3.
            ++v_letra[ (inputStr[i] + 32) - 97 ].quantidade;
        }

    } while (inputStr[i] != '\n');
    
    return inputStr;
}

/*
  It prints the char frequency to the desired file and also to the console.
*/
void printFrequencyToFile(char *filePathName, char *inputStr, TipoLetra *v_letra) {
    FILE *filePointer;
    filePointer = fopen(filePathName, "w+");
    
    fprintf(filePointer, "Input: %s \n\n", inputStr);
    
    int i;
    for (i = 0; i < 24; ++i) {
        printf("%c: %d \n", v_letra[i].letra, v_letra[i].quantidade);
        fprintf(filePointer, "%c: %d \n", v_letra[i].letra, v_letra[i].quantidade );
    }
    
    fclose(filePointer);
}

