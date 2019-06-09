'use strict';
const path = require( 'path' );
const express = require( 'express' );
const helmet = require( 'helmet' );
const helmetConfig = require( './configs/helmet' );
const cors = require( 'cors' );
const corsConfig = require( './configs/cors' );
const logger = require( 'morgan' );
const app = express();
const apiRoutes = require( './apiRoutes' );

const APP_STATICS_PATH = path.join( __dirname, '../client/dist/tutor-match' );
const PORT = 9999;

app.use( helmet( helmetConfig ) );
app.use( cors( corsConfig ) );
app.use( express.json() );
app.use( express.urlencoded( { extended: true } ) );
app.use( logger( 'combined' ) );

app.use( '/', express.static( APP_STATICS_PATH ) );
app.use( '/api', apiRoutes );

app.listen( PORT, () => {
  console.log( `The server started listening on http://localhost:${PORT}` );
} );
