/*

Last week I had a math lesson at my university about numbering sistems, and how to convert a decimal base number into a binary base one.

I thought to myself while I was at that class: 
"What better way of practicing javascript and learn math, 
than doing a program that converts decimal base numbers into binary base ones?"

Probably not the prettiest code, but here it is:

*/

function decimalToBinary(inputNum) {
    var rest = "";
    var lastQuotient = 0;
    var newQuotient = 0;
    
    // First division. It divides the first decimal number by 2 (and it gets the first quotient):
    lastQuotient = getFirstQuotient();
    
    // Returns the first rest, and adds it into the "rest" string variable:
    rest += getFirstRest();
    
    // Functions:
    function getFirstQuotient() {
        return parseInt(inputNum / 2);
    }


    function getNextQuotient() {
        return parseInt(lastQuotient / 2);
    }


    function getFirstRest() {
        return inputNum - parseInt((lastQuotient * 2));
    }


    function getNextRest() {
        return lastQuotient - parseInt((newQuotient * 2));
    }


    function reverseString(string) {
        var reversedString = "";
        for (var i = string.length - 1; i >= 0; i--) {
            reversedString += string[i];
        }
        return reversedString;
    }
    // End of Funtions.
    
    // Conditional:
    if (lastQuotient >= 1) {
        for (var i = 0; lastQuotient >= 1; i++) {
            newQuotient = getNextQuotient();
            rest += getNextRest();
            lastQuotient = newQuotient;
        }
        // (If the division of the last quotient by 2 is NOT greater than or equal to 0, reverse the "rest" string, and console.log the output):
    }
    return reverseString(rest);
}


// Calling the function:
decimalToBinary(269);
