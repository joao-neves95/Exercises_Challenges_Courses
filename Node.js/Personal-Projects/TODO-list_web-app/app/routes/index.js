'use strict'

const router = require('express').Router();
const items = require('./items.js');

// Items CRUD Operations:
router.get('/items', items.getItems);
router.get('/item', items.getItem);
router.post('/items', items.postItem);
router.put('/item', items.updateItem);
router.delete('/items', items.deleteItem);

module.exports = router;
