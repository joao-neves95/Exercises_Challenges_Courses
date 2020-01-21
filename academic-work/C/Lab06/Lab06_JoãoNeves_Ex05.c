/*

By: João Neves (SHIVAYL)

Defina três vetores de inteiros de dimensão 5, v_input, v_pares e v_impares.

• Inicialize v_pares e v_impares com 0.
• Preencha v_input com inteiros maiores ou iguais a 0, pedidos ao utilizador.
  Caso um dos números não o seja, pedir novamente.
• Separe os inteiros pares e impares em vetores separados.
  Para tal, deverá ter um contador de números pares e impares que vai crescendo
  à medida que encontra um número par/ímpar, e assim saber qual o índice do último
  elemento inserido.
• Imprima os vetores, um por linha.
  Apenas imprima os campos preenchidos de v_impares/v_pares.
  Deverá utilizar o contador de números pares/impares inseridos, para saber quantos
  números tem o vetor v_pares/v_impares.
• Exemplo de funcionamento:
Insira um número: 5
Insira um número: 6
Insira um número: 3
Insira um número: 8
Insira um número: 7
Números inseridos: 5 6 3 8 7
Números pares: 6 8
Números impares: 5 3 7

*/
#include <stdio.h>
#include <locale.h>

#define NUM_COUNT 5

void printIntArr(int intArr[], size_t size);

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");

    printf("Par ou Impar \n\n");
    
    int v_input[NUM_COUNT];
    // Em C, desta forma o compilador inicializa todas as ints a 0.
    // Em C++, não sería sequer necessário inicializar a 0, sería automático :)
    int v_pares[NUM_COUNT] = { 0 };
    int v_impares[NUM_COUNT] = { 0 };
    
    int iPares;
    int iImpares;
    int iInput;
    for (iInput = 0, iPares = 0, iImpares = 0; iInput < NUM_COUNT; ++iInput) {
        do {
            printf("Insira um número: ");
            scanf("%d", &v_input[iInput]);
    
            if (v_input[iInput] < 0) {
                printf("O número tem que ser maior que 0!\n");
            }
    
        } while (v_input[iInput] < 0);
        
        if (v_input[iInput] % 2 == 0) {
            v_pares[iPares] = v_input[iInput];
            ++iPares;
        
        } else {
            v_impares[iImpares] = v_input[iInput];
            ++iImpares;
        }
    }
    
    printf("\n\n");
    printf("Números inseridos: ");
    printIntArr(v_input, NUM_COUNT);
    printf("\n");
    
    printf("Números pares: ");
    printIntArr(v_pares, iPares);
    printf("\n");
    
    printf("Números impares: ");
    printIntArr(v_impares, iImpares);
    printf("\n\n");
    
    system("pause");
	return 0;
}

void printIntArr(int intArr[], size_t size) {
    int i;
    for (i = 0; i < size; ++i) {
        printf("%d ", intArr[i]);
    }
}

