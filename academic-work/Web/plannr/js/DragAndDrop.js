'use strict'
/* UTILITY FUNCTIONS */
const log = message => console.log(message)

const changePropOfChilds = (parentNode, childTag, prop, value) => {
  let childNodes = parentNode.querySelectorAll(childTag)
  if (childNodes.length <= 0)
    return 0

  for (let i = 0; i < childNodes.length; i++) {
    childNodes[i].setAttribute(prop, value)
  }
}

const getAttributeFromElem = (elem, attribute) => {
  let attr = elem.attributes
  for (let i = 0; i < attr.length; i++) {
    if (attr[i].localName === attribute)
      return attr[i].nodeValue
  }
}

const dragIsCard = (originalNode) => {
  if (originalNode.className.includes('draggable card'))
    return true
  return false
}

const dragIsCategory = (originalNode) => {
  if (originalNode.className.includes('draggable category'))
    return true
  return false
}

const clientPositionOnCard = (e) => {
  let that = e.target
  if (that.parentNode.className.includes('draggable card') && e.layerY <= that.parentNode.clientHeight * 0.40)
    return 'top'
  else if (that.parentNode.className.includes('draggable card') && e.layerY >= that.parentNode.clientHeight * 0.60)
    return 'bottom'
}

const clientPositionOnCategory = (e) => {
  let that = e.target
  if (that.parentNode.parentNode.parentNode.className.includes('draggable category') && e.layerX <= that.parentNode.parentNode.clientWidth * 0.40)
    return 'left'
  else if (that.parentNode.parentNode.parentNode.className.includes('draggable category') && e.layerX >= that.parentNode.parentNode.clientWidth * 0.60)
    return 'right'
}

const acceptDrop = (e) => {
  e.preventDefault()
  e.dataTransfer.effectAllowed = 'move'
  e.dataTransfer.dropEffect = 'move'
}

const removeTempCard = (originalNode) => {
  let tempCard
  if (dragIsCard(originalNode))
    tempCard = document.getElementById('tempCard')
  else if (dragIsCategory(originalNode))
    tempCard = document.getElementById('tempCategory')

  if (tempCard === null || tempCard === undefined)
    return false
  tempCard.parentNode.removeChild(tempCard)
  insertedTempCard = false
}

const resetCardIds = (originalCatId, newCatId) => {
  const newCards = document.getElementById(`category_${parseInt(newCatId)}`).childNodes[5].children
  for (let i = 0; i < newCards.length; i++) {
    newCards[i].id = `category-${newCatId}_card-${i}`
  }
  // Just update if needed.
  if (originalCatId === newCatId)
    return
  const originalCards = document.getElementById(`category_${parseInt(originalCatId)}`).childNodes[5].children
  for (let i = 0; i < originalCards.length; i++) {
    originalCards[i].id = `category-${originalCatId}_card-${i}`
  }
}

const resetCategoryIds = () => {
  const categories = document.getElementById('categories').children
  for (let i = 0; i < categories.length; i++) {
    categories[i].id = `category_${i}`
  }
}

// Update the card on the JSON (remove from the original category, and insert new card on the new category):
const updateJSONCard = (originalCategoryIndex, newCategoryIndex, newCardIndex, originalTargetId) => {
  let cardId = parseIDs(originalTargetId)[1]
  let newCard = categoryList.categories[originalCategoryIndex].cards[cardId]
  // Remove the card from the original category:
  categoryList.categories[originalCategoryIndex].Remove(cardId)
  // Insert the card on the new category:
  categoryList.categories[newCategoryIndex].cards.splice(newCardIndex, 0, newCard)
  categoryList.Save()
}

// Update the category on the JSON (update index):
const updateJSONCategory = (originalCategoryIndex, newCategoryIndex) => {
  let newCategory = categoryList.categories[originalCategoryIndex]
  // Remove category from JSON:
  categoryList.Remove(originalCategoryIndex)
  // Insert category on the JSON:
  categoryList.categories.splice(newCategoryIndex, 0, newCategory)
  categoryList.Save()
}
/* End of UTILITY FUNCTIONS */

/* DRAGGABLE: */
let draggableElements
const updateDraggableElements = (callback) => {
  draggableElements = document.getElementsByClassName('draggable')
  for (let i = 0; i < draggableElements.length; i++) {
    draggableElements[i].setAttribute('draggable', 'true')
    // Disable the draggable attribute from links and images:
    changePropOfChilds(draggableElements[i], 'a', 'draggable', 'false')
    changePropOfChilds(draggableElements[i], 'img', 'draggable', 'false')
  }
  callback()
}

