#include "../includes/exercise4.hpp"

#include <iostream>
using namespace std;

int exercise4() {
    
    // Because we have a do..while, there is no problem on to instantiate the variable with a value,
    // since the loop only evaluates on the end of each iteration.
    int input;

    do {
      cout << "Give me a positive number: " << endl;
      cin >> input;
      
    } while (input <= 0);
    
    cout << "Thank you!" << endl;
    
    return input;
}

