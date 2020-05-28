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

        SingleLinkedList() {
            this->length = 0;
        }

        ~SingleLinkedList() {
            this->dispose();
        }

        unsigned int count() {
            return this->length;
        }

        void dispose() {
            SingleListNode<T>* current = this->first;
            SingleListNode<T>* next = current->next;
            delete current;
            current = nullptr;
            --this->length;

            while (next != nullptr) {
                current = next;
                next = current->next;
                delete current;
                current = nullptr;
                --this->length;
            }
        }

        /**
         * Add a new value to the end of the list.
         */
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

        void addToStart(T newValue) {
            if (this->length == 0) {
                this->add(newValue);
                return;
            }

            SingleListNode<T>* newNode = new SingleListNode<T>(newValue);
            newNode->next = this->first;
            this->first = newNode;
            ++this->length;
        }

        void addToMiddle(T newValue) {
            if (this->length == 0) {
                this->add(newValue);
                return;
            }

            // If the number is odd, because we are calculating in an int,
            // we lose the decimal part (auto floor).
            unsigned int middle = this->length / 2;

            SingleListNode<T>* theNode = this->first;
            unsigned int i;
            for (i = 1; i < middle; ++i) {
                theNode = theNode->next;
            }

            SingleListNode<T>* newNode = new SingleListNode<T>(newValue);
            newNode->next = theNode->next;
            theNode->next = newNode;
            ++this->length;
        }

        void removeFirst() {
            if (this->length == 0) {
                return;
            }

            SingleListNode<T>* nodeToDelete = this->first;
            this->first = nodeToDelete->next;
            delete nodeToDelete;
            --this->length;
        }

        void removeLast() {
            if (this->length == 0) {
                return;
            }

            delete this->last;
            --this->length;
            this->last = this->getNode(this->length - 1);
            this->last->next = nullptr;
        }

        /**
         * In case it does not find the value, it returns the size of the type.
         */
        T get(unsigned int index) {
            if (this->length == 0) {
                return sizeof(T);
            }

            SingleListNode<T>* theNode = this->getNode(index);
            if (theNode == nullptr) {
                return sizeof(T);
            }

            return theNode->value;
        }

        SingleListNode<T>* getNode(unsigned int index) {
            if (this->length == 0) {
                return nullptr;

            } else if (index >= this->length) {
                return this->last;
            }

            SingleListNode<T>* theNode = this->first;
            unsigned int i;
            for (i = 0; i < index; ++i) {
                theNode = theNode->next;
            }

            return theNode;
        }

        void reverse() {
            SingleListNode<T>* start = this->first;
            SingleListNode<T>* nextHead = start->next;
            start->next = nullptr; // The first is now the last node, so nothing after it.
            this->last = start;

            this->first = nextHead;
            nextHead = nextHead->next;
            this->first->next = start;

            while (nextHead != nullptr) {
                start = this->first;
                this->first = nextHead;
                nextHead = nextHead->next;
                this->first->next = start;
            }
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
