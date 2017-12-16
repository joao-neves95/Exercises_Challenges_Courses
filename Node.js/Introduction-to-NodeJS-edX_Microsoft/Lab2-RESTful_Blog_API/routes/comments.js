const router = require('express').Router()
const db = require('../database')

const hasPost = (req, res, next) => {
  const post = db.database.posts[req.params.postId]
  if (!post)
    return res.status(400).send({ msg: 'Post does not exist.', postId: req.params.postId })

  next()
}

const hasComment = (req, res, next) => {
  const comment = db.database.posts[req.params.postId].comments[req.params.commentId]
  if (!comment)
    return res.status(201).send({ msg: 'Comment does not exist.', postId: req.params.postId, commentId: req.params.commentId })

  next()
}

router.get('/posts/:postId/comments', hasPost, (req, res) => {
  res.status(200).send(db.database.posts[req.params.postId].comments)
})

router.get('/posts/:postId/comments/:commentId', hasPost, hasComment, (req, res) => {
  res.status(200).send({ comment: db.database.posts[req.params.postId].comments[req.params.commentId] })
})

router.post('/posts/:postId/comments', hasPost, (req, res) => {
  let newComment = new db.Comment(req.body.comment)
  db.database.posts[req.params.postId].comments.push(newComment)
  res.status(201).send( {msg: 'Comment successfully added.', postId: req.params.postId, newComment} )
})

router.put('/posts/:postId/comments/:commentId', hasPost, hasComment, (req, res) => {
  let commentUpdate = new db.Comment(req.body.comment)
  db.database.posts[req.params.postId].comments[req.params.commentId] = commentUpdate
  res.status(201).send({ msg: 'Comment successfully updated.', postId: req.params.postId, commentId: req.params.commentId, commentUpdate })
})

const hasKey = (req, res, next) => {
  if (!req.body.api_key)
    return res.status(401).send( {msg: 'Not authorized.'} )

  next()
}

router.delete('/posts/:postId/comments/:commentId', hasKey, hasPost, hasComment, (req, res) => {
  db.database.posts[req.params.postId].comments.splice(req.params.commentId, 1)
  res.status(200).send({ msg: 'Comment successfully deleted.', postId: req.params.postId, commentId: req.params.commentId })
})

module.exports = router
