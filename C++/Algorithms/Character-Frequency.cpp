/*

[ 07/10/2019 ]

Character Frequency
-------------------

Write a function that takes a piece of text in the form of a string and returns the letter frequency count for the text.
This count excludes numbers, spaces and all punctuation marks. Upper and lower case versions of a character are 
equivalent and the result should all be in lowercase.

The function should return a list of tuples (in Python and Haskell) or arrays (in other languages) sorted by 
the most frequent letters first. The Rust implementation should return an ordered BTreeMap. Letters with the 
same frequency are ordered alphabetically. For example:

  letter_frequency('aaAabb dddDD hhcc')

will return

  [('d',5), ('a',4), ('b',2), ('c',2), ('h',2)]

*/

#include <iostream>
#include <cstddef> // std::size_t
#include <string>
#include <utility> // std::pair
#include <vector>
#include<algorithm>

std::vector<std::pair<char, std::size_t>> letter_frequency(const std::string& input) {

  std::vector<std::pair<char, std::size_t>> result;

  // Frequency Mapper
  int i;
  char thisChar;
  std::vector<std::pair<char, std::size_t>>::iterator thisPairIt;

  for (i = 0; i < input.length(); ++i) {
    thisChar = std::tolower(input[i]);

    if (!std::isalpha(thisChar)) {
      continue;
    }

    thisPairIt = std::find_if(result.begin(), result.end(), [&thisChar](std::pair<char, std::size_t>& pair) {
      return pair.first == thisChar;
    });

    if (thisPairIt == result.end()) {
      result.push_back( std::make_pair( thisChar, 1 ) );

    } else {
      ++thisPairIt->second;
    }
  }
  // end of Frequency Mapper

  // Sorter
  std::sort(result.begin(), result.end(), [](std::pair<char, std::size_t> &left, std::pair<char, std::size_t>& right) {
    if (left.second == right.second) {
      return left.first < right.first;
    }

    return left.second > right.second;
  });
  // end of Sorter

  return result;
}

int main()
{
    std::cout << "The function letter_frequency(\"aaAabb dddDD hhcc\")" << std::endl;
    std::cout << "Should return: [('d',5), ('a',4), ('b',2), ('c',2), ('h',2)]" << std::endl;
    std::cout << std::endl;
    std::cout << "Result:" << std::endl;
    std::cout << std::endl;

    std::vector<std::pair<char, std::size_t>> result = letter_frequency("aaAabb dddDD hhcc");

    std::cout << "[";

    int i;
    for (i = 0; i < result.size(); ++i) {
        std::cout << "('" << result[i].first << "'," << result[i].second << ")";

        if (i != result.size() - 1) {
            std::cout << ", ";
        }
    }

    std::cout << "]" << std::endl;

    return 0;
}
