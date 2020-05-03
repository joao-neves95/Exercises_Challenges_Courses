#include "./singleLinkedList.hpp"

int main(int argc, char const *argv[])
{
    SingleLinkedList<int>* newList = new SingleLinkedList<int>();

    newList->add(1);
    newList->add(2);
    newList->add(3);
    newList->add(4);

    cout << "The count is: " << newList->count() << endl;

    cout << "Print all values: " << endl;
    newList->printAllValues();

    cout << "Reverse all values: " << endl;
    newList->reverseSequential();
    newList->printAllValues();
    
    cout << "Add a value to the biggining: " << endl;
    newList->addToStart(-1);
    newList->printAllValues();   

    // Just to be sure.
    newList->dispose();
    return 0;
};
