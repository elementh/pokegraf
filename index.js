'use strict'

require('dotenv-safe').load({
  allowEmptyValues: true
})

const database = require('./src/database')
const pokegraf = require('./src/pokegraf')

database().then(() => {
  pokegraf.startPolling()
}).catch(function (error) {
  console.log('Error: ', error)
})
