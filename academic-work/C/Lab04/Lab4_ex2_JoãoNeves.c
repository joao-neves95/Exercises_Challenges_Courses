#include <stdio.h>
#include <locale.h>

int main() {
    setlocale(LC_ALL, "");

    int num;
    printf("Insere um número para eu te falar dele: ");
    scanf("%d", &num);

    // se está entre 0 e 100 (inclusivé).
    if (num >= 0 && num <= 100) {
        printf("O número está entre 0 e 100 (inclusivé)\n");
    } else {
        printf("O número é menor que 0 ou maior que 100\n");
    }
    if ( !(num < 0) && !(num > 100) ) {
        printf("O número está entre 0 e 100 (inclusivé)\n");
    } else {
        printf("O número é menor que 0 ou maior que 100\n");
    }

    // se é par
    if ( num % 2 == 0 ) {
        printf("O número é par\n");
    } else {
        printf("O número é impar\n");
    }
    if ( !(num % 2 != 0) ) {
        printf("O número é par\n");
    } else {
        printf("O número é impar\n");
    }

    // se é múltiplo de 5 mas não é múltiplo de 2
    if (num % 5 == 0 && num % 2 != 0) {
        printf("O número é múltiplo de 5 mas não é múltiplo de 2\n");
    }
    if ( !(num % 5 != 0) && !(num % 2 == 0) ) {
        printf("O número é múltiplo de 5 mas não é múltiplo de 2\n");
    }

    // Se está entre -20 e -10 (inclusivé) ou entre 10 e 20 (inclusivé) e é um
    // número par
    if ( (num >= -20 && num <= -10 || num >= 10 && num <= 20) && num % 2 == 0 ) {
        printf("O número está entre -20 e -10 (inclusivé) ou entre 10 e 20 (inclusivé) e é um número par\n");
    }
    if ( ( !(num < -20) && !(num > -10) || !(num < 10) && !(num > 20) ) && !(num % 2 != 0)) {
        printf("O número está entre -20 e -10 (inclusivé) ou entre 10 e 20 (inclusivé) e é um número par\n");
    }

    // Se é um múltiplo de 7, não é negativo e tem 3 dígitos
    int dividedBy100 = num / 100;
    if (num % 7 == 0 && num >= 0 && dividedBy100 >= 1 && dividedBy100 <= 9) {
        printf("O número é um múltiplo de 7, não é negativo e tem 3 dígitos\n");
    }
    if (!(num % 7 != 0) && !(num < 0) && !(dividedBy100 < 1) && !(dividedBy100 > 9)) {
        printf("O número é um múltiplo de 7, não é negativo e tem 3 dígitos\n");
    }

    system("pause");
    return 0;
}

