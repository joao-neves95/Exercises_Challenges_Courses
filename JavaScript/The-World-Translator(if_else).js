/*
The World Translator:

    Write a function named helloWorld that:
        - takes 1 argument, a language code (e.g. "es", "de", "en")
        - returns "Hello, World" for the given language, for atleast 3 languages. It should default to returning English. 
    Call that function for each of the supported languages and log the result to make sure it works. 
*/


function helloWorld(lang) {
  if (lang === "fr") {
    return "Bonjour, Monde";
  } else if (lang === "pt") {
    return "Olá, Mundo";
  } else {
    return "Hello, World"
  }
}
console.log(helloWorld());
console.log(helloWorld("pt"));
console.log(helloWorld("fr"));
