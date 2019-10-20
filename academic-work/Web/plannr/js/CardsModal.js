// UTILITY FUNCTIONS:
// It finds the wanted parent node by id.
const getParentNodeIdIncludes = (elem, query) => {
  let that = elem
  while (that && !that.id.includes(query)) {
    that = that.parentNode
  }
  return that
}

const getParentByTag = (elem, tag) => {
  let that = elem
  while (that && that.localName !== tag) {
    that = that.parentNode
  }
  return that
}

// Algorithm to get the id's.
// It returns an array with the id's in order, depending on the element type (category; card; etc...)
const parseIDs = (id) => {
  let split = id.split(/-|_/g)
  let ids = []
  for (let i = 0; i < split.length; i++) {
    let id = parseInt(split[i])
    if (!isNaN(id)) {
      ids.push(id)
    }
  }
  return ids
}
// It gets the category index from any child node of that category.
const getCategoryIdxFromChild = (elem) => {
  return parseIDs(getParentNodeIdIncludes(elem, 'category_').id)[0]
}

// It gets the card index from any child node of that card.
const getCardIdxFromChild = (elem) => {
  return parseIDs(getParentNodeIdIncludes(elem, '_card-').id)[1]
}

const getCatIdxFromModal = () => {
  return parseInt(document.getElementById('m-categoryIdx').value)
}

const getCardIdxFromModal = () => {
  return parseInt(document.getElementById('m-cardIdx').value)
}

// Enable/Disable inputs:
const removeDisabledClass = (elem) => {
  elem.classList.remove('disabled')
}
const addDisabledClass = (elem) => {
  elem.classList.add('disabled')
}

// This adds the card to the JSON file.
const saveCommentOnCard = (categoryIndex, cardIndex, comment) => {
  let newComment = new Comment(0, comment)
  categoryList.categories[categoryIndex].cards[cardIndex].AddComment(newComment)
  categoryList.Save()
}
// End of UTILITY FUNCTIONS.

// CARDS EVENT LISTENERS:
  // Cards DoubleClick:
  // Because the cards are dynamic data, there needs to be an update every time a new card is added to the DOM.
let cards
const updateCardsModal = () => {
  cards = document.querySelectorAll('[id*="_card-"]')
  for (let i = 0; i < cards.length; i++) {
    cards[i].addEventListener('dblclick', (e) => {
      const that = e.target
      const categoryIndex = getCategoryIdxFromChild(that)
      const cardIndex = getCardIdxFromChild(that)
      injectDataIntoModal(cardIndex, categoryIndex, true)
    }, false)
  }
}

  // Comments:
const updateCommentsListeners = () => {
  const comments = document.getElementsByClassName('comment')
  for (let i = 0; i < comments.length; i++) {
    comments[i].addEventListener('click', (e) => {
      const that = e.target
      removeDisabledClass(that)
    })

    comments[i].addEventListener('blur', (e) => {
      const that = e.target
      if (that.value === '')
        return
      addDisabledClass(that)
      editComment(that)
    })
  }
}

  // Delete comments button:
const updateDeleteCommentsListeners = () => {
  let deleteComments = document.getElementsByClassName('delete-comment')
  if (!deleteComments)
    return
  for (let i = 0; i < deleteComments.length; i++) {
    deleteComments[i].addEventListener('click', (e) => {
      const that = e.target
      deleteCommentHandler(that)
    })
  }
}
// End of CARDS EVENT LISTENERS.

