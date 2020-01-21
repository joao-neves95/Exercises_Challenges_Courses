#include "../includes/exercise3.hpp"

#include <iostream>
using namespace std;

void exercise3() {
    cout << "Type an integer number: ";
    int num1;
    cin >> num1;
    
    cout << "Type another integer number: ";
    int num2;
    cin >> num2;
    
    cout << "Choose the calculation type: " << endl;
    cout << "1. Addition, 2. Subtraction, 3. Multiplication, 4. Division" << endl;
    int calcType;
    cin >> calcType;

    cout << "Result: ";
    
    switch(calcType) {
        case 1:
            cout << num1 + num2 << endl;
            break;
        case 2:
            cout << num1 - num2 << endl;
            break;
        case 3:
            cout << num1 * num2 << endl;
            break;
        case 4:
            if (num2 == 0) {
                cout << "You can not divide by 0.\nPlease, try again." << endl;
                return exercise3();
            }
            
            cout << num1 / num2 << endl;
            break;
	   default:
	       cout << "Unknown calculation type.\nPlease, try again." << endl;
	       return exercise3();
    }    
}

