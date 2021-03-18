/*
Reverse String
--------------

Complete the solution so that it reverses the string value passed into it.

solution('world'); // returns 'dlrow'

_______________________________________________
Solutions not using the STL (Standard Library).

By: SHIVAYL (João Neves)

*/

#include <iostream>
#include <cstdio>
#include <string>
#include <algorithm>
using namespace std ;

class ReverseString {
	private:
		ReverseString() {}

	public:
		static string withoutSwap(string str) {
			string result = "";

		    int i;
		    for (i = str.size() - 1; i >= 0; --i) {
		      result += str[i];
		    }

		    return result;
		}

		static string withSwap(string str) {
			  char temp;
			  int begin;
			  int end;
			  for (begin = 0, end = str.size() - 1; begin < end; ++begin, --end) {
			    // std::swap(str[begin], str[end]);
			    temp = str[begin];
			    str[begin] = str[end];
			    str[end] = temp;
			  }

            return str;
		}

		static string withSwapWithoutTempVar(string s) {
			int iLeft;
        	int iRight;

        	for (iLeft = 0, iRight = s.size() - 1; iLeft < iRight; ++iLeft, --iRight) {
        	    // We add left and right, on the left side.
        	    s[iLeft] = s[iLeft] + s[iRight];
        	    // Get right by subtracting the right from the sum of both.
        	    s[iRight] = s[iLeft] - s[iRight];
        	    // Get left by subtracting the original left, now right, from
        	    // the sum of both, still on the left.
        	    s[iLeft] = s[iLeft] - s[iRight];
        	}

			return s;
		}

		static string withSTL(string str) {
			std::reverse(std::begin(str), std::end(str));
            return str;
		}
};

int main() {
	// Yes, this is a real word.
	string stringToReverse1 = "Pneumonoultramicroscopicsilicovolcanoconiosis";

	cout << "String to reverse 1 (yes, this is a real word):\n" << stringToReverse1 << endl << endl;
	cout << "With the STL:\n" << ReverseString::withSTL(stringToReverse1) << endl << endl;
	cout << "Without the STL, with swap logic:\n" << ReverseString::withSwap(stringToReverse1) << endl << endl;
	cout << "Without the STL, with swap logic, without temp var:\n" << ReverseString::withSwapWithoutTempVar(stringToReverse1) << endl << endl;
	cout << "Without the STL, without swap logic:\n" << ReverseString::withoutSwap(stringToReverse1) << endl << endl << endl;


	string stringToReverse2 = "world";

	cout << "String to reverse 2:\n" << stringToReverse2 << endl << endl;
	cout << "With the STL:\n" << ReverseString::withSTL(stringToReverse2) << endl << endl;
	cout << "Without the STL, with swap logic:\n" << ReverseString::withSwap(stringToReverse2) << endl << endl;
	cout << "Without the STL, with swap logic, without temp var:\n" << ReverseString::withSwapWithoutTempVar(stringToReverse2) << endl << endl;
	cout << "Without the STL, without swap logic:\n" << ReverseString::withoutSwap(stringToReverse2) << endl << endl;

	getchar();
	return 0;
}