// The function to inject the data into the modal.
const injectDataIntoModal = (cardIndex, categoryIndex, openModal, callback) => {
  // Inject HTML template:
  document.getElementById('cards-modal').innerHTML = cardsModalHTML()
  // Initialize:
  $('#cards-modal').modal()
  // EVENT LISTENERS:
  // Description:
  const descInput = document.getElementById('m-cardDesc')
  descInput.addEventListener('click', (e) => {
    const that = e.target
    removeDisabledClass(that)
  })
  descInput.addEventListener('blur', (e) => {
    const that = e.target
    addDisabledClass(that)
    editCard(that)
  })

  // Inputs:
  const inputs = document.querySelectorAll('section.cards-modal input')
  for (let i = 0; i < inputs.length; i++) {
    if (!inputs[i].className.includes('datepicker')) {
      inputs[i].addEventListener('click', (e) => {
        const that = e.target
        removeDisabledClass(that)
      })

      inputs[i].addEventListener('blur', (e) => {
        const that = e.target
        addDisabledClass(that)
        editCard(that)
      })
    }
  }

  // Datepickers:
  let startElem = document.getElementById('m-cardStart')
  let endElem = document.getElementById('m-cardEnd')
  $('.datepicker-start').pickadate({
    format: 'dd-mm-yyyy',
    onOpen: () => {
      removeDisabledClass(startElem)
    },
    onClose: () => {
      addDisabledClass(startElem)
      editCard(startElem)
    }
  })

  $('.datepicker-end').pickadate({
    format: 'dd-mm-yyyy',
    onOpen: () => {
      removeDisabledClass(startElem)
    },
    onClose: () => {
      addDisabledClass(endElem)
      editCard(endElem)
    }
  })

  // New Comment textarea KeyUp:
  document.getElementById('m-newComment').addEventListener('keyup', () => {
    disableSaveButtonLogic()
  })

  // New Comment textarea Focus:
  document.getElementById('m-newComment').addEventListener('focus', () => {
    disableSaveButtonLogic()
  })

  // Save Comment Click:
  document.getElementById('m-saveComment').addEventListener('click', () => {
    saveCommentHandler()
  })
  // End of cards modal Event listeners.
  // Get the Card instance from the JSON file.
  const card = categoryList.categories[categoryIndex].cards[cardIndex]
  // The category index and card index are stored on hidden input fields.
  document.getElementById('m-categoryIdx').value = categoryIndex.toString()
  document.getElementById('m-cardIdx').value = card.id.toString()
  document.getElementById('m-cardTitle').value = card.title
  document.getElementById('m-cardDesc').value = card.desc
  document.getElementById('m-cardStart').value = card.start
  document.getElementById('m-cardEnd').value = card.end

  addAssignedUsersToModal(categoryIndex, card)

  document.getElementById('m-cardComments').innerHTML = ''
  let comments = categoryList.categories[categoryIndex].cards[card.id].comments
  for (let i = 0; i < comments.length; i++) {
    document.getElementById('m-cardComments').innerHTML += commentHTML(categoryIndex, cardIndex, i, comments[i].comment)
  }

  updateCommentsListeners()
  updateDeleteCommentsListeners()
  disableSaveButtonLogic()
  updateTheme()

  if (openModal)
    $('#cards-modal').modal('open')
  if (callback)
    callback()
}

// Edit card:
  // onModalPOST is the callback when the modal edits a card.
  // It passes the categary and card id's. of the changed card.
var onModalPOST
const editCard = (elem) => {
  let field = elem.id
  let value = elem.value
  let categoryId = getCatIdxFromModal()
  let cardId = getCardIdxFromModal()
  let attribute
  switch (field) {
    case 'm-cardTitle':
      document.getElementById('m-cardTitle').value = value
      categoryList.categories[categoryId].cards[cardId].title = value
      attribute = 'title'
      break
    case 'm-cardDesc':
      document.getElementById('m-cardDesc').value = value
      categoryList.categories[categoryId].cards[cardId].desc = value
      attribute = 'desc'
      break
    case 'm-cardStart':
      document.getElementById('m-cardStart').value = value
      categoryList.categories[categoryId].cards[cardId].start = value
      attribute = 'start-end'
      break
    case 'm-cardEnd':
      document.getElementById('m-cardEnd').value = value
      categoryList.categories[categoryId].cards[cardId].end = value
      attribute = 'start-end'
      break
  }
  categoryList.Save(() => {
    if (onModalPOST)
      onModalPOST(categoryId, cardId)
    if (document.location.pathname === '/plannr/' || document.location.pathname === '/plannr/index.html' || document.location.pathname === '/index.html')
      changeCard(categoryId, cardId, attribute)
  })
}

