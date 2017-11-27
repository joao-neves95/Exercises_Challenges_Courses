/*

Title Case a Sentence:

Return the provided string with the first letter of each word capitalized.
Make sure the rest of the word is in lower case.

For the purpose of this exercise, you should also capitalize connecting words like "the" and "of".

*/


function toTitleCase(str) {
  let lowerCaseArr = str.toLowerCase().split(' ')

  let titleCasedArray = []
  for (let i = 0; i < lowerCaseArr.length; i++) {
    // Push the upper cased first char of index i, plus the rest of the lower cased string:
    titleCasedArray.push(lowerCaseArr[i].charAt(0).toUpperCase() + lowerCaseArr[i].substr(1))
  }

  let titleCasedStr = titleCasedArray.join(' ')
  return titleCasedStr
}

toTitleCase("I'm a little Tea pot");
