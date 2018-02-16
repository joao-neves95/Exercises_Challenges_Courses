'use strict'

module.exports = {
  getItems (req, res) {
    req.db.collection('items')
      .find(
        { "userId": req.user._id },
        { "userId": 0 }
      )
      .toArray((err, items) => {
        if (err)
          throw err;

        res.status(200).send(items);
      });
  },

  getItem (req, res) {
    req.db.collection('items')
      .find(
        { "_id": mongoDB.ObjectId(req.query.id), "userId": req.user._id },
        { "userId": 0 }
      )
      .toArray((err, item) => {
        if (err)
          throw err;

        res.status(200).send(item);
      })
  },

  postItem (req, res) {
    req.db.collection('items')
      .insertOne({
        "userId": req.user._id,
        "title": req.body.title,
        "priority": req.body.priority,
        "description": req.body.description,
        "dueDate": req.body.dueDate,
        "dueTime": req.body.dueTime
      },
      (err, results) => {
        if (err)
          throw err;

        res.status(201).send(results);
      })
  },

  updateItem (req, res) {
    req.db.collection('items')
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
            throw err;

        res.status(200).send(results);
      } )
  },

  deleteItem (req, res) {
    req.db.collection('items')
      .deleteOne( {"_id": mongoDB.ObjectId(req.query.id)}, (err, results) => {
        if (err)
         throw err;

        res.status(200).send(results);
      })
  }
}
