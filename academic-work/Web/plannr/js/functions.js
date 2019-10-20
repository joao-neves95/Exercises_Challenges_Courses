//binary search algorith, finds the result in 9 atempts at maximum, independently of the amount of data
function GetIndexOf(id, array){
    if(array.length == 1) return 0;
    
    var indexCounter = Math.round(array.length/2);
    var curUnit = Math.round(array.length/2);
    for(var i = 0; i < 9; i++){
        if(array[indexCounter].id == id){
            return indexCounter;
        }else if(array[indexCounter].id > id){
            curUnit -= Math.round(curUnit/2);
            indexCounter -= curUnit;
        }
        else if(array[indexCounter].id < id){
            curUnit -= Math.round(curUnit/2);
            indexCounter += curUnit;
        }
    }
}

function checkRgb (rgb) {
  var rxValidRgb = /([R][G][B][A]?[(]\s*([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\s*,\s*([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\s*,\s*([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])(\s*,\s*((0\.[0-9]{1})|(1\.0)|(1)))?[)])/i
  if (rxValidRgb.test(rgb)) {
    return true
  }
}

function updateCardsIds(category_id, card_id, callback = '') {
	for (let i = card_id; i < categoryList.categories[category_id].cards.length; i++) {
		let next_card_id = 0;
		next_card_id = parseInt(i) + 1;
		next_card_id = next_card_id.toString().replace(/^0+/, '');
		$(`#category-${category_id}_card-${card_id}`).attr('id', `category-${category_id}_card-${next_card_id}`);
		if (callback != '') callback();
	}
}

//To test for the project start, the function will:
//for each category
// for each card inside the current category
//  Check if the start year is lower to the last recorded, if it is set the last recorded to it, if not, next step
//  if the year is equals, do the same with the months and then with the days
function GetProjectStart(catList, callback = ""){

    var start = 0;

    for(var i = 0; i < catList.categories.length; i++){
        for(var j = 0; j < catList.categories[i].cards.length; j++){
            var tempStart = moment(catList.categories[i].cards[j].start, "DD-MM-YYYY");

            if(start == 0) start = tempStart;
            else if(tempStart < start) start = tempStart;
        }
    }

    if(callback != "")  setTimeout(callback, 100);
    return start;
}

function GetProjectEnd(catList, callback = ""){
    var end = 0;

    for(var i = 0; i < catList.categories.length; i++){
        for(var j = 0; j < catList.categories[i].cards.length; j++){
            var tempEnd = moment(catList.categories[i].cards[j].end, "DD-MM-YYYY");

            if(end == 0) end = tempEnd;
            else if(tempEnd > end) end = tempEnd;
        }
    }
    return end;
}

function IsLeapYear(year){
    if(year % 4 == 0){
        return true;
    }else return false;
}

function SortCards(categories, callback = ""){
    var cards = new Array();

    for(var i = 0; i < categories.length; i++){
        for(var c = 0; c < categories[i].cards.length; c++){
            var temp = new Card("", categories[i].cards[c]);
            temp.id = i+"_"+c;
            cards.push(temp);
        }
    }

    var cardsB = MergeSort(cards, "date");

    if(callback != "") setTimeout(callback, 100);
    return cardsB;
}

//This is an efficient algorithm. The algorithm itself wasn't created by Plannr team, only the code that applied it
//This function will divide the cards list in smaller lists until they reach lists of length = 1
//then it will Merge() them;
    function MergeSort(list, property){

        if(list.length <= 1) return list;

        var middle = Math.ceil(list.length/2);
        middle--;
        var left = new Array();
        var right = new Array();
        
        for(var i = 0; i <= middle; i++){
            left.push(list[i]);
            if(list.length-1 > middle+i) right.push(list[middle+(i+1)]);
        }

        left = MergeSort(left, property);
        right = MergeSort(right, property);

        return Merge(left, right, property);
    }

    //If the first element of the left is bigger than the fist of right, add left to final and remove it from left list
    //else do the same for the right
    //this will sort elements from lists, putting the smaller at left and the biggest at the right
    //returns the junction of 2 lists
    function Merge(left, right, property){
        var final = new Array();

        while( left.length > 0 || right.length > 0){
                if(left.length == 0 && right.length > 0){
                    final.push(right[0]);
                    right.shift();
                    break;
                }
                else if(left.length > 0 && right.length == 0){
                    final.push(left[0]);
                    left.shift();
                    break;
                }

                var first = false;//if this is true, it will run the first statment of the if
                if(property == "date"){
                    var date1 = moment(left[0].start, "DD-MM-YYYY").toDate();
                    var date2 = moment(right[0].start, "DD-MM-YYYY").toDate();
                    first = (date1 > date2) ? true : false;
                }else if(property == "urgency"){
                    first = (left[0].urgency > right[0].urgency) ? true : false;
                }
                if(first)
                {
                    final.push(right[0]);
                    right.shift();
                }else{
                    final.push(left[0]);
                    left.shift();
                }
        }
        return final;
    }

//src: https://gist.github.com/geekmy/5010419
function get_brightness(hexCode) {
  // strip off any leading #

  if(checkRgb(hexCode)){
    hexCode = rgb2hex(hexCode);
  }
  hexCode = hexCode.replace('#', '');

  var c_r = parseInt(hexCode.substr(0, 2),16);
  var c_g = parseInt(hexCode.substr(2, 2),16);
  var c_b = parseInt(hexCode.substr(4, 2),16);

  var result = ((c_r * 299) + (c_g * 587) + (c_b * 114)) / 1000;
  return result;
}

function SetTextColor(colorHex){
    var brightness = get_brightness(colorHex);

    if(brightness < 150) return "#fff";
    else return "#000";
}

// convert RGB to hex
// src: http://stackoverflow.com/questions/1740700/get-hex-value-rather-than-rgb-value-using-jquery
function rgb2hex(rgb) {
     if (  rgb.search("rgb") == -1 ) {
          return rgb;
     } else {
          rgb = rgb.match(/^rgba?\((\d+),\s*(\d+),\s*(\d+)(?:,\s*(\d+))?\)$/);
          function hex(x) {
               return ("0" + parseInt(x).toString(16)).slice(-2);
          }
          return "#" + hex(rgb[1]) + hex(rgb[2]) + hex(rgb[3]); 
     }
}