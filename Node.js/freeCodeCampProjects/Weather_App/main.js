$('#scale-btn').addClass('hide')
$(document).ready(function () {
  let usrLatitude
  let usrLongitude
  let celsius
  // User Geolocation:
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(function (position) {
      usrLatitude = position.coords.latitude
      usrLongitude = position.coords.longitude

      // API Call:
      let APILink = 'https://api.openweathermap.org/data/2.5/weather?lat=' + usrLatitude + '&lon=' + usrLongitude + '&appid=2be16b4f9b9e0645e776071c8092b02e'
      $.ajax({
        url: APILink,
        success: function (json) {
          $('#sky').html(json.weather[0].main)
          $('#sky-icon').attr('src', 'http://openweathermap.org/img/w/' + json.weather[0].icon + '.png')
          $('#sky-icon').attr('alt', json.weather[0].main)
          $('#location').html(json.name + ', ' + json.sys.country)
          // The temperature comes in Kelvin Scale (.C = .K - 273.15)
          celsius = (json.main.temp - 273.15).toFixed(2)
          $('#temp').html(celsius + ' &#x2103;')
          // Show the Temperature Scale Button:
          $('#scale-btn').removeClass('hide')
        }
      })
    })
  } else {
    $('#temp').html('Geolocation Not Available')
  }
  // Change Degree Scale Toggle Button (Celsius <-> Fahrenheit):
  function toCelsius (fahrenheit) {
    return ((fahrenheit - 32) / 1.8).toFixed(2)
  }

  let fahrenheit
  function toFahrenheit (celsius) {
    fahrenheit = (celsius * 1.8 + 32).toFixed(2)
    return fahrenheit
  }

  $('#change-scale').change(function () {
    if (this.checked) {
      $('#temp').html(toFahrenheit(celsius) + ' &#x2109;')
      console.log('Checked')
    } else {
      $('#temp').html(toCelsius(fahrenheit) + ' &#x2103;')
      console.log('Not Checked')
    }
  })
})
