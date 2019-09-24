'use strict'
const express = require('express')
const helmet = require('helmet')
const cors = require('cors')
const compression = require('compression')
const routes = require('./routes/index.js')
const app = express()

const corsOptions = {
  "origin": "https://findimages.glitch.me",
  "methods": "GET",
  "optionsSuccessStatus": 200 // some legacy browsers (IE11, various SmartTVs) choke on 204 
}
app.use(helmet())
app.use(cors(corsOptions))
app.use(compression())

// HOMEPAGE:
app.use(express.static('public'));
app.get("/", function (request, response) {
  response.sendFile(__dirname + '/views/index.html');
});

// API:
app.use('/api', routes)

const listener = app.listen(process.env.PORT, function () {
  console.log('The Node.js server is listening on port ' + listener.address().port);
});
