const isAbsoluteUrl = require('is-absolute-url')

const isUrl = (req, res, next) => {
  if (!isAbsoluteUrl(req.params[0]))
    return res.status('400').send({"Error": "Wrong url format, please make sure you type a valid url: 'https://github.com/' or 'https://github.com/joao-neves95/'."})

  next()
}

module.exports = isUrl
