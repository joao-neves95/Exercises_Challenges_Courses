
#include <iostream>
#include <cstdio>

using namespace std;

template <typename T>
class ArrayList
{
private:
    const size_t defaultSize = 7;

    T* elements;
    size_t elemCount;
    size_t arrSize;

    void newCopy(const size_t limit) {
        T* elemBak = new T[this->arrSize];
        size_t i;

        for (i = 0; i < limit; ++i) {
            elemBak[i] = this->elements[i];
        }

        if(this->elements) {
            delete[] this->elements;
        }

        this->elements = elemBak;
    }

    void doubleSize() {
        this->arrSize *= 2;
        this->newCopy(this->arrSize / 2);
    }

    void reduceSize() {
        this->arrSize /= 2;
        this->newCopy(this->arrSize);
    }

public:
    ArrayList(size_t initialSize = 0) {
        this->arrSize = initialSize <= this->defaultSize ? this->defaultSize : initialSize;
        this->elements = new T[initialSize];
        this->elemCount = 0;
    }

    ~ArrayList() {
        this->dispose();
    }

    void dispose() {
        delete[] this->elements;
    }

    /**
     * The current total size.
     * This is value is volatile, since it's a dynamic array.
     */
    size_t size() const {
        return this->arrSize;
    }

    /**
     * The number of elements.
    */
    size_t count() const {
        return this->elemCount;
    }

    T get(const size_t i) const {
        return this->elements[i];
    }

    void push(const T value) {
        if (this->elemCount == this->arrSize) {
            this->doubleSize();
        }

        this->elements[this->elemCount] = value;
        ++this->elemCount;
    }

    T pop() {
        return this->remove(this->elemCount - 1);
    }

    T remove(const size_t i) {
        if (this->elemCount == this->arrSize / 4) {
            this->reduceSize();
        }

        T temp = this->elements[i];

        size_t j;
        for (j = i; j < this->elemCount - 1; ++j) {
            this->elements[j] = this->elements[j + 1];
        }

        --this->elemCount;
        return temp;
    }

    // const T operator [](size_t i) const {
    //   return this->elements[i];
    // }

    // T & operator [](size_t i) {
    //   return this->elements[i];
    // }
};

void loggNL() {
    cout << endl;
};

template <typename T>
void logg(const T value) {
    cout << value;
};

template <typename T>
void loggNL(const T value) {
    logg<T>(value);
    loggNL();
};
