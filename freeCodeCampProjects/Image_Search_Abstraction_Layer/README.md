[freeCodeCamps's](https://www.freecodecamp.org/challenges/image-search-abstraction-layer) [Image Search Abstraction Layer (FindImages.com)](https://findimages.glitch.me/)
=========================

### User Stories:

  1) I can get the image URLs, alt text and page urls for a set of images relating to a given search string.
  
  2) I can paginate through the responses by adding a ?offset=2 parameter to the URL.
  
  3) I can get a list of the most recently submitted search strings.


  
---

### Example usage:

#### GET request to the API to get the images:

[https://findimage.glitch.me/api/search/something?offset=0](https://findimage.glitch.me/api/search/memes?offset=0)(Page One)

[https://findimage.glitch.me/api/search/something?offset=1](https://findimage.glitch.me/api/search/memes?offset=1)(Page Two)

[https://findimage.glitch.me/api/search/something?offset=2](https://findimage.glitch.me/api/search/memes?offset=2)(Page Three)


#### GET request to the API to get the last searches from the database:

[https://findimages.glitch.me/api/last](https://findimages.glitch.me/api/last)

[https://findimages.glitch.me/api/last/all](https://findimages.glitch.me/api/last/all)

Example output:
==============

```
[
  {
    "_id": "5a663fbd04bdfd005cd9481d",
    "query": "memes",
    "ip": "85.139.161.222",
    "timestamp": "2018-01-22 19:47:09.238"
  }
]
```
