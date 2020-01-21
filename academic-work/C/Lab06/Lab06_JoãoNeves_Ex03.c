/*

By: João Neves (SHIVAYL)

Crie um programa que recebe uma palavra introduzida pelo utilizador.
Peça igualmente para o utilizador inserir uma letra.
Deverá informar quantas vezes esse carater apareceu no texto.
Inclua sempre a instrução fflush(stdin) antes de ler um carater,
para garantir que está limpo o stdin:

fflush(stdin);
scanf("%c", &carater);

Exemplo de utilização do programa:

Insira uma palavra: abracadabra
Insira uma letra a pesquisar: a
A letra a ocorre 5 vezes

*/
#include <stdio.h>
#include <locale.h>

#define STRING_BUFFER_SIZE 100

int main( int argc, const char* argv[] )
{
    setlocale(LC_ALL, "");
    
    printf("Frequência de letras \n\n");
    
    char word[STRING_BUFFER_SIZE];
    printf("Insira uma palavra: ");
    scanf("%s", word);
    
    char letter;
    printf("Insira uma letra a pesquisar: ");
    fflush(stdin);
    scanf("%c", &letter);
    
    int frequencyCount = 0;
    
    int i;
    for (i = 0; word[i] != '\0'; ++i) {
        if (word[i] == letter) {
            ++frequencyCount;
        }
    }
    
    printf("A letra a ocorre %d vezes \n\n", frequencyCount);

    system("pause");
	return 0;
}

