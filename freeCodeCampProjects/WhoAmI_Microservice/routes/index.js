//API:
const router = require('express').Router()
const whoami = require('./whoami.js')

router.get('/whoami', whoami.whoami)

module.exports = router
