/*

Return the length of the longest word in the provided sentence.

Your response should be a number.

*/


function findLongestWord(str) {
  let splited = str.split(' ')

  let lenghtsArray = splited.map(function (val) {
    return val.length
  })
  
  lenghtsArray.sort(function (a, b) {
    return b - a
  })
  
  // Return the first value of the array (the biggest number)
  return lenghtsArray[0]
}

findLongestWord("The quick brown fox jumped over the lazy dog");
