#include <locale.h>
#include <stdio.h>
#include <time.h>

#include "associateService.h"

#define MAX_ASSOCIATES 2000

const char *INPUT_FILE_NAME = "associados.txt";

Associate* askForAssociateById( Associate *associatesArr, size_t arrLength );

int main(int argc, char *argv[]) {
    setlocale( LC_ALL, "" );
    Date date = newDate( 31, 12, 2019 );
    
    printf( "Today is: %s \n", asctime( getCurrentDate() ) );
    printf( "This should equal 19: %d \n\n", getTotalDaysFromBirth( date ) );
    
    Associate associates[MAX_ASSOCIATES];
    int count = readFileFillAssociates( INPUT_FILE_NAME, associates );
    
    if (associates == NULL) {
        printf( "Problema ao ler o ficheiro de associados." );
        printf( "\n\n" );
        system( "pause" );
        return 1;
    }
    
    Associate *associate = askForAssociateById( associates, count );
    printf( "Nome: %s %s \n\n", associate->firstName, associate->lastName );
    
    printf("Estatísticas sobre todos os associados: \n");
    AssociatesStatsModel stats = getAssociatesStats( associates, count );
    printf( "Número de associados: %d \n", stats.count );
    printf( "O apelido mais comprido é: %s \n", stats.withLongestLastName->lastName );
    printf( "O associado mais velho é: %s %s \n", stats.oldest->firstName, stats.oldest->lastName );
    
    sortAssociatesArrByBirth( associates, count );
    saveNewFileAssociates( "sorted_associates.txt", associates, count );
    
    printf( "\n\n" );
    system( "pause" );
	return 0;
}

Associate* askForAssociateById( Associate *associatesArr, size_t arrLength ) {
    Associate *associate = NULL;
    int inputId;
    
    while ( associate == NULL ) {
        printf( "Insira o numero de associado para encontar: \n" );
        scanf( "%d", &inputId );
        associate = getAssociateById( associatesArr, arrLength, inputId );
        
        if ( associate == NULL ) {
            printf( "Associado não encontardo! \n\n" );
        }
    }
    
    return associate;
}

