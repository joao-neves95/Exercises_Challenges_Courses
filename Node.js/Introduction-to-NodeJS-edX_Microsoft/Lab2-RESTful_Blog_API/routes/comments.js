const db = require('../database')

module.exports = {
  getComments(req, res) {
    res.status(200).send(db.database.posts[req.params.postId].comments)
  },

  getComment(req, res) {
    res.status(200).send({ comment: db.database.posts[req.params.postId].comments[req.params.commentId] })
  },

  addComment(req, res) {
    let newComment = new db.Comment(req.body.comment)
    db.database.posts[req.params.postId].comments.push(newComment)
    res.status(201).send( {msg: 'Comment successfully added.', postId: req.params.postId, newComment} )
  },

  updateComment(req, res) {
    let commentUpdate = new db.Comment(req.body.comment)
    db.database.posts[req.params.postId].comments[req.params.commentId] = commentUpdate
    res.status(201).send({ msg: 'Comment successfully updated.', postId: req.params.postId, commentId: req.params.commentId, commentUpdate })
  },

  deleteComment(req, res) {
    db.database.posts[req.params.postId].comments.splice(req.params.commentId, 1)
    res.status(200).send({ msg: 'Comment successfully deleted.', postId: req.params.postId, commentId: req.params.commentId })
  }
}
