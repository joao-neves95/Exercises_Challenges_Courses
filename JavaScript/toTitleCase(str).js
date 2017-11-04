/*

Title Case a Sentence:

Return the provided string with the first letter of each word capitalized.
Make sure the rest of the word is in lower case.

For the purpose of this exercise, you should also capitalize connecting words like "the" and "of".

*/


function toTitleCase(str) {
  let lowerCaseStr = str.toLowerCase()

  let splited = lowerCaseStr.split(" ")

  let titleCasedArray = []
  for (let i = 0; i < splited.length; i++) {
    // Push the upper cased first char of index i, plus the rest of the lower cased string:
    titleCasedArray.push(splited[i].charAt(0).toUpperCase() + splited[i].substr(1))
  }

  let titleCasedStr = titleCasedArray.join(" ")
  return titleCasedStr
}

toTitleCase("I'm a little Tea pot");
