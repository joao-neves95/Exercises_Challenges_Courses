/*
The Grade Assigner (With a For Loop)

Check the results of your assignGrade function from the conditionals exercise
(https://github.com/joao-neves95/Exercises_Challenges/blob/master/JavaScript/The-Grade-Assigner(if_else).js)
by logging every value from 60 to 100: your log should show "For 88, you got a B. For 89, you got a B.
For 90, you got an A. For 91, you got an A.", etc., logging each grade point in the range.

*/


for (var i = 60; i <= 100; i++) {
  if (i >= 90 && i <= 100) {
    console.log('For ' +i+ ', you got an A');
  } else if (i >= 80 && i <= 89) {
    console.log('For ' +i+ ', you got a B');
  } else if (i>= 70 && i <= 79) {
    console.log('For ' +i+ ', you got a C');
  } else if (i >= 60 && i <= 69) {
    console.log('For ' +i+ ', you got a D');
  } else if (i >= 0 && i <= 59) {
    console.log('For ' +i+ ', you got an F');
  }
}