// It disables/enables the save button if there is no content.
const disableSaveButtonLogic = () => {
  let that = document.getElementById('m-newComment')
  let saveBtn = document.getElementById('m-saveComment')
  if (that.value === '')
    saveBtn.setAttribute('disabled', 'disabled')
  else
    saveBtn.removeAttribute('disabled')
}

const saveCommentHandler = () => {
  let commentElem = document.getElementById('m-newComment')
  let newComment = commentElem.value
  if (newComment === '')
    return
  let categoryIndex = getCatIdxFromModal()
  let cardIndex = getCardIdxFromModal()

  let commentIndex = categoryList.categories[categoryIndex].cards[cardIndex].comments.length
  document.getElementById('m-cardComments').innerHTML += commentHTML(categoryIndex, cardIndex, commentIndex, newComment)
  commentElem.value = ''

  updateTheme()
  updateCommentsListeners()
  updateDeleteCommentsListeners()
  saveCommentOnCard(categoryIndex, cardIndex, newComment)
  disableSaveButtonLogic()
}

const editComment = (elem) => {
  let newComment = elem.value
  let categoryIndex = getCatIdxFromModal()
  let cardIndex = getCardIdxFromModal()
  let commentIndex = parseIDs(elem.parentNode.id)[2]
  categoryList.categories[categoryIndex].cards[cardIndex].comments[commentIndex].comment = newComment
  categoryList.Save()
}

const deleteCommentHandler = (that) => {
  let categoryIndex = getCatIdxFromModal()
  let cardIndex = getCardIdxFromModal()
  let commentIndex = parseInt(that.parentNode.id.substring(26))
  // Remove from the DOM:
  let commentsContainer = getParentByTag(that, 'ul')
  commentsContainer.removeChild(that.parentNode)
  // Save change on JSON:
  categoryList.categories[categoryIndex].cards[cardIndex].RemoveComment(commentIndex)
  categoryList.Save()
}

const addAssignedUsersToModal = (categoryIndex, card) => {
  const assignedUsersEl = document.getElementById('m-assignedUsers')
  assignedUsersEl.innerHTML = ''

  const userIds = categoryList.categories[categoryIndex].cards[card.id].assigned
  for (let i = 0; i < userIds.length; i++) {
    let userId = parseInt(userIds[i])
    assignedUsersEl.innerHTML += assignedUserHTML(String(categoryIndex), String(card.id), String(userId), userList.users[userId].Name)
  }

  const assignmentDelete = document.getElementsByClassName('m-assignmentDelete')
  for (let i = 0; i < assignmentDelete.length; i++) {
    assignmentDelete[i].addEventListener('click', (e) => {
      const that = e.target
      const parsedIDs = parseIDs(that.parentNode.id)
      const categoryId = parsedIDs[0]
      const cardId = parsedIDs[1]
      const userId = parsedIDs[2]
      categoryList.categories[categoryId].cards[cardId].Unsign(userId)
      categoryList.Save(() => {
        changeCard(categoryId, cardId, 'assigned', stopEditing)
      })
    })
  }
}

// Assigned input:
let clicked = false
$('#cards-modal').on('click', '#m-cardAssigned', function (e) {
  if (clicked) return
  e.stopPropagation()
  clicked = true
  const category_id = getCatIdxFromModal()
  const card_id = getCardIdxFromModal()
  const card = categoryList.categories[category_id].cards[card_id]
  $(this).parent().find(`div.chip`).remove()
  $(this).parent().find(`span.chip-connector`).remove()
  $(this).next().after(editableSelectAssigned(category_id, card_id))
  $('#user-input').change(function () {
    let userId = document.getElementById('user-input').value
    if (userId === '-1') {
      document.getElementById('user-form').remove()
      addAssignedUsersToModal(category_id, card)
    } else {
      $("#user-input").unbind("change")
      // Save on JSON:
      document.getElementById('user-form').remove()
      categoryList.categories[category_id].cards[card_id].Assign(parseInt(userId))
      categoryList.Save(() => {
        addAssignedUsersToModal(category_id, card)
        changeCard(category_id, card_id, 'assigned', stopEditing)
      })
    }
    clicked = false
  })
  $('#user-input').material_select()
})

$("#cards-modal").click(function () {
  $('#user-input').change()
})
// End of EVENT HADLERS.
