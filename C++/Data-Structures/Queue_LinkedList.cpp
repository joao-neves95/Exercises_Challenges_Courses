/*

By: João Neves (SHIVAYL)

*/
#include <iostream>
#include <locale>

using namespace std;

template<typename T>
class LinkedListNode {
    public:
        T value;
        LinkedListNode<T> *next;
        
        LinkedListNode(T value) {
            this->value = value;
            this->next = NULL;
        }
};

template<typename T>
class LinkedList {
    public:
        LinkedList() {
            this->head = NULL;
            this->tail = NULL;
        }
        
        LinkedListNode<T> *head;
        LinkedListNode<T> *tail;
};

template<typename T>
class QueueLinkedList {
    
    private:
        int count;
        LinkedList<T> *items;
    
    public:
        QueueLinkedList() {
            this->items = new LinkedList<T>();
            this->count = 0;
        }
        
        size_t size() {
            return this->count;
        }
        
        void enqueue(T item) {
            LinkedListNode<T> *newItem = new LinkedListNode<T>(item);
            
            if (this->count == 0) {
                this->items->head = newItem;
            
            } else {
                this->items->tail->next = newItem;
            }
            
            this->items->tail = newItem;
            ++this->count;
        }
        
        T dequeue() {
            LinkedListNode<T> *headNode = this->items->head;
            
            this->items->head = this->items->head->next;
            
            T headValue = headNode->value;
            delete headNode;
            headNode = NULL;
            return headValue;            
        }
        
        void dispose() {
            LinkedListNode<T> *currNode = this->items->head;
            LinkedListNode<T> *next;
            
            while(currNode != NULL) {
                next = currNode->next;
                delete currNode;
                currNode = next;
            }
            
            delete next;
        }
        
        void printAllItems() {
            LinkedListNode<T> *currNode = this->items->head;
            
            while(currNode != NULL) {
                cout << currNode->value << " ";
                currNode = currNode->next;
            }
            
            cout << endl;
        }
    
};

int main(int argc, char *argv[]) {
    setlocale(LC_ALL, "");

    QueueLinkedList<string> *queueOfStrings = new QueueLinkedList<string>();
    queueOfStrings->enqueue("1");
    queueOfStrings->enqueue("2");
    queueOfStrings->enqueue("3");
    queueOfStrings->enqueue("4");
    
    cout << "Print all queued elements" << endl;
    queueOfStrings->printAllItems();
    cout << "Queue size: " << queueOfStrings->size() << endl;
    
    cout << "Dequeue one element" << endl;
    queueOfStrings->dequeue();
    
    cout << "Print all queued elements" << endl;
    queueOfStrings->printAllItems();
    cout << "Queue size: " << queueOfStrings->size() << endl;
    
    queueOfStrings->dispose();


	system("pause");
    return 0;
}


