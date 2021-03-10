#include <iostream>
#include <vector>

using namespace std;

class Utils {
public:

    static void loggNL() {
        cout << endl;
    };

    template <typename T>
    static void logg(const T value) {
        cout << value;
    };

    template <typename T>
    static void loggNL(const T value) {
        Utils::logg<T>(value);
        Utils::loggNL();
    };

    template <typename T>
    static void loggVector(const vector<T> vect) {
        Utils::logg("[");

        size_t i;
        for (i = 0; i < vect.size(); ++i) {
            Utils::logg(vect[i]);

            if (i < vect.size() - 1) {
                Utils::logg(",");
            }
        }

        Utils::logg("]");
    }

    template <typename T>
    static void loggVectorNL(const vector<T>& vect) {
        Utils::loggVector(vect);
        Utils::loggNL();
    }

    template <typename T>
    static void loggVector(const vector<vector<T>> vect) {
        Utils::logg("[");

        size_t i;
        size_t j;

        size_t innerVectSize = 0;

        for (i = 0; i < vect.size(); ++i) {
            Utils::logg("[");
            innerVectSize = vect[i].size();

            for (j = 0; j < innerVectSize; ++j) {
                Utils::logg(vect[i][j]);

                if (j < innerVectSize - 1) {
                    Utils::logg(",");
                }
            }

            Utils::logg("]");
        }

        Utils::logg("]");
    }

    template <typename T>
    static void loggVectorNL(const vector<vector<T>>& vect) {
        Utils::loggVector(vect);
        Utils::loggNL();
    }

    template <typename T>
    static size_t arrSize(T* arr) {
        return sizeof(arr) / sizeof(arr[0]);
    }
};
