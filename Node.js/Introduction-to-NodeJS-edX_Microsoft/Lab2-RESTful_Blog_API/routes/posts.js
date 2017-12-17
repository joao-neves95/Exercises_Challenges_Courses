const router = require('express').Router()
const db = require('../database')

router.get('/posts', (req, res) => {
  res.status(200).send(db.database.posts)
})

const hasPost = (req, res, next) => {
  const post = db.database.posts[req.params.postId]
  if (!post)
    return res.status(400).send({ msg: 'Post does not exist.', postId: req.params.postId })

  next()
}

router.get('/posts/:postId', hasPost, (req, res) => {
  const comment = db.database.posts[req.params.postId]
  if (!comment)
    return res.status(400).send({ msg: 'Post does not exist.', postId: req.params.postId })

  res.status(200).send(comment)
})

router.post('/posts', (req, res) => {
  let newPost = new db.Post(req.body.name, req.body.url, req.body.text)
  db.database.posts.push(newPost)
  res.status(201).send({ msg: 'Post successfully added.', newPost })
})


router.put('/posts/:postId', hasPost, (req, res) => {
  let postUpdate = new db.Post(req.body.name, req.body.url, req.body.text)
  db.database.posts[req.params.postId] = postUpdate
  res.status(200).send({ msg: 'Post successfully updated', postId: req.params.postId, postUpdate })
})

router.delete('/posts/:postId', (req, res, next) => {
  if (!req.body.api_key)
    return res.status(401).send({ msg: 'Not authorized.' })
  else
    next()
},
  (req, res) => {
    db.database.posts.splice(req.params.postId, 1)
    res.status(200).send({ msg: 'Post successfully deleted.', postId: req.params.postId })
  })

module.exports = router
