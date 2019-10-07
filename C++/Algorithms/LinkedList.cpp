#include <iostream>

using namespace std;

#define ARRAY_SIZE(x) (sizeof((x)) / sizeof((x)[0]))

template <typename T>
class LinkedList {
    public:
      LinkedList(T value) {
        this->value = value;
      }
      
      T value;
      LinkedList<int>* next;
      
      T printList() {
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
    LinkedList<int>* currentNode = linkedList;
    int i;
    for (i = 1; i < ARRAY_SIZE( values ); ++i) {
        currentNode->next = new LinkedList<int>( values[i] );
        currentNode = currentNode->next;
    }
    
    linkedList->printList();
    
    delete currentNode;
    delete linkedList;
    return 0;
}
