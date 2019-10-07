#include <iostream>

using namespace std;

#define ARRAY_SIZE(x) (sizeof((x)) / sizeof((x)[0]))

template <typename T>
class LinkedList {
    private:
      LinkedList<int>* next;
      LinkedList<int>* last;
    
    public:
      LinkedList(T value) {
        this->value = value;
      }
      
      T value;
      
      LinkedList<int>* getNext() {
          return this->next;
      }
      
      LinkedList<int>* getLast() {
          return this->last;
      }
      
      void dispose() {
          LinkedList<int>* currentNode = this;
          LinkedList<int>* nextNode = NULL;
          
          while (currentNode != NULL) {
            nextNode = currentNode->next;
            delete currentNode;
            currentNode = nextNode;
          }
          
          delete currentNode;
          delete nextNode;
      }
      
      void add(LinkedList<int>* node) {
          if (this->next == NULL || this->last == NULL) {
              this->next = node;
          
          } else {
              this->last->next = node;
          }
          
          this->last = node;
      }
      
      void printList() {
          LinkedList<int>* currentNode = this;
          
          while (currentNode != NULL) {
            std::cout << currentNode->value << std::endl;
            currentNode = currentNode->next;
          }
      }
      
};

int main()
{
    int values[] = { 7, 3324, 65, 8975, 54 };
    
    // HEAD.
    LinkedList<int>* linkedList = new LinkedList<int>( values[0] );
    int i;
    for (i = 1; i < ARRAY_SIZE( values ); ++i) {
        linkedList->add( new LinkedList<int>( values[i] ) );
    }
    
    linkedList->printList();

    cout << "The last value is: " << linkedList->getLast()->value << endl;

    linkedList->dispose();
    delete linkedList;
    return 0;
}
