$(document).ready(() => {
  $('body').bootstrapMaterialDesign()

  // SEARCH BUTTON
  document.getElementById('btn-search').addEventListener('click', () => {
    handleSearchImages()
    return false
  }, false)
  
  document.getElementById('search-input').addEventListener('keypress', (e) => {
    if (e.keyCode === 13) {
      handleSearchImages()
      return e.preventDefault()
    }
    return false
  })
  
  // LAST SEARCHES PAGE:
  document.getElementById('btn-last-searches').addEventListener('click', () => {
    document.getElementById('images').style.display = 'none'
    hideAboutPage()
    
    getLastSearches()
    showLastSearches()
    return false
  }, false)
  
  // ABOUT PAGE:
  document.getElementById('btn-about').addEventListener('click', () => {
    hideLastSearches()
    document.getElementById('images').style.display = 'none'

    showAboutPage()
    return false
  }, false)
})

// FUNCTIONS:
const handleSearchImages = () => {
    hideAboutPage()
    hideLastSearches()
    document.getElementById('images').style.display = ''
    document.getElementById('offset').value = '0'
  
    searchImages()
}

const hideAboutPage = () => {
  document.getElementById('btn-about').classList.remove('active')
  document.getElementById('about').style.display = 'none'
}
const showAboutPage = () => {
  document.getElementById('btn-about').classList.add('active')
  document.getElementById('about').style.display = ''
}
const hideLastSearches = () => {
  document.getElementById('btn-last-searches').classList.remove('active')
  document.getElementById('last-searches').style.display = 'none'
}
const showLastSearches = () => {
  document.getElementById('btn-last-searches').classList.add('active')
  document.getElementById('last-searches').style.display = ''
}

const searchImages = () => {
  let searchQuery = document.getElementById('search-input').value
  let offset = document.getElementById('offset').value
  $.ajax({
    url: 'https://findimage.glitch.me/api/search/' + searchQuery + '?offset=' + offset,
    dataType: 'json',
    type: 'GET',
    contentType: 'application/json; charset=UTF-8',
    success: (json) => {
      document.getElementById('images').innerHTML = ''
      // Inject images on the page:
      for (let i = 0; i < json.length; i++) {
        document.getElementById('images').innerHTML +=
                                                      '<div class="col-lg-4 col-md-4">' +
                                                        `<a class="img-link" href="${json[i].url}" target="_blank">` +
                                                          `<img src="${json[i].url}" alt="${json[i].title}">` +
                                                        '</a>' +
                                                      '</div>'
      }
      // Inject "Next Page >" link on the page:
      document.getElementById('images').innerHTML += 
        '<div class="col-lg-12 col-md-12">' +
          `<a class="float-right" href="#" id="next-page" onclick="searchImages(); return false">Next Page &gt;</a>` +
        '</div>'
      
      let offset = document.getElementById('offset')
      offset.value = String(parseInt(offset.value) + 1)
      document.getElementById('next-page').style.visibility = 'visible'
    },
    error: (jqXHR, textStatus, errorThrown) => {
      console.log(`jqXHR.responseText: ${jqXHR.responseText}`)
      console.log('textStatus:')
      console.log(textStatus)
      console.log('errorThrown:')
      console.log(errorThrown)
    }
  })
}

const getLastSearches = () => {
  $.ajax({
    url: 'https://findimages.glitch.me/api/last',
    dataType: 'json',
    type: 'GET',
    contentType: 'application/json; charset=UTF-8',
    success: (json) => {
      let lastSearches = document.getElementById('last-searches')
      lastSearches.innerHTML = ''
      lastSearches.innerHTML +=
        '<div class="col-lg-12 col-md-12 mx-auto">' +
          '<h2 class="text-center mt-4">Last Searches</h2>' +
          '<button type="button" class="btn btn-"></button>' +
          `<pre class="mt-3">${JSON.stringify(json, null, 2)}</pre>` +
        '</div>'
    },
    error: (jqXHR, textStatus, errorThrown) => {
      console.log(`jqXHR.responseText: ${jqXHR.responseText}`)
      console.log('textStatus:')
      console.log(textStatus)
      console.log('errorThrown:')
      console.log(errorThrown)
    }
  })
}
