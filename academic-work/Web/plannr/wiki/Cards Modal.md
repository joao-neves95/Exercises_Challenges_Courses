# Initialization #

*The initialization is being made on the main.js file.*

To initialize the modal use the function ``` $('#cards-modal').modal() ``` from materialize. Here is possible to pass some options.
After that call the function ``` updateCardsModal() ```.

---

# Open the Cards Modal #

To open the Cards Modal call the function ``` $('#cards-modal').modal('open') ```

---

# Inject Data Onto the Cards Modal #

To inject data onto the cards modal use the function ``` injectDataIntoModal(cardIndex, categoryIndex, openModal, callback) ```

## Arguments ##

* cardIndex : Integer (the index of the Card instance you want to inject into the modal)
* categoryIndex : Integer (the category index from which the card belongs)
* openModal : Boolean (whether you want, or not, to open the CardModal after injecting the data)
* callback : Callback Function (optional callback you can use after completion)

## Examples ##

### Example 1: ###
```
#!js

injectDataIntoModal(cardIndex, categoryIndex, true) 

```

### Example 2: ###
```
#!js

injectDataIntoModal(cardIndex, categoryIndex, false, () => {
  // Do something.
  $('#cards-modal').modal('open')
})

```

---

# Updates #

The Cards Modal works with cards double click event listeners. Because the cards are dynamic, everytime a new one is created you'll have to call the function ``` updateCardsModal() ``` to update all the event listeners.

---

*** The CardsModal is still in development and not yet fully functional. ***