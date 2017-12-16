const router = require('express').Router()
const posts = require('./posts')
const comments = require('./comments')

router.use('/', posts)
router.use('/', comments)

router.get('/', (req, res) => {
  res.status(200).send('This is the Homepage.')
})

module.exports = router
