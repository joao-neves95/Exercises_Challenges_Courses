'use strict';
require('dotenv').config();
const express = require('express');
const path = require('path');
const helmet = require('helmet');
const bodyParser = require('body-parser');
const compression = require('compression');
const logger = require('morgan');
const sql = require('mssql');
const sessionsConfig = require('./middleware/sessions');
const authenticationConfig = require('./middleware/authentication');
const routes = require('./routes/index.js');
const app = express();

app.use(logger('dev'));
app.use(helmet());
app.use(compression());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// Serve Public FrontPage and static files:
app.use('/', express.static(path.join(__dirname, './public')));

const mssqlConfig = {
  server: process.env.MSSQL_INSTANCE,
  user: process.env.MSSQL_USER,
  password: process.env.MSSQL_PASS,
  database: 'TodoList'
}

sql.connect(mssqlConfig, (err) => {
  if (err)
    return console.error(err);

  console.log(`The connection to the MSSQL SERVER database ${mssqlConfig.database} was successful.`);

  app.use((req, res, next) => {
    req.sql = sql;
    next();
  });

  sessionsConfig(app);
  authenticationConfig(app, sql);

  // ROUTES:
  app.use('/', routes);

  const PORT = process.env.PORT || 3000;
  app.listen(PORT, () => {
    console.log(`Listening on port ${PORT}`);
  });
});

sql.on('error', err => {
  return console.error(err);
});
