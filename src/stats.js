'use strict'

const typeorm = require('typeorm')

function middleware () {
  return (ctx, next) => {
    console.log(typeorm.getConnection())
    return next()
  }
}

module.exports = {
  middleware: middleware
}
