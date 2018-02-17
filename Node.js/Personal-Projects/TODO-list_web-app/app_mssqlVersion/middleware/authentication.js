'use strict'
const passport = require('passport');
const LocalStrategy = require('passport-local');
const GitHubStrategy = require('passport-github').Strategy;
const ObjectID = require('mongodb').ObjectID;
const bcrypt = require('bcrypt')

module.exports = (app, sql) => {
  app.use(passport.initialize());
  app.use(passport.session());

  passport.serializeUser((user, done) => {
    done(null, user.User_Id);
  });

  // TODO: JOIN the other strategies data, when it's added a new one.
  passport.deserializeUser((id, done) => {
    const request = new sql.Request();
    request.query(
      `SELECT dbo.Users.User_Id, dbo.LocalAuth.Email, dbo.LocalAuth.Password
      FROM dbo.Users
      INNER JOIN dbo.LocalAuth
          ON dbo.Users.User_Id = dbo.LocalAuth.User_Id
      WHERE dbo.Users.User_Id = ${id}`,
      (err, data) => {
        if (err) {
          return done(err, null);
        }

      return done(null, data.recordsets[0][0]);
    });
  });

  // TODO: Properly handle errors.
  // LOCAL STRATEGY:
  passport.use(new LocalStrategy({
    usernameField: 'email',
    passwordField: 'password'
  },
    (username, password, done) => {
      const request = new sql.Request();
      request.query(
        `SELECT *
        FROM dbo.LocalAuth
        WHERE Email = '${username}'`,
        (err, data) => {
        if (err) {
          // Authentication error.
          return done(err);
        }
        if (data.recordsets[0].length <= 0) {
          // User does not exist/wrong email.
          return done(null, false);
        }
        // Check passwords.
        bcrypt.compare(password, data.recordsets[0][0].Password, (err, res) => {
          if (err) {
            return done(err);
          }

          // Not the same passwords.
          if (!res) {
          return done(null, false);
          }

          // Authentication successful.
          return done(null, data.recordsets[0][0]);
        });
      });
    }));

  // TODO: Implement Github Passport Strategy in MSSQL.
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
