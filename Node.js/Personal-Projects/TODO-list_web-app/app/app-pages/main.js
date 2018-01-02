'use strict'

let postItems
let deleteItem
let getItems

$('document').ready(() => {

  const hasClass = (el, selector) => {
    let className = " " + selector + " "
    return (" " + el.className + " ").replace(/[\n\t]/g, " ").indexOf(className) > -1
  }

  postItems = (formData) => {
    $.ajax({
      url: 'http://localhost:3000/api/items',
      dataType: 'json',
      type: 'POST',
      data: formData,
      processData: true,
      success: 0,
      error: (jqXHR, textStatus, errorThrown) => {
        console.log('jqXHR:');
        console.log(jqXHR);
        console.log('textStatus:');
        console.log(textStatus);
        console.log('errorThrown:');
        console.log(errorThrown);
      }
    })
  }

  deleteItem = (id) => {
    $.ajax({
      url: 'http://localhost:3000/api/items?id=' + id,
      type: 'DELETE',
      contentType: "application/json",
      error: (jqXHR, textStatus, errorThrown) => {
        console.log('jqXHR:');
        console.log(jqXHR);
        console.log('textStatus:');
        console.log(textStatus);
        console.log('errorThrown:');
        console.log(errorThrown);
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
          document.getElementById('items-cards-holder').innerHTML += '<a href="#" class="list-group-item list-group-item-action flex-column align-items-start hoverable">' +
                                                                       '<div class="d-flex w-100 justify-content-between">' +
                                                                         '<h5 class="mb-1">' + json[i].title+ '</h5>' +
                                                                         '<button class="btn btn-danger btn-floating" id="delete-item" onclick="deleteItem(' + '\'' + json[i]._id + '\''+ '); getItems(); return false">x</button>' +
                                                                       '</div>'+
                                                                         '<p class="mb-1">' + json[i].description + '</p>' +
                                                                     '</a>'
        }
      },
      error: (jqXHR, textStatus, errorThrown) => {
        console.log('jqXHR:');
        console.log(jqXHR);
        console.log('textStatus:');
        console.log(textStatus);
        console.log('errorThrown:');
        console.log(errorThrown);
      }
    })
  }

  $('#add-item-window').slideUp()
  getItems()

  // Initialize the Pickadate.js plugin:
  $('#datepicker').pickadate().get()
  $('#timepicker').pickatime().get()

  // HEADER BUTTON "New Item" ('click' EVENT):
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

  // BUTTON "Save" on the "New Item" form ('click' EVENT):
  document.getElementById('btn-save-item').addEventListener('click', () => {
    let formData = {
      "title": document.getElementById('title').value,
      "priority": (document.getElementById('priority').options.selectedIndex + 1).toString(),
      "description": document.getElementById('description').value,
      "dueDate": document.getElementById('datepicker').value,
      "dueTime": document.getElementById('timepicker').value
    }
    postItems(formData)
    getItems()
    if (document.getElementById('title').value === '')
      return null
    else {
      // Hide the "New Item" form:
      document.getElementById('btn-new-item').classList.remove('active')
      $('#add-item-window').slideUp()

      // Clean form values:
      document.getElementById('title').value = ''
      document.getElementById('priority').options.selectedIndex = 2
      document.getElementById('description').value = ''
      document.getElementById('datepicker').value = ''
      document.getElementById('timepicker').value = ''
    }
  })
})
