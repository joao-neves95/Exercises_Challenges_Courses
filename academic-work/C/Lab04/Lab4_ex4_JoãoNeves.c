/*
Escreva um programa que solicite ao utilizador para inserir um carater.
Indique o código ASCII do carater inserido.
Indique também se se trata de uma letra ou um número, e se a letra é maiúscula ou minúscula.
*/

#include <stdio.h>
#include <locale.h>

int main() {
    setlocale(LC_ALL, "");

    printf("Escreva um caracter: ");
    char inputChar;
    scanf("%c", &inputChar);

    printf("O código ASCII do carater inserido é: %d.\n", inputChar);

    if (inputChar >= 65 && inputChar <= 122) {
        const char message[] = "O caracter inserido é uma letra";

        if (inputChar <= 90) {
            printf("%s maiúscula.\n", message);

        } else if (inputChar >= 97) {
            printf("%s minúscula.\n", message);
        }

    } else if (inputChar >= 48 && inputChar <= 57) {
        printf("O caracter inserido é um número.\n");
    }

    system("pause");
    return 0;
}

