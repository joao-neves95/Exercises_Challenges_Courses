
/*

Find the odd int frequency
--------------------------

Given an array, find the int that appears an odd number of times.

There will always be only one integer that appears an odd number of times.

By: shivayl (João Neves)
[NOTE: ADD THE "-std=c++11" COMMAND TO THE COMPILER]

*/

#include <iostream>
#include <cstdio>
#include <vector>
#include <map>
using namespace std;

int findOdd(const std::vector<int>& numbers){
  std::map<int, int> freq;
  
  std::map<int, int>::iterator currentInt;
  int i;
  for (i = 0; i < numbers.size(); ++i) {
    currentInt = freq.find(numbers[i]);
    
    if (currentInt != freq.end()) {
      currentInt->second = ++currentInt->second;
    
    } else {
      freq.emplace(numbers[i], 1);
    }
  }
  
  int oddResponse;
  for (currentInt = freq.begin(); currentInt != freq.end(); ++currentInt) {
    if (currentInt->second % 2 != 0) {
      oddResponse = currentInt->first;
      break;
    }
  }
  
  return oddResponse;  
}

int main() {
	std::vector<int> numbers = { 20,1,-1,2,-2,3,3,5,5,1,2,4,20,4,-1,-2,5 };
	std::cout << "The number that has an odd frequency is: " << findOdd(numbers) << endl;
	
	getchar();
	return 0;
}

