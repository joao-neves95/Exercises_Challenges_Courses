/*


Objective - Print something like:

"
Greetings
Greeting
Greetin
Greeti
Greet
Gree
Gre
Gr
G
Gr
Gre
Gree
Greet
Greeti
Greetin
Greeting
Greetings
"


*/


var word = "Greetings";

for (var i = word.length; i > 0-1; i--) {
  console.log(word.substring(0, i));
  if (i < 0+1) {
    for (var j = 2; j < word.length+1; j++) {
      console.log(word.substring(0, j));
    }
  }
}
