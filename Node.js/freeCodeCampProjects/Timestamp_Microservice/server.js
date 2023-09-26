const express = require('express')
const path = require('path')
const routes = require('./routes/index.js')
const app = express()

app.use('/', express.static('public'));

app.get('/', function (req, res) {
  res.sendFile(path.join(__dirname, '/views/index.html'))
})

app.use(express.json())

app.get('/:date', routes.timestamp.getTimestamp)

// On Glitch.com PORT:
const listener = app.listen(process.env.PORT, function () {
  console.log('The server is listening on port ' + listener.address().port);
})
