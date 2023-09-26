// Docs: https://developers.google.com/custom-search/json-api/v1/reference/cse/list
'use strict'
const request = require('request')
const newTimestamp = require('../middleware/timestamp')
const CX = process.env.CX
const KEY = process.env.KEY

module.exports = {
  searchimage: (req, res) => {
    // https://findimage.glitch.me/api/search/something?offset=0
    let searchQuery =  encodeURIComponent(req.params.q)
    const IP = req.headers["x-forwarded-for"].split(',', 1).join()
    // The offset it's the page number (the first one is 0). It's incremented by 9 to get the start paremeter of the API request:
    let offset
    let start
    if (req.query.offset === undefined || req.query.offset === '0') {
      start = '1'
    } else if (parseInt(req.query.offset) > 0) {
      start = String(parseInt(req.query.offset) + 9)
    }
    
    // Google API Request:
    let URL = 'https://www.googleapis.com/customsearch/v1?key=' + KEY + '&cx=' + CX + '&searchType=image&safe=off&alt=json&start=' + start + '&q=' + searchQuery
    request.get(URL, (err, response, body) => {
      if (err)
        res.status(err.status || 500).json({"Error": err.message})
      
      class Record {
        constructor () {
          this.query = searchQuery
          this.ip = IP
          this.timestamp = newTimestamp()
        }
      }
      
      const newRecord = new Record()
      req.db.collection('logs').insertOne(newRecord, (err) => {
        if (err)
          res.status(err.status || 500).json({"Error": err.message})
      })
  
      const orderImageMeta = (jsonBody, callback) => {
        let imageMeta = []
        for (let i = 0; i < jsonBody.items.length - 1; i++) {
          imageMeta.push({
            title: jsonBody.items[i].title,
            htmlTitle: jsonBody.items[i].htmlTitle,
            url: jsonBody.items[i].link,
            thumbnailUrl: jsonBody.items[i].image.thumbnailLink,
            contextUrl: jsonBody.items[i].image.contextLink,
            sourceUrl: jsonBody.items[i].displayLink4
          })
        }
        callback(imageMeta)
      }

      let jsonBody = JSON.parse(body)
      orderImageMeta(jsonBody, (imageMeta) => {
        res.status(response.statusCode || 200).json(imageMeta)
      })
   })
  }
}
