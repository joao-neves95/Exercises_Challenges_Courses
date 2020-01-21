#include <locale.h>
#include <stdio.h>
// #include <stdlib.h>

int main () {
    /*
    Adicionei o setlocale, a fim de poder imprimir caracteres especiais.

    Por alguma razão, adicionar "Portuguese" no setlocale, impossíbilita
    o input de números decimais com pontos e apenas admite scanfs com virgulas.
    Para resolver o problema, configurei o setlocale para a linguagem que o
    utilizador tem na sua máquina. Testei em duas máquinas diferentes e
    não se verifica o problema (o utilizador têm que ter a sua máquina
    configrada para português)
    */
    setlocale(LC_ALL, "");

    // Alterei a disposição da declaração de variáveis de forma lógica.
    // (situam-se no seu devido bloco lógico)

    // Alterei o tipo das variaveis de input de int para float, de forma
    // admitir calculos com numeros decimais.
    float n1;
    float n2;
    printf("Introduza um número: ");
    scanf("%f", &n1);
    printf("Introduza outro número: ");
    // Adicionei o simbolo "&" à variavel de entrada no scanf (n2),
    // de forma a enviar o seu endereço e não valor, sendo possivel
    // assim o scanf criar um ponteiro para a mesma.
    scanf("%f", &n2);

    char opcao;
    printf("Introduza a operação (carater: +, -, *, /): ");
    /*
    Alterei a formatação do scanf para de %d (ints) para %c (chars),
    adicionando um espaço antes do format de forma a retirar whitespace.
    Como exemplificado abaixo, também é possível fazer um fflush ao stdin,
    porém não considero necessário importar o stdlib completo apenas devido
    a esta função. Além do mais, assim é mais limpo, simples e certos compiladores
    não reconhecem o fflush.
    */
    // fflush(stdin);
    scanf(" %c", &opcao);

    printf("\n");

    float resultado;
    switch (opcao) {
            // Alterei as operações de soma e subtração pois estavam trocadas.
        case '+':
            resultado = n1 + n2;
            // Alterei a formatação do printf de int para float.
            printf ("Resultado da soma: %.2f \n", resultado);
            break;
        case '-':
            resultado = n1 - n2;
            printf ("Resultado da subtração: %.2f \n", resultado);
            // Adicionei um beak.
            break;
        case '*':
            // Alterei o segundo número para n2.
            resultado = n1 * n2;
            printf ("Resultado da multiplicação: %.2f \n", resultado);
            // Adicionei um beak.
            break;
        case '/':
            // Adicionei chavetas, pois todos os blocos de código, incluído
            // ifs, devem conte-las devido a proporcionarem melhor readability
            // e melhor manutenção.
            //
            // Alterei a condição de != para == (igualdade).
            if (n2 == 0) {
                printf("Nao é possivel dividir por 0!! \n");
            }
            // Adicionei chavetas de forma a conter ambas as seguintes
            // expressões e a serem executadas, nomeadamente o printf,
            // apenas a quando da execussão do bloco else.
            else {
                // Alterei a ordem das variáveis
                // (o primeiro número a dividir pelo segundo).
                resultado = n1 / n2;
                printf("Resultado da divisão: %.2f \n", resultado);
            }
            break;
        default:
            printf("Selecionou uma operação não existente.");
            // Adicionei uma pausa de sistema.
            system("pause");
            // Adicionei o return abaixo de forma a informar que o programa
            // terminou com um erro.
            return 1;
    }

    // Adicionei uma pausa de sistema de forma a ser possível ver o resultado
    // antes do processo da consola terminar.
    system("pause");
    // Adicionei o return do valor 0, de forma a indicar que o programa terminou
    // sem erros.
    return 0;
}

