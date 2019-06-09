const express = require( 'express' );
const router = express.Router();
const tutorsController = require( './controllers/tutorsController' );

router.use( ( req, res, next ) => {
  res.setHeader( 'Content-Type', 'application/json' );
  return next();
} );

router.use( '/tutors', tutorsController );

module.exports = router;
