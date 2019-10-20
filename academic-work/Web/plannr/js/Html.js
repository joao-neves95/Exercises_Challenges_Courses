// HTML TEMPLATES.

// DragAndDrop.js
const tempCardHtml = () => {
	return `<article class="col x12 m12 l12 hoverable droppable temp-card" id="tempCard">
          </article>`
}

const tempCategoryHtml = () => {
	return `<article class="col x12 m12 l12 category card-category flex-category hoverable droppable temp-category" id="tempCategory">
          </article>`
}

// CardsModal.js
const cardsModalHTML = () => {
	return `<div class="modal-content">
            <form class="row">
              <input type="hidden" id="m-categoryIdx">
              <input type="hidden" id="m-cardIdx">
              <div class="input-field col m12 l12">
                <p>Title</p>
                <input id="m-cardTitle" type="text" placeholder="Title" class="disabled">
              </div>
              <div class="input-field col m12 l12">
                <p id="desc-label">Description</p>
                <textarea id="m-cardDesc" class="materialize-textarea disabled"></textarea>
			        </div>
			        <div class="input-field col m12 l12">
				        <div id="m-cardAssigned" class="info editable-select">
					        Assigned to <i class="tiny material-icons center-y" style="position:relative !important;">add_circle</i>
				        </div>
				        <div id="m-assignedUsers">
					      </div>
						  </div>
						  <div class="input-field col m6 l6">
							  <p>From</p>
							  <input id="m-cardStart" type="text" class="datepicker-start disabled">
						  </div>
						  <div class="input-field col m6 l6">
							  <p>To</p>
						  	<input id="m-cardEnd" type="text" class="datepicker-end disabled">
					  	</div>
						  <div class="input-field col m12 l12">
							  <textarea id="m-newComment" class="materialize-textarea" placeholder="Add a comment..."></textarea>
					  	</div>
						  <a href="#" id="m-saveComment" class="btn-floating waves-effect waves-light green">
							  <i class="material-icons">save</i>
						  </a>
					  </form>
						<label for="m-cardComments">
						  <i class="material-icons comments-label">chat</i> Comments
						</label>
					  <ul id="m-cardComments" class="collection">
					  </ul>
				  </div>`
}

const assignedUserHTML = (categoryId, cardId, userId, userName) => {
	return `<div id="category-${categoryId}_card-${cardId}user-${userId}" class="chip">
	          ${userName}<i class="close material-icons m-assignmentDelete">close</i>
	        </div>`
}

const commentHTML = (categoryIdx, cardIdx, commentIdx, comment) => {
	return `<li id="category-${categoryIdx}_card-${cardIdx}_comment-${commentIdx}" class="collection-item hoverable">
            <textarea class="materialize-textarea comment disabled">${comment}</textarea><i class="material-icons small delete-comment">delete_forever</i>
          </li>`
}
// End of CardsModal.js

// For debugging.
const categoryHtml = () => {
	return `<section class="category col x12 s12 m5 l2">
            <div class="card teal lighten-2 col x12 m12 l12">
              <div class="card-title white-text">
                <h2 class="category-title">To-do list</h2>
              </div>
            </div>
            <div class="center-align">
              <a id="addcard" class="btn-floating btn-medium waves-effect waves-light red tooltipped" data-position="right" data-delay="50"
                data-tooltip="Add New Card">
                <i class="small material-icons">add</i>
              </a>
            </div>
            <section class="droppable col x12 m12 l12">
            </section>
          </section>`
}

