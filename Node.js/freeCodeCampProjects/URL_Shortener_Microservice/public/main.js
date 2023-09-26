$('document').ready(() => {
  
  const newUrl = () => {
    $.ajax({
      url: 'https://xs-url.glitch.me/shorten/' + document.getElementById('inputUrl').value,
      dataType: 'json',
      type: 'GET',
      contentType: 'application/json; charset=UTF-8',
      success: (json) => {
        // Open url modal:
        $('#url-modal').modal({
          keyboard: false,
          show: true,
          focus:true
        })
        // Inject the short link into the modal:
        document.getElementById('modal-url-link').innerHTML = `<a href="${json.shortUrl}" target="_blank">${json.shortUrl}</a>`
        // Clean the url form input:
        document.getElementById('inputUrl').value = ''
      },
      error: (jqXHR, textStatus, errorThrown) => {
        // console.log(`jqXHR: ${jqXHR.responseText}`)
        // console.log(`textStatus: ${textStatus}`)
        // console.log(`errorThrown: ${errorThrown}`)
        document.getElementById('errors').innerHTML = `<div class="alert alert-danger magictime tinRightIn" role="alert">
                                                         ${JSON.parse(jqXHR.responseText).Error}
                                                       </div>` 
      }
    })
  }
    
  document.getElementById('submit-button').addEventListener('click', (e) => {
    e.preventDefault()
    newUrl()
    return false
  })

})
