/*

By: João Neves (SHIVAYL)

Escreva um programa que pede um inteiro positivo (se não o for, peça novamente).
Apresente no ecrã um padrão semelhante a metade de um triangulo com números.
Recorra a um ciclo que imprime os numeros de 1 a n. No entanto, em cada iteração, 
deverá ter um segundo ciclo que repete a impressão desse número a devida quantidade de vezes.

Exemplo de funcionamento:
Insira o número de linhas: -1
Tem que ser um número maior que 0.
Insira o número de linhas: 7
1
22
333
4444
55555
666666
7777777

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
    
    int i;
    int j;
    for (i = 1; i <= inputNum; ++i) {
        for (j = 0; j < i; ++j) {
            printf("%2d", i);
        }

        printf("\n");
    }

    system("pause");
	return 0;
}



