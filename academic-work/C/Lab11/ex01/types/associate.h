#include "date.h"

#define MAX_NAME_SIZE 256

typedef struct Associate {
    int id;
    char firstName[MAX_NAME_SIZE];
    char lastName[MAX_NAME_SIZE];
    Date birthDate;
    
    struct Associate *previous;
    struct Associate *next;
    
} Associate;

typedef struct AssociateList {
    Associate *first;
    int count;
    int _maxCount;

} AssociateList;

typedef struct AssociatesStatsModel {
    int count;
    Associate *withLongestLastName;
    Associate *oldest;
    
} AssociatesStatsModel;

