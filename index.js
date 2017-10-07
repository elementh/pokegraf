'use strict'

require('dotenv-safe').load({
  allowEmptyValues: true
})

const pokegraf = require('./src/pokegraf')

const db = require('./src/db')

db.checkTables()

pokegraf.startPolling()
