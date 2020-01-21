
#include <stdio.h>
#include <locale.h>

#define INPUT_SIZE 20

int pede_numero();

int main()
{
  setlocale(LC_ALL, "");
  
  return 0;
}

int pede_numero() {
	int inputNum[INPUT_SIZE];
	int i = 0;
	
	do {
		printf("Escreva um número impar entre 10 e 20: ");
		scanf("%d", &inputNum[i]);
		
		if (inputNum[i] < 10 || inputNum[i] > 20 || inputNum[i] % 2 == 0) {
			printf("O número tem que ser impar e entre 10 a 20! \n");

		} else {
			++i;
		}
		
	} while (i < INPUT_SIZE);
}

