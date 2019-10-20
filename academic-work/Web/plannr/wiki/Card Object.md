# Properties #

* title : string;
* desc : string;
* start : date (dd/MM/yyyy-hh:mm)
* end : date (dd/MM/yyyy-hh:mm)
* assigned : int[];//[user](Link URL) id array
* complete : boolean;
* comments : [Comment](Link URL)[];
* importance : int;

### Constructor ###
The title is mandatory to create the card, other fields must be edited after his creation;

```
#!json

var card = new Card("Just Do it!");

```


# Methods #

### .AddComment(comment : [Comment]()) ###
Adds a new comment to the card.
The id is automatically defined

For more information please visit the [Comments]() page;

```
#!js

var newComment = new Comment(0,"You forgot the thing...");
catList.categories[0].cards[0].AddComment(newComment);

```

### RemoveComment(id : int) ###
Removes the give comment id from the comment list;

```
#!js

catList.categories[0].cards[0].RemoveComment(0);

```

### Assign(userId : int) ###
Adds the user to the assigned list

```
#!js

catList.categories[0].cards[0].Assign(1);

```

### .Unsign(userId : int) ###
Removes the user from the assigned task.

```
#!js

catList.categories[0].cards[0].Unsign(1);

```