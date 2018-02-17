'use strict'
const hashPassword = require('../utils/hashPassword')

module.exports = (req, res, next) => {
  let request = new req.sql.Request();
  request.query(
    `SELECT * 
    FROM dbo.LocalAuth
    WHERE Email = '${req.body.email}'`,
    (err, data) => {
    if (err)
      return next(err);

    // TODO: Add error hadling here to send "Email already exists" response.
    else if (data.recordsets[0].length > 0) {
      return res.status(400).redirect('/');
    }

    else {
      const plainTextPassword = req.body.password;

      hashPassword(plainTextPassword, (err, hash) => {
        request = new req.sql.Request();
        request.query(
          `INSERT INTO dbo.Users (FirstName, LastName) 
          VALUES ('', '');

          SELECT SCOPE_IDENTITY() AS User_Id;`,
          (err, data) => {
          if (err)
            return console.log(`request2 error: ${err}`);

          request = new req.sql.Request();
          request.query(
            `INSERT INTO dbo.LocalAuth 
            VALUES (${data.recordsets[0][0].User_Id}, '${req.body.email}', '${hash}', GETDATE());

            SELECT *
            FROM dbo.LocalAuth
            WHERE User_Id = ${data.recordsets[0][0].User_Id};`,
            (err, data) => {
            if (err)
              return res.redirect('/');

            return next(null, data.recordsets[0][0]);
          })
        })
      });
    }
  });
}
