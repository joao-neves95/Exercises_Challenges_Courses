#include <stdio.h>
#include <locale.h>

int main()
{
	setlocale(LC_ALL, "");
	printf("Média de notas.\n\n");
	
	printf("Quantas notas pretende inserir? ");
	int notasNum;
	scanf("%d", &notasNum);
	
	printf("Insira as notas:\n");
	
	double result = 0;
	float notaAtual;
	int i;
	for (i = 0; i < notasNum; ++i) {
		scanf("%f", &notaAtual);
		result += notaAtual;
	}
	
	result /= notasNum;

	printf("Nota final : %f valores\n", result);

	system("pause");
	return 0;
}

/*
e) Altere o programa de modo a que as notas dos testes sejam inseridas pelo utilizador.

*/

