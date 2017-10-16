/*

This function is an algorithm that console.log's the desired Fibonacci Sequence Numbers as a string.

*/


function logFibonacciSeq (numberOfNums) {
  var fibSequence = [0, 1];
  if (numberOfNums > 2) {
  // "numberOfNums - 3": subtract -1 due to array indexing, and -2 from the already delivered sequence numbers on the array.
    for (var i = 0; i <= numberOfNums - 3; i++) {
      let previousNum = fibSequence[i];
      let lastNum = fibSequence[i + 1];
      let newNum = previousNum + lastNum;
      fibSequence.push(newNum);
    }
  }
  return fibSequence.join(", ");
}

// Calling the function:
console.log(logFibonacciSeq(2));
console.log(logFibonacciSeq(10));
