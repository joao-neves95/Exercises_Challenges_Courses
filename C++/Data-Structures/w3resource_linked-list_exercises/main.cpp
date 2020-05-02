#include "./linkedList.hpp"

int main(int argc, char const *argv[])
{
    LinkedList<int>* newList = new LinkedList<int>;

    newList->add(1);
    newList->add(1);
    newList->add(1);
    newList->add(1);

    cout << "The count is: " << newList->count() << endl;

    cout << "Print all values: " << endl;
    newList->printAllValues();

    return 0;
};
