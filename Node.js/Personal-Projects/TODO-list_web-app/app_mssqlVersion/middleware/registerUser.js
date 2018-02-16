'use strict'
const hashPassword = require('../utils/hashPassword')

module.exports = (req, res, next) => {
  req.db.collection('users')
    .findOne({ email: req.body.email }, (err, user) => {
      // TODO: Properly handle errors.
      if (err)
        return next(err);

      else if (user)
        return res.status(400).redirect('/');

      else {
        const plainTextPassword = req.body.password;

        hashPassword(plainTextPassword, (err, hash) => {
          console.log("Creating user.")
          req.db.collection('users')
            .insertOne({
              email: req.body.email,
              password: hash,
              created_on: new Date()
            }, (err, user) => {
              if (err)
                return res.redirect('/');

              return next(null, user);
            });
        });
      }
    });
}
