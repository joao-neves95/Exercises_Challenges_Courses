'use strict'
const session = require('express-session');
const passport = require('passport');
const LocalStrategy = require('passport-local');
const ObjectID = require('mongodb').ObjectID;
const bcrypt = require('bcrypt')

module.exports = (app, db) => {
  app.use(session({
    secret: process.env.SESSION_SECRET,
    resave: true,
    saveUninitialized: true,
    name: "sessionId",
    cookie: {}
  }));

  app.use(passport.initialize());
  app.use(passport.session());

  passport.serializeUser((user, done) => {
    done(null, user._id);
  });

  passport.deserializeUser((id, done) => {
    db.collection('users')
      .findOne(
        { _id: new ObjectID(id) },
        (err, user) => {
          if (err)
            return done(err, null);

          return done(null, user);
        });
  });

  // TODO: Properly handle errors.
  passport.use(new LocalStrategy({
    usernameField: 'email',
    passwordField: 'password'
  },
    (username, password, done) => {
      db.collection('users')
        .findOne({email: username}, (err, user) => {
          // Login attempt.
          if (err) {
            // Authentication error.
            return done(err);
          }

          if (!user) {
            // User does not exist/wrong email.
            return done(null, false);
          }

          // Check passwords:
          bcrypt.compare(password, user.password, (err, res) => {
            if (err)
              return done(null, false);

            if (!res)
              return done(null, false);

            // Authentication successful.
            return done(null, user)
          });
        });
    }));
}
