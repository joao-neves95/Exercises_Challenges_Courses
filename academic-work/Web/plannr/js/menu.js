$(function(){
	let category_id = -1;
	let card_id = -1;
	$('section#categories').on('mousedown', '#category-name, article.card', function(event) {
		event.stopPropagation();
        if(event.which == 3) {
			if ($(this).attr('id') == 'category-name') {
				category_id = getCategory_id($(this));
				card_id = -1;
				let containsCategory = false;
				for (let i = 0; i < categoryList.categories.length; i++) {
						if (categoryList.categories[i].id == category_id) containsCategory = true;
				}
				if (!containsCategory) {
					console.log('categoryList does not contain id ' + category_id);
					category_id = -1;
					return;
				}
			}
			else {
				category_id = getCategory_id($(this));
				card_id = $(this).attr('id').split('-')[2];
				let containsCard = false;
				for (let i = 0; i < categoryList.categories[category_id].cards.length; i++) {
					if (categoryList.categories[category_id].cards[i].id == card_id) containsCard = true;
				}
				if (!containsCard) {
					console.log('categoryList id ' + category_id + ' does not contain card id ' + card_id);
					category_id = -1;
					card_id = -1;
					return;
				}
			}
			if (card_id != -1) {
				if (categoryList.categories[category_id].cards[card_id].complete) $("#categoryMenu").find('#menu-complete').prop('innerHTML', 'Incomplete');
				else $("#categoryMenu").find('#menu-complete').prop('innerHTML', 'Complete');
				$('#menu-complete').removeClass('hidden');
			}
			else $('#menu-complete').addClass('hidden');
			$('#categoryMenu').slideDown();
            $('#categoryMenu').css('top', event.pageY-5);
            $('#categoryMenu').css('left', event.pageX-5);
        }
    });

    $("#categoryMenu").on("mouseleave", function(){
        $(this).slideUp();
    });
	
	$("#categoryMenu").on("click", "#menu-delete", function(){
        $("#categoryMenu").slideUp();
		var result;
		if (card_id == -1) result = confirm("Are you sure you want to delete this category?");
		else result = confirm("Are you sure you want to delete this card?");
		if (result) {
			if (card_id == -1) removeCategory(category_id, stopEditing);
			else removeCard(category_id, card_id, stopEditing);
		}
		category_id = -1;
		card_id = -1;
    });
	
	$("#categoryMenu").on("click", "#menu-complete", function(){
        $("#categoryMenu").slideUp();
		categoryList.categories[category_id].cards[card_id].complete = !categoryList.categories[category_id].cards[card_id].complete;
		category_id = -1;
		card_id = -1;
    });
});