const updateDraggableListeners = () => {
  for (let i = 0; i < draggableElements.length; i++) {
    // With event listeners there's less JavaScript on the html page and it's faster.
    // ondragstart EVENT LISTENER:
    draggableElements[i].addEventListener('dragstart', (e) => {
      e.stopPropagation();
      dragstartHandler(e)
    })
  }
}

const updateDraggableItems = () => {
  updateDraggableElements(() => updateDraggableListeners())
}
updateDraggableItems()
/* End of DRAGGABLE. */

/* DROPPABLE */
let droppableElements
const updateDroppableItems = () => {
  droppableElements = document.getElementsByClassName('droppable')
  for (let i = 0; i < droppableElements.length; i++) {
    // ondragover EVENT LISTENER:
    droppableElements[i].addEventListener('dragover', (e) => {
      e.stopPropagation();
      dragoverHandler(e)
    }, false)
    // ondrop EVENT LISTENER:
    droppableElements[i].addEventListener('drop', (e) => {
      e.stopPropagation();
      dropHandler(e)
    }, false)
    // dragend EVENT LISTENER:
    droppableElements[i].addEventListener('dragend', (e) => dragendHandler(e), false)
  }
}
updateDroppableItems()
/* End of DROPPABLE */

// EVENT HANDLERS:
// DRAGSTART
let originalNode
let originalParentNode
let originalCategoryIndex
let originalTargetId
let insertedTempCard
// There needs to be a dropped boolean, because the drop event get's fired multiple times due to multiple drop zones.
let dropped
const dragstartHandler = (e) => {
  e.stopPropagation()
  originalNode = e.target
  // console.log(originalNode)
  // originalParentNode is the droppable section for cards, or the categories section for the categories.
  originalParentNode = originalNode.parentNode
  originalCategoryIndex = getCategoryIdxFromChild(originalNode)
  originalTargetId = e.target.id
  let tag = originalNode.localName
  let classes = originalNode.className.replace(/animated bounceIn/g, '')
  let data = `<${tag} id="${originalTargetId}" class="${classes}" style="${getAttributeFromElem(originalNode, 'style')}"> ${originalNode.innerHTML} </${tag}>`
  e.dataTransfer.setData('text/html', data)
  e.dataTransfer.effectAllowed = 'move'
  e.dataTransfer.dropEffect = 'move'
  insertedTempCard = false
  dropped = false
  return false
}

// DRAGOVER
// lastInsertedLocation (cards) === top/bottom/category (String) && lastInsertedLocation (categories) === left/right (String)
let lastInsertedLocation
let lastInsertedCategoryLocal
const dragoverHandler = (e) => {
  e.stopPropagation()
  // console.log(e)
  let that = e.target

  // Cards DRAGOVER logic:
  if (dragIsCard(originalNode)) {
    let clientPosition = clientPositionOnCard(e)
    // If the user is hovering on temporary card, accept the drop.
    if (that.id === 'tempCard') {
      acceptDrop(e)
      return false
      // If the user is hovering on an empty category, insert the temporary card.
    } else if (that.className.includes('droppable') && that.id !== 'categories' && that.children.length <= 0 && !insertedTempCard) {
      that.insertAdjacentHTML('afterbegin', tempCardHtml())
      insertedTempCard = true
      lastInsertedLocation = 'category'
      lastInsertedCategoryLocal = that
      updateTheme()
      return false
      // If the client is on the top of a card and hasn´t inserted a card yet:
    } else if (clientPosition === 'top' && !insertedTempCard) {
      that.parentNode.insertAdjacentHTML('beforebegin', tempCardHtml())
      insertedTempCard = true
      lastInsertedLocation = 'top'
      updateTheme()
      return false
      // If the client is on the bottom of a card and hasn´t inserted a card yet:
    } else if (clientPosition === 'bottom' && !insertedTempCard) {
      that.parentNode.insertAdjacentHTML('afterend', tempCardHtml())
      insertedTempCard = true
      lastInsertedLocation = 'bottom'
      updateTheme()
      return false
      // If a card was inserted and the client position is not the same as his last insert location:
    } else if (insertedTempCard && clientPosition !== lastInsertedLocation && (lastInsertedCategoryLocal !== that || lastInsertedLocation !== 'category')) {
      removeTempCard(originalNode)
      // Else, accept the drop on the card.
    } else if (insertedTempCard && (clientPosition === 'top' || clientPosition === 'bottom')) {
      acceptDrop(e)
      return false
    }

    // Categories DRAGOVER logic:
  } else if (dragIsCategory(originalNode) && that.parentNode.parentNode.parentNode.id !== originalTargetId) {
    let clientPosition = clientPositionOnCategory(e)
    let thisCategory = that.parentNode.parentNode.parentNode
    const categoriesNode = thisCategory.parentNode
    const lastCategory = categoriesNode.children[categoriesNode.children.length - 1]
    // If the client is on a temporary category, accept the drop.
    if (that.id === 'tempCategory') {
      acceptDrop(e)
      return false
      // If the temporary category was not yet inserted, and the client is in the LEFT part of the target:
    } else if (!insertedTempCard && clientPosition === 'left') {
      thisCategory.insertAdjacentHTML('beforebegin', tempCategoryHtml())
      insertedTempCard = true
      lastInsertedLocation = 'left'
      updateTheme()
      return false
      // If the temporary category was not yet inserted, and the client is in the RIGHT part of the last category:
    } else if (!insertedTempCard && clientPosition === 'right' && thisCategory === lastCategory) {
      thisCategory.insertAdjacentHTML('afterend', tempCategoryHtml())
      insertedTempCard = true
      lastInsertedLocation = 'right'
      updateTheme()
      return false
      // If the temporary category was inserted and the client's position is not the same as his last inserted location:
    } else if (insertedTempCard && clientPosition !== lastInsertedLocation) {
      removeTempCard(originalNode)
      // Else, accept the drop on the category.
    } else if (insertedTempCard && that.className.includes('category-title')) {
      acceptDrop(e)
      return false
    }
  }
}
// End of DRAGOVER.

