'use strict'
const express = require('express')
const path = require('path')
const helmet = require('helmet')
const bodyParser = require('body-parser')
const compression = require('compression')
const logger = require('morgan')
const routes = require('./routes/index.js')
const PORT = 3000

const app = express()

app.use(logger('dev'))
app.use(helmet())
app.use(compression())

// Serve FrontPage and static files:
app.use('/', express.static(path.join(__dirname, './public')))

app.use(bodyParser.json())
app.use(bodyParser.urlencoded( {extended: true} ))

// Serve WebApp Page (Dashboard) and static files:
app.use('/dashboard', express.static(path.join(__dirname, './app-pages')))

// API:

app.use('/api', routes)

app.listen(PORT, () => {
  console.log(`Listening on port ${PORT}`)
})
