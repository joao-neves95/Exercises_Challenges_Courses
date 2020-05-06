#include "./singleLinkedList.hpp"

int main(int argc, char const *argv[])
{
    SingleLinkedList<int>* newList = new SingleLinkedList<int>();

    newList->add(1);
    newList->add(2);
    newList->add(3);
    newList->add(4);

    cout << "Print all values: " << endl;
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();

    cout << "Reverse all values: " << endl;
    newList->reverse();
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();

    cout << "Add a value to the biggining: " << endl;
    newList->addToStart(-1);
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();

    cout << "Add a value to the end: " << endl;
    newList->add(-1);
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();

    cout << "Add a value to the middle: " << endl;
    newList->addToMiddle(69);
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();

    // Just to be sure.
    newList->dispose();
    return 0;
};
