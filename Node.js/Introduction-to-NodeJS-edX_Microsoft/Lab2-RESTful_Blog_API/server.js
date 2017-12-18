const express = require('express')
const fs = require('fs')
const path = require('path')
const helmet = require('helmet')
const logger = require('morgan')
const compression = require('compression')
const bodyParser = require('body-parser')
const db = require('./database')
const routes = require('./routes/index')
const app = express()


// create a write stream (in append mode) - LOGS:
const accessLogStream = fs.createWriteStream(path.join(__dirname, 'access.log'), {flags: 'a'})

app.use(logger('dev'))
app.use(logger('combined', {stream: accessLogStream}))
app.use(helmet())
app.use(compression())
app.use(bodyParser.json())

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

const hasApiKey = (req, res, next) => {
  if (!req.body.api_key)
    return res.status(401).send({ msg: 'Not authorized.' })
  else
    next()
}


app.get('/', (req, res) => {
  res.status(200).send('This is the Homepage.')
})

app.get('/posts', routes.posts.getPosts)
app.get('/posts/:postId', hasPost, routes.posts.getPost)
app.post('/posts', routes.posts.addPost)
app.put('/posts/:postId', hasPost, routes.posts.updatePost)
app.delete('/posts/:postId', hasApiKey, hasPost, routes.posts.deletePost)

app.get('/posts/:postId/comments', routes.comments.getComments)
app.get('/posts/:postId/comments/:commentId', hasPost, hasComment, routes.comments.getComment)
app.post('/posts/:postId/comments', routes.comments.addComment)
app.put('/posts/:postId/comments/:commentId', hasPost, hasComment, routes.comments.updateComment)
app.delete('/posts/:postId/comments/:commentId', hasApiKey, hasPost, hasComment, routes.comments.deleteComment)

const PORT = 3000
app.listen(PORT, () => {
  console.log(`Listening to port ${PORT}`)
})
