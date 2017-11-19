/*

Slasher Flick:

Return the remaining elements of an array after chopping off n elements from the head.
The head means the beginning of the array, or the zeroth index.

*/


function arraySlasher (arr, howMany) {
  let choppedArr = arr.slice(0 + howMany, arr.length)
  return choppedArr
}

console.log(slasher([1, 2, 3], 2))
console.log(slasher([1, 2, 3], 9))
console.log(slasher([1, 2, "chicken", 3, "potatoes", "cheese", 4], 5))
