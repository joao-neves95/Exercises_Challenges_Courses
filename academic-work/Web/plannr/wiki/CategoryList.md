# Properties #

* categories: [Category[]](https://bitbucket.org/shivayl/plannr/wiki/Card%20Object)

### Example ###


```
#!json
{
     "categories": []
}
```


# Constructor #

To create a new category list, you don't need any arguments.

### Example ###
```
#!js

var categories = new CategoryList();

```


# Methods#

### .Add(category : [Category](https://bitbucket.org/shivayl/plannr/wiki/Category%20Object)) ###
After creating a category, you pass it as an argument to the method and it will be added to the list.
It adds the id automatically, so, only the name is mandatory to create the category.

See [Category Object](https://bitbucket.org/shivayl/plannr/wiki/Category%20Object) for more information.


```
#!js

   var catList = new CategoryList();
   var newCat = new Category("Test");
   catList.Add(newCat);

```

### .Remove(id : int, callback : function(optional)) ###
Removes the given id from the categorylist, but all of it's contents will be lost.


```
#!js

catList.Remove(0);

```

### .Save(success : function(optional), fail : function (optional)) ###
Saves the current category list with all it's contents to the json file.
After any change, it is recommended to save, but it might take a while, so, auto_save might be considered to add as optional.

Returns true if save was successfull.
Returns false if it wasn't.

```
#!js

   catList.Save();

```

### .Load(callback : function (optional)) ###
This will load the saved information to the variable.
A callback will be executed(if any was given) after the ajax call is done. This is recomended if any further action is to be made(like in the function.ready, call the ui interface function).

```
#!js

var catList = new CategoryList();
catList.Load(DrawCategories);

```