/*

Minimum:

The previous chapter introduced the standard function Math.min that returns
its smallest argument. We can do that ourselves now. Write a
function min that takes two arguments and returns their minimum.

*/



function min(num1, num2) {
  if (num1 < num2)
    return num1;
  else if (num2 < num1)
    return num2;
  else
    return 'The numbers are equal.';
}
console.log(min(715, 78));
console.log(min(789, 970));
console.log(min(7, 7));
