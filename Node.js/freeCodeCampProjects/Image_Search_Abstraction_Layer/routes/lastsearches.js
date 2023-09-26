'use strict'
module.exports = {
  getLastSearches: (req, res) => {
    req.db.collection('logs')
      .find( {} )
      .limit(20)
      .toArray((err, data) => {
      if (err)
        res.status(err.status || 500).json({"Error": err.message})
      
      res.status(200).json(data)
    })
  },

  getAllSearches: (req, res) => {
    req.db.collection('logs')
      .find( {} )
      .toArray((err, data) => {
      // TODO: Error handling.
      if (err)
        res.status(err.status || 500).json({"Error": err.message})
      
      res.status(200).json(data)
    })
  }
}
