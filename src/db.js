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
    if (rows[3] &&
        rows[0].Tables_in_pokegraf === 'stats_commands' &&
        rows[1].Tables_in_pokegraf === 'stats_fusion' &&
        rows[2].Tables_in_pokegraf === 'stats_pokemon' &&
        rows[3].Tables_in_pokegraf === 'stats_users') {
      console.log('Found existing database.')
    } else {
      console.log('Database not found. Creating one...')
      db.generateTables()
    }
  })
  .catch(err => {
    console.log(err)
  })
}

db.generateTables = function () {
  db.exec('CREATE OR REPLACE TABLE `stats_commands` (`name` varchar(45) NOT NULL, `times_used` int(11) DEFAULT NULL, PRIMARY KEY (`name`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;')
  .then(rows => {
    console.log('...stats_commands created...')
  })
  .catch(err => {
    console.log(err)
  })
  db.exec('CREATE OR REPLACE TABLE `stats_fusion` (`id_first` int(11) NOT NULL, `id_second` int(11) NOT NULL, `count` varchar(45) DEFAULT NULL, PRIMARY KEY (`id_first`,`id_second`)) ENGINE=InnoDB DEFAULT CHARSET=utf8')
  .then(rows => {
    console.log('...stats_fusion created...')
  })
  .catch(err => {
    console.log(err)
  })
  db.exec('CREATE OR REPLACE TABLE `stats_pokemon` (`id` int(11) NOT NULL, `count` int(11) DEFAULT NULL, PRIMARY KEY (`id`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;')
  .then(rows => {
    console.log('...stats_pokemon created...')
  })
  .catch(err => {
    console.log(err)
  })
  db.exec('CREATE TABLE `stats_users` (`id` varchar(10) NOT NULL, `number` int(11) DEFAULT \'0\', PRIMARY KEY (`id`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;')
  .then(() => {
    return db.exec('INSERT INTO `pokegraf`.`stats_users` (`id`,`number`) VALUES(\'groups\',0);')
  })
  .then(() => {
    return db.exec('INSERT INTO `pokegraf`.`stats_users` (`id`,`number`) VALUES(\'users\',0);')
  })
  .then(rows => {
    console.log('...stats_users created...')
  })
  .catch(err => {
    console.log(err)
  })
}

db.addUser = function () {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addGroup = function () {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addPokemon = function (id) {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addFusion = function (fId, sId) {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}

db.addCommand = function (command) {
  db.exec()
  .then(rows => {

  })
  .catch(err => {
    console.log(err)
  })
}
