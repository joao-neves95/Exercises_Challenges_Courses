const express = require('express')
const path = require('path')
const helmet = require('helmet')
const morgan = require('morgan')
const mongoDB = require('mongodb')
const routes = require('./routes/index.js')
const app = express()

app.use(helmet())
app.disable('x-powered-by')
app.use(morgan('dev'))

// Statics and front-page:
app.use(express.static('public'))
app.get('/', function (request, response) {
  response.sendFile(path.join(__dirname + '/views/index.html'))
})

app.use(express.json())

// mongoDB CONNECTION:
const user = encodeURIComponent(process.env.mongoUSER)
const pass = encodeURIComponent(process.env.mongoPASS)
const config = process.env.mongoCONFIG
const url = `mongodb://${user}:${pass}@${config}`

mongoDB.MongoClient.connect(url, (err, database) => {
  if (err)
    return console.log(`Unable to connect to the mongoDB server. Error: ${err}`)

  const DB = database.db('xs-url')
  
  // Use the MongoDB present connection across the Router:
  app.use((req, res, next) => {
    res.locals.db = DB
    next()
  })
  
  // API ROUTING:
  app.use('/', routes)
})


// Listen for Glitch.com PORT:
var listener = app.listen((process.env.PORT || 3000), function () {
  console.log('The server is listening on port ' + listener.address().port)
})
