module.exports = Object.freeze( {
  contentSecurityPolicy: {
    directives: {
      defaultSrc: ['*', "'self'"],
      connectSrc: ["'self'"],
      scriptSrc: ["'self'", "'unsafe-inline'", "'unsafe-eval'"],
      styleSrc: ["'self'", "'unsafe-inline'"],
      fontSrc: ["'self'", 'data: font'],
      imgSrc: ["'self'", 'data: image'],
      mediaSrc: ["'self'"],
      frameSrc: ["'self'"]
    }
  },
  permittedCrossDomainPolicies: {
    permittedPolicies: 'none'
  },
  hsts: {
    maxAge: 5184000,
    includeSubDomains: true
  },
  frameguard: {
    action: 'sameorigin'
  }
} );