// DROP
const dropHandler = (e) => {
  if (dropped)
    return false

  e.stopPropagation()
  const that = e.target
  let data = new DOMParser().parseFromString(e.dataTransfer.getData('text/html'), 'text/html').body.firstChild
  data.classList.add('animated', 'bounceIn')

  // Cards DROP logic:
  if (dragIsCard(originalNode)) {
    let clientPosition = clientPositionOnCard(e)
    let newCategoryNode = getParentNodeIdIncludes(that, 'category_')
    let newCategoryIndex = parseIDs(newCategoryNode.id)[0]
    let newCardIndex
    // Insert logic on a card:
    if (that.parentNode.className.includes('draggable card') && clientPosition === 'bottom') {
      e.preventDefault()
      newCardIndex = parseIDs(that.parentNode.id)[1] + 1
      that.parentNode.insertAdjacentElement('afterend', data)
      // Insert logic on the temporary card:
    } else if (that.id === 'tempCard') {
      if (newCategoryNode.children[2].children.length <= 1)
        newCardIndex = 0
      else
        newCardIndex = parseIDs(that.nextSibling.id)[1]
      that.insertAdjacentElement('afterend', data)
    }
    removeTempCard(originalNode)
    // Remove drop animation from new card:
    newCategoryNode.querySelector(`#${originalTargetId}`).classList.remove('animated', 'bounceIn')
    // Remove the original card from the DOM with animation (dragAndDrop.css):
    originalNode.classList.add('slideUp')
    setTimeout(() => {
      originalParentNode.removeChild(originalNode)
      resetCardIds(originalCategoryIndex, newCategoryIndex)
    }, 350)
    // Update the JSON:
    updateJSONCard(originalCategoryIndex, newCategoryIndex, newCardIndex, originalTargetId)

    // Categories DROP logic:
  } else if (dragIsCategory(originalNode)) {
    let clientPosition = clientPositionOnCategory(e)
    let newCategoryIndex
    // Insert the new category:
    // On the temporary category:
    if (that.id === 'tempCategory') {
      const nextSiblingId = parseIDs(that.nextSibling.id)[0]
      if (nextSiblingId === 1)
        newCategoryIndex = 1
      else if (nextSiblingId === 0)
        newCategoryIndex = 0
      else
        newCategoryIndex = nextSiblingId - 1
      that.insertAdjacentElement('afterend', data)
      // On another category:
    } else if (that.className.includes('category-title') && clientPosition === 'right') {
      const thisCategory = that.parentNode.parentNode.parentNode
      newCategoryIndex = parseIDs(thisCategory.id)[0]
      thisCategory.insertAdjacentElement('afterend', data)
    }
    document.getElementById(`${originalTargetId}`).id = `category_${newCategoryIndex}`
    removeTempCard(originalNode)
    // Remove the original category from the DOM:
    originalParentNode.removeChild(originalNode)
    updateJSONCategory(originalCategoryIndex, newCategoryIndex)
    resetCategoryIds()
    resetCardIds(originalCategoryIndex, newCategoryIndex)
    updateDroppableItems()
  }
  dropped = true
  updateCardsModal()
  updateDraggableItems()
  return false
}

// DRAGEND
// Just to be sure it gets deleted.
const dragendHandler = (e) => {
  removeTempCard(originalNode)
}
