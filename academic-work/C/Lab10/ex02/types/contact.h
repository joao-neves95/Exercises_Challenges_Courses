#include <stdlib.h>

#include "./date.h"

#define MAX_STR_SIZE 256

typedef struct ContactNode {
	char firstName[MAX_STR_SIZE];
	char lastName[MAX_STR_SIZE];
	char phoneNumber[MAX_STR_SIZE];
	Date birthDate;

	struct ContactNode *next;

} ContactNode;

typedef struct {
	ContactNode *first;
	int count;

} ContactList;

void freeContactList( ContactList *contactList ) {
    ContactNode *currentContact = contactList->first;
    ContactNode *nextContact;
    
    while(currentContact != NULL) {
        nextContact = currentContact->next;
        free( currentContact );
        currentContact = nextContact;
    }
}

