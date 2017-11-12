'use strict'

const typeorm = require('typeorm')

async function connect () {
  return typeorm.createConnection({
    type: 'mysql',
    host: process.env.DB_HOST,
    port: process.env.DB_PORT,
    username: process.env.DB_USER,
    password: process.env.DB_PASS,
    database: process.env.DB_NAME,
    synchronize: true,
    logging: false,
    cache: true,
    entitySchemas: [
      require('./entity/ChatSchema'),
      require('./entity/UserSchema'),
      require('./entity/FusionUsageSchema'),
      require('./entity/PokemonUsageSchema')
    ]
  })
}
module.exports = connect
