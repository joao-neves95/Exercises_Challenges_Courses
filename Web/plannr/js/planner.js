$(function(){
    var start, end;
    var plannerCards;

    var resetCard = "";

    onModalPOST = function(){

        let curCard;
        let ids = resetCard.split('_');
        let originalCard = categoryList.categories[ids[1]].cards[ids[0]];

        plannerCards.forEach(function(card){
            if(card.id == resetCard){
                if(card.title == originalCard.title) location.reload();
                card = originalCard;
                 curCard = originalCard;
                 return;
            }
        });

        $("#"+resetCard).children("p").text(curCard.title);
    }

    categoryList.Load(function(){
        start = GetProjectStart(categoryList);
        end = GetProjectEnd(categoryList);
        while(end == undefined){

        }
        plannerCards = SortCards(categoryList.categories, CalculateDuration);
    });

    var days = 0;
    var months = 0;
    var years = 0;

    var monthsList = new Array();//this will store all the month dom elements

    var monthsDuration = [31,28,31,30,31,30,31,31,30,31,30,31];
    var monthsName = ["January", "February", "March", "April", "May", "June", "Jully", "August", "September", "October", "Novemder", "December"]

    function LoadProject(){
        $("#week-counter").empty();
    }

    function CalculateDuration(){

        //Calculate the year duration and motnh duration
        years = (moment(end).year()-moment(start).year())+1;
        months = (moment(end).month()-moment(start).month())+1;//the months duration needs to be at least 1
        
        //Calculate the day duration
        for(var y = 0; y < years; y++){
            var currentYear = moment(start).year()+y;
            for(var i = 0; i < months; i++){
                
                //-1 because months range from 1-12, indexes range from 0-11
                var curMonth = moment(start).month()+i;//

                //If this is the starting month, see the duration in days of this month
                //ex: starts day 28 of march, then 31-28 = 4 days
                if(i == 0){
                    //if the current year is a leap year and the current month is february, extend th month duration to 29, else use regular month duration
                    days += (curMonth == 1 && IsLeapYear(currentYear)) ? 29-Number(moment(start).date()) : (monthsDuration[curMonth]-moment(start).date())+1;//+1 to add the current day
                }else if(curMonth == moment(end).month() && currentYear == moment(end).year()){//if it is the last month, just add the day
                    days += moment(end).date();
                }else{//else add the whole month duration
                    //if current year is a leap year and the current month is february, add 29, else add regular 28
                    days += (curMonth == 1 && IsLeapYear(currentYear)) ? 29 : monthsDuration[curMonth];

                }
            }
        }
        if(end != 0 && start != 0)
        days = (moment.duration(end.diff(start)).asDays())+1;
        else {
            months = 1;
            days = 1;
            start = moment().add(-1, 'days');
            end = moment().add(-1, 'days');
        }
        DrawPlanner();
    }

    function DrawPlanner(){
        $("#planner").empty();
        var remainingDays = days;
        var curDay = moment(start).date();
        for(var y = 0; y < years; y++){
            for(var m = 0; m < months; m++){
                var curMonth = moment(start).month()+m;//months range from 1-12, indexes range from 0-11
                var $month = $("<section class='month'><p>"+monthsName[curMonth]+"</p></section>");
                var dayCounter = 0;
                for(var d = 0; d < remainingDays; d++){

                    if(curDay > monthsDuration[curMonth]){
                        curDay = 1;
                        break;
                    }
                    var tempDate = curDay+"/"+((curMonth.length == 1) ? (curMonth+1) : "0"+(curMonth+1) )+"/"+(moment(start).year()+y);
                    tempDate = moment(tempDate, "DD/MM/YYYY");
                    tempDate = moment(tempDate).format("dddd");
                    var $day = $('<article class="day-1 official"><p>'+tempDate.substr(0, 1)+'</p><p>'+curDay+'</p></article>');
                    if(tempDate.substr(0,1) == "S") $day.css("cssText", "background-color: #c5c5c5 !important");
                    $month.append($day);
                    dayCounter++;
                    curDay++;
                }
                remainingDays -= dayCounter;
                var monthStarts = monthsDuration[curMonth]-(dayCounter-1);//-1 to count the last day and avoid months to start on day 0
                if(monthStarts < 10) monthStarts = "0"+monthStarts;
                var formatedMonth = (curMonth < 10) ? "0"+(curMonth+1) : (curMonth+1);
                $($month).css("width", 100*dayCounter).attr("data-plannr-duration", dayCounter).attr("data-plannr-date", monthStarts+"-"+formatedMonth+"-"+(moment(start).year()+y));
                $("#planner").append($month);
                monthsList.push($month);
            }
        }

        //Cards are drawn in parts, each part is inside a month
        //To avoid tasks being on the same row, each month, after the cards, needs to have an empty 
        //div with the duration of the project
        //this will design each part
        for(var c = 0; c < plannerCards.length; c++){

            var curCard = plannerCards[c];

            var cardStart = moment(plannerCards[c].start, "DD-MM-YYYY");
            var cardEnd = moment(plannerCards[c].end, "DD-MM-YYYY");

            //controll wich part is left to be added, when both true, then needs to add a div to break the row
            var startImplemented = false;
            var endImplemented = false;

            for(var m = 0; m < monthsList.length; m++){

                var tempDate = $(monthsList[m]).attr("data-plannr-date");
                var thisMonth = moment(tempDate, 'DD-MM-YYYY');
                var $tempCard = $("<article id='"+curCard.id+"'></article>");
                var $tempText = $("<p>"+curCard.title+"</p>");
                $($tempCard).append($tempText);

                var taskOffset = 0;
                var taskDuration = monthsDuration[moment(thisMonth).month()];
                var bg = urgencyList.importances[curCard.importance].color;

                var startsThisMonth = (moment(thisMonth).isSame(cardStart, "year") && moment(thisMonth).isSame(cardStart, "month")) ? true : false;
                var endsThisMonth = (moment(thisMonth).isSame(cardEnd, "year") && moment(thisMonth).isSame(cardEnd, "month")) ? true : false;
                var rbDur = 0;

                    //if this task starts and ends in the same month
                    if(startsThisMonth && endsThisMonth){
                            $($tempCard).addClass("dur day");//set full duration class and set length
                            taskOffset = (moment(start).isSame(thisMonth, "month")) ? (moment(cardStart).date()-1)-(moment(start).date()-1): moment(cardStart).date()-1;//if the first day = 1, offset should be 0
                            taskDuration = curCard.GetDuration("days");
                            endImplemented = true;
                            startImplemented = true;
                            $(monthsList[m]).append($tempCard);
                    }else if(!startImplemented && startsThisMonth){//set start duration class and set the rest of the month(days left) to article length
                        $($tempCard).addClass("dur-start day");
                        taskOffset = moment(cardStart).date()-moment(thisMonth).date();
                        taskDuration = monthsDuration[moment(thisMonth).month()]-(moment(thisMonth).date()-1)-taskOffset;
                        startImplemented = true;
                        $(monthsList[m]).append($tempCard);
                    }else if(!endImplemented){
                        if(endsThisMonth && startImplemented){
                            $($tempCard).addClass("dur-end day");//set full duration class and set length
                            taskOffset = 0;
                            taskDuration = moment(cardEnd).date();
                            endImplemented = true;
                            $(monthsList[m]).append($tempCard);
                        }else if(startImplemented){
                            $($tempCard).addClass("dur day");//set full duration class and set length
                            taskOffset = 0;
                            taskDuration = monthsDuration[moment(thisMonth).month()];
                            $(monthsList[m]).append($tempCard);
                        }
                    }
                    $($tempCard).css({"margin-left": 100*taskOffset+"px",
                                      "width": 100*taskDuration+"px",
                                      "background-color": bg});
                    $($tempText).css({"color": SetTextColor(bg)});
                    
                    $tempCard.on("click", function(){
                        var tempId = $(this).attr("id").split('_');
                        injectDataIntoModal(parseInt(tempId[1]), parseInt(tempId[0]), true);
                        resetCard = $(this).attr("id");
                    });

                    if(startImplemented && endImplemented){

                        if(!endsThisMonth) taskDuration = 0;
                        var $rb = $("<div class='row-breaker day'></div>");
                         rbDur = monthsDuration[moment(thisMonth).month()];
                        //if this is the last month of the project, set the width of the div to the days duration
                        if(moment(end).isSame(thisMonth, "month") && moment(end).isSame(thisMonth, "year")){
                            rbDur = moment(end).date(); 
                        }
                        
                        if(moment(thisMonth).month() == moment(start).month() && moment(thisMonth).year() == moment(start).year()){//this is the first month
                            rbDur -= moment(start).date();
                        }

                        rbDur = rbDur-taskDuration-taskOffset;
                        $($rb).css({"width": 100*rbDur+"px"});
                        $(monthsList[m]).append($rb);
                        $(monthsList[m]).append("<div class='clearfix'></div>");
                    }
                }
            }           
    }
});