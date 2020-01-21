#include <stdio.h>
#include <stdlib.h>
#include <locale.h>

#include "./types/contact.h"

void readContactsFile( char *fileName, ContactList *contactList );
void printContacts(ContactList *contactList);

int main(int argc, char *argv[]) {
	setlocale( LC_ALL, "" );
	// Using a linked list we don't need to specify a fized max size.
	ContactList *contactList = ( ContactList* )malloc( sizeof( ContactList ) );
	contactList->first = NULL;
	contactList->count = 0;
	
	readContactsFile( "input.txt", contactList );
	
	if (contactList == NULL) {
		printf( "Occoreu um erro ao ler os dados do ficheiro de input." );
	}
	
	printContacts( contactList );
	
	freeContactList( contactList );
	free( contactList );
	contactList = NULL;
	
	printf( "\n" );
	system( "pause" );
	return 0;
}

void readContactsFile(char *fileName, ContactList *contactList) {
	FILE *filePtr;
	filePtr = fopen( fileName, "r" );
	
	if (filePtr == NULL) {
		contactList = NULL;
		return;
	}
	
	ContactNode *currentContact;
	ContactNode *newContact;
	
	while ( !feof( filePtr ) ) {
	    newContact = ( ContactNode* )malloc( sizeof(ContactNode) );
		
		fscanf(
		    filePtr,
			"%s %s %s %d %d %d",
			newContact->firstName, newContact->lastName, newContact->phoneNumber,
			&newContact->birthDate.day, &newContact->birthDate.month, &newContact->birthDate.year
		);

		if (contactList->count == 0) {
			contactList->first = newContact;
			currentContact = contactList->first;
			currentContact->next = NULL;

		} else {
			currentContact->next = newContact;
			// Move forward to the next node.
			currentContact = currentContact->next;
			currentContact->next = NULL;
		}
		
		++contactList->count;
	}
	
	--contactList->count;
	fclose( filePtr );
}

void printContacts(ContactList *contactList) {
	if (contactList->count == 0) {
		return;
	}
	
	ContactNode *currentContact = contactList->first;
	
	int i;
	for (i = 0; i < contactList->count; ++i) {
		printf(
		    "%s %s faz anos a %02d/%02d \n",
			currentContact->firstName, currentContact->lastName,
			currentContact->birthDate.day, currentContact->birthDate.month
	    );
	    
	    currentContact = currentContact->next;
	}
}

