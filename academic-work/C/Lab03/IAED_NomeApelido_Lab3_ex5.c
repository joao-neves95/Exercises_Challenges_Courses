#include <stdio.h>

int main()
{
	int notaTeste1 = 15;
	int notaTeste2 = 15;
	int notaTeste3 = 17;
	double media;
    // media = (notaTeste1 + notaTeste2 + notaTeste3) / 3;
    media = (notaTeste1 + notaTeste2 + notaTeste3) / 3.0;
	printf("Nota final (%%f) : %f valores\n", media);
    printf("Nota final (%%3.0f) : %3.0f valores\n", media);
    printf("Nota final (%%3.2f) : %3.2f valores\n", media);
	return 0;
}

/*
a) Compile, corra o programa e examine a sua saída. Concorda com o resultado? O que terá acontecido?
O output do programa (média) é 15.000000. Este valor é errado e deve.se ao facto da média não ser
feita com um float ou decimal. Como as contas são feitas apenas como ints, o compilador retorna
o resultado arredondado ao número decimal mais próximo (floor).

b) Substitua a instrução do cálculo da média pela instrução seguinte e observe o que é escrito pelo programa.
media = (notaTeste1 + notaTeste2 + notaTeste3)/3.0;
Neste caso o resultado já é 15.66666, o valor correto sem arredondamentos.

c) Na instrução printf substitua %f por %3.0f e observe o resultado.

d) Na instrução printf substitua %f por %3.2f e observe o resultado

e) Altere o programa de modo a que as notas dos testes sejam inseridas pelo utilizador.

*/

