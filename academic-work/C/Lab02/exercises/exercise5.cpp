#include "../includes/exercise5.hpp"
#include "../includes/exercise4.hpp"

#include <iostream>
using namespace std;

void exercise5() {
    
    int input = exercise4();
    cout << "This is the sum of all the positive integer numbers from 1 to " << input << ": ";

    int result = 0;
    int i;
    for (i = 1; i <= input; ++i) {
        result += i;
    }
    
    cout << result << endl;
}

