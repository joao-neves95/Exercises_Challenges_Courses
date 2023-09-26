const express = require('express')
const path = require('path')
const routes = require('./routes/index.js')
const morgan = require('morgan')
const app = express()

app.use(morgan('dev'))

app.use(express.static('public'))
app.get("/", (request, response) => {
  response.sendFile(path.join(__dirname, '/views/index.html'))
})

// API:
app.use(express.json())
app.use("/api", routes)

// Listen in Glitch.com PORT:
var listener = app.listen(process.env.PORT, function () {
  console.log('The server is listening on port ' + listener.address().port);
})
