'use strict'

let postItems
let updateItem
let deleteItem
let getItems
let getItem
let updateItemClick

$('document').ready(() => {

  const hasClass = (el, selector) => {
    let className = " " + selector + " "
    return (" " + el.className + " ").replace(/[\n\t]/g, " ").indexOf(className) > -1
  }

  // RESTFUL API AJAX REQUESTS:
  postItems = (formData) => {
    $.ajax({
      url: 'http://localhost:3000/api/items',
      dataType: 'json',
      type: 'POST',
      method: "POST",
      data: formData,
      processData: true,
      success: null,
      error: (jqXHR, textStatus, errorThrown) => {
        console.log(`jqXHR: ${jqXHR.message}`);
        console.log(`textStatus: ${textStatus}`);
        console.log(`errorThrown: ${errorThrown}`);
      },
      complete: () => { getItems(); }
    })
  }

  updateItem = (formData, id) => {
    $.ajax({
      url: 'http://localhost:3000/api/item?id=' + id,
      dataType: 'json',
      type: 'PUT',
      method: "PUT",
      data: formData,
      processData: true,
      success: null,
      error: (jqXHR, textStatus, errorThrown) => {
        console.log(`jqXHR: ${jqXHR.message}`);
        console.log(`textStatus: ${textStatus}`);
        console.log(`errorThrown: ${errorThrown}`);
      },
      complete: () => { getItems(); }
    })
  }

  deleteItem = (id) => {
    $.ajax({
      url: 'http://localhost:3000/api/items?id=' + id,
      type: 'DELETE',
      contentType: "application/json",
      error: (jqXHR, textStatus, errorThrown) => {
        console.log(`jqXHR: ${jqXHR.message}`);
        console.log(`textStatus: ${textStatus}`);
        console.log(`errorThrown: ${errorThrown}`);
      }
    })
  }

  getItems = () => {
    $.ajax({
      url: 'http://localhost:3000/api/items',
      dataType: 'json',
      type: 'GET',
      contentType: 'application/json; charset=UTF-8',
      success: (json) => {
        document.getElementById('items-cards-holder').innerHTML = ''
        for (let i = 0; i < json.length; i++) {
          document.getElementById('items-cards-holder').innerHTML += `<a href="#" class="list-group-item list-group-item-action flex-column align-items-start" onclick="updateItemClick('${json[i]._id}'); return false">` +
                                                                       '<div class="d-flex w-100 justify-content-between">' +
                                                                         `<h5 class="mb-1">${json[i].title}</h5>` +
                                                                         '<div>' +
                                                                           `<button class="btn btn-danger btn-sm btn-floating float-right ml-1" id="delete-item" onclick="deleteItem('${json[i]._id}'); getItems(); return false" aria-label="Close">` +
                                                                             '<span aria-hidden="true">&times;</span>' +
                                                                           '</button>' +
                                                                           // `<button class="btn btn-warning btn-sm btn-floating float-right" id="delete-item" onclick="updateItemClick('${json[i]._id}'); getItems(); return false" aria-label="Close">Edit</button>` +
                                                                         '</div>' +
                                                                       '</div>'+
                                                                       `<p class="mb-1">${json[i].description}</p>` +
                                                                     '</a>'

        }
      },
      error: (jqXHR, textStatus, errorThrown) => {
        console.log(`jqXHR: ${jqXHR.message}`);
        console.log(`textStatus: ${textStatus}`);
        console.log(`errorThrown: ${errorThrown}`);
      }
    })
  }

  getItem = (id, callback) => {
    $.ajax({
      url: 'http://localhost:3000/api/item?id=' + id,
      dataType: 'json',
      type: 'GET',
      contentType: 'application/json; charset=UTF-8',
      success: (json) => {
        callback(json)
      },
      error: (jqXHR, textStatus, errorThrown) => {
        console.log(`jqXHR: ${jqXHR.message}`);
        console.log(`textStatus: ${textStatus}`);
        console.log(`errorThrown: ${errorThrown}`);
      }
    })
  }
  // End of RESTFUL API AJAX REQUESTS.

  // INITIALIZATION:
  $('#add-item-window').slideUp()
  document.getElementById('method').value = 'POST'
  getItems()

  // Initialize the Pickadate.js plugin:
  $('#datepicker').pickadate().get()
  $('#timepicker').pickatime().get()

  // HEADER BUTTON "New Item" DROP-DOWN ('click' EVENT):
  document.getElementById('btn-new-item').addEventListener('click', () => {
    if (!hasClass(document.getElementById('btn-new-item'), 'active')) {
      document.getElementById('btn-new-item').classList.add('active')
      document.getElementById('add-item-window').style.display = ''
      document.getElementById('add-item-window').style.visibility = 'visible'
      $('#add-item-window').slideDown()
    } else {
      document.getElementById('btn-new-item').classList.remove('active')
      $('#add-item-window').slideUp()
    }
  })
  // End of BUTTON "New Item".

  // UPDATE ITEM ON ITEM CLICK (It puts the item data on the form):
  updateItemClick = (id) => {
    // Change the hidden "method" input from the "New Item" form:
    // When the user clicks "SAVE", it checks the hidden "method" input -> 'PUT' / 'POST').
    document.getElementById('method').value = 'PUT'

    getItem(id, (json) => {
      // Insert the item data on the "New Item" form:
      document.getElementById('title').value = json[0].title
      document.getElementById('itemId').value = id // Store the item id on an hidden input on the "New Item" form.
      document.getElementById('priority').options.selectedIndex = json[0].priority - 1
      document.getElementById('description').value = json[0].description
      document.getElementById('datepicker').value = json[0].dueDate
      document.getElementById('timepicker').value = json[0].dueTime

      // Show the "New Item" form:
      document.getElementById('btn-new-item').classList.add('active')
      document.getElementById('add-item-window').style.display = ''
      document.getElementById('add-item-window').style.visibility = 'visible'
      $('#add-item-window').slideDown()
    })
  }

  // BUTTON "Save" ON THE "New Item" FORM ('click' EVENT):
  // It is used to POST and PUT items (that's why there are 2 chained if statements)
  document.getElementById('btn-save-item').addEventListener('click', () => {
    if (document.getElementById('title').value === '')
      return null
    else {
      let formData = {
        "title": document.getElementById('title').value,
        "priority": (document.getElementById('priority').options.selectedIndex + 1).toString(),
        "description": document.getElementById('description').value,
        "dueDate": document.getElementById('datepicker').value,
        "dueTime": document.getElementById('timepicker').value
      }

      if (document.getElementById('method').value === 'PUT') {
        updateItem(formData, document.getElementById('itemId').value)
        // Make the hidden input (AJAX method type) POST again:
        document.getElementById('method').value = 'POST'
      } else if (document.getElementById('method').value === 'POST') {
        postItems(formData)
      }

      // HIDE THE "New Item" FORM:
      document.getElementById('btn-new-item').classList.remove('active')
      $('#add-item-window').slideUp()

      // Clean form values:
      document.getElementById('title').value = ''
      document.getElementById('title').value = ''
      document.getElementById('priority').options.selectedIndex = 2
      document.getElementById('description').value = ''
      document.getElementById('datepicker').value = ''
      document.getElementById('timepicker').value = ''
    }
  })
  // End of BUTTON "Save".
})
