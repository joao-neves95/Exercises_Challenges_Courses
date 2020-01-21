#include "../includes/exercise1.hpp"

#include <iostream>
using namespace std;

void exercise1() {
    int password = 1234;
    cout << "Gess the password (4 numbers): " << endl;

    int passwordInput;
    cin >> passwordInput;
    
    if (passwordInput == password) {
      cout << "Acertou na password!" << endl;
    
    } else if (passwordInput < password) {
      cout << "Demasiado pequeno." << endl;

    } else if (passwordInput > password) {
      cout << "Demasiado grade." << endl;
    }
}

