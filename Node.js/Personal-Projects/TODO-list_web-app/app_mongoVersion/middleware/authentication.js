'use strict'
const passport = require('passport');
const LocalStrategy = require('passport-local');
const GitHubStrategy = require('passport-github').Strategy;
const ObjectID = require('mongodb').ObjectID;
const bcrypt = require('bcrypt')

module.exports = (app, db) => {
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
  // LOCAL:
  passport.use(new LocalStrategy({
    usernameField: 'email',
    passwordField: 'password'
  },
    (username, password, done) => {
      db.collection('users')
        .findOne({ email: username }, (err, user) => {
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

  // GITHUB:
  passport.use(new GitHubStrategy({
    clientID: process.env.GITHUB_CLIENT_ID,
    clientSecret: process.env.GITHUB_CLIENT_SECRET,
    callbackURL: 'http://localhost:3000/auth/github/callback'
  }, (accessToken, refreshToken, profile, done) => {
    db.collection('users')
      .findAndModify(
      { id: profile.id },
      {},
      { $setOnInsert: {
        id: profile.id,
        name: profile._json.name || 'John Doe',
        photo: profile._json.avatar_url || '',
        email: profile._json.email || 'No public email',
        bio: profile._json.bio || '',
        created_on: new Date(),
        provider: profile.provider || ''
      }, $set: {
        lastLogin: new Date()
      }, $inc: {
        loginCount: 1
      }
      },
      { upsert: true, new: true },
      (err, doc) => {
        if (err)
          return done(err, null);

        return done(null, doc.value);
      });
  }));
}
