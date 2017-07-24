var array1 = [true,  true,  true,  false,
              true,  true,  true,  true ,
              true,  false, true,  false,
              true,  false, false, true ,
              true,  true,  true,  true ,
              false, false, true,  true ];


function countSheeps(arrayOfSheep) {
  var count = 0;
  for (var i = 0; i < arrayOfSheep.length; i++) {  // For loop, to loop along the given array ("arrayOfSheep").
    if (arrayOfSheep[i] == true) {                 // During the loop, if it apears "true",
      count++;                                     // it adds 1 to the variable "count" (number of true's).
    }
  }
  return count;
  console.log("There are", count, "sheeps in total");
}
