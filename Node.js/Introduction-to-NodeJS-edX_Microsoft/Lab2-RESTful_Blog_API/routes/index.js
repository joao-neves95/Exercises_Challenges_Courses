const router = require('express').Router()
const cors = require('cors')
const posts = require('./posts')
const comments = require('./comments')

router.use(cors({ origin: 'http://localhost:3000' }))

router.use('/', posts)
router.use('/', comments)

module.exports = router
