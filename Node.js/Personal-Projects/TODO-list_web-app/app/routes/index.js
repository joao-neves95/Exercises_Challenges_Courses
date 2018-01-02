'use strict'
const router = require('express').Router()
const items = require('./items.js')
global.mongoDB = require('mongodb')
const URL = 'mongodb://localhost:27017/todo-list_web-app'

mongoDB.MongoClient.connect(URL, (err, database) => {
  if (err)
    return process.exit(1)

  const DB = database.db('todo-list_web-app')

  // Use the MongoDB package and present connection across the router:
  router.use((req, res, next) => {
    res.locals.db = DB
    next()
  })

  console.log('Connection successful.')

  // Items CRUD Operations:
  router.get('/items', items.getItems)
  router.get('/items', items.getItem)
  router.post('/items', items.postItem)
  router.put('/items/:id', items.updateItem)
  router.delete('/items', items.deleteItem)
})

module.exports = router
