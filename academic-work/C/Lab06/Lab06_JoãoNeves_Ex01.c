/*

By: João Neves (SHIVAYL)

Crie um programa que preenche um vetor v_celcius com 5 temperaturas em graus Celcius pedidas ao
utilizador usando um do while.
Preencha um segundo vetor v_fahrenheit com os valores correspondentes em
graus Fahrenheit (T(°F) = T(°C) × 1.8 + 32) recorrendo a um ciclo for.
Imprima os resultados devidamente alinhados, recorrendo a um ciclo while, em que em cada linha coloca o
valor em Celcius e o valor correspondente em Fahrenheit.
Apresente também as temperaturas média, máxima e mínima em Celcius.
Exemplo:

Insira uma temperatura em Celcius: 20
Insira uma temperatura em Celcius: 30
Insira uma temperatura em Celcius: 15
Insira uma temperatura em Celcius: -10
Insira uma temperatura em Celcius: 0
Celcius Fahrenheit
20.0C 68.0F
30.0C 86.0F
15.0C 59.0F
-10.0C 14.0F
0.0C 32.0F
Temperatura maxima: 30.0C
Temperatura minima: -10.0C
Temperatura média: 11.0C

*/
#include <stdio.h>
#include <locale.h>
#include <limits.h>

#define N 5

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    float v_celcius[N];
    float v_fahrenheit[N];
    float min_celcius = INT_MAX;
    float max_celcius = INT_MIN;
    float sum_celcius = 0;
    
    int i;
    for (i = 0; i < N; ++i) {
        printf("Insira uma temperatura em Celcius: ");
        scanf("%f", &v_celcius[i]);
        
        sum_celcius += v_celcius[i];
        
        if (v_celcius[i] < min_celcius) {
            min_celcius = v_celcius[i];
        }
        
        if (v_celcius[i] > max_celcius) {
            max_celcius = v_celcius[i];
        }
        
        // Formula: [ T(°F) = T(°C) × 1.8 + 32 ]
        v_fahrenheit[i] = v_celcius[i] * 1.8 + 32;
    }
    
    printf("\n");
    printf("Celcius Fahrenheit \n");
    
    for (i = 0; i < N; ++i) {
        printf("%5.1fC %5.1fF \n", v_celcius[i], v_fahrenheit[i]);
    }
    
    printf("\n");
    printf("Temperatura máxima: %.1fC \n", max_celcius);
    printf("Temperatura minima: %.1fC \n", min_celcius);
    printf("Temperatura média: %.1fC \n", (float)sum_celcius / N);
    printf("\n");
    
    system("pause");
	return 0;
}

