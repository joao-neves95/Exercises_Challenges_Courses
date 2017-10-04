/* 

This is a continuation of decimalToBinary().
https://github.com/joao-neves95/Exercises_Challenges/blob/master/JavaScript/decimalToBinary.js

It converts binary base numbers into decimal base ones.

*/

function binaryToDecimal(inputNum) {
    /*
       split("") doesn't work with numbers, so to index each number from the binary number into an array,
       it's imperative to convert the binary to a string, and then .split("") each number in order it can proceed:
    */
    inputNum = String(inputNum);
    var binaryStringArray = inputNum.split("");

    // Convert each string value number, into a number value:
    var binaryNumArray = [];
    for (var i = 0; i < binaryStringArray.length; i++) {
        binaryNumArray.push(parseInt(binaryStringArray[i]));
    }

    // Reverse the array:
    var reversedBinaryNumArray = [];
    for (var j = binaryNumArray.length - 1; j >= 0; j--) {
        reversedBinaryNumArray.push(binaryNumArray[j]);
    }

    // Calculate the decimal number:
    var decimalNumber = 0;
    for (var k = 0; k < reversedBinaryNumArray.length; k++) {
        decimalNumber += reversedBinaryNumArray[k] * Math.pow(2, k);
    }
    return decimalNumber;
}



// Calling the function:
console.log(binaryToDecimal(1011));
console.log(binaryToDecimal(1010111));
