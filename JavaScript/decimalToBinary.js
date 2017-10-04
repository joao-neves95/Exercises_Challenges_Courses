/*

Last week I had a maths lesson at my university about numbering systems, and how to convert a decimal base number 
into other bases.

I thought to myself while I was at that class: 
"What better way of practicing javascript and learn maths, 
than doing a program that converts decimal base numbers into binary base ones?"

binaryToDecimal():
https://github.com/joao-neves95/Exercises_Challenges/blob/master/JavaScript/binaryToDecimal.js

*/

function decimalToBinary(inputNum) {
    var rest = "";
    var lastQuotient = 0;
    var newQuotient = 0;
    
    // First division. It divides the first decimal number by 2 (and it gets the first quotient):
    lastQuotient = Math.floor(inputNum / 2);
    
    // Returns the first rest, and adds it into the "rest" string variable:
    rest += inputNum - Math.floor((lastQuotient * 2));
    
    // Conditional. (If the division of the last quotient by 2 is greater than or equal to 1, continue the operation with a for loop):
    if (lastQuotient >= 1) {
        for (var i = 0; lastQuotient >= 1; i++) {
            newQuotient = Math.floor(lastQuotient / 2);
            rest += lastQuotient - Math.floor((newQuotient * 2));
            lastQuotient = newQuotient;
        }
        // (If the division of the last quotient by 2 is NOT greater than or equal to 1, reverse the "rest" string, and console.log the output):
    }
    function reverseString(string) {
        var reversedString = "";
        for (var i = string.length - 1; i >= 0; i--) {
            reversedString += string[i];
        }
        return reversedString;
    }
    return reverseString(rest);
}


// Calling the function:
decimalToBinary(269);
