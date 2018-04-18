Userlists is an object that allows do load users from the json file.
There are no other functions because user edition/creation will not be implemented.

# Properties #
* users : [User](Link URL)[];


### Example ###

```
#!json

{
     "users" : []
}

```

# Constructor #
There are no necessary parameters to create a new ImportanceList.

### Example ###
var userList = new UserList();

# Methods #

### .Load(callback : function(optional)) ###
Load method will load the users that are in the json file.

```
#!js

var userList = new UserList();
userList.Load(function(){urgencyList.Load(function(){catList.Load()});});

```