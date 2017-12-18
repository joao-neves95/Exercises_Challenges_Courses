const db = require('../database')

module.exports = {
  getPosts(req, res) {
    res.status(200).send(db.database.posts)
  },

  getPost(req, res) {
    const post = db.database.posts[req.params.postId]
    res.status(200).send(post)
  },

  addPost(req, res) {
    let newPost = new db.Post(req.body.name, req.body.url, req.body.text)
    db.database.posts.push(newPost)
    res.status(201).send({ msg: 'Post successfully added.', newPost })
  },

  updatePost(req, res) {
    let postUpdate = new db.Post(req.body.name, req.body.url, req.body.text)
    db.database.posts[req.params.postId] = postUpdate
    res.status(200).send({ msg: 'Post successfully updated', postId: req.params.postId, postUpdate })
  },

  deletePost(req, res) {
    db.database.posts.splice(req.params.postId, 1)
    res.status(200).send({ msg: 'Post successfully deleted.', postId: req.params.postId })
  }
}
