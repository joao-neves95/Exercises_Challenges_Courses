This method works with a binary search algorithm and will return the desired object within(at maximum) 9 steps, greatly improving the search from elements inside arrays.

Any element that is being searched for, must have and id attribute or it will not work(might give a nullPointerExeption because .id is null).

# Usage #

The function requires the id of the element you want to find, wich  must be an int, and the array where it will look for, returning the index(int) in the array;

### GetIndexOf(id : int, array : Array[])###

```
#!js
var categoryList = new CategoryList();
categoryList.Load();
var index = GetIndexOf(3, categoryList.categories);
console.log(index);
```