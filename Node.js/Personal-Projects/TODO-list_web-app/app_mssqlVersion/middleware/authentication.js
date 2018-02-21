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

  // TODO: Change everithing for the new architecture.
  passport.deserializeUser((id, done) => {
    let request = new sql.Request();
    request.query(
      `SELECT *
      FROM dbo.Users
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
      let request = new sql.Request();
      request.query(
        `SELECT User_Id, Password
        FROM dbo.Users
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
          request = new sql.Request();
          request.query(
            `UPDATE dbo.Users
            SET dbo.Users.LastLogin = GETDATE(),
                dbo.Users.LoginCount += 1
            WHERE dbo.Users.User_Id = ${data.recordsets[0][0].User_Id};

            SELECT User_Id, Email, Password, Created, LastLogin, LoginCount
            FROM dbo.Users
            WHERE dbo.Users.User_Id = ${data.recordsets[0][0].User_Id}`,
            (err, data) => {
              return done(null, data.recordsets[0][0]);
            }
          );
        });
      });
    }));

  // GITHUB:
  passport.use(new GitHubStrategy({
    clientID: process.env.GITHUB_CLIENT_ID,
    clientSecret: process.env.GITHUB_CLIENT_SECRET,
    callbackURL: 'http://localhost:3000/auth/github/callback'
  }, (accessToken, refreshToken, profile, done) => {
    let request = new sql.Request();
    request.query(
      `SELECT Github_Id
      FROM dbo.Users
      WHERE Github_Id = '${profile.id}';`,
      (err, data) => {
        if (err)
          return done(err, null);

        // No user.
        if (data.recordsets[0].length <= 0) {
          request = new sql.Request();
          request.query(
            `INSERT INTO dbo.Users (Github_Id, FirstName, LastName, Created, LastLogin, LoginCount)
            VALUES ('${profile.id}', '', '', GETDATE(), GETDATE(), 1);

            SELECT *
            FROM dbo.Users
            WHERE Github_Id = '${profile.id}';`,
            (err, data) => {
              if (err)
                return done(err, null);

              return done(null, data.recordsets[0][0]);
            }
          );
        } else {
          request = new sql.Request();
          request.query(
            `UPDATE dbo.Users
            SET dbo.Users.LoginCount += 1,
                dbo.Users.LastLogin = GETDATE()
            WHERE dbo.Users.Github_Id = '${profile.id}';

            SELECT User_Id, Github_Id, Created, LastLogin, LoginCount
            FROM dbo.Users
            WHERE Github_Id = '${profile.id}';`,
            (err, data) => {
              if (err)
                return done(err, null);

              return done(null, data.recordsets[0][0]);
          });
        }
    });
  }));
}
