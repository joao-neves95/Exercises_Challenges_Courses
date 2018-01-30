'use strict'

module.exports = {
  getItems (req, res) {
    res.locals.db.collection('items')
      .find( {} )
      .toArray((err, items) => {
        if (err)
          throw err

        res.status(200).send(items)
      })
  },

  getItem (req, res) {
    res.locals.db.collection('items')
      .find( {"_id": mongoDB.ObjectId(req.query.id)} )
      .toArray((err, item) => {
        if (err)
          throw err

        res.status(200).send(item)
      })
  },

  postItem (req, res) {
    res.locals.db.collection('items')
      .insertOne({
        "title": req.body.title,
        "priority": req.body.priority,
        "description": req.body.description,
        "dueDate": req.body.dueDate,
        "dueTime": req.body.dueTime
      },
      (err, results) => {
        if (err)
          throw err

      res.status(201).send(results)
      })
  },

  updateItem (req, res) {
    res.locals.db.collection('items')
      .updateOne(
        {"_id": mongoDB.ObjectId(req.query.id)},
        {$set: {
          "title": req.body.title,
          "priority": req.body.priority,
          "description": req.body.description,
          "dueDate": req.body.dueDate,
          "dueTime": req.body.dueTime
          }
        },
        (err, results) => {
          if (err)
            throw err

        res.status(200).send(results)
      } )
  },

  deleteItem (req, res) {
    res.locals.db.collection('items')
      .deleteOne( {"_id": mongoDB.ObjectId(req.query.id)}, (err, results) => {
        if (err)
         throw err

        console.log(req.query.id)
        res.status(200).send(results)
      })
  }
}
