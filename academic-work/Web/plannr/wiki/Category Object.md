# Properties #

* id : int;
* name : string;
* cards : [Card](https://bitbucket.org/shivayl/plannr/wiki/Card%20Object)[];

## Example ##

```
#!json
{
     "id": 0,
     "name": "Front-End",
     "cards": []
}

```

# Constructor #
To create a new category in js, you need to give at least the name;

```
#!js
var category = new Category("General");

```

# Methods #

### .Add(card : [Card](https://bitbucket.org/shivayl/plannr/wiki/Card%20Object)) ###
After creating a card, you can add them to the category with this method.
It will set the id automatically, so you only need to, at least, define the title on the card.

```
#!js

   var newCard = new Card("Just do it!");
   catList.categories[0].Add(newCard);

```

### .Remove(index : int) ###
This will remove the card, that corresponds to the given index, from the category


```
#!js

   catList.categories[0].Remove(0);

```

### .Empty() ###
This will delete all the cards within this category and the card's content.


```
#!js

   catList.categories[0].Empty();

```