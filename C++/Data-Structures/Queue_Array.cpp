#include <iostream>

using namespace std;

template <typename T>
class Queue {
    private:
        T *items;
        unsigned int length;

    public:
        Queue(size_t maxSize) {
            this->items = new T[maxSize];
            this->length = 0;
        }

        size_t size() {
            return this->length;
        }

        void enqueue(T item) {
            this->items[this->length] = item;
            ++this->length;
        }

        T dequeue() {
            T firstItem = this->items[0];

            int i;
            for (i = 0; i < this->length; ++i) {
                this->items[i] = this->items[i + 1];
            }
            
            --this->length;
            return firstItem;
        }

        void printAllItems() {
            int i;
            for (i = 0; i < this->length; ++i) {
                cout << this->items[i] << endl;
            }
        }
};

int main() {
  Queue<string> *queueOfStrings = new Queue<string>(12);
  queueOfStrings->enqueue("1");
  queueOfStrings->enqueue("2");
  queueOfStrings->enqueue("3");
  queueOfStrings->enqueue("4");

  cout << "Print all queued elements" << endl;
  queueOfStrings->printAllItems();
  cout << "Queue size: " << queueOfStrings->size() << endl;

  cout << "Dequeue one element" << endl;
  queueOfStrings->dequeue();

  cout << "Print all queued elements" << endl;
  queueOfStrings->printAllItems();
  cout << "Queue size: " << queueOfStrings->size() << endl;
}
