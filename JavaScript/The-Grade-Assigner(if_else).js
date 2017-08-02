/*
The Grade Assigner

    Write a function named assignGrade that:
        - takes 1 argument, a number score.
        - returns a grade for the score, either "A", "B", "C", "D", or "F". 
    Call that function for a few different scores and log the result to make sure it works. 
*/


function assignGrade(num) {
  if (num >= 90 && num <= 100) {
    return 'A';
  } else if (num >= 80 && num <= 89) {
    return 'B';
  } else if (num >= 70 && num <= 79) {
    return 'C';
  } else if (num >= 60 && num <= 69) {
    return 'D';
  } else if (num >= 0 && num <= 59) {
    return 'F';
  } else {
    return 'Invalid Number';
  }
}
console.log(assignGrade(98));
console.log(assignGrade(52));
console.log(assignGrade(79));
console.log(assignGrade(80));
console.log(assignGrade(101));
console.log(assignGrade(-1));
