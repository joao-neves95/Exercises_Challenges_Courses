/*

A set of functions that loockup for a given input property value from an array, and console.log that object.

In this example, it was created an array of music albums, and then a set of functions to lookup by year, or by artist name.

*/


var myMusic = [
  {
    "artist": "Billy Joel",
    "title": "Piano Man",
    "release_year": 1973,
    "formats": [ 
      "CS", 
      "8T", 
      "LP" ],
    "gold": true
  },
  {
    "artist": "Frank Sinatra",
    "title": "In the Wee Small Hours",
    "release_year": 1955,
    "formats": [ 
      "CS", 
      "8T", 
      "LP" ],
    "gold": true
  },
  {
    "artist": "Michael Jackson",
    "title": "Thriller",
    "release_year": 1983,
    "formats": [ 
      "CS", 
      "8T", 
      "LP" ],
    "gold": true
  }
];

function findAlbumByYear (theYear) {
  for (var i = 0; i < myMusic.length; i++) {
    if (myMusic[i].release_year === theYear)
      return myMusic[i]
  }
}

function findAlbumByArtist (theArtist) {
  for (var i = 0; i < myMusic.length; i++) {
    if (myMusic[i].artist === theArtist)
      return myMusic[i]
  }
}


// TESTING:
console.log(findAlbumByYear(1955))
console.log(findAlbumByArtist('Frank Sinatra'))
