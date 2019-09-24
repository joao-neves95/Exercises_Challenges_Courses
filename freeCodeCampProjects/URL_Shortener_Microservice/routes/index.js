const router = require('express').Router()
const isUrl = require('../middleware/isAbsoluteUrl.js')
const url = require('./url.js')

// Redirect to shortened URL:
router.get('/:url', url.redirectTo)

// Shorten new URL:
router.get('/shorten/*', isUrl, url.shortenUrl)
// * wildcard explanation: https://xs-url.glitch.me/8c5190da

// Get all URL's:
router.get('/get/all', url.getAllUrls)

module.exports = router
