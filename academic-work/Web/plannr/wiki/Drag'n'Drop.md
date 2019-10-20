# HTML/CSS Use #

* In order to make an item (card) draggable, simply add the CSS class "draggable" to the outer container (<article>).
* Then, to make a <section> container tag (the categories) droppable, simply add the CSS class "droppable".

## Examples (HTML/CSS) ##


Example 1:
```
#!html

<article class="draggable">

</article>

--------------------

<section class="dropabble">

</section>

```

Example 2:
```
#!html
<section class="dropabble">

  <article class="draggable">

  </article>

  <article class="draggable">

  </article>

</section>

```

# Methods #
When dynamically adding elements to the DOM, you will need to update the DragAndDrop functionality (after all the logic and HTML injections in an event handler, for example) with these two methods:

### updateDroppableItems() ###
This is the first one that you'll need to add. It updates all the DROPPABLE DOM nodes.

### updateDraggableItems() ###
This is the second one that you'll need to add. It updates all the DRAGGABLE DOM nodes.

## Examples (JavaScript) ##

Example 1:
```
#!js
updateDroppableItems()
updateDraggableItems()

```

Example 2:
```
#!js
document.getElementById('addcategory').addEventListener('click', () => {
  document.getElementById('categories').innerHTML += categoryHtml()
  updateDroppableItems()
  updateDraggableItems()
})

```
---
# Known Bugs #

 - ***SOLVED*** ~~When the user drags a link or image on the card, only them where dragged~~;
 - ***SOLVED*** ~~When dropping a card inside another card, the dragged card where inserted inside the second one (merging them together)~~.
 - ***SOLVED*** ~~The Drag'n'Drop does not move id's~~.
 - **SOLVED** ~~In a drop, the card is inserted only after that target (need to check client position and cards position)~~;
 - ***SOLVED*** ~~When the user drops a card on the same DROPPABLE location, the card gets duplicated~~;
 - ***SOLVED*** ~~When the user cancels a drag (drops in a non DROPPABLE location) the card gets deleted~~;
 - ***SOLVED*** ~~When the user is hovering (dragover) on the top of the target card, the temp card gets injected and deleted multiple times~~;
 - ***SOLVED*** ~~When the user drops the card on a DROPPABLE container (category) with cards in it, it ignores that and drop's as if it had no cards inside (i.e., it drops the card at the beginning of the container)~~.

---