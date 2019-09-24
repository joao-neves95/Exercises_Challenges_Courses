API Basejump: [Timestamp Microservice Project](https://www.freecodecamp.org/challenges/timestamp-microservice)
=========================

User stories:

  1) I can pass a string as a parameter, and it will check to see whether that string contains either a unix timestamp or a natural language date (example: January 1, 2016)

  2) If it does, it returns both the Unix timestamp and the natural language form of that date.

  3) If it does not contain a date or Unix timestamp, it returns null for those properties.
  
---

Example usage:
==============
https://timestamp-microservice-shivayl.glitch.me

https://timestamp-microservice-shivayl.glitch.me/January%2010,%202018

https://timestamp-microservice-shivayl.glitch.me/1515542400

Example output:
==============

```
{
"status":	"Success."
"input":	"January 10, 2018"
"response":	{
  "unix":	1515542400
  "natural":	"January 10, 2018"
  }
}
```
