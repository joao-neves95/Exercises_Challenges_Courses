'use strict'
const express = require('express');
const router = require('express').Router();
const path = require('path');
const passport = require('passport');
const ensureAuthentication = require('../middleware/ensureAuthentication')
const registerUser = require('../middleware/registerUser')
const items = require('./items.js');

// Serve WebApp Page (Dashboard) and static files:
router.use('/dashboard/public', ensureAuthentication, express.static(path.join(process.cwd(), './app-pages/public')));
router.get('/dashboard',
  ensureAuthentication,
  (req, res) => {
    res.status(200).sendFile(path.join(process.cwd(), '/app-pages/index.html'));
})

// REGISTER:
router.post('/register',
  registerUser,
  passport.authenticate('local', { failureRedirect: '/' }),
  (req, res) => {
    res.status(201).redirect('/dashboard');
  }
);

// LOGIN:
router.post('/login',
  passport.authenticate('local', { failureRedirect: '/dashboard' }),
  (req, res) => {
    res.status(202).redirect('/dashboard');
  }
);

// LOGOUT:
router.get('/logout', (req, res) => {
  req.logout();
  res.status(202).redirect('/');
});

// API: Items CRUD Operations:
router.get('/api/items', ensureAuthentication, items.getItems);
router.get('/api/item', ensureAuthentication, items.getItem);
router.post('/api/items', ensureAuthentication, items.postItem);
router.put('/api/item', ensureAuthentication, items.updateItem);
router.delete('/api/items', ensureAuthentication, items.deleteItem);

module.exports = router;
