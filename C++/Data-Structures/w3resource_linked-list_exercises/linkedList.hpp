#include <iostream>
using namespace std;

template <class T>
class ListNode {
    public:
        ListNode<T>() {
            this->value = nullptr;
            this->next = nullptr;
        }

        ListNode<T>(T value) {
            this->value = value;
            this->next = nullptr;
        }

        T value;
        ListNode<T>* next;
};

template <class T>
class LinkedList {
    private:
        unsigned int length;

    public:
        ListNode<T>* first;
        ListNode<T>* last;

        unsigned int count() {
            return this->length;
        }

        void add(T newValue) {
            ListNode<T>* newNode = new ListNode<T>(newValue);

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

            ListNode<T>* theNode = this->first;
            unsigned int i;
            for (i = 0; i < index && theNode-> next != nullptr; ++i) {
                theNode = theNode->next;
            }

            return theNode->value;
        }

        void printAllValues() {
            if (this->length == 0) {
                return;
            }

            ListNode<T>* currNode = this->first;

            unsigned int i;
            for (i = 0; i < this->length && currNode != nullptr; ++i) {
                cout << currNode->value << endl;
                currNode = currNode->next;
            }
        }
};
