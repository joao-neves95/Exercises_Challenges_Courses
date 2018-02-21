'use strict'
const hashPassword = require('../utils/hashPassword')

module.exports = (req, res, next) => {
  let request = new req.sql.Request();
  request.query(
    `SELECT Email
    FROM dbo.Users
    WHERE Email = '${req.body.email}'`,
    (err, data) => {
      if (err) {
        return next(err);
      }
      // TODO: Add error handling here to send "Email already exists" response.
      else if (data.recordsets[0].length > 0) {
          return res.status(400).redirect('/');
      } else {
        const plainTextPassword = req.body.password;

        hashPassword(plainTextPassword, (err, hash) => {
          request = new req.sql.Request();
          request.query(
            `INSERT INTO dbo.Users (Email, Password, FirstName, LastName, Created) 
            VALUES ('${req.body.email}', '${hash}', '', '', GETDATE());

            SELECT *
            FROM dbo.Users
            WHERE Email = '${req.body.email}';`,
            (err, data) => {
              if (err) {
                return res.redirect('/');
              }

              return next(null, data.recordsets[0][0]);
            }
          );
        });
      }
  });
}
