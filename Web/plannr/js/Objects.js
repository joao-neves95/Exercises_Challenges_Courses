class Card{
    constructor(title = "", card = []) {

		if(title != ""){
            this.id = -1;
            this.title = title;
            this.desc = "";
            this.start = "";
            this.end = "";
            this.assigned = [];
            this.complete = false;
            this.comments = [];
            this.importance = -1;
        } else if(card != undefined && card != []){
            this.id = card.id;
            this.title = card.title;
            this.desc = card.desc;
            this.start = card.start;
            this.end = card.end;
            this.assigned = card.assigned;
            this.complete = card.complete;
            this.comments = card.comments;
            this.importance = card.importance;
        } else{
            console.error("To create a card either a title or object must be provided.");
        }
    }

    AddComment(comment){
        comment.id = (this.comments.length > 0) ? this.comments[this.comments.length-1].id+1 : 0;
        this.comments.push(comment);
    }

    RemoveComment(id){
        var index = GetIndexOf(id, this.comments);
        this.comments.splice(index, 1);
    }

    Assign(id){
        this.assigned.push(id);
    }

    Unsign(id){
        var index = this.assigned.indexOf(id.toString());
        this.assigned.splice(index, 1);
    }

    GetDuration(type){
        var tempStart = moment(this.start, "DD-MM-YYYY");
        var tempEnd = moment(this.end, "DD-MM-YYYY");
        switch(type){
            case "days":return (moment.duration(tempEnd.diff(tempStart)).asDays())+1;
            case "months": return (moment.duration(tempEnd.diff(tempStart)).asMonths())+1;
        }
    
}
}

class Comment{
    constructor(userID, message){
        this.id = -1;
        this.user = userID;
        this.comment = message;
    }

}

class Importance{
    constructor(name, color){
        this.id = -1;
        this.name = name;
        this.color = color;
    }
}

class Category{
    constructor(name = "", obj = ""){
        if(obj != ""){
            this.id = obj.id;
            this.name = obj.name;
            this.cards = [];
            if(obj.cards.length > 0){
                for(var i = 0; i < obj.cards.length; i++){
                    this.cards.push(new Card(undefined, obj.cards[i]));
                }
            }
        }else if(name != ""){
            this.id = -1;
            this.name = name;
            this.cards = [];
        }else{
            console.error("To create a category, either the name or and object must be assigned.");
        }
    }

    Add(card){
        card.id = (this.cards.length > 0) ? this.cards[this.cards.length-1].id+1 : 0;
        this.cards.push(card);
    }

    Remove(id){
        var index = GetIndexOf(id, this.cards);
        this.cards.splice(index, 1);
		this.ResetIDs();
    }

    Empty(){
        this.cards = [];
    }

    ResetIDs(){
        for(var i = 0; i < this.cards.length; i++){
            this.cards[i].id = i;
        }
    }
}

class ImportanceList{
    constructor(){
        this.importances = [];
        this.obj = this;
        this.setData = (data, callback = "") => {
            this.importances = data;
            if(callback != "") callback();
        }
    }

    Load(callback = ""){
            $.ajax({
                url: "json/importances.json",
                method: "POST",
                dataType: "json"
            }).done(function(data){
                urgencyList.setData(data, callback);
            }).fail(function(xhr, status, error){
                console.error(error);
            });
        }

    Add(importance){
        importance.id = (this.importances.length > 0) ? this.importances[this.importances.length-1].id+1 : 0;
        this.importances.push(importance);
    }

    Remove(id, callback = ""){
        var index = GetIndexOf(id, this.importances);
        this.importances.splice(index, 1);
        this.ResetIDs(callback);
    }

    ResetIDs(callback){
        for(var i = 0; i < this.importances.length; i++){
            this.importances[i].id = i;
        }
        if(callback != "") callback();
    }

    Save(success = "", fail = ""){
        var imToSave = JSON.stringify(this.importances);
        $.ajax({
            data: "data="+imToSave,
            url: "php/SaveImportances.php",
            method: "POST",
            dataType: "JSON"
        }).done(function(data){
            if(success != "") success();
        }).fail(function(xhr, status, error){
            if(fail != "") fail();
        });
    }
}

class CategoryList{

    constructor(){
        this.categories = [];
    }
    Add(category){
        category.id = (this.categories.length > 0) ? this.categories[this.categories.length-1].id+1: 0;
        this.categories.push(category);
    }

    Remove(id, callback = ''){
        var index = GetIndexOf(id, this.categories);
        this.categories.splice(index,1);
        this.ResetIDs(callback);
    }
    
    ResetIDs(callback){

        for(var i = 0; i < this.categories.length; i++){
            this.categories[i].id = i;
            this.categories[i].ResetIDs();
        }
        if(callback) callback();
    }

    Save(success = '', fail = ''){
        this.ResetIDs()
        var catToSave = JSON.stringify(this.categories);
        $.ajax({
            data: "data="+catToSave,
            url: "php/SaveCards.php",
            method: "POST",
            dataType: "JSON"
        }).done(function(data){
            if(success != '') success();
        }).fail(function(xhr, status, error){
            if(fail != '') fail();
        });
    }

    Load(callback = ''){
        $.ajax({
            url: "json/cards.json",
            method: "POST",
            dataType: "json"
        }).done(function(data){
            categoryList.SetData(data, callback);
        }).fail(function(xhr, status, error){
            console.error(error);
        });
    }

    SetData(data, callback){
        this.categories = [];
        for(var i = 0; i < data.length; i++){
            this.categories.push(new Category(undefined, data[i]));
        }
        if(callback != '') callback();
    }
}

class UserList{
    constructor(){
        this.users = [];
    }

    Load(callback = ''){
           $.ajax({
            url: "json/users.json",
            method: "POST",
            dataType: "json"
        }).done(function(data){
            userList.SetData(data, callback);
        }).fail(function(xhr, status, error){
            console.error(error);
        });
    }

    SetData(data,callback){
        this.users = data;
        if(callback != '')callback();
    }
}

class SortList{
	
	constructor(){
		this.categories = [];
	}
	
	Load(callback = '') {
		for (let i = 0; i < categoryList.categories.length; i++) {
			this.categories.push({'category_id': categoryList.categories[i].id, 'card_ids': []});
			for (let j = 0; j < categoryList.categories[i].cards.length; j++) {
				this.categories[i].card_ids.push(categoryList.categories[i].cards[j].id);
			}
		}
		if(callback != '')callback();
	}
	addCategory(category_id){
		this.categories.push({'category_id': category_id, 'card_ids': []});
	}
	addCard(category_id, card_id){
		for (let i = 0; i < categories.length; i++) {
			if (this.categories[i].category_id == category_id) this.categories[i].cards.push(card_id);
		}
	}
}

    var userList = new UserList();

    var urgencyList = new ImportanceList();

    var categoryList = new CategoryList();

	var defaultSortList = new SortList();
