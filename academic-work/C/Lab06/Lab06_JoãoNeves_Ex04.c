/*

By: João Neves (SHIVAYL)

Crie um programa que recebe uma frase introduzida pelo utilizador.
Peça igualmente para o utilizador inserir uma letra.
Deverá informar qual a posição em que esse carater aparece pela primeira vez.
Caso não apareça indique a posição -1.
Exemplo de utilização do programa:

Insira uma palavra: abracadabra
Insira uma letra a pesquisar: r
A letra a ocorre a primeira vez na posição 2

*/
#include <stdio.h>
#include <locale.h>

#define STRING_BUFFER_SIZE 100

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");

    printf("Posição da Primeira Ocorrência de uma Letra \n\n");
    
    char word[STRING_BUFFER_SIZE];
    printf("Insira uma palavra: ");
    scanf("%s", word);
    
    char letter;
    printf("Insira uma letra a pesquisar: ");
    fflush(stdin);
    scanf("%c", &letter);
    
    int i;
    for (i = 0; ; ++i) {
        if (word[i] == letter) {
            break;
        
        } else if (word[i + 1] == '\0') {
            i = -1;
            break;
        }
    }
    
    printf("A letra \"%c\" ocorre a primeira vez na posição %d \n\n", letter, i);

    system("pause");
	return 0;
}

