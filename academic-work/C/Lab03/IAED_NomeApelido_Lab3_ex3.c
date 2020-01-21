#include <stdio.h>

int main()
{
	// char ch = 'A';
	char ch = 'Z';
	printf("Um char : %c\n", ch);
	printf("Outro char : %c\n", ch + 32);
	// printf("O carater %c tem o codigo ASCII %d\n", 100, 100);
	printf("O carater %c tem o codigo ASCII %d\n", 90, 90);
	return 0;
}

/*
a) Compile e execute o programa. O que aparece escrito no monitor?
Aparece o output:
"Um char : A
Outro char : a
O carater d tem o codigo ASCII 100"

b) Troque o 'A' por 'Z' compile e corra o programa.

c) O que irá aparecer no monitor se alterar o especificador de formato de %c para %d. (Nota: o código
ASCII do carácter ‘Z’ é 90)
Aparece o output:
"Um char : Z
Outro char : z
O carater Z tem o codigo ASCII 90"

*/

