const darkTheme = {
  aside: {
    color: 'grey',
    shade: 'darken-4'
  },
  main: {
    color: 'grey',
    shade: 'darken-3'
  },
  footer: {
    color: 'grey',
    shade: 'darken-2'
  },
  panelTitle: {
    color: 'grey',
    shade: 'darken-4'
  },
  panelContent: {
    color: 'grey',
    shade: 'darken-3',
    fontColor: 'grey-text'
  },
  settings: {
    color: 'grey',
    shade: 'darken-3',
    fontColor: 'white-text'
  },
  tempCard: {
    bg: '#77757581',
    border: '#f3f0f0'
  }
}

const lightTheme = {
  aside: {
    color: 'green',
    shade: 'darken-1'
  },
  main: {
    color: 'white',
    shade: 0
  },
  footer: {
    color: 'green',
    shade: 0
  },
  panelTitle: {
    color: 'white',
    shade: 0
  },
  panelContent: {
    color: 'white',
    shade: 0,
    fontColor: 'black-text'
  },
  settings: {
    color: 'white',
    shade: 0,
    fontColor: 'black-text'
  },
  tempCard: {
    bg: '#cec9c973',
    border: '#888888'
  }
}

let toRemove
let toAdd
const setTheme = (page) => {
  const aside = document.getElementsByTagName('aside')[0]
  const body = document.getElementsByTagName('body')[0]
  const main = document.getElementsByTagName('main')[0]
  const footer = document.getElementsByTagName('footer')[0]
  const panelTitle = document.querySelector('#manage-urgencies > article.panel > div.row a')
  const panelContent = document.querySelector('#manage-urgencies > .panel')
  const panelContentText = document.querySelectorAll('#manage-urgencies > article.panel table.urgency-table')[0]
  const settings = document.getElementById('settings')
  const cardsModal = document.getElementById('cards-modal')
  // Theme persistance initialization (persist in localStorage):
  // If the user has never set up the theme, it defaults to false.
  let isDarkTheme = localStorage.getItem('theme')
  if (isDarkTheme === null) {
    isDarkTheme = false
    localStorage.setItem('theme', isDarkTheme)
  } else {
    if (isDarkTheme === 'true')
      isDarkTheme = true
    else 
      isDarkTheme = false
  }

  document.getElementById('theme-checkbox').checked = isDarkTheme

  if (isDarkTheme)
    toAdd = darkTheme
  else
    toAdd = lightTheme

  if (toAdd === lightTheme)
    toRemove = darkTheme
  else if (toAdd === darkTheme)
    toRemove = lightTheme

  // General theme removal:
  aside.classList.remove(toRemove.aside.color, toRemove.aside.shade)
  body.classList.remove(toRemove.main.color, toRemove.main.shade)
  main.classList.remove(toRemove.main.color, toRemove.main.shade)
  footer.classList.remove(toRemove.footer.color, toRemove.footer.shade)
  settings.classList.remove(toRemove.settings.color, toRemove.settings.shade)
  if(panelTitle) panelTitle.classList.remove(toRemove.panelTitle.color, toRemove.panelTitle.shade)
  if(panelContent)panelContent.classList.remove(toRemove.panelContent.color, toRemove.panelContent.shade)
  if(panelContentText)panelContentText.classList.remove(toRemove.panelContent.fontColor)
  cardsModal.classList.remove(toRemove.settings.color, toRemove.settings.shade, toRemove.settings.fontColor)
  // General theme addition:
  aside.classList.add(toAdd.aside.color, toAdd.aside.shade)
  body.classList.add(toAdd.main.color, toAdd.main.shade)
  main.classList.add(toAdd.main.color, toAdd.main.shade)
  footer.classList.add(toAdd.footer.color, toAdd.footer.shade)
  settings.classList.add(toAdd.settings.color, toAdd.settings.shade)
  if(panelTitle) panelTitle.classList.add(toAdd.panelTitle.color, toAdd.panelTitle.shade)
  if(panelContent) panelContent.classList.add(toAdd.panelContent.color, toAdd.panelContent.shade)
  if(panelContentText) panelContentText.classList.add(toAdd.panelContent.fontColor)
  cardsModal.classList.add(toAdd.settings.color, toAdd.settings.shade, toAdd.settings.fontColor)

  if (page === 'index') {
    let categoryTitles = document.querySelectorAll('section.category > div.card')
    // Theme removal:
    for (let i = 0; i < categoryTitles.length; i++) {
      categoryTitles[i].classList.remove(toRemove.aside.color, toRemove.aside.shade)
    }

    // Theme addition:
    for (let i = 0; i < categoryTitles.length; i++) {
      categoryTitles[i].classList.add(toAdd.aside.color, toAdd.aside.shade)
    }
  } else if (page === 'schedule') {
    const months = document.querySelectorAll('section.month > p')
    const weeks = document.getElementsByClassName('official')
    // Months removal/addition:
    for (let i = 0; i < months.length; i++) {
      months[i].classList.remove(toRemove.aside.color, toRemove.aside.shade)
      months[i].classList.add(toAdd.aside.color, toAdd.aside.shade)
    }
    // Weeks removal/addition:
    for (let i = 0; i < weeks.length; i++) {
      weeks[i].classList.remove(toRemove.footer.color, toRemove.footer.shade)
      weeks[i].classList.add(toAdd.footer.color, toAdd.footer.shade)
    }
  }
}

// The update function for all the dynamic content.
const updateTheme = () => {
  updateTempCard()
  updateTempCategory()
  updateComments()

  const categoryTitles = document.querySelectorAll('section.category > div.card')
  // If the class of the last category DOM node was already changed, return.
  if (!categoryTitles || categoryTitles.length === 0 || categoryTitles[categoryTitles.length - 1].className.includes(toAdd.aside.color))
    return false
  for (let i = 0; i < categoryTitles.length; i++) {
    categoryTitles[i].classList.add(toAdd.aside.color, toAdd.aside.shade)
  }
}

const updateTempCard = () => {
  const tempCard = document.getElementById('tempCard')
  if (!tempCard)
    return false
  tempCard.style.backgroundColor = toAdd.tempCard.bg
  tempCard.style.borderColor = toAdd.tempCard.border
}

const updateTempCategory = () => {
  let tempCategory = document.getElementById('tempCategory')
  if (!tempCategory)
    return false
  tempCategory.style.backgroundColor = toAdd.tempCard.bg
  tempCategory.style.borderColor = toAdd.tempCard.border
}

const updateComments = () => {
  const comments = document.querySelectorAll('section.cards-modal li.collection-item')
  if (!comments || comments.length === 0 || comments[comments.length - 1].className.includes(toAdd.settings.color))
    return false
  for (let i = 0; i < comments.length; i++) {
    comments[i].classList.remove(toRemove.settings.color, toRemove.settings.shade, toRemove.settings.fontColor)
    comments[i].classList.add(toAdd.settings.color, toAdd.settings.shade, toAdd.settings.fontColor)
  }
}

$(() => {
  // SETTINGS EVENT HANDLER:
  document.getElementById('settings-btn').addEventListener('click', () => {
    // Modal options.
    $('#settings').modal()
    // Open modal.
    $('#settings').modal('open')
  })
})