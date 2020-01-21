/*

By: João Neves (SHIVAYL)

Escreva um programa que pede um número, n,
e imprime os primeiros n carateres do alfabeto. 
Recorde-se que os carateres em C são inteiros, e que 'a' + 1 = 'b'.

Exemplo:
Insira o número de letras: 5
Letras: a b c d e

*/
#include <stdio.h>
#include <locale.h>

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    int inputNum;
    printf("Give me a number and I will print you all ther characters of the alphabet until that number: ");
    scanf("%d", &inputNum);
    
    // Just convert the negative number into a positive.
    if (inputNum < 0) {
        inputNum *= -1;
    }

    printf("The letters are: ");
    
    // Start at 0 to print the 'a'.
    int i;
    for (i = 0; i < inputNum; ++i) {
        printf("%c ", 'a' + i);
    }

    printf("\n\n");
    system("pause");
	return 0;
}

