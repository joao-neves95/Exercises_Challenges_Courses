Importance lits is an object that allows to manage the importances in a easier way, with mothods that will make it easier to deal with it and with faster(better performance) results.

# Proprieties #
* importances : [Importances](https://bitbucket.org/shivayl/plannr/wiki/Importance%20Object)[];

### Example ###


```
#!json

{
     "importances": [];
}

```
# Constructor #

There are no necessary parameters to create a new ImportanceList.

### Example ###
```
#!js

var importanceList = new ImportanceList();

```

---

# Methods #

### .Add(importance : [Importance](https://bitbucket.org/shivayl/plannr/wiki/Importance%20Object)) ###
To add an importance to the importanceList, create a new importance and pass it as an argument.´
The id will be added automaticly and should not be altered.

For more information about importances, please visit the [Importance Object](https://bitbucket.org/shivayl/plannr/wiki/Importance%20Object) page.

```
#!js

var importance = new Importance("Urgent", "#f00");
importanceList.Add(importance);

```

### .Remove(id : int, callback : function (optional)) ###
This method removes the given id from the importance list. Be aware that all the contents inside the choosen importance will be lost.
After this, the method ResetIDs() will be automaticly called to set the current objects order.
The callback parameter should be inside an anonymous function, ortherwise it will be executed before the .Remove method.

```
#!js

importanceList.Remove(0, function(){Foo();});

```

### .ResetIDs(callback : function (optional))###
This method will reset the id's of every element, without altering the current order, making it permanent, without a way to undo it.


```
#!js

importanceList.ResetID's(function(){Foo();});

```



### .Save(success : function (optional), fail : function (optional)) ###
In order to save the importance list to a json file, the this method must be called.
If the save is successfull, it will return true, otherwise will return false;
The success function and fail functions are optional callbacks.
In this function, the callbacks can be functions directly input in the argument list, there is no need to call an anonymous function;
```
#!js

function fail(){
     console.log("An error ocurred while saving, please try again later");
}

importanceList.Save(function(){console-log("successfull save!");},fail);

```

### .Load(callback : function) ###
When creating a importance list, this is the first method to be called, otherwise it will override the existing project.
The load function is asynchronous, meaning it wont execute in the instant, it will wait until the server has returned the data and only after that the values will be set, so a callback must be passed if the data is meant to be used.
The callback function can't be put directly inside the parameter because if it is, the parameter will execute before the load function, in order to avoid that, create a anonymous function with the parameter callback inside.

```
#!js

var importanceList = new ImportanceList();
importaceList.Load(function(){callback();});

```