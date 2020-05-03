#include <iostream>
using namespace std;

template <class T>
class SingleListNode {
    public:
        SingleListNode<T>() {
            this->value = nullptr;
            this->next = nullptr;
        }

        SingleListNode<T>(T value) {
            this->value = value;
            this->next = nullptr;
        }

        T value;
        SingleListNode<T>* next;
};

template <class T>
class SingleLinkedList {
    private:
        unsigned int length;

    public:
        SingleListNode<T>* first;
        SingleListNode<T>* last;

        unsigned int count() {
            return this->length;
        }

        void add(T newValue) {
            SingleListNode<T>* newNode = new SingleListNode<T>(newValue);

            if (this->length == 0) {
                this->first = newNode;
                this->last = newNode;

            } else {
                this->last->next = newNode;
                this->last = newNode;
            }

            ++this->length;
        }

        T get(unsigned int index) {
            if (this->length == 0) {
                return;

            } else if (index >= this->length) {
                return this->last;
            }

            SingleListNode<T>* theNode = this->first;
            unsigned int i;
            for (i = 0; i < index && theNode-> next != nullptr; ++i) {
                theNode = theNode->next;
            }

            return theNode->value;
        }

        void printAllValues() {
            if (this->length == 0) {
                cout << "The list is empty" << endl;
                return;
            }

            SingleListNode<T>* currNode = this->first;

            unsigned int i;
            for (i = 0; i < this->length && currNode != nullptr; ++i) {
                cout << currNode->value << endl;
                currNode = currNode->next;
            }
        }
};
