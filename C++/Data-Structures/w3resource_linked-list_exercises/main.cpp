#include "./singleLinkedList.hpp"

void logNewLine() {
    cout << "\n" << endl;
}

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
    logNewLine();

    cout << "Get last value by index: " << endl;
    cout << newList->get(newList->count() - 1);
    logNewLine();

    cout << "Reverse all values: " << endl;
    newList->reverse();
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();
    logNewLine();

    cout << "Add a value to the biggining: " << endl;
    newList->addToStart(-1);
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();
    logNewLine();

    cout << "Add a value to the end: " << endl;
    newList->add(-1);
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();
    logNewLine();

    cout << "Add a value to the middle: " << endl;
    newList->addToMiddle(69);
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();
    logNewLine();

    cout << "Remove first value: " << endl;
    newList->removeFirst();
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();
    logNewLine();

    cout << "Remove last value: " << endl;
    newList->removeLast();
    cout << "The count is: " << newList->count() << endl;
    newList->printAllValues();
    logNewLine();

    // Just to be sure.
    newList->dispose();
    return 0;
};
