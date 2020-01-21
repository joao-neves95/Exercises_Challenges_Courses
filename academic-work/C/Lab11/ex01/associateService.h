#include <stdlib.h>

#include "types/associate.h"

int readFileFillAssociates( const char *fileName, Associate *associateArr );

int saveNewFileAssociates( const char *fileName, Associate *associateArr, const size_t arrLength );

Associate* getAssociateById( Associate *associateArr, const size_t arrLength, const int idToFind );

AssociatesStatsModel getAssociatesStats( const Associate *associateArr, const size_t arrLength );

void sortAssociatesArrByBirth( Associate *associateArr, const size_t arrSize );

void sortAssociates( AssociateList *associateList );

