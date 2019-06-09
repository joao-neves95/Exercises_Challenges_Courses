const express = require( 'express' );
const router = express.Router();
const DB = require( '../mockDB' );

router.get( '/', ( req, res ) => {
  res.status( 200 ).send( JSON.stringify( DB.tutors ) );
} );

module.exports = router;
