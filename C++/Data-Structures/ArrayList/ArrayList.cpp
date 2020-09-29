// TODO: Find a VS Code C++ config.

#include <cstdio>
#include <string>

using namespace std;

#include "./ArrayList.hpp"

int main(int argc, char const *argv[])
{
    ArrayList<int>* arrList = new ArrayList<int>(0);

    logg("The count is: ");
    logg(arrList->count());
    loggNL();

    size_t i;
    for (i = 0; i < 52; ++i) {
        arrList->push(i);
    }

    logg("The count is: ");
    logg(arrList->count());
    loggNL();

    for (i = 0; i < arrList->count(); ++i) {
        logg(arrList->get(i));
        loggNL();
    }

    loggNL();

    loggNL("Remove index 12");
    loggNL(arrList->remove(12));
    logg("The count is: ");
    loggNL(arrList->count());
    loggNL();

    size_t originalCount = arrList->count();
    for (i = 0; i < originalCount; ++i) {
        logg(arrList->pop());
        loggNL();
    }

    arrList->dispose();

    system("pause");
    return 0;
}
