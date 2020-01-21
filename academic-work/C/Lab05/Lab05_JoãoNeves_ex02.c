/*

By: João Neves (SHIVAYL)

Faça um programa que peça dois números inteiros n1 e n2,
garantindo que n1 < n2, recorrendo a um ciclo do-while.
O programa deverá imprimir todos os inteiros entre n1 e 
n2 assim como a soma de todos os números entre n1 e n2, 
inclusive, recorrendo a um ciclo for.

Exemplo de funcionamento:
Insira um inteiro: 7
Insira outro inteiro maior que o primeiro: 3
Não respeitou o requisito.
Insira um inteiro: 7
Insira outro inteiro maior que o primeiro: 11
Os inteiros entre 7 e 11 são: 7 8 9 10 11
A soma dos inteiros entre 7 e 11 é 45

*/
#include <stdio.h>
#include <locale.h>

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    int inputNum1;
    int inputNum2;
    
    do {
        printf("Give me a number: ");
        scanf("%d", &inputNum1);
        
        printf("Give me another number bigger than the first one: ");
        scanf("%d", &inputNum2);
        
        if (inputNum1 > inputNum2) {
            printf("No! The second number must be bigger than the first one!\n\n");

        } else {
            printf("Thanks!\n\n");
        }
        
    } while (inputNum1 > inputNum2);
    
    printf("The integer numbers from %d to %d are: ", inputNum1, inputNum2);
    
    int sum = 0;
    int i;
    for(i = inputNum1; i <= inputNum2; ++i) {
        sum += i;
        printf("%d ", i);
    }
    
    printf("\nThe sum of all the integer numbers from %d to %d is: %d\n", inputNum1, inputNum2, sum);

    system("pause");
	return 0;
}


