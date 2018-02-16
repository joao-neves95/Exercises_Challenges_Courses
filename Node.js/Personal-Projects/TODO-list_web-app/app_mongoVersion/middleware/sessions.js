'use strict'
const session = require('express-session');

module.exports = (app) => {
  app.use(session({
    secret: process.env.SESSION_SECRET,
    resave: false,
    saveUninitialized: true,
    name: "sessionId",
    // 90 day session cookie:
    cookie: {maxAge: 7776000000, sameSite: 'lax'}
  }));
}
