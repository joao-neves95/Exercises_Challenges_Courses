const newTimestamp = require('../middleware/newTimestamp.js')
const cryptoRandomString = require('crypto-random-string')

module.exports = {
  
  // REDIRECT TO A SHORTENED URL:
  redirectTo(req, res) {
    res.locals.db.collection('url')
      .find( {shortUrl: 'https://xs-url.glitch.me/' + req.params.url} )
      .toArray((err, items) => {
        if (err)
          res.status(err.status || 500).send({"Error": err.message})
      
        res.redirect(302, items[0].originalUrl)
      })
  },
  
  // SHORTEN A NEW URL:
  shortenUrl(req, res) {
    const originalURL = req.params[0]
    const posterIpAddress = req.headers["x-forwarded-for"].split(',', 1).join()
    
    // New url document constructor:
    class UrlDocument {
      constructor (originalURL, shortURL, posterIpAddress) {
        this.originalUrl = originalURL
        this.shortUrl = shortURL
        this.info = {
          posterIP: posterIpAddress,
          timestamp: newTimestamp()
        }
      }
    }
    
    // Store the new document on a variable in order for it to be reusable:
    let newUrlDocument = new UrlDocument(originalURL, 'https://xs-url.glitch.me/' + cryptoRandomString(8), posterIpAddress)

    // Insert the new url document into the collection:
    res.locals.db.collection('url')
      .insertOne(newUrlDocument,
      (err, results) => {
        if (err)
          res.status(err.status || 500).send({"Error": err.message})

        res.status(201).send({
          "originalUrl": newUrlDocument.originalUrl,
          "shortUrl": newUrlDocument.shortUrl,
          "info": {
            "posterIP": newUrlDocument.info.posterIP,
            "timestamp": newUrlDocument.info.timestamp
          }
        })
      }
      )
  },
  
  // GET ALL URL'S:
  getAllUrls(req, res) {
    res.locals.db.collection('url')
      .find( {} )
      .toArray((err, items) => {
        if (err)
          res.status(err.status || 500).send({"Error": err.message})
      
        res.status('200').send(items)
      })
  }
}
