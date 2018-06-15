/*

Mutations:

Return true if the string in the first element of the array contains
  all of the letters of the string in the second element of the array.
  
For example, ["hello", "Hello"], should return true because all of the
  letters in the second string are present in the first, ignoring case.
The arguments ["hello", "hey"] should return false because the string
  "hello" does not contain a "y".
Lastly, ["Alien", "line"], should return true because all of the letters 
  n "line" are present in "Alien".

*/

const mutation = (arr) => {
  const w1 = arr[0].toUpperCase();
  const w2 = arr[1].toUpperCase();

  let found = false;
  for (let i = 0; i < w2.length; i++) {
    for (let j = 0; j < w1.length; j++) {
      if (w1[j] === w2[i]) {
        found = true;
        break;
      }
    }
    if (!found)
      return false;

    found = false;
  }

  return true;
}

console.log(mutation(["hello", "Hello"]));
console.log(mutation(["hello", "hey"]));
console.log(mutation(["Alien", "line"]));
