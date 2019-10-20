const loadingPageHTML = () => {
  return `<div class="container-fluid loading-page open valign-wrapper">
            <div class="row center-align">
              <div class="preloader-wrapper big active">
                <div class="spinner-layer spinner-blue-only">
                  <div class="circle-clipper left">
                    <div class="circle"></div>
                  </div><div class="gap-patch">
                    <div class="circle"></div>
                  </div><div class="circle-clipper right">
                    <div class="circle"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>`
}

const openLoadingPage = () => {
  document.getElementById('loadingPage').innerHTML = loadingPageHTML()
}

const closeLoadingPage = () => {
  document.getElementById('loadingPage').innerHTML = ''
}
