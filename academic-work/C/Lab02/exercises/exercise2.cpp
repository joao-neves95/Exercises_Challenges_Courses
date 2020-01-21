#include "../includes/exercise2.hpp"

#include <iostream>
using namespace std;

void exercise2() {
    cout << "Type a number: ";

    int num;
    cin >> num;

    // se está entre 0 e 100 (inclusivé).
    if (num >= 0 && num <= 100) {
        cout << "O número está entre 0 e 100 (inclusivé)" << endl;
    } else {
        cout << "O número é menor que 0 ou maior que 100" << endl;
    }
    if ( !(num < 0) && !(num > 100) ) {
        cout << "O número está entre 0 e 100 (inclusivé)" << endl;
    } else {
        cout << "O número é menor que 0 ou maior que 100" << endl;
    }

    // se é par
    if ( num % 2 == 0 ) {
        cout << "O número é par" << endl;
    } else {
        cout << "O número é impar" << endl;
    }
    if ( !(num % 2 != 0) ) {
        cout << "O número é par" << endl;
    } else {
        cout << "O número é impar" << endl;
    }

    // se é múltiplo de 5 mas não é múltiplo de 2
    if (num % 5 == 0 && num % 2 != 0) {
        cout << "O número é múltiplo de 5 mas não é múltiplo de 2" << endl;
    }
    if ( !(num % 5 != 0) && !(num % 2 == 0) ) {
        cout << "O número é múltiplo de 5 mas não é múltiplo de 2" << endl;
    }

    // Se está entre -20 e -10 (inclusivé) ou entre 10 e 20 (inclusivé) e é um
    // número par
    if ( (num >= -20 && num <= -10 || num >= 10 && num <= 20) && num % 2 == 0 ) {
        cout << "O número está entre -20 e -10 (inclusivé) ou entre 10 e 20 (inclusivé) e é um número par" << endl;
    }
    if ( ( !(num < -20) && !(num > -10) || !(num < 10) && !(num > 20) ) && !(num % 2 != 0)) {
        cout << "O número está entre -20 e -10 (inclusivé) ou entre 10 e 20 (inclusivé) e é um número par" << endl;
    }

    // Se é um múltiplo de 7, não é negativo e tem 3 dígitos
    int dividedBy100 = num / 100;
    if (num % 7 == 0 && num >= 0 && dividedBy100 >= 1 && dividedBy100 <= 9) {
        cout << "O número é um múltiplo de 7, não é negativo e tem 3 dígitos" << endl;
    }
    if (!(num % 7 != 0) && !(num < 0) && !(dividedBy100 < 1) && !(dividedBy100 > 9)) {
        cout << "O número é um múltiplo de 7, não é negativo e tem 3 dígitos" << endl;
    }
}

