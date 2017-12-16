const express = require('express')
const fs = require('fs')
const path = require('path')
const helmet = require('helmet')
const logger = require('morgan')
const compression = require('compression')
const bodyParser = require('body-parser')
const db = require('./database')
const routes = require('./routes/index')
const PORT = 3000
const app = express()

// create a write stream (in append mode) - LOGS:
const accessLogStream = fs.createWriteStream(path.join(__dirname, 'access.log'), {flags: 'a'})

app.use(logger('dev'))
app.use(logger('combined', {stream: accessLogStream}))
app.use(helmet())
app.use(compression())
app.use(bodyParser.json())

app.use('/', routes)

app.get('/', (req, res) => {
  res.status(200).send('This is the Homepage.')
})

app.listen(PORT, () => {
  console.log(`Listening to port ${PORT}`)
})
