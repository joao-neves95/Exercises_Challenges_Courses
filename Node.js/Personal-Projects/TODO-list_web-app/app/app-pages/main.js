'use strict'
document.getElementById('add-item-window').style.visibility = 'hidden'
$('#add-item-window').slideUp()

$('document').ready(() => {

  const hasClass = (el, selector) => {
    let className = " " + selector + " "

    return (" " + el.className + " ").replace(/[\n\t]/g, " ").indexOf(className) > -1
  }

  const postItems = (formData) => {
    $.ajax({
      url: 'http://localhost:3000/api/items',
      dataType: 'json',
      type: 'POST',
      // contentType: 'application/json; charset=UTF-8',
      data: formData,
      processData: true,
      success: 0,
      error: (jqXhr, textStatus, err) => {console.log(err)}
    })
  }

  const getItems = () => {
    $.ajax({
      url: 'http://localhost:3000/api/items',
      dataType: 'json',
      type: 'GET',
      contentType: 'application/json; charset=UTF-8',
      success: (json) => {
        document.getElementById('items-cards-holder').innerHTML = ''
        for (let i = 0; i < json.length; i++) {
          document.getElementById('items-cards-holder').innerHTML += '<div class="col-sm-12 col-md-12 card-container">'+
                                                                      '<article class="card">'+
                                                                        '<div class="card-body">'+
                                                                          '<h4 class="card-title">' + json[i].title + '</h4>'+
                                                                          '<p class="card-text">' + json[i].description +'</p>'+
                                                                        '</div>'+
                                                                      '</article>'+
                                                                    '</div>'
        }
      },
      error: (jqXhr, textStatus, err) => {
        console.log(err)
      }
    })
  }

  getItems()
  // Initialize the Pickadate.js plugin:
  $('#datepicker').pickadate().get()
  $('#timepicker').pickatime().get()

  // HEADER BUTTON "New Item" ('click' EVENT):
  document.getElementById('btn-new-item').addEventListener('click', () => {
    if (!hasClass(document.getElementById('btn-new-item'), 'active')) {
      document.getElementById('btn-new-item').classList.add('active')
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

    // Hide the "New Item" form:
    document.getElementById('btn-new-item').classList.remove('active')
    $('#add-item-window').slideUp()
  })
})
