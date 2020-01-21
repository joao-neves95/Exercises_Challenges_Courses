/*
Crie um programa que, utilizando um switch:
• solicita a introdução do número de um mês.
• apresenta o nome desse mês por extenso.
• Caso o número não corresponda a um mês válido, deverá mostrar a mensagem “Mês Inválido”.
Exemplo de interação com o utilizador:
Insira um numero do mes: 2
O mes 2 corresponde a fevereiro
*/
#include <stdio.h>
#include <locale.h>

int main() {
    setlocale(LC_ALL, "");

    printf("Insira um número do mês: ");
    short monthInput;

    scanf("%hd", &monthInput);
    printf("O mês %hd corresponde a ", monthInput);

    switch(monthInput) {
        case 1:
            printf("janeiro");
            break;
        case 2:
            printf("fevereiro");
            break;
        case 3:
            printf("março");
            break;
        case 4:
            printf("abril");
            break;
        case 5:
            printf("maio");
            break;
        case 6:
            printf("junho");
            break;
        case 7:
            printf("julho");
            break;
        case 8:
            printf("agosto");
            break;
        case 9:
            printf("setembro");
            break;
        case 10:
            printf("outubro");
            break;
        case 11:
            printf("novembro");
            break;
        case 12:
            printf("dezembro");
            break;
        default:
            printf("UM MÊS INVÁLIDO!.");
            return 1;
    }

    system("pause");
    return 0;
}

