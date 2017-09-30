'use strict'
require('dotenv-safe').load({
    allowEmptyValues: true
})

const db = require('./src/db')
const pokegraf = require('./src/pokegraf')

db.checkTables()

// pokegraf.catchThemAll()

pokegraf.startPolling()

