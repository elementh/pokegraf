'use strict'

const mysql = require('nodejs-mysql').default

const db = mysql.getInstance({
  host: process.env.DB_HOST,
  port: process.env.DB_PORT,
  user: process.env.DB_USER,
  password: process.env.DB_PASS,
  database: process.env.DB_NAME
})

module.exports = db

db.checkTables = function () {
  db.exec('SHOW TABLES')
  .then(rows => {
    if (rows.Tables_in_pokegraf === 'stats') {
      console.log('found stats')
    }
    console.log(rows)
    // if (rows) {
    //   console.log('Found existing database.')
    // } else {
    //   generateTables()
    // }
  })
  .catch(err => {
    console.log(err)
  })
}
