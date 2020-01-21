/*

By: João Neves (SHIVAYL)

Escreva um programa que pede ao utilizador um inteiro positivo usando um ciclo do-while,
insistindo caso não seja satisfeito o requisito recorrendo a um ciclo do-while.
Recorrendo a um ciclo for, imprima no ecrã a tabuada desse numero, devidamente alinhada.

Exemplo de funcionamento:
Insira um numero: 7
7 x 1 = 7
7 x 2 = 14
7 x 3 = 21
7 x 4 = 28
7 x 5 = 35
7 x 6 = 42
7 x 7 = 49
7 x 8 = 56
7 x 9 = 63
7 x 10 = 70

*/
#include <stdio.h>
#include <locale.h>

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    int inputNum;
    
    do {
        printf("Give me a positive number: ");
        scanf("%d", &inputNum);
        
        if (inputNum < 0) {
            printf("No! I want a positive number!\n\n");
        
        } else {
            printf("Thanks!\n\n");
        }
        
    } while (inputNum < 0);

    printf("Here is the multiplication table of %d:\n", inputNum);
    
    int i;
    for (i = 1; i <= 10; ++i) {
        printf("%d * %d = %d\n", inputNum, i, inputNum * i);
    }

    system("pause");
	return 0;
}

