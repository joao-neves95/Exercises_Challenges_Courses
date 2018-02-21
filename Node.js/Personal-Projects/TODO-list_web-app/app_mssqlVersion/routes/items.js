'use strict'
// const sql = require('mssql');

module.exports = {
  getItems(req, res) {
    console.log(req.user.User_Id);
    let request = new req.sql.Request();
    request.query(
      `SELECT Item_Id, Title, Priority, Description, DueDate, DueTime
      FROM dbo.Item
      WHERE User_Id = ${parseInt(req.user.User_Id)};`,
      (err, data) => {
        if (err)
          return console.error(err);

        res.status(200).send(data.recordsets[0]);
      }
    );
  },

  getItem(req, res) {
    let request = new req.sql.Request();
    request.query(
      `SELECT Item_Id, Title, Priority, Description, DueDate, DueTime
      FROM dbo.Item
      WHERE Item_Id = ${req.query.id} AND User_Id = ${parseInt(req.user.User_Id)};`,
      (err, data) => {
        if (err)
          return console.error(err);

        res.status(200).send(data.recordsets[0]);
    });
  },

  postItem(req, res) {
    let request = new req.sql.Request();
    request.query(
      `INSERT INTO dbo.Item (User_Id, Title, Priority, Description, DueDate, DueTime, Created, lastUpdate)
      VALUES (${req.user.User_Id}, '${req.body.title}', ${req.body.priority}, '${req.body.description}', CONVERT(DATE, '${req.body.dueDate}', 5), CONVERT(TIME, '${req.body.dueTime}', 8), GETDATE(), GETDATE());`,
      (err, data) => {
        if (err)
          return console.error(err);

        res.status(204).end();
    });
  },

  updateItem(req, res) {
    let request = new req.sql.Request();
    request.query(
      `UPDATE dbo.Item
      SET dbo.Item.Title = '${req.body.title}',
          dbo.Item.Priority = ${parseInt(req.body.priority)},
          dbo.Item.Description = '${req.body.description}',
          dbo.Item.DueDate = CONVERT(DATE, '${req.body.dueDate}', 5),
          dbo.Item.DueTime = CONVERT(TIME, '${req.body.dueTime}', 8),
          dbo.Item.LastUpdate = GETDATE()
      WHERE Item_Id = ${parseInt(req.query.id)};`,
      (err, data) => {
        if (err)
          console.error(err);

        res.status(204).end();
    });
  },

  deleteItem(req, res) {
    let request = new req.sql.Request();
    request.query(
      `DELETE FROM dbo.Item
      WHERE dbo.Item.Item_Id = ${parseInt(req.query.id)};`,
      (err, data) => {
        if (err)
          return console.error(err);

        res.status(204).end();
    });
  }
}
