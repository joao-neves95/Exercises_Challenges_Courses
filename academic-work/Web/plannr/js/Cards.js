$("document").ready(function () {
	moment.locale('pt');
	userList.Load();
	//Interface functions
	//Stores the control's id before transforming it into the standard input #user-input
	let userInputId;
	let transitionSpeedSlow = 400, transitionSpeedMedium = 200, transitionSpeedFast = 50;
	$(".options").on("click", function () {
		let id = $(this).attr("id").split('_');
		id = ".menu_" + id[1] + "_" + id[2];
		let state = $(this).attr("data-state");
		if (state == "on") {
			$(this).attr("data-state", "off").removeClass("open");
			$(id).attr("data-state", "off").slideUp(300);
		} else {
			$(this).attr("data-state", "on").addClass("open");
			$(id).attr("data-state", "on").slideDown(300);
		}
	});
	//Add Categories
	$('#addcategory').click(function (e) {
		e.stopPropagation();
		$('#categories').append(newCategory());
		slideRight('#category_new', transitionSpeedMedium);
		userInputId = 'new-category-name';
		$('#new-category-name').click();
		$(this).attr('disabled', true);
		$(this).children().text('chevron_left');
		Refresh();
	});
	//Add Cards
	$("section#categories").on("click", '#addcard', function (e) {
		e.stopPropagation();
		$(this).parent().after(newCard());
		$('#card_new').slideDown(transitionSpeedMedium);
		userInputId = 'new-card-title';
		$('#new-card-title').click();
		$(this).attr('disabled', true);
		$(this).children().text('expand_more');
		Refresh();
	});
	//Edit Categories & Cards (Allows the user to edit information by transforming an element into an input)
	$("section#categories").on("click", '.editable', function (e) {
		e.stopPropagation();
		if (userInputId == 'importance' || userInputId == 'assigned' || userInputId == 'start-end') $('body').click();
		userInputId = $(this).attr('id');
		let userInputText = $(this).text();
		let userInputPlaceholder;
		let transition = false;
		let importanceColor = '';
		switch (userInputId) {
			case 'new-category-name':
				userInputPlaceholder = 'My new category...';
				break;
			case 'category-name':
				userInputPlaceholder = 'My category...';
				break;
			case 'new-card-title':
				userInputPlaceholder = 'My new card...';
				transition = true;
				break;
			case 'title': {
				userInputPlaceholder = 'My title...';
				let category_id = getCategory_id($(this));
				let card_id = getCard_id($(this));
				importanceColor = urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color;
				break;
			}
			case 'desc': {
				userInputPlaceholder = 'My description...';
				let category_id = getCategory_id($(this));
				let card_id = getCard_id($(this));
				importanceColor = urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color;
				break;
			}
			default:
				userInputPlaceholder = 'Write here...';
		}
		$(this).replaceWith(editableInput(transition, userInputText, userInputPlaceholder, importanceColor));
		if (userInputId == 'new-card-title') $('#user-form').slideDown(transitionSpeedMedium*4.5);
		$('#user-input').focus();
		$('#user-input')[0].setSelectionRange(0, $('#user-input').val().length);
	});
	//Finish editing Categories & Cards (Updates the JSON file after the user has unfocused the input)
	////Important Note: Since selects don't have a focusout event, it's safe to keep this function even if the #user-input is a select.
	////The same can't be said about the change event, used whenever #user-input is a select, since text inputs happen to have that function, the change event has to be declared and deleted accordingly
	$("section#categories").on("focusout", '#user-input', function (e) {
		e.stopPropagation();
		let newVal = $(this).val();
		let category_id = getCategory_id($(this));
		//If the category in question was newly created
		if (userInputId == 'new-category-name') {
			$('#addcategory').attr('disabled', false);
			$('#addcategory').children().text('add');
			if (newVal.trim().length == 0) {
				let category = $(this).parentsUntil('section').parent();
				slideLeft('#category_new', transitionSpeedMedium, function() { category.remove() });
				return;
			}
			addCategory(newVal);
		}
		//Else if the category already exists, and its attributes were just modified
		else if (userInputId == 'category-name') {
			if (validateUserInput($('#user-input')) == false) return;
			editCategory(category_id, newVal, stopEditing);
		}
		//Else if the category already existed, and its card was just edited
		else {
			let card_id = getCard_id($(this));
			//If the card in question was newly created
			if (userInputId == 'new-card-title') {
				$(this).parentsUntil('section.category').parent().find('a#addcard').attr('disabled', false);
				$(this).parentsUntil('section.category').parent().find('a#addcard').children().text('add');
				if (newVal.trim().length == 0) {
					let card = $(this).parentsUntil('article.card').parent();
					card.slideUp(transitionSpeedMedium, function() { card.remove() });
					return;
				}
				addCard(category_id, newVal);
			}
			//Else if the card already existed, and its attributes were just modified
			else {
				if (userInputId == 'title' && !validateUserInput($('#user-input'))) return;
				editCard(category_id, card_id, userInputId, newVal, stopEditing);
			}
		}
	});
	$("section#categories").on("keypress", '#user-input', function (e) {
		e.stopPropagation();
		if (e.keyCode == 13) {
			$(this).blur();
		}
	});
	//Cards' Importance and Assigned
	//Add/Edit Cards' Importance, Assigned and Start/End Dates
	$("section#categories").on("click", '.editable-select', function (e) {
		e.stopPropagation();
		if (userInputId == 'importance' || userInputId == 'assigned' || userInputId == 'start-end') $('body').click();
		let id = $(this).attr('id').split('_')[0];
		console.log($(this));
		let category_id = getCategory_id($(this));
		let card_id = getCard_id($(this));
		switch (id) {
			case 'importance': {
				userInputId = 'importance';
				$(this).replaceWith(editableSelectImportance(category_id, card_id));
				$('#user-input').change(function () {
					if ($("#user-input").val() == -1) {
						changeCard(category_id, card_id, 'importance', stopEditing);
						return;
					}
					$("#user-input").unbind("change");
					editCard(category_id, card_id, 'importance', $('#user-input').val(), stopEditing);
				});
				break;
			}
			case 'assigned': {
				userInputId = 'assigned';
				$(this).parent().find(`div.chip`).remove();
				$(this).parent().find(`span.chip-connector`).remove();
				$(this).next().after(editableSelectAssigned(category_id, card_id));
				$('#user-input').change(function () {
					if ($("#user-input").val() == -1) {
						changeCard(category_id, card_id, 'assigned', stopEditing);
						return;
					}
					$("#user-input").unbind("change");
					editCard(category_id, card_id, 'assigned', 'A_'+$("#user-input").val(), stopEditing);
				});
			}
		}
		$('#user-input').material_select();
	});
	//Delete Cards' Assigned
	$("section#categories").on("click", '#assignment_delete', function (e) {
		e.stopPropagation();
		let category_id = getCategory_id($(this));
		let card_id = getCard_id($(this));
		let assignment_id = $(this).parent().attr('id').split('_')[1];
		console.log(assignment_id);
		editCard(category_id, card_id, 'assigned', 'U_'+assignment_id, stopEditing);
	});
	//Cards' Dates
	$("section#categories").on("click", '#start-end', function (e) {
		e.stopPropagation();
		if (userInputId == 'importance' || userInputId == 'assigned' || userInputId == 'start-end') $('body').click();
		userInputId = 'start-end';
		let category_id = getCategory_id($(this));
		let card_id = getCard_id($(this));
		start = categoryList.categories[category_id].cards[card_id].start;
		end = categoryList.categories[category_id].cards[card_id].end;
		if (start.length == 0 || end.length == 0) {
			start = moment();
			end = moment().add(7, 'days');
		}
		$(this).html(editableInputsStartEnd(category_id, card_id));
		$('.datepicker').pickadate({
			selectMonths: true, // Creates a dropdown to control month
			selectYears: 15, // Creates a dropdown of 15 years to control year,
			today: 'Today',
			close: 'Ok',
			closeOnSelect: false, // Close upon selecting a date,
			format: 'dd-mm-yyyy'
		});
	});
	$("section#categories").on("click", '#start-end .picker', function (e) {
		e.stopPropagation();
		$(this).find('.picker__footer').find('.picker__close').click();
	});
	$('body').click(function() {
		if (userInputId == 'start-end') {
			let category_id = getCategory_id($($('#start')));
			let card_id = getCard_id($($('#end')));
			if (!validateUserInput('#start_#end', 1)) return;
			$('.datepicker').stop();
			editCard(category_id, card_id, 'start-end', $('#start').val()+'_'+$('#end').val(), stopEditing);
		}
		else if (userInputId == 'assigned' || userInputId == 'importance') {
			$('#user-input').change();
		}
	});
	//Validate inputs
	/*validation_type:
	No value - default, inly invalidates empty fields;
	1 - two-date validation, pass the 'firstDate-lastDate';
	*/
	window.validateUserInput = function (field, validation_type = '') {
		if (validation_type != '') {
			if (validation_type == 1) {
				let startend = field.split('_');
				let start = $(`${startend[0]}`).val();
				let end = $(`${startend[1]}`).val();
				if (moment(start, 'DD-MM-YYYY').isAfter(moment(end, 'DD-MM-YYYY'))) {
					$(`${startend[1]}`).val($(`${startend[0]}`).val());
					return false;
				}
			}
		}
		else if (field.val().length == 0) {
			field.addClass('invalid');
			return false;
		}
		return true;
	}	
	//-----
	//Refresh functions
	window.stopEditing = function() {
		categoryList.Save();
		Refresh();
	}
	//Common functions between JavaScript files
	function Refresh() {
		$('#c-filter').change();
		$('.tooltipped').tooltip({delay: 50});
		updateTheme();
		updateCardsModal();
		updateDroppableItems();
		updateDraggableItems();
	}
	window.getCategory_id = function ($childElem) {
		return $childElem.parentsUntil('section.category').parent().attr('id').split('_')[1];
	}
	window.getCard_id = function ($childElem) {
		return $childElem.parentsUntil('article').parent().attr('id').split('-')[2];
	}
	function loadAllCategories() {
		for (let i = 0; i < categoryList.categories.length; i++) {
			loadCategory(i, function() {
				for (let j = 0; j < categoryList.categories[i].cards.length; j++) {
					loadCard(i, j);
				}
			});
		}
		Refresh();
	}
	function addCategory(newVal) {
		categoryList.Add(new Category(newVal));
		category_id = categoryList.categories.length - 1;
		slideLeft('#category_new', (transitionSpeedMedium / 1.5), function() {
			$('#category_new').remove();
			loadCategory(category_id, stopEditing());
		});
	}
	function addCard(category_id, newVal) {
		let new_card = new Card(newVal);
		new_card.desc = "";
		new_card.start = moment().format('DD-MM-YYYY');
		new_card.end = moment().add(7, 'days').format('DD-MM-YYYY');
		new_card.importance = urgencyList.importances[2].id;
		categoryList.categories[category_id].Add(new_card);
		card_id = categoryList.categories[category_id].cards.length - 1;
		$('#card_new').slideUp(transitionSpeedMedium, function() {
			$('#card_new').remove();
			previousSortedVal = -1;
			loadCard(category_id, card_id, stopEditing());
		});
		
	}
	function loadCategory(category_id, callback = '') {
		//If the HTML doesn't have any category yet, append the new one here, else append it elsewhere 
		if (!$('#categories').children().length) $('#categories').append(category(category_id));
		else $(`#category_${(category_id-1)}`).after(category(category_id));
		if (callback != '') slideRight(`#category_${categoryList.categories[category_id].id}`, transitionSpeedSlow, callback);
		else slideRight(`#category_${categoryList.categories[category_id].id}`, transitionSpeedSlow);
		updateDraggableItems();
		logAllCategories();
	}
	function loadCard(category_id, card_id, callback = '') {
		//If the category doesn't have any card yet, append the new one here, else append it elsewhere 
		if (!$(`#category_${category_id}`).children('section.droppable').children().length) $(`#category_${category_id}`).children('section.droppable').append(card(category_id, card_id, true));
		else $(`#category-${category_id}_card-${(card_id-1)}`).after(card(category_id, card_id, true));
		$(`#category-${category_id}_card-${card_id}`).slideDown(transitionSpeedSlow);
		updateCardsModal();
		updateDraggableItems();
		if (callback != '') callback();
		logAllCategories();
	}
	//While the functions editCategory and editCard are meant to modify the categories and cards through the Cards.js' editing functionalities, the functions changeCategory and changeCard are meant to
	//modify them through unconventional means, such as the CardsModal.js
	function editCategory(category_id, newVal, callback = '') {
		categoryList.categories[category_id].name = newVal;
		$(`#category_${category_id}`).find('#user-form').parent().replaceWith(categoryTitle(category_id, true));
		$(`#category_${category_id}`).find('#category-name').fadeIn(transitionSpeedSlow);
		if (callback != '') callback();
	}
	function editCard(category_id, card_id, attribute, newVal, callback = '') {
		switch (attribute) {
			case 'title': {
				categoryList.categories[category_id].cards[card_id].title = newVal;
				$(`#category-${category_id}_card-${card_id}`).find('#user-form').replaceWith(cardTitle(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('p#title').fadeIn(transitionSpeedSlow);
				break;
			}
			case 'desc': {
				categoryList.categories[category_id].cards[card_id].desc = newVal;
				$(`#category-${category_id}_card-${card_id}`).find('#user-form').replaceWith(cardDesc(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('p#desc').fadeIn(transitionSpeedSlow);
				break;
			}
			case 'importance': {
				categoryList.categories[category_id].cards[card_id].importance = newVal;
				$(`#category-${category_id}_card-${card_id}`).replaceWith(card(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).fadeIn(transitionSpeedSlow);
				break;
			}
			case 'assigned': {
				let operation = newVal.split('_')[0];
				let val = newVal.split('_')[1];
				console.log(val);
				if (operation == 'A') categoryList.categories[category_id].cards[card_id].Assign(val);
				else if (operation == 'U') categoryList.categories[category_id].cards[card_id].Unsign(val);
				$(`#category-${category_id}_card-${card_id}`).find('span#assigned').parent().replaceWith(cardAssigned(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('span#assigned').parent().fadeIn(transitionSpeedSlow);
				break;
			}
			case 'start-end': {
				let start = newVal.split('_')[0];
				let end = newVal.split('_')[1];
				categoryList.categories[category_id].cards[card_id].start = start;
				categoryList.categories[category_id].cards[card_id].end = end;
				$(`#category-${category_id}_card-${card_id}`).find('span#start-end').parent().replaceWith(cardStartEnd(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('span#start-end').parent().fadeIn(transitionSpeedSlow);
				break;
			}
		}
		userInputId = '';
		previousSortedVal = -1;
		if (callback != '') callback();
	}
	function changeCategory(category_id, newVal, callback = '') {
		$(`#category_${category_id}`).find('#user-form').replaceWith(category(category_id, true));
		$(`#category_${category_id}`).find('#category-name').fadeIn(transitionSpeedSlow);
		if (callback != '') callback();
	}
	window.changeCard = function (category_id, card_id, attribute, callback = '') {
		switch (attribute) {
			case 'title': {
				$(`#category-${category_id}_card-${card_id}`).find('p#title').replaceWith(cardTitle(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('p#title').fadeIn(transitionSpeedSlow);
				break;
			}
			case 'desc': {
				$(`#category-${category_id}_card-${card_id}`).find('p#desc').replaceWith(cardDesc(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('p#desc').fadeIn(transitionSpeedSlow);
				break;
			}
			case 'importance': {
				$(`#category-${category_id}_card-${card_id}`).find('span#importance').replaceWith(cardImportance(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find(`span#importance_${categoryList.categories[category_id].cards[card_id].importance}`).fadeIn(transitionSpeedSlow);
				break;
			}
			case 'assigned': {
				$(`#category-${category_id}_card-${card_id}`).find('span#assigned').parent().replaceWith(cardAssigned(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('span#assigned').parent().fadeIn(transitionSpeedSlow);
				break;
			}
			case 'start-end': {
				$(`#category-${category_id}_card-${card_id}`).find('span#start-end').parent().replaceWith(cardStartEnd(category_id, card_id, true));
				$(`#category-${category_id}_card-${card_id}`).find('span#start-end').parent().fadeIn(transitionSpeedSlow);
				break;
			}
		}
		userInputId = '';
		if (callback != '') callback();
	}
	window.removeCategory = function (category_id, callback = '') {
		$(`#category_${category_id}`).find('article.card').each(function() { $(this).slideUp(transitionSpeedSlow, function() { $(this).remove(); }); });
		slideLeft(`#category_${category_id}`, transitionSpeedSlow,function () {
			$(`#category_${category_id}`).remove();
		});
		categoryList.Remove(category_id);
		categoryList.Save();
		updateCategoriesIds(category_id);
		if (callback != '') callback();
		logAllCategories();
	}
	window.removeCard = function (category_id, card_id, callback = '') {
		$(`#category-${category_id}_card-${card_id}`).slideUp(transitionSpeedSlow, function () {
			$(`#category-${category_id}_card-${card_id}`).remove();
		});
		categoryList.categories[category_id].Remove(card_id);
		categoryList.Save();
		updateCardsIds(category_id, card_id);
		if (callback != '') callback();
		logAllCategories();
	}
	function logAllCategories() {
		/*console.log('----------');
		$('section.category').each(function() {
			console.log('category: ' + $(this).attr('id'));
			$(this).find('article.card').each(function() {
				console.log('card: ' + $(this).attr('id'));
			});
		});*/
	}
	function updateCategoriesIds(category_id, callback = '') {
		for (let i = category_id; i < categoryList.categories.length; i++) {
			let next_category_id = 0;
			next_category_id = parseInt(i) + 1;
			next_category_id = next_category_id.toString().replace(/^0+/, '');
			$(`#category_${next_category_id}`).attr('id', `category_${i}`);
			for (let j = 0; j < categoryList.categories[i].cards.length; j++) {
				$(`#category-${next_category_id}_card-${j}`).attr('id', `category-${i}_card-${j}`);
			}
		}
		if (callback != '') callback();
	}
	function updateCardsIds(category_id, card_id, callback = '') {
		for (let i = card_id; i < categoryList.categories[category_id].cards.length; i++) {
			let next_card_id = 0;
			next_card_id = parseInt(i) + 1;
			next_card_id = next_card_id.toString().replace(/^0+/, '');
			$(`#category-${category_id}_card-${next_card_id}`).attr('id', `category-${category_id}_card-${i}`);
		}
		if (callback != '') callback();
	}
	/*Arguments:
	category_id: the id of the category you want to move
	dest_category_id: the id of the category you want to move next to
	beforeafter: pass 0 if you want to place the category before the dest_category, or 1 if you want to place it after
	given this explanation, the moveCard is intuitive enough.
	Examples for dummies:	moveCategory(0, 0, 2);	moveCard(0, 0, 1, 2);
	*/
	function moveCategory(category_id, beforeorafter, dest_category_id) {
		//Save in the defaultSortList
		$(`#category_${category_id}`).fadeOut(transitionSpeedMedium, function() {
			let html = $(`#category_${category_id}`).prop('outerHTML');
			$(`#category_${category_id}`).remove();
			if (!beforeorafter) $(`#category_${dest_category_id}`).before(html);
			else $(`#category_${dest_category_id}`).after(html);
			$(`#category_${category_id}`).fadeIn(transitionSpeedMedium);
		});
	}
	function moveCard(category_id, card_id, beforeorafter, dest_card_id) {
		//Save in the defaultSortList
		$(`#category-${category_id}_card-${card_id}`).fadeOut(transitionSpeedMedium, function() {
			let html = $(`#category-${category_id}_card-${card_id}`).prop('outerHTML');
			$(`#category-${category_id}_card-${card_id}`).remove();
			if (!beforeorafter) $(`#category-${category_id}_card-${dest_card_id}`).before(html);
			else $(`#category-${category_id}_card-${dest_card_id}`).after(html);
			$(`#category-${category_id}_card-${card_id}`).fadeIn(transitionSpeedMedium);
		});
	}
	//-----
	//Animate functions
	function slideRight(elementIdentifier, duration, callback = '') {
		duration *= 2;
		$(`${elementIdentifier}`).finish();
		let elementWidth = $(`${elementIdentifier}`).prop('clientWidth');
		$(`${elementIdentifier}`).each(function() {
			$(this).width(0+"px");
			$(this).show();
			$(this).animate({
				width: elementWidth+"px",
			}, duration, 'swing', function () { 
				$(this).width(elementWidth+"px");
				if (callback != '') callback();
			});
		});
		updateTheme() 
	}
	function slideLeft(elementIdentifier, duration, callback = '') {
		duration *= 2;
		$(`${elementIdentifier}`).finish();
		let elementWidth = $(`${elementIdentifier}`).prop('clientWidth');
		$(`${elementIdentifier}`).each(function() {
			$(this).animate({
				width: 0+"px",
			}, duration, 'swing', function () { 
				$(this).width(0+"px");
				$(this).hide();
				if (callback != '') callback();
			});
		});
		updateTheme() 
	}
	//-----
	//Filter functions
	let filteredCategoryList = [];
	let filtersFirstOption = $('#c-filter-container').find('ul.dropdown-content li:eq(1)');
	let filterSelectedUrgencies = [];	
	$('a#open-manage-filtersort').click(function(e) {
        e.preventDefault();//stops page refresh since button is an A
        $("#manage-filtersort").fadeIn();
        $("#manage-filtersort .blackout").css("overflow-y", "scroll");
	});
	$('#manage-filtersort article.panel').click(function() {
		$('#c-filter').parent().find('ul.dropdown-content').fadeOut();
		$('#c-sortby').parent().find('ul.dropdown-content').fadeOut();
	});
	filtersFirstOption.on("click", function() {
		let filterVal = $('#c-filter').val();
		if ($.inArray("1", filterVal) == '-1') {
			$('#manage-filtersort #manage_urgency').slideUp();
			$(".urgency").each(function () {
				$(this).prop('checked', false);
			});
			let index = filterVal.indexOf('1');
			filterVal = filterVal.splice(index, 0);
			$("#c-filter").val(filterVal);
			$("#c-filter").change();
		}
		else {
			$('#manage-filtersort #manage_urgency').slideDown();
			updateUrgencyFilter();

		}
    });
	$('#manage-filtersort article.panel').on('click', 'input:checkbox', function() {
		updateUrgencyFilter();
	});
	function updateUrgencyFilter() {
		filterSelectedUrgencies = [];
		$(".urgency").each(function () {
			if ($(this).is(":checked")) filterSelectedUrgencies.push(parseInt($(this).attr('id')));
		});
		let filterVal = $("#c-filter").val();
		if ($.inArray("1", filterVal) == '-1') {	
			filterVal.push('1');
			$("#c-filter").val(filterVal);
		}
        $('#c-filter').change();
	}
	/*$('#c-filter').val()
	1. Filter by Urgency
	2. Filter by Due date
	3. Filter by starting date
	4. Open the Urgency Selector*/
	$('#c-filter').change(function() {
		filteredCategoryList = [];
		let cardsToHide = [];
		let filterVal = $(this).val();
		if (filterVal.length > 0) {
			for (let i = 0; i < categoryList.categories.length; i++) {
				//Filter all the cards, adding the ones that should stay visible to filteredCategoryList
				//this variable, filteredCategoryList will be used in the later sorting function so that the browser only sorts the visible cards
				let category_id = categoryList.categories[i].id;
				for (let j = 0; j < categoryList.categories[i].cards.length; j++) {
					let card_id = categoryList.categories[i].cards[j].id;
					let card = categoryList.categories[category_id].cards[card_id];
					//If the filter isn't active or if it's active and the card qualifies as one that should stay visible, advance to the next filter
					if ($.inArray("1", filterVal) == '-1' || ($.inArray("1", filterVal) != '-1' && $.inArray(parseInt(card.importance), filterSelectedUrgencies) != '-1')) {
						if ($.inArray("2", filterVal) == '-1' || ($.inArray("2", filterVal) != '-1' && moment(card.end, 'DD-MM-YYYY').isAfter(moment().startOf('week')) && moment(card.end, 'DD-MM-YYYY').isBefore(moment().endOf('week')))) {
							if ($.inArray("3", filterVal) == '-1' || ($.inArray("3", filterVal) != '-1' && moment(card.start, 'DD-MM-YYYY').isAfter(moment().startOf('week')) && moment(card.start, 'DD-MM-YYYY').isBefore(moment().endOf('week')))) {
								if ($.inArray("4", filterVal) == '-1' || ($.inArray("4", filterVal) != '-1' && card.complete)) {
									//If the card has passed all the filters, qualifying as a card that should stay visible
									//If it doesn't have the category, add it
									let containsCategory = false;
									for (let k = 0; k < filteredCategoryList.length; k++) {
										if (filteredCategoryList[k].category_id == category_id) containsCategory = true;
									}
									if (!containsCategory) filteredCategoryList.push({'category_id': category_id, 'cards': []});
									//Add the card to the existing category
									for (let k = 0; k < filteredCategoryList.length; k++) {
										if (filteredCategoryList[k].category_id == category_id) filteredCategoryList[k].cards.push(card_id);
									}
								}
							}
						}
					}
				}
				//After setting the filteredCategoryList, change the HTML accordingly
				//Hide all categories without visible cards, and show the ones that have cards
				let containsCategory = false;
				for (let k = 0; k < filteredCategoryList.length; k++) {
					if (filteredCategoryList[k].category_id == category_id) containsCategory = true;
				}
				if (!containsCategory) $(`#category_${category_id}`).fadeOut(transitionSpeedMedium);
					else {
					$(`#category_${category_id}`).fadeIn(transitionSpeedMedium)
					//If the category has visible cards, hide the cards that should be hidden
					for (let j = 0; j < categoryList.categories[i].cards.length; j++) {
						let card_id = categoryList.categories[i].cards[j].id;
						//If the categoryList's card is not within the array of cards that should be visible, mark it
						let containsCard = false;
						for (let k = 0; k < filteredCategoryList.length; k++) {
							if (filteredCategoryList[k].category_id == category_id) {
								for (let l = 0; l < filteredCategoryList[k].cards.length; l++) {
									if (filteredCategoryList[k].cards[l] == card_id) {
										containsCard = true;
									}
								}
							}
						}
						if (!containsCard) cardsToHide.push(`#category-${category_id}_card-${card_id}`);
						else $(`#category-${category_id}_card-${card_id}`).fadeIn(transitionSpeedMedium);
					}
				}
			}
			for (let i = 0; i < cardsToHide.length; i++) {
				$(`${cardsToHide[i]}`).fadeOut(transitionSpeedMedium);
			}
		}
		else {
			$('section.category').fadeIn(transitionSpeedMedium);
			$('article.card').fadeIn(transitionSpeedMedium);
		}
		$('#c-sortby').change();
	});
	//Checks whether the sortedVal has changed, if it hasn't then there's no need to sort again
	let previousSortedVal;
	//When sorting, checks whether the card is in its correct position, if it is don't fade it Out and In
	let previousSortedCategoryList = [];
	$('#c-sortby').change(function() {	
		let sortedVal = $(this).val();
		if (previousSortedVal === sortedVal) return;
		else previousSortedVal = sortedVal;
		if (sortedVal != 0) $("#c-sortby option:first").text('No sorting');
		else $("#c-sortby option:first").text('Sort by...');
		$('#c-sortby').material_select();
		let sortedCategoryList = [];
		if (!filteredCategoryList.length) {
			for (let i = 0; i < categoryList.categories.length; i++) {
				filteredCategoryList.push({'category_id': categoryList.categories[i].id, 'cards': []});
				for (let j = 0; j < categoryList.categories[i].cards.length; j++) {
					filteredCategoryList[i].cards.push(categoryList.categories[i].cards[j].id);
				}
			}
		}
		switch (sortedVal) {
			case '0':
				for (let i = 0; i < filteredCategoryList.length; i++) {
					sortedCategoryList.push({'category_id': filteredCategoryList[i].category_id, 'cards': []});
					for (let j = 0; j < filteredCategoryList[i].cards.length; j++) {
						sortedCategoryList[i].cards.push(filteredCategoryList[i].cards[j]);
					}
				}
			break;
			case '1':
				for (let i = 0; i < urgencyList.importances.length; i++) {
					let importance_id = urgencyList.importances[i].id;
					for (let j = 0; j < filteredCategoryList.length; j++) {
						let category_id = filteredCategoryList[j].category_id;
						for (let k = 0; k < filteredCategoryList[j].cards.length; k++) {
							let card_id = filteredCategoryList[j].cards[k];
							if (categoryList.categories[category_id].cards[card_id].importance == importance_id) {
								//If it doesn't have the category, add it
								hasCategory = false;
								for (let l = 0; l < sortedCategoryList.length; l++) {
									if (category_id == sortedCategoryList[l].category_id) hasCategory = true;
								}
								if (!hasCategory) sortedCategoryList.push({'category_id': filteredCategoryList[j].category_id, 'cards': []});
								//Add the card to the existing category
								for (let l = 0; l < sortedCategoryList.length; l++) {
									if (category_id == sortedCategoryList[l].category_id) {
										sortedCategoryList[l].cards.push(card_id);
									}
								}
							}
						}
					}
				}
			break;
			case '2':
				for (let j = 0; j < filteredCategoryList.length; j++) {
					let category_id = filteredCategoryList[j].category_id;
					for (let k = 0; k < filteredCategoryList[j].cards.length; k++) {
						let card_id = filteredCategoryList[j].cards[k];
						let end_date = moment(categoryList.categories[category_id].cards[card_id].end, 'DD-MM-YYYY');
						if (k == 0) {
							//Add the unexisting category
							sortedCategoryList.push({'category_id': filteredCategoryList[j].category_id, 'cards': []});
							//Add the card to the existing category
							for (let l = 0; l < sortedCategoryList.length; l++) {
								if (category_id == sortedCategoryList[l].category_id) {
									sortedCategoryList[l].cards.push(card_id);
								}
							}
						}
						else {
							let prev_card_id = filteredCategoryList[j].cards[k - 1];
							let current_cap = moment(categoryList.categories[category_id].cards[prev_card_id].end, 'DD-MM-YYYY');
							//Add the card to the existing category
							for (let l = 0; l < sortedCategoryList.length; l++) {
								if (category_id == sortedCategoryList[l].category_id) {
									if (current_cap.isAfter(end_date)) sortedCategoryList[l].cards.splice(0, 0, card_id);
									else sortedCategoryList[l].cards.push(card_id);
								}
							}
						}
					}
				}
		}
		for (let i = 0; i < sortedCategoryList.length; i++) {
			let category_id = sortedCategoryList[i].category_id;
			for (let j = 0; j < sortedCategoryList[i].cards.length; j++) {
				let card_id = sortedCategoryList[i].cards[j];
				if (previousSortedCategoryList != undefined && previousSortedCategoryList.length > i && previousSortedCategoryList[i].cards.length > j && previousSortedCategoryList[i].cards[j] == card_id) {
					//console.log(previousSortedCategoryList[i].cards[j] + ' is equal to ' + card_id);
					continue;
				}
				if (j == 0) {
					$(`#category-${category_id}_card-${card_id}`).fadeOut(transitionSpeedMedium, function() {
						let html = $(`#category-${category_id}_card-${card_id}`).prop('outerHTML');
						$(`#category-${category_id}_card-${card_id}`).remove();
						$(`#category_${category_id}`).children('section.droppable').prepend(html);
						$(`#category-${category_id}_card-${card_id}`).fadeIn(transitionSpeedMedium);
					});
				}
				else {
					let prev_card_id = sortedCategoryList[i].cards[j - 1];
					$(`#category-${category_id}_card-${card_id}`).fadeOut(transitionSpeedMedium, function() {
						let html = $(`#category-${category_id}_card-${card_id}`).prop('outerHTML');
						$(`#category-${category_id}_card-${card_id}`).remove();
						$(`#category-${category_id}_card-${prev_card_id}`).after(html);
						$(`#category-${category_id}_card-${card_id}`).fadeIn(transitionSpeedMedium);
					});
				}
			}
		}
		previousSortedCategoryList = sortedCategoryList;
	});
	//-----
	//Load
	urgencyList.Load(function loadBoth() {
		categoryList.Load(function loadBothAgain() {
			defaultSortList.Load(function loadBothYetAgain() {
				userList.Load(function loadBothStillYetAgain() {
					$('#manage-filtersort #manage_urgency').hide();
					$('#c-filter').val([]);
					filterSelectedUrgencies = [];
					loadAllCategories();
				});
			});
		});
	});
});
