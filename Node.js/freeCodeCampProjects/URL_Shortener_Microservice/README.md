[freeCodeCamps's](https://www.freecodecamp.org/challenges/url-shortener-microservice) [URL Shortener Microservice (XS-URL)](https://xs-url.glitch.me/)
=========================

User Stories:

  1) I can pass a URL as a parameter and I will receive a shortened URL in the JSON response.
  
  2) If I pass an invalid URL that doesn't follow the valid http://www.example.com format, the JSON response will contain an error instead.
  
  3) When I visit that shortened URL, it will redirect me to my original link.

  
---

Example usage:
==============

[https://xs-url.glitch.me/](https://xs-url.glitch.me/)

[https://xs-url.glitch.me/shorten/https://www.freecodecamp.org/challenges/url-shortener-microservice ](https://xs-url.glitch.me/shorten/https://www.freecodecamp.org/challenges/url-shortener-microservice )


Example output:
==============

```
{
  "originalUrl": "https://github.com/joao-neves95/freeCodeCamp",
  "shortUrl": "https://xs-url.glitch.me/5f0ac507",
  "info": {
    "posterIP": "11.111.111.111",
    "timestamp": "2018-01-11 22:23:46.691"
  }
}
```

NOTE: This project uses a free database, so please, do not spam the server. Thank you.
