'use strict'
const router = require('express').Router()
const mongoDB = require('mongodb')
const searchimage = require('./searchimage').searchimage
const lastsearches = require('./lastsearches')

const mongoUSER = encodeURIComponent(process.env.mongoUSER)
const mongoPASS = encodeURIComponent(process.env.mongoPASS)
const mongoCONFIG = process.env.mongoCONFIG
const URI = `mongodb://${mongoUSER}:${mongoPASS}@${mongoCONFIG}`

mongoDB.MongoClient.connect(URI, (err, database) => {
  if (err)
    return console.log(`Unable to connect to the mongoDB server. Error: ${err}`)

  const DB = database.db('findimage')
  
  // Use the MongoDB present connection across the Router:
  router.use((req, res, next) => {
    req.db = DB
    next()
  })

  // SEARCH IMAGE:
  router.get('/search/:q', searchimage)

  //LAST SEARCHES:
  router.get('/last', lastsearches.getLastSearches)
  router.get('/last/all', lastsearches.getAllSearches)
})

module.exports = router
