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
  print = word.substring(0, i);
  console.log(print);
  if (i < 0+1) {
    for (var j = 2; j < word.length+1; j++) {
      rePrint = word.substring(0, j);
      console.log(rePrint);
    }
  }
}
