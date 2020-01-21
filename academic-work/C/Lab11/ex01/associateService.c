#include <stdio.h>

#include "associateService.h"
#include "utils.h"

int readFileFillAssociates( const char *fileName, Associate *associateArr ) {
    FILE *filePtr;
    filePtr = fopen( fileName, "r" );

    if (filePtr == NULL) {
        associateArr = NULL;
        return -1;
    }

    int i;
    for ( i = 0; !feof( filePtr ); ++i ) {
        fscanf(
            filePtr,
            "%s %s %d %d %d %d",
            associateArr[i].firstName, associateArr[i].lastName,
            &associateArr[i].id,
            &associateArr[i].birthDate.day, &associateArr[i].birthDate.month, &associateArr[i].birthDate.year
        );
    }
    
    // This is because the for loop increments before testing the condition.
    return --i;
}

int saveNewFileAssociates( const char *fileName, Associate *associateArr, const size_t arrLength ) {
    FILE *filePtr;
    filePtr = fopen( fileName, "w" );

    if (filePtr == NULL) {
        return -1;
    }

    int i;
    for ( i = 0; i < arrLength; ++i ) {
        fprintf(
            filePtr,
            "%s %s %d %d %d %d\n",
            &associateArr[i].firstName, &associateArr[i].lastName,
            associateArr[i].id,
            associateArr[i].birthDate.day, associateArr[i].birthDate.month, associateArr[i].birthDate.year
        );
    }
    
    
    fclose( filePtr );
    return 0;
}

/*
  Binary search.
*/
Associate* getAssociateById( Associate *associateArr, const size_t arrLength, const int idToFind ) {
    int left = 0;
    int right = arrLength - 1;
    int middle;
    Associate *currentAssociate;
    
    while( left <= right ) {
        middle = (left + right) / 2;
        currentAssociate = &associateArr[middle];
        
        if ( currentAssociate->id == idToFind ) {
            return currentAssociate;
        
        } else if ( idToFind < currentAssociate->id ) {
            right = middle - 1;
        
        } else if ( idToFind > currentAssociate->id ) {
            left = middle + 1;
        }
    }
    
    return NULL;
}

/**
Indique no ecrã quantos associados existem.
Qual o apelido mais comprido.
Qual o associado mais velho.
*/
AssociatesStatsModel getAssociatesStats( const Associate *associateArr, const size_t arrLength ) {
    AssociatesStatsModel stats;
    stats.count = arrLength;

    if (arrLength == 0) {
        return stats;
    }

    stats.oldest = &associateArr[0];
    stats.withLongestLastName = &associateArr[0];
    
    if (arrLength == 1) {
        return stats;
    }
    
    int maxDaysFromBirth = getTotalDaysFromBirth( associateArr[0].birthDate );
    int maxNameLength = strLen( &associateArr[0].lastName );
    int tempInt;
    
    int i;
    for (i = 1; i < arrLength; ++i) {
        tempInt = getTotalDaysFromBirth( associateArr[i].birthDate );
        if ( tempInt > maxDaysFromBirth ) {
            stats.oldest = &associateArr[i];
            maxDaysFromBirth = tempInt;
        }
        
        tempInt = strLen( &associateArr[i].lastName );
        if (tempInt > maxNameLength ) {
            stats.withLongestLastName = &associateArr[i];
            maxNameLength = tempInt;
        }
    }
    
    return stats;
}

void sortAssociatesArrByBirth( Associate *associateArr, const size_t arrSize ) {
    int i;
    int j;
    int smallestIdx;
    Associate temp;
    
    for (i = 0; i < arrSize; ++i) {
        smallestIdx = i;
        
        for (j = i + 1; j < arrSize; ++j) {
            if ( associateArr[j].birthDate.year < associateArr[smallestIdx].birthDate.year ) {
                smallestIdx = j;
            
            } else if (associateArr[j].birthDate.year == associateArr[smallestIdx].birthDate.year && 
                       associateArr[j].birthDate.month < associateArr[smallestIdx].birthDate.month
            ) {
                smallestIdx = j;
            
            } else if (associateArr[j].birthDate.year == associateArr[smallestIdx].birthDate.year && 
                       associateArr[j].birthDate.month == associateArr[smallestIdx].birthDate.month &&
                       associateArr[j].birthDate.day < associateArr[smallestIdx].birthDate.day
            ) {
                smallestIdx = j;
            }
        }
        
        if (smallestIdx != i) {
            temp = associateArr[i];
            associateArr[i] = associateArr[smallestIdx];
            associateArr[smallestIdx] = temp;            
        }
    }
}

void sortAssociates( AssociateList *associateList ) {
    Associate *associateI;
    Associate *associateJ;
    Associate *currentSmallest;
    int smallestDaysFromBirth;
    
    Associate *tempIPrevious;
    Associate *tempINext;
    
    for ( associateI = associateList->first; associateI != NULL; associateI = associateI->next) {
        currentSmallest = associateI;
        smallestDaysFromBirth = getTotalDaysFromBirth( associateI->birthDate );
        
        for ( associateJ = associateI->next; associateJ != NULL; associateJ = associateJ->next ) {
            if ( getTotalDaysFromBirth( associateJ->birthDate ) < smallestDaysFromBirth ) {
                currentSmallest = associateJ;
                smallestDaysFromBirth = getTotalDaysFromBirth( associateJ->birthDate );
            }
        }
        
        if (currentSmallest != associateI) {
            // 1 - 2 - [6] - 4 - 5 - [3] - 7
            //          i         smallest
            // 1 - 2 - [3] - 4 - 5 - [6] - 7
            //      smallest          i
            // We have to rebuild the chain.

            tempIPrevious = associateI->previous;
            tempINext = associateI->next;
            
            associateI->previous = currentSmallest->previous;
            associateI->next = currentSmallest->next;
            
            currentSmallest->previous = tempIPrevious;
            currentSmallest->next = tempINext;
        }
    }
}

