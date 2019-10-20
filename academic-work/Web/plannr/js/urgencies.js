$(function(){
SwitchTheme();
$(".blackout").hide();
    //Opens the urgencies panel
    $("a#open-manage-urgencies").on("click", function(e){
        e.preventDefault();//stops page refresh since button is an A
        $("#manage-urgencies").fadeIn();
        $("#manage-urgencies .blackout").css("overflow-y", "scroll");
    });

    $("#theme-checkbox").on("change", function(){
        SwitchTheme();
    });

    function SwitchTheme(){
        let theme = ($("#theme-checkbox").is(":checked")) ? "dark" : "light";
        if(theme == "dark") {
            $("#urgency_name").css("color", "white");
            $("#color-input").css("color", "white");
        }
        else{
            $(".urgencies-panel input").css("color", "black");
            $("#color-input").css("color", "black");
        }
    }

    //Adds an urgency when clicked
    $("#add_urgency").on("click", function(){
        var name = $("#urgency_name").val();
        var color = $("#color-input").val();

        var execute = true;//stops the Function execution without stopping the rgb check

        if(name == ''){//urgencies need to have a name
            $("#urgency_name").addClass("invalid");
            execute = false;
        }
        if(!checkRgb(color)){//color must be rgb format
            $("#color-input").addClass("invalid");
            execute = false;
        }

        //if one or both of the fields are not valid, stop function
        if(!execute) return;

        //reset inputs
        $("#urgency_name").val("").removeClass("valid");
        $("#color-input").val("").removeClass("valid");

        //creates a new urgency object and sets values
        var newUrgency = new Importance();
        newUrgency.name = name;
        newUrgency.color = color;

        urgencyList.Add(newUrgency);//add urgency to the urgency list in json
        AddUrgency(newUrgency);//Add urgency in html

        //save list to json and show info pop-up
        urgencyList.Save(function(){
            Materialize.toast("<span class='green'>Successfully</span>&nbspcreated&nbspurgency&nbsp"+name, 4000);
        });
    });

    urgencyList.Load(function(){LoadUrgencies()});

    function LoadUrgencies(){
        var $table = $(".urgency-table");
        $table.empty();
        $table.append("<thead><th class='col s5'>Name</th><th class='col s5'>Color</th><th class='col s2'></th></thead>");
        var $body = $("<tbody class='urgency_body'></tbody>");
        $table.append($body);
        urgencyList.importances.forEach(function(element) {
            AddUrgency("#manage-urgencies .urgency_body", element)
        });
		urgencyList.importances.forEach(function(element) {
            AddUrgency("#manage-filtersort .urgency_body", element)
        });
    }

    function AddUrgency(tableid, element){

            var id = (element.Id == undefined) ? element.id : element.ID;
            var name = (element.Name == undefined) ? element.name : element.Name;
            var color = (element.Color == undefined) ? element.color : element.Color;
            var $tr = $("<tr id='urgency_"+id+"'></tr>");
            var $td = $("<td class='col s2 m5'></td>");
            var p = $("<p id='urgency_p_"+id+"'>"+name+"</p>");
            EditP(p);
            $td.append(p);
            $tr.append($td);
            $colorTd = $("<td class='col s8 m5'></td>");

            var colorP = $("<p id='u_color_"+id+"' class='inline-p'>"+color+"</p>").on("click", function(){
                SetColorPick($(this));
            });
            $colorTd.append("<button class='btn pickButton' id='picker_"+id+"' style='background:"+color+"' value='#5367ce'>").append(colorP);
            $tr.append($colorTd);
			var $option;
            if (tableid == "#manage-urgencies .urgency_body") {
				$option = $("<a data-target='urgency_"+id+"' class='btn-floating btn-small waves-effect waves-light red'><i class='material-icons'>delete</i></a>");
				$option.on("click", function(){
					if(!confirm("Are you sure you want to delete this category? \n Deleted urgencies can't be recovered after being deleted.")) return;
					var id = $(this).attr("data-target").split('_');
					var name = urgencyList.importances[id[1]].name;
					var $target = $("#urgency_"+id[1]);
					urgencyList.Remove(id[1], function(){LoadUrgencies();});
					urgencyList.Save(function(){
						Materialize.toast("<span class='red'>Deleted</span>&nbspurgency&nbsp"+name, 4000);
					});

				});
			}
            else $option = $("<input id='"+id+"' type='checkbox' checked='checked' class='urgency filled-in'/><label for='"+id+"'>"+name+"</label>");
            $td = $("<td class='col s2'></td>").append($option);
            $tr.append($td);
            $(tableid).append($tr);
    }

    function EditP(p){
        $(p).on("click", function(){
                var id = $(this).attr("id").split('_');
                var $input = $("<input autofocus type='text' id='u_input_"+id[2]+"' />");
                $input.val($(this).text());
                SaveInput($input);
                $(this).parent().append($input);
                $($input).focus();
                $(this).remove();
            });
    }

    function SaveInput(input){
        $(input).blur(function(){
            var id = $(this).attr("id").split('_');
            var name = $(this).val();
            var oldName = urgencyList.importances[id[2]].name;
            var p = $("<p id='urgency_p_"+id[2]+"'>"+name+"</p>");
            if(name != urgencyList.importances[id[2]].name){
                urgencyList.importances[id[2]].name = name;
                var saved = urgencyList.Save(function(){
                    Materialize.toast('Successfully renamed&nbsp<span class="red">'+oldName+'</span>&nbspto&nbsp<span class="green">'+urgencyList.importances[id[2]].name+'</span>', 4000);
                }, function(){
                    Materialize.toast("<span class='red'>Failed</span> to rename urgency", 4000);
                });
            }
            EditP(p);
            $(this).parent().append(p);
            $(this).remove();
        });
    }

    function SetColorPick(p){
        
        var div = $(p).parent();
        var color = $(p).text();
        $(div).empty().addClass("file-field");
        var id = $(p).attr("id").split('_');
        var button;
        button = $("<button class='btn' id='picker_"+id[2]+"' value='"+color+"' style='background-color:"+color+"'><div class='color-picker' id='color-picker_"+id[2]+"'></div></button>");
        var wrapper = $("<div class='file-path-wrapper relative'></div>");
        var input = $("<input autofocus type='text' id='color-input_"+id[2]+"' value='"+color+"' class='validate'/>").blur(function(){
            SetColorP($(this));
        });
        wrapper.append(input);
        $(div).append(button).append(wrapper);
        $(input).colorpicker({
            // Alpha is not supported for some html elements.
            format: 'rgb',
            // Gets the actual background color
            color: $('#picker_'+id[2]).css('background-color'),
            container: "#color-picker_"+id[2],
            component: '#picker_'+id[2]
    }).on('changeColor', function(ev) {
      // Sets the new color while it is changing.
      var id = $(this).attr("id").split('_');
      $("#picker_"+id[1]).css('background-color',ev.color.toHex());
    });
        $(input).focus();
}

    function SetColorP(element){
        var color = $(element).val();
        var $td = $(element).parent().parent();
        var id = $(element).attr("id").split('_');
        $(element).parent().parent().empty();
        var colorP = $("<p id='u_color_"+id[1]+"' class='inline-p'>"+color+"</p>").on("click", function(){
                SetColorPick($(this));
        });
        $td.append("<button class='btn pickButton' id='picker_"+id[1]+"' style='background:"+color+"' value='#5367ce'>").append(colorP);
        urgencyList.importances[id[1]].color = color;
        var saved = urgencyList.Save(function(){
			let categoryListLength = categoryList.categories.length;
			for (let i = 0; i < categoryListLength; i++) {
				let categoryListCardsLength = categoryList.categories[i].cards.length;
				for (let j = 0; j < categoryListCardsLength; j++) {
					changeCard(i, j, 'importance');
				}
			}
            Materialize.toast('Successfully edited&nbsp<span class="green">'+urgencyList.importances[id[1]].name+'</span>', 4000);
        }, function(){
            Materialize.toast("<span class='red'>Failed</span> to edit urgency", 4000);
        });
}
});