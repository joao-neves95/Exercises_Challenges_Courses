'use strict'

const session = require('express-session');
const passport = require('passport');

module.exports = (app) => {
  app.use(session({
    secret: process.env.SESSION_SECRET,
    resave: true,
    saveUninitialized: true,
    name: "sessionID",
    cookie: {}
  }));

  app.use(passport.initialize())
  app.use(passport.session())
}
