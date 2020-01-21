#include "./includes/exerciseSelector.hpp"

#include <iostream>
using namespace std;

#include "./includes/exercise1.hpp"
#include "./includes/exercise2.hpp"
#include "./includes/exercise3.hpp"
#include "./includes/exercise4.hpp"
#include "./includes/exercise5.hpp"

// It's better to pass a reference here, instead of using pointers.
/**
 * "out" should be: 0 to end the program, 1 to continue and 
 *   show the menu again.
 */
void exerciseSelector(int &out) {
    cout << endl;
    cout << "Select the exercise: " << endl;
    cout << "0 - EXIT" << endl;
    cout << "1 - \"Acerte na Passowrd - Maior ou Superior\"" << endl;
    cout << "2 - \"Descrição de um Número\"" << endl;
    cout << "3 - \"Calculadora\"" << endl;
    cout << "4 - \"Inserir Número Positivo\"" << endl;
    cout << "5 - \"Inserir Número Positivo e mostrar a soma de todos os números de 1 a n\"" << endl;

    int exerciseNumber;
    cin >> exerciseNumber;
    cout << endl;

    switch(exerciseNumber) {
        // Only if it's 0, we call the loop
        // to terminate the program.
        case 0:
            cout << "Escolheu terminar o programa." << endl;
            out = 0;
            // Get out!
            return;
        case 1:
            cout << "Exercise 1" << endl;
            exercise1();
            break;
        case 2:
            cout << "Exercise 2" << endl;
            exercise2();
            break;
        case 3:
            cout << "Exercise 3" << endl;
            exercise3();
            break;
        case 4:
            cout << "Exercise 4" << endl;
            exercise4();
            break;
        case 5:
            cout << "Exercise 5" << endl;
            exercise5();
            break;
        default:
            cout << "Exercise " << exerciseNumber << " not found." << endl;
            break;
    }

    out = 1;
}