// Cards.js
//New category
////New category
const newCategory = () => {
	return `<section id="category_new" class="category flex-category">
				<div class="card teal lighten-2 col s12 m12 l12 card-category">
					<div class="card-title white-text">
						<h2 id="new-category-name" class="category-title editable"></h2>
					</div>
				</div>
			</section>`;
}
////Category
const category = (category_id, hidden = false) => {
	return `<section id="category_${categoryList.categories[category_id].id}" class="draggable category card-category flex-category" ${(!hidden ? '' : 'style="display:none;"')}>
                <div class="card teal lighten-2 col s12 m12 l12 xl12 hoverable">
                  ${categoryTitle(category_id)}
                </div>
                <div class="center-align">
                    <a id="addcard" class="btn-floating btn-medium waves-effect waves-light red tooltipped" data-position="right" data-delay="50"
                    data-tooltip="Add New Card">
                    <i class="small material-icons">add</i>
                    </a>
                </div>
                <section class="droppable col s12 m12 l12 xl12">
				</section>
			</section>`;
}
const categoryTitle = (category_id, hidden = false) => {
	return `<div class="card-title white-text">
                    <h2 id="category-name" class="category-title truncate editable" ${(!hidden ? '' : 'style="display:none;"')}>${categoryList.categories[category_id].name}</h2>
                  </div>`;
}
////New card
const newCard = () => {
	return `<article id="card_new" class="draggable card teal darken-1 col xl12 s12 m12 l12 hoverable" style="display:none">
				<div class="card-content white-text">
					<p id="new-card-title" class="text-left editable"></p>
				</div>
			</article>`;
}
////Card's attributes
const cardImportance = (category_id, card_id, hidden = false) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	$(`#category-${category_id}_card-${card_id}`).attr('style', `background-color: ${urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color} !important;`)
	$(`#category-${category_id}_card-${card_id}`).find('p#title').attr('style', `color: ${textColor} !important`);
	$(`#category-${category_id}_card-${card_id}`).find('p#desc').attr('style', `color: ${textColor} !important`);
	$(`#category-${category_id}_card-${card_id}`).find('span#assigned').attr('style', `color: ${textColor} !important`);
	$(`#category-${category_id}_card-${card_id}`).find('span#start-end').attr('style', `color: ${textColor} !important`);
	return `<span id="importance_${categoryList.categories[category_id].cards[card_id].importance}" class="info icon truncate editable-select" style="${(!hidden ? '' : 'display:none;')} color:${textColor} !important;"><i class="tiny material-icons">warning</i>${urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].name}</span>`;
}
const cardTitle = (category_id, card_id, hidden = false) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	return `<p id="title" class="text-left truncate editable" style="${(!hidden ? '' : 'display:none;')} color:${textColor} !important;"> ${categoryList.categories[category_id].cards[card_id].title}</p>`;
}
const cardDesc = (category_id, card_id, hidden = false) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	if (categoryList.categories[category_id].cards[card_id].desc.length > 0) return `<p id="desc" class="truncate editable" style="${(!hidden ? '' : 'display:none;')} color:${textColor} !important;">${categoryList.categories[category_id].cards[card_id].desc}</p>`;
	else return `<p id="desc" class="truncate editable no-attribute" style="${(!hidden ? '' : 'display:none;')} color:${textColor} !important;">There is no description.</p>`;
}
const cardAssigned = (category_id, card_id, hidden = false) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	let toReturn = `<div class="row" style="${(!hidden ? '' : 'display:none;')}">
						<span id="assigned" class="info editable-select" style="color:${textColor} !important;">Assigned to <i class="tiny material-icons center-y" style="position:relative !important;">add_circle</i></span>
						<br />`;
	for (let k = 0; k < categoryList.categories[category_id].cards[card_id].assigned.length; k++) {
		if (k > 0) {
			if (k < categoryList.categories[category_id].cards[card_id].assigned.length - 1) toReturn += `<span class="chip-connector">,</span>`;
			else if (k == categoryList.categories[category_id].cards[card_id].assigned.length - 1) toReturn += `<span class="chip-connector">&nbsp;and&nbsp;</span>`;
		}
		toReturn += `<div id="assignment_${categoryList.categories[category_id].cards[card_id].assigned[k]}" class="chip">${userList.users[categoryList.categories[category_id].cards[card_id].assigned[k]].Name}
						<i id="assignment_delete" class="close material-icons">close</i>
					</div>`;
	}
	toReturn += `</div>`;
	return toReturn;
}
const cardStartEnd = (category_id, card_id, hidden = false, importanceColor = '') => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	let toReturn = `<div class="row" style="${(!hidden ? '' : 'display:none;')}">`;
	if (categoryList.categories[category_id].cards[card_id].start.length == 0 || categoryList.categories[category_id].cards[card_id].end.length == 0) toReturn = `<span id="start-end" class="no-attribute">There's no specified date.</span>`;
	else {
		let start = moment(categoryList.categories[category_id].cards[card_id].start, 'DD-MM-YYYY'),
			end = moment(categoryList.categories[category_id].cards[card_id].end, 'DD-MM-YYYY'),
			remainingTime,
			yearDiff = Math.floor(moment.duration(end.diff(moment())).asYears());
		if (yearDiff >= 1) remainingTime = `${yearDiff} years remaining.`;
		else {
			let monthDiff = Math.floor(moment.duration(end.diff(moment())).asMonths());
			if (monthDiff >= 1) remainingTime = `${monthDiff} months remaining.`;
			else {
				let dayDiff = Math.floor(moment.duration(end.diff(moment().subtract(1, 'days'))).asDays());
				if (dayDiff >= 1) remainingTime = `${dayDiff} days remaining.`;
				else if (dayDiff == 0) remainingTime = `Due today.`;
				else remainingTime = `${(dayDiff*-1)} days late.`
			}
		}
		toReturn += `<span id="start-end" class="no-attribute" style="color:${textColor} !important;">${remainingTime}</span>`;
	}
	toReturn += `</div>`;
	return toReturn;
}
////Card
const card = (category_id, card_id, hidden = false) => {
	let importanceColor = urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color;
	return `<article id="category-${category_id}_card-${card_id}" class="draggable card card-category darken-1 col xl12 s12 m12 l12 hoverable" style="background-color:${importanceColor} !important;${(!hidden ? '' : 'display:none;')}">
				<div class="card-content white-text">
					${cardImportance(category_id, card_id)}
					${cardTitle(category_id, card_id)}
					${cardDesc(category_id, card_id)}
					${cardAssigned(category_id, card_id)}
					${cardStartEnd(category_id, card_id)}
				</div>
			</article>`;
}
////Editable types of inputs
const editableInput = (transition, userInputText, userInputPlaceholder, importanceColor = '') => {
	return `<div id="user-form" class="input-field col s12"${(!transition) ? "":'style="display:none;"'}>
				<input id="user-input" type="text" value="${userInputText}" placeholder="${userInputPlaceholder}" class="validate" style="${(importanceColor.length == '' ? '' : 'color: ' + SetTextColor(importanceColor) + ' !important;')}">
				<label id="user-validation" class="validator" for="user-input" data-error="This field can't be empty!" data-sucess="right!"></label>
			</div>`;
}
const editableSelectImportance = (category_id, card_id) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	let toReturn = `<div id="user-form" class="input-field" style="color:${textColor} !important;">
						<select id="user-input"><option value="-1">How urgent is it?
							</option>`;
	for (let i = 0; i < urgencyList.importances.length; i++) {
		toReturn += `<option value="${urgencyList.importances[i].id}">${urgencyList.importances[i].name}</option>`;
	}
	toReturn += `</select>
			</div>`;
	return toReturn;
}
const editableSelectAssigned = (category_id, card_id) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	if (categoryList.categories[category_id].cards[card_id].assigned.length == userList.users.length) return `<div id="user-form" class="input-field"><select disabled id="user-input"><option value="-1">There's noone left to assign!</option></select>`;
	else {
		let toReturn = `<div id="user-form" class="input-field" style="color:${textColor} !important;"><select id="user-input"><option value="-1">Who's encharged?</option>`;
		for (let i = 0; i < userList.users.length; i++) {
			if ($.inArray(userList.users[i].id.toString(), categoryList.categories[category_id].cards[card_id].assigned) != "-1") continue;
			toReturn += `<option value="${userList.users[i].id}">${userList.users[i].Name}</option>`;
		}
		toReturn += `</select>
				</div>`;
		return toReturn;
	}
}
const editableInputsStartEnd = (category_id, card_id) => {
	let textColor = SetTextColor(urgencyList.importances[categoryList.categories[category_id].cards[card_id].importance].color);
	return `<div id="user-form" class="input-field col s6 m6 l6 x6">
							<p class="datepicker-paragraph">From</p>
							<input id="start" type="text" data-value="${start}" class="text-center datepicker" style="color:${textColor} !important;">
						</div>
						<div class="input-field col s6 m6 l6 x6">
							<p class="datepicker-paragraph">to</p>
							<input id="end" type="text" data-value="${end}" class="text-center datepicker" style="color:${textColor} !important;">
						</div>`;
}