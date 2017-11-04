/*

Return Largest Numbers in each Arrays:

Return an array consisting of the largest number from each provided sub-array.
For simplicity, the provided array will contain exactly 4 sub-arrays.

*/


function largestOfFour(arr) {
  let sortedArrays = []
  let output = []

  for (let i = 0; i < arr.length; i++) {
    // Sort from largest to smaller:
    sortedArrays.push(arr[i].sort(function (a, b) {
      return b - a
    }))
    // Push the first number (the largest) from each array:
    output.push(sortedArrays[i][0])
  }
  return output
}

largestOfFour([[4, 5, 1, 3], [13, 27, 18, 26], [32, 35, 37, 39], [1000, 1001, 857, 1]]);
