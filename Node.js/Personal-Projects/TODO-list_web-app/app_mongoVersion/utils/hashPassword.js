'use strict'
const bcrypt = require('bcrypt');
const SALT_ROUNDS = 13;

module.exports = (plainTextPassword, callback) => {
  bcrypt.hash(plainTextPassword, SALT_ROUNDS, (err, hash) => {
    if (err)
      return callback(err, null)

    return callback(null, hash);
  })
}
